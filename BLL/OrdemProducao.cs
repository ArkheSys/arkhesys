using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System;

namespace BLL
{
    public partial class OrdemProducao
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.OrdemProducao objOrdemProducao)
        {
            Dictionary<string, string> ret = ValidaObjeto(objOrdemProducao);
            if (ret.Count == 0)
            {
                if (objOrdemProducao.ID == 0)
                    objOrdemProducao.IncData = DateTime.Now;
                db.SubmitChanges();
            }            
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.OrdemProducao objOrdemProducao)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objOrdemProducao.Codigo <= 0)
                ret.Add("txtCodigo", "Campo Obrigatório.");
            if (objOrdemProducao.IdCliente <= 0)
                ret.Add("lkPessoa", "Campo Obrigatório.");
            if (objOrdemProducao.IdFilial <= 0)
                ret.Add("lkFilial", "Campo Obrigatório.");
            if (objOrdemProducao.IdProjeto <= 0)
                ret.Add("lkProjeto", "Campo Obrigatório.");
            return ret;
        }

        public DataTable GetOrdemProducaoImprimir(int pIdOrdemProducao)
        {
            DataTable lista = new DataTable();
            SqlConnection conexao = new SqlConnection(Modelo.cwkGlobal.CONN_STRING);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT OrdemProducao.codigo AS OrdemProducao");
            sql.AppendLine(",Pessoa.Nome AS Cliente");
            sql.AppendLine(",PessoaEndereco.Endereco + ', ' + PessoaEndereco.Numero AS Endereco");
            sql.AppendLine(",PessoaEndereco.Telefone AS Telefone");
            sql.AppendLine(",Cidade.Nome AS Cidade");
            sql.AppendLine(",OrdemProducaoExecucao.descricao AS DescricaoServicoExecucao");
            sql.AppendLine(",OrdemProducaoExecucao.quantidade AS QuantidadeServicoExecucao");
            sql.AppendLine(",Projeto.Descricao");
            sql.AppendLine(",Projeto.InstrucoesProducao AS Instrucoes");
            sql.AppendLine("FROM OrdemProducao");
            sql.AppendLine("INNER JOIN Projeto ON Projeto.ID = OrdemProducao.IDProjeto");
            sql.AppendLine("INNER JOIN Pessoa ON Pessoa.ID = OrdemProducao.IDCliente");
            sql.AppendLine("INNER JOIN PessoaEndereco ON PessoaEndereco.IDPessoa = Pessoa.ID");
            sql.AppendLine("INNER JOIN Cidade ON Cidade.ID = PessoaEndereco.IDCidade");
            sql.AppendLine("LEFT JOIN OrdemProducaoExecucao ON OrdemProducaoExecucao.IdOrdemProducao = OrdemProducao.ID");
            sql.AppendLine("WHERE OrdemProducao.id = " + pIdOrdemProducao);
            sql.AppendLine(" AND PessoaEndereco.bPrincipal = 1");

            SqlCommand comando = new SqlCommand(sql.ToString());
            comando.Connection = conexao;
            conexao.Open();
            lista.Load(comando.ExecuteReader());
            conexao.Close();
            return lista;
        }

        public DataTable GetOrdemProducaoComValoresImprimir(int pIdOrdemProducao)
        {
            DataTable lista = new DataTable();
            SqlConnection conexao = new SqlConnection(Modelo.cwkGlobal.CONN_STRING);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.AppendLine(" ope.DescricaoServicoExecucao");
            sql.AppendLine(",ROUND(ISNULL(Projeto_ProdutoServico.Quantidade, 0), 2) AS QuantidadeOrcada");
            sql.AppendLine(",ISNULL(Projeto_ProdutoServico.ValorUnitario, 0) AS ValorUnitarioOrcado");
            sql.AppendLine(",ISNULL((Projeto_ProdutoServico.ValorUnitario * ROUND(Projeto_ProdutoServico.Quantidade, 2)), 0) AS TotalOrcado");
            sql.AppendLine(", ROUND(ope.QuantidadeServicoExecucao, 2) AS QuantidadeServicoExecucao");
            sql.AppendLine(",case when Projeto_ProdutoServico.ValorUnitario is null then Servico.Valor else Projeto_ProdutoServico.ValorUnitario end AS ValorUnitario");
            sql.AppendLine(",((case when Projeto_ProdutoServico.ValorUnitario is null then Servico.Valor else Projeto_ProdutoServico.ValorUnitario end) * ROUND(ope.QuantidadeServicoExecucao, 2)) AS TotalApontado");
            sql.AppendLine("FROM");
            sql.AppendLine("(SELECT OrdemProducaoExecucao.IdOrdemProducao");
            sql.AppendLine(",OrdemProducaoExecucao.descricao AS DescricaoServicoExecucao");
            sql.AppendLine(",OrdemProducaoExecucao.IdServico");
            sql.AppendLine(",SUM(OrdemProducaoExecucao.quantidade) AS QuantidadeServicoExecucao");
            sql.AppendLine("FROM OrdemProducaoExecucao");
            sql.AppendLine("WHERE OrdemProducaoExecucao.IdOrdemProducao = " + pIdOrdemProducao);
            sql.AppendLine("group by OrdemProducaoExecucao.IdOrdemProducao, OrdemProducaoExecucao.IdServico, OrdemProducaoExecucao.descricao) ope");
            sql.AppendLine("INNER JOIN Servico ON Servico.Id = ope.IdServico");
            sql.AppendLine("INNER JOIN OrdemProducao ON OrdemProducao.ID = ope.IdOrdemProducao");
            sql.AppendLine("LEFT JOIN Projeto_ProdutoServico ON Projeto_ProdutoServico.IDServico = ope.IdServico AND Projeto_ProdutoServico.IDOrdemProducao = OrdemProducao.ID");

            SqlCommand comando = new SqlCommand(sql.ToString());
            comando.Connection = conexao;
            conexao.Open();
            lista.Load(comando.ExecuteReader());
            conexao.Close();
            return lista;
        }

        public DataTable GetOrdemProducaoVazia(int idOrdemProducao)
        {
            DataTable ordemProducao = GetOrdemProducaoImprimir(idOrdemProducao);
            DataTable lista = new DataTable();
            lista.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("OrdemProducao")
                ,new DataColumn("Cliente")
                ,new DataColumn("Endereco")
                ,new DataColumn("Cidade")
                ,new DataColumn("Telefone")
                ,new DataColumn("DescricaoServicoExecucao")
                ,new DataColumn("QuantidadeServicoExecucao", typeof(Decimal))
                ,new DataColumn("Descricao")
                ,new DataColumn("Instrucoes")
            });

            for (int i = 0; i < 18; i++)
            {
                lista.Rows.Add(new object[]
                {
                    ordemProducao.Rows[0]["OrdemProducao"]
                    ,ordemProducao.Rows[0]["Cliente"]
                    ,ordemProducao.Rows[0]["Endereco"]
                    ,ordemProducao.Rows[0]["Cidade"]
                    ,ordemProducao.Rows[0]["Telefone"]
                    ,""
                    , 0
                    ,ordemProducao.Rows[0]["Descricao"]
                    ,ordemProducao.Rows[0]["Instrucoes"]
                });
            }

            return lista;
        }
    }
}
