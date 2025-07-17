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
	public partial class PedidoParcelaController : BaseController<PedidoParcela>
	{
		#region Singleton
        private static PedidoParcelaController instance;
        private static IRepositorioPedidoParcela repositorioPedidoParcela;

        private PedidoParcelaController()
        { }
		
		static PedidoParcelaController()
        {
            instance = new PedidoParcelaController();
            repositorioT = RepositorioFactory<PedidoParcela>.GetRepositorio();
            repositorioPedidoParcela = (IRepositorioPedidoParcela)repositorioT;
        }
			
		public static PedidoParcelaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PedidoParcela objeto)
        {
            return new Dictionary<string,string>();
        }

        public IList<pxyRelFluxoCaixa> GetItemsFluxoDeCaixa(DateTime dataInicial, DateTime dataFinal, ConsiderarPedidos filtroPedidos)
        {
            if (filtroPedidos == ConsiderarPedidos.Nenhum)
                return new List<pxyRelFluxoCaixa>();
            return repositorioPedidoParcela.GetItemsFluxoDeCaixa(dataInicial, dataFinal, filtroPedidos);
        }

        public IList<PedidoParcela> GetParcelasPedido(Pedido p)
        {
            return repositorioPedidoParcela.GetParcelasPedido(p);
        }
	}
}
