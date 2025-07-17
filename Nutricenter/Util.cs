using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Importacao.Nutricenter
{
    public static class Util
    {
        public static string RetiraAcentos(this string texto)
        {
            if (String.IsNullOrEmpty(texto))
                return String.Empty;

            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }
    }
}
