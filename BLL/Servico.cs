using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Servico
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Servico objServico)
        {
            Dictionary<string, string> ret = ValidaObjeto(objServico);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.Servico objServico)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(objServico.nome))
                ret.Add("txtNome", "Campo Obrigatório.");
            if (objServico.codigo <= 0)
                ret.Add("txtCome", "Campo Obrigatório.");
            if (objServico.valor < 0)
                ret.Add("txtValor", "Campo Obrigatório.");

            return ret;
        }
    }
}
