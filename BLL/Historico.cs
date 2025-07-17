using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Historico
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Historico objHistorico)
        {
            Dictionary<string, string> ret = ValidaObjeto(objHistorico);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.Historico objHistorico)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objHistorico.Nome == String.Empty)
            {
                ret.Add("txtNome", "Digite um nome.");
            }
            return ret;
        }
    }
}
