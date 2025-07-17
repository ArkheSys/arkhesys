using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FormaPgtoMovimentoRepositorio : RepositorioAbstrato<FormaPgtoMovimento>, IRepositorioFormaPgtoMovimento
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }
        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<FormaPgtoMovimento>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
