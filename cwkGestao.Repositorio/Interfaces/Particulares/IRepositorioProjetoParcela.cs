using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProjetoParcela : IRepositorio<ProjetoParcela>
    {
        IList<ProjetoParcela> GetParcelasPorProjeto(Projeto proj);
    }
}

