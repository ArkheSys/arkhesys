using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using Ez.Newsletter.MagentoApi;
using cwkGestao.Integracao.Magento.Importacao;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using cwkGestao.Negocio.Tributacao;
using cwkGestao.Negocio.Tributacao.Impl;

namespace cwkGestao.Integracao.Magento.Utils
{
    public class PedidosMagento
    {
        private readonly MagentoConfiguracao configuracao;
        private readonly Buscador importador;
        private ImportadorClientes importarCliente;
        private ImportadorProdutos importarProduto;
        private Filial filial;
        private IList<Cidade> cidades;
        private Hashtable pessoasMagentoCadastrados;
        private Hashtable produtosMagentoCadastrados;
        private int proximoCodigoDisponivel;
        private int proximoNumeroDisponivel;
        private IList<ProdutoSKU> listaProdutoSKU;
        private decimal totalPedidoItens = 0;
        private Produto produtoSelecionado;

        public PedidosMagento(MagentoConfiguracao configuracao, Buscador importador, Filial filial)
        {
            this.configuracao = configuracao;
            this.importador = importador;
            this.filial = filial;

            importarCliente = new ImportadorClientes(configuracao, importador, filial);
            importarProduto = new ImportadorProdutos(configuracao, importador, filial);

            
            
            produtosMagentoCadastrados = ProdutoController.Instancia.GetProdutosImportados();
        }



        public void IncluirPedido(salesOrderEntity pedidoMagento)
        {

            Pedido pedidoGestao = new Pedido();

            var PedidosPendente = new SincronismoPendente();
            var MagentoV1 = new MagentoSoapV1();

            bool ProdutoNaoEncontrado = false;

            var aux = "";

            foreach (var item in pedidoMagento.items)
            {
                var listaSku = ProdutoSKUController.Instancia.GetProdutoPorSKU(item.sku);
                
                if (listaSku.Count == 0)
                {
                    ProdutoNaoEncontrado = true;

                    if (item.product_type != "configurable")
                    {
                        aux += item.sku + ", ";
                    }
                    
                }
            }

            if (ProdutoNaoEncontrado)
            {
                var Exist = SincronismoPendenteController.Instancia.GetPedidoPendente(Convert.ToInt32(pedidoMagento.order_id));
                customerCustomerEntity clienteMagentoV2 = importador.ImportarCliente(Convert.ToInt32(pedidoMagento.customer_id));

                PedidosPendente.Erro = "Produtos Magento " + aux + " Não Encontrado pelo SKU";

                if (clienteMagentoV2.group_id == 2 || clienteMagentoV2.group_id == 5)
                {
                    var _clienteMagentoV1 = MagentoV1.ImportacaoCliente(pedidoMagento.customer_id);

                    foreach (var item in _clienteMagentoV1)
                    {
                        switch (item.FirstChild.InnerText)
                        {
                            case "razao_social":
                                PedidosPendente.ClienteNome = item.LastChild.InnerText;
                                break;
                        }
                    }
                }
                else
                {
                    PedidosPendente.ClienteNome = pedidoMagento.customer_firstname + " " + pedidoMagento.customer_lastname;
                }
                
                PedidosPendente.IDIntegracao = Convert.ToInt32(pedidoMagento.order_id);
                PedidosPendente.IDPedidoMagento = Convert.ToInt32(pedidoMagento.increment_id);

                if (Exist != null)
                {
                    PedidosPendente.ID = Exist.ID;
                    SincronismoPendenteController.Instancia.Salvar(PedidosPendente, Acao.Alterar);
                }
                else
                    SincronismoPendenteController.Instancia.Salvar(PedidosPendente, Acao.Incluir);

                throw new Exception("Pedido nº " + pedidoMagento.increment_id + " Foi adicionado a lista de pendências \n" +
                "Verifique a tabela de pendências.");
            }

            pedidoGestao.Pessoa = BuscarCliente(pedidoMagento.customer_id);

            if (pedidoGestao.Pessoa == null)
                pedidoGestao.Pessoa = importarCliente.Importar(Convert.ToInt32(pedidoMagento.customer_id));

            PreencherDadosPedido(pedidoGestao, pedidoMagento);

            GerarItens(pedidoGestao, pedidoMagento);
            GerarParcelas(pedidoGestao);

            PedidoController.Instancia.Salvar(pedidoGestao, Acao.Incluir);

        }

