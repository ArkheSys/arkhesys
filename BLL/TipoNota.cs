using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BLL
{
    public class TipoNota
    {
        
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.TipoNota objTipoNota)
        {
            Dictionary<string, string> ret = ValidaObjeto(objTipoNota, db);
            if (ret.Count == 0)
            {
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'IX_TipoNota_Codigo'."))
                        throw new Exception("O código escolhido já existe, por favor escolha outro.");
                    else
                        throw ex;
                }
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.TipoNota objTipoNota, Modelo.DataClassesDataContext db)
        {
            Modelo.PlanoConta objPlanoConta = new Modelo.PlanoConta();
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(objTipoNota.Nome))
                ret.Add("txtNome", "Campo Obrigatório.");

            if (String.IsNullOrEmpty(objTipoNota.Ordem))
                ret.Add("txtOrdem", "Campo Obrigatório.");

            if (String.IsNullOrEmpty(objTipoNota.Serie))
                ret.Add("txtSerie", "Campo Obrigatório.");

            if (!objTipoNota.ModeloDocto.HasValue)
                ret.Add("txtModeloDocto", "Campo Obrigatório.");

#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'OrigemNFType' nunca é igual a "null" do tipo 'OrigemNFType?'
            if (objTipoNota.NFOrigem == null)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'OrigemNFType' nunca é igual a "null" do tipo 'OrigemNFType?'
                ret.Add("cbNfOrigem", "Campo Obrigatório.");

            if (objTipoNota.Ent_Sai == 0)
                ret.Add("rgEntSai", "Campo Obrigatório.");

            if (objTipoNota.bGeraFinanceiro == Modelo.TituloType.Sim)
            {
                if (!objTipoNota.IDPlanoConta.HasValue)
                    ret.Add("cbPlanoConta", "Campo Obrigatório.");
                else
                {
                    if (objPlanoConta.GetPlanoConta((int)objTipoNota.IDPlanoConta, db).bTitulo == Modelo.TituloType.Sim)
                    {
                        ret.Add("cbPlanoConta", "O Plano de conta não pode ser título.");                        
                    }                    
                }
                if (!objTipoNota.IDPlanoContaFrete.HasValue)
                    ret.Add("cbPlanoContaFrete", "Campo Obrigatório.");
                else
                {
                    if (objPlanoConta.GetPlanoConta((int)objTipoNota.IDPlanoContaFrete, db).bTitulo == Modelo.TituloType.Sim)
                    {
                        ret.Add("cbPlanoContaFrete", "O Plano de conta Frete não pode ser título.");                        
                    } 
                }

                if (!objTipoNota.IDPlanoContaIPI.HasValue)
                    ret.Add("cbPlanoContaIPI", "Campo Obrigatório.");
                else
                {
                    if (objPlanoConta.GetPlanoConta((int)objTipoNota.IDPlanoContaIPI, db).bTitulo == Modelo.TituloType.Sim)
                    {
                        ret.Add("cbPlanoContaIPI", "O Plano de conta IPI não pode ser título.");                        
                    }                    
                }

                if (!objTipoNota.IDBanco.HasValue)
                    ret.Add("cbBanco", "Campo Obrigatório.");

                if (!objTipoNota.IDAcrescimo.HasValue)
                    ret.Add("cbAcrescimo", "Campo Obrigatório.");

                if (!objTipoNota.IDHistorico.HasValue)
                    ret.Add("cbHistorico", "Campo Obrigatório.");

                if (!objTipoNota.IDPortador.HasValue)
                    ret.Add("cbPortador", "Campo Obrigatório.");
            }

            return ret;
        }
    }
}