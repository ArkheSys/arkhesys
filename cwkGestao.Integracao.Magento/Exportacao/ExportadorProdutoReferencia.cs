using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.WebService;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class ExportadorProdutoReferencia : IExportador<Produto>
    {
        private catalogProductReturnEntity produtoMagento;
        private Comunicador comunicador;
        private IList<ProdutoSKU> listaSKU;
        private bool status;

        public ExportadorProdutoReferencia(Comunicador comunicador)
        {
            this.comunicador = comunicador;
        }

        public void Exportar(Produto produto)
        {
            //if (produto.NovoProduto)
            //{
            //    listaSKU = ProdutoSKUController.Instancia.GetSKUPorProduto(produto.ID);
            //    produtoMagento = new catalogProductReturnEntity();
            //    produtoMagento.price = ProdutoController.Instancia.getPreco(produto, produto.TabelaPrecoProdutos.First().TabelaPreco, TipoPrecoType.Normal).ToString();

            //    produtoMagento.description = produto.DescLonga1;
            //    produtoMagento.name = produto.Nome;
            //    produtoMagento.product_id = produto.ID.ToString();
            //    produtoMagento.sku = listaSKU.First().SKU;
            //    produtoMagento.meta_title = produto.Nome;
            //    produtoMagento.status = "Habilitado";
            //    produtoMagento.weight = produto.PesoLiquido.ToString();
                

            //    comunicador.ExportarNovoProduto(produtoMagento, listaSKU);

            //}
                produtoMagento = new catalogProductReturnEntity();
                produtoMagento.price = ProdutoController.Instancia.getPreco(produto, produto.TabelaPrecoProdutos.First().TabelaPreco, TipoPrecoType.Normal).ToString();

                listaSKU = ProdutoSKUController.Instancia.GetSKUPorProduto(produto.ID);

                status = comunicador.AlterarPrecoProdutos(produtoMagento, listaSKU);

            ProdutoController.Instancia.Salvar(produto, Acao.Alterar);
        }

        public bool StatusExportacao()
        {
            return status;
        }
    }
}