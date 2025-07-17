using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio.Utils
{
    public class ArquivosUtil
    {
        public static FileInfo CompactarArquivos(string nomeArquivo, List<FileInfo> arquivosCompactar)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (FileInfo item in arquivosCompactar)
                {
                    zip.AddFile(item.FullName, ".");
                }
                zip.Save(nomeArquivo);
            }
            return new FileInfo(nomeArquivo);
        }

        public static FileInfo[] GetArquivosPasta(string diretorio, Func<FileInfo, bool> filtro)
        {
            DirectoryInfo pasta = new DirectoryInfo(diretorio);
            FileInfo[] arquivosDaPasta = pasta.GetFiles();
            
            if (filtro != null)
                return arquivosDaPasta.Where(filtro).ToArray();

            return arquivosDaPasta;
        }
    }
}
