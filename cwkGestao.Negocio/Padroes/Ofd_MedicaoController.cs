using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_MedicaoController : BaseController<Ofd_Medicao>
	{
		#region Singleton
        private static Ofd_MedicaoController instance;
        private static IRepositorioOfd_Medicao repositorioOfd_Medicao;

        private Ofd_MedicaoController()
        { }
		
		static Ofd_MedicaoController()
        {
            instance = new Ofd_MedicaoController();
            repositorioT = RepositorioFactory<Ofd_Medicao>.GetRepositorio();
            repositorioOfd_Medicao = (IRepositorioOfd_Medicao)repositorioT;
        }
			
		public static Ofd_MedicaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_Medicao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
