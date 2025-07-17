using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Modelo
{
    public partial class Cotacao : Modelo.IMetodosCwork
    {
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.Cotacaos where u.ID == pID select u).Single();
        }

        public IQueryable getListaComboProd(DataClassesDataContext db, List<string> listaProd)
        {
            IQueryable lista;
            lista = from prod in db.Produtos 
                    where !listaProd.Contains(prod.Nome)
                    select new
                    {
                        Nome = prod.Nome,
                        ID = prod.ID
                    };
            return lista;
        }

        public IQueryable getListaComboProdAlterar(DataClassesDataContext db, List<string> listaProd)
        {
            IQueryable lista;
            lista = from prod in db.Produtos
                    select new
                    {
                        Nome = prod.Nome,
                        ID = prod.ID
                    };
            return lista;
        }

        public object getID(DataClassesDataContext db, int pValor)
        {
            if (pValor != 0)
            {
            return (from u in db.Produtos where u.ID == pValor select u).Single();
            }
            else
            {
                return null;
            }
        }

        public IQueryable getListaComboForn(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from f in db.Pessoas where f.bFornecedor == 1
                    select new
                    {
                        Nome = f.Nome,
                        ID = f.ID
                    };
            return lista;
        }

        public System.Collections.IList getListaComboForn(DataClassesDataContext db, int[] array )
        {
            System.Collections.IList lista;
            lista = (from f in db.Pessoas
                    where f.bFornecedor == 1
                    && !array.Contains(f.ID)
                    select new
                    {
                        Nome = f.Nome,
                        ID = f.ID
                    }).ToList();
            return lista;
        }

        public System.Collections.IList getFornecedores(DataClassesDataContext db, Modelo.Cotacao pobjCotacao)
        {
            System.Collections.IList lista;

                lista = (from f in db.Pessoas
                         where f.ID == pobjCotacao.IDFornecedor_01 || f.ID == pobjCotacao.IDFornecedor_02 ||
                         f.ID == pobjCotacao.IDFornecedor_03 || f.ID == pobjCotacao.IDFornecedor_04 ||
                         f.ID == pobjCotacao.IDFornecedor_05 || f.ID == pobjCotacao.IDFornecedor_06
                         select new {f.Nome}).ToList();
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, string pFiltro)
        {
            IQueryable lista;
            lista = from c in db.Cotacaos 
                    where c.Resumo.Contains(pFiltro)
                    select new
                    {
                        Codigo = c.Codigo,
                        Resumo = c.Resumo,
                        ID = c.ID,
                        TipoCotacao = c.TipoCotacao
                    };
            return lista;
        }

        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from p in db.Cotacaos
                             where p.Codigo == db.Cotacaos.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public int RetornaMAXCodigo(Modelo.Cotacao cotacao)
        {
            int maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from p in cotacao.CotacaoProdutos
                                  where p.Codigo == cotacao.CotacaoProdutos.Max(b => b.Codigo)
                                  select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
                
        public IQueryable getProdutos(DataClassesDataContext db, int ID)
        {
            IQueryable lista;
            lista = from p in db.CotacaoProdutos
                    where p.IDCotacao == ID
                    select new 
                    {
                        ID = p.ID,
                        Codigo = p.Codigo,
                        DescricaoProduto = p.DescricaoProduto,
                        Quantidade = p.Quantidade,
                        Produto = p.Produto.Codigo
                    };
            return lista;
        }

        public List<Modelo.CotacaoProduto> getProdutosPrecos(DataClassesDataContext db, int pID) 
        {
            List<Modelo.CotacaoProduto> lista;

            lista = (from p in db.CotacaoProdutos
                     where p.IDCotacao == pID
                     select p).ToList();

            return lista;
        }

        public System.Collections.IList GetProdutosList(DataClassesDataContext db, int ID)
        {
            System.Collections.IList lista;
            lista = (from p in db.CotacaoProdutos
                          where p.IDCotacao == ID
                          select new 
                          {
                              ID = p.ID,
                              Codigo = p.Codigo,
                              DescricaoProduto = p.DescricaoProduto,
                              Quantidade = p.Quantidade,
                              Produto = p.Produto
                          }).ToList();
            return lista;
        }

        public IQueryable getListaCondicao(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from c in db.Condicaos
                    select new
                    {
                        Nome = c.Nome,
                        ID = c.ID
                    };
            return lista;
        }

        public IQueryable getListaFiliais(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from c in db.Filials
                    select new
                    {
                        Nome = c.Nome,
                        ID = c.ID
                    };
            return lista;
        }

        public object GetIdListaCotacao(DataClassesDataContext db) 
        {
            return (from c in db.Cotacaos where c.TipoCotacao == 0 select c.ID).Single();
        }

        public List<Modelo.Objeto.pxyMontaCotacao> GetProdutosLista(DataClassesDataContext db)
        {
            List<Modelo.Objeto.pxyMontaCotacao> lista = new List<Modelo.Objeto.pxyMontaCotacao>();

            try
            {
                lista = (from c in db.CotacaoProdutos
                         where c.Cotacao.TipoCotacao == 0
                         select new Modelo.Objeto.pxyMontaCotacao
                         {
                             ID = c.ID,
                             Codigo = (int)c.Codigo,
                             Produto = c.Produto.Nome,
                             IDProduto = (int)c.IDProduto,
                             Quantidade = (int)c.Quantidade,
                             IDCotacao = (int)c.IDCotacao,
                             IDcot = (int)c.Cotacao.ID
                         }).ToList<Modelo.Objeto.pxyMontaCotacao>();
                return lista;
            }
            catch
            {
                return lista = new List<Modelo.Objeto.pxyMontaCotacao>();
            }
        }

        public IQueryable GetCotcaoes(DataClassesDataContext db)
        {
            IQueryable lista;

            lista = from c in db.Cotacaos
                    where c.TipoCotacao != 0
                    select new
                    {
                        ID = c.ID,
                        Nome = c.Resumo
                    };
            return lista;
        }

        public List<Modelo.Objeto.pxyMontaCotacao> GetProdListaCotacao (DataClassesDataContext db, int ID)
        {
            List<Modelo.Objeto.pxyMontaCotacao> lista;
            lista = (from c in db.CotacaoProdutos
                     where c.IDCotacao == ID
                     select new Modelo.Objeto.pxyMontaCotacao
                     {
                         ID = c.ID,
                         Codigo = (int)c.Codigo,
                         Produto = c.Produto.Nome,
                         IDProduto = (int)c.IDProduto,
                         Quantidade = (int)c.Quantidade
                     }).ToList<Modelo.Objeto.pxyMontaCotacao>();

            return lista;
        }

        public List<Modelo.CotacaoProduto> GetQtdProdutosCotacaoLista(DataClassesDataContext db, int ID) 
        {
            List<Modelo.CotacaoProduto> obj = new List<Modelo.CotacaoProduto>();
            try
            {
                obj = (from c in db.CotacaoProdutos
                       where c.IDCotacao == ID
                       select c).ToList<Modelo.CotacaoProduto>();
                return obj;
            }
            catch 
            {
                return obj;
            }
            
        }

        public object GetIDCotLista(DataClassesDataContext db)
        {
            return (from c in db.Cotacaos where c.TipoCotacao == 0 select c.ID).Single();
        }

        public List<int> GetlistIDs(DataClassesDataContext db) 
        {
            List<int> lista;

            lista = (from c in db.CotacaoProdutos
                     select (int)c.IDProduto).ToList();
            return lista;
            
        }

        public List<Modelo.CotacaoProduto> getobjs(DataClassesDataContext db)
        {
            List<Modelo.CotacaoProduto> a;
            try
            {
                a = (from c in db.CotacaoProdutos
                     where c.IDCotacao == null && c.IDProduto == null
                     select c).ToList<Modelo.CotacaoProduto>();
                return a;
            }
            catch
            {
                return a = new List<CotacaoProduto>();
            }
        }

        //public List<Modelo.Objeto.pxyMontaCotacao> GetProdutosLista2(DataClassesDataContext db)
        //{
        //    List<Modelo.Objeto.pxyMontaCotacao> lista = new List<Modelo.Objeto.pxyMontaCotacao>();

        //    try
        //    {
        //        lista = (from c in db.Cotacaos
        //                 where c.TipoCotacao == 0
        //                 join d in db.CotacaoProdutos on c.ID equals d.IDCotacao
        //                 select new Modelo.Objeto.pxyMontaCotacao
        //                 {
        //                     ID = d.ID,
        //                     Codigo = (int)d.Codigo,
        //                     Produto = d.Produto.Nome,
        //                     IDProduto = (int)d.IDProduto,
        //                     Quantidade = (int)d.Quantidade,
        //                     IDCotacao = (int)d.IDCotacao,
        //                     IDcot = (int)c.ID
        //                 }).ToList<Modelo.Objeto.pxyMontaCotacao>();
        //        return lista;
        //    }
        //    catch
        //    {
        //        return lista = new List<Modelo.Objeto.pxyMontaCotacao>();
        //    }
        //}

        #region IMetodosCwork Members

        public IList getListaCombo(DataClassesDataContext db) 
        {
            throw new NotImplementedException();
        }

        public IQueryable getLista(DataClassesDataContext db)
        {
            throw new NotImplementedException();
        }

        public IQueryable getListaCombo(DataClassesDataContext db, string pFiltro)
        {
            throw new NotImplementedException();
        }

        public int getID(DataClassesDataContext db, string pValor)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
