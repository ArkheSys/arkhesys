using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Exceptions;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class EstoqueController : BaseController<Estoque>
    {
        #region Singleton

        private static readonly IRepositorioEstoque repositorioEstoque;

        private EstoqueController() { }

        static EstoqueController()
        {
            Instancia = new EstoqueController();
            repositorioT = RepositorioFactory<Estoque>.GetRepositorio();
            repositorioEstoque = (IRepositorioEstoque) repositorioT;
        }

        public static EstoqueController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Estoque objeto)
        {
            return new Dictionary<string, string>();
        }

        public List<Pedido> VerificarEstoqueNegativo(Nota nota)
        {
            List<Pedido> pedidosCompra = new List<Pedido>();
            if (nota.Ent_Sai != InOutType.Entrada)
            {
                StringBuilder produtosEstoqueNegativo = new StringBuilder();
                IList<pxyEstoque> pxyEstoques = new List<pxyEstoque>();

                foreach (var item in nota.NotaItems)
                {
                    pxyEstoque estoque = null; //ProdutoController.Instancia.GetListaEstoque(item.Produto.ID).Where(l => l.LocalEstoque == nota.LocalEstoqueNota.Descricao).FirstOrDefault();

                    if (estoque != null && estoque.Saldo - item.Quantidade < 0)
                        produtosEstoqueNegativo.AppendLine(item.Produto.Codigo + " - " + item.ProdutoNome);

                    VerificaEstoqueMinimo(nota.GerouPedidoCompra, pxyEstoques, estoque, item.Produto, item.Quantidade);
                }

                if (produtosEstoqueNegativo.Length > 0)
                {
                    TipoNota tipoNota = nota.TipoNota;
                    if (tipoNota != null && !tipoNota.BPermiteEstqueNegativo) throw new EstoqueNegativoException(produtosEstoqueNegativo.ToString());
                }

                if (pxyEstoques.Count > 0)
                {
                    pedidosCompra = PedidoController.Instancia.GerarPedidoCompra(nota.Filial, nota.LocalEstoqueNota, pxyEstoques).ToList();
                    nota.GerouPedidoCompra = true;
                }
            }

            return pedidosCompra;
        }

        public List<Pedido> VerificarEstoqueNegativo(Pedido pedido)
        {
            List<Pedido> pedidosCompra = new List<Pedido>();
            if (pedido.Ent_Sai != 1)
            {
                IList<pxyEstoque> pxyEstoques = new List<pxyEstoque>();
                StringBuilder produtosEstoqueNegativo = new StringBuilder();

                foreach (var item in pedido.Items)
                {
                    pxyEstoque estoque = null;
                    try { estoque = ProdutoController.Instancia.GetListaEstoque(item.Produto.ID).FirstOrDefault(l => l.LocalEstoque == pedido.LocalEstoquePedido.Descricao); }
                    catch { estoque = null; }

                    if (estoque != null && estoque.Saldo - item.Quantidade < 0)
                        produtosEstoqueNegativo.AppendLine(item.Produto.Codigo + " - " + item.ProdutoNome);

                    VerificaEstoqueMinimo(pedido.GerouPedidoCompra, pxyEstoques, estoque, item.Produto, item.Quantidade);
                }

                if (produtosEstoqueNegativo.Length > 0)
                {
                    TipoPedido tipoPedido = pedido.TipoPedido;
                    if (tipoPedido != null && !tipoPedido.BPermiteEstoqueNegativo) throw new EstoqueNegativoException(produtosEstoqueNegativo.ToString());
                }

                if (pxyEstoques.Count > 0)
                {
                    pedido.GerouPedidoCompra = true;
                    pedidosCompra = PedidoController.Instancia.GerarPedidoCompra(pedido.Filial, pedido.LocalEstoquePedido, pxyEstoques).ToList();
                }
            }

            return pedidosCompra;
        }

        private static void VerificaEstoqueMinimo(bool gerouPedidoCompra, IList<pxyEstoque> pxyEstoques,
            pxyEstoque estoque, Produto produto, decimal quantidade)
        {
            if (!gerouPedidoCompra && ConfiguracaoController.Instancia.GetConfiguracao().GerarPedidoCompra &&
                estoque != null && estoque.Saldo - quantidade < estoque.EstoqueMin)
            {
                estoque.Produto = produto;
                estoque.QtdPedido = quantidade;
                pxyEstoques.Add(estoque);
            }
        }

        public Estoque GetEstoque(LocalEstoque localEstoque, Produto produto)
        {
            var ret = repositorioEstoque.GetEstoque(localEstoque, produto);
            if (ret == null)
                ret = new Estoque {LocalEstoque = localEstoque, Produto = produto};
            return ret;
        }

        public Estoque GetEstoque(Filial filial, Produto produto)
        {
            var ret = repositorioEstoque.GetEstoque(filial, produto);
            if (ret == null)
                ret = new Estoque { Produto = produto };
            return ret;
        }

        public List<Estoque> GetEstoque(string filiais, Produto produto)
        {
            return repositorioEstoque.GetEstoque(filiais, produto);
        }

        public List<Estoque> GetEstoque(string filiais, GrupoEstoque grupoEstoque)
        {
            return repositorioEstoque.GetEstoque(filiais, grupoEstoque);
        }

        public List<Estoque> GetEstoque(string filiais, string produtoNome)
        {
            return repositorioEstoque.GetEstoque(filiais, produtoNome);
        }

        public IList<Estoque> GetEstoque(Filial Filial)
        {
            return repositorioEstoque.GetEstoque(Filial);
        }

        public IList<object> GetRelMovimentacaoPorProduto(string filiais, string produtos, string classificacao, string tiponota, DateTime datainicial, DateTime datafinal)
        {
            return repositorioEstoque.GetRelMovimentacaoPorProduto(filiais, produtos, classificacao, tiponota, datainicial, datafinal);
        }

        public IList<object> GetRelTempoEstoque(string filiais, string produtos, string grupo)
        {
            return repositorioEstoque.GetRelTempoEstoque(filiais, produtos, grupo);
        }

        public IList<EstoqueProdutoView> GetEstoquePorFilial(Filial filial)
        {
            return repositorioEstoque.GetEstoquePorFilial(filial);
        }

        public IList<Estoque> GetEstoquePorProduto(Produto produto)
        {
            return repositorioEstoque.GetEstoquePorProduto(produto);
        }

        public IList<EstoqueProdutoView> GetEstoquePorLocalEstoque(LocalEstoque localEstoque)
        {
            return repositorioEstoque.GetEstoquePorLocalEstoque(localEstoque);
        }
    }
}