using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using cwkGestao.Integracao.Magento.Magentov1;
using cwkGestao.Integracao.Magento.Utils;

namespace cwkGestao.Integracao.Magento.Importacao
{
    public class ImportadorClientes
    {
        private readonly MagentoConfiguracao configuracao;
        private readonly Buscador importador;
        private Filial filial;
        private Hashtable clientesMagentoCadastrados;
        private int proximoCodigoDisponivel;
        private IList<Cidade> cidades;
        private Pessoa clientePessoa;

        public ImportadorClientes(MagentoConfiguracao configuracao, Buscador importador, Filial filial)
        {
            this.configuracao = configuracao;
            this.importador = importador;
            this.filial = filial;

            clientesMagentoCadastrados = PessoaController.Instancia.GetPessoasImportadas();
        }

        public void Importar()
        {
            foreach (var clienteMagento in GetClientesMagento())
            {
                clientePessoa = BuscarCliente(clienteMagento);

                if (clientePessoa == null)
                    IncluirClienteInexistente(clienteMagento);
                else
                    AlterarClienteExistente(clientePessoa, clienteMagento);
            }
        }

        public Pessoa Importar(int id)
        {
            customerCustomerEntity clienteMagento = importador.ImportarCliente(id);
            clientePessoa = BuscarCliente(clienteMagento);

            if (clientePessoa == null)
                if (!clientesMagentoCadastrados.ContainsKey(clienteMagento.customer_id))
                    return IncluirClienteInexistente(clienteMagento);


            return clientePessoa;
        }

        private void AlterarClienteExistente(Pessoa cliente, customerCustomerEntity clienteMagento)
        {
            cliente.BAtivo = true;
            cliente.Nome = (clienteMagento.firstname + " " + clienteMagento.lastname);

            foreach (var clienteEndereco in importador.ImportarClienteEndereco(clienteMagento.customer_id))
            {
                PessoaEndereco pessoaEndereco = cliente.PessoaEnderecos.Where(e => e.IDIntegracao == clienteEndereco.customer_address_id).FirstOrDefault();

                if (pessoaEndereco == null)
                {
                    pessoaEndereco = new PessoaEndereco
                    {
                        Pessoa = cliente
                    };
                    cliente.PessoaEnderecos.Add(pessoaEndereco);
                }
                AtribuirDadosEndereco(clienteMagento, clienteEndereco, pessoaEndereco);
            }

            PessoaController.Instancia.Salvar(cliente, Acao.Alterar);
        }

        private Pessoa IncluirClienteInexistente(customerCustomerEntity clienteMagento)
        {
            proximoCodigoDisponivel = PessoaController.Instancia.MaxCodigoIntegracao();
            var ConfigMagento = MagentoConfiguracaoController.Instancia.GetConfiguracao();
            var Magentov1 = new MagentoSoapV1();
            

            Pessoa cliente = new Pessoa();

            cliente.Codigo = proximoCodigoDisponivel;
            cliente.BCliente = true;
            cliente.BAtivo = true;
            cliente.Filial = filial;
            cliente.DtCadastro = DateTime.Now;
            cliente.IDIntegracao = clienteMagento.customer_id;
            cliente.CNPJ_CPF = (clienteMagento.taxvat ?? "");

            if (clienteMagento.group_id == 2 || clienteMagento.group_id == 5)
            {
                cliente.TipoPessoa = "Jurídica";

                if (clienteMagento.group_id == 5)
                    cliente.BOrgaoPublico = true;

                //no caso de PJ foi necessário a utilização da api V1 para pegar alguns campos
                //os métodos abaixo são implementação em api V1
                //{
                var _cliente = Magentov1.ImportacaoCliente(clienteMagento.customer_id.ToString());

                foreach (var item in _cliente)
                {
                    switch (item.FirstChild.InnerText)
                    {
                        case "nome_fantasia":
                            cliente.Fantasia = item.LastChild.InnerText;
                            break;
                        case "razao_social":
                            cliente.Nome = item.LastChild.InnerText;
                            break;
                        case "ie_rg":
                            cliente.Inscricao = item.LastChild.InnerText;
                            break;
                    }
                   
                }
                //}
            }
            else
            {
                cliente.TipoPessoa = "Física";
                cliente.Nome = (clienteMagento.firstname + " " + clienteMagento.lastname);
            }

            cliente.Sexo = "0";

            cliente.PessoaClientes.Add(new PessoaCliente { Pessoa = cliente });

            int seq = 1;
            foreach (var clienteEndereco in importador.ImportarClienteEndereco(clienteMagento.customer_id))
            {
                PessoaEndereco endereco = new PessoaEndereco();

                endereco.IDIntegracao = clienteEndereco.customer_address_id;
                endereco.Pessoa = cliente;
                endereco.Sequencia = seq;
                endereco.BPrincipal = seq == 1 ? true : false;

                AtribuirDadosEndereco(clienteMagento, clienteEndereco, endereco);
                cliente.PessoaEnderecos.Add(endereco);

                if (!String.IsNullOrEmpty(clienteEndereco.fax))
                {
                    PessoaTelefone fax = new PessoaTelefone();
                    int seqFax = cliente.PessoaTelefones.Count() + 1;

                    fax.Pessoa = cliente;
                    fax.Sequencia = seqFax;
                    fax.BPrincipal = seqFax == 1 ? true : false;
                    fax.Numero = clienteEndereco.fax;
                    cliente.PessoaTelefones.Add(fax);
                }
                if (!String.IsNullOrEmpty(clienteEndereco.telephone))
                {
                    PessoaTelefone telephone = new PessoaTelefone();
                    int seqTelephone = cliente.PessoaTelefones.Count() + 1;

                    telephone.Pessoa = cliente;
                    telephone.Sequencia = seqTelephone;
                    telephone.BPrincipal = seqTelephone == 1 ? true : false;
                    telephone.Numero = clienteEndereco.telephone;
                    cliente.PessoaTelefones.Add(telephone);
                }

                seq++;
            }

            if (cliente.PessoaEnderecos.Count == 0)
                cliente.PessoaEnderecos.Add(new PessoaEndereco
                {
                    Pessoa = cliente,
                    BPrincipal = true,
                    BAtivo = true,
                    Sequencia = 1
                });

             PessoaController.Instancia.Salvar(cliente, Acao.Incluir);

            return cliente;
        }

