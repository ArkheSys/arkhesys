using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using System.Linq.Expressions;

namespace BLL
{
    public class Pessoa : IDisposable, InterfaceBLL
    {
        private Modelo.DataClassesDataContext db;

        public Pessoa()
        {
            db = new Modelo.DataClassesDataContext();
            //db.ObjectTrackingEnabled = false;
            //db.DeferredLoadingEnabled = false;
        }

        public Pessoa(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public void Dispose()
        {
            db.Dispose();
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
            lista = from p in db.Pessoas
                    join end in db.Enderecos on p.ID equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    //join tel in db.Telefones on p.ID equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = end.Cidade.Nome + " | " + end.Cidade.UF.Sigla,
                        Telefone = end.Numero,
                        Classificação = p.Classificacao.Descricao,
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

            lista = from p in db.Pessoas
                    join end in db.Enderecos on p.ID equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    //join tel in db.Telefones on p.ID equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    where p.Codigo.ToString() == pFiltro ||
                    p.Nome.Contains(pFiltro) ||
                    p.CNPJ_CPF == pFiltro ||
                    end.Numero.Contains(pFiltro) ||
                    end.Cidade.Nome.Contains(pFiltro) ||
                    end.Cidade.UF.Sigla.Contains(pFiltro)
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = end.Cidade.Nome + " | " + end.Cidade.UF.Sigla,
                        Telefone = end.Numero,
                        Classificação = p.Classificacao.Descricao,
                        ID = p.ID
                    };
            return lista;
        }

        public IQueryable getLista(bool bCliente, bool bFuncionario, bool bVendedor, bool bFornecedor)
        {
            var predicate = Modelo.PredicateBuilder.False<Modelo.Pessoa>();
            if (bCliente)
                predicate = predicate.Or(p => p.bCliente == 1);
            if (bFuncionario)
                predicate = predicate.Or(p => p.bFuncionario == 1);
            if (bVendedor)
                predicate = predicate.Or(p => p.bVendedor == 1);
            if (bFornecedor)
                predicate = predicate.Or(p => p.bFornecedor == 1);

            var query = (from p in db.Pessoas.Where(predicate)
                         join e in db.Enderecos on p.ID equals e.IDPessoa into end
                         from xe in end.DefaultIfEmpty()
                         where xe.bPrincipal == 1
                        select new
                        {
                            Codigo = p.Codigo,
                            Nome = p.Nome,
                            Fantasia = p.Fantasia,
                            CNPJ_CPF = p.CNPJ_CPF,
                            Cidade = xe.Cidade.Nome,
                            Estado = xe.Cidade.UF.Sigla,
                            Telefone = xe.Numero,
                            Classificação = p.Classificacao.Descricao,
                            bCliente = (p.bCliente == 1 ? "Sim" : "Não"),
                            bFuncionario = (p.bFuncionario == 1 ? "Sim" : "Não"),
                            bVendedor = (p.bVendedor == 1 ? "Sim" : "Não"),
                            bFornecedor = (p.bFornecedor == 1 ? "Sim" : "Não"),
                            NomeVendedor = (p.bCliente == 1 ? p.Clientes.First().Vendedor.Nome : String.Empty),
                            ID = p.ID
                        });

            return query;
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
            lista = from p in db.Pessoas
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
            lista = from p in db.Pessoas
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
            return (from u in db.Pessoas where u.ID == pID select u).Single();
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
        public Int32 RetornaMAXCodigo()
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

        public int getQtdFornecedores(DataClassesDataContext db, string pValor, out int pID)
        {
            List<Modelo.Pessoa> lista = new List<Modelo.Pessoa>();

            lista = (from p in db.Pessoas
                     where p.Codigo.ToString() == pValor
                     && p.bFornecedor == 1
                     select p).ToList();

            if (lista.Count() == 1)
                pID = lista[0].ID;
            else
            {
                lista = (from p in db.Pessoas
                         join tel in db.Telefones on p.ID equals tel.IDPessoa
                         where tel.bPrincipal.Value == 1
                         where p.Codigo.ToString() == pValor
                         || p.Nome.Contains(pValor)
                         || p.Fantasia.Contains(pValor)
                         || p.CNPJ_CPF == pValor
                         || tel.Numero == pValor
                         select p).ToList();

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

        public bool getPessoaAtiva(DataClassesDataContext db, int pIDPessoa)
        {
            byte? ret = (from p in db.Pessoas
                         where p.ID == pIDPessoa
                         select p).Single().bAtivo;

            return ret == null ? false : (ret == 0 ? false : true);
        }

        public bool ClienteLiberado(DataClassesDataContext db, int iDPessoa, bool permitirFatClienteNaoAtivo, out string mensagem)
        {
            var pes = (from p in db.Pessoas
                       where p.ID == iDPessoa
                       select p).Single();
            if (pes.Clientes.Count == 0)
            {
                mensagem = "A pessoa selecionada não é um cliente.";
                return false;
            }

            if (pes.bAtivo != 1)
            {
                if (permitirFatClienteNaoAtivo)
                {
                    if (pes.Clientes.First().IDCondicao > 0)
                    {
                        mensagem = String.Empty;
                        return true;
                    }
                    mensagem = "O cliente está inativo e não possui uma condição de pagamento definida. Para realizar essa operação ative o cliente ou defina uma condição de pagamento para ele.";
                    return false;
                }
                mensagem = "Essa operação não é permitida para clientes inativos.";
                return false;
            }
            mensagem = String.Empty;
            return true;
        }

        public List<Modelo.Objeto.PxyPessoaDoc> GetAtributos(Int32 ID)
        {
            List<Modelo.Objeto.PxyPessoaDoc> Lista = new List<Modelo.Objeto.PxyPessoaDoc>();

            Lista = (from p in db.Pessoas
                     join d in db.Documentos on p.ID equals d.IDPessoa
                     where p.ID == ID
                     select new Modelo.Objeto.PxyPessoaDoc
                     {
                         ID = p.ID,
                         Codigo = d.Codigo,
                         Tipo = d.Tipo,
                         valor = d.Valor,
                         Vencimento = d.DtVencimento,
                         NumDoc = d.NumDocumento
                     }).ToList();

            return Lista;
        }
    }
}
