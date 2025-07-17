using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FichaController : BaseController<Ficha>
	{
		#region Singleton
        private static FichaController instance;
        private static IRepositorioFicha repositorioFicha;

        private FichaController()
        { }
		
		static FichaController()
        {
            instance = new FichaController();
            repositorioT = RepositorioFactory<Ficha>.GetRepositorio();
            repositorioFicha = (IRepositorioFicha)repositorioT;
        }
			
		public static FichaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ficha objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
