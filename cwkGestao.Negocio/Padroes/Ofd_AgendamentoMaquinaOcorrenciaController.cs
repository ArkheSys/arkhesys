using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_AgendamentoMaquinaOcorrenciaController : BaseController<Ofd_AgendamentoMaquinaOcorrencia>
	{
		#region Singleton
        private static Ofd_AgendamentoMaquinaOcorrenciaController instance;
        private static IRepositorioOfd_AgendamentoMaquinaOcorrencia repositorioOfd_AgendamentoMaquinaOcorrencia;

        private Ofd_AgendamentoMaquinaOcorrenciaController()
        { }
		
		static Ofd_AgendamentoMaquinaOcorrenciaController()
        {
            instance = new Ofd_AgendamentoMaquinaOcorrenciaController();
            repositorioT = RepositorioFactory<Ofd_AgendamentoMaquinaOcorrencia>.GetRepositorio();
            repositorioOfd_AgendamentoMaquinaOcorrencia = (IRepositorioOfd_AgendamentoMaquinaOcorrencia)repositorioT;
        }
			
		public static Ofd_AgendamentoMaquinaOcorrenciaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_AgendamentoMaquinaOcorrencia objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
