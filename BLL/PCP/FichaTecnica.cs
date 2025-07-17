using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.PCP
{
    public class FichaTecnica
    {
        public static IQueryable getLista(Modelo.DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.Produtos
                    where db.FichaTecnicas.Where(f => f.IDProduto == p.ID).Count() > 0
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        ID = p.ID
                    };
            return lista;
        }

        public static List<Modelo.Objeto.pxyItemFichaTecnica> getItensFichaTecnica(Modelo.DataClassesDataContext db, int idFichaTecnica)
        {
            List<Modelo.Objeto.pxyItemFichaTecnica> ret = (from i in db.ItemFichaTecnicas
                                                           join p in db.Produtos on i.IDProduto equals p.ID
                                                           where i.IDFichaTecnica == idFichaTecnica
                                                           select new Modelo.Objeto.pxyItemFichaTecnica
                                                           {
                                                               ID = i.ID,
                                                               IDProduto = i.IDProduto,
                                                               Acao = 4,
                                                               Quantidade = i.Quantidade,
                                                               Codigo = p.Codigo,
                                                               Descricao = p.Nome
                                                           }).ToList();

            for (int i = 0; i < ret.Count; i++)
            {
                ret[i].Sequencia = i + 1;
            }

            return ret;
        }

        public static List<Modelo.Objeto.pxyEtapaProducao> getEtapasProducao(Modelo.DataClassesDataContext db, int idProduto)
        {
            List<Modelo.Objeto.pxyEtapaProducao> ret = (from i in db.EtapaProducaos
                                                           where i.IDProduto == idProduto
                                                           select new Modelo.Objeto.pxyEtapaProducao
                                                           {
                                                               ID = i.ID,
                                                               Sequencia = i.Sequencia,
                                                               IDProduto = i.IDProduto,
                                                               IDServico = i.IDServico,
                                                               Titulo = i.Titulo,
                                                               Observacao = i.Observacao,
                                                               Acao = 4
                                                           }).ToList();

            return ret;
        }

        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.FichaTecnica objFichaTecnica, List<Modelo.Objeto.pxyItemFichaTecnica> itens, int acao, bool novaVersao, List<Modelo.Objeto.pxyEtapaProducao> etapas)
        {
            Dictionary<string, string> ret = ValidaObjeto(objFichaTecnica);

            if (ret.Count == 0)
            {
                if (acao == 1)
                {
                    objFichaTecnica.DataVersao = DateTime.Now;
                    IncluiItens(itens, objFichaTecnica);
                }
                if (acao == 2 && novaVersao)
                {
                    Modelo.FichaTecnica objFichaNew = new Modelo.FichaTecnica();
                    objFichaNew.IDProduto = objFichaTecnica.IDProduto;
                    objFichaNew.Descricao = objFichaTecnica.Descricao;
                    objFichaNew.Versao = objFichaTecnica.Versao;
                    objFichaNew.DataVersao = DateTime.Now;
                    IncluiItens(itens, objFichaNew);
                    db.FichaTecnicas.InsertOnSubmit(objFichaNew);
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, objFichaTecnica);
                }
                else if (acao == 2)
                {
                    Modelo.ItemFichaTecnica objItem;
                    foreach (Modelo.Objeto.pxyItemFichaTecnica item in itens)
                    {
                        switch (item.Acao)
                        {
                            case 1:
                                objItem = new Modelo.ItemFichaTecnica();
                                objItem.IDProduto = item.IDProduto;
                                objItem.Quantidade = item.Quantidade;
                                objFichaTecnica.ItemFichaTecnicas.Add(objItem);
                                break;
                            case 2:
                                objItem = objFichaTecnica.ItemFichaTecnicas.Where(i => i.ID == item.ID).Single();
                                objItem.IDProduto = item.IDProduto;
                                objItem.Quantidade = item.Quantidade;
                                break;
                            case 3:
                                if (item.ID > 0)
                                {
                                    objItem = objFichaTecnica.ItemFichaTecnicas.Where(i => i.ID == item.ID).Single();
                                    objFichaTecnica.ItemFichaTecnicas.Remove(objItem);
                                    db.ItemFichaTecnicas.DeleteOnSubmit(objItem);
                                }
                                break;
                        }
                    }
                }

                Modelo.EtapaProducao objEtapa;
                foreach (Modelo.Objeto.pxyEtapaProducao item in etapas)
                {
                    switch (item.Acao)
                    {
                        case 1:
                            objEtapa = new Modelo.EtapaProducao();
                            objEtapa.Sequencia = item.Sequencia;
                            objEtapa.IDProduto = objFichaTecnica.IDProduto;
                            objEtapa.IDServico = item.IDServico;
                            objEtapa.Titulo = item.Titulo;
                            objEtapa.Observacao = item.Observacao;
                            db.EtapaProducaos.InsertOnSubmit(objEtapa);
                            break;
                        case 2:
                            objEtapa = db.EtapaProducaos.Where(e => e.ID == item.ID).Single();
                            objEtapa.Sequencia = item.Sequencia;
                            objEtapa.IDServico = item.IDServico;
                            objEtapa.Titulo = item.Titulo;
                            objEtapa.Observacao = item.Observacao;
                            break;
                        case 3:
                            if (item.ID > 0)
                            {
                                objEtapa = db.EtapaProducaos.Where(e => e.ID == item.ID).Single();
                                db.EtapaProducaos.DeleteOnSubmit(objEtapa);
                            }
                            break;
                    }
                }

                db.SubmitChanges();
            }

            return ret;
        }

        private static void IncluiItens(List<Modelo.Objeto.pxyItemFichaTecnica> itens, Modelo.FichaTecnica objFichaNew)
        {
            Modelo.ItemFichaTecnica objItem;
            foreach (Modelo.Objeto.pxyItemFichaTecnica item in itens.Where(i=> i.Acao != 3))
            {
                objItem = new Modelo.ItemFichaTecnica();
                objItem.IDProduto = item.IDProduto;
                objItem.Quantidade = item.Quantidade;
                objFichaNew.ItemFichaTecnicas.Add(objItem);
            }
        }

        private static Dictionary<string, string> ValidaObjeto(Modelo.FichaTecnica objFichaTecnica)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objFichaTecnica.Versao <= 0)
            {
                ret.Add("txtVersao", "A versão deve ser maior do que zero(0).");
            }

            if (objFichaTecnica.IDProduto == 0)
            {
                ret.Add("lkProduto", "Selecione o produto.");
            }

            return ret;
        }
    }
}
