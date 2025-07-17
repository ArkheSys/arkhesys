using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_TipoLigacao : IRepositorio<Tel_TipoLigacao>
    {
        HashSet<int> GetHashCodigos();

        IList<Tel_TipoLigacao> GetIncompletos();
    }
}
