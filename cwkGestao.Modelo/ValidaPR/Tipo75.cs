using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo75
    {
        #region AtributosPrivados
        private string _tipo;
        private DateTime _dataInicial;
        private DateTime _dataFinal;
        private string _codigoProduto;
        private string _codigoNCM;
        private string _descricaoProduto;
        private string _unidade;
        private Decimal _aliquotaIPI;
        private Decimal _aliquotaICMS;
        private Decimal _reducaoBaseCalculo;
        private Decimal _baseCalculoST;
        #endregion

        #region Construtor
        public Tipo75()
        {
            _tipo = "75";
            _aliquotaIPI = 0;
            _aliquotaICMS = 0;
            _reducaoBaseCalculo = 0;
            _baseCalculoST = 0;
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Tipo "75"
        /// </summary>
        public string Tipo
        {
            get { return _tipo; }
        }

        /// <summary>
        /// Data inicial do período de validade  das informações
        /// </summary>
        public DateTime DataInicial
        {
            get { return _dataInicial; }
            set { _dataInicial = value; }
        }

        /// <summary>
        /// Data final do período de validade das  informações
        /// </summary>
        public DateTime DataFinal
        {
            get { return _dataFinal; }
            set { _dataFinal = value; }
        }

        /// <summary>
        /// Código do produto ou serviço  utilizado pelo contribuinte
        /// </summary>
        public string CodigoProduto
        {
            get { return _codigoProduto; }
            set { _codigoProduto = value; }
        }

        /// <summary>
        /// Codificação da Nomenclatura Comum  do Mercosul
        /// </summary>
        public string CodigoNCM
        {
            get { return _codigoNCM; }
            set { _codigoNCM = value; }
        }

        /// <summary>
        /// Descrição do produto ou Serviço
        /// </summary>
        public string DescricaoProduto
        {
            get { return _descricaoProduto; }
            set { _descricaoProduto = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Unidade de medida de  comercialização do produto(un, kg,  mt, m3, sc, frd, kwh, etc.)
        /// </summary>
        public string Unidade
        {
            get { return _unidade; }
            set { _unidade = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Alíquota do IPI do produto (com 2 decimais)
        /// </summary>
        public Decimal AliquotaIPI
        {
            get { return _aliquotaIPI; }
            set { _aliquotaIPI = value; }
        }

        /// <summary>
        /// Alíquota do ICMS aplicável a  mercadoria ou serviço nas operações  ou prestações internas ou naquelas  
        /// que tiverem sido iniciadas no exterior (com 2 decimais)
        /// </summary>
        public Decimal AliquotaICMS
        {
            get { return _aliquotaICMS; }
            set { _aliquotaICMS = value; }
        }

        /// <summary>
        /// % de Redução na base de cálculo do  ICMS, nas operações internas (com 2   decimais)
        /// </summary>
        public Decimal ReducaoBaseCalculo
        {
            get { return _reducaoBaseCalculo; }
            set { _reducaoBaseCalculo = value; }
        }

        /// <summary>
        /// Base de Cálculo do ICMS de  substituição tributária (com 2  decimais)
        /// </summary>
        public Decimal BaseCalculoST
        {
            get { return _baseCalculoST; }
            set { _baseCalculoST = value; }
        }

        /// <summary>
        /// Método para montar a linha conforme layout do Valida-PR
        /// </summary>
        /// <returns>Linha formatada pronta para gravar</returns>
        public override string ToString()
        {
            // Monta a string para ser gravada no arquivo
            // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
            string linha = "";
            linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
            linha = linha.Insert(2, String.Format("{0,-8}", (this.DataInicial != null ? String.Format("{0:yyyyMMdd}", this.DataInicial) : "")));
            linha = linha.Insert(10, String.Format("{0,-8}", (this.DataFinal != null ? String.Format("{0:yyyyMMdd}", this.DataFinal) : "")));
            linha = linha.Insert(18, String.Format("{0,-14}", (this.CodigoProduto != null ? this.CodigoProduto : "")));
            linha = linha.Insert(32, String.Format("{0,-8}", (this.CodigoNCM != null ? this.CodigoNCM : "")));
            linha = linha.Insert(40, String.Format("{0,-53}", this.DescricaoProduto != null ? this.DescricaoProduto.Substring(0, (this.DescricaoProduto.Length < 53 ? this.DescricaoProduto.Length : 53)) : ""));
            linha = linha.Insert(93, String.Format("{0,-6}", (this.Unidade != null ? this.Unidade : "")));
            linha = linha.Insert(99, String.Format("{0:D5}", Convert.ToInt32((this.AliquotaIPI * 100))));
            linha = linha.Insert(104, String.Format("{0:D4}", Convert.ToInt32((this.AliquotaICMS * 100))));
            linha = linha.Insert(108, String.Format("{0:D5}", Convert.ToInt32((this.ReducaoBaseCalculo * 100))));
            linha = linha.Insert(113, String.Format("{0:D13}", Convert.ToInt32((this.BaseCalculoST * 100))));

            return linha;
        }

        #endregion
    }
}