        private void GerarItens(Pedido pedidoGestao, salesOrderEntity pedidoMagento)
        {
            int sequencia = 1;

            try
            {
                foreach (var produtoMagento in pedidoMagento.items)
                {
                    if (produtoMagento.product_type != "configurable")
                    {
                        PedidoItem pedidoItem = null;

                        listaProdutoSKU = ProdutoSKUController.Instancia.GetProdutoPorSKU(produtoMagento.sku);

                        if (listaProdutoSKU.Count() > 0)
                        {
                           
                            produtoSelecionado = listaProdutoSKU.FirstOrDefault().Produto;

                            if (configuracao.BAgruparProdutos)
                                pedidoItem = pedidoGestao.Items.Where(p => p.Produto.ID == produtoSelecionado.ID).FirstOrDefault();
                        }

                        if (pedidoItem == null)
                        {
                            pedidoItem = new PedidoItem();

                            pedidoItem.Dt = pedidoGestao.Dt;
                            pedidoItem.Ent_Sai = pedidoGestao.Ent_Sai;
                            pedidoItem.Pedido = pedidoGestao;
                            pedidoItem.Sequencia = sequencia;

                            decimal produtoPreco = ProdutoController.Instancia.getPreco(produtoSelecionado, pedidoGestao.TabelaPreco, TipoPrecoType.Normal);

                            pedidoItem.ValorCalculado = produtoPreco;
                            pedidoItem.Valor = produtoPreco;
                            pedidoItem.Quantidade = 0m;
                            pedidoItem.Produto = produtoSelecionado;
                            pedidoItem.Unidade = pedidoItem.Produto.Unidade.Sigla;
                            pedidoItem.ProdutoNome = pedidoItem.Produto.Nome;
                            pedidoItem.ProdutoDescReduzida = pedidoItem.Produto.DescReduzida;

                            pedidoGestao.Items.Add(pedidoItem);
                        }

                        if (MagentoConfiguracaoController.Instancia.GetConfiguracao().BAtualizarInfAdicional)
                            if (String.IsNullOrEmpty(pedidoItem.InformacoesAdicionais))
                                pedidoItem.InformacoesAdicionais = pedidoMagento.items[sequencia - 1].name;
                            else
                                pedidoItem.InformacoesAdicionais += "\n\n" + pedidoMagento.items[sequencia - 1].name;

                        pedidoItem.NomeMagento = produtoMagento.name;
                        pedidoItem.Quantidade += pedidoMagento.items[sequencia - 1].qty_invoiced.ValorMagentoToDecimal();
                        pedidoItem.SubTotal = (pedidoItem.Quantidade * pedidoItem.Valor);
                        pedidoItem.Total = pedidoItem.SubTotal;


                        if (pedidoItem.Total > 0)
                            if (configuracao.BAgruparProdutos)
                                totalPedidoItens = pedidoItem.Total;
                            else
                                totalPedidoItens += pedidoItem.Total;

                        sequencia++;


                        //Calcular ST
                        pedidoGestao.TipoPedido = TipoPedidoController.Instancia.LoadObjectById(pedidoGestao.TipoPedido.ID);

                        if (pedidoGestao.Filial != null && pedidoGestao.TipoPedido != null && pedidoGestao.TipoPedido.CalcularST)
                        {
                            foreach (var item in pedidoGestao.Items)
                            {
                                var produto = ProdutoController.Instancia.LoadObjectById(item.Produto.ID);
                                item.Produto = produto;
                                var tributacao = new Tributacao(new PedidoItemTributadoMediator(item, pedidoGestao));
                                tributacao.CalculaIcms();
                            }
                        }
                        else
                        {
                            foreach (var item in pedidoGestao.Items)
                            {
                                item.ValorRetidoICMS = 0;
                                item.BaseICMSSubst = 0;
                            }
                        }
                        Configuracao Conf = ConfiguracaoController.Instancia.GetConfiguracao();
                        pedidoGestao.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private Produto GetProduto(string id)
        {
            var product_id = Convert.ToInt32(id);

            if (produtosMagentoCadastrados.ContainsKey(product_id))
                return (Produto)produtosMagentoCadastrados[product_id];

            return null;
        }

        
        private void PreencherDadosPedido(Pedido pedidoGestao, salesOrderEntity pedidoMagento)
        {
            proximoCodigoDisponivel = PedidoController.Instancia.MaxCodigoIntegracao();
            proximoNumeroDisponivel = PedidoController.Instancia.MaxNumero(filial, configuracao.TipoPedido);

            var confgMagento = MagentoConfiguracaoController.Instancia.GetConfiguracao();
            var Vendedor = PessoaVendedorController.Instancia.GetVendedorPorIDPessoa(confgMagento.Vendedor.ID);

            pedidoGestao.Filial = filial;
            pedidoGestao.Ent_Sai = (int)InOutType.Saída;
            pedidoGestao.Dt = Convert.ToDateTime(pedidoMagento.created_at);
            pedidoGestao.DtPrazoEntrega = pedidoGestao.Dt.AddDays(1);
            pedidoGestao.DtDigitacao = pedidoGestao.Dt;
            pedidoGestao.IDIntegracao = Convert.ToInt32(pedidoMagento.order_id);
            pedidoGestao.CodigoPedidoMagento = Convert.ToInt32(pedidoMagento.increment_id);
            pedidoGestao.TipoPedido = configuracao.TipoPedido;
            pedidoGestao.LocalEstoquePedido = configuracao.TipoPedido.LocalEstoque;
            pedidoGestao.Vendedor = configuracao.Vendedor;
            pedidoGestao.IDVendedor = Vendedor.ID; //pedidoGestao.Vendedor.PessoaVendedores.First().ID;
            pedidoGestao.TabelaPreco = configuracao.TabelaPreco;
            pedidoGestao.Condicao = configuracao.Condicao;
            pedidoGestao.Status = StatusOrcamento.AguardandoExpedicao;
            pedidoGestao.Codigo = proximoCodigoDisponivel;
            pedidoGestao.Numero = proximoNumeroDisponivel;
           

            if (pedidoMagento.shipping_amount == "0")
                pedidoGestao.TipoFrete = TipoFrete.SemFrete;
            else
                pedidoGestao.TipoFrete = TipoFrete.Destinatario;

            pedidoGestao.ValorFrete = pedidoMagento.shipping_amount.ValorMagentoToDecimal();
            pedidoGestao.ValorDesconto = pedidoMagento.base_discount_amount.ValorMagentoToDecimal() * -1;
            pedidoGestao.PessoaNome = pedidoGestao.Pessoa.Nome;

            PreencherDadosCliente(pedidoGestao, pedidoMagento);
        }

        private void PreencherDadosCliente(Pedido pedidoGestao, salesOrderEntity pedidoMagento)
        {
            if (pedidoGestao.Pessoa != null )//&& pedidoMagento.customer_id != null)
            {
                if (pedidoGestao.Pessoa.TipoPessoa == "Física")
                    pedidoGestao.PessoaInscRG = pedidoGestao.Pessoa.RG;
                else
                    pedidoGestao.PessoaInscRG = pedidoGestao.Pessoa.Inscricao;

                pedidoGestao.PessoaNome = pedidoGestao.Pessoa.Nome;
                pedidoGestao.PessoaCNPJCPF = pedidoGestao.Pessoa.CNPJ_CPF;
                pedidoGestao.PessoaTelefone = pedidoGestao.Pessoa.TelefonePrincipal;

                var Endereco = pedidoGestao.Pessoa.EnderecoPrincipal();

                pedidoGestao.PessoaBairro = Endereco.Bairro;
                pedidoGestao.PessoaCEP = Endereco.CEP;

                if (Endereco.Cidade == null)
                    throw new Exception("A cidade do cliente " + pedidoGestao.Pessoa.Nome + " não foi definida.");

                pedidoGestao.PessoaCidade = Endereco.Cidade.Nome;
                pedidoGestao.PessoaUF = Endereco.Cidade.UF.Sigla;

                pedidoGestao.PessoaEndereco = Endereco.Endereco;
                pedidoGestao.EnderecoEntrega = BuscarEnderecoDeEntrega(pedidoGestao, pedidoMagento);
            }
            
        }

        private PessoaEndereco BuscarEnderecoDeEntrega(Pedido pedidoGestao, salesOrderEntity pedidoMagento)
        {
            var EnderecoPedido = pedidoGestao.Pessoa.PessoaEnderecos.Where(i => i.IDIntegracao == Convert.ToInt32(pedidoMagento.shipping_address.address_id)).FirstOrDefault();

            if (EnderecoPedido == null)
            {
                EnderecoPedido = new PessoaEndereco();

                EnderecoPedido.Endereco = pedidoMagento.shipping_address.street.Replace("\n"," ");
                EnderecoPedido.CEP = pedidoMagento.shipping_address.postcode;
                EnderecoPedido.Contato = (pedidoMagento.shipping_address.firstname + " " + pedidoMagento.shipping_address.lastname);
                EnderecoPedido.Telefone = pedidoMagento.shipping_address.telephone;
                EnderecoPedido.BEntrega = true;
                EnderecoPedido.IDIntegracao = Convert.ToInt32(pedidoMagento.shipping_address.address_id);

                if (pedidoGestao.Pessoa.PessoaEnderecos.Count == 0)
                {
                    EnderecoPedido.BPrincipal = true;
                    EnderecoPedido.Sequencia = 1;
                }
                else
                    EnderecoPedido.Sequencia = pedidoGestao.Pessoa.PessoaEnderecos.Max(p => p.Sequencia) + 1;

                var Cidade = GetCidade(pedidoMagento.shipping_address);

                EnderecoPedido.Cidade = Cidade;

                EnderecoPedido.Pessoa = pedidoGestao.Pessoa;
                pedidoGestao.Pessoa.PessoaEnderecos.Add(EnderecoPedido);

                PessoaEnderecoController.Instancia.RegisterNewIntoTransaction(EnderecoPedido);
            }

            return EnderecoPedido;
        }

        private Cidade GetCidade(salesOrderAddressEntity endereco)
        {
            if (cidades == null)
                cidades = CidadeController.Instancia.GetAll();

            var CidadesNome = cidades.Where(c => c.Nome.ToUpper() == endereco.city.ToUpper());

            if (CidadesNome.Count() > 1)
                CidadesNome = CidadesNome.Where(c => c.UF.Sigla.ToUpper() == endereco.region.ToUpper());

            return CidadesNome.FirstOrDefault();
        }

        private Pessoa BuscarCliente(string id)
        {
            pessoasMagentoCadastrados = PessoaController.Instancia.GetPessoasImportadas();

            var customer_id = Convert.ToInt32(id);

            if (pessoasMagentoCadastrados.ContainsKey(customer_id))
                return (Pessoa)pessoasMagentoCadastrados[customer_id];

            return null;
        }

        private void GerarParcelas(Pedido pedidoGestao)
        {
            pedidoGestao.Parcelas = CondicaoController.Instancia.GerarParcelas(pedidoGestao);
        }

        public bool DesconsiderarPedido(salesOrderEntity pedidoMagento)
        {
           var _pedidosMagentoCadastrados = PedidoController.Instancia.GetHashIdsIntegracao();
           var _pedidosMagentoPendentes = PedidoController.Instancia.GetHashIdsIntegracaoPendente();

           return String.IsNullOrEmpty(pedidoMagento.order_id) || _pedidosMagentoCadastrados.Contains(Convert.ToInt32(pedidoMagento.order_id)) || _pedidosMagentoPendentes.Contains(Convert.ToInt32(pedidoMagento.order_id)) || String.IsNullOrEmpty(pedidoMagento.customer_id);
        }
    }
}
