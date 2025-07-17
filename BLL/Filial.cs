using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Filial
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Filial objFilial)
        {
            Dictionary<string, string> ret = ValidaObjeto(objFilial);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.Filial objFilial)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objFilial.Nome == "")
            {
                ret.Add("txtPessoa", "Digite um nome.");
            }

            if (objFilial.CNPJ == String.Empty) 
            {
                ret.Add("txtCNPJ", "Digite um CNPJ válido.");
            }

            if (objFilial.Empresa == null) 
            {
                ret.Add("cbGrupo", "Escolha um grupo.");
            }

            if (objFilial.BEnviarCopiaEmail && objFilial.EmailNFe.Trim().Length == 0)
            {
                ret.Add("txtEmailNFe", "Informe um email.");
            }

            return ret;
        }

    }
}
