using System;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo54
    {
        #region AtributosPrivados

        private string _cnpj;

        #endregion

        #region Construtor

        public Tipo54()
        {
            Tipo = "54";
            Modelo = 0;
            NumeroNF = 0;
            CFOP = 0;
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Tipo "54"
        /// </summary>
        public string Tipo { get; }

        /// <summary>
        ///     CNPJ do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string CNPJ
        {
            get => _cnpj;
            set
            {
                if (value != null)
                    _cnpj = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                else
                    _cnpj = "";
            }
        }

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
        ///     Código da Situação Tributária
        /// </summary>
        public string CST { get; set; }

        /// <summary>
        ///     Número de ordem do item na nota fiscal
        /// </summary>
        public int NumeroItem { get; set; }

        /// <summary>
        ///     Código do produto ou serviço do informante
        /// </summary>
        public string CodigoProduto { get; set; }

        /// <summary>
        ///     Quantidade do produto (com 3 decimais)
        /// </summary>
        public decimal Quantidade { get; set; }

        /// <summary>
        ///     Valor bruto do produto - valor unitário multiplicado por quantidade (com 2 decimais)
        /// </summary>
        public decimal ValorProduto { get; set; }

        /// <summary>
        ///     Valor do Desconto  Concedido no  item (com 2 decimais)
        /// </summary>
        public decimal ValorDesconto { get; set; }

        /// <summary>
        ///     Base de cálculo do ICMS (com 2  decimais)
        /// </summary>
        public decimal BaseCalculoIcms { get; set; }

        /// <summary>
        ///     Base de cálculo do ICMS de  retenção na Substituição Tributária  (com 2 decimais)
        /// </summary>
        public decimal BaseCalculoIcms_ST { get; set; }

        /// <summary>
        ///     Valor do IPI (com 2 decimais)
        /// </summary>
        public decimal ValorIPI { get; set; }

        /// <summary>
        ///     Alíquota utilizada no cálculo do  ICMS  (com 2 decimais)
        /// </summary>
        public decimal AliquotaIcms { get; set; }

        public override string ToString()
        {
            // Monta a string para ser gravada no arquivo
            // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
            string linha = "";
            linha = linha.Insert(0, string.Format("{0,-2}", Tipo));
            linha = linha.Insert(2, string.Format("{0,-14}", string.IsNullOrEmpty(CNPJ) == false ? string.Format("{0:00000000000000}", Convert.ToInt64(CNPJ)) : "00000000000000"));
            linha = linha.Insert(16, string.Format("{0:D2}", Modelo));
            linha = linha.Insert(18, string.Format("{0,-3}", Serie != null ? Serie : ""));
            linha = linha.Insert(21, string.Format("{0:D6}", NumeroNF));
            linha = linha.Insert(27, string.Format("{0:D4}", CFOP));
            linha = linha.Insert(31, string.Format("{0,-3}", CST != null ? CST : ""));
            linha = linha.Insert(34, string.Format("{0:D3}", NumeroItem));
            linha = linha.Insert(37, string.Format("{0,-14}", CodigoProduto != null ? CodigoProduto : ""));
            linha = linha.Insert(51, string.Format("{0:D11}", Convert.ToInt32(Quantidade * 1000)));
            linha = linha.Insert(62, string.Format("{0:D12}", Convert.ToInt32(ValorProduto * 100)));
            linha = linha.Insert(74, string.Format("{0:D12}", Convert.ToInt32(ValorDesconto * 100)));
            linha = linha.Insert(86, string.Format("{0:D12}", Convert.ToInt32(BaseCalculoIcms * 100)));
            linha = linha.Insert(98, string.Format("{0:D12}", Convert.ToInt32(BaseCalculoIcms_ST * 100)));
            linha = linha.Insert(110, string.Format("{0:D12}", Convert.ToInt32(ValorIPI * 100)));
            linha = linha.Insert(122, string.Format("{0:D4}", Convert.ToInt32(AliquotaIcms * 100)));

            return linha;
        }

        #endregion
    }
}