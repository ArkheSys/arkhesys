using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Faturamento.Holders;

namespace cwkGestao.Negocio.Faturamento
{
    public class Requisicao
    {
        private NotaController notaController = NotaController.Instancia;
        private PedidoController requisicaoController = PedidoController.Instancia;
        private ConfiguracaoController configuracaoController = ConfiguracaoController.Instancia;

        private IList<pxyBaixaRequisicao> itensBaixa;
        public Nota nota { get; private set; }
        public bool RequisicaoFoiExcluida { get; private set; }

        public TipoNota TipoNota { get; set; }
        public DateTime DataBaixa { get; set; }
        public string Observacao1 { get; set; }
        public string Observacao2 { get; set; }
        public string Observacao3 { get; set; }


        public Requisicao(IList<pxyBaixaRequisicao> itensBaixa)
        {
            if (itensBaixa == null || itensBaixa.Count == 0)
                throw new ArgumentException("Para efetuar a baixa deve ser selecionado no mínimo um item.");

            this.itensBaixa = itensBaixa;

            var requisicao = itensBaixa.First().PedidoItem.Pedido;
            TipoNota = requisicao.TipoPedido.TipoNota;
            Observacao1 = requisicao.Observacao1;
            Observacao2 = requisicao.Observacao2;
            Observacao3 = requisicao.Observacao3;
            DataBaixa = DateTime.Now;
        }

        public Requisicao(IList<Pedido> requisicoesBaixa)
        {
            if (requisicoesBaixa == null || requisicoesBaixa.Count == 0)
                throw new ArgumentException("Para efetuar a baixa deve ser selecionada no mínimo uma requisição.");

            var itens = new List<pxyBaixaRequisicao>();
            foreach (var req in requisicoesBaixa)
            {
                itens.AddRange(from i in req.Items
                               select new pxyBaixaRequisicao
                               {
                                   PedidoItem = i,
                                   Selecionado = true
                               });
            }
            itensBaixa = itens;
        }

        public void Baixar()
        {
            ConstruirNota();
            AtualizarRequisicao();
        }

        public void PersistirDados()
        {
            notaController.CommitUnitOfWorkTransaction();
        }

        private void ConstruirNota()
        {
            var requisicao = itensBaixa.First().PedidoItem.Pedido;

            NotaBuilder builder = new NotaBuilder();
            builder.CalcularTributacao = false;
            builder.EntSai = (InOutType)requisicao.Ent_Sai;
            builder.PreencherDadosBasicosNota(requisicao.Filial, DateTime.Now, Observacao1, Observacao2, Observacao3, null, TipoFrete.SemFrete);
            builder.PreencherNotaComTipoNota(TipoNota);
            builder.PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(requisicao.Pessoa, requisicao.Condicao, null, null, null);

            IList<IProdutoHolder> produtos = GetItensHolders();
            builder.IncluirProdutosNota(produtos);

            nota = builder.Nota;
            notaController.RegisterNewIntoTransaction(nota);

            AtualizarEstoqueNota();
        }

        private void AtualizarEstoqueNota()
        {
            var atualizaEstoque = new ControleEstoque.NotaAtualizaEstoque(nota);
            atualizaEstoque.Atualizar(Acao.Incluir);
        }

        private void AtualizarRequisicao()
        {
            if (nota.TipoNota.BAtualizaPedido)
            {
                foreach (var item in itensBaixa)
                {
                    item.PedidoItem.QtdRetirada += item.Quantidade;
                }

                foreach (var req in itensBaixa.Select(i => i.PedidoItem.Pedido).Distinct())
                {
                    if (EhBaixaParcial(req))
                        req.Status = StatusOrcamento.FaturadoParcial;
                    else
                        req.Status = StatusOrcamento.Faturado;

                    requisicaoController.RegisterNewIntoTransaction(req);
                }
            }
            else
            {
                var requisicoes = itensBaixa.Select(i => i.PedidoItem.Pedido).Distinct().ToList();
                //TODO validar essa regra com  o PO
                foreach (var req in requisicoes)
                {
                    var atualizaEstoque = new ControleEstoque.PedidoAtualizaEstoque(req);
                    atualizaEstoque.Atualizar(Acao.Excluir);
                }

                Configuracao config = configuracaoController.GetConfiguracao();
                if (config.BExcluirOrcamentoFaturado)
                {
                    requisicaoController.RegisterDeleteIntoTransaction(requisicoes);
                    RequisicaoFoiExcluida = true;
                }
            }
        }

        private bool EhBaixaParcial(Pedido req)
        {
            if (req.Items.Where(i => i.QtdRetirada != i.Quantidade).Count() > 0)
                return true;
            return false;
        }

        private IList<IProdutoHolder> GetItensHolders()
        {
            IList<IProdutoHolder> produtos = new List<IProdutoHolder>();
            foreach (var item in itensBaixa)
            {
                produtos.Add(new RequisicaoHolder(item));
            }
            return produtos;
        }
    }
}
