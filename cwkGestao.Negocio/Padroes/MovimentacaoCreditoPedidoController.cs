using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

using Modelo.ModeloProxy;

namespace cwkGestao.Negocio
{
    public class MovimentacaoCreditoPedidoController : BaseController<MovimentacaoCreditoPedido>
	{
		#region Singleton
        private static MovimentacaoCreditoPedidoController instance;
        private static IRepositorioMovimentacaoCreditoPedido repositorioMovimentacaoCreditoPedido;

        private MovimentacaoCreditoPedidoController()
        { }
		
		static MovimentacaoCreditoPedidoController()
        {
            instance = new MovimentacaoCreditoPedidoController();
            repositorioT = RepositorioFactory<MovimentacaoCreditoPedido>.GetRepositorio();
            repositorioMovimentacaoCreditoPedido = (IRepositorioMovimentacaoCreditoPedido)repositorioT;
        }
			
		public static MovimentacaoCreditoPedidoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(MovimentacaoCreditoPedido objeto)
        {
            return new Dictionary<string, string>();
        }


        public IList<MovimentacaoCreditoPedido> GetTodasMovimentacoes()
        {
            return repositorioMovimentacaoCreditoPedido.GetTodasMovimentacoes();
        }

        public virtual IList<pxyClientesComCredito> GetAllClientesComCredito()
        {
            return repositorioMovimentacaoCreditoPedido.GetAllClientesComCredito();
        }

    }
}
