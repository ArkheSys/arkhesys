using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ContatoDetalheController : BaseController<ContatoDetalhe>
	{
		#region Singleton
        private static ContatoDetalheController instance;
        private static IRepositorioContatoDetalhe repositorioContatoDetalhe;

        private ContatoDetalheController()
        { }
		
		static ContatoDetalheController()
        {
            instance = new ContatoDetalheController();
            repositorioT = RepositorioFactory<ContatoDetalhe>.GetRepositorio();
            repositorioContatoDetalhe = (IRepositorioContatoDetalhe)repositorioT;
        }
			
		public static ContatoDetalheController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ContatoDetalhe objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ContatoDetalhe> GetPorIDContato(int IDContato)
        {
            return repositorioContatoDetalhe.GetPorIDContato(IDContato);
        }
	}
}
