using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioServicoImposto : IRepositorio<ServicoImposto>
    {
        IList<object> GetByInicial(string inicial, string final);
    }
}