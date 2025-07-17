using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaTipoServicoTransporteRepositorio : RepositorioAbstrato<PessoaTipoServicoTransporte>, IRepositorioPessoaTipoServicoTransporte
    {
        protected override string HqlLoadById()
        {
            return "from PessoaTipoServicoTransporte n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PessoaTipoServicoTransporte n";
        }

        #region IRepositorioPessoaTipoServicoTransporte Members

        public IList<PessoaTipoServicoTransporte> GetAllServicosPorPessoa(int IDPessoa)
        {
            IList<PessoaTipoServicoTransporte> result = new List<PessoaTipoServicoTransporte>();
            using (var session = GetSession())
            {
                try
                {
                    string hql = "from PessoaTipoServicoTransporte n where n.Pessoa.ID = :id";
                    result = session.CreateQuery(hql).SetInt32("id", IDPessoa).List<PessoaTipoServicoTransporte>();
                }
                catch (Exception z)
                {
                    throw z;
                }
            }
            return result;
        }

        #endregion

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
