using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioVersao : IRepositorio<Versao>
    {
        IList<cwkGestao.Modelo.Versao> GetAllBySistema(int pIdSistema);
        bool IsReferenced(Versao versao);
    }
}
