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
	public class PLPController : BaseController<PLP>
	{
		#region Singleton
        private static PLPController instance;
        private static IRepositorioPLP repositorioPLP;

        private PLPController()
        { }
		
		static PLPController()
        {
            instance = new PLPController();
            repositorioT = RepositorioFactory<PLP>.GetRepositorio();
            repositorioPLP = (IRepositorioPLP)repositorioT;
        }
			
		public static PLPController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PLP objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxyPLPDetalhe> GetListaPedidosParaPLP()
        {
            IList<pxyPLPDetalhe> plpsDetalhe = new List<pxyPLPDetalhe>();
            pxyPLPDetalhe plpDetalhePxy = new pxyPLPDetalhe();

            string aux = "";

            var plpDetalheAgrupado = repositorioPLP.GetListaPedidosParaPLP().Select(i => i).GroupBy(i => i.IdPedido);

            foreach (var item in plpDetalheAgrupado)
            {
                foreach (var t in item)
                {
                    if (t.IdPedido == item.Key)
                    {
                        aux += t.CodigosDeRastreamento + ",";
                    }

                    plpDetalhePxy = t;
                }

                plpDetalhePxy.CodigosDeRastreamento = aux.Substring(0, aux.Length -1);
                plpsDetalhe.Add(plpDetalhePxy);

                plpDetalhePxy = new pxyPLPDetalhe();
                aux = "";
            }

            return plpsDetalhe;

        }

        public IList<pxyPLPDetalhe> GetListaPedidosParaPLPComIDPLP(int idPLP)
        {
            IList<pxyPLPDetalhe> plpsDetalhe = new List<pxyPLPDetalhe>();
            pxyPLPDetalhe plpDetalhePxy = new pxyPLPDetalhe();

            string aux = "";

            var plpDetalheAgrupado = repositorioPLP.GetListaPedidosParaPLPComIDPLP(idPLP).Select(i => i).GroupBy(i => i.IdPedido);

            foreach (var item in plpDetalheAgrupado)
            {
                foreach (var t in item)
                {
                    if (t.IdPedido == item.Key)
                    {
                        aux += t.CodigosDeRastreamento + ",";
                    }

                    plpDetalhePxy = t;
                }

                plpDetalhePxy.CodigosDeRastreamento = aux.Substring(0, aux.Length - 1);
                plpsDetalhe.Add(plpDetalhePxy);

                plpDetalhePxy = new pxyPLPDetalhe();
                aux = "";
            }

            return plpsDetalhe;

        }
	}
}
