using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Tributacao.Impl
{
    public class NotaItemTributadaMediator : ITributavel
    {
        private Modelo.NotaItem notaItem;
        private Modelo.Nota nota;
        private Modelo.IcmsBase icms;
#pragma warning disable CS0414 // O campo "NotaItemTributadaMediator.TAG_CST" é atribuído, mas seu valor nunca é usado
        private string TAG_CST = null;
#pragma warning restore CS0414 // O campo "NotaItemTributadaMediator.TAG_CST" é atribuído, mas seu valor nunca é usado
        cwkGestao.Modelo.TabelaFaixaFatSimples tabelaFaixa;

        public NotaItemTributadaMediator(Modelo.NotaItem _notaItem, Modelo.Nota _nota)
        {
            notaItem = _notaItem;
            nota = _nota;
            InicializaICMS();
        }

        private void InicializaICMS()
        {
            //parametro vazio para que eu possa realizar alterações na rotina de nota.
            if (notaItem.Produto != null)
            {
                if ((InOutType)nota.Ent_Sai == InOutType.Entrada)
                {
                    icms = null;              
                }
                else
                {
                    var BContribuinte = nota.Pessoa.BCliente && nota.Pessoa.BContribuinte;
                    icms = NotaItemController.Instancia.RecuperaIcmsOriginal((InOutType)nota.Ent_Sai, nota.Filial.Cidade.UF.Sigla, nota.PessoaUF, nota.Pessoa.GetCliente == null ? BContribuinte : nota.Pessoa.GetCliente.BContribuinte, notaItem.Produto, notaItem);
                }                
                if (icms == null)
                {
                    var produto = notaItem.Produto;
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
                        CSOSN = produto.CSOSN,
                    };
                }
            }
        }

        public decimal BaseCalculoGeral
        {
            get
            {
                return notaItem.Total + notaItem.RateioFrete + notaItem.RateioOutrasDespesas +
                    notaItem.RateioSeguro + notaItem.RAT_Acrescimo - notaItem.RAT_Desconto;
            }
            set
            {
                
            }
        }

        public string PisCST
        {
            get { return notaItem.CST_Pis; }
        }

        public decimal PisBaseCalculo
        {
            get
            {
                return notaItem.VBC_Q07;
            }
            set
            {
                notaItem.VBC_Q07 = value;
            }
        }

        public decimal PisAliquota
        {
            get
            {
                return notaItem.PPIS_Q08;
            }
            set
            {
                notaItem.PPIS_Q08 = value;
            }
        }

        public decimal PisValor
        {
            get
            {
                return notaItem.VPIS_Q09;
            }
            set
            {
                notaItem.VPIS_Q09 = value;
            }
        }

        public string IpiCST
        {
            get { return notaItem.CST_Ipi; }
        }

        public decimal IpiBaseCalculo
        {
            get
            {
                return notaItem.VBC_O10;
            }
            set
            {
                notaItem.VBC_O10 = value;
            }
        }

        public decimal IpiAliquota
        {
            get
            {
                return notaItem.PIPI_O13;
            }
            set
            {
                notaItem.PIPI_O13 = value;
            }
        }

        public decimal IpiValor
        {
            get
            {
                return notaItem.VIPI_O14;
            }
            set
            {
                notaItem.VIPI_O14 = value;
            }
        }

        public string CofinsCST
        {
            get
            {
                return notaItem.CST_Cofins;
            }
            set
            {
                notaItem.CST_Cofins = value;
            }
        }

        public decimal CofinsBaseCalculo
        {
            get
            {
                return notaItem.VBC_S07;
            }
            set
            {
                notaItem.VBC_S07 = value;
            }
        }

        public decimal CofinsAliquota
        {
            get
            {
                return notaItem.PCOFINS_S08;
            }
            set
            {
                notaItem.PCOFINS_S08 = value;
            }
        }

        public decimal CofinsValor
        {
            get
            {
                return notaItem.VCOFINS_S11;
            }
            set
            {
                notaItem.VCOFINS_S11 = value;
            }
        }

        public string IcmsCST
        {
            get
            {
                return notaItem.TAG_CST;
            }
            set
            {
                notaItem.TAG_CST = value;
            }
        }

        public string IcmsTextoLei
        {
            get
            {
                return notaItem.TextoLei;
            }
            set
            {
                notaItem.TextoLei = value;
            }
        }

        public decimal IcmsBaseCalculo
        {
            get
            {
                return notaItem.BaseICMS;
            }
            set
            {
                notaItem.BaseICMS = value;
             }
        }

        public decimal IcmsReducaoBaseCalculo
        {
            get
            {
                return notaItem.PRedBC_N14;
            }
            set
            {
                notaItem.PRedBC_N14 = value;
            }
        }

        public decimal IcmsAliquota
        {
            get
            {
                return notaItem.AliqICMS;
            }
            set
            {
                notaItem.AliqICMS = value;
            }
        }

        public decimal IcmsAliquotaNormal
        {
            get
            {
                return notaItem.AliqICMSNormal;
            }
            set
            {
                notaItem.AliqICMSNormal = value;
            }
        }

        public decimal ReducaoImposto 
        {
             get
             {
                 return notaItem.ReducaoImposto;
             }
            set 
            { 
                notaItem.ReducaoImposto = value;
            }
        }
        public decimal IcmsValorDiferencaComReducao
        {
            get
            {
                return notaItem.ValorICMSReducaoDif;
            }
            set
            {
                notaItem.ValorICMSReducaoDif = value;
            }
        }

        public decimal IcmsValor
        {
            get
            {
                return notaItem.ValorICMS;
            }
            set
            {
                notaItem.ValorICMS = value;
            }
        }

        public decimal IcmsValorIsento
        {
            get
            {
                return notaItem.ValorIsentoICMS;
            }
            set
            {
                notaItem.ValorIsentoICMS = value;
            }
        }

        public decimal IcmsValorRetido
        {
            get
            {
                return notaItem.ValorRetidoICMS;
            }
            set
            {
                notaItem.ValorRetidoICMS = value;
            }
        }

        public decimal IcmsValorOutros
        {
            get
            {
                return notaItem.ValorOutroICMS;
            }
            set
            {
                notaItem.ValorOutroICMS = value;
            }
        }

        public decimal IcmsSTBaseCalculo
        {
            get
            {
                return notaItem.BaseICMSSubst;
            }
            set
            {
                notaItem.BaseICMSSubst = value;
            }
        }

        public decimal IcmsSTReducaoBaseCalculo
        {
            get
            {
                return notaItem.PRedBCST_N20;
            }
            set
            {
                notaItem.PRedBCST_N20 = value;
            }
        }

        public decimal IcmsSTAliquota
        {
            get
            {
                return notaItem.PICMSST_N22;
            }
            set
            {
                notaItem.PICMSST_N22 = value;
            }
        }

        public decimal IcmsSTMargemValorAgregado
        {
            get
            {
                    return notaItem.PMVAST_N19;
            }
            set
            {
                notaItem.PMVAST_N19 = value;
            }
        }

        public decimal FCPBaseClaculo
        {
            get
            {
                return notaItem.vBCFCP_N17a;
            }
            set
            {
                notaItem.vBCFCP_N17a = value;
            }
        }
        public decimal FCPAliquota
        {
            get
            {
                return notaItem.pFCP_N17b;
            }
            set
            {
                notaItem.pFCP_N17b = value;
            }
        }
        public decimal FCPValor
        {
            get
            {
                return notaItem.vFCP_N17c;
            }
            set
            {
                notaItem.vFCP_N17c = value;
            }
        }

        public decimal FCPAliquotaST
        {
            get => notaItem.pFCPST_N23b;
            set => notaItem.pFCPST_N23b = value;
        }

        public decimal FCPBaseClaculoST
        {
            get => notaItem.vBCFCPST_N23a;
            set => notaItem.vBCFCPST_N23a = value;
        }

        public decimal FCPValorST
        {
            get => notaItem.vFCPST_N23d;
            set => notaItem.vFCPST_N23d = value;
        }

        public decimal AliquotaDiferimento 
        {
            get => notaItem.AliquotaDiferimento;
            set => notaItem.AliquotaDiferimento = value;
        }
        public decimal ValorIcmsDiferimento 
        {
            get => notaItem.ValorIcmsDiferimento;
            set => notaItem.ValorIcmsDiferimento = value;
        }
        public decimal ValorIcmsOp 
        {
            get => notaItem.ValorIcmsOp;
            set => notaItem.ValorIcmsOp = value;
        }

        public decimal ValorICMSDesoneracao 
        {
            get => notaItem.ValorICMSDesoneracao; 
            set => notaItem.ValorICMSDesoneracao = value;
        }

        public decimal CreditoReducaoBasePercentual
        {
            get
            {
                try
                {
                    if (tabelaFaixa == null)
                    {
                        tabelaFaixa = TabelaFaixaFatSimplesController.Instancia.LoadObjectById(nota.Filial.TabelaFaixaFatSimples.ID);
                    }
                    return tabelaFaixa.PercReducaoBC;
                }
                catch
                {
                    return 200;
                }
            }
            set
            { }
        }

        public decimal CreditoPercentualReduzido
        {
            get
            {
                return notaItem.PCredSN_N29;
            }
            set
            {
                notaItem.PCredSN_N29 = value;
            }
        }

        public decimal CreditoValor
        {
            get
            {
                return notaItem.VCredICMSSN_N30;
            }
            set
            {
                notaItem.VCredICMSSN_N30 = value;
            }
        }

        public decimal CreditoPercentual
        {
            get
            {
                try
                {
                    if (tabelaFaixa == null)
                    {
                        tabelaFaixa = TabelaFaixaFatSimplesController.Instancia.LoadObjectById(nota.Filial.TabelaFaixaFatSimples.ID);
                    }
                    return tabelaFaixa.ICMS;
                }
                catch
                {
                    return 66.6m;
                }
            }
            set
            { }
        }

        public decimal EmpresaAliquota
        {
            get
            {
                return nota.Filial.AliqSimplesSubst;
            }
            set
            {
                nota.Filial.AliqSimplesSubst = value;
            }
        }

        public cwkGestao.Negocio.Enums.EmpresaST EmpresaST
        {
            get
            {
                switch (nota.Filial.SimplesNacional)
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
            get { return Convert.ToBoolean(nota.Pessoa.BOrgaoPublico); }
        }

        public bool EhCfopTributavelEmIcms
        {
            get
            {
                try { return notaItem.CFOP.BCalcICMS; }
                catch { throw new Exception("CFOP não atribuído."); }
            }
        }

        public bool EhSubstitutoTributario
        {
            get
            {
                return nota.Filial.TipoST == (int)Modelo.TipoST.Substituto;
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
                    return ConversorCsts.CSOSNsGeramST.Contains(notaItem.TAG_CST);
                }
                return false;
            }
        }

        public decimal PrecoProduto
        {
            get
            {
                var precoProduto = ProdutoController.Instancia.getPreco(notaItem.Produto, notaItem.Nota.TabelaPreco, TipoPrecoType.Normal);
                if (notaItem.Valor != precoProduto)
                    return notaItem.Valor;
                else
                    return precoProduto;
            }

        }

        public Filial Filial
        {
            get
            {
                return FilialController.Instancia.LoadObjectById(nota.Filial.ID);
            }
        }

        public Pessoa Pessoa
        {
            get
            {
                return PessoaController.Instancia.LoadObjectById(nota.Pessoa.ID);
            }
        }
    }
}
