using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.ControleEstoque
{
    public class NotaAtualizaEstoque : AtualizaEstoque
    {
        private Modelo.Nota nota;
        private IEnumerable<Modelo.NotaItem> ItemsAtualizar
        {
            get
            {
                return nota.NotaItems.Where(i => i.BAtualizaEstoque);
            }
        }

        public NotaAtualizaEstoque(Modelo.Nota objeto)
        {
            nota = objeto;
            permiteEstoqueNegativo = nota.TipoNota.BPermiteEstqueNegativo;
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
            ret.AddRange(GetAlteracoesExclusoesNota());
            return ret;
        }

        protected override IEnumerable<ParametroEstoque> GetExclusaoParametros()
        {
            foreach (var item in ItemsAtualizar)
            {
                yield return GetParametroExcluirItem(item);
            }
        }

        private IEnumerable<ParametroEstoque> GetAlteracoesExclusoesNota()
        {
            List<ParametroEstoque> ret = new List<ParametroEstoque>();

            var notaOld = NotaController.Instancia.LoadObjectById(nota.ID);
            foreach (var itemOld in notaOld.NotaItems.Where(i => i.BAtualizaEstoque))
            {
                var xuo = ItemsAtualizar.Where(i => i.ID == itemOld.ID);
                if (xuo.Count() == 0)
                    ret.Add(GetParametroExcluirItem(itemOld));
                else
                {
                    var itemNew = xuo.First();
                    if (itemNew.Produto.ID != itemOld.Produto.ID || nota.Filial.ID != notaOld.Filial.ID)
                    {
                        ret.AddRange(GetParametrosFilialOuProdutoDiferente(itemOld, itemNew));
                    }
                    else //if (itemNew.Quantidade != itemOld.Quantidade)
                    {
                        var parm = ParametroEstoque.GetNovoParametro(nota.TipoNota.LocalEstoqueTipoNota, itemNew.Produto);
                        SetQuantidadeECustoUnitarioParametro(ref parm, (InOutType)nota.Ent_Sai, itemNew.Quantidade - itemOld.Quantidade, itemNew.CustoUnit);
                        ret.Add(parm);
                    }
                }
            }

            return ret;
        }

        private ParametroEstoque GetParametroIncluirItem(NotaItem item)
        {
            var ret = ParametroEstoque.GetNovoParametro(item.Nota.TipoNota.LocalEstoqueTipoNota, item.Produto);
            SetQuantidadeECustoUnitarioParametro(ref ret, (InOutType)item.Nota.Ent_Sai, item.Quantidade, item.CustoUnit);
            return ret;
        }

        private ParametroEstoque GetParametroExcluirItem(NotaItem item)
        {
            var ret = ParametroEstoque.GetNovoParametro(item.Nota.TipoNota.LocalEstoqueTipoNota, item.Produto);
            SetQuantidadeECustoUnitarioParametro(ref ret, (InOutType)item.Nota.Ent_Sai, -item.Quantidade, item.CustoUnit);
            return ret;
        }

        private IEnumerable<ParametroEstoque> GetParametrosFilialOuProdutoDiferente(NotaItem itemOld, NotaItem itemNew)
        {
            var parmOld = ParametroEstoque.GetNovoParametro(itemOld.Nota.TipoNota.LocalEstoqueTipoNota, itemOld.Produto);
            SetQuantidadeECustoUnitarioParametro(ref parmOld, (InOutType)itemOld.Nota.Ent_Sai, -itemOld.Quantidade, itemOld.CustoUnit);
            yield return (parmOld);

            var parmNew = ParametroEstoque.GetNovoParametro(itemNew.Nota.TipoNota.LocalEstoqueTipoNota, itemNew.Produto);
            SetQuantidadeECustoUnitarioParametro(ref parmNew, (InOutType)itemNew.Nota.Ent_Sai, itemNew.Quantidade, itemNew.CustoUnit);
            yield return (parmNew);
        }

        private void SetQuantidadeECustoUnitarioParametro(ref ParametroEstoque parametro, InOutType ent_sai, decimal quantidade, decimal custoUnitario)
        {
            if (ent_sai == InOutType.Entrada)
            {
                parametro.qtdAcrescentar_Fisico = quantidade;
                parametro.custoUnitarioNotaItem = custoUnitario;
            }
            else
            {
                parametro.qtdDescontar_Fisico = quantidade;
            }
        }
    }
}
