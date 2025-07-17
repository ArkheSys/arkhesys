using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class OperacaoController : BaseController<Operacao>
	{
		#region Singleton
        private static OperacaoController instance;
        private static IRepositorioOperacao repositorioOperacao;

        private OperacaoController()
        { }
		
		static OperacaoController()
        {
            instance = new OperacaoController();
            repositorioT = RepositorioFactory<Operacao>.GetRepositorio();
            repositorioOperacao = (IRepositorioOperacao)repositorioT;
        }
			
		public static OperacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Operacao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