        private void AtribuirDadosEndereco(customerCustomerEntity clienteMagento, customerAddressEntityItem clienteMagentoEndereco, PessoaEndereco pessoaEndereco)
        {
            var ExisteEndereco = PessoaEnderecoController.Instancia.GetAll().Where(i => i.IDIntegracao == clienteMagentoEndereco.customer_address_id).FirstOrDefault();

            var enderecoCompleto = clienteMagentoEndereco.street.Split('\n');

            if (ExisteEndereco == null)
            {
                pessoaEndereco.IDIntegracao = clienteMagentoEndereco.customer_address_id;
                if ((enderecoCompleto.Count() - 1) >= 0)
                    pessoaEndereco.Endereco = enderecoCompleto[0];

                if ((enderecoCompleto.Count() - 1) >= 1)
                    pessoaEndereco.Numero = enderecoCompleto[1];

                if ((enderecoCompleto.Count() - 1) >= 2)
                    pessoaEndereco.Complemento = enderecoCompleto[2];

                if ((enderecoCompleto.Count() - 1) >= 3)
                    pessoaEndereco.Bairro = enderecoCompleto[3];

                pessoaEndereco.CEP = clienteMagentoEndereco.postcode;
                pessoaEndereco.Contato = (clienteMagentoEndereco.firstname + " " + clienteMagentoEndereco.lastname);
                pessoaEndereco.Telefone = clienteMagentoEndereco.telephone;
                pessoaEndereco.BEntrega = clienteMagentoEndereco.is_default_shipping;
                pessoaEndereco.BCobranca = clienteMagentoEndereco.is_default_billing;

                var cidade = GetCidade(clienteMagentoEndereco);

                if (cidade == null)
                    throw new Exception(String.Format("Processo interrompido. Ocorreu um problema ao importar a cidade '{0}', do cliente '{1}'.", clienteMagentoEndereco.city, clienteMagento.firstname + " " + clienteMagento.lastname));
                else
                    pessoaEndereco.Cidade = cidade;
            }
            else
            {
                pessoaEndereco = ExisteEndereco;
            }

        }

        private Cidade GetCidade(customerAddressEntityItem clienteEndereco)
        {
            if (cidades == null)
                cidades = CidadeController.Instancia.GetAll();

            var cidadesNome = cidades.Where(c => c.Nome.ToUpper() == clienteEndereco.city.ToUpper()).ToList();

            if (cidadesNome.Count() > 1)
                cidadesNome = cidadesNome.Where(c => c.UF.Sigla.ToUpper() == clienteEndereco.region.ToUpper()).ToList();

            return cidadesNome.FirstOrDefault();
        }

        private IEnumerable<customerCustomerEntity> GetClientesMagento()
        {
            return importador.ImportarClientes();
        }

        private Pessoa BuscarCliente(customerCustomerEntity clienteMagento)
        {
            if (clientesMagentoCadastrados.Contains(clienteMagento.customer_id))
                return (Pessoa)clientesMagentoCadastrados[clienteMagento.customer_id];

            return null;
        }
    }
}
