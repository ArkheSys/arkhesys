using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioVinculo : IRepositorio<Vinculo>
    {
        Vinculo GetProdVinculo(string CodProd, string CFOP, int idFornecedor, ref bool temCFOP);
    }
}

