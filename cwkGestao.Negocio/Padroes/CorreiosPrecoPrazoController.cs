using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CorreiosPrecoPrazoController : BaseController<CorreiosPrecoPrazo>
	{
		#region Singleton
        private static CorreiosPrecoPrazoController instance;
        private static IRepositorioCorreiosPrecoPrazo repositorioCorreiosPrecoPrazo;

        private CorreiosPrecoPrazoController()
        { }
		
		static CorreiosPrecoPrazoController()
        {
            instance = new CorreiosPrecoPrazoController();
            repositorioT = RepositorioFactory<CorreiosPrecoPrazo>.GetRepositorio();
            repositorioCorreiosPrecoPrazo = (IRepositorioCorreiosPrecoPrazo)repositorioT;
        }
			
		public static CorreiosPrecoPrazoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CorreiosPrecoPrazo objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<CorreiosPrecoPrazo> GetPorIdPedido(int idPedido)
        {
            return repositorioCorreiosPrecoPrazo.GetPorIDPedido(idPedido);
        }

        public IList<CorreiosPrecoPrazo> GetPorIdRastreamento(int idRastreamento)
        {
            return repositorioCorreiosPrecoPrazo.GetPorIDRastreamento(idRastreamento);
        }
        public IList<CorreiosPrecoPrazo> GetSelecionadosPorIDPedido(int IdPedido)
        {
            return repositorioCorreiosPrecoPrazo.GetSelecionadosPorIDPedido(IdPedido);
        }
	}
}
