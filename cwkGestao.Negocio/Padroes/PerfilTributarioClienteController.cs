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
    public class PerfilTributarioClienteController : BaseController<PerfilTributarioCliente>
    {
        #region Singleton
        private static PerfilTributarioClienteController instance;
        private static IRepositorioPerfilTributarioCliente repositorioPerfilTributarioCliente;

        private PerfilTributarioClienteController()
        { }

        static PerfilTributarioClienteController()
        {
            instance = new PerfilTributarioClienteController();
            repositorioT = RepositorioFactory<PerfilTributarioCliente>.GetRepositorio();
            repositorioPerfilTributarioCliente = (IRepositorioPerfilTributarioCliente)repositorioT;
        }

        public static PerfilTributarioClienteController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override Dictionary<string, string> ValidaObjeto(PerfilTributarioCliente objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
