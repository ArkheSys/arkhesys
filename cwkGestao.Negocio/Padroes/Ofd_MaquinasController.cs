using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class Ofd_MaquinasController : BaseController<Ofd_Maquinas>
    {
        #region Singleton
        private static Ofd_MaquinasController instance;
        private static IRepositorioOfd_Maquinas repositorioOfd_Maquinas;

        private Ofd_MaquinasController()
        { }

        static Ofd_MaquinasController()
        {
            instance = new Ofd_MaquinasController();
            repositorioT = RepositorioFactory<Ofd_Maquinas>.GetRepositorio();
            repositorioOfd_Maquinas = (IRepositorioOfd_Maquinas)repositorioT;
        }

        public static Ofd_MaquinasController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Ofd_Maquinas objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
