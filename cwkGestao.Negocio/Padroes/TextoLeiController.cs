using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TextoLeiController : BaseController<TextoLei>
	{
		#region Singleton
        private static TextoLeiController instance;
        private static IRepositorioTextoLei repositorioTextoLei;

        private TextoLeiController()
        { }
		
		static TextoLeiController()
        {
            instance = new TextoLeiController();
            repositorioT = RepositorioFactory<TextoLei>.GetRepositorio();
            repositorioTextoLei = (IRepositorioTextoLei)repositorioT;
        }
			
		public static TextoLeiController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TextoLei objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
