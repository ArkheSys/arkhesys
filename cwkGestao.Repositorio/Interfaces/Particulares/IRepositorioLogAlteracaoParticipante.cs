using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioLogAlteracaoParticipante : IRepositorio<LogAlteracaoParticipante>
    {
        IList<LogAlteracaoParticipante> LoadObjectByData(DateTime start, DateTime end);
    }
}

