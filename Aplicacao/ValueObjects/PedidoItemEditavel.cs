﻿using System;
using cwkGestao.Modelo;
using Aplicacao.ValueObjects.Interfaces;
using cwkGestao.Negocio.Tributacao;
using cwkGestao.Negocio.Tributacao.Impl;
using System.Linq;

namespace Aplicacao.ValueObjects
{
    public class PedidoItemEditavel : DonoProduto
    {
        private Tributacao tributacao;
        public PedidoItem PedidoItem { get; private set; }
        public static Action AtualizaTotais;

        public PedidoItemEditavel(PedidoItem pedidoItem)
        {
            this.PedidoItem = pedidoItem;
        }

        public Produto Produto
        {
            get { return PedidoItem.Produto; }
            set
            {
                PedidoItem.Produto = value;
                if (PedidoItem.Pedido.Filial != null && PedidoItem.Pedido.TipoPedido != null)
                {
                    if (PedidoItem.Pedido.TipoPedido.CalcularST)
                        tributacao = new Tributacao(new PedidoItemTributadoMediator(PedidoItem, PedidoItem.Pedido));
                }
            }
        }

        public TabelaPreco TabelaPreco
        {
            get { return PedidoItem.Pedido.TabelaPreco; }
            set { PedidoItem.Pedido.TabelaPreco = value; }
        }

        public int Sequencia
        {
            get { return PedidoItem.Sequencia; }
            set { PedidoItem.Sequencia = value; }
        }

        public string ProdutoNome
        {
            get { return PedidoItem.ProdutoNome; }
            set { PedidoItem.ProdutoNome = value; }
        }

        public decimal Quantidade
        {
            get { return PedidoItem.Quantidade; }
            set { PedidoItem.Quantidade = value; }
        }

        public string Unidade
        {
            get { return PedidoItem.Unidade; }
            set { PedidoItem.Unidade = value; }
        }

        public decimal ValorOriginal
        {
            get { return PedidoItem.ValorCalculado; }
            set { PedidoItem.ValorCalculado = value; }
        }

        public decimal Valor
        {
            get { return PedidoItem.Valor; }
            set { PedidoItem.Valor = value; }
        }

        public decimal DescontoPerc
        {
            get { return PedidoItem.PercDesconto; }
            set { PedidoItem.PercDesconto = value; }
        }

        public decimal Total
        {
            get { return PedidoItem.Total; }
            set
            {
                PedidoItem.Total = value;
                CalculaSubstituicaoTributaria();
            }
        }

        public void ReCalculaTotal()
        {
            PedidoItem.Pedido.CalculaTotalProdutos();
            if (AtualizaTotais != null)
                AtualizaTotais();
        }

        public void CalculaSubstituicaoTributaria()
        {
            Pedido pedido = PedidoItem.Pedido;
            if (tributacao != null)
            {
                tributacao.CalculaIcms();
            }
        }

        public decimal AliquotaIPI
        {
            get { return PedidoItem.AliquotaIPI; }
            set { PedidoItem.AliquotaIPI = value; }
        }

        public string ObservacaoItem {
            get { return PedidoItem.Observacao; }
            set { PedidoItem.Observacao = value;  }
        }

        public string QuantidadeEstoque
        {
            get
            {
                var estoques = PedidoItem.Produto?.Estoques;
                if (estoques?.Any() ?? false)
                    return (estoques.FirstOrDefault()?.Qtd_Estoque_Fisico ?? 0).ToString();

                return string.Empty;
            }
            set
            {
                QuantidadeEstoque = "";
            }
        }
        public string LocalEstoque
        {
            get
            {
                var estoques = PedidoItem.Produto?.Estoques;
                if (estoques?.Any() ?? false)
                    return estoques.FirstOrDefault()?.LocalEstoque?.Descricao;

                return string.Empty;
            }
            set
            {
                LocalEstoque = "";
            }
        }
        public string CodigoFabricante { get { return PedidoItem.Produto?.CodigoFabricante; } set { CodigoFabricante = ""; } }
        public string CodigoOriginal { get { return PedidoItem.Produto?.CodigoOriginal; } set { CodigoOriginal = ""; } }
        public string Marca { get { return PedidoItem.Produto?.Grupo1?.Nome; } set { Marca = ""; } }
        public string Aplicacao { get { return PedidoItem.Produto?.Aplicacao; } set { Aplicacao = ""; } }

        //public DateTime? DataEntrega
        //{
        //    get { return PedidoItem.DataEntrega; }
        //    set { PedidoItem.DataEntrega = value; }
        //}

        //public string NCM
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(PedidoItem.NCM))
        //        {
        //            PedidoItem.IDNcm = PedidoItem.Produto?.ID_NCM;
        //            PedidoItem.NCM = PedidoItem.Produto?.NCM;
        //        }
        //        return PedidoItem.NCM;
        //    }
        //    set { PedidoItem.NCM = value; }
        //}
    }
}