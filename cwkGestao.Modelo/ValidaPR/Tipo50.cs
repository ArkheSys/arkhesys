using System;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo50
    {
        #region AtributosPrivados

        private string _CNPJ;
        private string _InscricaoEstadual;

        #endregion

        #region Construtor

        public Tipo50()
        {
            Tipo = "50";
            Modelo = 0;
            NumeroNF = 0;
            CFOP = 0;
            ValorTotal = 0;
            BaseCalculoICMS = 0;
            ValorICMS = 0;
            ValorIsentoICMS = 0;
            ValorOutras = 0;
            Aliquota = 0;
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Tipo "50"
        /// </summary>
        public string Tipo { get; }

        /// <summary>
        ///     CNPJ do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string CNPJ
        {
            get => _CNPJ;
            set => _CNPJ = value != null ? value.OnlyNumbers() : "";
        }

        /// <summary>
        ///     Inscrição estadual do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string InscricaoEstadual
        {
            get => _InscricaoEstadual;
            set => _InscricaoEstadual = value != null ? value.OnlyNumbers() : "";
        }

        /// <summary>
        ///     Data de emissão na saída ou de recebimento na entrada
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        ///     Sigla da unidade da Federação do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        ///     Código do modelo da nota fiscal
        /// </summary>
        public int Modelo { get; set; }

        /// <summary>
        ///     Série da nota fiscal
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        ///     Número da nota fiscal
        /// </summary>
        public int NumeroNF { get; set; }

        /// <summary>
        ///     Código Fiscal de Operação e Prestação
        /// </summary>
        public int CFOP { get; set; }

        /// <summary>
        ///     Emitente da Nota Fiscal (P-próprio/T-terceiros)
        /// </summary>
        public string Emitente { get; set; }

        /// <summary>
        ///     Valor total da nota fiscal (com 2 decimais)
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        ///     Base de Cálculo do ICMS (com 2 decimais)
        /// </summary>
        public decimal BaseCalculoICMS { get; set; }

        /// <summary>
        ///     Montante do imposto ICMS (com 2 decimais
        /// </summary>
        public decimal ValorICMS { get; set; }

        /// <summary>
        ///     Valor amparado por isenção ou   não  incidência (com 2 decimais)
        /// </summary>
        public decimal ValorIsentoICMS { get; set; }

        /// <summary>
        ///     Valor que não confira débito ou crédito do ICMS (com 2 decimais)
        /// </summary>
        public decimal ValorOutras { get; set; }

        /// <summary>
        ///     Alíquota do ICMS (com 2 decimais)
        /// </summary>
        public decimal Aliquota { get; set; }

        /// <summary>
        ///     Situação da nota fiscal quanto ao cancelamento (S-cancelada, N-não cancelada)
        /// </summary>
        public string Situacao { get; set; }

        /// <summary>
        ///     Método para montar a linha conforme layout do Valida-PR
        /// </summary>
        /// <returns>Linha formatada pronta para gravar</returns>
        public override string ToString()
        {
            // Monta a string para ser gravada no arquivo
            // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
            string linha = "";
            linha = linha.Insert(0, $"{Tipo,-2}");
            linha = linha.Insert(2, $"{(string.IsNullOrEmpty(CNPJ) == false ? $"{Convert.ToInt64(CNPJ):00000000000000}" : "00000000000000"),-14}");
            linha = linha.Insert(16, $"{(InscricaoEstadual.IsNotNullOrEmpty() ? InscricaoEstadual : "ISENTO"),-14}");
            linha = linha.Insert(30, $"{$"{Data:yyyyMMdd}",-8}");
            linha = linha.Insert(38, $"{Estado ?? "",-2}");
            linha = linha.Insert(40, $"{Modelo:D2}");
            linha = linha.Insert(42, $"{Serie ?? "",-3}");
            linha = linha.Insert(45, $"{NumeroNF:D6}");
            linha = linha.Insert(51, $"{CFOP:D4}");
            linha = linha.Insert(55, $"{Emitente ?? "",-1}");
            linha = linha.Insert(56, $"{Convert.ToInt32(ValorTotal * 100):D13}");
            linha = linha.Insert(69, $"{Convert.ToInt32(BaseCalculoICMS * 100):D13}");
            linha = linha.Insert(82, $"{Convert.ToInt32(ValorICMS * 100):D13}");
            linha = linha.Insert(95, $"{Convert.ToInt32(ValorIsentoICMS * 100):D13}");
            linha = linha.Insert(108, $"{Convert.ToInt32(ValorOutras * 100):D13}");
            linha = linha.Insert(121, $"{Convert.ToInt32(Aliquota * 100):D4}");
            linha = linha.Insert(125, $"{Situacao ?? "",-1}");

            return linha;
        }

        #endregion
    }
}