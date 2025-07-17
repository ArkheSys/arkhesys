using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CotacaoTransportadoraPedidoController : BaseController<CotacaoTransportadoraPedido>
	{
		#region Singleton
        private static CotacaoTransportadoraPedidoController instance;
        private static IRepositorioCotacaoTransportadoraPedido repositorioCotacaoTransportadoraPedido;

        private CotacaoTransportadoraPedidoController()
        { }
		
		static CotacaoTransportadoraPedidoController()
        {
            instance = new CotacaoTransportadoraPedidoController();
            repositorioT = RepositorioFactory<CotacaoTransportadoraPedido>.GetRepositorio();
            repositorioCotacaoTransportadoraPedido = (IRepositorioCotacaoTransportadoraPedido)repositorioT;
        }
			
		public static CotacaoTransportadoraPedidoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CotacaoTransportadoraPedido objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
