using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class AcrescimoController : BaseController<Acrescimo>
    {
        #region Singleton
        private static AcrescimoController instance;
        private static IRepositorioAcrescimo repositorioAcrescimo;

        private AcrescimoController()
        { }

        static AcrescimoController()
        {
            instance = new AcrescimoController();
            repositorioT = RepositorioFactory<Acrescimo>.GetRepositorio();
            repositorioAcrescimo = (IRepositorioAcrescimo)repositorioT;
        }

        public static AcrescimoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Acrescimo objeto)
        {
            return new Dictionary<string, string>();
        }

        [Obsolete]
        public decimal CalculaJuros(cwkGestao.Modelo.Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime dataVencimento, decimal pctMora)
        {
            return repositorioAcrescimo.CalculaJuros(acrescimo, valor, (DateTime)dataBase, dataVencimento, pctMora);
        }
        [Obsolete]
        public decimal CalculaMulta(cwkGestao.Modelo.Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime dataVencimento, decimal valorjuro, decimal rpporcmulta, int rpdiasmulta)
        {
            return repositorioAcrescimo.CalculaMulta(acrescimo, valor, dataBase, dataVencimento, valorjuro, rpporcmulta, rpdiasmulta);
        }

        public decimal CalculaJuros(Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime vencimento)
        {
            return CalculaJuros(acrescimo.PercJuro, acrescimo.DiasJuro, valor, dataBase, vencimento, (TipoJuroType)acrescimo.TipoJuro, acrescimo.VlrJuro);
        }

        public decimal CalculaJuros(decimal percJuros, int diasTolerancia, decimal valor, DateTime dataBase, DateTime vencimento, TipoJuroType tipojuro, decimal vlrJurosDiario)
        {
            if (!EhPraCalcular(dataBase, vencimento, diasTolerancia))
            {
                return 0;
            }
            else
            {
                int diasEmUmMes = 30;
                int qtdDiasVencido = (dataBase - vencimento).Days;

                if (tipojuro == TipoJuroType.Percentual)
                    return Math.Round((percJuros * valor) / (100 * diasEmUmMes) * qtdDiasVencido, 2);
                else
                    return Math.Round((qtdDiasVencido * vlrJurosDiario), 2);
            }
           
        }

        private bool EhPraCalcular(DateTime dataBase, DateTime vencimento, int diasTolerancia)
        {
            DateTime vencimentoReal = vencimento.Date.AddDays(diasTolerancia);

            return dataBase.Date > vencimentoReal;
        }

        public decimal CalculaMulta(Acrescimo acrescimo, decimal valor, DateTime dataBase, DateTime vencimento)
        {
            return CalculaMulta(acrescimo.PercMulta, acrescimo.DiasMulta, valor, (!acrescimo.JurosMulta ? 0 : CalculaJuros(acrescimo, valor, dataBase, vencimento)), dataBase, vencimento);
        }

        public decimal CalculaMulta(decimal percMulta, int diasTolerancia, decimal valor, decimal valorJuros, DateTime dataBase, DateTime vencimento)
        {
            if (!EhPraCalcular(dataBase, vencimento, diasTolerancia))
                return 0;
            else
            {
                decimal valorBase = valor + valorJuros;

                return Math.Round(valorBase * percMulta / 100, 2);
            }
        }
    }
}
