using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using MercadoLibre.SDK;
using cwkGestao.Integracao.MercadoLivre.WebService;
using Modelo;
using Modelo.VendaAtributos;
using cwkGestao.Integracao.MercadoLivre.Util;

namespace cwkGestao.Integracao.MercadoLivre.Negocio
{
    public class ImportadorPedidos
    {
        MercadoLivreConfiguracao mConf = MercadoLivreConfiguracaoController.Instancia.GetAll().First();
        Validar validacoes = new Validar();

        WS_Autorizacao ws_autorizacao = new WS_Autorizacao();
        WS_Pedido ws_pedido = new WS_Pedido();
        WS_Usuario ws_user = new WS_Usuario();

        public void ImportarPedidosMercadoLivre()
        {
            Meli m = ws_autorizacao.InstanciaMeli();


            Pessoa pessoa = new Pessoa();
            Produto produto = new Produto();

            if (mConf.IDUsuarioMercadoLivre == 0)
            {
                ws_user.GetIdUserLogado(mConf);
            }


            try
            {
                var vResposta = ws_pedido.GetAllPedidos(mConf, m);

                foreach (var venda in vResposta.Results)
                {
                    if (DateTime.Parse(venda.DateClosed) >= mConf.DtInicioImportacao)
                    {
                        IList<PedidoItem> lPedidoItem = new List<PedidoItem>();
                        IList<OrderItem> lSincroniaPendente = new List<OrderItem>();
                        PedidoItem pItem = new PedidoItem();

                        pessoa = validacoes.ValidarCliente(venda, mConf);
                        bool erroAoBaixar = false;

                        foreach (var prod in venda.OrderItems)
                        {
                            produto = validacoes.ValidarProduto(prod.Item);
                            if (produto != null)
                            {
                                pItem.Produto = produto;
                                pItem.CodigoProduto = produto.Codigo;
                                pItem.Quantidade = prod.Quantity;
                                pItem.Valor = ProdutoController.Instancia.getPreco(produto, mConf.TabelaPreco, TipoPrecoType.Normal);
                                pItem.ValorProduto = Convert.ToDecimal(prod.UnitPrice);
                                if (pItem.Valor == 0)
                                {
                                    MontarPedidoSincroniaPendente(pessoa, venda, "Erro: o item " + produto.Nome + ", Cod: "  + prod.Item.Id.Substring(3) + ",  possui valor 0,00 na tabela de preço configurada. Por favor verifique!");
                                    erroAoBaixar = true;
                                    break;
                                }
                                pItem.ProdutoNome = produto.Nome;
                                pItem.Dt = DateTime.Parse(venda.DateClosed);
                                pItem.IncData = DateTime.Now;
                                pItem.SubTotal = pItem.Valor * pItem.Quantidade;
                                pItem.Total = pItem.Valor * pItem.Quantidade;
                                pItem.Unidade = pItem.Produto.Unidade.Sigla;
                                pItem.ValorCalculado = ProdutoController.Instancia.getPreco(produto, mConf.TabelaPreco, TipoPrecoType.Normal);
                                pItem.NomeMagento = prod.Item.Title;
                                pItem.Sequencia = 1;

                                try
                                {
                                    NCM ncm = NCMController.Instancia.LoadObjectById(pItem.Produto.ID_NCM);
                                    pItem.AliquotaIPI = pItem.Produto.AliquotaIPI == 0 ? ncm.Ipi : pItem.Produto.AliquotaIPI;
                                }
                                catch (Exception)
                                {
                                    pItem.AliquotaIPI = 0;
                                }
                                lPedidoItem.Add(pItem);
                            }
                            else
                                erroAoBaixar = true;
                        }
                        if (!erroAoBaixar)
                            MontarPedido(pessoa, lPedidoItem, mConf, venda);
                        else
                            MontarPedidoSincroniaPendente(pessoa, venda, "");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }                 
        }

        public void GetPedidoResolverSincronismo(MercadoLivreConfiguracao mConf, SincronismoPendenteMercadoLivre sinc)
        {
            Meli m = ws_autorizacao.InstanciaMeli();
            Pessoa pessoa = new Pessoa();
            Produto produto = new Produto();
            PedidoItem pItem = new PedidoItem();

            Result venda = ws_pedido.GetPedidoById(mConf, m, sinc.PedidoMercadoLivre.ToString());
            pessoa = validacoes.ValidarCliente(venda, mConf);

            IList<OrderItem> lSincroniaPendente = new List<OrderItem>();
            IList<PedidoItem> lPedidoItem = new List<PedidoItem>();
            bool erroAoBaixar = false;
            foreach (var prod in venda.OrderItems)
            {
                //SE NÃO ENCONTRAR NENHUM, DEVE SALVAR NA TABELA DE SINCRONIA.
                produto = validacoes.ValidarProduto(prod.Item);
                if (produto != null)
                {
                    pItem.Ent_Sai = 2; // <-- tipo saída, incluido porque estava invertendo o UF do cliente/filial
                    pItem.Produto = produto;
                    pItem.CodigoProduto = produto.Codigo;
                    pItem.Quantidade = prod.Quantity;
                    pItem.Valor = ProdutoController.Instancia.getPreco(produto, mConf.TabelaPreco, TipoPrecoType.Normal);
                    pItem.ValorProduto = Convert.ToDecimal(prod.UnitPrice);
                    pItem.ProdutoNome = produto.Nome;
                    pItem.Dt = DateTime.Parse(venda.DateClosed);
                    pItem.IncData = DateTime.Now;
                    pItem.SubTotal = pItem.Valor * pItem.Quantidade;
                    pItem.Total = pItem.Valor * pItem.Quantidade;
                    pItem.Unidade = pItem.Produto.Unidade.Sigla;
                    pItem.ValorCalculado = ProdutoController.Instancia.getPreco(produto, mConf.TabelaPreco, TipoPrecoType.Normal);
                    pItem.NomeMagento = prod.Item.Title;
                    pItem.Sequencia = 1;
                    try
                    {
                        NCM ncm = NCMController.Instancia.LoadObjectById(pItem.Produto.ID_NCM);
                        pItem.AliquotaIPI = pItem.Produto.AliquotaIPI == 0 ? ncm.Ipi : pItem.Produto.AliquotaIPI;
                    }
                    catch (Exception)
                    {
                        pItem.AliquotaIPI = 0;
                    }

                    lPedidoItem.Add(pItem);
                }
                else
                    erroAoBaixar = true;
            }
            if (!erroAoBaixar)
            {
                MontarPedido(pessoa, lPedidoItem, mConf, venda);
                SincronismoPendenteMercadoLivreController.Instancia.Salvar(sinc, Acao.Excluir);
            }
            else
                MontarPedidoSincroniaPendente(pessoa, venda, "");
        }

        public void MontarPedido(Pessoa cliente, IList<PedidoItem> itensPedido, MercadoLivreConfiguracao mConf, Result venda)
        {
            Pedido pedido = PedidoController.Instancia.LoadObjectByIdMercadoLivre(venda.Id);

            if (pedido == null)
            {
                pedido = new Pedido();
                pedido.Status = (StatusOrcamento)mConf.TipoOrcamento.Status;
                pedido.Ent_Sai = 2;
                pedido.Items = itensPedido;
                pedido.Pessoa = cliente;
                pedido.PessoaNome = cliente.Nome;
                pedido.Filial = mConf.Filial;
                pedido.TabelaPreco = mConf.TabelaPreco;
                pedido.Condicao = mConf.Condicao;
                pedido.Vendedor = mConf.Vendedor;
                pedido.IDVendedor = mConf.Vendedor.PessoaVendedores.FirstOrDefault().ID;
                pedido.LocalEstoquePedido = mConf.LocalEstoque;
                pedido.TipoPedido = mConf.TipoOrcamento;
                pedido.IDPedidoMercadoLivre = venda.Id;
                pedido.DtDigitacao = DateTime.Now;
                pedido.ValorFrete = venda.Shipping.Cost != null ? (decimal)venda.Shipping.Cost : 0;
                pedido.TotalPedido = pedido.Items[0].Valor + pedido.ValorFrete;
                pedido.Dt = DateTime.Parse(venda.DateClosed);
                pedido.PessoaUF = pedido.Pessoa.PessoaEnderecos[0].Cidade.UF == null ? "PR" : pedido.Pessoa.PessoaEnderecos[0].Cidade.UF.Sigla;
                pedido.PessoaBairro = pedido.Pessoa.PessoaEnderecos[0].Bairro;
                pedido.PessoaCEP = pedido.Pessoa.PessoaEnderecos[0].CEP == null ? "" : pedido.Pessoa.PessoaEnderecos[0].CEP;
                pedido.PessoaCidade = pedido.Pessoa.PessoaEnderecos[0].Cidade.Nome;
                pedido.PessoaEndereco = pedido.Pessoa.PessoaEnderecos[0].Endereco;
                itensPedido[0].Pedido = pedido;

                Configuracao Conf = ConfiguracaoController.Instancia.GetConfiguracao();               
                pedido.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);
                pedido.RepassarDescontoPedidoParaItem();
                pedido.RecalcularRateios(Conf, pedido.Pessoa.bSTRevenda);
                pedido.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);
               
                pedido.Parcelas = CondicaoController.Instancia.GerarParcelas(pedido);

                PedidoController.Instancia.Salvar(pedido, Acao.Incluir);
            }
        }

        public void MontarPedidoSincroniaPendente(Pessoa cliente, Result venda, string errotratado)
        {
            SincronismoPendenteMercadoLivre sPendente =
                SincronismoPendenteMercadoLivreController.Instancia.hqlLoadByIdMercadoLivre(venda.Id);

            Pedido pedido = PedidoController.Instancia.LoadObjectByIdMercadoLivre(venda.Id);

            if (sPendente == null && pedido == null)
            {
                sPendente = new SincronismoPendenteMercadoLivre();
                sPendente.IdCliente = cliente;
                sPendente.PedidoMercadoLivre = venda.Id;
                StringBuilder erro = new StringBuilder();
                erro.Append("Erro: o item ");
                foreach (var item in venda.OrderItems)
                {
                    erro.Append(item.Item.Title + ", Cod: " + item.Item.Id.Substring(3) + ",");
                }
                erro.Append("não possui um código para Mercado Livre configurado.");
                sPendente.Erro = erro.ToString();
                if (errotratado != "")
                {
                    sPendente.Erro = errotratado;
                }
                SincronismoPendenteMercadoLivreController.Instancia.Salvar(sPendente, Acao.Incluir);

            }
        }
    }
}
