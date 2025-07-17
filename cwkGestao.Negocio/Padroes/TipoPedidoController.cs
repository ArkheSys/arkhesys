using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class TipoPedidoController : BaseController<TipoPedido>
	{
		#region Singleton
        private static TipoPedidoController instance;
        private static IRepositorioTipoPedido repositorioTipoPedido;

        private TipoPedidoController()
        { }
		
		static TipoPedidoController()
        {
            instance = new TipoPedidoController();
            repositorioT = RepositorioFactory<TipoPedido>.GetRepositorio();
            repositorioTipoPedido = (IRepositorioTipoPedido)repositorioT;
        }
			
		public static TipoPedidoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TipoPedido objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            return ret;
        }

        public IList<TipoPedido> GetAll(InOutType entSai, TipoPedidoType? tipoPedido)
        {
            return repositorioTipoPedido.GetAll(entSai, tipoPedido);
        }

        public IList<TipoPedido> GetByTipoAndEnt_Sai(InOutType EntSai)
        {
            return repositorioTipoPedido.GetByTipoAndEnt_Sai(EntSai);
        }
	}
}
