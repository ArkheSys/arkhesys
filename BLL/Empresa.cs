using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Empresa
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Empresa objEmpresa)
        {
            Dictionary<string, string> ret = ValidaObjeto(objEmpresa);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.Empresa objEmpresa)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objEmpresa.Nome == String.Empty)
            {
                ret.Add("txtNome", "Digite um nome.");
            }

            return ret;
        }


    }
}
