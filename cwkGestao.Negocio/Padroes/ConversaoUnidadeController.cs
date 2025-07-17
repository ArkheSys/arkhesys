using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public class ConversaoUnidadeController : BaseController<ConversaoUnidade>
	{
		#region Singleton
        private static ConversaoUnidadeController instance;
        private static IRepositorioConversaoUnidade repositorioConversaoUnidade;

        private ConversaoUnidadeController()
        { }
		
		static ConversaoUnidadeController()
        {
            instance = new ConversaoUnidadeController();
            repositorioT = RepositorioFactory<ConversaoUnidade>.GetRepositorio();
            repositorioConversaoUnidade = (IRepositorioConversaoUnidade)repositorioT;
        }
			
		public static ConversaoUnidadeController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ConversaoUnidade objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxyConversaoUnidade> GetConversaoUnidadeProduto(int idProduto)
        {
            return repositorioConversaoUnidade.GetConversaoUnidadeProduto(idProduto);
        }

        public bool ValidaExistenciaConversao(Unidade unidadeEntrada, Unidade unidadeSaida)
        {
            return repositorioConversaoUnidade.ValidaExistenciaConversao(unidadeEntrada, unidadeSaida);
        }
	}
}
