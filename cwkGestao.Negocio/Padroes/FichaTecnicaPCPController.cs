using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FichaTecnicaPCPController : BaseController<FichaTecnicaPCP>
	{
		#region Singleton
        private static FichaTecnicaPCPController instance;
        private static IRepositorioFichaTecnicaPCP repositorioFichaTecnicaPCP;

        private FichaTecnicaPCPController()
        { }
		
		static FichaTecnicaPCPController()
        {
            instance = new FichaTecnicaPCPController();
            repositorioT = RepositorioFactory<FichaTecnicaPCP>.GetRepositorio();
            repositorioFichaTecnicaPCP = (IRepositorioFichaTecnicaPCP)repositorioT;
        }
			
		public static FichaTecnicaPCPController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FichaTecnicaPCP objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<FichaTecnicaPCP> GetImpressao()
        {
            return repositorioFichaTecnicaPCP.GetImpressao();
        }

        public Dictionary<string, string> SalvarFichas(Produto p)
        {
            IList<FichaTecnicaPCP> Fichas = Extensions.Clone(p.FichaTecnicaPCP);
            foreach (FichaTecnicaPCP ficha in p.FichaTecnicaPCP)
            {
                ficha.FichaTecnicaPCPItens = null;
                ficha.FichaTecnicaPCPRegras = null;
            }
            ProdutoController pc = ProdutoController.Instancia;
            for (int i = 0; i < p.FichaTecnicaPCP.Count; i++)
            {
                p.FichaTecnicaPCP[i].FichaTecnicaPCPItens = Fichas[i].FichaTecnicaPCPItens;
                if (p.FichaTecnicaPCP[i].FichaTecnicaPCPItens != null)
                {
                    foreach (FichaTecnicaPCPItem item in p.FichaTecnicaPCP[i].FichaTecnicaPCPItens)
                    {
                        item.FichaTecnicaPCP = p.FichaTecnicaPCP[i];
                    }
                }
                p.FichaTecnicaPCP[i].FichaTecnicaPCPRegras = Fichas[i].FichaTecnicaPCPRegras;
                if (p.FichaTecnicaPCP[i].FichaTecnicaPCPRegras != null)
                {
                    foreach (FichaTecnicaPCPRegra item in p.FichaTecnicaPCP[i].FichaTecnicaPCPRegras)
                    {
                        item.FichaTecnicaPCP = p.FichaTecnicaPCP[i];
                    }
                }
            }
            try
            {
                pc.Salvar(p, Acao.Alterar);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            return new Dictionary<string,string>();
        }

        public IList<FichaTecnicaPCP> GetAllPorIDProduto(int id)
        {
            return repositorioFichaTecnicaPCP.GetAllPorIDProduto(id);
        }
	}

    public static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
