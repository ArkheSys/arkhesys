using cwkGestao.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioStatusOS : IRepositorio<StatusOS>
    {
        IList<string> GetAllDescricao();

        StatusOS GetStatusTipoFechamento();

        StatusOS GetStatusTipoAgendamento();

        StatusOS GetStatusTipoFaturamento();
    }
}
