using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Negocio.Padroes
{
    public class PerfilPisCofinsController : BaseController<PerfilPisCofins>
    {
        #region Singleton
        private static PerfilPisCofinsController instance;
        private static IRepositorioPerfilPisCofins repositorioPerfilPisCofins;

        private PerfilPisCofinsController()
        { }

        static PerfilPisCofinsController()
        {
            instance = new PerfilPisCofinsController();
            repositorioT = RepositorioFactory<PerfilPisCofins>.GetRepositorio();
            repositorioPerfilPisCofins = (IRepositorioPerfilPisCofins)repositorioT;
        }

        public static PerfilPisCofinsController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override Dictionary<string, string> ValidaObjeto(PerfilPisCofins objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
