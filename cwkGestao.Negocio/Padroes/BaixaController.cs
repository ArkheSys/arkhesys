using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class BaixaController : BaseController<Baixa>
	{
		#region Singleton
        private static BaixaController instance;
        private static IRepositorioBaixa repositorioBaixa;

        private BaixaController()
        { }
		
		static BaixaController()
        {
            instance = new BaixaController();
            repositorioT = RepositorioFactory<Baixa>.GetRepositorio();
            repositorioBaixa = (IRepositorioBaixa)repositorioT;
        }
			
		public static BaixaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Baixa objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
