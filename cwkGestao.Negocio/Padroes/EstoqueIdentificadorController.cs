using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class EstoqueIdentificadorController : BaseController<EstoqueIdentificador>
	{
		#region Singleton
        private static EstoqueIdentificadorController instance;
        private static IRepositorioEstoqueIdentificador repositorioEstoqueIdentificador;

        private EstoqueIdentificadorController()
        { }
		
		static EstoqueIdentificadorController()
        {
            instance = new EstoqueIdentificadorController();
            repositorioT = RepositorioFactory<EstoqueIdentificador>.GetRepositorio();
            repositorioEstoqueIdentificador = (IRepositorioEstoqueIdentificador)repositorioT;
        }
			
		public static EstoqueIdentificadorController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(EstoqueIdentificador objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
