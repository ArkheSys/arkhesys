using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Util;
using cwkNotaFiscalEletronica.Interfaces;
using cwkGestao.Modelo;
using cwkGestao.Negocio;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHNotaItem : AdapterNotaItem
    {
        Modelo.NotaItem notaItem;
        private IList<IAdicaoNotaItem> adicoesNotaItem;

        public NHNotaItem(Modelo.NotaItem _notaItem)
        {
            notaItem = _notaItem;

            adicoesNotaItem = new List<IAdicaoNotaItem>();
            foreach (var item in _notaItem.AdicoesNotaItem)
            {
                adicoesNotaItem.Add(new NHAdicaoNotaItem(item));
            }
        }

        public override string AliqCupom
        {
            get { return notaItem.AliqCupom; }
        }

        public override decimal AliqICMS
        {
            get { return notaItem.AliqICMS; }
        }

        public override decimal AliqICMSNormal
        {
            get { return notaItem.AliqICMSNormal; }
        }

        public override decimal BaseICMS
        {
            get { return notaItem.BaseICMS; }
        }

        public override decimal BaseICMSSubst
        {
            get { return notaItem.BaseICMSSubst; }
        }

        public override string CST_Cofins
        {
            get { return notaItem.CST_Cofins; }
        }

        public override string CST_Ipi
        {
            get { return notaItem.CST_Ipi; }
        }

        public override string CST_Pis
        {
            get { return notaItem.CST_Pis; }
        }

        public override bool CalcCustoMedio
        {
            get { return notaItem.CalcCustoMedio; }
        }

        public override DateTime Dt
        {
            get { return notaItem.Dt; }
        }

        public override int Ent_Sai
        {
            get { return (int)notaItem.Ent_Sai; }
        }

        public override string InfAdicionais
        {
            get { return notaItem.InfAdicionais.RemoverEspacosDuplosEQuebras(); }
        }

        public override decimal PercDesconto
        {
            get { return notaItem.PercDesconto; }
        }

        public override decimal PesoBruto
        {
            get { return notaItem.PesoBruto; }
        }

        public override decimal PesoLiquido
        {
            get { return notaItem.PesoLiquido; }
        }

        public override string ProdutoCFOP
        {
            get { return notaItem.CFOP.CFOP; }
        }

        public override string ProdutoCodigo
        {
            get { return notaItem.Produto.Codigo; }
        }

        public override string ProdutoDescReduzida
        {
            get { return notaItem.ProdutoDescReduzida; }
        }

        public override string ProdutoNCM
        {
            get { return notaItem.Produto.NCM; }
        }



        public override string ProdutoNome
        {
            get { return notaItem.ProdutoNome; }
        }

        public override decimal Quantidade
        {
            get { return notaItem.Quantidade; }
        }

        public override decimal RAT_Acrescimo
        {
            get { return notaItem.RAT_Acrescimo; }
        }

        public override decimal RAT_Desconto
        {
            get { return notaItem.RAT_Desconto; }
        }

        public override int Sequencia
        {
            get { return notaItem.Sequencia; }
        }

        public override string SitTrib
        {
            get { return notaItem.SitTrib; }
        }

        public override decimal SubTotal
        {
            get { return notaItem.SubTotal; }
        }

        public override string TAG_CST
        {
            get { return notaItem.TAG_CST; }
        }

        public override string TextoLei
        {
            get { return notaItem.TextoLei; }
        }

        public override decimal Total
        {
            get { return notaItem.Total; }
        }

        public override string Unidade
        {
            get { return notaItem.Unidade; }
        }

        public override decimal Valor
        {
            get { return notaItem.Valor; }
        }

        public override decimal ValorCalculado
        {
            get { return notaItem.ValorCalculado; }
        }

        public override decimal ValorCustoMedia
        {
            get { return notaItem.ValorCustoMedio; }
        }

        public override decimal ValorDesconto
        {
            get { return notaItem.ValorDesconto; }
        }

        public override decimal ValorICMS
        {
            get { return notaItem.ValorICMS; }
        }

        public override decimal ValorICMSReducao
        {
            get { return notaItem.ValorICMSReducao; }
        }

        public override decimal ValorICMSReducaoDif
        {
            get { return notaItem.ValorICMSReducaoDif; }
        }

        public override decimal ValorIsentoICMS
        {
            get { return notaItem.ValorIsentoICMS; }
        }

        public override decimal ValorOutroICMS
        {
            get { return notaItem.ValorOutroICMS; }
        }

        public override decimal ValorRetidoICMS
        {
            get { return notaItem.ValorRetidoICMS; }
        }

        public override string cEnq_O06
        {
            get { return notaItem.cEnq_O06; }
        }

        public override int modBCST_N18
        {
            get { return notaItem.modBCST_N18; }
        }

        public override int modBC_N13
        {
            get { return notaItem.modBC_N13; }
        }

        public override int orig_N11
        {
            get { return notaItem.orig_N11; }
        }

        public override decimal pCOFINS_S08
        {
            get { return notaItem.PCOFINS_S08; }
        }

        public override decimal pICMSST_N22
        {
            get { return notaItem.PICMSST_N22; }
        }

        public override decimal pIPI_O13
        {
            get { return notaItem.PIPI_O13; }
        }

        public override decimal pPIS_Q08
        {
            get { return notaItem.PPIS_Q08; }
        }

        public override decimal pRedBC_N14
        {
            get { return notaItem.PRedBC_N14; }
        }

        public override decimal vBC_O10
        {
            get { return notaItem.VBC_O10; }
        }

        public override decimal vBC_Q07
        {
            get { return notaItem.VBC_Q07; }
        }

        public override decimal vBC_S07
        {
            get { return notaItem.VBC_S07; }
        }

        public override decimal vCOFINS_S11
        {
            get { return notaItem.VCOFINS_S11; }
        }

        public override decimal vIPI_O14
        {
            get { return notaItem.VIPI_O14; }
        }

        public override decimal vPIS_Q09
        {
            get { return notaItem.VPIS_Q09; }
        }

        public override string CFOPCodigo
        {
            get { return notaItem.CFOP.CFOP; }
        }

        public override string CFOPDescricao
        {
            get { return notaItem.CFOP.Nome; }
        }

        public override decimal pCredSN_N29
        {
            get { return notaItem.PCredSN_N29; }
        }

        public override decimal pMVAST_N19
        {
            get { return notaItem.PMVAST_N19; }
        }

        public override decimal pRedBCST_N20
        {
            get { return notaItem.PRedBCST_N20; }
        }

        public override decimal vCredICMSSN_N30
        {
            get { return notaItem.VCredICMSSN_N30; }
        }

        public override string cEAN
        {
            get
            {
                // CodigoBarrasRegistrado = true = SEM GTIN
                return !notaItem.Produto.CodigoBarrasRegistrado ? notaItem.Produto.Barra : "";
            }
        }

        public override string cEANTrib
        {
            get
            {
                return !notaItem.Produto.CodigoBarrasRegistrado ? notaItem.Produto.Barra : "";
            }
        }

        public override IList<cwkNotaFiscalEletronica.Interfaces.IAdicaoNotaItem> AdicoesNotaItem
        {
            get { return adicoesNotaItem; }
        }

        public override string I19_nDI
        {
            get { return notaItem.I19_nDI; }
        }

        public override DateTime? I20_dDI
        {
            get { return notaItem.I20_dDI; }
        }

        public override string I21_xLocDesemb
        {
            get { return notaItem.I21_xLocDesemb; }
        }

        public override string I22_UFDesemb
        {
            get { return notaItem.I22_UFDesemb; }
        }

        public override DateTime? I23_dDesemb
        {
            get { return notaItem.I23_dDesemb; }
        }

        public override string I24_cExportador
        {
            get { return notaItem.I24_cExportador; }
        }

        public override decimal P02_vBC
        {
            get { return notaItem.P02_vBC; }
        }

        public override decimal P03_vDespAdu
        {
            get { return notaItem.P03_vDespAdu; }
        }

        public override decimal P04_vII
        {
            get { return notaItem.P04_vII; }
        }

        public override decimal P05_vIOF
        {
            get { return notaItem.P05_vIOF; }
        }

        public override decimal? TotalImpostos
        {
            get { return notaItem.TotalImpostos; }
        }

        public override decimal OutrasDespesas
        {
            get { return notaItem.RateioOutrasDespesas; }
        }

        public override decimal ValorFrete
        {
            get { return notaItem.RateioFrete; }
        }

        public override decimal ValorSeguro
        {
            get { return notaItem.RateioSeguro; }
        }

        public override string FCI
        {
            get { return notaItem.ProdutoFCI == null ? null : notaItem.ProdutoFCI.FCI; }
        }

        public override string NumDrawBack
        {
            get { return notaItem.NumDrawBack; }
        }

        public override decimal vICMSDeson
        {
            get { return notaItem.ValorICMSDesoneracao; }
        }

        public override int motDesICMS
        {
            get { return (int)notaItem.MotivoICMSDesoneracao; }
        }

        public override int ViaTransp
        {
            get { return (int)notaItem.ViaTransp; }
        }

        public override decimal ValorFreteRenovacaoMarinhaMercante
        {
            get { return notaItem.ValorFreteRenovacaoMarinhaMercante; }
        }

        public override decimal AliqInterna
        {
            get { return notaItem.AliqInterna; }
        }

        public override string Cest
        {
            get { return notaItem.Cest; }
        }

        public override decimal vICMSUFDest_NA15 
        {
            get { return notaItem.vICMSUFDest_NA15; }
        }

        public override decimal vICMSUFRemet_NA17
        {
            get { return notaItem.vICMSUFRemet_NA17; }
        }

        public override decimal pICMSInter
        {
            get { return notaItem.pICMSInter; }
        }

        public override string cBenef_I05f
        {
            get { return notaItem.cBenef_I05f; }
        }
        public override int indEscala_I05d
        {
            get { return notaItem.indEscala_I05d; }
        }
        public override string CNPJFab_I05e
        {
            get { return notaItem.CNPJFab_I05e; }
        }
        public override decimal vBCFCP_N17a
        {
            get { return notaItem.vBCFCP_N17a; }
        }
        public override decimal pFCP_N17b
        {
            get { return notaItem.pFCP_N17b; }
        }
        public override decimal vFCP_N17c
        {
            get { return notaItem.vFCP_N17c; }
        }
        public override decimal vBCFCPST_N23a
        {
            get { return notaItem.vBCFCPST_N23a; }
        }
        public override decimal pFCPST_N23b
        {
            get { return notaItem.pFCPST_N23b; }
        }
        public override decimal vFCPST_N23d
        {
            get { return notaItem.vFCPST_N23d; }
        }

        public override decimal pNacionalFederal { get { return notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.NacionalFederal ?? 0; } }
        public override decimal pImportadoFederal { get { return notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.ImportadosFederal ?? 0; } }
        public override decimal pEstadual { get { return notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.Estadual ?? 0; } }
        public override decimal pMunicipal { get { return notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.Municipal ?? 0; } }

        public override decimal AliquotaDiferimento => notaItem.AliquotaDiferimento;
        public override decimal ValorIcmsDiferimento => notaItem.ValorIcmsDiferimento;
        public override decimal ValorIcmsOp => notaItem.ValorIcmsOp;

        public override string cProdANP => notaItem.Produto?.cProdANP;

        public override string descANP => notaItem.Produto?.descANP;

        public override string UFCons => notaItem.Produto?.UFCons;
    }
}
