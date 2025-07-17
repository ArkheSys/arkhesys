using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class AcrescimoRepositorio : RepositorioAbstrato<Acrescimo>, IRepositorioAcrescimo
    {
        protected override string HqlLoadById()
        {
            return "from Acrescimo ac where ac.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Acrescimo ";
        }

        public decimal CalculaJuros(cwkGestao.Modelo.Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime dataVencimento, decimal pctMora)
        {
            if (dataBase.Subtract(dataVencimento).Days <= acrescimo.DiasJuro) return 0;
            using (var session = GetSession())
            {
                string sql = @" declare @juro as numeric(10,2)
                                exec @juro = f_VlrJuro :idAcrescimo,:valor,:dataBase,:dataVencimento, :pctMora
                                select @juro";

                IQuery query = session.CreateSQLQuery(sql);
                query = query.SetDecimal("idAcrescimo", acrescimo.ID);
                query = query.SetDecimal("valor", valor);
                query = query.SetDateTime("dataBase", dataBase.Date);
                query = query.SetDateTime("dataVencimento", dataVencimento);
                query = query.SetDecimal("pctMora", pctMora);

                var valorJuros = query.List<object>()[0] ?? 0;
                return Convert.ToDecimal(valorJuros);
            }
        }

        public decimal CalculaMulta(cwkGestao.Modelo.Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime dataVencimento, decimal valorjuro, decimal rpporcmulta, int rpdiasmulta)
        {
            if (dataBase.Subtract(dataVencimento).Days <= acrescimo.DiasMulta) return 0;
            using (var session = GetSession())
            {
                string sql = @" declare @multa as numeric(10,2)
                                exec @multa = f_vlrmulta :idAcrescimo,:valor,:dataBase,:dataVencimento, :valorJuro, :rPPorcMulta, :rPDiasMulta
                                select @multa";

                IQuery query = session.CreateSQLQuery(sql);
                query = query.SetDecimal("idAcrescimo", acrescimo.ID);
                query = query.SetDecimal("valor", valor);
                query = query.SetDateTime("dataBase", dataBase);
                query = query.SetDateTime("dataVencimento", dataVencimento);
                query = query.SetDecimal("valorJuro", valorjuro);
                query = query.SetDecimal("rPPorcMulta", rpporcmulta);
                query = query.SetInt32("rPDiasMulta", rpdiasmulta);

                var valorJuros = query.List<object>()[0] ?? 0;
                return Convert.ToDecimal(valorJuros);
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
