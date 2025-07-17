using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_OcorrenciasController : BaseController<Ofd_Ocorrencias>
	{
		#region Singleton
        private static Ofd_OcorrenciasController instance;
        private static IRepositorioOfd_Ocorrencias repositorioOfd_Ocorrencias;

        private Ofd_OcorrenciasController()
        { }
		
		static Ofd_OcorrenciasController()
        {
            instance = new Ofd_OcorrenciasController();
            repositorioT = RepositorioFactory<Ofd_Ocorrencias>.GetRepositorio();
            repositorioOfd_Ocorrencias = (IRepositorioOfd_Ocorrencias)repositorioT;
        }
			
		public static Ofd_OcorrenciasController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_Ocorrencias objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
