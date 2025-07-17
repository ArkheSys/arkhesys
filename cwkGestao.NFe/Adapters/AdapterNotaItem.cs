using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkNotaFiscalEletronica.Interfaces;
using MO = Modelo;
using cwkGestao.NFe.Adapters.NH;
using cwkGestao.NFe.Adapters.Linq;

namespace cwkGestao.NFe.Adapters
{
    abstract class AdapterNotaItem : INotaItem
    {
        public static AdapterNotaItem Produce(object notaItem)
        {
            if (notaItem.GetType() == typeof(MO.NotaItem))
            {
                return new LinqNotaItem((MO.NotaItem)notaItem);
            }
            else if (notaItem.GetType() == typeof(Modelo.NotaItem))
            {
                return new NHNotaItem((Modelo.NotaItem)notaItem);
            }

            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.NotaItem ou cwkGestao.Modelo.NotaItem");
        }

        public abstract string AliqCupom
        {
            get;
        }

        public abstract decimal AliqICMS
        {
            get;
        }

        public abstract decimal AliqICMSNormal
        {
            get;
        }

        public abstract decimal BaseICMS
        {
            get;
        }

        public abstract decimal BaseICMSSubst
        {
            get;
        }

        public abstract string CST_Cofins
        {
            get;
        }

        public abstract string CST_Ipi
        {
            get;
        }

        public abstract string CST_Pis
        {
            get;
        }

        public abstract bool CalcCustoMedio
        {
            get;
        }

        public abstract DateTime Dt
        {
            get;
        }

        public abstract int Ent_Sai
        {
            get;
        }

        public abstract string InfAdicionais
        {
            get;
        }

        public abstract decimal PercDesconto
        {
            get;
        }

        public abstract decimal PesoBruto
        {
            get;
        }

        public abstract decimal PesoLiquido
        {
            get;
        }

        public abstract string ProdutoCFOP
        {
            get;
        }

        public abstract string ProdutoCodigo
        {
            get;
        }

        public abstract string ProdutoDescReduzida
        {
            get;
        }

        public abstract string ProdutoNCM
        {
            get;
        }

        public abstract string ProdutoNome
        {
            get;
        }

        public abstract decimal Quantidade
        {
            get;
        }

        public abstract decimal RAT_Acrescimo
        {
            get;
        }

        public abstract decimal RAT_Desconto
        {
            get;
        }

        public abstract int Sequencia
        {
            get;
        }

        public abstract string SitTrib
        {
            get;
        }

        public abstract decimal SubTotal
        {
            get;
        }

        public abstract string TAG_CST
        {
            get;
        }

        public abstract string TextoLei
        {
            get;
        }

        public abstract decimal Total
        {
            get;
        }

        public abstract string Unidade
        {
            get;
        }

        public abstract decimal Valor
        {
            get;
        }

        public abstract decimal ValorCalculado
        {
            get;
        }

        public abstract decimal ValorCustoMedia
        {
            get;
        }

        public abstract decimal ValorDesconto
        {
            get;
        }

        public abstract decimal ValorICMS
        {
            get;
        }

        public abstract decimal ValorICMSReducao
        {
            get;
        }

        public abstract decimal ValorICMSReducaoDif
        {
            get;
        }

        public abstract decimal ValorIsentoICMS
        {
            get;
        }

        public abstract decimal ValorOutroICMS
        {
            get;
        }

        public abstract decimal ValorRetidoICMS
        {
            get;
        }

        public abstract string cEnq_O06
        {
            get;
        }

        public abstract int modBCST_N18
        {
            get;
        }

        public abstract int modBC_N13
        {
            get;
        }

        public abstract int orig_N11
        {
            get;
        }

        public abstract decimal pCOFINS_S08
        {
            get;
        }

        public abstract decimal pICMSST_N22
        {
            get;
        }

        public abstract decimal pIPI_O13
        {
            get;
        }

