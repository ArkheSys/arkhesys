using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cwkGestao.Negocio.Utils;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio.Backup
{
    public static class Backup
    {
        public static void Logs(string diretorio)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\Log\");
            if (Directory.Exists(directoryInfo.ToString()))
            {
                List<FileInfo> arquivos = new List<FileInfo>(directoryInfo.EnumerateFiles());

                Compactador.Compactar(diretorio + @"\Logs.zip", arquivos);
            }
            directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\LogHom\");

            if (Directory.Exists(directoryInfo.ToString()))
            {
                List<FileInfo> arquivosHom = new List<FileInfo>(directoryInfo.EnumerateFiles());

                Compactador.Compactar(diretorio + @"\LogsHom.zip", arquivosHom);
            }
        }

        public static void XmlDestinatario(string diretorio)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\XmlDestinatario\");

            if (Directory.Exists(directoryInfo.ToString()))
            {
                List<FileInfo> arquivos = new List<FileInfo>(directoryInfo.EnumerateFiles());

                Compactador.Compactar(diretorio + @"\XmlDestinatario.zip", arquivos);
            }

            directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\XmlDestinatarioHom\");

            if (Directory.Exists(directoryInfo.ToString()))
            {
                List<FileInfo> arquivosHom = new List<FileInfo>(directoryInfo.EnumerateFiles());

                Compactador.Compactar(diretorio + @"\XmlDestinatariosHom.zip", arquivosHom);
            }
        }

        public static void BancoDeDados(string diretorio)
        {
            string arquivoTemporario = diretorio + @"\backupBanco.bak";

            List<FileInfo> arquivos = new List<FileInfo>() { SqlServerUtil.GeraBackupBanco(arquivoTemporario) };

            Compactador.Compactar(diretorio + @"\BancoDeDados.zip", arquivos);

            File.Delete(arquivoTemporario);
        }

        public static void Restaurar(string fileBackup)
        {
            List<FileInfo> fileInfos = Compactador.Descompactar(fileBackup);

            try
            {
                if (fileInfos.Count != 1)
                {
                    throw new Exception("O arquivo selecionado não é um Backup de banco de dados.");
                }
                SqlServerUtil.RestauraBackupBanco(fileInfos[0]);
            }
            finally
            {
                Compactador.DeletaDiretorioTemporario(new FileInfo(fileBackup).DirectoryName);
            }
        }
    }
}
