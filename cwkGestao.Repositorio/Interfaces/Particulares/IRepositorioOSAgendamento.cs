using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOSAgendamento : IRepositorio<OSAgendamento>
    {
        IList<OSAgendamento> GetAgendamentosDia(DateTime dia);
        IList<OSAgendamento> GetAgendamentosPeriodo(DateTime periodioInicial, DateTime periodioFinal);
    }
}
