using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace cwkGestao.Negocio.Utils
{
    internal class Compactador
    {
        public static FileInfo Compactar(string fileName, List<FileInfo> origens)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (FileInfo item in origens)
                {
                    zip.AddFile(item.FullName, ".");
                }
                zip.Save(fileName);
            }
            return new FileInfo(fileName);
        }

        public static List<FileInfo> Descompactar(string fileName)
        {
            string path = new FileInfo(fileName).DirectoryName;
            CriaDiretorioTemporario(path);
            List<FileInfo> descompactados = new List<FileInfo>();
            using (ZipFile zip = new ZipFile(fileName))
            {
                foreach (ZipEntry zipEntry in zip)
                {
                    zipEntry.Extract(path +  @"\Temp");
                    descompactados.Add(new FileInfo(path + @"\Temp\" + zipEntry.FileName));
                }
            }
            return descompactados;
        }

        private static void CriaDiretorioTemporario(string path)
        {
            Directory.CreateDirectory(path + @"\Temp");
        }

        public static void DeletaDiretorioTemporario(string path)
        {
            DirectoryInfo temp = new DirectoryInfo(path + @"\Temp");
            temp.Delete(true);
        }
    }
}
