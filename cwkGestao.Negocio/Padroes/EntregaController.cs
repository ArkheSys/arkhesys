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
	public class EntregaController : BaseController<Entrega>
	{
		#region Singleton
        private static EntregaController instance;
        private static IRepositorioEntrega repositorioEntrega;

        private EntregaController()
        { }
		
		static EntregaController()
        {
            instance = new EntregaController();
            repositorioT = RepositorioFactory<Entrega>.GetRepositorio();
            repositorioEntrega = (IRepositorioEntrega)repositorioT;
        }
			
		public static EntregaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Entrega objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxyEntregas> GetPorAllPorIDPedido(int IDPedido)
        {
            decimal total = 0;
            IList<pxyEntregas> retorno = repositorioEntrega.GetPorAllPorIDPedido(IDPedido);
#pragma warning disable CS0168 // A variável "retornoFiltrado" está declarada, mas nunca é usada
            pxyEntregas retornoFiltrado;
#pragma warning restore CS0168 // A variável "retornoFiltrado" está declarada, mas nunca é usada

            foreach (var item in retorno)
            {
                if (total == 0)
                    total = item.QuantidadeEntregar;
                else
                    item.QuantidadeEntregar = total;

                total -= item.QuantidadeEntregue;
                item.QuantidadeRestante = total;
            }

            return retorno;
                
        }
    }
}
