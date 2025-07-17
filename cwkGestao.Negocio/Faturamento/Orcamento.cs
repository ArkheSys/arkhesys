using System;
using System.Collections.Generic;
using System.Linq;

using cwkGestao.Integracao.SHL;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Faturamento.Holders;
using cwkGestao.Negocio.Utils;

namespace cwkGestao.Negocio.Faturamento
{
    public class Orcamento
    {
        public Nota Nota { get; private set; }
        private IList<Imei> listaDeImeis;

        public Orcamento(IList<Pedido> pedidos, IList<FinanceiroParcela> parcelas, DateTime faturamento,
            Condicao condicao, TipoNota tipoNota, string observacao1, string observacao2,
            string observacao3, PessoaEndereco entrega, List<pxyFormaPagamentoBaixaDoc> formasDePagamento, bool VemDoFrenteDeCaixa, bool VemDoDelivery, bool GerarFinanceiro, bool FaturarPedido, int? Serie)
        {
            try
            {
                if (tipoNota.Categoria == TipoNota.CategoriaMovimentacao.Produtos)
                {
                    Nota notaFinalizada = ConstruirNota(tipoNota, pedidos, faturamento, observacao1, observacao2, observacao3, condicao, parcelas, Serie, VemDoFrenteDeCaixa);

                    FinalizaExpedicao(pedidos, notaFinalizada, entrega);

                    if (notaFinalizada.Ent_Sai == InOutType.Saída)
                    {
                        notaFinalizada.Codigo = NotaController.Instancia.GetNovoCodigoNota();

                        NotaController.Instancia.RegisterNewIntoTransaction(notaFinalizada);
                        Nota = notaFinalizada;

                        FinalizaNota(notaFinalizada, tipoNota, formasDePagamento, pedidos, VemDoFrenteDeCaixa, VemDoDelivery, GerarFinanceiro, FaturarPedido);

                        if (listaDeImeis != null && listaDeImeis.Any())
                            ImeiController.Instancia.RegisterNewIntoTransaction(listaDeImeis);

                        CommitarTransacao();
                    }

                    Nota = notaFinalizada;
                }

                if (tipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos)
                {
                    //OSOrdemServicoController.Instancia.
                }
            }
            catch (Exception e)
            {
                NotaController.Instancia.RollBackUnitOfWorkTransaction();
                string mensagem = e.InnerException == null ? e.Message : e.InnerException.Message;
                if (mensagem.Contains("SHL"))
                    throw new Exception(mensagem, e);
                throw new Exception("Ocorreram erros no processo de faturamento dos pedidos. Rotina de faturamento abortada", e);
            }
        }

        private void FinalizaExpedicao(IList<Pedido> pedidos, Nota notaFinalizada, PessoaEndereco entrega)
        {
            decimal pesoBruto = 0;
            decimal pesoLiquido = 0;
            foreach (var item in pedidos)
            {
                IList<ExpedicaoRastreamento> listaExpedicao = ExpedicaoRastreamentoController.Instancia.GetExpedicaoRastreamentoPorPedido(item.ID);
                foreach (var expedicao in listaExpedicao)
                {
                    pesoBruto += expedicao.PesoBruto;
                    pesoLiquido += expedicao.PesoLiquido;
                    notaFinalizada.VolumeQuant += 1;
                }
            }

            notaFinalizada.VolumePesoBruto = pesoBruto;
            notaFinalizada.VolumePesoLiquido = pesoLiquido;
            notaFinalizada.EnderecoEntrega = entrega;
            if (entrega != null)
            {
                if (notaFinalizada.ObservacaoUsuario == "")
                    notaFinalizada.ObservacaoUsuario += "Endereço de Entrega: " + entrega.Endereco + " " + entrega.Numero + " " + entrega.Complemento + " " + entrega.CEP + " " + entrega.Cidade.Nome + " - " + entrega.Cidade.UF;
                else
                    notaFinalizada.ObservacaoUsuario += " " + "Endereço de Entrega: " + entrega.Endereco + " " + entrega.Numero + " " + entrega.Complemento + " " + entrega.CEP + " " + entrega.Cidade.Nome + " - " + entrega.Cidade.UF;
            }
        }

