using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.MercadoLivre.Util
{
    public class Util
    {
        public string ToAscii(string dirty)
        {
            string s = dirty;
            byte[] caracteres = System.Text.Encoding.Unicode.GetBytes(s);

            return System.Text.Encoding.ASCII.GetString(caracteres);
        }
    }
}
