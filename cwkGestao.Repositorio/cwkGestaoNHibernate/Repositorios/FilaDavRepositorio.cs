using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FilaDavRepositorio : RepositorioAbstrato<FilaDav>, IRepositorioFilaDav
    {
        protected override string HqlLoadById()
        {
            return "from FilaDav n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FilaDav n";
        }

        public IList<cwkGestao.Modelo.FilaDav> GetPorIDPedido(int IDPedido)
        {
            using (var session = GetSession())
            {
                string hql = "from FilaDav f where f.Pedido.ID = :IDPedido order by f.ID";
                var resultado = session.CreateQuery(hql).SetInt32("IDPedido", IDPedido).List<cwkGestao.Modelo.FilaDav>();
                if (resultado.Count > 0)
                    return resultado;
                return null;
            }
        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
