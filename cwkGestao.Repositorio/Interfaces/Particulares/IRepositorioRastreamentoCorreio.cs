using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioRastreamentoCorreio : IRepositorio<RastreamentoCorreio>
    {
        IList<RastreamentoCorreio> GetCodigosRastreamentosPorQuantidadeVolume(int quantidade, int idTipoServico);

        IList<RastreamentoCorreio> GetCodigosDisponiveis();

        IList<RastreamentoCorreio> GetCodigosDisponiveisPorTipoServico(TipoServicoTransporte tst);
    }
}

