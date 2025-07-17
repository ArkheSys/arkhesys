using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class Fornecedor : Modelo.IMetodosCwork
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
            lista = from p in db.Pessoas
                    where p.bFornecedor == 1
                    join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    from xe in end.DefaultIfEmpty()
                    where xe.bPrincipal == 1
                    //join tel in db.Telefones on p.ID equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = xe.Cidade.Nome + " | " + xe.Cidade.UF.Sigla,
                        Telefone = xe.Numero,
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

            lista = from p in db.Pessoas
                    where p.bFornecedor == 1
                    join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    from xe in end.DefaultIfEmpty()
                    where xe.bPrincipal == 1
                    //join tel in db.Telefones on p.ID equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    where p.Codigo.ToString() == pFiltro ||
                    p.Nome.Contains(pFiltro) ||
                    p.CNPJ_CPF == pFiltro ||
                    xe.Numero.Contains(pFiltro) ||
                    xe.Cidade.Nome.Contains(pFiltro) ||
                    xe.Cidade.UF.Sigla.Contains(pFiltro)
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = xe.Cidade.Nome + " | " + xe.Cidade.UF.Sigla,
                        Telefone = xe.Numero,
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
            return (from p in db.Pessoas
                    where p.bFornecedor == 1
                    select new
                    {
                        Nome = p.Nome + " | " + p.Codigo,
                        ID = p.ID
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
            lista = from p in db.Pessoas
                    where p.bVendedor == 0 && p.bFuncionario == 0 && p.bFornecedor == 1
                    select new
                    {
                        Nome = p.Nome + " | " + p.Codigo,
                        ID = p.ID
                    };
            return lista;
        }


        /* public IQueryable getListaEnderecos(DataClassesDataContext db, int pID)
         {
             IQueryable lista;
             lista = from e in db.Enderecos
                     where e.IDPessoa == pID
                     select new
                     {
                         Rua = e.Rua,
                         Bairro = e.Bairro,
                         CEP = e.CEP,
                         Cidade = e.Cidade.Nome + " | " + e.Cidade.UF.Nome,
                         IDPessoa = e.IDPessoa,
                         Sequencia = e.Sequencia,
                         ID = e.ID
                     };
             return lista;
         }*/

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.Pessoas where u.ID == pID select u).Single();
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
                return (from u in db.Pessoas where u.Codigo.ToString() == pValor select u).Single().ID;
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
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.Pessoas
                             where p.Codigo == db.Pessoas.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public int QtdPessoaDemonstracao(DataClassesDataContext db)
        {
            int qtd = db.Pessoas.Count();

            return qtd;
        }
    }
}
