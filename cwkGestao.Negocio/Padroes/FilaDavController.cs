using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FilaDavController : BaseController<FilaDav>
	{
		#region Singleton
        private static FilaDavController instance;
        private static IRepositorioFilaDav repositorioFilaDav;

        private FilaDavController()
        { }
		
		static FilaDavController()
        {
            instance = new FilaDavController();
            repositorioT = RepositorioFactory<FilaDav>.GetRepositorio();
            repositorioFilaDav = (IRepositorioFilaDav)repositorioT;
        }
			
		public static FilaDavController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FilaDav objeto)
        {
            return new Dictionary<string, string>();
        }

        IList<cwkGestao.Modelo.FilaDav> GetPorIDPedido(int IDPedido)
        {
            return repositorioFilaDav.GetPorIDPedido(IDPedido);
        }
	}
}
