using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class BalancoController : BaseController<Balanco>
	{
		#region Singleton
        private static BalancoController instance;
        private static IRepositorioBalanco repositorioBalanco;

        private BalancoController()
        { }
		
		static BalancoController()
        {
            instance = new BalancoController();
            repositorioT = RepositorioFactory<Balanco>.GetRepositorio();
            repositorioBalanco = (IRepositorioBalanco)repositorioT;
        }
			
		public static BalancoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Balanco objeto)
        {
            return new Dictionary<string, string>();
        }

        public Balanco LoadBalancoComNotas(int idBalanco)
        {
            return repositorioBalanco.LoadBalancoComNotas(idBalanco);
        }
    }
}
