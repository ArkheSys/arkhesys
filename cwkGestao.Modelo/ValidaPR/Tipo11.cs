using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo11
    {
        #region AtributosPrivados
        private string _Tipo;
        private string _Logradouro;
        private int _Numero;
        private string _Complemento;
        private string _Bairro;
        private string _Cep;
        private string _Contato;
        private string _Telefone;
        #endregion

        #region Construtor
        public Tipo11()
        {
            this._Tipo = "11";
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Fixo 11 (Somente Leitura)
        /// </summary>
        public string Tipo
        {
            get
            {
                return _Tipo;
            }
        }

        /// <summary>
        /// Endereço da Filial
        /// </summary>
        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Número do endereço da Filial
        /// </summary>
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        /// <summary>
        /// Complemento do endereço da Filial
        /// </summary>
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Bairro do endereço da Filial
        /// </summary>
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Cep do endereçco da Filial
        /// </summary>
        public string Cep
        {
            get { return _Cep; }
            set { _Cep = value.Replace("-", "").Replace(".", ""); }
        }

        /// <summary>
        /// Nome do Responsável na Filial
        /// </summary>
        public string Contato
        {
            get { return _Contato; }
            set { _Contato = Util.StringUtil.RetiraAcentos(value); }
        }

        /// <summary>
        /// Telefone da Filial
        /// </summary>
        public string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value != null ? (value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")).TrimEnd() : ""; }
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
            linha = linha.Insert(2, String.Format("{0,-34}", this.Logradouro != null ? this.Logradouro.Trim().Substring(0, (this.Logradouro.Length < 34 ? this.Logradouro.Length : 34)) : ""));
            linha = linha.Insert(36, String.Format("{0:D5}", this.Numero));
            linha = linha.Insert(41, String.Format("{0,-22}", this.Complemento != null ? this.Complemento.Trim().Substring(0, (this.Complemento.Length < 22 ? this.Complemento.Length : 22)) : ""));
            linha = linha.Insert(63, String.Format("{0,-15}", this.Bairro != null ? this.Bairro.Trim().Substring(0, (this.Bairro.Length < 15 ? this.Bairro.Length : 15)) : ""));
            linha = linha.Insert(78, String.Format("{0,-8}", this.Cep != null ? this.Cep.Trim().Substring(0, (this.Cep.Length < 8 ? this.Cep.Length : 8)) : ""));
            linha = linha.Insert(86, String.Format("{0,-28}", this.Contato != null ? this.Contato.Trim().Substring(0, (this.Contato.Length < 28 ? this.Contato.Length : 28)) : ""));
            linha = linha.Insert(114, String.Format("{0:D12}", "00" + (this.Telefone != null ? this.Telefone.Trim().Substring(0, 10) : "")));

            return linha;
        }
        #endregion
    }
}
