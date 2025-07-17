using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_AgendamentoMaquinaController : BaseController<Ofd_AgendamentoMaquina>
	{
		#region Singleton
        private static Ofd_AgendamentoMaquinaController instance;
        private static IRepositorioOfd_AgendamentoMaquina repositorioOfd_AgendamentoMaquina;

        private Ofd_AgendamentoMaquinaController()
        { }
		
		static Ofd_AgendamentoMaquinaController()
        {
            instance = new Ofd_AgendamentoMaquinaController();
            repositorioT = RepositorioFactory<Ofd_AgendamentoMaquina>.GetRepositorio();
            repositorioOfd_AgendamentoMaquina = (IRepositorioOfd_AgendamentoMaquina)repositorioT;
        }
			
		public static Ofd_AgendamentoMaquinaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_AgendamentoMaquina objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
