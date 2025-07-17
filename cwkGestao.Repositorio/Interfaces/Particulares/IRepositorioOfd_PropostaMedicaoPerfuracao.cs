using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOfd_PropostaMedicaoPerfuracao : IRepositorio<Ofd_PropostaMedicaoPerfuracao>
    {
        IList<cwkGestao.Modelo.Proxy.pxyFaturamentoPerfuracao> GetByPropostaENumeroMedicao(Ofd_Proposta proposta, int numero);
        IList<cwkGestao.Modelo.Proxy.pxyFechamentoMedicao> GetFechamentoByProposta(Ofd_Proposta proposta);

        IList<Ofd_PropostaMedicaoPerfuracao> GetByProposta(Ofd_Proposta ofd_Proposta);
    }
}

