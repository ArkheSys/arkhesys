using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCondicaoFornecimento : IRepositorio<CondicaoFornecimento>
    {
        IList<CondicaoFornecimento> GetTodasCondicaoFornecimento();
    }
}
