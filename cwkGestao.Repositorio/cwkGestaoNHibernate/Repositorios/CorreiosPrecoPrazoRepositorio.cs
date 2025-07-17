using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CorreiosPrecoPrazoRepositorio : RepositorioAbstrato<CorreiosPrecoPrazo>, IRepositorioCorreiosPrecoPrazo
    {
        protected override string HqlLoadById()
        {
            return "from CorreiosPrecoPrazo n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CorreiosPrecoPrazo n";
        }

        public IList<CorreiosPrecoPrazo> GetPorIDPedido(int idPedido)
        {
            using (var session = GetSession())
            {
                string hql = "from CorreiosPrecoPrazo n where n.IDPedido = :idPedido";
                var resultado = session.CreateQuery(hql).SetInt32("idPedido", idPedido).List<CorreiosPrecoPrazo>();
               
                return resultado;
            }
        }

        public IList<CorreiosPrecoPrazo> GetPorIDRastreamento(int idRastreamento)
        {
            using (var session = GetSession())
            {
                string hql = "from CorreiosPrecoPrazo n where n.IDRastreamento = :idRastreamento";
                var resultado = session.CreateQuery(hql).SetInt32("idRastreamento", idRastreamento).List<CorreiosPrecoPrazo>();
                
                return resultado;
            }
        }

        public IList<CorreiosPrecoPrazo> GetSelecionadosPorIDPedido(int IdPedido)
        {
            try
            {
                using (var session = GetSession())
                {
                    string hql = "select cpp from CorreiosPrecoPrazo cpp left join cpp.TipoServicoTransporte tst where tst.IDServicoEnvioCorreio is not null and cpp.IDPedido = :IdPedido and cpp.bSelecionado = true";
                    IList<CorreiosPrecoPrazo> ret = session.CreateQuery(hql).SetInt32("IdPedido", IdPedido).List<CorreiosPrecoPrazo>();
                    return ret;
                }
            }
            catch (Exception z)
            {
                
                throw z;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
