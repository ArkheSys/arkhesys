using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CwkGestao.Integracao.PafEcf.Salc.Arquivos_de_Texto
{
    public static class TextBuilder
    {
        public static void GerarArquivoTexto(string _nome, IList<string> _dados)
        {
            if (!System.IO.File.Exists(_nome))
                System.IO.File.Create(_nome).Close();

            System.IO.TextWriter arquivo = System.IO.File.AppendText(_nome);

            foreach (var item in _dados)
            {
                arquivo.WriteLine(item);
            }

            arquivo.Close();
        }
    }
}
