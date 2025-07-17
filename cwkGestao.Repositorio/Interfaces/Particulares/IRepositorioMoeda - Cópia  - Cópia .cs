using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioMoeda : IRepositorio<Moeda>
    {
        IList<Moeda> GetTodasMoedas();
    }
}
