using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.Sped.Fiscal.Util
{
    public class ValidarElemento
    {
        public static string ValidarTamanhoMaximo(string item, int tamanho)
        {
            string retorno = item.Substring(0, tamanho);
            return retorno;
        }
    }
}
