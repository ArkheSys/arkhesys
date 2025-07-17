using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Windows.Forms;

namespace cwkGestao.Negocio
{
	public class ContatoController : BaseController<Contato>
	{
		#region Singleton
        private static ContatoController instance;
        private static IRepositorioContato repositorioContato;

        private ContatoController()
        { }
		
		static ContatoController()
        {
            instance = new ContatoController();
            repositorioT = RepositorioFactory<Contato>.GetRepositorio();
            repositorioContato = (IRepositorioContato)repositorioT;
        }
			
		public static ContatoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Contato objeto)
        {
            return new Dictionary<string, string>();
        }

        public pxyAvisos GetAvisosAnteriores(int IDUsuario)
        {
            return repositorioContato.GetAvisosAnteriores(IDUsuario);
        }

        public pxyAvisos GetAvisosPosteriores(int IDUsuario)
        {
            return repositorioContato.GetAvisosPosteriores(IDUsuario);
        }

        public pxyAvisosPorPeriodo GetAvisosPorPeriodo(int IDUsuario)
        {
            return repositorioContato.GetAvisosPorPeriodo(IDUsuario);
        }

    }
}
