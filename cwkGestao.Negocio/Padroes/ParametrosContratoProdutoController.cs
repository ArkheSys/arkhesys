using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ParametrosContratoProdutoController : BaseController<ParametrosContratoProduto>
	{
		#region Singleton
        private static ParametrosContratoProdutoController instance;
        private static IRepositorioParametrosContratoProduto repositorioParametrosContratoProduto;

        private ParametrosContratoProdutoController()
        { }
		
		static ParametrosContratoProdutoController()
        {
            instance = new ParametrosContratoProdutoController();
            repositorioT = RepositorioFactory<ParametrosContratoProduto>.GetRepositorio();
            repositorioParametrosContratoProduto = (IRepositorioParametrosContratoProduto)repositorioT;
        }
			
		public static ParametrosContratoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ParametrosContratoProduto objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
