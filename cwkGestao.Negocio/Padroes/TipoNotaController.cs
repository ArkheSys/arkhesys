using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TipoNotaController : BaseController<TipoNota>
	{
		#region Singleton
        private static TipoNotaController instance;
        private static IRepositorioTipoNota repositorioTipoNota;

        private TipoNotaController()
        { }
		
		static TipoNotaController()
        {
            instance = new TipoNotaController();
            repositorioT = RepositorioFactory<TipoNota>.GetRepositorio();
            repositorioTipoNota = (IRepositorioTipoNota)repositorioT;
        }
			
		public static TipoNotaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TipoNota objeto)
        {
            return new Dictionary<string, string>();
        }

        public int GetTipoFrete()
        {
            return Convert.ToInt32(null);
        }

        public IList<TipoNota> GetAllPorCategoria(Enum categoria)
        {
            return repositorioTipoNota.GetAllPorCategoria(categoria);
        }
	}
}
