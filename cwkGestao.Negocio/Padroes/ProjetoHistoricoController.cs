using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProjetoHistoricoController : BaseController<ProjetoHistorico>
	{
		#region Singleton
        private static ProjetoHistoricoController instance;
        private static IRepositorioProjetoHistorico repositorioProjetoHistorico;

        private ProjetoHistoricoController()
        { }
		
		static ProjetoHistoricoController()
        {
            instance = new ProjetoHistoricoController();
            repositorioT = RepositorioFactory<ProjetoHistorico>.GetRepositorio();
            repositorioProjetoHistorico = (IRepositorioProjetoHistorico)repositorioT;
        }
			
		public static ProjetoHistoricoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProjetoHistorico objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
