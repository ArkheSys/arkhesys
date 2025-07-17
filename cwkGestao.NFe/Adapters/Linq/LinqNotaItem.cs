using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = Modelo;
using cwkNotaFiscalEletronica.Interfaces;
using cwkGestao.Negocio;

namespace cwkGestao.NFe.Adapters.Linq
{
    internal class LinqNotaItem : AdapterNotaItem
    {
        MO.NotaItem notaItem;
        private IList<IAdicaoNotaItem> adicoesNotaItem;

        public LinqNotaItem(MO.NotaItem _notaItem)
        {
            notaItem = _notaItem;

            adicoesNotaItem = new List<IAdicaoNotaItem>();
            foreach (var item in _notaItem.AdicaoNotaItems)
            {
                adicoesNotaItem.Add(new LinqAdicaoNotaItem(item));
            }
        }

        public override string AliqCupom
        {
            get { return notaItem.AliqCupom; }
        }

        public override decimal AliqICMS
        {
            get { return notaItem.AliqICMS.Value; }
        }

        public override decimal AliqICMSNormal
        {
            get { return notaItem.AliqICMSNormal.Value; }
        }

        public override decimal BaseICMS
        {
            get { return notaItem.BaseICMS.Value; }
        }

        public override decimal BaseICMSSubst
        {
            get { return notaItem.BaseICMSSubst.Value; }
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
            get { return notaItem.CalcCustoMedio.Value; }
        }

        public override DateTime Dt
        {
            get { return notaItem.Dt.Value; }
        }

        public override int Ent_Sai
        {
            get { return (int)notaItem.Ent_Sai; }
        }

        public override string InfAdicionais
        {
            get { return notaItem.InfAdicionais; }
        }

        public override decimal PercDesconto
        {
            get { return notaItem.PercDesconto.Value; }
        }

        public override decimal PesoBruto
        {
            get { return notaItem.PesoBruto.Value; }
        }

        public override decimal PesoLiquido
        {
            get { return notaItem.PesoLiquido.Value; }
        }

        public override string ProdutoCFOP
        {
            get { return notaItem.TabelaCFOP.CFOP; }
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
            get { return notaItem.RAT_Acrescimo.Value; }
        }

        public override decimal RAT_Desconto
        {
            get { return notaItem.RAT_Desconto.Value; }
        }

        public override int Sequencia
        {
            get { return notaItem.Sequencia.Value; }
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
            get { return (notaItem.Valor - Math.Round((notaItem.Valor * (decimal)notaItem.PercDesconto) / 100, 2)); }
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
            get { return notaItem.ValorDesconto.Value; }
        }

        public override decimal ValorICMS
        {
            get { return notaItem.ValorICMS.Value; }
        }

        public override decimal ValorICMSReducao
        {
            get { return notaItem.ValorICMSReducao.Value; }
        }

        public override decimal ValorICMSReducaoDif
        {
            get { return notaItem.ValorICMSReducaoDif.Value; }
        }

        public override decimal ValorIsentoICMS
        {
            get { return notaItem.ValorIsentoICMS.Value; }
        }

        public override decimal ValorOutroICMS
        {
            get { return notaItem.ValorOutroICMS.Value; }
        }

        public override decimal ValorRetidoICMS
        {
            get { return notaItem.ValorRetidoICMS.Value; }
        }

        public override string cEnq_O06
        {
            get { return notaItem.cEnq_O06; }
        }

        public override int modBCST_N18
        {
            get { return notaItem.modBCST_N18.Value; }
        }

        public override int modBC_N13
        {
            get { return notaItem.modBC_N13.Value; }
        }

        public override int orig_N11
        {
            get { return notaItem.orig_N11.Value; }
        }

        public override decimal pCOFINS_S08
        {
            get { return notaItem.pCOFINS_S08.Value; }
        }

        public override decimal pICMSST_N22
        {
            get { return notaItem.pICMSST_N22.Value; }
        }

        public override decimal pIPI_O13
        {
            get { return notaItem.pIPI_O13.Value; }
        }

        public override decimal pPIS_Q08
        {
            get { return notaItem.pPIS_Q08.Value; }
        }

        public override decimal pRedBC_N14
        {
            get { return notaItem.pRedBC_N14.Value; }
        }

        public override decimal vBC_O10
        {
            get { return notaItem.vBC_O10.Value; }
        }

        public override decimal vBC_Q07
        {
            get { return notaItem.vBC_Q07.Value; }
        }

        public override decimal vBC_S07
        {
            get { return notaItem.vBC_S07.Value; }
        }

        public override decimal vCOFINS_S11
        {
            get { return notaItem.vCOFINS_S11.Value; }
        }

        public override decimal vIPI_O14
        {
            get { return notaItem.vIPI_O14.Value; }
        }

        public override decimal vPIS_Q09
        {
            get { return notaItem.vPIS_Q09.Value; }
        }

        public override string CFOPCodigo
        {
            get { return notaItem.TabelaCFOP.CFOP; }
        }

        public override string CFOPDescricao
        {
            get { return notaItem.TabelaCFOP.Nome; }
        }

