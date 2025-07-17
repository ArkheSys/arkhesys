using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_GrupoClienteController : BaseController<Tel_GrupoCliente>
	{
		#region Singleton
        private static Tel_GrupoClienteController instance;
        private static IRepositorioTel_GrupoCliente repositorioTel_GrupoCliente;

        private Tel_GrupoClienteController()
        { }
		
		static Tel_GrupoClienteController()
        {
            instance = new Tel_GrupoClienteController();
            repositorioT = RepositorioFactory<Tel_GrupoCliente>.GetRepositorio();
            repositorioTel_GrupoCliente = (IRepositorioTel_GrupoCliente)repositorioT;
        }
			
		public static Tel_GrupoClienteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_GrupoCliente objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
