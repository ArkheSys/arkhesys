using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_MedicaoServicosController : BaseController<Ofd_MedicaoServicos>
	{
		#region Singleton
        private static Ofd_MedicaoServicosController instance;
        private static IRepositorioOfd_MedicaoServicos repositorioOfd_MedicaoServicos;

        private Ofd_MedicaoServicosController()
        { }
		
		static Ofd_MedicaoServicosController()
        {
            instance = new Ofd_MedicaoServicosController();
            repositorioT = RepositorioFactory<Ofd_MedicaoServicos>.GetRepositorio();
            repositorioOfd_MedicaoServicos = (IRepositorioOfd_MedicaoServicos)repositorioT;
        }
			
		public static Ofd_MedicaoServicosController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_MedicaoServicos objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
