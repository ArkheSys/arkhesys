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
	public class NotaImpostoServicoController : BaseController<NotaImpostoServico>
	{
		#region Singleton
        private static NotaImpostoServicoController instance;
        private static IRepositorioNotaImpostoServico repositorioNotaImpostoServico;

        private NotaImpostoServicoController()
        { }

        static NotaImpostoServicoController()
        {
            instance = new NotaImpostoServicoController();
            repositorioT = RepositorioFactory<NotaImpostoServico>.GetRepositorio();
            repositorioNotaImpostoServico = (IRepositorioNotaImpostoServico)repositorioT;
        }

        public static NotaImpostoServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaImpostoServico objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
