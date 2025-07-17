using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class Funcionario : Modelo.IMetodosCwork
    {

        public static int GetID(DataClassesDataContext db, int idPessoa)
        {
            var aux = from f in db.Funcionarios
                      where f.IDPessoa == idPessoa
                      select f.ID;

            if (aux.Count() > 0)
                return aux.First();
            return 0;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from f in db.Funcionarios
                    join end in db.Enderecos on f.IDPessoa equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    //join tel in db.Telefones on f.IDPessoa equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    select new
                    {
                        Codigo = f.Pessoa.Codigo,
                        Nome = f.Pessoa.Nome,
                        CPF = f.Pessoa.CNPJ_CPF,
                        Cidade = end.Cidade.Nome + " | " + end.Cidade.UF.Sigla,
                        //Telefone = tel.Numero,
                        ID = f.ID,
                        IDP = f.IDPessoa
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
            lista = from f in db.Funcionarios
                    join end in db.Enderecos on f.IDPessoa equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    join tel in db.Telefones on f.IDPessoa equals tel.IDPessoa
                    where tel.bPrincipal.Value == 1
                    where (f.Pessoa.Codigo.ToString() == pFiltro ||
                    f.Pessoa.Nome.Contains(pFiltro) ||
                    f.Pessoa.CNPJ_CPF == pFiltro ||
                    //f.NumCarteiraTrab.Contains(pFiltro) ||
                    end.Cidade.Nome.Contains(pFiltro) ||
                    end.Cidade.UF.Sigla.Contains(pFiltro)) &&
                    f.Pessoa.bFuncionario == 1
                    select new
                    {
                        Codigo = f.Pessoa.Codigo,
                        Nome = f.Pessoa.Nome,
                        CPF = f.Pessoa.CNPJ_CPF,
                        Cidade = end.Cidade.Nome + " | " + end.Cidade.UF.Sigla,
                        Telefone = tel.Numero,
                        ID = f.ID,
                        IDP = f.IDPessoa
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
            return (from f in db.Funcionarios
                    select new
                    {
                        Nome = f.Pessoa.Nome,
                        ID = f.ID
                    }).ToList();
        }

        /// <summary>
        /// Retorna uma lista para ser utilizada em Combobox
        /// </summary>
        /// <param name="db">Contexto utilizado no Formulário ou  Grid</param>
        /// <param name="pFiltro">Filtro para pesquisa dos registros</param>
        /// <returns>Retorna a lista de registros (IQueryable)</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from f in db.Funcionarios
                    where NumCarteiraTrab.Contains(pFiltro) ||
                    f.Pessoa.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = f.Pessoa.Nome,
                        ID = f.ID
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
            return (from u in db.Funcionarios where u.ID == pID select u).Single();
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
