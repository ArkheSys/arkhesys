using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Itens do Pedido")]
    public class PedidoItem : ModeloBase
    {
        public PedidoItem()
        {
            Gerente = "";
            ProdutoDescReduzida = "";
            Estoques = new List<PedidoItemEstoque>();
        }

        public override int ID { get; set; }

        public virtual Pedido Pedido { get; set; }

        public virtual int Sequencia { get; set; }

        public virtual Produto Produto { get; set; }



        public virtual string CodigoProduto { get { if (Produto != null) return Produto.Codigo; else return ""; } set { } }
        [InformacaoExibicao(1, "Produto", 90)]
        public virtual string ProdutoNome { get; set; }
        [InformacaoExibicao(2, "Descrição", 90)]
        public virtual string ProdutoDescReduzida { get; set; }
        [InformacaoExibicao(5, "Qtd", 90)]
        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(6, "Und", 90)]
        public virtual string Unidade { get; set; }

        public virtual decimal ValorCalculado { get; set; }
        [InformacaoExibicao(7, "Preço", 90)]
        public virtual decimal Valor { get; set; }

        // Valor base do produto, que veio da tabela de preço ou da importacao, esse valor não deve ser alterado.
        public virtual decimal ValorProduto { get; set; }

        public virtual decimal SubTotal { get; set; }
        [InformacaoExibicao(8, "Desc", 90)]
        public virtual decimal PercDesconto { get; set; }

        public virtual decimal ValorDesconto { get; set; }

        public virtual decimal RAT_Desconto { get; set; }

        public virtual decimal RAT_Acrescimo { get; set; }

        public virtual decimal RAT_Frete { get; set; }

        [InformacaoExibicao(9, "Total", 90)]
        public virtual decimal Total { get; set; }

        public virtual decimal PesoBruto { get; set; }

        public virtual decimal PesoLiquido { get; set; }

        public virtual DateTime Dt { get; set; }

        public virtual bool bReservaEstoque { get; set; }

        public virtual int Ent_Sai { get; set; }

        public virtual DateTime? PrazoEntrega { get; set; }

        public virtual string Gerente { get; set; }

        public virtual decimal BaseICMSSubst { get; set; }

        public virtual decimal ValorRetidoICMS { get; set; }

        public virtual decimal QtdCancelada { get; set; }

        public virtual decimal QtdRetirada { get; set; }

        public virtual int OS { get; set; }

        public virtual PedidoItem OrcamentoItem { get; set; }

        public virtual string InformacoesAdicionais { get; set; }

        public virtual IList<NotaItem> NotaItems { get; set; }

        //public virtual IList<PedidoItem> PedidoItems { get; set; }
        public virtual IList<PedidoItemEstoque> Estoques { get; set; }
        //public virtual IList<NotaItemPedidoItem> PedidoItens { get; set; }
        public virtual IList<Imei> ImeisSelecionado { get; set; }
        public virtual string NomeMagento { get; set; }

        private string _Grupo1Nome;
        public virtual string Grupo1Nome { get { return Produto.Grupo1 == null ? "" : Produto.Grupo1.Nome; } set { _Grupo1Nome = value; } }
        public virtual string Grupo2Nome { get { return Produto.Grupo2 == null ? "" : Produto.Grupo2.Nome; } set { _Grupo1Nome = value; } }
        public virtual string Grupo3Nome { get { return Produto.Grupo3 == null ? "" : Produto.Grupo3.Nome; } set { _Grupo1Nome = value; } }

        [InformacaoExibicao(0, "x", 10)]
        public virtual bool Selecionado { get; set; }

        public virtual PedidoItem Clone()
        {
            PedidoItem clone = new PedidoItem();

            foreach (PropertyInfo item in clone.GetType().GetProperties())
            {
                if (!item.PropertyType.ContainsGenericParameters)
                    item.SetValue(clone, item.GetValue(this, null), null);
            }

            return clone;
        }

        public override ModeloBase Clonar()
        {
            PedidoItem objPedidoItem = new PedidoItem();

            AutoMapper.Mapper.CreateMap<PedidoItem, PedidoItem>()
                .ForMember(dest => dest.ID, o => o.Ignore())
                .ForMember(dest => dest.NotaItems, o => o.Ignore())
                //.ForMember(dest => dest.PedidoItens, o => o.Ignore())
                .ForMember(dest => dest.Estoques, o => o.Ignore())
                .ForMember(dest => dest.ImeisSelecionado, o => o.Ignore());
            objPedidoItem = AutoMapper.Mapper.Map<PedidoItem>(this);

            return objPedidoItem;
        }

        public virtual string AplicacaoCompra { get; set; }

        public virtual decimal IPICompra { get; set; }

        public virtual decimal ICMSCompra { get; set; }

        public virtual decimal AliquotaIPI { get; set; }

        public virtual decimal ValorIPI { get; set; }

        public virtual decimal ValorICMS { get; set; }
        public virtual decimal ValorICMSST { get; set; }
        public virtual decimal TotalIPI { get; set; }

        public virtual decimal PercentualIPI { get; set; }
        public virtual decimal PercentualICMS { get; set; }

        public virtual bool BFaturado { get; set; }

        [InformacaoExibicao(4, "Prazo Entrega", 90)]
        public virtual DateTime? DataEntrega { get; set; }

        public virtual int? IDNcm { get; set; }

        [InformacaoExibicao(3, "NCM", 90)]
        public virtual string NCM { get; set; }

        public virtual string Observacao { get; set; }


        #region Calculos
        public virtual void CalculaIPI(Configuracao config, bool revenda)
        {
            try
            {

                if (config.NaoSomarIpiTotalNota == 1 && revenda == false)
                {
                    decimal vProduto = ValorCalculado * Quantidade;
                    decimal desconto = Math.Round((vProduto * (PercDesconto / 100)), 2);
                    vProduto -= desconto;

                    //if (considerarFreteIPI == true)
                    //    vProduto += RAT_Frete;

                    decimal vIPI = 0;
                    if (AliquotaIPI > 0)
                    {
                        vProduto = (vProduto * 100) / (AliquotaIPI + 100);
                        vIPI = vProduto * (AliquotaIPI / 100);
                        Valor = Math.Round(ValorCalculado - desconto - (vIPI / Quantidade), 2);
                    }
                    else
                        Valor = Math.Round(ValorCalculado - desconto, 2);
                    ValorIPI = Math.Round(vIPI, 2);
                    SubTotal = (Quantidade * Valor);
                    Total = SubTotal;
                }
                else
                {
                    ValorIPI = Math.Round(((Math.Round((Valor - ValorDesconto) * Quantidade, 2) + RAT_Frete) * (AliquotaIPI / 100)), 2);
                }
            }
            catch (Exception e)
            {

                throw new Exception("Erro ao calcular o ipi do item do pedido! Detalhe: " + e.Message);
            }
        }
        #endregion
    }
}