        public abstract decimal pPIS_Q08
        {
            get;
        }

        public abstract decimal pRedBC_N14
        {
            get;
        }

        public abstract decimal vBC_O10
        {
            get;
        }

        public abstract decimal vBC_Q07
        {
            get;
        }

        public abstract decimal vBC_S07
        {
            get;
        }

        public abstract decimal vCOFINS_S11
        {
            get;
        }

        public abstract decimal vIPI_O14
        {
            get;
        }

        public abstract decimal vPIS_Q09
        {
            get;
        }

        public abstract string CFOPCodigo
        {
            get;
        }

        public abstract string CFOPDescricao
        {
            get;
        }

        public abstract decimal pCredSN_N29
        {
            get;
        }

        public abstract decimal pMVAST_N19
        {
            get;
        }

        public abstract decimal pRedBCST_N20
        {
            get;
        }

        public abstract string cEAN { get; }
        public abstract string cEANTrib { get; }

        public abstract decimal vCredICMSSN_N30
        {
            get;
        }

        public abstract IList<IAdicaoNotaItem> AdicoesNotaItem
        {
            get;
        }

        public abstract string I19_nDI
        {
            get;
        }

        public abstract DateTime? I20_dDI
        {
            get;
        }

        public abstract string I21_xLocDesemb
        {
            get;
        }

        public abstract string I22_UFDesemb
        {
            get;
        }

        public abstract DateTime? I23_dDesemb
        {
            get;
        }

        public abstract string I24_cExportador
        {
            get;
        }

        public abstract decimal P02_vBC
        {
            get;
        }

        public abstract decimal P03_vDespAdu
        {
            get;
        }

        public abstract decimal P04_vII
        {
            get;
        }

        public abstract decimal P05_vIOF
        {
            get;
        }

        public abstract decimal? TotalImpostos
        {
            get;
        }

        public abstract string FCI
        {
            get;
        }

        public abstract string NumDrawBack
        {
            get;
        }

        public abstract decimal vICMSDeson
        {
            get;
        }

        public abstract int motDesICMS
        {
            get;
        }

        public abstract int ViaTransp
        {
            get;
        }

        public abstract decimal ValorFreteRenovacaoMarinhaMercante
        {
            get;
        }

        public abstract decimal AliqInterna
        {
            get;
        }

        public abstract string Cest
        {
            get;
        }

        public abstract decimal vICMSUFDest_NA15
        {
            get;
        }

        public abstract decimal vICMSUFRemet_NA17
        {
            get;
        }

        public abstract decimal pICMSInter
        {
            get;
        }
        public abstract string cBenef_I05f
        {
            get;
        }
        public abstract int indEscala_I05d
        {
            get;
        }
        public abstract string CNPJFab_I05e
        {
            get;
        }
        public abstract decimal vBCFCP_N17a
        {
            get;
        }
        public abstract decimal pFCP_N17b
        {
            get;
        }
        public abstract decimal vFCP_N17c
        {
            get;
        }
        public abstract decimal vBCFCPST_N23a
        {
            get;
        }
        public abstract decimal pFCPST_N23b
        {
            get;
        }
        public abstract decimal vFCPST_N23d
        {
            get;
        }

        public abstract decimal pNacionalFederal { get; }
        public abstract decimal pImportadoFederal { get; }
        public abstract decimal pEstadual { get; }
        public abstract decimal pMunicipal { get; }

        // Diferu
        public abstract decimal AliquotaDiferimento { get; }
        public abstract decimal ValorIcmsDiferimento { get; }
        public abstract decimal ValorIcmsOp { get; }


        #region INotaItem Members


        public abstract decimal OutrasDespesas
        {
            get;
        }

        public abstract decimal ValorFrete
        {
            get;
        }

        public abstract decimal ValorSeguro
        {
            get;
        }

        public abstract string cProdANP { get; }
        public abstract string descANP { get; }
        public abstract string UFCons { get; }

        #endregion
    }
}