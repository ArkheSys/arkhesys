using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using ADOX;

namespace cwkGestao.Integracao.Telefonia
{
    public class ImportadorAccess : IImportador
    {
        private readonly List<Ligacao> ligacoesImportadas = new List<Ligacao>();
        private readonly string connString;
        private readonly string nomeTabela;

        public ImportadorAccess(string caminho)
        {
            connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + caminho + ";Persist Security Info=False";
            nomeTabela = GetNomeTabela();
        }

        public int GetQuantidadeRegistros()
        {
            using (var conexao = AbrirConexao())
            {
                var cmd = new OleDbCommand("SELECT COUNT(*) FROM " + nomeTabela, conexao);
                object resultado = cmd.ExecuteScalar();
                if (resultado != null)
                    return Convert.ToInt32(resultado);
                return 0;
            }
        }

        private string CriarQueryLigacoes()
        {
            return "SELECT * FROM " + (nomeTabela ?? "8682");
        }

        public IEnumerable<Ligacao> Importar()
        {
            using (var conexao = AbrirConexao())
            {
                using (var leitor = CriarDataReader(conexao))
                {
                    while (leitor.Read())
                    {
                        yield return CriarLigacao(leitor);
                    }
                    leitor.Close();
                }
                conexao.Close();
            }
        }

        private Ligacao CriarLigacao(OleDbDataReader leitor)
        {
            return new Ligacao
           {
               Cidade = leitor["cidade"].ToString(),
               DataHora = Convert.ToDateTime(leitor["data"]),
               DescricaoUso = leitor["desc_uso"].ToString(),
               Destino = leitor["destino"].ToString(),
               Horario = leitor["horario"].ToString(),
               Origem = leitor["origem"].ToString(),
               TempoSegundos = Convert.ToInt32(leitor["tempo_segundos"]),
               TipoUso = Convert.ToInt32(leitor["tipo_uso"]),
               Uf = leitor["uf"].ToString(),
               ValorComImposto = leitor["valor_com_impostos"].ToString()
           };
        }

        private string GetNomeTabela()
        {
            var catalog = new ADOX.CatalogClass();
            var conexao = new ADODB.ConnectionClass();
            try
            {
                conexao.Open(connString, null, null, 0);
                catalog.ActiveConnection = conexao;
                foreach (ADOX.Table tabela in catalog.Tables)
                {
                    if (tabela.Name.Contains("MSys"))
                        continue;
                    return tabela.Name;
                }
            }
            finally
            {
                conexao.Close();
            }
            return null;
        }

        private OleDbDataReader CriarDataReader(OleDbConnection conexao)
        {
            var cmd = new OleDbCommand(CriarQueryLigacoes(), conexao);
            return cmd.ExecuteReader();
        }

        private OleDbConnection AbrirConexao()
        {
            var conexao = new OleDbConnection(connString);
            conexao.Open();
            return conexao;
        }

        #region Membros de IImportador


        public IEnumerable<Ligacao> ImportarFixoECelular()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
