using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ProjetoHistorico
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.ProjetoHistorico ObjProjHistorico)
        {
            Dictionary<string, string> ret = ValidaObjeto(ObjProjHistorico);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.ProjetoHistorico ObjProjHistorico)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (ObjProjHistorico.Descricao.TrimEnd() == String.Empty)
            {
                ret.Add("txtDescricao", "Digite uma descrição.");
            }

            if (ObjProjHistorico.Data.ToString().Substring(0,10) == "01/01/0001")
            {
                ret.Add("txtData", "Entre com a data.");
            }

            if (ObjProjHistorico.Data > DateTime.Now)
            {
                ret.Add("txtData", "A data deve ser menor ou igual a data atual do sistema.");
            }

            return ret;
        }

    }
}
