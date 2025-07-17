using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioEstoqueIdentificadorExpedicao : IRepositorio<EstoqueIdentificadorExpedicao>
    {
        EstoqueIdentificadorExpedicao GetPorIdIdentificador(int IdIdentificador);
    }
}
