using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ImeiController : BaseController<Imei>
    {
        #region Singleton

        private static readonly IRepositorioImei repositorioImei;

        private ImeiController() { }

        static ImeiController()
        {
            Instancia = new ImeiController();
            repositorioT = RepositorioFactory<Imei>.GetRepositorio();
            repositorioImei = (IRepositorioImei) repositorioT;
        }

        public static ImeiController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Imei objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Imei> GetAllByIDProduto(int IDProduto, bool bAtivo)
        {
            return repositorioImei.GetAllByIDProduto(IDProduto, bAtivo);
        }

        public IList<Imei> GetAllByIDNotaItemEntrada(int IDNotaItemEntrada, bool bAtivo)
        {
            return repositorioImei.GetAllByIDNotaItemEntrada(IDNotaItemEntrada, bAtivo);
        }

        public IList<Imei> GetAllByIDNotaItemSaida(int IDNotaItemSaida, bool bAtivo)
        {
            return repositorioImei.GetAllByIDNotaItemSaida(IDNotaItemSaida, bAtivo);
        }

        public IList<Imei> GetAllAtivos()
        {
            return repositorioImei.GetAllAtivos();
        }

        public IList<Imei> GetAllInativos()
        {
            return repositorioImei.GetAllInativos();
        }

        public static bool VerificaUtilizacaoImei(Produto produtoSelecionado)
        {
            Configuracao conf = ConfiguracaoController.Instancia.GetConfiguracao();

            return produtoSelecionado.UtilizarIMEI && conf.UtilizaControleIMEI;
        }

        public static bool VerificaUtilizacaoImei(IList<Produto> lstProdutos)
        {
            var configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            if (!configuracao.UtilizaControleIMEI) return false;

            int numProdUtilizaImei = lstProdutos.Count(s => s.UtilizarIMEI);
            return numProdUtilizaImei > 0;
        }

        public IList<Imei> GetAllByIDProdutoByIDNotaItem(int IDProduto, int IDNotaItem)
        {
            return repositorioImei.GetAllByIDProdutoByIDNotaItem(IDProduto, IDNotaItem);
        }
    }
}