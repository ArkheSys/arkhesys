using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_ObraController : BaseController<Ofd_Obra>
	{
		#region Singleton
        private static Ofd_ObraController instance;
        private static IRepositorioOfd_Obra repositorioOfd_Obra;

        private Ofd_ObraController()
        { }
		
		static Ofd_ObraController()
        {
            instance = new Ofd_ObraController();
            repositorioT = RepositorioFactory<Ofd_Obra>.GetRepositorio();
            repositorioOfd_Obra = (IRepositorioOfd_Obra)repositorioT;
        }
			
		public static Ofd_ObraController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public IList<Servico> GetServicosPorObra(int IDObra)
        {
            return repositorioOfd_Obra.GetServicosPorObra(IDObra);
        }

		public override Dictionary<string, string> ValidaObjeto(Ofd_Obra objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
