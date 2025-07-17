using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo10
    {
        #region AtributosPrivados
        private string _Tipo;
        private string _CNPJ;
        private string _InscricaoEstadual;
        private string _Contribuinte;
        private string _Municipio;
        private string _Estado;
        private string _Fax;
        private DateTime _DataInicial;
        private DateTime _DataFinal;
        private string _CodigoConvenio;
        private string _CodigoIdentificacao;
        private string _CodigoFinalidade;
        #endregion

        #region Contrutor
        public Tipo10()
        {
            _Tipo = "10";
            _CodigoConvenio = "3";
            _CodigoIdentificacao = "3";
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Fixo 10 (Somente Leitura)
        /// </summary>
        public string Tipo
        {
            get
            {
                return _Tipo;
            }
        }

        /// <summary>
        /// CNPJ do estabelecimento informante
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
        /// Inscrição estadual do estabelecimento informante
        /// </summary>
        public string InscricaoEstadual
        {
            get { return _InscricaoEstadual; }
            set
            {
                _InscricaoEstadual = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
            }
        }

        /// <summary>
        /// Nome comercial (razão social / denominação) do contribuinte
        /// </summary>
        public string Contribuinte
        {
            get { return _Contribuinte; }
            set { _Contribuinte = value; }
        }

        /// <summary>
        /// Município onde está Domiciliado o estabelecimento informante
        /// </summary>
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Unidade da Federação referente ao  Município
        /// </summary>
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Número do fax do estabelecimento informante
        /// </summary>
        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                _Fax = value != null ? value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : "";
            }
        }

        /// <summary>
        /// A data do início do período referente às informações prestadas
        /// </summary>
        public DateTime DataInicial
        {
            get { return _DataInicial; }
            set { _DataInicial = value; }
        }

        /// <summary>
        /// A data do fim do período referente às informações prestadas
        /// </summary>
        public DateTime DataFinal
        {
            get { return _DataFinal; }
            set { _DataFinal = value; }
        }

        /// <summary>
        /// Fixo 3 (Código de Identificação do convenio 2	Convênio 69/02 / 3	Convênio 76/03) (Somente Leitura)
        /// </summary>
        public string CodigoConvenio
        {
            get { return _CodigoConvenio; }
        }

        /// <summary>
        /// Fixo 3 (3	Totalidade das operações do informante) (Somente Leitura)
        /// </summary>
        public string CodigoIdentificacao
        {
            get { return _CodigoIdentificacao; }
        }

        /// <summary>
        /// Código da finalidade utilizado no   arquivo magnético
        /// </summary>
        public string CodigoFinalidade
        {
            get { return _CodigoFinalidade; }
            set { _CodigoFinalidade = value; }
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
            linha = linha.Insert(2, String.Format("{0,-14}", (this.CNPJ != null ? this.CNPJ : "")));
            linha = linha.Insert(16, String.Format("{0,-14}", (this.InscricaoEstadual != null ? this.InscricaoEstadual : "")));
            linha = linha.Insert(30, String.Format("{0,-35}", (this.Contribuinte != null ? this.Contribuinte.Substring(0, (this.Contribuinte.Length < 35 ? this.Contribuinte.Length : 35)) : "")));
            linha = linha.Insert(65, String.Format("{0,-30}", (this.Municipio != null ? this.Municipio.Substring(0, (this.Municipio.Length < 30 ? this.Municipio.Length : 30)) : "")));
            linha = linha.Insert(95, String.Format("{0,-2}", (this.Estado != null ? this.Estado : "")));
            linha = linha.Insert(97, String.Format("{0,-10}", (this.Fax != null ? (this.Fax.Length > 0 ? this.Fax.Trim().Substring(0, 10) : "") : "")));
            linha = linha.Insert(107, String.Format("{0,-8}", (this.DataInicial != null ? String.Format("{0:yyyyMMdd}", this.DataInicial) : "")));
            linha = linha.Insert(115, String.Format("{0,-8}", (this.DataFinal != null ? String.Format("{0:yyyyMMdd}", this.DataFinal) : "")));
            linha = linha.Insert(123, String.Format("{0,-1}", (this.CodigoConvenio != null ? this.CodigoConvenio : "")));
            linha = linha.Insert(124, String.Format("{0,-1}", (this.CodigoIdentificacao != null ? this.CodigoIdentificacao : "")));
            linha = linha.Insert(125, String.Format("{0,-1}", (this.CodigoFinalidade != null ? this.CodigoFinalidade : "")));

            return linha;
        }
        #endregion
    }

}
