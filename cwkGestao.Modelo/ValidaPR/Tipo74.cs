using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo74
    {
        #region AtributosPrivados
        private string _tipo;
        private DateTime _dataInventario;
        private string _codigoProduto;
        private decimal _quantitade;
        private decimal _valorProduto;
        private string _codigoPosse;
        private string _cnpj;
        private string _inscricaoEstProprietario;
        private string _uf;
        #endregion

        #region Construtor
        public Tipo74()
        {
            _tipo = "74";
            _quantitade = 0;
            _valorProduto = 0;

        }
        #endregion

        #region Métodos
        ///<sumary>
        ///Tipo"75"
        ///</sumary>

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        /// <summary>
        /// Data inicial do período de validade  das informações
        /// </summary>
        public DateTime DataInventario
        {
            get { return _dataInventario; }
            set { _dataInventario = value; }
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
        /// quantidade do produto
        /// </summary>
        public decimal Quantidade
        {
            get { return _quantitade; }
            set { _quantitade = value; }
        }

        /// <summary>
        /// valor do Produto
        /// </summary>
        public decimal ValorProduto
        {
            get { return _valorProduto; }
            set { _valorProduto = value; }
        }

        /// <summary>
        /// Código de Posse das  Mercadorias  Inventariadas
        /// </summary>
        public string codigoPosse
        {
            get { return _codigoPosse; }
            set { _codigoPosse = value; }
        }

        /// <summary>
        ///  CNPJ do Possuidor / Proprietário
        /// </summary>
        public string CNPJ
        {
            get
            {
                return _cnpj;
            }
            set
            {
                if (value != null)
                    _cnpj = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                else
                    _cnpj = "";
            }
        }

        /// <summary>
        ///  Inscrição Estadual do  Possuidor/ Proprietári
        /// </summary>
        public string InscricaoProprietario
        {
            get { return _inscricaoEstProprietario; }
            set
            {
                if (value != null)
                    _inscricaoEstProprietario = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                else
                    _inscricaoEstProprietario = "";
            }
       }

        /// <summary>
        ///  UF do Possuidor/  Proprietário
        /// </summary>
        public string UF
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public override string ToString()
        {
            string linha = new string(' ', 126);

            linha = linha.Insert(0, String.Format("{0,-2}", this.Tipo));
            linha = linha.Insert(2, String.Format("{0,-8}", (this.DataInventario != null ? String.Format("{0:yyyyMMdd}", this.DataInventario) : "")));
            linha = linha.Insert(10, String.Format("{0,-14}", (this.CodigoProduto != null ? this.CodigoProduto : "")));
            linha = linha.Insert(24, String.Format("{0:D13}", Convert.ToInt32((this.Quantidade * 1000))));
            linha = linha.Insert(37, String.Format("{0:D13}", Convert.ToInt32((this.ValorProduto * 100))));
            linha = linha.Insert(50, String.Format("{0:-1}", this.codigoPosse));
            linha = linha.Insert(51, String.Format("{0,-14}", (String.IsNullOrEmpty(this.CNPJ) == false ? String.Format("{0:00000000000000}", Convert.ToInt64(this.CNPJ)) : "00000000000000")));
            linha = linha.Insert(65, String.Format("{0:-14}", this.InscricaoProprietario));
            linha = linha.Insert(79, String.Format("{0:-2}", this.UF));
            linha = linha.Insert(125, String.Format("{0:-1}", ""));

            return linha.TrimEnd();
        }
        #endregion

    }

}
