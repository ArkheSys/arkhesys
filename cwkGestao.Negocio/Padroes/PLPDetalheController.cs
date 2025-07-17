using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PLPDetalheController : BaseController<PLPDetalhe>
	{
		#region Singleton
        private static PLPDetalheController instance;
        private static IRepositorioPLPDetalhe repositorioPLPDetalhe;

        private PLPDetalheController()
        { }
		
		static PLPDetalheController()
        {
            instance = new PLPDetalheController();
            repositorioT = RepositorioFactory<PLPDetalhe>.GetRepositorio();
            repositorioPLPDetalhe = (IRepositorioPLPDetalhe)repositorioT;
        }
			
		public static PLPDetalheController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PLPDetalhe objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<PLPDetalhe> GetPLPDetalhePorIDPLP(int IDPLP)
        {
            var x = repositorioPLPDetalhe.GetPLPDetalhePorIDPLP(IDPLP);
            return x;
        }
	}
}
