using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ContratoControleController : BaseController<ContratoControle>
	{
		#region Singleton
        private static ContratoControleController instance;
        private static IRepositorioContratoControle repositorioContratoControle;

        private ContratoControleController()
        { }
		
		static ContratoControleController()
        {
            instance = new ContratoControleController();
            repositorioT = RepositorioFactory<ContratoControle>.GetRepositorio();
            repositorioContratoControle = (IRepositorioContratoControle)repositorioT;
        }
			
		public static ContratoControleController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ContratoControle objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ContratoControle> GetRegistrosContratosFaturados(int idContrato, int tpProdutoServico, int idItemContrato)
        {
            return repositorioContratoControle.GetRegistrosContratosFaturados(idContrato, tpProdutoServico, idItemContrato);
        }

        public ContratoControle LoadObjectByIDNotaItem(int idNotaItem)
        {
            return repositorioContratoControle.LoadObjectByIDNotaItem(idNotaItem);
        }

        public ContratoControle LoadObjectByIDNotaItemServico(int idNotaItemServico)
        {
            return repositorioContratoControle.LoadObjectByIDNotaItemServico(idNotaItemServico);
        }
	}
}
