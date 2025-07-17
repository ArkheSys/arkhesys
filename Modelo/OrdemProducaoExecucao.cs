using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Modelo
{
    public partial class OrdemProducaoExecucao
    {
        public IQueryable getListaGrid(Modelo.DataClassesDataContext db, int pIdOrdemProducao)
        {
            //DataTable dt = new DataTable();
            IQueryable lista = from OSExecucao in db.OrdemProducaoExecucaos
                               join Servico in db.Servicos on OSExecucao.IdServico equals Servico.id
                               join OS in db.OrdemProducaos on OSExecucao.IdOrdemProducao equals OS.ID
                               where OS.ID == pIdOrdemProducao
                               select new
                               {
                                   Sequencia = OSExecucao.Sequencia,
                                   Descricao = OSExecucao.Descricao,
                                   Servico = Servico.nome,
                                   Funcionario = OSExecucao.IdFuncionario,
                                   Quantidade = OSExecucao.Quantidade,
                                   ID = OSExecucao.ID,
                                   OrdemProducao = OS.Codigo,
                                   DataInicio = OSExecucao.DataInicio,
                                   DataTermino = OSExecucao.DataTermino
                               };
            return lista;
        }

        public List<Modelo.Objeto.pxyOrdemProducaoExecucaoList> getLista(Modelo.DataClassesDataContext db, int pIdOrdemProducao)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("SELECT");
            str.AppendLine("OrdemProducaoExecucao.Sequencia");
            str.AppendLine(", OrdemProducaoExecucao.Descricao");
            str.AppendLine(", Servico.nome AS Servico");
            str.AppendLine(", Pessoa.Nome AS Funcionario");
            str.AppendLine(", OrdemProducaoExecucao.Quantidade");
            str.AppendLine(", OrdemProducaoExecucao.ID");
            str.AppendLine(", OrdemProducao.Codigo AS OrdemProducao");
            str.AppendLine(", OrdemProducaoExecucao.DataInicio");
            str.AppendLine(", OrdemProducaoExecucao.DataTermino");
            str.AppendLine("FROM OrdemProducaoExecucao");
            str.AppendLine("INNER JOIN OrdemProducao ON OrdemProducao.ID = OrdemProducaoExecucao.IdOrdemProducao");
            str.AppendLine("INNER JOIN Servico ON Servico.ID = OrdemProducaoExecucao.IdServico");
            str.AppendLine("LEFT JOIN PessoaFuncionario ON PessoaFuncionario.ID = OrdemProducaoExecucao.IdFuncionario");
            str.AppendLine("LEFT JOIN Pessoa ON Pessoa.ID = PessoaFuncionario.IDPessoa");
            str.AppendLine("WHERE OrdemProducao.ID = " + pIdOrdemProducao.ToString());

            return db.ExecuteQuery<Modelo.Objeto.pxyOrdemProducaoExecucaoList>(str.ToString(), new object[0]).ToList();
        }

        public string getNomeFuncionario(DataClassesDataContext db, int pIdFuncionario)
        {
            PropertyInfo propriedade;                
            Object nome = (from Func in db.Funcionarios
                             join pes in db.Pessoas on Func.IDPessoa equals pes.ID
                             where Func.ID == pIdFuncionario
                             select new
                             {
                                 Nome = pes.Nome
                             }).First();
            
            Type tiponome = nome.GetType();
            propriedade = tiponome.GetProperty("Nome");
            return propriedade.GetValue(nome, null).ToString();                        
        }


        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(Modelo.DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.OrdemProducaoExecucaos
                    where p.IdOrdemProducao.ToString() == pFiltro ||
                    p.IdOrdemProducao.ToString().Contains(pFiltro)
                    select new
                    {
                        Sequencia = p.Sequencia,
                        Descricao = p.Descricao,
                        Servico = p.IdServico,
                        Funcionario = p.IdFuncionario,
                        OrdemProducao = p.IdOrdemProducao,
                        Quantidade = p.Quantidade,
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
        public IQueryable getListaCombo(Modelo.DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.OrdemProducaoExecucaos
                    select new
                    {
                        Nome = " | " + p.IdOrdemProducao,
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
        public IQueryable getListaCombo(Modelo.DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.OrdemProducaoExecucaos
                    where p.IdOrdemProducao.ToString() == pFiltro ||
                    p.IdOrdemProducao.ToString().Contains(pFiltro)
                    select new
                    {
                        Nome = " | " + p.IdOrdemProducao,
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
        public Object getObjeto(Modelo.DataClassesDataContext db, int pID)
        {
            return (from u in db.OrdemProducaoExecucaos where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(Modelo.DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.OrdemProducaoExecucaos where u.IdOrdemProducao.ToString() == pValor select u).Single().ID;
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
        public Int32 RetornaMAXCodigo(Modelo.DataClassesDataContext db, int pIdOrdemProducao)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.OrdemProducaoExecucaos
                             where p.Sequencia == db.OrdemProducaoExecucaos.Max(b => b.Sequencia)
                             && p.IdOrdemProducao == pIdOrdemProducao
                             select p).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
    }
}
