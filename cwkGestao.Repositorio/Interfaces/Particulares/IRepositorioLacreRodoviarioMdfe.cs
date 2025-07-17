using cwkGestao.Modelo;

using System.Collections.Generic;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioLacreRodoviarioMdfe : IRepositorio<LacreRodoviarioMdfe>
    {
        IList<LacreRodoviarioMdfe> GetLacresPorMDFe(int IDMDFe);
    }
}

