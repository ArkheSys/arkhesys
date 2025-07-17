using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public class Ofd_PropostaMedicaoController : BaseController<Ofd_PropostaMedicao>
    {
        #region Singleton
        private static Ofd_PropostaMedicaoController instance;
        private static IRepositorioOfd_PropostaMedicao repositorioOfd_PropostaMedicao;

        private Ofd_PropostaMedicaoController()
        { }

        static Ofd_PropostaMedicaoController()
        {
            instance = new Ofd_PropostaMedicaoController();
            repositorioT = RepositorioFactory<Ofd_PropostaMedicao>.GetRepositorio();
            repositorioOfd_PropostaMedicao = (IRepositorioOfd_PropostaMedicao)repositorioT;
        }

        public static Ofd_PropostaMedicaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaMedicao objeto)
        {
            return new Dictionary<string, string>();
        }

        public int[] GetMedicoesNaoFaturadasByProposta(Ofd_Proposta ofd_Proposta)
        {
            return repositorioOfd_PropostaMedicao.GetMedicoesNaoFaturadasByProposta(ofd_Proposta);
        }

        public Ofd_PropostaMedicao GetByPropostaENumero(Ofd_Proposta proposta, int numero)
        {
            return repositorioOfd_PropostaMedicao.GetByPropostaENumero(proposta, numero);
        }

        public IList<Ofd_PropostaMedicao> GetLstByPropostaENumero(Ofd_Proposta proposta, int numero)
        {
            return repositorioOfd_PropostaMedicao.GetLstByPropostaENumero(proposta, numero);
        }

        public int[] GetMedicoesFaturadasByProposta(Ofd_Proposta ofd_Proposta)
        {
            return repositorioOfd_PropostaMedicao.GetMedicoesFaturadasByProposta(ofd_Proposta);
        }

        public IList<Ofd_PropostaMedicao> GetByObra(Ofd_Obra obra)
        {
            return repositorioOfd_PropostaMedicao.GetByObra(obra);
        }

        public IList<pxyRelatorioMedicaoMes> GetRelatorioByMes(DateTime mes)
        {
            var medicoes = repositorioOfd_PropostaMedicao.GetByRelatorioMes(mes);
            return (from m in medicoes
                    group m by m.Proposta.Obra into g
                    select new pxyRelatorioMedicaoMes(g)).ToList();
        }
    }
}
