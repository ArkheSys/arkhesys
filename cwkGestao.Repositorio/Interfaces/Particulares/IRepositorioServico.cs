using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioServico : IRepositorio<Servico>
    {
        Servico GetByCodigo(int codigo);
        IList<object> GetByInicial(string inicial, string final);
        IList<Modelo.Servico> GetServicosPorIDOfd_Proposta(int IDOfd_Proposta);
    }
}
