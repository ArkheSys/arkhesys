using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOfd_PropostaMedicaoOcorrencias : IRepositorio<Ofd_PropostaMedicaoOcorrencias>
    {
        IList<Ofd_PropostaMedicaoOcorrencias> GetByPropostaMedicao(int idPropostaMedicao);
    }
}

