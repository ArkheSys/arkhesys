using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class Cliente : Modelo.IMetodosCwork
    {

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from c in db.Clientes
                    join p in db.Pessoas on c.IDPessoa equals p.ID
                    join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    from xe in end.DefaultIfEmpty()
                    where xe.bPrincipal == 1
                    //join t in db.Telefones on p.ID equals t.IDPessoa into tel
                    //from xt in tel.DefaultIfEmpty()
                    //where xt.bPrincipal == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = xe.Cidade == null ? "" : xe.Cidade.Nome + " | " + xe.Cidade.UF.Sigla,
                        Telefone = xe.Telefone,
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
        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;

            lista = from c in db.Clientes
                    join p in db.Pessoas on c.IDPessoa equals p.ID
                    where (p.Codigo.ToString() == pFiltro ||
                    p.Nome.Contains(pFiltro) ||
                    p.CNPJ_CPF == pFiltro) && p.bCliente == 1
                    join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    from xe in end.DefaultIfEmpty()
                    where xe.bPrincipal == 1
                    //join t in db.Telefones on p.ID equals t.IDPessoa into tel
                    //from xt in tel.DefaultIfEmpty()
                    //where xt.bPrincipal == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = xe.Cidade == null ? "" : xe.Cidade.Nome + " | " + xe.Cidade.UF.Sigla,
                        Telefone = xe.Telefone,
                        ID = p.ID
                    };

            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IList getListaCombo(DataClassesDataContext db)
        {
            IList lista;
            lista = (from p in db.Clientes
                     join c in db.Pessoas on p.IDPessoa equals c.ID
                     select new
                     {
                         Nome = c.Nome + " | " + c.Codigo,
                         ID = p.Pessoa.ID
                     }).OrderBy(o => o.Nome).ToList();
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.Clientes
                    where p.LimiteCredito.ToString() == pFiltro
                    select new
                    {
                        Nome = p.Pessoa.Nome + " | " + p.Pessoa.Codigo,
                        ID = p.Pessoa.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.Clientes where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.Clientes where u.Pessoa.Codigo.ToString() == pValor select u).Single().Pessoa.ID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
        }

        /// <summary>
        /// Método responsável para buscar as pessoas por Codigo, Nome, Fantasia, CNPJ, Telefone
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getBuscaPessoa(DataClassesDataContext db, string pValor)
        {
            IQueryable lista;

            lista = from p in db.Clientes
                    join end in db.Enderecos on p.IDPessoa equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    join tel in db.Telefones on p.IDPessoa equals tel.IDPessoa
                    where tel.bPrincipal.Value == 1
                    where p.Pessoa.Nome.Contains(pValor)
                    || p.Pessoa.Fantasia.Contains(pValor)
                    || p.Pessoa.CNPJ_CPF == pValor
                    || tel.Numero == pValor
                    select new
                    {
                        Codigo = p.Pessoa.Codigo,
                        Nome = p.Pessoa.Nome,
                        Fantasia = p.Pessoa.Fantasia,
                        CNPJ_CPF = p.Pessoa.CNPJ_CPF,
                        Cidade = end.Cidade == null ? "" : end.Cidade.Nome,
                        Estado = end.Cidade == null ? "" : (end.Cidade.UF == null ? "" : end.Cidade.UF.Sigla),
                        Telefone = tel.Numero,
                        Classificação = p.Pessoa.Classificacao == null ? "" : p.Pessoa.Classificacao.Descricao,
                        bCliente = (p.Pessoa.bCliente == 1 ? "Sim" : "Não"),
                        bFuncionario = (p.Pessoa.bFuncionario == 1 ? "Sim" : "Não"),
                        bVendedor = (p.Pessoa.bVendedor == 1 ? "Sim" : "Não"),
                        bFornecedor = (p.Pessoa.bFornecedor == 1 ? "Sim" : "Não"),
                        ID = p.Pessoa.ID
                    };

            return lista;
        }

        /// <summary>
        /// Método responsável para buscar as pessoas por Codigo, Nome, Fantasia, CNPJ, Telefone
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public int getQtdPessoa(DataClassesDataContext db, string pValor, out int pID)
        {
            List<Modelo.Pessoa> lista = new List<Modelo.Pessoa>();

            lista = (from p in db.Clientes
                     where p.Pessoa.Codigo.ToString() == pValor
                     select p.Pessoa).ToList();

            if (lista.Count() == 1)
                pID = lista[0].ID;
            else
            {
                lista = (from p in db.Clientes
                         join tel in db.Telefones on p.IDPessoa equals tel.IDPessoa
                         where tel.bPrincipal.Value == 1
                         where p.Pessoa.Codigo.ToString() == pValor
                         || p.Pessoa.Nome.Contains(pValor)
                         || p.Pessoa.Fantasia.Contains(pValor)
                         || p.Pessoa.CNPJ_CPF == pValor
                         || tel.Numero == pValor
                         select p.Pessoa).ToList();

                if (lista.Count() == 1)
                {
                    pID = lista[0].ID;
                }
                else
                {
                    pID = 0;
                }
            }

            return lista.Count();
        }

        public IQueryable getListaComboCliente(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = (from p in db.Clientes
                     join c in db.Pessoas on p.IDPessoa equals c.ID
                     select new
                     {
                         Nome = c.Nome + " | " + c.Codigo,
                         ID = p.ID
                     }).OrderBy(o => o.Nome);
            return lista;
        }

        public int QtdPessoaDemonstracao(DataClassesDataContext db)
        {
            int qtd = db.Pessoas.Count();

            return qtd;
        }

        public int GetCondicaoPgto(DataClassesDataContext db, int ID)
        {
            int? ret = null;
            var aux = (from c in db.Clientes
                       where c.IDPessoa == ID
                       select c.IDCondicao);
            if (aux.Count() > 0)
                ret = aux.First();
            return ret.HasValue ? ret.Value : 0;
        }
    }
}
