using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ContratoServicoController : BaseController<ContratoServico>
	{
		#region Singleton
        private static ContratoServicoController instance;
        private static IRepositorioContratoServico repositorioContratoServico;

        private ContratoServicoController()
        { }
		
		static ContratoServicoController()
        {
            instance = new ContratoServicoController();
            repositorioT = RepositorioFactory<ContratoServico>.GetRepositorio();
            repositorioContratoServico = (IRepositorioContratoServico)repositorioT;
        }
			
		public static ContratoServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ContratoServico objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ContratoServico> GetContratosParaFaturar(DateTime referencia, Int32 vencimento)
        {
            return repositorioContratoServico.GetContratosParaFaturar(referencia, vencimento);
        }

        public IList<ContratoServico> GetContratosServicoPorIDContrato(int idContrato)
        {
            return repositorioContratoServico.GetContratosServicoPorIDContrato(idContrato);
        }

    }
}
