using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ClassificacaoController : BaseController<Classificacao>
	{
		#region Singleton
        private static ClassificacaoController instance;
        private static IRepositorioClassificacao repositorioClassificacao;

        private ClassificacaoController()
        { }
		
		static ClassificacaoController()
        {
            instance = new ClassificacaoController();
            repositorioT = RepositorioFactory<Classificacao>.GetRepositorio();
            repositorioClassificacao = (IRepositorioClassificacao)repositorioT;
        }
			
		public static ClassificacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Classificacao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
