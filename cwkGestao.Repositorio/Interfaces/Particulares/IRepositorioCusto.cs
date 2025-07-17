using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCusto : IRepositorio<Custo>
    {
        IList<Custo> GetAllByIDNota(int idNota);
    }
}

