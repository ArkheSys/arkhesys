using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class EmpresaController : BaseController<Empresa>
    {
        #region Singleton
        private static EmpresaController instance;
        private static IRepositorioEmpresa repositorioEmpresa;

        private EmpresaController()
        { }

        static EmpresaController()
        {
            instance = new EmpresaController();
            repositorioT = RepositorioFactory<Empresa>.GetRepositorio();
            repositorioEmpresa = (IRepositorioEmpresa)repositorioT;
        }

        public static EmpresaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Empresa objeto)
        {
            return new Dictionary<string, string>();
        }

        public void InicializaBase()
        {
            repositorioEmpresa.InicializarBase();
        }
    }
}