using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaMedicaoOcorrenciasController : BaseController<Ofd_PropostaMedicaoOcorrencias>
	{
		#region Singleton
        private static Ofd_PropostaMedicaoOcorrenciasController instance;
        private static IRepositorioOfd_PropostaMedicaoOcorrencias repositorioOfd_PropostaMedicaoOcorrencias;

        private Ofd_PropostaMedicaoOcorrenciasController()
        { }
		
		static Ofd_PropostaMedicaoOcorrenciasController()
        {
            instance = new Ofd_PropostaMedicaoOcorrenciasController();
            repositorioT = RepositorioFactory<Ofd_PropostaMedicaoOcorrencias>.GetRepositorio();
            repositorioOfd_PropostaMedicaoOcorrencias = (IRepositorioOfd_PropostaMedicaoOcorrencias)repositorioT;
        }
			
		public static Ofd_PropostaMedicaoOcorrenciasController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaMedicaoOcorrencias objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Ofd_PropostaMedicaoOcorrencias> GetByPropostaMedicao(int idPropostaMedicao)
        {
            return repositorioOfd_PropostaMedicaoOcorrencias.GetByPropostaMedicao(idPropostaMedicao);
        }
	}
}
