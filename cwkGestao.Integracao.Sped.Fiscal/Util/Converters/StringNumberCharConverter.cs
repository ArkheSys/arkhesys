using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Util.Converters
{
    public class StringNumberCharConverter : ConverterBase
    {
        private int tam;
        private string charPadding;
        private enum TipoPadding
        {
            Esquerda, Direita, Nenhum
        };
        private bool removeCharsEspeciais;
        private TipoPadding padding;
        /// <summary>
        /// Annotation de conversão/padding de strings e inteiros para o Filehelpers
        /// </summary>
        /// <param name="tamanho">String contendo o número máximo de carateres que o campo deverá ter. Se vazio, o padrão é 255</param>
        /// <param name="caracterePadding">String contendo o caractere que será usado para realizar o Padding. Exemplo: char = "X", tamanho = 5, valor = 20. Resultado: "XXX20"</param>
        /// <param name="tipoPadding">String informando o posicionamento do padding. Opções: "direita" / "esquerda". Se vazio ou qualquer outra string, não haverá padding</param>
        /// <param name="removerCharsEspeciais">String informando a necessidade de remover ou não os caracteres especiais da string. Valores possíveis: "true" / "false". Padrão: "false"</param>
        public StringNumberCharConverter(string tamanho, string caracterePadding, string tipoPadding, string removerCharsEspeciais)
        {
            try
            {
                if (String.IsNullOrEmpty(tamanho))
                {
                    tam = 257;
                }
                else
                {
                    tam = Convert.ToInt32(tamanho) + 2; // + 2 por causa do pipe inicial e pipe final. 
                                                        //exemplo: (5) |XXXXX| (comprimento = 7)
                }
                charPadding = caracterePadding;
                if (tipoPadding.ToLower() == "esquerda")
                {
                    padding = TipoPadding.Esquerda;
                }
                else if (tipoPadding.ToLower() == "direita")
                {
                    padding = TipoPadding.Direita;
                }
                else
                {
                    padding = TipoPadding.Nenhum;
                }
                removeCharsEspeciais = removerCharsEspeciais == "true";
            }
            catch (Exception ex)
            {
                Exception e = new Exception("O tamanho foi informado de maneira incorreta", ex);
            }

        }
        public override object StringToField(string from)
        {
            object o = new object();
            try
            {
                o = Convert.ToInt32(from);
            }
            catch (Exception)
            {
                return from;
            }
            return o;
        }

        public override string FieldToString(object from)
        {
            if (from == null)
            {
                return "";
            }
            string resultado = from.ToString();
            if (removeCharsEspeciais)
            {
                char[] arr = resultado.ToCharArray();

                arr = Array.FindAll<char>(arr, (c => char.IsLetterOrDigit(c)));
                resultado = new string(arr);
            }
            if (resultado.Length >= tam)
            {
                resultado = resultado.Substring(0, tam);
            }
            else
            {
                switch (padding)
                {
                    case TipoPadding.Esquerda:
                        if (!String.IsNullOrEmpty(charPadding))
                        {
                            resultado = resultado.PadLeft(tam - resultado.Length, charPadding.ToCharArray()[0]);
                        }
                        break;
                    case TipoPadding.Direita:
                        if (!String.IsNullOrEmpty(charPadding))
                        {
                            resultado = resultado.PadRight(tam - resultado.Length, charPadding.ToCharArray()[0]);
                        }
                        break;
                    case TipoPadding.Nenhum:
                        break;
                    default:
                        break;
                }
            }
            return resultado;
        }
    }
}
