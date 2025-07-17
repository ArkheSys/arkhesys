using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Acrescimo
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Acrescimo objAcrescimo)
        {
            Dictionary<string, string> ret = ValidaObjeto(objAcrescimo);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.Acrescimo objAcrescimo)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objAcrescimo.Nome == "")
            {
                ret.Add("txtPessoa", "Digite um nome.");
            }

            return ret;
        }
    }
}
