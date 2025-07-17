using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
    public partial class Grupo1Controller : BaseController<Grupo1>
    {
        #region Singleton
        private static Grupo1Controller instance;
        private static IRepositorioGrupo1 repositorioGrupoEstoque;

        private Grupo1Controller()
        { }

        static Grupo1Controller()
        {
            instance = new Grupo1Controller();
            repositorioT = RepositorioFactory<Grupo1>.GetRepositorio();
            repositorioGrupoEstoque = (IRepositorioGrupo1)repositorioT;
        }

        public static Grupo1Controller Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Grupo1 objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();            

            return ret;
        }
    }
}
