using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio.Faturamento.Holders;
using cwkGestao.Negocio.Tributacao;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Padroes;

namespace cwkGestao.Negocio.Faturamento
{
    public abstract class OrdemServico
    {
        public static OrdemServico Get(cwkGestao.Modelo.TipoNota.CategoriaMovimentacao categoria)
        {
            
            switch (categoria)
            {
                case cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.Produtos:
                    return new OrdemServico_Produto();
                case cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.Servicos:
                    return new OrdemServico_Servico();
                case cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.ProdutosEServicos:
                    return new OrdemServico_ProdutoServico();
            }
            return null;
        }

        public static OrdemServico Get(IList<pxyFaturamentoOS> listaPxy)
        {
            TipoNota.CategoriaMovimentacao categoria = SetCategoria(listaPxy);
            return Get(categoria);
        }

        #region Atributos

        public Modelo.TipoNota TipoMovimentacao1 { get; set; }
        public Modelo.TipoNota TipoMovimentacao2 { get; set; }
        public Modelo.Condicao Condicao { get; set; }
        public Modelo.Pessoa Cliente { get; set; }
        public Modelo.Filial Filial { get; set; }
        public Modelo.TipoNota.CategoriaMovimentacao Categoria { get; set; }
        public IList<Modelo.FinanceiroParcela> Parcelas { get; set; }
        public IList<Modelo.Proxy.pxyFaturamentoOS> ItemsSelecionados { get; set; }
        public DateTime DataFaturamento { get; set; }
        public string Observacao1 { get; set; }
        public string Observacao2 { get; set; }
        public string Observacao3 { get; set; }
        public Modelo.Pessoa Transp { get; set; }


        protected List<Modelo.Nota> notasGravar = new List<cwkGestao.Modelo.Nota>();
        protected List<Modelo.OSOrdemServico> ordensServicoFaturar = null;

        public List<Modelo.Nota> notasGeradas = new List<cwkGestao.Modelo.Nota>();

        #endregion

        public void Faturar()
        {
            SetOSFaturar();
            ValidarDados();
            GerarNotas();
            AtualizarEstoque();
            PersistirDados();
            GerarFinanceiro();
        }

        public void FaturarOSOrcamentoServico(OSOrdemServico os, IList<Pedido> pedidos)
        {
            ordensServicoFaturar = new List<OSOrdemServico>() { os };
            if (TipoMovimentacao1 != null)
                TipoMovimentacao1 = TipoNotaController.Instancia.LoadObjectById(TipoMovimentacao1.ID);
            if (TipoMovimentacao2 != null)
                TipoMovimentacao2 = TipoNotaController.Instancia.LoadObjectById(TipoMovimentacao2.ID);

            ValidarDados();
            GerarNotas();
            AtualizarEstoque();

            PersistirNotasSetandoPedidosServicos(pedidos);
            
            GerarFinanceiro();

        }

        protected abstract void GerarNotas();

        protected abstract void ValidarDados();

        private void SetOSFaturar()
        {
            ordensServicoFaturar = Negocio.OSOrdemServicoController.Instancia.Get(ItemsSelecionados.Select(i => i.ID).ToList<int>());
            if (TipoMovimentacao1 != null)
                TipoMovimentacao1 = TipoNotaController.Instancia.LoadObjectById(TipoMovimentacao1.ID);
            if (TipoMovimentacao2 != null)
                TipoMovimentacao2 = TipoNotaController.Instancia.LoadObjectById(TipoMovimentacao2.ID);
        }

