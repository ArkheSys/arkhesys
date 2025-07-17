using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo53
    {
        #region AtributosPrivados
        private string _Tipo;
        private string _CNPJ;
        private string _InscricaoEstadual;
        private DateTime _Data;
        private string _Estado;
        private int _Modelo;
        private string _Serie;
        private int _NumeroNF;
        private int _CFOP;
        private string _Emitente;
        private Decimal _BaseCalculoICMS_ST;
        private Decimal _ValorICMSRetido;
        private Decimal _ValorDespesas;
        private string _Situacao;
        private string _CodigoAntecipacao;
        #endregion

        #region Construtor
        public Tipo53()
        {
            _Tipo = "53";
            this.Modelo = 0;
            this.NumeroNF = 0;
            this.CFOP = 0;
            this.BaseCalculoICMS_ST = 0;
            this.ValorICMSRetido = 0;
            this.ValorDespesas = 0;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Tipo "53"
        /// </summary>
        public string Tipo
        {
            get { return _Tipo; }
        }

        /// <summary>
        /// CNPJ do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string CNPJ
        {
            get
            {
                return _CNPJ;
            }
            set
            {
                _CNPJ = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
            }
        }

        /// <summary>
        /// Inscrição estadual do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string InscricaoEstadual
        {
            get { return _InscricaoEstadual; }
            set
            {
                if (value != null)
                    _InscricaoEstadual = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                else
                    _InscricaoEstadual = "";
            }
        }

        /// <summary>
        /// Data de emissão na saída ou de recebimento na entrada
        /// </summary>
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        /// <summary>
        /// Sigla da unidade da Federação do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        /// <summary>
        /// Código do modelo da nota fiscal	
        /// </summary>
        public int Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        /// <summary>
        /// Série da nota fiscal
        /// </summary>
        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        /// <summary>
        /// Número da nota fiscal
        /// </summary>
        public int NumeroNF
        {
            get { return _NumeroNF; }
            set { _NumeroNF = value; }
        }

        /// <summary>
        /// Código Fiscal de Operação e Prestação
        /// </summary>
        public int CFOP
        {
            get { return _CFOP; }
            set { _CFOP = value; }
        }

        /// <summary>
        /// Emitente da Nota Fiscal (P-próprio/T-terceiros)
        /// </summary>
        public string Emitente
        {
            get { return _Emitente; }
            set { _Emitente = value; }
        }

        /// <summary>
        /// Base de cálculo de retenção do ICMS (com 2 decimais)
        /// </summary>
        public Decimal BaseCalculoICMS_ST
        {
            get { return _BaseCalculoICMS_ST; }
            set { _BaseCalculoICMS_ST = value; }
        }

        /// <summary>
        /// ICMS retido pelo substituto (com 2  decimais)
        /// </summary>
        public Decimal ValorICMSRetido
        {
            get { return _ValorICMSRetido; }
            set { _ValorICMSRetido = value; }
        }

        /// <summary>
        /// Soma das despesas acessórias (frete,  seguro e outras - com 2 decimais)
        /// </summary>
        public Decimal ValorDespesas
        {
            get { return _ValorDespesas; }
            set { _ValorDespesas = value; }
        }

        /// <summary>
        /// Situação da nota fiscal quanto ao cancelamento (S-cancelada, N-não cancelada)
        /// </summary>
        public string Situacao
        {
            get { return _Situacao; }
            set { _Situacao = value; }
        }

        /// <summary>
        /// Código que identifica o tipo de antecipação tributária
        /// </summary>
        public string CodigoAntecipacao
        {
            get { return _CodigoAntecipacao; }
            set { _CodigoAntecipacao = value; }
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
            linha = linha.Insert(2, String.Format("{0,-14}", (String.IsNullOrEmpty(this.CNPJ) == false ? String.Format("{0:00000000000000}", Convert.ToInt64(this.CNPJ)) : "00000000000000")));
            linha = linha.Insert(16, String.Format("{0,-14}", (this.InscricaoEstadual != null ? this.InscricaoEstadual : "")));
            linha = linha.Insert(30, String.Format("{0,-8}", (this.Data != null ? String.Format("{0:yyyyMMdd}", this.Data) : "")));
            linha = linha.Insert(38, String.Format("{0,-2}", (this.Estado != null ? this.Estado : "")));
            linha = linha.Insert(40, String.Format("{0:D2}", this.Modelo));
            linha = linha.Insert(42, String.Format("{0,-3}", (this.Serie != null ? this.Serie : "")));
            linha = linha.Insert(45, String.Format("{0:D6}", this.NumeroNF));
            linha = linha.Insert(51, String.Format("{0:D4}", this.CFOP));
            linha = linha.Insert(55, String.Format("{0,-1}", (this.Emitente != null ? this.Emitente : "")));
            linha = linha.Insert(56, String.Format("{0:D13}", Convert.ToInt32((this.BaseCalculoICMS_ST * 100))));
            linha = linha.Insert(69, String.Format("{0:D13}", Convert.ToInt32((this.ValorICMSRetido * 100))));
            linha = linha.Insert(82, String.Format("{0:D13}", Convert.ToInt32((this.ValorDespesas * 100))));
            linha = linha.Insert(95, String.Format("{0,-1}", (this.Situacao != null ? this.Situacao : "")));
            linha = linha.Insert(96, String.Format("{0,-1}", (this.CodigoAntecipacao != null ? this.CodigoAntecipacao : "")));
            linha = linha.Insert(97, String.Format("{0,29}", ""));

            return linha;
        }

        #endregion
    }
}

