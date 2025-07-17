using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FormaPagamentoRepositorio : RepositorioAbstrato<FormaPagamento>, IRepositorioFormaPagamento
    {
        protected override string HqlLoadById()
        {
            return "from FormaPagamento f where f.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return " from FormaPagamento f ";
        }

        public FormaPagamento GetCheque()
        {
            using (var session = GetSession())
            {
                string sql = "from FormaPagamento f where f.Tipo = " + (int)FormaPagamentoTipo.Cheque;
                return session.CreateQuery(sql).List<FormaPagamento>().FirstOrDefault();
            }
        }

        public FormaPagamento FindByCodigoSIS(string cod)
        {
            using (var session = GetSession())
            {
                string hql = "from FormaPagamento f where f.CodigoSisECommerce = :cod";
                return session.CreateQuery(hql).SetString("cod", cod).UniqueResult<FormaPagamento>();
            }
        }

        public FormaPagamento FindByCodigo(int cod)
        {
            using (var session = GetSession())
            {
                string hql = "from FormaPagamento f where f.Codigo = :cod";
                return session.CreateQuery(hql).SetInt32("cod", cod).List<FormaPagamento>().FirstOrDefault();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
