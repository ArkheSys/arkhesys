using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Operacao
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Operacao objOperacao)
        {
            Dictionary<string, string> ret = ValidaObjeto(objOperacao);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.Operacao objOperacao)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objOperacao.Nome == String.Empty)
            {
                ret.Add("txtNome", "Digite um nome.");
            }

            return ret;
        }
    }
}
