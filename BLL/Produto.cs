using Modelo.ModeloProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Produto : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;

        //public Produto()
        //{
        //    db = new Modelo.DataClassesDataContext();
        //    db.ObjectTrackingEnabled = false;
        //    db.DeferredLoadingEnabled = false;
        //}

        public Produto(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista()
        {
            IQueryable lista;

            lista = from p in db.Produtos
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        DescReduzida = p.DescReduzida,
                        Grupo = p.GrupoEstoque.Nome,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(String pFiltro)
        {
            IQueryable lista;

            lista = from p in db.Produtos
                    where (p.Codigo.ToString() == pFiltro ||
                    p.Nome.Contains(pFiltro) ||
                    p.DescReduzida.Contains(pFiltro))
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        DescReduzida = p.DescReduzida,
                        Unidade = p.Unidade.Sigla,
                        Grupo = p.GrupoEstoque.Nome,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método que pesquisa os produtos em uma View (v_Produtos)
        /// Esse método ordena os registros por uma coluna chamada NomeTrocado
        /// </summary>
        /// <param name="pFiltro">campo para pesquisa</param>
        /// <returns>Retorna uma lista com os produtos ordenados considerando a unidade de medida (Polegada)</returns>
        public List<pxyProduto> getListaView(String pFiltro)
        {
            string filtro = String.Empty;

            if (pFiltro != null)
            {
                if (pFiltro.Length > 0)
                {
                    if (pFiltro[0] == '%')
                    {
                        filtro = pFiltro.Substring(1);

                        return (from p in db.v_Produtos
                                where p.Codigo == filtro ||
                                p.Nome.Contains(filtro) ||
                                p.Barra.Contains(filtro) ||
                                p.BarraFornecedor.Contains(filtro)
                                select new pxyProduto
                                {
                                    Codigo = p.Codigo,
                                    Nome = p.Nome,
                                    DescReduzida = p.DescReduzida,
                                    Unidade = p.Unidade,
                                    Grupo = p.Grupo,
                                    NomeTrocado = p.NomeTrocado,
                                    ID = p.ID,
                                    Marcado = false,
                                    Barra = p.Barra,
                                    BarraFornecedor = p.BarraFornecedor
                                }).OrderBy(o => o.NomeTrocado).ToList();
                    }
                    filtro = pFiltro;
                }
            }
            return (from p in db.v_Produtos
                    where p.Nome.Substring(0, filtro.Length) == filtro
                    || p.Barra == filtro
                    select new pxyProduto
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        DescReduzida = p.DescReduzida,
                        Unidade = p.Unidade,
                        Grupo = p.Grupo,
                        NomeTrocado = p.NomeTrocado,
                        ID = p.ID,
                        Marcado = false,
                        Barra = p.Barra,
                        BarraFornecedor = p.BarraFornecedor
                    }).OrderBy(o => o.NomeTrocado).ToList();
        }

        public List<pxyProduto> getListaViewProduto(String pFiltro)
        {
            string filtro = String.Empty;

            if (pFiltro != null)
            {
                if (pFiltro.Length > 0)
                {
                    if (pFiltro[0] == '%')
                    {
                        filtro = pFiltro.Substring(1);

                        return (from p in db.v_Produtos
                                join g in db.GrupoEstoques on p.IDGrupoEstoque equals g.ID
                                where (p.Codigo == filtro ||
                                p.Nome.Contains(filtro) ||
                                p.Barra.Contains(filtro) ||
                                p.BarraFornecedor.Contains(filtro)) && (g.ClassificacaoProduto == Modelo.ClassificacaoGrupoEstoqueType.MatériaPrima ||
                                g.ClassificacaoProduto == Modelo.ClassificacaoGrupoEstoqueType.ProdutoAcabado)
                                select new pxyProduto
                                {
                                    Codigo = p.Codigo,
                                    Nome = p.Nome,
                                    DescReduzida = p.DescReduzida,
                                    Unidade = p.Unidade,
                                    Grupo = p.Grupo,
                                    NomeTrocado = p.NomeTrocado,
                                    ID = p.ID,
                                    Marcado = false,
                                    Barra = p.Barra,
                                    BarraFornecedor = p.BarraFornecedor
                                }).OrderBy(o => o.NomeTrocado).ToList();
                    }
                    filtro = pFiltro;
                }
            }
            return (from p in db.v_Produtos
                    join g in db.GrupoEstoques on p.IDGrupoEstoque equals g.ID
                    where p.Nome.Substring(0, filtro.Length) == filtro && (g.ClassificacaoProduto == Modelo.ClassificacaoGrupoEstoqueType.MatériaPrima ||
                    g.ClassificacaoProduto == Modelo.ClassificacaoGrupoEstoqueType.ProdutoAcabado)
                    select new pxyProduto
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        DescReduzida = p.DescReduzida,
                        Unidade = p.Unidade,
                        Grupo = p.Grupo,
                        NomeTrocado = p.NomeTrocado,
                        ID = p.ID,
                        Marcado = false,
                        Barra = p.Barra,
                        BarraFornecedor = p.BarraFornecedor
                    }).OrderBy(o => o.NomeTrocado).ToList();
        }

        public List<pxyProduto> getListaViewMateriais(String pFiltro)
        {
            string filtro = String.Empty;

            if (pFiltro != null)
            {
                if (pFiltro.Length > 0)
                {
                    if (pFiltro[0] == '%')
                    {
                        filtro = pFiltro.Substring(1);

                        return (from p in db.v_Produtos
                                join g in db.GrupoEstoques on p.IDGrupoEstoque equals g.ID
                                where (p.Codigo == filtro ||
                                p.Nome.Contains(filtro) ||
                                p.Barra.Contains(filtro) ||
                                p.BarraFornecedor.Contains(filtro)) && (g.ClassificacaoProduto == Modelo.ClassificacaoGrupoEstoqueType.Imobilizado)
                                select new pxyProduto
                                {
                                    Codigo = p.Codigo,
                                    Nome = p.Nome,
                                    DescReduzida = p.DescReduzida,
                                    Unidade = p.Unidade,
                                    Grupo = p.Grupo,
                                    NomeTrocado = p.NomeTrocado,
                                    ID = p.ID,
                                    Marcado = false,
                                    Barra = p.Barra,
                                    BarraFornecedor = p.BarraFornecedor
                                }).OrderBy(o => o.NomeTrocado).ToList();
                    }
                    filtro = pFiltro;
                }
            }
            return (from p in db.v_Produtos
                    join g in db.GrupoEstoques on p.IDGrupoEstoque equals g.ID
                    where p.Nome.Substring(0, filtro.Length) == filtro && (g.ClassificacaoProduto == Modelo.ClassificacaoGrupoEstoqueType.Imobilizado)
                    select new pxyProduto
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        DescReduzida = p.DescReduzida,
                        Unidade = p.Unidade,
                        Grupo = p.Grupo,
                        NomeTrocado = p.NomeTrocado,
                        ID = p.ID,
                        Marcado = false,
                        Barra = p.Barra,
                        BarraFornecedor = p.BarraFornecedor
                    }).OrderBy(o => o.NomeTrocado).ToList();
        }

        public List<pxyProduto> getListaPorCodigoStr(string pFiltro)
        {
            List<pxyProduto> lista;

            lista = (from p in db.v_Produtos
                     where p.Codigo.Contains(pFiltro)
                     select new pxyProduto
                     {
                         Codigo = p.Codigo,
                         Nome = p.Nome,
                         DescReduzida = p.DescReduzida,
                         Unidade = p.Unidade,
                         Grupo = p.Grupo,
                         NomeTrocado = p.NomeTrocado,
                         ID = p.ID,
                         Marcado = false,
                         Barra = p.Barra,
                         BarraFornecedor = p.BarraFornecedor
                     }).OrderBy(o => o.Codigo).ToList();

            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo()
        {
            IQueryable lista;

            lista = from p in db.Produtos
                    select new
                    {
                        Nome = p.Nome + " | " + p.Codigo,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(String pFiltro)
        {
            IQueryable lista;

            lista = from p in db.Produtos
                    where p.Codigo.ToString() == pFiltro ||
                    p.Nome.Contains(pFiltro) ||
                    p.DescReduzida.Contains(pFiltro)
                    select new
                    {
                        Nome = p.Nome + " | " + p.Codigo,
                        ID = p.ID
                    };

            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(int pID)
        {
            return (from p in db.Produtos where p.ID == pID select p).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from p in db.Produtos where p.Codigo.ToString() == pValor select p).Single().ID;
            }
            else
            {
                return 0;
            }
        }

        public string RetornaMAXCodigoStr()
        {
            string maxCodigo;
            try
            {
                maxCodigo = (db.Produtos.Max(b => Convert.ToInt32(b.Codigo)) + 1).ToString();
            }
            catch (Exception)
            {
                maxCodigo = "1";
            }
            return maxCodigo;
        }

        public int RetornaMAXCodigo()
        {
            return Convert.ToInt32(RetornaMAXCodigoStr());
        }

        public bool CodigoExiste(string codigo, int idProduto)
        {
            return db.Produtos.Where(p => p.Codigo == codigo && p.ID != idProduto).Count() > 0;
        }

        /// <summary>
        /// Método responsável para calcular o preço do produto
        /// </summary>
        /// <param name="db">Contexto a ser utilizado</param>
        /// <param name="pTabelaPreco">Tabela de Preço</param>
        /// <param name="pTipoPreco">Tipo do Preço desejado (Normal, Mínimo, Máximo)</param>
        /// <returns>Preço (Decimal)</returns>
        public Decimal getPreco(Modelo.Produto objProduto, int pTabelaPreco, Modelo.TipoPrecoType pTipoPreco)
        {
            return getPreco(objProduto, pTabelaPreco, pTipoPreco, null);
        }
        public Decimal getPreco(Modelo.Produto objProduto, int pTabelaPreco, Modelo.TipoPrecoType pTipoPreco, Modelo.Condicao condicao)
        {
            Decimal preco = 0;

            Modelo.TabelaPrecoProduto objTabelaPrecoProduto;

            try
            {
                objTabelaPrecoProduto = (db.ExecuteQuery<Modelo.TabelaPrecoProduto>("select tpp.* from TabelaPrecoProduto tpp inner join TabelaPreco tp on tp.id = tpp.IDTabelaPreco inner join Produto p on p.ID = tpp.IDProduto where p.Codigo = {0} and tp.Codigo = {1}", objProduto.Codigo, pTabelaPreco)).Single();

                if (pTipoPreco == Modelo.TipoPrecoType.Normal)
                {
                    preco = (decimal)objProduto.PrecoBase + (((decimal)objProduto.PrecoBase * (decimal)objTabelaPrecoProduto.MargemLucro) / 100);
                }
                else
                {
                    if (pTipoPreco == Modelo.TipoPrecoType.Mínimo)
                    {
                        preco = getPreco(objProduto, pTabelaPreco, Modelo.TipoPrecoType.Normal) - ((getPreco(objProduto, pTabelaPreco, Modelo.TipoPrecoType.Normal) * (decimal)objTabelaPrecoProduto.PDesconto) / 100);
                    }
                    else
                    {
                        if (pTipoPreco == Modelo.TipoPrecoType.Máximo)
                        {
                            preco = getPreco(objProduto, pTabelaPreco, Modelo.TipoPrecoType.Normal) - ((getPreco(objProduto, pTabelaPreco, Modelo.TipoPrecoType.Normal) * (decimal)objTabelaPrecoProduto.PAcrescimo) / 100);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("O produto não está configurado na tabela de preço.");
                //string a = ex.Message;
                //preco = 0;
            }
            //#594b
            if (condicao != null && pTipoPreco == Modelo.TipoPrecoType.Normal)
            {
                preco = preco * (100 + condicao.Variacao) / 100;
            }
            //#594e
            return preco;
        }

        /// <summary>
        /// Método responsável para calcular o preço do produto
        /// </summary>
        /// <param name="db">Contexto a ser utilizado</param>
        /// <param name="pTabelaPreco">Tabela de Preço</param>
        /// <param name="pTipoPreco">Tipo do Preço desejado (Normal, Mínimo, Máximo)</param>
        /// <returns>Preço (Decimal)</returns>
        public Decimal getPreco(Modelo.Produto objProduto, Decimal pMargemLucro, Decimal pAcrescimo, Decimal pDesconto, Modelo.TipoPrecoType pTipoPreco)
        {
            Decimal preco = 0;

            try
            {
                if (pTipoPreco == Modelo.TipoPrecoType.Normal)
                {
                    preco = (decimal)objProduto.PrecoBase + ((decimal)objProduto.PrecoBase * (pMargemLucro) / 100);
                }
                else
                {
                    if (pTipoPreco == Modelo.TipoPrecoType.Mínimo)
                    {
                        preco = getPreco(objProduto, pMargemLucro, pAcrescimo, pDesconto, Modelo.TipoPrecoType.Normal) - ((getPreco(objProduto, pMargemLucro, pAcrescimo, pDesconto, Modelo.TipoPrecoType.Normal) * pDesconto) / 100);
                    }
                    else
                    {
                        if (pTipoPreco == Modelo.TipoPrecoType.Máximo)
                        {
                            preco = getPreco(objProduto, pMargemLucro, pAcrescimo, pDesconto, Modelo.TipoPrecoType.Normal) - ((getPreco(objProduto, pMargemLucro, pAcrescimo, pDesconto, Modelo.TipoPrecoType.Normal) * pAcrescimo) / 100);
                        }
                    }
                }
            }
            catch (Exception)
            {
                preco = 0;
            }

            return preco;
        }

        /// <summary>
        /// Metodo que retorno o valor do preço base
        /// </summary>
        /// <returns>Preço Base (Decimal)</returns>
        public Decimal getPrecoBase(Modelo.Produto objProduto)
        {
            Decimal preco = 0;
            Decimal frete = 0;
            Decimal ipi = 0;
            Decimal custoex = 0;

            preco = (decimal)objProduto.PrecoFornecedor - ((decimal)objProduto.PrecoFornecedor * (decimal)objProduto.DescontoFornecedor / 100);
            frete = ((preco * (decimal)objProduto.CustoFrete) / 100);
            ipi = ((preco * (decimal)objProduto.CustoIPI) / 100);
            custoex = ((preco * (decimal)objProduto.CustoExtra) / 100);

            preco = preco + frete + ipi + custoex;

            return preco;
        }

        public List<string> verificaTabPreco(Modelo.Pedido pPedido, List<Modelo.ItensOrcamento> itens)
        {
            List<string> lista = new List<string>();
            bool contem;

            List<string> codigos = (from c in itens select c.Produto).ToList();

            var listaIDProdutos = (from p in db.Produtos
                                   where codigos.Contains(p.Codigo)
                                   select new
                                   {
                                       ID = p.ID,
                                       Nome = p.Nome,
                                       Codigo = p.Codigo
                                   });

            foreach (var i in listaIDProdutos)
            {
                contem = db.TabelaPrecoProdutos.Where(t => t.IDProduto == i.ID && t.IDTabelaPreco == pPedido.TabelaPreco.ID).Count() > 0;
                if (!contem)
                {
                    lista.Add("Produto: " + i.Codigo.ToString() + " - " + i.Nome);
                }
            }
            return lista;
        }

        public List<string> verificaTabPreco(Modelo.Nota pNota)
        {
            List<string> lista = new List<string>();
            bool contem;

            List<int> IDs = (from c in pNota.NotaItems select c.IDProduto).ToList();

            var listaIDProdutos = (from p in db.Produtos
                                   where IDs.Contains(p.ID)
                                   select new
                                   {
                                       ID = p.ID,
                                       Nome = p.Nome,
                                       Codigo = p.Codigo
                                   });

            foreach (var i in listaIDProdutos)
            {
                contem = db.TabelaPrecoProdutos.Where(t => t.IDProduto == i.ID && t.IDTabelaPreco == pNota.TabelaPreco.ID).Count() > 0;
                if (!contem)
                {
                    lista.Add("Produto: " + i.Codigo.ToString() + " - " + i.Nome);
                }
            }

            return lista;
        }

        public int QtdProdutoDemonstracao()
        {
            int qtd = db.Produtos.Count();

            return qtd;
        }
    }
}