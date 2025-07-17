using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.ControleEstoque
{
    public class PedidoAtualizaEstoque : AtualizaEstoque
    {
        private Modelo.Pedido pedido;
        private IEnumerable<Modelo.PedidoItem> ItemsAtualizar
        {
            get
            {
                return pedido.Items.Where(i => i.bReservaEstoque
                                    && !pedido.Items.Contains(i.OrcamentoItem));
            }
        }

        public PedidoAtualizaEstoque(Modelo.Pedido objeto)
        {
            pedido = objeto;
            permiteEstoqueNegativo = pedido.TipoPedido.BPermiteEstoqueNegativo;
        }

        protected override IEnumerable<ParametroEstoque> GetInclusaoParametros()
        {
            foreach (var item in ItemsAtualizar.Where(i => i.ID == 0))
            {
                yield return GetParametroIncluirItem(item);
            }
        }

        protected override IEnumerable<ParametroEstoque> GetAlteracaoParametros()
        {
            var ret = new List<ParametroEstoque>();
            ret.AddRange(GetInclusaoParametros());
            ret.AddRange(GetAlteracoesExclusoesPedido());
            return ret;
        }

        protected override IEnumerable<ParametroEstoque> GetExclusaoParametros()
        {
            foreach (var item in ItemsAtualizar)
            {
                yield return GetParametroExcluirItem(item);
            }
        }

        private IEnumerable<ParametroEstoque> GetAlteracoesExclusoesPedido()
        {
            List<ParametroEstoque> ret = new List<ParametroEstoque>();

            var pedidoOld = PedidoController.Instancia.LoadObjectById(pedido.ID);
            foreach (var itemOld in pedidoOld.Items.Where(i => i.bReservaEstoque
                                                       && !pedidoOld.Items.Contains(i.OrcamentoItem)))
            {
                var xuo = ItemsAtualizar.Where(i => i.ID == itemOld.ID);
                if (xuo.Count() == 0)
                    ret.Add(GetParametroExcluirItem(itemOld));
                else
                {
                    var itemNew = xuo.First();
                    if (itemNew.Produto.ID != itemOld.Produto.ID || pedido.Filial.ID != pedidoOld.Filial.ID)
                    {
                        ret.AddRange(GetParametrosFilialOuProdutoDiferente(itemOld, itemNew));
                    }
                    else if (itemNew.Quantidade != itemOld.Quantidade)
                    {
                        var parm = ParametroEstoque.GetNovoParametro(pedido.TipoPedido.LocalEstoque, itemNew.Produto);
                        SetQuantidadeParametro(ref parm, (InOutType)pedido.Ent_Sai, itemNew.Quantidade - itemOld.Quantidade);
                        ret.Add(parm);
                    }
                }
            }

            return ret;
        }

        private ParametroEstoque GetParametroIncluirItem(PedidoItem item)
        {
            var ret = ParametroEstoque.GetNovoParametro(item.Pedido.TipoPedido.LocalEstoque, item.Produto);
            SetQuantidadeParametro(ref ret, (InOutType)item.Pedido.Ent_Sai, item.Quantidade);
            return ret;
        }

        private ParametroEstoque GetParametroExcluirItem(PedidoItem item)
        {
            var ret = ParametroEstoque.GetNovoParametro(item.Pedido.TipoPedido.LocalEstoque, item.Produto);
            SetQuantidadeParametro(ref ret, (InOutType)item.Pedido.Ent_Sai, -item.Quantidade);
            return ret;
        }

        private IEnumerable<ParametroEstoque> GetParametrosFilialOuProdutoDiferente(PedidoItem itemOld, PedidoItem itemNew)
        {
            var parmOld = ParametroEstoque.GetNovoParametro(itemOld.Pedido.TipoPedido.LocalEstoque, itemOld.Produto);
            SetQuantidadeParametro(ref parmOld, (InOutType)itemOld.Pedido.Ent_Sai, -itemOld.Quantidade);
            yield return (parmOld);

            var parmNew = ParametroEstoque.GetNovoParametro(itemNew.Pedido.TipoPedido.LocalEstoque, itemNew.Produto);
            SetQuantidadeParametro(ref parmNew, (InOutType)itemNew.Pedido.Ent_Sai, itemNew.Quantidade);
            yield return (parmNew);
        }

        private void SetQuantidadeParametro(ref ParametroEstoque parametro, InOutType ent_sai, decimal quantidade)
        {
            if (ent_sai == InOutType.Entrada)
            {
                parametro.qtdCompra_Pedido = quantidade;
            }
            else
            {
                parametro.qtdEstoque_Pedido = quantidade;
            }
        }
    }
}
