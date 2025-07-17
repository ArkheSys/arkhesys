using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class GrupoEstoque
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.GrupoEstoque objGrupoEstoque)
        {
            Dictionary<string, string> ret = ValidaObjeto(objGrupoEstoque);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.GrupoEstoque objGrupoEstoque)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objGrupoEstoque.Nome == "")
            {
                ret.Add("txtNome", "Digite um nome.");
            }
           
            return ret;
        }
    }
}
