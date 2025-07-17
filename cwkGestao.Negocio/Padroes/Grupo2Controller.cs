using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
    public partial class Grupo2Controller : BaseController<Grupo2>
    {
        #region Singleton
        private static Grupo2Controller instance;
        private static IRepositorioGrupo2 repositorioGrupoEstoque;

        private Grupo2Controller()
        { }

        static Grupo2Controller()
        {
            instance = new Grupo2Controller();
            repositorioT = RepositorioFactory<Grupo2>.GetRepositorio();
            repositorioGrupoEstoque = (IRepositorioGrupo2)repositorioT;
        }

        public static Grupo2Controller Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Grupo2 objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
          
            return ret;
        }
    }
}
