using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaMedicaoPerfuracaoController : BaseController<Ofd_PropostaMedicaoPerfuracao>
	{
		#region Singleton
        private static Ofd_PropostaMedicaoPerfuracaoController instance;
        private static IRepositorioOfd_PropostaMedicaoPerfuracao repositorioOfd_PropostaMedicaoPerfuracao; 

        private Ofd_PropostaMedicaoPerfuracaoController()
        { }
		
		static Ofd_PropostaMedicaoPerfuracaoController()
        {
            instance = new Ofd_PropostaMedicaoPerfuracaoController();
            repositorioT = RepositorioFactory<Ofd_PropostaMedicaoPerfuracao>.GetRepositorio();
            repositorioOfd_PropostaMedicaoPerfuracao = (IRepositorioOfd_PropostaMedicaoPerfuracao)repositorioT;
        }
			
		public static Ofd_PropostaMedicaoPerfuracaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaMedicaoPerfuracao objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<cwkGestao.Modelo.Proxy.pxyFechamentoMedicao> GetFechamentoByProposta(Ofd_Proposta proposta)
        {
            return repositorioOfd_PropostaMedicaoPerfuracao.GetFechamentoByProposta(proposta);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyFaturamentoPerfuracao> GetByPropostaENumeroMedicao(Ofd_Proposta proposta, int numero)
        {
            return repositorioOfd_PropostaMedicaoPerfuracao.GetByPropostaENumeroMedicao(proposta, numero);
        }

        public IList<Ofd_PropostaMedicaoPerfuracao> GetByProposta(Ofd_Proposta ofd_Proposta)
        {
            return repositorioOfd_PropostaMedicaoPerfuracao.GetByProposta(ofd_Proposta);
        }
    }
}