        public static void FinalizaNota(Nota notaFinalizada, TipoNota tipoNota, List<pxyFormaPagamentoBaixaDoc> formasDePagamento, IList<Pedido> pedidos, bool VemDoFrenteDeCaixa, bool VemDoDelivery, bool GerarFinanceiro, bool FaturarPedido)
        {
            #region RegraEstoqueAntiga


            #endregion

            //EstoqueController.Instancia.VerificarEstoqueNegativo(notaFinalizada);

            // foletto implementar
            if (VemDoDelivery)
            {
                if (tipoNota.ModeloDocto == TipoNota.ModeloDocumento.NFCe)
                {
                    notaFinalizada.BGeraFinanceiro = true;
                    GeraFinanceiro(notaFinalizada, formasDePagamento);
                    AtualizaStatusPedido(pedidos);
                }
                else
                {
                    if (GerarFinanceiro)
                    {
                        notaFinalizada.BGeraFinanceiro = true;
                        GeraFinanceiro(notaFinalizada, formasDePagamento);
                    }

                    if (FaturarPedido)
                        AtualizaStatusPedido(pedidos);
                }
            }
            else
            {

                GeraFinanceiro(notaFinalizada, formasDePagamento);

                if (tipoNota.BAtualizaPedido)
                    AtualizaStatusPedido(pedidos);
                else if (ConfiguracaoController.Instancia.GetConfiguracao().BExcluirOrcamentoFaturado || VemDoFrenteDeCaixa)
                    if (!VemDoDelivery)
                    {
                        try
                        {
                            if (VemDoFrenteDeCaixa)
                                notaFinalizada.CodigoPedido = 0;

                            NotaController.Instancia.CommitUnitOfWorkTransaction();
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Erro ao submeter transação dos dados gerados.", e);
                        }

                        ExcluiPedidos(pedidos);
                    }
            }

            MontaObservacaoSistema(notaFinalizada);
        }

        private static void AtualizaEstoque(Nota notaFinalizada)
        {
            try
            {
                var estoque = ControleEstoque.AtualizaEstoque.GetAtualizaEstoque(notaFinalizada);
                estoque.Atualizar(Acao.Incluir);
                EstoqueController.Instancia.VerificarEstoqueNegativo(notaFinalizada);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preparar a atualização dos estoques dos produtos faturados.", e);
            }
        }

        private static void MontaObservacaoSistema(Nota notaFinalizada)
        {
            string observacao;
            NotaController.Instancia.MontarObservacao(notaFinalizada, out observacao);
            notaFinalizada.ObservacaoSistema = observacao;
        }

        private void CommitarTransacao()
        {
            try
            {
                NotaController.Instancia.CommitUnitOfWorkTransaction();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao submeter transação dos dados gerados.", e);
            }
        }

        private static void ExcluiPedidos(IList<Pedido> pedidos)
        {
            try
            {
                foreach (Pedido pedido in pedidos)
                    if (pedido.Status != StatusOrcamento.FaturadoParcial)
                    {
                        IList<NotaItemPedidoItem> itens = NotaItemPedidoItemController.Instancia.GetAllNiPiPorIDPedido(pedido.ID);
                        NotaItemPedidoItemController.Instancia.RegisterDeleteIntoTransaction(itens);
                        PedidoController.Instancia.RegisterDeleteIntoTransaction(pedido);
                    }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preparar exclusão dos pedidos/orçamentos faturados.", e);
            }
        }

        private static void AtualizaStatusPedido(IList<Pedido> pedidos)
        {
            try
            {
                foreach (Pedido pedido in pedidos)
                    if (pedido.Ent_Sai == 2)
                    {
                        pedido.Status = StatusOrcamento.Faturado;

                        foreach (var item in pedido.Items)
                            item.BFaturado = true;

                        PedidoController.Instancia.RegisterNewIntoTransaction(pedido);
                    }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preparar atualização dos status dos pedidos/orçamentos faturados.", e);
            }
        }