        private void PersistirDados()
        {
            PersistirNotas();
            PersistirOS();
            NotaController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void PersistirNotasSetandoPedidosServicos(IList<Pedido> pedidos)
        {
            IList<NotaItemServicoPedidoItemServico> intermediarios = new List<NotaItemServicoPedidoItemServico>();
            foreach (Pedido p in pedidos)
            {
                foreach (PedidoItemServico svc in p.ItemsServicos)
                {
                    NotaItemServicoPedidoItemServico nispis = new NotaItemServicoPedidoItemServico();
                    nispis.PedidoItemServico = svc;
                    intermediarios.Add(nispis);
                }
            }
            
            PersistirNotas();
            NotaController.Instancia.CommitUnitOfWorkTransaction();

            for (int i = 0; i < intermediarios.Count; i++)
            {
                intermediarios[i].NotaItemServico = notasGeradas.FirstOrDefault().NotaItemsServicos.FirstOrDefault();
                NotaItemServicoPedidoItemServicoController.Instancia.Salvar(intermediarios[i], Acao.Incluir);
            }
        }
        
        private void PersistirNotas()
        {
            notasGravar.AddRange(notasGeradas);
            foreach (Modelo.Nota nota in notasGravar)
            {
                if (nota.ID == 0)
                    nota.Codigo = NotaController.Instancia.GetNovoCodigoNota();
                NotaController.Instancia.RegisterNewIntoTransaction(nota);
            }
        }

        private void PersistirOS()
        {
            foreach (Modelo.OSOrdemServico os in ordensServicoFaturar)
            {
                os.Status = StatusOSController.Instancia.GetStatusTipoFechamento();
                OSOrdemServicoController.Instancia.RegisterNewIntoTransaction(os);
            }
        }

        private void AtualizarEstoque()
        {
            foreach (var nota in notasGeradas)
            {
                var estoque = ControleEstoque.AtualizaEstoque.GetAtualizaEstoque(nota);
                estoque.Atualizar(Acao.Incluir);
            }
        }

        private void GerarFinanceiro()
        {
            foreach (Modelo.Nota nota in notasGeradas)
            {
                if (nota.BGeraFinanceiro)
                    NotaController.Instancia.GeraFinanceiroEGrava(nota);
            }
        }

        protected virtual void PreencherDadosBasicosNota(Modelo.Nota nota)
        {
            new NotaBuilder(nota).PreencherDadosBasicosNota(Filial, DataFaturamento, Observacao1, Observacao2, Observacao3, Transp, TipoFrete.SemFrete);
        }

        protected virtual void PreencherNotaComTipoNota(Modelo.Nota nota, Modelo.TipoNota tipoNota)
        {
            new NotaBuilder(nota).PreencherNotaComTipoNota(tipoNota);
        }

        protected virtual void PreencherNotaComOrdemServico(Modelo.Nota nota, Modelo.OSOrdemServico ordemServico)
        {
            new NotaBuilder(nota).PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(ordemServico.Cliente, ordemServico.Condicao, ordemServico.Vendedor, ordemServico.TabelaPreco, null);
        }

        protected virtual void IncluirProdutosNota(Modelo.Nota nota, Modelo.OSOrdemServico os)
        {
            List<int> idsOSProdutoItems = new List<int>();
            IList<IProdutoHolder> produtos = new List<IProdutoHolder>();

            foreach (OSProdutoItem osProdutoItem in os.OSProdutoItems.Distinct())
            {
                idsOSProdutoItems.Add(osProdutoItem.ID);
                produtos.Add(new OSProdutoHolder(osProdutoItem));
            }

            new NotaBuilder(nota).IncluirProdutosNota(produtos, idsOSProdutoItems);
        }

        protected virtual void IncluirServicosNota(Modelo.Nota nota, Modelo.OSOrdemServico os)
        {
            foreach (var pi in os.OSServicoItems.Distinct())
            {
                var notaServicoItem = new Modelo.NotaItemServico();

                notaServicoItem.Servico = pi.Servico;
                notaServicoItem.NomeServico = pi.Descricao;
                notaServicoItem.Quantidade = pi.Quantidade;
                notaServicoItem.Valor = pi.Preco;
                notaServicoItem.SubTotal = pi.Total;
                notaServicoItem.PercDesconto = 0;
                notaServicoItem.Nota = nota;
                notaServicoItem.IDOSServicoItem = pi.ID;
                nota.NotaItemsServicos.Add(notaServicoItem);
            }
            nota.ValorAcrescimo = os.DespesasAdicionais;
            nota.CodigoPedido = os.CodigoPedido;
        }

        protected virtual void CalcularTributacaoServico(cwkGestao.Modelo.Nota nota)
        {
            IList<Modelo.Nota> notasAtualizar = new List<Modelo.Nota>();
            Tributacao.TributacaoServicoOld.CalculaServicoImpostos(nota, out notasAtualizar);
            Tributacao.TributacaoServico trib = new TributacaoServico(nota);
            trib.AtualizarImpostosServico();
            notasGravar.AddRange(notasAtualizar);
        }

        protected virtual void CriarParcelasNotasGeradas()
        {
            var valorTotalParcelas = Parcelas.Sum(p => p.Valor);
            foreach (Modelo.Nota nota in notasGeradas)
            {
                foreach (Modelo.FinanceiroParcela parc in Parcelas)
                {
                    var percentualValor = Math.Round(parc.Valor / valorTotalParcelas, 4);

                    Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();
                    objNotaParcela.Portador = nota.TipoNota.Portador;
                    objNotaParcela.Banco = nota.TipoNota.Banco;
                    objNotaParcela.Valor = Math.Round(nota.TotalNota * percentualValor, 2);

                    objNotaParcela.Parcela = (int)parc.Parcela;
                    objNotaParcela.BEntrada = parc.BEntrada;
                    objNotaParcela.TipoDocumento = parc.TipoDocumento;
                    objNotaParcela.Vencimento = parc.Vencimento;
                    objNotaParcela.BAlterado = parc.BAlterado;
                    objNotaParcela.NumBanco = parc.NumBanco;
                    objNotaParcela.NumAgencia = parc.NumAgencia;
                    objNotaParcela.NumContaCorrente = parc.NumContaCorrente;
                    objNotaParcela.NumCheque = parc.NumCheque;
                    objNotaParcela.Emitente = parc.Emitente;
                    objNotaParcela.CpfCnpj = parc.CpfCnpj;
                    objNotaParcela.NomeBanco = parc.NomeBanco;
                    objNotaParcela.Nota = nota;

                    objNotaParcela.FormaPagamento = (ParcelaBase.EnumFormaPagamento)Convert.ToInt32(parc.TipoDocumento.FormaPagamento.CodigoSefaz);

                    nota.NotaParcelas.Add(objNotaParcela);
                }
            }
        }

        protected virtual void SetTotaisNota(cwkGestao.Modelo.Nota nota)
        {
            CalcularTributacaoServico(nota);
            new NotaBuilder(nota).SetTotaisNota();
        }

        private static TipoNota.CategoriaMovimentacao SetCategoria(IList<pxyFaturamentoOS> orcamentosServicoFaturar)
        {
            TipoNota.CategoriaMovimentacao categoria = TipoNota.CategoriaMovimentacao.ProdutosEServicos;
            var qtdProdutos = orcamentosServicoFaturar.Sum(o => o.QtdTotProdutos);
            var qtdServicos = orcamentosServicoFaturar.Sum(o => o.QtdTotServicos);
            if (qtdProdutos > 0 && qtdServicos > 0)
            {
                categoria = cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.ProdutosEServicos;
            }
            else if (qtdProdutos > 0)
            {
                categoria = cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.Produtos;
            }
            else if (qtdServicos > 0)
            {
                categoria = cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.Servicos;
            }
            return categoria;            
        }
    }
}
