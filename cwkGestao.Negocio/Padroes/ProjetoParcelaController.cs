using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class ProjetoParcelaController : BaseController<ProjetoParcela>
	{
		#region Singleton
        private static ProjetoParcelaController instance;
        private static IRepositorioProjetoParcela repositorioProjetoParcela;

        private ProjetoParcelaController()
        { }

        static ProjetoParcelaController()
        {
            instance = new ProjetoParcelaController();
            repositorioT = RepositorioFactory<ProjetoParcela>.GetRepositorio();
            repositorioProjetoParcela = (IRepositorioProjetoParcela)repositorioT;
        }

        public static ProjetoParcelaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(ProjetoParcela objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ProjetoParcela> GetParcelasPorProjeto(Projeto p)
        {
            return repositorioProjetoParcela.GetParcelasPorProjeto(p);
        }
	}
}
