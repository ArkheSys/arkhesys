using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BalancoItem
    {
        public static Dictionary<string, string> Salvar(int pAcao, Modelo.BalancoItem objBalancoItem, Modelo.Balanco objBalanco, Modelo.DataClassesDataContext db)
        {
            Dictionary<string, string> ret = ValidaObjeto(pAcao, objBalancoItem, objBalanco);
            if (ret.Count == 0)
            {
                if (objBalancoItem.ID == 0)
                {
                    objBalancoItem.IncData = DateTime.Now.Date;
                    objBalancoItem.IncHora = DateTime.Now;
                    objBalancoItem.IncUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;

                    objBalanco.BalancoItems.Add(objBalancoItem);
                }
                else if (pAcao == 3)
                {
                    if (objBalancoItem.ID != 0)
                    {
                        db.BalancoItems.DeleteOnSubmit(objBalancoItem);
                    }
                    objBalanco.BalancoItems.Remove(objBalancoItem);
                }
                else
                {
                    objBalancoItem.AltData = DateTime.Now.Date;
                    objBalancoItem.AltHora = DateTime.Now;
                    objBalancoItem.AltUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                }
            }
            else if (pAcao == 1)
                objBalancoItem.Produto = null;
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(int pAcao, Modelo.BalancoItem objBalancoItem, Modelo.Balanco objBalanco)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objBalancoItem.IDProduto == 0)
                ret.Add("lkProduto", "Campo Obrigatório.");
            if (pAcao == 1 && objBalanco.BalancoItems.Where(b => b.IDProduto == objBalancoItem.IDProduto).Count() > 0)
                ret.Add("lkProduto", "O produto selecionado já foi adicionado ao balanço.");
            if (objBalancoItem.Quantidade < 0)
                ret.Add("txtQuantidade", "A quantidade deve ser maior ou igual a zero.");
            return ret;
        }
    }
}
