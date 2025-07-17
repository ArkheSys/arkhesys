using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class StatusAtendimentoController : BaseController<StatusAtendimento>
	{
		#region Singleton
        private static StatusAtendimentoController instance;
        private static IRepositorioStatusAtendimento repositorioStatusAtendimento;

        private StatusAtendimentoController()
        { }
		
		static StatusAtendimentoController()
        {
            instance = new StatusAtendimentoController();
            repositorioT = RepositorioFactory<StatusAtendimento>.GetRepositorio();
            repositorioStatusAtendimento = (IRepositorioStatusAtendimento)repositorioT;
        }
			
		public static StatusAtendimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(StatusAtendimento objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
