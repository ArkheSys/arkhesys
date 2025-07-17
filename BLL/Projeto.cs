using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BLL
{
    public class Projeto
    {
        private Modelo.DataClassesDataContext db;

        public Projeto()
        {
            db = new Modelo.DataClassesDataContext();
        }

        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Projeto objProjeto)
        {
            Dictionary<string, string> ret = ValidaObjeto(objProjeto);
            if (ret.Count == 0)
            {
                if(objProjeto.IDAcrescimo == 0) objProjeto.IDAcrescimo = null;
                if (objProjeto.IDBanco == 0) objProjeto.IDBanco = null;
                if (objProjeto.IDCondicao == 0) objProjeto.IDCondicao = null;
                if (objProjeto.IDFilial == 0) objProjeto.IDFilial = null;
                if (objProjeto.IDHistorico == 0) objProjeto.IDHistorico = null;
                if (objProjeto.IDPlanoConta == 0) objProjeto.IDPlanoConta = null;
                if (objProjeto.IDPlanoContaServico == 0) objProjeto.IDPlanoContaServico = null;
                if (objProjeto.IDPortador == 0) objProjeto.IDPortador = null;
                

                if (objProjeto.ID > 0)
                {
                    objProjeto.AltData = DateTime.Now.Date;
                    objProjeto.AltHora = DateTime.Now;
                    objProjeto.AltUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                }
                else
                {
                    objProjeto.IncData = DateTime.Now.Date;
                    objProjeto.IncHora = DateTime.Now;
                    objProjeto.IncUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                }
                db.SubmitChanges();
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.Projeto objProjeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objProjeto.IDFilial == 0)
            {
                ret.Add("lkFilial", "Selecione a empresa.");
            }

            if (objProjeto.IDPessoa == 0)
            {
                ret.Add("lkPessoa", "Selecione o cliente.");
            }

            if (objProjeto.Nome == null)
            {
                ret.Add("txtNome", "Preencha o nome do projeto.");
            }
            else
            {
                if (objProjeto.Nome.TrimEnd() == String.Empty)
                {
                    ret.Add("txtNome", "Preencha o nome do projeto.");
                }
            }

            if (objProjeto.Status == -1)
            {
                ret.Add("cbStatus", "Selecione um status da lista.");
            }
            else
            {
                if (objProjeto.Status == 5 && objProjeto.Status_Ant != 5)
                    ret.Add("cbStatus", "A conclusão do projeto deve ser feita na tela anterior.");
            }

            if (objProjeto.Data == new DateTime() || objProjeto.Data == null)
            {
                ret.Add("txtData", "Preencha a data de início do projeto.");
            }

            if (objProjeto.Descricao == null)
            {
                ret.Add("txtDescricao", "Preencha a descrição do projeto.");
            }
            else
            {
                if (objProjeto.Descricao.TrimEnd() == String.Empty)
                {
                    ret.Add("txtDescricao", "Preencha a descrição do projeto.");
                }
            }

            if (!String.IsNullOrEmpty(objProjeto.CaminhoDocumento))
            {
                if (!File.Exists(objProjeto.CaminhoDocumento))
                {
                    ret.Add("txtCaminhoDocumento", "Selecione um documento existente.");
                }
            }

            return ret;
        }

        #region Produto e Servico

        public static Dictionary<string, string> SalvarProdutoItem(Modelo.DataClassesDataContext db, Modelo.Projeto objProjeto, Modelo.Projeto_ProdutoServico objProdutoServico, Modelo.Produto objProduto, int pAcao)
        {
            Dictionary<string, string> ret = ValidaProdutoItem(objProdutoServico, objProduto);
            if (ret.Count == 0)
            {
                switch (pAcao)
                {
                    case 1:
                        if (objProjeto.Projeto_ProdutoServicos.Where(p => p.IDProduto == objProduto.ID).Count() == 0)
                        {
                            objProdutoServico.Produto = objProduto;
                            objProdutoServico.IncData = DateTime.Now.Date;
                            objProdutoServico.IncHora = DateTime.Now;
                            objProdutoServico.IncUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                            objProjeto.Projeto_ProdutoServicos.Add(objProdutoServico);
                        }
                        else
                        {
                            ret.Add("lkProduto", "O produto selecionado já foi adicionado ao orçamento.");
                        }
                        break;
                    case 2:
                        objProdutoServico.Produto = objProduto;
                        objProdutoServico.AltData = DateTime.Now.Date;
                        objProdutoServico.AltData = DateTime.Now;
                        objProdutoServico.AltUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                        break;
                    case 3:
                        if (objProdutoServico.ID > 0)
                        {
                            db.Projeto_ProdutoServicos.DeleteOnSubmit(objProdutoServico);
                        }
                        objProjeto.Projeto_ProdutoServicos.Remove(objProdutoServico);
                        objProdutoServico.Produto = null;
                        objProdutoServico.Projeto = null;
                        objProdutoServico = null;
                        break;
                }
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaProdutoItem(Modelo.Projeto_ProdutoServico objProdutoServico, Modelo.Produto objProduto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objProduto == null)
            {
                ret.Add("lkProduto", "Selecione o produto.");
            }

            if (objProdutoServico.Quantidade <= 0)
            {
                ret.Add("txtQuantidade", "A quantidade deve ser maior que zero(0).");
            }
            return ret;
        }

        public static Dictionary<string, string> SalvarServicoItem(Modelo.DataClassesDataContext db, Modelo.Projeto objProjeto, Modelo.Projeto_ProdutoServico objProdutoServico, int pAcao)
        {
            Dictionary<string, string> ret = ValidaServicoItem(objProdutoServico);
            if (ret.Count == 0)
            {
                switch (pAcao)
                {
                    case 1:
                        objProdutoServico.IncData = DateTime.Now.Date;
                        objProdutoServico.IncHora = DateTime.Now;
                        objProdutoServico.IncUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                        objProdutoServico.Agrupamento++;
                        while (objProjeto.Projeto_ProdutoServicos.Where(p => p.IDServico == objProdutoServico.IDServico && p.Agrupamento == objProdutoServico.Agrupamento).Count() > 0)
                        {
                            
                            objProdutoServico.Agrupamento++;
                            
                        }
                        objProjeto.Projeto_ProdutoServicos.Add(objProdutoServico);
                       /* else
                        {
                            ret.Add("lkServico", "O serviço selecionado já foi adicionado ao orçamento.");
                        }*/
                        break;
                    case 2:
                        objProdutoServico.AltData = DateTime.Now.Date;
                        objProdutoServico.AltData = DateTime.Now;
                        objProdutoServico.AltUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                        break;
                    case 3:
                        if (objProdutoServico.ID > 0)
                        {
                            db.Projeto_ProdutoServicos.DeleteOnSubmit(objProdutoServico);
                        }
                        objProjeto.Projeto_ProdutoServicos.Remove(objProdutoServico);
                        objProdutoServico.IDServico = 0;
                        objProdutoServico.Projeto = null;
                        objProdutoServico = null;
                        break;
                }
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaServicoItem(Modelo.Projeto_ProdutoServico objProdutoServico)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objProdutoServico.IDServico == 0)
            {
                ret.Add("lkServico", "Selecione o serviço.");
            }

            if (objProdutoServico.Quantidade <= 0)
            {
                ret.Add("txtQuantidade", "A quantidade deve ser maior que zero(0).");
            }
            return ret;
        }

        #endregion

        #region Material

        public static Dictionary<string, string> SalvarMaterialItem(Modelo.DataClassesDataContext db, Modelo.Projeto objProjeto, Modelo.Projeto_Material objMaterial, Modelo.Produto objProduto, int pAcao)
        {
            Dictionary<string, string> ret = ValidaMaterialItem(objMaterial, objProduto);
            if (ret.Count == 0)
            {
                switch (pAcao)
                {
                    case 1:
                        if (objProjeto.Projeto_Materials.Where(p => p.IDProduto == objProduto.ID).Count() == 0)
                        {
                            objMaterial.Produto = objProduto;
                            objMaterial.IncData = DateTime.Now.Date;
                            objMaterial.IncHora = DateTime.Now;
                            objMaterial.IncUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                            objProjeto.Projeto_Materials.Add(objMaterial);
                        }
                        else
                        {
                            ret.Add("lkProduto", "O produto selecionado já foi adicionado ao orçamento.");
                        }
                        break;
                    case 2:
                        objMaterial.Produto = objProduto;
                        objMaterial.AltData = DateTime.Now.Date;
                        objMaterial.AltData = DateTime.Now;
                        objMaterial.AltUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                        break;
                    case 3:
                        if (objMaterial.ID > 0)
                        {
                            db.Projeto_Materials.DeleteOnSubmit(objMaterial);
                        }
                        objProjeto.Projeto_Materials.Remove(objMaterial);
                        objMaterial.Produto = null;
                        objMaterial.Projeto = null;
                        objMaterial = null;
                        break;
                }
            }
            return ret;
        }

        private static Dictionary<string, string> ValidaMaterialItem(Modelo.Projeto_Material objMaterial, Modelo.Produto objProduto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objProduto == null)
            {
                ret.Add("lkProduto", "Selecione o produto.");
            }

            if (objMaterial.Quantidade <= 0)
            {
                ret.Add("txtQuantidade", "A quantidade deve ser maior que zero(0).");
            }
            return ret;
        }

        #endregion

        public decimal GetValorProduto(Modelo.DataClassesDataContext db, int tipoPreco, Modelo.Produto objProduto)
        {
            switch (tipoPreco)
            {
                case 0: //Venda
                    BLL.Produto bllProduto = new BLL.Produto(db);
                    var tabPreco = db.TabelaPrecos.OrderBy(t => t.ID);
                    if (tabPreco.Count() > 0)
                        return bllProduto.getPreco(objProduto, tabPreco.First().ID, Modelo.TipoPrecoType.Normal);
                    break;
                case 1: //Último Custo
                    return objProduto.UltimoCusto.HasValue ? objProduto.UltimoCusto.Value : 0.0m;
                case 2: //Custo Médio
                    var ultimaNotaItem = db.NotaItems.Where(n => n.CalcCustoMedio == true && objProduto.ID == n.IDProduto)
                                                     .OrderByDescending(a => a.ID).Take(1);
                    if (ultimaNotaItem.Count() == 1)
                        return ultimaNotaItem.Single().ValorCustoMedio;
                    break;
            }
            return 0m;
        }

        public IQueryable getListaCombo()
        {
            IQueryable lista;
            lista = from p in db.Projetos
                    select new
                    {
                        Nome = p.Nome + " | " + p.ID
                    };
            return lista;
        }
    }
}