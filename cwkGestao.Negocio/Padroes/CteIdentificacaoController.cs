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
	public class CteIdentificacaoController : BaseController<CteIdentificacao>
	{
		#region Singleton
        private static CteIdentificacaoController instance;
        private static IRepositorioCteIdentificacao repositorioCteIdentificacao;

        private CteIdentificacaoController()
        { }
		
		static CteIdentificacaoController()
        {
            instance = new CteIdentificacaoController();
            repositorioT = RepositorioFactory<CteIdentificacao>.GetRepositorio();
            repositorioCteIdentificacao = (IRepositorioCteIdentificacao)repositorioT;
        }
			
		public static CteIdentificacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CteIdentificacao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
