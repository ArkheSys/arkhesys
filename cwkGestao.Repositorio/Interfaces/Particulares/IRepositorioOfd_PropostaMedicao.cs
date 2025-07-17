using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOfd_PropostaMedicao : IRepositorio<Ofd_PropostaMedicao>
    {
        int[] GetMedicoesNaoFaturadasByProposta(Ofd_Proposta ofd_Proposta);

        IList<Ofd_PropostaMedicao> GetLstByPropostaENumero(Ofd_Proposta proposta, int numero);

        Ofd_PropostaMedicao GetByPropostaENumero(Ofd_Proposta proposta, int numero);

        int[] GetMedicoesFaturadasByProposta(Ofd_Proposta ofd_Proposta);

        IList<Ofd_PropostaMedicao> GetByObra(Ofd_Obra obra);

        IList<Ofd_PropostaMedicao> GetByRelatorioMes(DateTime mes);
    }
}