        private static void GeraFinanceiro(Nota notaFinalizada, List<pxyFormaPagamentoBaixaDoc> formasDePagamento)
        {
            try
            {
                if (notaFinalizada.BGeraFinanceiro)
                {
                    NotaController.Instancia.GeraFinanceiro(notaFinalizada, formasDePagamento);
                    GerarDocumentoReceberFatParcial(notaFinalizada);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preparar financeiro da nota gerada.", e);
            }
        }

        private static void GerarDocumentoReceberFatParcial(Nota notaFinal)
        {
            var sePercFatProNota = PedidoController.Instancia.GetPercFatProdNota(notaFinal.CodigoPedido);
            if (sePercFatProNota)
            {
                Pedido objpedidolocal = PedidoController.Instancia.GetByCodigo(notaFinal.CodigoPedido);

                decimal novoValorParcela = objpedidolocal.TotalProduto - notaFinal.TotalProduto;
                TipoDocumento tipoDocumento = TipoDocumentoController.Instancia.LoadObjectById(1);
                Nota notaLocal = new Nota();
                notaLocal.TotalNota = novoValorParcela;

#pragma warning disable CS0219 // A variável "dataFaturamento" é atribuída, mas seu valor nunca é usado
                DateTime? dataFaturamento = null;
#pragma warning restore CS0219 // A variável "dataFaturamento" é atribuída, mas seu valor nunca é usado
                Documento documentoLocal = new Documento();
                documentoLocal.Codigo = DocumentoController.Instancia.GetNovoCodigoDocumento();
                documentoLocal.Banco = notaFinal.TipoNota.Banco;
                documentoLocal.Acrescimo = notaFinal.TipoNota.Acrescimo;
                documentoLocal.Condicao = notaFinal.Condicao;
                documentoLocal.Filial = FilialController.Instancia.LoadObjectById(2);
                documentoLocal.Historico = notaFinal.TipoNota.Historico;
                documentoLocal.Pessoa = notaFinal.Pessoa;
                documentoLocal.Portador = notaFinal.TipoNota.Portador;
                documentoLocal.NumDocumento = notaFinal.Numero.ToString();
                documentoLocal.NumRequisicao = "";
                documentoLocal.Tipo = TipoDocumentoType.Receber;
                documentoLocal.Situacao = "Norm";
                documentoLocal.QtParcela = 1;
                documentoLocal.NumeroPedido = objpedidolocal.Numero;
                documentoLocal.DtVencimento = notaFinal.Dt.AddDays(3);
                documentoLocal.DtPrevisao = notaFinal.Dt.AddDays(3);
                documentoLocal.Dt = DateTime.Now;
                documentoLocal.DtDigitacao = notaFinal.DtDigitacao.Value;
                documentoLocal.Valor = novoValorParcela;
                documentoLocal.ValorTotal = novoValorParcela;
                documentoLocal.Saldo = novoValorParcela;
                documentoLocal.TipoDocumento = tipoDocumento;
                documentoLocal.ComplementoHist = notaFinal.TipoNota.ComplementoHist;
                documentoLocal.Nota = null;
                documentoLocal.Contrato = null;

                Movimento objMovimento = new Movimento();
                objMovimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
                objMovimento.Tipo = TipoMovimentoType.Documento;
                objMovimento.Sequencia = 1;
                objMovimento.Documento = documentoLocal;
                objMovimento.Operacao = OperacaoMovimentoType.Lancamento;
                objMovimento.Historico = documentoLocal.Historico;
                objMovimento.ComplementoHist = documentoLocal.ComplementoHist;
                objMovimento.Dt = documentoLocal.Dt;
                objMovimento.PlanoConta = PlanoContaController.Instancia.LoadObjectById(notaFinal.TipoNota.PlanoConta.ID);
                objMovimento.Valor = documentoLocal.Valor;
                documentoLocal.Movimentos.Add(objMovimento);

                DocumentoController.Instancia.RegisterNewIntoTransaction(documentoLocal);
            }
        }

        private Nota ConstruirNota(TipoNota tipoNota, IList<Pedido> pedidos, DateTime faturamento, string observacao1, string observacao2, string observacao3, Condicao condicao, IList<FinanceiroParcela> parcelas, int? Serie, bool VemDoFrenteDeCaixa)
        {
            try
            {
                NotaBuilder builder = new NotaBuilder();
                var pedido = pedidos.First();
                builder.PreencherNotaComTipoNota(tipoNota);
                builder.EntSai = pedido.Ent_Sai == 1 ? InOutType.Entrada : InOutType.Saída;
                builder.PreencherDadosBasicosNota(pedido.Filial, faturamento, observacao1, observacao2, observacao3, pedido.PessoaTransportadora, pedido.TipoFrete);
                builder.PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(pedido.Pessoa, condicao, pedido.Vendedor, pedido.TabelaPreco, Serie);
                
                IList<IProdutoHolder> produtos = new List<IProdutoHolder>();

                foreach (Pedido p in pedidos)
                {
                    decimal qtdretirada = 0;
                    bool bPercFatProdNota = p.PercFatProdNota > 0;

                    IList<NotaItemPedidoItem> nipi = NotaItemPedidoItemController.Instancia.GetAllNiPiPorIDPedido(p.ID);
                    foreach (PedidoItem pedidoItem in p.Items)
                    {
                        qtdretirada = nipi.Where(w => w.PedidoItem.ID == pedidoItem.ID).Sum(n => n.QtdRetiradaPedido);
                        decimal qtdOriginal = pedidoItem.Quantidade;
                        if (pedidoItem.Quantidade > qtdretirada)
                        {
                            pedidoItem.Quantidade = pedidoItem.Quantidade - qtdretirada;
                            pedidoItem.Total = TruncarTotal(p, pedidoItem);
                            produtos.Add(new PedidoHolder(pedidoItem.Clone()));
                            pedidoItem.Quantidade = qtdOriginal;
                        }

                        if (bPercFatProdNota)
                            foreach (IProdutoHolder produtoHolder in produtos)
                            {
                                decimal percFatProdNota = p.PercFatProdNota / 100;
                                decimal novoValor = produtoHolder.Preco * percFatProdNota;
                                decimal novoTotal = produtoHolder.Total * percFatProdNota;

                                produtoHolder.PedidoItem.Valor = novoValor;
                                produtoHolder.PedidoItem.Total = novoTotal;
                            }
                    }
                }

                builder.IncluirProdutosNota(produtos);

                builder.Nota.ValorFrete = pedidos.Sum(i => i.ValorFrete);
                builder.Nota.ValorDesconto = pedidos.Sum(i => i.ValorDesconto);

                var valorDescontoItens = builder.Nota.NotaItems.Sum(x => x.ValorDesconto);
                if (builder.Nota.ValorDesconto == 0 && valorDescontoItens != 0)
                    builder.Nota.ValorDesconto = valorDescontoItens;

                builder.Nota.OutrasDespesas = pedidos.Sum(i => i.ValorAcrescimo);
                builder.Nota.CodigoPedido = pedido.Codigo;
                builder.Nota.DtEnvio = faturamento;

                bool dentroDoEstado = pedido.Filial.Cidade.UF.Sigla == pedido.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
                if (dentroDoEstado)
                    builder.Nota.idDest = 1;
                else
                    builder.Nota.idDest = pedido.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla == "EX" || pedido.Filial.Cidade.UF.Sigla == "EX" ? 3 : 2;

                listaDeImeis = builder.listaDeImeis;
                if (pedido.CodShl != 0 && pedido.StatusSHL == 20)
                    try
                    {
                        Configuracao conf = ConfiguracaoController.Instancia.GetConfiguracao();
                        SHLConfiguracao shlconf = SHLConfiguracaoController.Instancia.GetAll().FirstOrDefault();
                        if (conf.ServidorSHL != 1)
                            builder.Nota.VolumeQuant = UtilHomologacao.GetCodigosRastreamentoSHL(shlconf.ChaveSHLHomologacao, shlconf.InterfaceSHL, pedido.Codigo).Count();
                        else
                            builder.Nota.VolumeQuant = UtilProducao.GetCodigosRastreamentoSHL(shlconf.ChaveSHL, shlconf.InterfaceSHL, pedido.Codigo).Count();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Não foi possível requisitar os volumes da SHL. Verifique conexão com a Internet e os parâmetros de comunicação com o servidor da SHL!", e);
                    }
                else
                    builder.Nota.VolumeQuant = pedido.CorreiosPrecoPrazo?.Where(i => i.bSelecionado == true).Count() ?? 0;

                builder.SetTotaisNota();
                builder.SetParcelas(parcelas);

                //Rotina PAF-ECF | Caso exista número de cupom fiscal, irá setar no número da nota.
                if (pedidos.Count == 1)
                    if (pedidos.First().NumeroCupomFiscal > 0)
                        builder.Nota.Numero = pedidos.First().NumeroCupomFiscal;

                LogicaTelaNotaFiscal ltnf = new LogicaTelaNotaFiscal(builder.Nota);

                ltnf.AtualizaSomatoriosTotais();
                if (ConfiguracaoController.Instancia.GetConfiguracao().NaoSomarIpiTotalNota == 1)
                    foreach (var item in builder.Nota.NotaItems)
                    {
                        ltnf.nItem = item;
                        if (!VemDoFrenteDeCaixa)
                            ltnf.RefazerRateiosEAtualizarImpostos();
                    }
                else
                if (!VemDoFrenteDeCaixa)
                    ltnf.RefazerRateiosEAtualizarImpostos();

                ltnf.AtualizaTotalNotaFiscal();
                ltnf.AtualizarObservacaoSistema();

                return ltnf.GetNota;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preparar a nota.", e);
            }
        }

        private decimal TruncarTotal(Pedido p, PedidoItem pi)
        {
            decimal _total = 0;
            if (p.TipoPedido != null && p.TipoPedido.Truncar)
            {
                _total = pi.Valor * pi.Quantidade;
                _total *= 100;
                _total = Math.Truncate(_total);
                _total /= 100;
            }
            else if(pi.PercDesconto != 0) // Leandro
            { 
                _total = pi.Total;
            }
            else
            {
                _total = Math.Round(pi.Valor * pi.Quantidade, 2);
            }

            return _total;
        }

        public static void AtualizaStatusOrcamento(IList<Pedido> pedidos)
        {
            try
            {
                foreach (Pedido pedido in pedidos)
                {
                    IList<PedidoItem> itensPedido = pedido.Items;

                    decimal qtdRetirada = 0;
                    IList<NotaItemPedidoItem> nipi = NotaItemPedidoItemController.Instancia.GetAllNiPiPorIDPedido(pedido.ID);
                    qtdRetirada = nipi.Sum(n => n.QtdRetiradaPedido);

                    decimal qtdItensPedido = 0;
                    qtdItensPedido = pedido.Items.Sum(d => d.Quantidade);
                    if (qtdItensPedido > qtdRetirada && qtdRetirada > 0)
                        pedido.Status = StatusOrcamento.FaturadoParcial;
                    else if (qtdItensPedido <= qtdRetirada)
                        pedido.Status = StatusOrcamento.Faturado;
                    else if (qtdRetirada == 0) pedido.Status = StatusOrcamento.Aberto;

                    PedidoController.Instancia.Salvar(pedido, Acao.Alterar);
                    PedidoController.Instancia.CommitUnitOfWorkTransaction();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}