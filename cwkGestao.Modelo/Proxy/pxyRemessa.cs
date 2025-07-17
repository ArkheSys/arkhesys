using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRemessa
    {
        public pxyRemessa()
        {

        }

        public pxyRemessa(int id, int codigo, DateTime? vencimento, string cliente, string documento
                        , decimal? valor, decimal? saldo, string situacao, string filial, string fantasia
                        , int tipoDocumento, int? parcela, int? qtdParcela, DateTime? dataEmissao, string tipoDocumentoCob, string tipoCobranca)
        {
            ID = id;
            Codigo = codigo;
            DataVencimento = vencimento;
            Cliente = cliente;
            NumeroDocumento = documento;
            Valor = valor;
            Saldo = saldo;
            Situacao = situacao;
            Filial = filial;
            NomeFantasia = fantasia;
            TipoDocumento = ((TipoDocumentoType)tipoDocumento).ToString();
            Parcela = parcela;
            QtdParcela = qtdParcela;
            DataEmissao = dataEmissao;
            TipoDocumentoCob = tipoDocumentoCob;
            TipoCobranca = tipoCobranca;
        }

        public int ID { get; set; }//Documento
        public int Codigo { get; set; }//Documento
        public string Filial { get; set; }//Pessoa
        public string Cliente { get; set; }//Pessoa
        public string Situacao { get; set; }//Documento
        public string NomeFantasia { get; set; }//Pessoa
        public int? Parcela { get; set; } //Documento
        public DateTime? DataVencimento { get; set; }//Documento
        public DateTime? DataEmissao { get; set; }//Documento
        public string NumeroDocumento { get; set; }//Documento
        public Decimal? Valor { get; set; }//Documento
        public Decimal? Saldo { get; set; }//Documento
        public int? QtdParcela { get; set; } //Documento
        public string TipoDocumento { get; set; }//Tipo Documento
        public bool Selecionado { get; set; }
        public string TipoDocumentoCob { get; set; } //Tabela Tipo Documento
        public string TipoCobranca { get; set; } //Portador
    }
}
