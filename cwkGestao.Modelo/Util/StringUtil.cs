using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using SautinSoft;

namespace cwkGestao.Modelo.Util
{
    public static class StringUtil
    {
        public static IList<string> SepararAspas(this string input)
        {
            IList<string> retorno = new List<string>();

            bool estaCasandoAspas = true;
            do
            {
                int primeiroIndice = input.IndexOf('\'');
                int segundoIndice = input.Substring(primeiroIndice + 1).IndexOf('\'');

                if (primeiroIndice > 0 && segundoIndice > 0)
                {
                    string palavraEntreAspas = input.Substring(primeiroIndice + 1, segundoIndice);
                    retorno.Add(palavraEntreAspas);
                    estaCasandoAspas = true;

                    input = input.Substring(primeiroIndice + segundoIndice + 2);
                }
                else
                    estaCasandoAspas = false;
            }
            while (estaCasandoAspas);

            //if (retorno.Count > 0)
            return retorno;
            //else
            //    return null;
        }

        public static string RemoverEspacosDuplosEQuebras(this string valor)
        {
            if (String.IsNullOrEmpty(valor))
                return String.Empty;

            valor = valor.Trim().Replace(Environment.NewLine, " ").Replace("\t", " ");
            while (valor.Contains("  "))
            {
                valor = valor.Replace("  ", " ");
            }

            return valor;
        }

        public static bool ContemApenasNumeros(this string valor)
        {
            for (int i = 0; i < valor.Length; i++)
            {
                if (!Char.IsNumber(valor[i]))
                    return false;
            }
            return true;
        }

        public static string GetNoMaximoXCaracteres(this string valor, int x)
        {
            if (String.IsNullOrEmpty(valor))
                return String.Empty;
            if (valor.Length >= x)
                return valor.Substring(0, x);
            return valor;
        }
        /// <summary>
        /// Método responsável em tirar os Acentos de uma string
        /// </summary>
        /// <param name="texto">Texto com os Acentos</param>
        /// <returns>Texto sem os Acentos</returns>
        public static string RetiraAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçº";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc.";
            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }
        /// <summary>
        /// Método responsável por contar quantas vezes uma determinada string contém em um texto.
        /// </summary>
        /// <param name="valor">string a ser pesquisada</param>
        /// <param name="textoPesquisa">texto onde contém a string a ser pesquisada</param>
        /// <returns>retorna um int com o número de vezes que a string foi repetida no texto</returns>
        public static int VerificaQuantasVezesTemStringNoTexto(string valor, string textoPesquisa)
        {
            MatchCollection mc;
            Regex r = new Regex(valor);
            mc = r.Matches(textoPesquisa);

            return mc.Count;
        }

        public static String RemoveSpecialCharacters(this String self)
        {
            var normalizedString = self;

            // Prepara a tabela de símbolos.
            var symbolTable = new Dictionary<char, char[]>();
            symbolTable.Add('a', new char[] { 'à', 'á', 'ä', 'â', 'ã' });
            symbolTable.Add('c', new char[] { 'ç' });
            symbolTable.Add('e', new char[] { 'è', 'é', 'ë', 'ê' });
            symbolTable.Add('i', new char[] { 'ì', 'í', 'ï', 'î' });
            symbolTable.Add('o', new char[] { 'ò', 'ó', 'ö', 'ô', 'õ' });
            symbolTable.Add('u', new char[] { 'ù', 'ú', 'ü', 'û' });

            // Substitui os símbolos.
            foreach (var key in symbolTable.Keys)
            {
                foreach (var symbol in symbolTable[key])
                {
                    normalizedString = normalizedString.Replace(symbol, key);
                }
            }

            // Remove os outros caracteres especiais.
            normalizedString = Regex.Replace(normalizedString, "[^0-9a-zA-Z._:Pd (,),(:),(;),(.),(@),(/),(=),(|),/-]+?", "");
            return normalizedString;
        }

        public static string RemoveAcentosRegex(string temp)
        {
            string normalizedString = temp.Normalize(NormalizationForm.FormD);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < normalizedString.Length; i++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(normalizedString[i]);
                    }
                }

             return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string GetSomenteNumeros(string texto)
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(texto, "[^\\d]"));
        }
		
		public static string ConvertRtfToHtml(string texto)
        {
            try
            {
                RtfToHtml t = new RtfToHtml();
                return t.ConvertString(texto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool VerificaSeEHtml(string texto)
        {
            if (texto != null && System.Text.RegularExpressions.Regex.IsMatch(texto, "<html>", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                return true;
            else
                return false;
        }

        public static string RemoverAcentos(this string texto)
        {

            string s = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }
    }
}
