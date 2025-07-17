using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOfd_Obra : IRepositorio<Ofd_Obra>
    {
        IList<Servico> GetServicosPorObra(int IDObra);
    }
}

