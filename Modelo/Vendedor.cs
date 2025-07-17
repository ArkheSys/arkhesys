using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class Vendedor : Modelo.IMetodosCwork
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
            lista = from v in db.Vendedors
                    join end in db.Enderecos on v.IDPessoa equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    
                    select new
                    {
                        Codigo = v.Pessoa.Codigo,
                        Nome = v.Pessoa.Nome,
                        CPF = v.Pessoa.CNPJ_CPF,
                        Cidade = end.Cidade.Nome + " | " + end.Cidade.UF.Sigla,
                        ID = v.ID,
                        IDP = v.IDPessoa
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
            lista = from v in db.Vendedors
                    join end in db.Enderecos on v.IDPessoa equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    //join tel in db.Telefones on f.IDPessoa equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    where v.Pessoa.Codigo.ToString() == pFiltro ||
                    v.Pessoa.Nome.Contains(pFiltro) ||
                    v.Pessoa.CNPJ_CPF == pFiltro ||
                    v.ComissaoVendedor.ToString() == pFiltro ||
                    end.Cidade.Nome.Contains(pFiltro) ||
                    end.Cidade.UF.Sigla.Contains(pFiltro)
                    select new
                    {
                        Codigo = v.Pessoa.Codigo,
                        Nome = v.Pessoa.Nome,
                        CPF = v.Pessoa.CNPJ_CPF,
                        Cidade = end.Cidade.Nome + " | " + end.Cidade.UF.Sigla,
                        //Telefone = tel.Numero,
                        ID = v.ID,
                        IDP = v.IDPessoa
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
            return (from v in db.Vendedors
                    where v.ComissaoVendedor != null || v.Senha != null || v.Pessoa.bVendedor == 1
                    orderby v.Pessoa.Nome
                    select new
                    {
                        Nome = String.IsNullOrEmpty(v.Pessoa.Apelido) ? v.Pessoa.Nome : v.Pessoa.Apelido,
                        Codigo = v.Pessoa.Codigo,
                        ID = v.IDPessoa
                    }).ToList();
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
            lista = from v in db.Vendedors
                    where v.Pessoa.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = v.Pessoa.Nome,
                        ID = v.IDPessoa
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
            return (from u in db.Vendedors where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public int RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
        }

        /// <summary>
        /// Método responsável para buscar as Vendedores por Codigo, Nome
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getBuscaComposta(DataClassesDataContext db, string pValor)
        {
            IQueryable lista;

            lista = from p in db.Vendedors
                    where p.Pessoa.Nome.Contains(pValor)
                    select new
                    {
                        Codigo = p.Pessoa.Codigo,
                        Nome = p.Pessoa.Apelido,
                        ID = p.Pessoa.ID
                    };

            return lista;
        }

        /// <summary>
        /// Método responsável para buscar as pessoas por Codigo, Nome, Fantasia, CNPJ, Telefone
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public int getQtdBuscaComposta(DataClassesDataContext db, string pValor, out int pID)
        {
            List<Modelo.Pessoa> lista = new List<Modelo.Pessoa>();

            lista = (from p in db.Vendedors
                     where p.Pessoa.Codigo.ToString() == pValor
                     select p.Pessoa).ToList();

            if (lista.Count() == 1)
                pID = lista[0].ID;
            else
            {
                lista = (from p in db.Vendedors
                         where p.Pessoa.Codigo.ToString() == pValor
                         || p.Pessoa.Apelido.Contains(pValor)
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

        public int QtdPessoaDemonstracao(DataClassesDataContext db)
        {
            int qtd = db.Pessoas.Count();

            return qtd;
        }
    }
}
