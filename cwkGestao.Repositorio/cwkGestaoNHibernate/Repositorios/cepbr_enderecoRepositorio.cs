using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class cepbr_enderecoRepositorio : RepositorioAbstrato<cepbr_endereco>, IRepositoriocepbr_endereco
    {
        protected override string HqlLoadById()
        {
            return "from cepbr_endereco n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from cepbr_endereco n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public cepbr_endereco BuscaInfEnderecos(string cep)
        {
            using (var session = GetSession())
            {
                string hql = @" from cepbr_endereco ende 
                                where ende.cep = :cep";
                cepbr_endereco e = session.CreateQuery(hql).SetString("cep", cep).UniqueResult<cepbr_endereco>();
   
                return e;
            }
        }
    }
}
