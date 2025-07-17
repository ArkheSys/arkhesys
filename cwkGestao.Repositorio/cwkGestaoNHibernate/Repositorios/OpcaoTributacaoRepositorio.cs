using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OpcaoTributacaoRepositorio : RepositorioAbstrato<OpcaoTributacao>, IRepositorioOpcaoTributacao
    {
        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<OpcaoTributacao>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public Hashtable GetHashNomeCidade()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            return "from OpcaoTributacao";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from OpcaoTributacao c where c.ID = :id";
        }
    }
}
