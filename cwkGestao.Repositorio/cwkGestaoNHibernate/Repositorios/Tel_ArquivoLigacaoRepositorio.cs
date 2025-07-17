using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate.Transform;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_ArquivoLigacaoRepositorio : RepositorioAbstrato<Tel_ArquivoLigacao>, IRepositorioTel_ArquivoLigacao
    {
        protected override string HqlLoadById()
        {
            return "from Tel_ArquivoLigacao l where l.ID = :id"; //left join fetch l.Ligacoes li
        }

        protected override string hqlGetAll()
        {
            return "from Tel_ArquivoLigacao l";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

    }
}
