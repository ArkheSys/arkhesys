using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections;

namespace Modelo
{
    public partial class Projeto : IMetodosCwork
    {
        public int? Status_Ant;

        partial void OnLoaded()
        {
            Status_Ant = Status;
        }

        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from e in db.Projetos
                    select new
                    {
                        Nome = e.Nome,
                        Descricao = e.Descricao,
                        ID = e.ID
                    };
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from e in db.Projetos
                    where e.Nome.Contains(pFiltro) && e.IDPessoa != 0
                    select new
                    {
                        Cli = e.Pessoa.Nome,
                        Data = e.Data == null ? null : e.Data.Value.ToShortDateString(),
                        Status = NomeStatus(e.Status),
                        Nome = e.Nome,
                        TotalProdutos = e.TotalProdutos,
                        TotalServicos = e.TotalServicos,
                        TotalGeral = e.TotalGeral,
                        ID = e.ID
                    };

            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, int idPessoa)
        {
            IQueryable lista;
            lista = from e in db.Projetos
                    where e.Pessoa.ID == idPessoa && e.IDPessoa != 0
                    select new
                    {
                        Cli = e.Pessoa.Nome,
                        Data = e.Data == null ? null : e.Data.Value.ToShortDateString(),
                        Status = NomeStatus(e.Status),
                        Nome = e.Nome,
                        TotalProdutos = e.TotalProdutos,
                        TotalServicos = e.TotalServicos,
                        TotalGeral = e.TotalGeral,
                        ID = e.ID
                    };

            return lista;
        }

        public IQueryable getListaProjetos(DataClassesDataContext db, int pTipo)
        {
            List<int> Tipo = new List<int>();
            switch (pTipo)
            {
                case 1:
                    Tipo.Add(0);
                    Tipo.Add(1);
                    Tipo.Add(2);
                    break;
                case 2:
                    Tipo.Add(3);
                    Tipo.Add(4);
                    break;
                case 3:
                    Tipo.Add(5);
                    break;
                case 4:
                    Tipo.Add(2);
                    break;
                default:
                    break;
            }
            IQueryable lista;
            lista = from e in db.Projetos
                    where e.Data != null
                    && (Tipo.Count > 0 ? Tipo.Contains(e.Status == null ? 0 : (int)e.Status) : true)
                    select new
                    {
                        Cli = e.Pessoa.Nome,
                        Data = e.Data == null ? null : e.Data.Value.ToShortDateString(),
                        Status = NomeStatus(e.Status),
                        Nome = e.Nome,
                        TotalProdutos = e.TotalProdutos,
                        TotalServicos = e.TotalServicos,
                        TotalGeral = e.TotalGeral,
                        TipoCancelamento = e.MotivoCancelamento,
                        ID = e.ID
                    };
            return lista;
        }

        public string NomeStatus(int? status)
        {
            switch (status)
            {
                case 0:
                    return "Em Digitação";
                case 1:
                    return "Aberto";
                case 2:
                    return "Aprovado";
                case 3:
                    return "Não Aprovado";
                case 4:
                    return "Cancelado";
                case 5:
                    return "Concluído";
                default:
                    return String.Empty;
            }
        }

        public IList getListaCombo(DataClassesDataContext db)
        {
            return (from e in db.Projetos
                    select new
                    {
                        Nome = e.Nome,
                        ID = e.ID
                    }).ToList();
        }

        public IQueryable getAprovados(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from e in db.Projetos
                    where e.Status == 2
                    select new
                    {
                        Nome = e.Nome,
                        ID = e.ID
                    };
            return lista;
        }

        public IQueryable getCentrosDeCustoEProjetosAprovados(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from e in db.Projetos
                    where e.Status == 2 || e.IDPessoa == null
                    select new
                    {
                        Nome = e.Nome,
                        ID = e.ID
                    };
            return lista;
        }

        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from e in db.Projetos
                    where e.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = e.Nome,
                        ID = e.ID
                    };
            return lista;
        }

        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.Projetos where u.ID == pID select u).Single();
        }

        public int getID(DataClassesDataContext db, string pValor)
        {
            return 0;
        }

        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
        }

        /// <summary>
        /// Método responsável por retornar uma lista de receitas ou despesas do projeto agrupadas por documento
        /// </summary>
        /// <param name="db">contexto</param>
        /// <param name="pID">id do projeto</param>
        /// <param name="tipo">Receber ou Pagar</param>
        /// <returns></returns>
        /// WNO - 09/06/2010
        public static IQueryable getReceitasDespesas(DataClassesDataContext db, int pID, TipoDocumentoType tipo)
        {
            return (from p in db.Projetos
                    from r in p.Rateio_Movs
                    join m in db.Movimentos on r.IDMovimento equals m.ID
                    where p.ID == pID && r.Movimento.Documento.Tipo == tipo
                    group r by new { r.Movimento.Documento } into rat
                    select new
                    {
                        ID = rat.Key.Documento.ID,
                        Codigo = rat.Key.Documento.Codigo,
                        Vencimento = rat.Key.Documento.DtVencimento,
                        Valor = rat.Sum(r => r.Valor),
                        Situacao = rat.Key.Documento.Situacao,
                        Pessoa = rat.Key.Documento.Pessoa.Nome
                    });
        }

        public static IQueryable getNotas(DataClassesDataContext db, int pID, InOutType ent_sai)
        {
            return (from p in db.Projetos
                    from r in p.Rateio_NotaItems
                    join ni in db.NotaItems on r.IDNotaItem equals ni.ID
                    where p.ID == pID && ni.Nota.Ent_Sai == ent_sai
                    group r by new { r.NotaItem.Nota } into rat
                    select new
                    {
                        ID = rat.Key.Nota.ID,
                        Numero = rat.Key.Nota.Numero,
                        Data = rat.Key.Nota.Dt,
                        Valor = rat.Sum(r => r.Valor)
                    });
        }

        public static IQueryable getPedidos(DataClassesDataContext db, int pID, InOutType ent_sai)
        {
            return (from pro in db.Projetos
                    from ped in pro.Pedidos
                    where pro.ID == pID && ped.Ent_Sai == ent_sai
                    select new
                    {
                        ID = ped.ID,
                        Codigo = ped.Codigo,
                        Data = ped.Dt,
                        Validade = ped.DtValidade,
                        Total = ped.TotalPedido
                    });
        }

        #region Valida Status do Projeto

        public static bool projetoValido(DataClassesDataContext db, int pID)
        {
            var aux = db.Projetos.Where(p => p.ID == pID);
            if (aux.Count() > 0)
            {
                Modelo.Projeto objProjeto = aux.First();
                return auxProjetoValido(objProjeto.Status);
            }
            return false;
        }

        public static bool auxProjetoValido(int? status)
        {
            return status == 2;
        }
        #endregion       
    }
}