        public override decimal pCredSN_N29
        {
            get { return notaItem.pCredSN_N29.Value; }
        }

        public override decimal pMVAST_N19
        {
            get { return notaItem.pMVAST_N19.Value; }
        }

        public override decimal pRedBCST_N20
        {
            get { return notaItem.pRedBCST_N20.Value; }
        }

        public override decimal vCredICMSSN_N30
        {
            get { return notaItem.vCredICMSSN_N30.Value; }
        }

        public override string cEAN
        {
            get
            {
                // SEM GTIN
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
            get { return notaItem.P02_vBC == null ? 0 : (decimal)notaItem.P02_vBC; }
        }

        public override decimal P03_vDespAdu
        {
            get { return notaItem.P03_vDespAdu == null ? 0 : (decimal)notaItem.P03_vDespAdu; }
        }

        public override decimal P04_vII
        {
            get { return notaItem.P04_vII == null ? 0 : (decimal)notaItem.P04_vII; }
        }

        public override decimal P05_vIOF
        {
            get { return notaItem.P05_vIOF == null ? 0 : (decimal)notaItem.P05_vIOF; }
        }

        public override decimal? TotalImpostos
        {
            get { return notaItem.TotalImpostos; }
        }

        public override string FCI
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal OutrasDespesas
        {
            get { return notaItem.RateioOutrasDespesas == null ? 0 : (decimal)notaItem.RateioOutrasDespesas; }
        }

        public override decimal ValorFrete
        {
            get { return notaItem.RateioFrete == null ? 0 : (decimal)notaItem.RateioFrete; }
        }

        public override decimal ValorSeguro
        {
            get { return notaItem.RateioSeguro == null ? 0 : (decimal)notaItem.RateioSeguro; }
        }


        public override string NumDrawBack
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal vICMSDeson
        {
            get { throw new NotImplementedException(); }
        }

        public override int motDesICMS
        {
            get { throw new NotImplementedException(); }
        }

        public override int ViaTransp
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal ValorFreteRenovacaoMarinhaMercante
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal AliqInterna
        {
            get { return notaItem.AliqInterna == null ? 0 : (decimal)notaItem.AliqInterna; }
        }

        public override string Cest
        {
            get { return notaItem.Cest; }
        }

        public override decimal vICMSUFDest_NA15
        {
            get { return notaItem.AliqInterna == null ? 0 : (decimal)notaItem.vICMSUFDest_NA15; }
        }

        public override decimal vICMSUFRemet_NA17
        {
            get { return notaItem.AliqInterna == null ? 0 : (decimal)notaItem.vICMSUFRemet_NA17; }
        }

        public override decimal pICMSInter
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.pICMSInter == null ? 0 : (decimal)notaItem.pICMSInter; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }

        public override string cBenef_I05f
        {
            get { return notaItem.cBenef_I05f; }
        }

        public override int indEscala_I05d
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            get { return notaItem.indEscala_I05d == null ? 0 : (int)notaItem.indEscala_I05d; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
        }

        public override string CNPJFab_I05e
        {
            get { return notaItem.CNPJFab_I05e; }
        }

        public override decimal vBCFCP_N17a
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.vBCFCP_N17a == null ? 0 : (decimal)notaItem.vBCFCP_N17a; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }

        public override decimal pFCP_N17b
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.pFCP_N17b == null ? 0 : (decimal)notaItem.pFCP_N17b; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }
        public override decimal vFCP_N17c
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.vFCP_N17c == null ? 0 : (decimal)notaItem.vFCP_N17c; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }

        public override decimal vBCFCPST_N23a
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.vBCFCPST_N23a == null ? 0 : (decimal)notaItem.vBCFCPST_N23a; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }

        public override decimal pFCPST_N23b
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.pFCPST_N23b == null ? 0 : (decimal)notaItem.pFCPST_N23b; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }

        public override decimal vFCPST_N23d
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            get { return notaItem.vFCPST_N23d == null ? 0 : (decimal)notaItem.vFCPST_N23d; }
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
        }

        public override decimal pNacionalFederal { get { return 0; } } //notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.NacionalFederal ?? 0; } }
        public override decimal pImportadoFederal { get { return 0; } } //notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.ImportadosFederal ?? 0; } }
        public override decimal pEstadual { get { return 0; } } // notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.Estadual ?? 0; } }
        public override decimal pMunicipal { get { return 0; } } // notaItem.Produto.TabelaNCM == null ? 0 : notaItem.Produto.TabelaNCM.Municipal ?? 0; } }

        // Diferimento
        public override decimal AliquotaDiferimento => notaItem.AliquotaDiferimento;
        public override decimal ValorIcmsDiferimento => notaItem.ValorIcmsDiferimento;
        public override decimal ValorIcmsOp => notaItem.ValorIcmsOp;

        public override string cProdANP => notaItem.Produto?.cProdANP;

        public override string descANP => notaItem.Produto?.descANP;

        public override string UFCons => notaItem.Produto?.UFCons;
    }
}
