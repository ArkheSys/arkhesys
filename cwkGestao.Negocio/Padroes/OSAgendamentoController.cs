using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class OSAgendamentoController : BaseController<OSAgendamento>
    {
        #region Singleton
        private static OSAgendamentoController instance;
        private static IRepositorioOSAgendamento repositorioOSAgendamento;

        private OSAgendamentoController()
        { }

        static OSAgendamentoController()
        {
            instance = new OSAgendamentoController();
            repositorioT = RepositorioFactory<OSAgendamento>.GetRepositorio();
            repositorioOSAgendamento = (IRepositorioOSAgendamento)repositorioT;
        }

        public static OSAgendamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(OSAgendamento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Modelo.OSAgendamento> GetAgendamentosDia(DateTime dia)
        {
            return repositorioOSAgendamento.GetAgendamentosDia(dia);
        }

        public IList<OSAgendamento> GetAgendamentosDia(DateTime dateTime, TipoListagem tipoListagem)
        {
            DateTime referencia = dateTime.Date;
            DateTime periodoInicial, periodoFinal;

            SetPeriodos(tipoListagem, referencia, out periodoInicial, out periodoFinal);
            return repositorioOSAgendamento.GetAgendamentosPeriodo(periodoInicial, periodoFinal);

        }

        public void SetPeriodos(TipoListagem tipoListagem, DateTime referencia, out DateTime periodoInicial, out DateTime periodoFinal)
        {
            switch (tipoListagem)
            {
                case TipoListagem.UmDia:
                    periodoInicial = periodoFinal = referencia;
                    break;
                case TipoListagem.TresDias:
                    PeriodoTresDias(referencia, out periodoInicial, out periodoFinal);
                    break;
                case TipoListagem.Semanal:
                    PeriodoSemanal(referencia, out periodoInicial, out periodoFinal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("tipoListagem");
            }
        }

        private void PeriodoSemanal(DateTime referencia, out DateTime periodoInicial, out DateTime periodoFinal)
        {
            int offsetDomingo = 0, offsetSabado = 0;
            switch (referencia.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    offsetDomingo = 0;
                    offsetSabado = 6;
                    break;
                case DayOfWeek.Monday:
                    offsetDomingo = -1;
                    offsetSabado = 5;
                    break;
                case DayOfWeek.Tuesday:
                    offsetDomingo = -2;
                    offsetSabado = 4;
                    break;
                case DayOfWeek.Wednesday:
                    offsetDomingo = -3;
                    offsetSabado = 3;
                    break;
                case DayOfWeek.Thursday:
                    offsetDomingo = -4;
                    offsetSabado = 2;
                    break;
                case DayOfWeek.Friday:
                    offsetDomingo = -5;
                    offsetSabado = 1;
                    break;
                case DayOfWeek.Saturday:
                    offsetDomingo = -6;
                    offsetSabado = 0;
                    break;
            }
            periodoInicial = referencia + new TimeSpan(offsetDomingo, 0, 0, 0);
            periodoFinal = referencia + new TimeSpan(offsetSabado, 0, 0, 0);
        }

        private void PeriodoTresDias(DateTime dateTime, out DateTime periodoInicial, out DateTime periodoFinal)
        {
            periodoInicial = dateTime;
            periodoFinal = dateTime + new TimeSpan(3, 0, 0, 0);
        }

        public enum TipoListagem
        {
            UmDia = 0,
            TresDias = 1,
            Semanal = 2
        }
    }
}
