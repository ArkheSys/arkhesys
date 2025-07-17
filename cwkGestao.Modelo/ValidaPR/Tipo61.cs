using System;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo61
    {
        #region AtributosPrivados

        #endregion

        #region Construtor

        public Tipo61()
        {
            Tipo = "61";
            Modelo = 0;
            Serie = 0;
            ValorTotal = 0;
            BaseCalculoIcms = 0;
            ValorIcms = 0;
            ValorIsentoNaoTributado = 0;
            ValorOutros = 0;
            ValorAliquota = 0;
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Tipo "61"
        /// </summary>
        public string Tipo { get; }

        /// <summary>
        ///     Data de emissão do(s) documento(s) fiscal(is) - 8 N AAAAMMDD
        /// </summary>
        public DateTime DataEmissao { get; set; }

        /// <summary>
        ///     Modelo do(s) documento(s) fiscal(is) - 2 N
        /// </summary>
        public int Modelo { get; set; }

        /// <summary>
        ///     Série do(s) documento(s) fiscal(is) - 3 X
        /// </summary>
        public int Serie { get; set; }

        /// <summary>
        ///     Subsérie do(s) documento(s) fiscal(is) - 2 X (Envia vazio)
        /// </summary>
        public string Subserie { get; set; }

        /// <summary>
        ///     Número do primeiro documento fiscal emitido no dia do mesmo modelo, série e subsérie - 6 N
        /// </summary>
        public string NumeroInicialOrdem { get; set; }

        /// <summary>
        ///     Número do último documento fiscal emitido no dia do mesmo modelo, série e subsérie - 6 N
        /// </summary>
        public string NumeroFinalOrdem { get; set; }

        /// <summary>
        ///     Valor total do(s) documento(s) fiscal(is)/Movimento diário (com 2 decimais) - 13 N
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        ///     Base de cálculo do(s) documento(s) fiscal(is)/Total diário (com 2 decimais) - 13 N
        /// </summary>
        public decimal BaseCalculoIcms { get; set; }

        /// <summary>
        ///     Valor do Montante do Imposto/Total diário (com 2 decimais) - 12 N
        /// </summary>
        public decimal ValorIcms { get; set; }

        /// <summary>
        ///     Valor amparado por isenção ou não-incidência/Total diário (com 2 decimais) - 13 N
        /// </summary>
        public decimal ValorIsentoNaoTributado { get; set; }

        /// <summary>
        ///     Valor que não confira débito ou crédito de ICMS/Total diário (com 2 decimais) - 13 N
        /// </summary>
        public decimal ValorOutros { get; set; }

        /// <summary>
        ///     Alíquota do ICMS (com 2 decimais) - 4 N
        /// </summary>
        public decimal ValorAliquota { get; set; }

        /// <summary>
        ///     Método para montar a linha conforme layout do Valida-PR
        /// </summary>
        /// <returns>Linha formatada pronta para gravar</returns>
        public override string ToString()
        {
            // Monta a string para ser gravada no arquivo
            string linha = "";
            linha = linha.Insert(0, $"{Tipo,-2}");
            linha = linha.Insert(2, new string(' ', 14));
            linha = linha.Insert(16, new string(' ', 14));
            linha = linha.Insert(30, $"{$"{DataEmissao:yyyyMMdd}",-8}");
            linha = linha.Insert(38, $"{Modelo:D2}");
            linha = linha.Insert(40, Serie.ToString().PadRight(3));
            linha = linha.Insert(43, "  "); // subserie
            linha = linha.Insert(45, $"{NumeroInicialOrdem}");
            linha = linha.Insert(51, $"{NumeroFinalOrdem}");
            linha = linha.Insert(57, $"{Convert.ToInt32(ValorTotal * 100):D13}");
            linha = linha.Insert(70, $"{Convert.ToInt32(BaseCalculoIcms * 100):D13}");
            linha = linha.Insert(83, $"{Convert.ToInt32(ValorIcms * 100):D12}");
            linha = linha.Insert(95, $"{Convert.ToInt32(ValorIsentoNaoTributado * 100):D13}");
            linha = linha.Insert(108, $"{Convert.ToInt32(ValorOutros * 100):D13}");
            linha = linha.Insert(121, $"{Convert.ToInt32(ValorAliquota * 100):D4}");
            linha = linha.Insert(125, " ");

            return linha;
        }

        #endregion
    }
}