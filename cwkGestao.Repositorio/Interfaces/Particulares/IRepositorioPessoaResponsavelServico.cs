using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPessoaResponsavelServico : IRepositorio<PessoaResponsavelServico>
    {
        IList<pxResponsavelServicoCliente> GetResponsaveisPorCliente(int? IDResponsavel, int? IDServico);
    }
}
