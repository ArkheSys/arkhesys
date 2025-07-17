using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCotacaoMoeda : IRepositorio<CotacaoMoeda>
    {
        IList<CotacaoMoeda> GetTodasCotacaoMoedas();

        IList<pxCotacaoMoeda> GetCotacoesMoeda();
    }
}
