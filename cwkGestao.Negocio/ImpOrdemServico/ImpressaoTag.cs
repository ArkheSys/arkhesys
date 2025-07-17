using System;

namespace cwkGestao.Negocio.ImpOrdemServico
{
    public class ImpressaoTag
    {
        private int vezesGerado;

        public int Tamanho { get; set; }
        public string Formato { get; set; }
        public bool MultiplasLinhas { get; set; }

        public ImpressaoTag() : this(20, "")
        { }

        public ImpressaoTag(int tamanho, string formato)
        {
            Formato = formato;
            Tamanho = tamanho;
        }

        public string FormatString<T>(T valor)
        {
            if (MultiplasLinhas)
            {
                int alcanceAtual = vezesGerado*Tamanho + Tamanho;
                int offset = alcanceAtual > valor.ToString().Length ? valor.ToString().Length : alcanceAtual;


                try
                {
                    string valorString = valor.ToString();
                    int tamanhoString = valorString.Length < Tamanho ? valorString.Length : Tamanho;

                    string retorno = string.Format("{0,-" + Tamanho + "}",
                        valorString.Substring(vezesGerado * Tamanho, tamanhoString));
                    vezesGerado++;
                    return retorno;
                }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
                catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
                {
                    return string.Format("{0,-" + Tamanho + "}", " ");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Formato) && (Formato.StartsWith("C") || Formato.StartsWith("F")))
                    return string.Format("{0," + Tamanho + ":" + Formato + "}", valor);
                else
                    return string.Format("{0,-" + Tamanho + "}", valor).Substring(0, Tamanho);
            }
        }
    }
}