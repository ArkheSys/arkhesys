using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_GrupoClientePrecoController : BaseController<Tel_GrupoClientePreco>
	{
		#region Singleton
        private static Tel_GrupoClientePrecoController instance;
        private static IRepositorioTel_GrupoClientePreco repositorioTel_GrupoClientePreco;

        private Tel_GrupoClientePrecoController()
        { }

        static Tel_GrupoClientePrecoController()
        {
            instance = new Tel_GrupoClientePrecoController();
            repositorioT = RepositorioFactory<Tel_GrupoClientePreco>.GetRepositorio();
            repositorioTel_GrupoClientePreco = (IRepositorioTel_GrupoClientePreco)repositorioT;
        }

        public static Tel_GrupoClientePrecoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_GrupoClientePreco objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
