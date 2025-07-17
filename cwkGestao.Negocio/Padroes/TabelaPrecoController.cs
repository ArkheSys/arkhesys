using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class TabelaPrecoController : BaseController<TabelaPreco>
	{
		#region Singleton
        private static TabelaPrecoController instance;
        private static IRepositorioTabelaPreco repositorioTabelaPreco;

        private TabelaPrecoController()
        { }
		
		static TabelaPrecoController()
        {
            instance = new TabelaPrecoController();
            repositorioT = RepositorioFactory<TabelaPreco>.GetRepositorio();
            repositorioTabelaPreco = (IRepositorioTabelaPreco)repositorioT;
        }
			
		public static TabelaPrecoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaPreco objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
                ret.Add("txtNome", "Campo obrigatório.");

            return ret;
        }

        public IList<TabelaPreco> hqlGetAllAtiva()
        {
            return repositorioTabelaPreco.hqlGetAllAtiva();
        }

        public IList<pxyProdutoTabelaPreco> GetProdutosPorTabelaPreco(int? IDTabelaPreco, int? CodigoInicial, int? CodigoFinal, bool? ProdutosAtivos)
        {
            return repositorioTabelaPreco.GetProdutosPorTabelaPreco(IDTabelaPreco, CodigoInicial, CodigoFinal, ProdutosAtivos);
        }
	}
}
