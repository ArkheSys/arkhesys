using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOfd_PropostaOrcamentoServicos : IRepositorio<Ofd_PropostaOrcamentoServicos>
    {
        IList<Modelo.Ofd_PropostaOrcamentoServicos> GetOfdServicosPorIDOfd_Proposta(int IDOfd_Proposta);
        Modelo.Ofd_PropostaOrcamentoServicos GetOfdServicoPorIDServico(int IDServico);
    }
}

