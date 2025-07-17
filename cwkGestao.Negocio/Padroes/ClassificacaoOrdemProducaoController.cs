using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ClassificacaoOrdemProducaoController : BaseController<ClassificacaoOrdemProducao>
	{
		#region Singleton
        private static ClassificacaoOrdemProducaoController instance;
        private static IRepositorioClassificacaoOrdemProducao repositorioClassificacaoOrdemProducao;

        private ClassificacaoOrdemProducaoController()
        { }
		
		static ClassificacaoOrdemProducaoController()
        {
            instance = new ClassificacaoOrdemProducaoController();
            repositorioT = RepositorioFactory<ClassificacaoOrdemProducao>.GetRepositorio();
            repositorioClassificacaoOrdemProducao = (IRepositorioClassificacaoOrdemProducao)repositorioT;
        }
			
		public static ClassificacaoOrdemProducaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ClassificacaoOrdemProducao objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            return ret;
        }
	}
}
