using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFichaTecnicaPCP : IRepositorio<FichaTecnicaPCP>
    {
        IList<FichaTecnicaPCP> GetImpressao();

        IList<FichaTecnicaPCP> GetAllPorIDProduto(int id);
    }
}

