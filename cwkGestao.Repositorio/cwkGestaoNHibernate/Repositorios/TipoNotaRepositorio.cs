using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class TipoNotaRepositorio : RepositorioAbstrato<TipoNota>, IRepositorioTipoNota
    {
        protected override string HqlLoadById()
        {
            return "select t from TipoNota t"
                   + " left join fetch t.Banco b "
                   + " left join fetch t.Portador p "
                   + " left join fetch t.Acrescimo a "
                   + " left join fetch t.Historico h "
                   + " left join fetch t.PlanoConta pc "
                   + " left join fetch t.Operacao o "
                   + " left join fetch t.Impressora i "
                   + " left join fetch t.PlanoContaFrete pcf "
                   + " left join fetch t.PlanoContaIPI pci "
                   + " left join fetch t.PlanoContaServico pcs "
                   + " left join fetch t.LocalEstoqueTipoNota l "
                   +" where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from TipoNota t";
        }

        public IList<TipoNota> GetAllPorCategoria(Enum categoria)
        {
#pragma warning disable CS0219 // A variável "a" é atribuída, mas seu valor nunca é usado
            var a = TipoNota.CategoriaMovimentacao.Servicos;
#pragma warning restore CS0219 // A variável "a" é atribuída, mas seu valor nunca é usado
            using (var session = GetSession())
            {
                string hql = "from TipoNota tn where tn.Categoria = :categoria";
                IQuery query = session.CreateQuery(hql).SetEnum("categoria", categoria); ;
                return query.List<TipoNota>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
