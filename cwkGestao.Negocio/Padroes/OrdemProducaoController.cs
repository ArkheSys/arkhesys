using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class OrdemProducaoController : BaseController<OrdemProducao>
	{
		#region Singleton
        private static OrdemProducaoController instance;
        private static IRepositorioOrdemProducao repositorioOrdemProducao;

        private OrdemProducaoController()
        { }
		
		static OrdemProducaoController()
        {
            instance = new OrdemProducaoController();
            repositorioT = RepositorioFactory<OrdemProducao>.GetRepositorio();
            repositorioOrdemProducao = (IRepositorioOrdemProducao)repositorioT;
        }
			
		public static OrdemProducaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(OrdemProducao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public OrdemProducao GetByCodigo(int codigo)
        {
            return repositorioOrdemProducao.GetByCodigo(codigo);
        }
	}
}
