using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioContratoProduto : IRepositorio<ContratoProduto>
    {
        IList<ContratoProduto> GetItensContratoParaFaturar(DateTime referencia, Int32 diavcto);
    }
}

