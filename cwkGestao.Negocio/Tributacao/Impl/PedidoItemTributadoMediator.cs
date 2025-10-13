using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Tributacao.Impl
{
    public class PedidoItemTributadoMediator : ITributavel
    {
        private Modelo.PedidoItem pedidoItem;
        private Modelo.Pedido pedido;
        private Modelo.IcmsBase icms;
        private string TAG_CST = null;

        public PedidoItemTributadoMediator(Modelo.PedidoItem _pedidoItem, Modelo.Pedido _pedido)
        {
            pedidoItem = _pedidoItem;
            pedido = _pedido;
            InicializaICMS();
            InicializaTAG_CST();

        }

        private void InicializaTAG_CST()
        {
            if (pedidoItem.Produto != null)
            {
                bool dentroDoEstado;
                try
                {
                    dentroDoEstado = pedido.Filial.Cidade.UF.Sigla == pedido.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
                }
                catch (Exception ex)
                {

                    throw new Exception("Erro no cadastro do endereço do cliente: " + pedido.Pessoa.Nome + ". Por favor verifique! Erro: " + ex.Message);
                }
                bool adesaoParcial = ConfiguracaoController.Instancia.GetTipoNfe() == AdesaoNFe.Parcial;


                ConversorCsts.ModeloNota modeloNota = new ConversorCsts.ModeloNota();

                try
                {
                    modeloNota = (pedido.TipoPedido.TipoNota.ModeloDocto != TipoNota.ModeloDocumento.NFe && ((adesaoParcial && dentroDoEstado) || //venda dentro do estado
                                      pedido.Filial.SimplesNacional != 0)) //empresa normal 
                                      ? ConversorCsts.ModeloNota.Modelo1
                                      : ConversorCsts.ModeloNota.NFe;
                }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                {
                    if (pedido.TipoPedido.TipoNota == null)
                    {
                        throw new Exception("Tipo nota não cadastrado dentro do tipo do orçamento.");
                    }
                }

                ConversorCsts.STEmpresa stEmpresa = pedido.Filial.SimplesNacional == 0 ? ConversorCsts.STEmpresa.RegimeSimples : ConversorCsts.STEmpresa.RegimeNormal;

                TAG_CST = ConversorCsts.IndiceParaTributacao(icms.Tributacao, icms.CSOSN, modeloNota, stEmpresa, false);
            }
        }

        private void InicializaICMS()
        {
            //parametro vazio para que eu possa realizar alterações na rotina de nota.
            NotaItem notaitem = new NotaItem();

            if (pedidoItem.Produto != null)
            {
                icms = NotaItemController.Instancia.RecuperaIcmsOriginal((InOutType)pedido.Ent_Sai, pedido.Filial.Cidade.UF.Sigla, pedido.PessoaUF, pedido.Pessoa.GetCliente?.BContribuinte ?? false, pedidoItem.Produto, notaitem);
                if (icms == null)
                {
                    var produto = pedidoItem.Produto;
                    icms = new IcmsBase
                    {
                        AliqContrib = produto.AliqContrib,
                        AliqContribNormal = produto.AliqContribNormal,
                        TextoLeiContrib = produto.TextoLeiContrib,
                        AliqNContrib = produto.AliqNContrib,
                        AliqNContribNormal = produto.AliqNContribNormal,
                        TextoLeiNContrib = produto.TextoLeiNContrib,
                        ReducaoContrib = produto.ReducaoContrib,
                        PRedBCST = produto.PRedBCST,
                        LucroST = produto.LucroST,
                        AliqSubstTributaria = produto.AliqSubstTributaria,
                        Tributacao = produto.Tributacao,
                        CSOSN = produto.CSOSN
                    };
                }
            }
        }

        public decimal BaseCalculoGeral
        {
            get
            {
                return pedidoItem.Total - pedidoItem.ValorDesconto - pedidoItem.RAT_Desconto + pedidoItem.RAT_Acrescimo + pedidoItem.RAT_Frete;
            }
            set { }
        }

        public string PisCST
        {
            get { throw new NotImplementedException(); }
        }

        public decimal PisBaseCalculo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal PisAliquota
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal PisValor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string IpiCST
        {
            get { return string.Empty; }
        }

        public decimal IpiBaseCalculo
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }

        public decimal IpiAliquota
        {
            get
            {
                return pedidoItem.Produto.AliquotaIPI;
            }
            set
            {

            }
        }

        public decimal IpiValor
        {
            get
            {
                NCM ncm = NCMController.Instancia.LoadObjectById(pedidoItem.Produto.ID_NCM);
                if (ncm == null)
                {
                    return ((pedidoItem.Valor + pedidoItem.RAT_Acrescimo + pedidoItem.RAT_Frete) * pedidoItem.Quantidade) * (pedidoItem.Produto.AliquotaIPI / 100);
                }
                else
                {
                    if (pedidoItem.Produto.AliquotaIPI > 0)
                    {
                        return ((pedidoItem.Valor + pedidoItem.RAT_Acrescimo + pedidoItem.RAT_Frete) * pedidoItem.Quantidade) * (pedidoItem.Produto.AliquotaIPI / 100);
                    }
                    else
                    {
                        return ((pedidoItem.Valor + pedidoItem.RAT_Acrescimo + pedidoItem.RAT_Frete) * pedidoItem.Quantidade); //* (ncm.Ipi / 100);
                    }
                }
            }
            set
            {

            }
        }

        public string CofinsCST
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CofinsBaseCalculo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CofinsAliquota
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CofinsValor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal FCPBaseClaculo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public decimal FCPAliquota
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public decimal FCPValor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal FCPAliquotaST { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal FCPBaseClaculoST { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal FCPValorST { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string IcmsCST
        {
            get
            {
                return TAG_CST;
            }
            set
            {

            }
        }

        private string icmsTextoLei;
        public string IcmsTextoLei
        {
            get
            {
                return icmsTextoLei;
            }
            set
            {
                icmsTextoLei = value;
            }
        }

        private decimal icmsBaseCalculo;
        public decimal IcmsBaseCalculo
        {
            get
            {
                return icmsBaseCalculo;
            }
            set
            {
                icmsBaseCalculo = value;
            }
        }

        public decimal IcmsReducaoBaseCalculo
        {
            get
            {
                return icms.ReducaoContrib;
            }
            set
            {

            }
        }

        public decimal IcmsAliquota
        {
            get
            {
                if (pedido.Pessoa.GetCliente?.BContribuinte ?? false)
                    return icms.AliqContrib;
                else
                    return icms.AliqNContrib;
            }
            set { }
        }

        public decimal IcmsAliquotaNormal
        {
            get
            {
                if (pedido.Pessoa.GetCliente?.BContribuinte ?? false)
                    return icms.AliqContribNormal;
                else
                    return icms.AliqNContribNormal;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private decimal icmsValorDiferencaComReducao;
        public decimal IcmsValorDiferencaComReducao
        {
            get
            {
                return icmsValorDiferencaComReducao;
            }
            set
            {
                icmsValorDiferencaComReducao = value;
            }
        }

        private decimal icmsValor;
        public decimal IcmsValor
        {
            get
            {
                return icmsValor;
            }
            set
            {
                icmsValor = value;
            }
        }

        private decimal icmsValorIsento;
        public decimal IcmsValorIsento
        {
            get
            {
                return icmsValorIsento;
            }
            set
            {
                icmsValorIsento = value;
            }
        }

        public decimal IcmsValorRetido
        {
            get
            {
                return pedidoItem.ValorRetidoICMS;
            }
            set
            {
                pedidoItem.ValorRetidoICMS = value;
            }
        }

        private decimal icmsValorOutros;
        public decimal IcmsValorOutros
        {
            get
            {
                return icmsValorOutros;
            }
            set
            {
                icmsValorOutros = value;
            }
        }

        public decimal IcmsSTBaseCalculo
        {
            get
            {
                return pedidoItem.BaseICMSSubst;
            }
            set
            {
                pedidoItem.BaseICMSSubst = value;
            }
        }

        public decimal IcmsSTReducaoBaseCalculo
        {
            get
            {
                return icms.PRedBCST;
            }
            set { }
        }

        public decimal IcmsSTAliquota
        {
            get
            {
                return icms.AliqSubstTributaria;
            }
            set { }
        }

        public decimal IcmsSTMargemValorAgregado
        {
            get
            {
                return icms.LucroST;
            }
            set { }
        }

        public decimal CreditoReducaoBasePercentual
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CreditoPercentualReduzido
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CreditoValor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CreditoPercentual
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal EmpresaAliquota
        {
            get
            {
                return pedido.Filial.AliqSimplesSubst;
            }
            set
            {
                pedido.Filial.AliqSimplesSubst = value;
            }
        }

        public decimal PrecoProduto
        {
            get { return ProdutoController.Instancia.getPreco(pedidoItem.Produto, pedido.TabelaPreco, TipoPrecoType.Normal); }
        }

        public cwkGestao.Negocio.Enums.EmpresaST EmpresaST
        {
            get
            {
                switch (pedido.Filial.SimplesNacional)
                {
                    case 0: return cwkGestao.Negocio.Enums.EmpresaST.Simples;
                    case 1: return cwkGestao.Negocio.Enums.EmpresaST.SimplesComExcesso;
                    case 2: return cwkGestao.Negocio.Enums.EmpresaST.Normal;
                    default: return cwkGestao.Negocio.Enums.EmpresaST.Simples;
                }
            }
        }

        public bool EhClienteOrgaoPublico
        {
            get { return pedido.Pessoa.BOrgaoPublico; }
        }

        public bool EhCfopTributavelEmIcms
        {
            get
            {
                if (pedidoItem.Produto.CFOP != null)
                    return pedidoItem.Produto.CFOP.BCalcICMS;

                TabelaCFOP tabelaCFOP;
                if (pedido.Filial.Cidade.UF.Sigla != pedido.PessoaUF)
                {
                    if (pedido.Pessoa.BContribuinte == true)
                        tabelaCFOP = pedido.TipoPedido.TipoNota.Operacao.CFOP_FEstado_C;
                    else
                        tabelaCFOP = pedido.TipoPedido.TipoNota.Operacao.CFOP_FEstado_NC;
                }
                else
                    tabelaCFOP = pedido.TipoPedido.TipoNota.Operacao.CFOP_DEstado;

                return tabelaCFOP.BCalcICMS;
            }
        }

        public bool EhSubstitutoTributario
        {
            get
            {
                return pedido.Filial.TipoST == (int)Modelo.TipoST.Substituto;
            }
            set
            { }
        }

        public bool EhICMSProprio
        {
            get
            {
                if (EmpresaST != cwkGestao.Negocio.Enums.EmpresaST.Normal)
                {
                    return ConversorCsts.CSOSNsGeramST.Contains(TAG_CST);
                }
                return false;
            }
        }

        public decimal ReducaoImposto
        {
            get
            {
                if (pedido.Pessoa.GetCliente?.BContribuinte ?? false)
                    return icms.ReducaoContrib;
                else
                    return icms.ReducaoNContrib;
            }
            set
            {
            }
        }

        public Filial Filial
        {
            get
            {
                return pedido.Filial;
            }
        }

        public Pessoa Pessoa
        {
            get
            {
                return pedido.Pessoa;
            }
        }

        public decimal AliquotaDiferimento { get { return 0; } set { } }
        public decimal ValorIcmsDiferimento { get { return 0; } set { } }
        public decimal ValorIcmsOp { get { return 0; } set { } }
        public decimal ValorICMSDesoneracao { get => 0; set { } }
    }
}
