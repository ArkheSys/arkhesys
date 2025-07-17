using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEstoqueProduto : pxyEstoque
    {
        public pxyEstoqueProduto(string localestoque, decimal estoquefisico, decimal qtdpedido, decimal estoquemin, int idestoque
            , string produto, string unidade, string filial, string classificacaoGrupoEstoque, string nomeGrupoEstoque, int idGrupoEstoque, decimal custo, string codigoProduto, string fornecedor)
            : base(localestoque, estoquefisico, qtdpedido, estoquemin, idestoque)
        {
            Produto = produto;
            Unidade = unidade;
            Filial = filial;
            ClassificacaoGrupoEstoque = classificacaoGrupoEstoque;
            NomeGrupoEstoque = nomeGrupoEstoque;
            IDGrupoEstoque = idGrupoEstoque;
            Custo = custo;
            CodigoProduto = codigoProduto;
            Fornecedor = fornecedor;
        }

#pragma warning disable CS0108 // "pxyEstoqueProduto.Produto" oculta o membro herdado "pxyEstoque.Produto". Use a nova palavra-chave se foi pretendido ocultar.
        public string Produto { get; set; }
#pragma warning restore CS0108 // "pxyEstoqueProduto.Produto" oculta o membro herdado "pxyEstoque.Produto". Use a nova palavra-chave se foi pretendido ocultar.
        public string Unidade { get; set; }
        public string Filial { get; set; }
        public string ClassificacaoGrupoEstoque { get; set; }
        public int IDGrupoEstoque { get; set; }
        public string NomeGrupoEstoque { get; set; }
        public decimal Custo { get; set; }

        public string CodigoProduto { get; set; }
        public string Fornecedor { get; set; }
    }
}
