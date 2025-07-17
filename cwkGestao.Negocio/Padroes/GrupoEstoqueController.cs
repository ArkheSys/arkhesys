using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
    public partial class GrupoEstoqueController : BaseController<GrupoEstoque>
    {
        #region Singleton
        private static GrupoEstoqueController instance;
        private static IRepositorioGrupoEstoque repositorioGrupoEstoque;

        private GrupoEstoqueController()
        { }

        static GrupoEstoqueController()
        {
            instance = new GrupoEstoqueController();
            repositorioT = RepositorioFactory<GrupoEstoque>.GetRepositorio();
            repositorioGrupoEstoque = (IRepositorioGrupoEstoque)repositorioT;
        }

        public static GrupoEstoqueController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(GrupoEstoque objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            return ret;
        }

        public IList<GrupoEstoque> GetGruposEstoquePorClassificacao(string classificacao)
        {
            return repositorioGrupoEstoque.GetGruposEstoquePorClassificacao(classificacao);
        }

        protected override void Update(GrupoEstoque objeto)
        {
            repositorioGrupoEstoque.Merge(objeto);
        }
    }
}
