using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FichaTecnicaPCPItemController : BaseController<FichaTecnicaPCPItem>
	{
		#region Singleton
        private static FichaTecnicaPCPItemController instance;
        private static IRepositorioFichaTecnicaPCPItem repositorioFichaTecnicaPCPItem;

        private FichaTecnicaPCPItemController()
        { }
		
		static FichaTecnicaPCPItemController()
        {
            instance = new FichaTecnicaPCPItemController();
            repositorioT = RepositorioFactory<FichaTecnicaPCPItem>.GetRepositorio();
            repositorioFichaTecnicaPCPItem = (IRepositorioFichaTecnicaPCPItem)repositorioT;
        }
			
		public static FichaTecnicaPCPItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FichaTecnicaPCPItem objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<FichaTecnicaPCP> GetFichaTecnicaByIdProduto(int IdProduto)
        {
            return repositorioFichaTecnicaPCPItem.GetFichaTecnicaByIdProduto(IdProduto);
        }

        public IList<FichaTecnicaPCPItem> GetFichaTecnicaPCPItensVinculadosComFichaTecnica(int IDFichaTecnicaPCP)
        {
            return repositorioFichaTecnicaPCPItem.GetFichaTecnicaPCPItensVinculadosComFichaTecnica(IDFichaTecnicaPCP);
        }
	}
}
