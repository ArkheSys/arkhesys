using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.WebService;
using Ez.Newsletter.MagentoApi;
using cwkGestao.Negocio;
using System.Threading;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class ExportadorProduto : IExportador<Produto>
    {
        
        //private Ez.Newsletter.MagentoApi.Product ProdutoMagento;
        private Comunicador ComunicadorMagento;
        //string IDIntegracao;
        //string StatusProduto;

        public ExportadorProduto(Comunicador comunicador)
        {
            ComunicadorMagento = comunicador;
        }

        
        public void Exportar(Produto produto)
        {
            /*
            ProdutoMagento = new Ez.Newsletter.MagentoApi.Product();
            PreparaProduto(produto);

            if (produto.IDIntegracao > 0)
                ComunicadorMagento.AlterarProduto(ProdutoMagento, produto.IDIntegracao.ToString());
            else
            {
                IDIntegracao = ComunicadorMagento.IncluirProduto(ProdutoMagento);
                produto.IDIntegracao = Convert.ToInt32(IDIntegracao);
            }

            ProdutoController.Instancia.Salvar(produto, Acao.Alterar);
            */
        }

        /*
        public void PreparaProduto(Produto produto)
        {
            //// 1: Habilitado
            //// 2: Desabilitado
            //if (produto.Situacao == "0")
            //    StatusProduto = "2";
            //else
            //    StatusProduto = "1";

            //ProdutoMagento.name = produto.Nome;
            //ProdutoMagento.price = ProdutoController.Instancia.getPreco(produto, produto.TabelaPrecoProdutos.First().TabelaPreco, TipoPrecoType.Normal).ToString();
            //ProdutoMagento.sku = produto.SKU;
            //ProdutoMagento.weight = produto.PesoLiquido.ToString();
            //ProdutoMagento.tax_class_id = "1";

            //// Associação de categorias
            //ProdutoMagento.categories = new string[] { produto.GrupoEstoque.IDIntegracao.ToString() };

            //ProdutoMagento.description = produto.DescLonga;
            //ProdutoMagento.short_description = produto.DescCurta;
            //ProdutoMagento.model = produto.Model;
            //ProdutoMagento.status = StatusProduto;
        }
        */
    }
}