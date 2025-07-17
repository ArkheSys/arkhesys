using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaOrcamentoServicosController : BaseController<Ofd_PropostaOrcamentoServicos>
	{
		#region Singleton
        private static Ofd_PropostaOrcamentoServicosController instance;
        private static IRepositorioOfd_PropostaOrcamentoServicos repositorioOfd_PropostaOrcamentoServicos;

        private Ofd_PropostaOrcamentoServicosController()
        { }
		
		static Ofd_PropostaOrcamentoServicosController()
        {
            instance = new Ofd_PropostaOrcamentoServicosController();
            repositorioT = RepositorioFactory<Ofd_PropostaOrcamentoServicos>.GetRepositorio();
            repositorioOfd_PropostaOrcamentoServicos = (IRepositorioOfd_PropostaOrcamentoServicos)repositorioT;
        }
			
		public static Ofd_PropostaOrcamentoServicosController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaOrcamentoServicos objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Modelo.Ofd_PropostaOrcamentoServicos> GetOfdServicosPorIDOfd_Proposta(int IDOfd_Proposta)
        {
            return repositorioOfd_PropostaOrcamentoServicos.GetOfdServicosPorIDOfd_Proposta(IDOfd_Proposta);
        }
        public Modelo.Ofd_PropostaOrcamentoServicos GetOfdServicoPorIDServico(int IDServico)
        {
            return repositorioOfd_PropostaOrcamentoServicos.GetOfdServicoPorIDServico(IDServico);
        }
	}
}
