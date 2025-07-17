using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFollowUp : IRepositorio<FollowUp>
    {
        IList<FollowUp> GetTodasFollowUp();
    }
}
