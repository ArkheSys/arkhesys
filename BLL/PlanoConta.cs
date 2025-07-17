using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PlanoConta
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.PlanoConta objPlanoConta)
        {
            Dictionary<string, string> ret = ValidaObjeto(objPlanoConta);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.PlanoConta objPlanoConta)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objPlanoConta.Nome == "")
            {
                ret.Add("txtNome", "Digite um nome.");
            }

            return ret;
        }
    }
}
