using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
    public partial class Grupo3Controller : BaseController<Grupo3>
    {
        #region Singleton
        private static Grupo3Controller instance;
        private static IRepositorioGrupo3 repositorioGrupoEstoque;

        private Grupo3Controller()
        { }

        static Grupo3Controller()
        {
            instance = new Grupo3Controller();
            repositorioT = RepositorioFactory<Grupo3>.GetRepositorio();
            repositorioGrupoEstoque = (IRepositorioGrupo3)repositorioT;
        }

        public static Grupo3Controller Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Grupo3 objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
           
            return ret;
        }
    }
}
