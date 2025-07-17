using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class NotaItem : Modelo.IMetodosCwork
    {
        public decimal Quantidade_Ant { get; set; }

        partial void OnLoaded()
        {
            Quantidade_Ant = Quantidade;
        }

        partial void OnCreated()
        {
            this.Valor = 0;
            this.Quantidade = 1;
            this.ValorDesconto = 0;
            this.PercDesconto = 0;
            this.RAT_Acrescimo = 0;
            this.RAT_Desconto = 0;
            this.vBC_O10 = 0;
            this.pIPI_O13 = 0;
            this.vIPI_O14 = 0;
            this.vBC_Q07 = 0;
            this.pPIS_Q08 = 0;
            this.vPIS_Q09 = 0;
            this.vBC_S07 = 0;
            this.pCOFINS_S08 = 0;
            this.vCOFINS_S11 = 0;
            this.BaseICMS = 0;
            this.AliqICMS = 0;
            this.AliqICMSNormal = 0;
            this.ValorICMS = 0;
            this.ValorIsentoICMS = 0;
            this.ValorOutroICMS = 0;
            this.BaseICMSSubst = 0;
            this.ValorICMSReducao = 0;
            this.ValorICMSReducaoDif = 0;
            this.modBC_N13 = 0;
            this.modBCST_N18 = 0;
            this.pRedBC_N14 = 0;
            this.pICMSST_N22 = 0;
            this.pCredSN_N29 = 0;
            this.vCredICMSSN_N30 = 0;
            this.pMVAST_N19 = 0;
            this.pRedBCST_N20 = 0;
            this.ValorRetidoICMS = 0;
            this.TextoLei = String.Empty;
            this.pFCP_N17b = 0;
            this.vBCFCP_N17a = 0;
            this.vFCP_N17c = 0;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = null;
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = null;
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IList getListaCombo(DataClassesDataContext db)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = null;
            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.NotaItems where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.NotaItems where u.Sequencia.ToString() == pValor select u).Single().ID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar a maior sequencia de uma determinada lista
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXSequencia(Modelo.Nota pNota)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from c in pNota.NotaItems
                                  where c.Sequencia == pNota.NotaItems.Max(b => b.Sequencia)
                                  select c).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// Método que atualiza os valores calculados de ICMS (Imposto)
        /// </summary>
        /// <param name="pImposto">Valores para Atualização</param>
        public void AtualizaImposto(Modelo.ImpostoNF pImposto)
        {
            BaseICMS = pImposto.baseIcms;
            AliqICMS = pImposto.aliquotaIcms;
            AliqICMSNormal = pImposto.aliquotaIcmsNormal;
            AliqCupom = pImposto.aliquotaCupomFiscal;
            ValorICMS = pImposto.valorIcms;
            ValorIsentoICMS = pImposto.valorIsentoIcms;
            ValorOutroICMS = pImposto.valorOutrosIcms;
            ValorRetidoICMS = pImposto.valorRetidoIcms;
            SitTrib = pImposto.situacaoTibutaria;
            BaseICMSSubst = pImposto.baseIcmsSubst;
            ValorICMSReducao = pImposto.pRedBC_N14;
            ValorICMSReducaoDif = pImposto.diferencaReducaoIcms;
            TextoLei = pImposto.textoLei;

            TAG_CST = pImposto.tagCST;
            modBC_N13 = Convert.ToInt32(pImposto.modBC_N13);
            modBCST_N18 = Convert.ToInt32(pImposto.modBCST_N18);
            CST_Pis = pImposto.CST_Pis;
            vBC_Q07 = pImposto.vBC_Q07;
            pPIS_Q08 = pImposto.pPIS_Q08;
            vPIS_Q09 = pImposto.vPIS_Q09;
            CST_Cofins = pImposto.CST_Cofins;
            vBC_S07 = pImposto.vBC_S07;
            pCOFINS_S08 = pImposto.pCOFINS_S08;
            vCOFINS_S11 = pImposto.vCOFINS_S11;
            orig_N11 = pImposto.orig_N11;
            vBC_O10 = pImposto.vBC_O10;

            vBC_O10 = pImposto.vBC_O10;
            pIPI_O13 = pImposto.pIPI_O13;
            vIPI_O14 = pImposto.vIPI_O14;
            CST_Ipi = pImposto.CST_Ipi;
            pRedBC_N14 = pImposto.pRedBC_N14;
            pICMSST_N22 = pImposto.aliquotaSubstituicaoTributaria;
            cEnq_O06 = pImposto.cEnq_O06.ToString();

            pMVAST_N19 = pImposto.lucroSubstituicaoTributaria;
            pRedBCST_N20 = pImposto.percRedBCST;
            pCredSN_N29 = pImposto.pCredSN_N29;
            vCredICMSSN_N30 = pImposto.vCredICMSSN_N30;
            pFCP_N17b = pImposto.AliqFCP;
            vBCFCP_N17a = pImposto.BCFCP;
            vFCP_N17c = pImposto.VFCP;
        }

        public string cBenef_I05f { get; set; }
        public int indEscala_I05d { get; set; }
        public string CNPJFab_I05e { get; set; }

        public decimal vBCFCP_N17a { get; set; }
        public decimal pFCP_N17b { get; set; }
        public decimal vFCP_N17c { get; set; }
        public decimal vBCFCPST_N23a { get; set; }
        public decimal pFCPST_N23b { get; set; }
        public decimal vFCPST_N23d { get; set; }

        public decimal AliquotaDiferimento { get; set; }
        public decimal ValorIcmsDiferimento { get; set; }
        public decimal ValorIcmsOp { get; set; }

        public decimal ValorICMSDesoneracao { get; set; }
    }
}