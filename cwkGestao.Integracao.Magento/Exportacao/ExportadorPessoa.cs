using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.WebService;
using Ez.Newsletter.MagentoApi;
using cwkGestao.Negocio;
using System.Threading;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class ExportadorPessoa : IExportador<Pessoa>
    {
        //private Ez.Newsletter.MagentoApi.Customer PessoaMagento;
        //private Ez.Newsletter.MagentoApi.CustomerAddress PessoaEnderecoMagento;
        private Comunicador ComunicadorMagento;
        //string IDIntegracao, IDIntegracaoEndereco;

        public ExportadorPessoa(Comunicador comunicador)
        {
            ComunicadorMagento = comunicador;
        }

        
        public void Exportar(Pessoa pessoa)
        {
            /*
            PessoaMagento = new Ez.Newsletter.MagentoApi.Customer();
            PessoaEnderecoMagento = new Ez.Newsletter.MagentoApi.CustomerAddress();

            PreparaPessoa(pessoa);
            PreparaPessoaEndereco(pessoa);

            if (pessoa.IDIntegracao > 0)
            {
                ComunicadorMagento.AlterarPessoa(PessoaMagento, pessoa.IDIntegracao);
                ComunicadorMagento.AlterarPessoaContato(PessoaEnderecoMagento, ((PessoaEndereco)pessoa.EnderecoPrincipal()).IDIntegracao);
            }
            else
            {
                PessoaEnderecoMagento.customer_id = ComunicadorMagento.IncluirPessoa(PessoaMagento);

                IDIntegracaoEndereco = ComunicadorMagento.IncluirPessoaEndereco(PessoaEnderecoMagento);

                pessoa.IDIntegracao = Convert.ToInt32(PessoaEnderecoMagento.customer_id);
                ((PessoaEndereco)pessoa.EnderecoPrincipal()).IDIntegracao = Convert.ToInt32(IDIntegracaoEndereco);
            }

            PessoaController.Instancia.Salvar(pessoa, Acao.Alterar);
            */
        }

        /*
        public string CriptografiaMD5(string PessoaSenha)
        {
            System.Security.Cryptography.MD5 MD5 = System.Security.Cryptography.MD5.Create();

            byte[] HashMD5 = MD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(PessoaSenha));

            System.Text.StringBuilder SenhaMD5 = new System.Text.StringBuilder();

            for (int i = 0; i < HashMD5.Length; i++)
                SenhaMD5.Append(HashMD5[i].ToString("X2"));

            return SenhaMD5.ToString();
        }

        public void PreparaPessoa(Pessoa pessoa)
        {
            string[] NomeCompletoPessoa = pessoa.Nome.Trim().Split(' ');

            PessoaMagento.taxvat = pessoa.CNPJ_CPF;
            PessoaMagento.prefix = pessoa.Tratamento;

            for (int i = 0; i < NomeCompletoPessoa.Count(); i++)
            {
                if (i == 0)
                    PessoaMagento.firstname = NomeCompletoPessoa[i];
                else
                    if (i == 1)
                        PessoaMagento.lastname += NomeCompletoPessoa[i];
                    else
                        PessoaMagento.lastname += " " + NomeCompletoPessoa[i];
            }

            if (pessoa.Sexo == "0")
                PessoaMagento.gender = "123";
            else
                PessoaMagento.gender = "124";

            var PessoaEmail = pessoa.PessoaEmails.OrderByDescending(p => p.BContato);

            if (PessoaEmail.Count() > 0)
                PessoaMagento.email = PessoaEmail.First().Email;
            else
                PessoaMagento.email = "sememail@" + PessoaMagento.taxvat + ".com";

            var PessoaSenhaLojaVirtual = global::Modelo.Criptografia.Decriptografar(pessoa.SenhaLojaVirtual);

            PessoaMagento.password_hash = CriptografiaMD5(PessoaSenhaLojaVirtual);
            PessoaMagento.store_id = "1";
            PessoaMagento.website_id = "1";
        }

        public void PreparaPessoaEndereco(Pessoa pessoa)
        {
            if (pessoa.IDIntegracao > 0)
                PessoaEnderecoMagento.customer_id = pessoa.IDIntegracao.ToString();

            PessoaEnderecoMagento.prefix = PessoaMagento.prefix;
            PessoaEnderecoMagento.firstname = PessoaMagento.firstname;
            PessoaEnderecoMagento.lastname = PessoaMagento.lastname;
            PessoaEnderecoMagento.city = ((PessoaEndereco)pessoa.EnderecoPrincipal()).Cidade.Nome;
            PessoaEnderecoMagento.region = ((PessoaEndereco)pessoa.EnderecoPrincipal()).Cidade.UF.Sigla;
            PessoaEnderecoMagento.country_id = "BR";
            PessoaEnderecoMagento.postcode = ((PessoaEndereco)pessoa.EnderecoPrincipal()).CEP;
            PessoaEnderecoMagento.street = ((PessoaEndereco)pessoa.EnderecoPrincipal()).Endereco;
            PessoaEnderecoMagento.telephone = pessoa.TelefonePrincipal;
            PessoaEnderecoMagento.is_default_billing = true;
            PessoaEnderecoMagento.is_default_shipping = true;
        }
        */
    }
}
