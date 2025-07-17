using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFichaTecnicaPCPItem : IRepositorio<FichaTecnicaPCPItem>
    {
        IList<FichaTecnicaPCP> GetFichaTecnicaByIdProduto(int IdProduto);

        IList<FichaTecnicaPCPItem> GetFichaTecnicaPCPItensVinculadosComFichaTecnica(int IDFichaTecnicaPCP);
    }
}

