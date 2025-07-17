using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CteCargaController : BaseController<CteCarga>
	{
		#region Singleton
        private static CteCargaController instance;
        private static IRepositorioCteCarga repositorioCteCarga;

        private CteCargaController()
        { }
		
		static CteCargaController()
        {
            instance = new CteCargaController();
            repositorioT = RepositorioFactory<CteCarga>.GetRepositorio();
            repositorioCteCarga = (IRepositorioCteCarga)repositorioT;
        }
			
		public static CteCargaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CteCarga objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
