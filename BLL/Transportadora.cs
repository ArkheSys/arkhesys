using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Transportadora
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Transportadora objTransportadora)
        {
            Dictionary<string, string> ret = ValidaObjeto(objTransportadora);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.Transportadora objTransportadora)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(objTransportadora.Nome))
                ret.Add("txtNome", "Campo Obrigatório.");

            if (objTransportadora.IDCidade == 0)
                ret.Add("cbCidade", "Campo Obrigatório.");

            return ret;
        }
    }
}