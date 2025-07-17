using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using System.Text.RegularExpressions;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Negocio.Impressao
{
    public class ImpressaoGs1DataBar
    {
        private readonly Nota nota;
        private readonly LogicaTelaNotaFiscal logicaTelaNota;

        public ImpressaoGs1DataBar(Nota notaSelecionada, LogicaTelaNotaFiscal logicaTela)
	    {
            nota = notaSelecionada;
            logicaTelaNota = logicaTela;
	    }

        public IEnumerable<ImpressaoEtiquetaProdutoView> GerarImpressaoEtiqueta()
        {
            foreach (var item in nota.NotaItems)
            {
                yield return AdicionaImpressaoEtiquetaView(item);              
            }
        }

        private ImpressaoEtiquetaProdutoView AdicionaImpressaoEtiquetaView(NotaItem item)
        {
            Produto Produto = ProdutoController.Instancia.LoadObjectById(item.Produto.ID);

            return new ImpressaoEtiquetaProdutoView(

                Produto.ID,
                Produto.Estoques[0].Qtd_Compra_Pedido,
                Produto.Estoques[0].Qtd_Estoque_Fisico,
                Produto.Estoques[0].Qtd_Estoque_Min,
                Produto.Estoques[0].Qtd_Estoque_Pedido,

                //Quantidade Efetiva (estoque Fisico - Pedido)
                (Produto.Estoques[0].Qtd_Estoque_Fisico - Produto.Estoques[0].Qtd_Estoque_Pedido),

                Produto.Codigo,
                CriaListaCodigoBarra(item),
                Produto.Nome,
                Produto.DescReduzida,
                item.Unidade,
                Produto.GrupoEstoque.Nome,
                Produto.BarraFornecedor,
                Produto.NCM,
                Produto.Grupo1 == null ? "" : Produto.Grupo1.Nome,
                Produto.Grupo2 == null ? "" : Produto.Grupo2.Nome,
                Produto.Grupo3 == null ? "" : Produto.Grupo3.Nome,
                Produto.PrecoBase,
                Convert.ToInt32(item.Quantidade)
                
            );
        }

        private string CriaListaCodigoBarra(NotaItem notaItem)
        {
            string GrupoEstoque, CodigoProduto, Identificador, BarraFormatada, auxCodProd;
            var _aux = ProdutoController.Instancia.LoadObjectById(notaItem.Produto.ID);

            if (notaItem.Produto.Codigo.Contains('-'))
            {
                string[] codProdDelimiter = notaItem.Produto.Codigo.Split('-');
                auxCodProd = codProdDelimiter[0] + "-" + codProdDelimiter[1];
                CodigoProduto = auxCodProd;

            }
            else
            {
                CodigoProduto = _aux.Codigo;
            }

            Identificador = String.Format("{0:00000}",  Convert.ToInt64(logicaTelaNota.Identificador.Codigo));
            GrupoEstoque = String.Format("{0:000}", _aux.GrupoEstoque.Codigo);
            BarraFormatada = StringUtil.GetNoMaximoXCaracteres(Identificador + GrupoEstoque + CodigoProduto, 20);

            return BarraFormatada;
        }

        
    }
}
