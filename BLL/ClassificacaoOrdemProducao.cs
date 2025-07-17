using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ClassificacaoOrdemProducao
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.ClassificacaoOrdemProducao objClassificacaoOS)
        {
            Dictionary<string, string> ret = ValidaObjeto(objClassificacaoOS);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.ClassificacaoOrdemProducao objClassificacaoOS)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(objClassificacaoOS.Nome))
                ret.Add("txtNome", "Campo Obrigatório.");
            if (objClassificacaoOS.Codigo <= 0)
                ret.Add("txtCodigo", "Campo Obrigatório.");           

            return ret;
        }
    }
}
