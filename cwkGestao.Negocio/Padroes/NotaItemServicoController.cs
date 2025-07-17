using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Negocio.Tributacao;

namespace cwkGestao.Negocio
{
	public class NotaItemServicoController : BaseController<NotaItemServico>
	{
		#region Singleton
        private static NotaItemServicoController instance;
        private static IRepositorioNotaItemServico repositorioNotaItemServico;

        private NotaItemServicoController()
        { }

        static NotaItemServicoController()
        {
            instance = new NotaItemServicoController();
            repositorioT = RepositorioFactory<NotaItemServico>.GetRepositorio();
            repositorioNotaItemServico = (IRepositorioNotaItemServico)repositorioT;
        }

        public static NotaItemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaItemServico objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
