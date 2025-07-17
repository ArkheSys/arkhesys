using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MO = Modelo;

namespace cwkGestao.Repositorio
{
    public class SqlServerUtil
    {
        public static FileInfo GeraBackupBanco(string bkpPath)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            PegaUsuarioESenhaDoAppConfig(connString);

            using (SqlConnection conn = new SqlConnection(connString.ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand {Connection = conn, CommandTimeout = 250};
                    cmd.CommandText = "BACKUP DATABASE " + connString.InitialCatalog + " TO DISK = '" + bkpPath + @"'";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }

                var bkpFile = new FileInfo(bkpPath);

                return bkpFile.Exists ? bkpFile : null;
            }
        }

        public static bool RestauraBackupBanco(FileInfo backup)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            PegaUsuarioESenhaDoAppConfig(connString);

            string nomeMdf, nomeLdf;
            GetNomesMdfLdf(connString, out nomeMdf, out nomeLdf);

            string comandoBak = "ALTER DATABASE " + connString.InitialCatalog + " SET SINGLE_USER WITH ROLLBACK AFTER 60;" +
                                "RESTORE DATABASE " + connString.InitialCatalog + " FROM DISK = '" + backup.FullName + "' WITH MOVE '" + connString.InitialCatalog + "' TO '" + nomeMdf + "', MOVE '" + connString.InitialCatalog + "_Log' TO '" +
                                nomeLdf + "',REPLACE;" +
                                "ALTER DATABASE " + connString.InitialCatalog + " SET MULTI_USER;";

            connString.InitialCatalog = "master";
            using (SqlConnection conn = new SqlConnection(connString.ConnectionString))
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand {Connection = conn, CommandTimeout = 240};
                    cmd.CommandText = comandoBak;
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }

            return true;
        }

        private static void GetNomesMdfLdf(SqlConnectionStringBuilder connString, out string nomeMdf, out string nomeLdf)
        {
            using (SqlConnection conn = new SqlConnection(connString.ConnectionString))
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand {Connection = conn, CommandTimeout = 240};

                    cmd.CommandText = "select top 1 physical_name from sys.database_files where type = 0";
                    nomeMdf = Convert.ToString(cmd.ExecuteScalar());
                    cmd.CommandText = "select top 1 physical_name from sys.database_files where type = 1";
                    nomeLdf = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();
                }
                finally
                {
                    conn.Close();
                }
        }

        private static void PegaUsuarioESenhaDoAppConfig(SqlConnectionStringBuilder connStringBuilder)
        {
            connStringBuilder.ConnectionString = MO.cwkGlobal.CONN_STRING; //cwkGestao.Modelo.Global.CONN_STRING;           
        }
    }
}