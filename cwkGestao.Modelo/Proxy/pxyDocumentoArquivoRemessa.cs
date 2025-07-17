using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyDocumentoArquivoRemessa
    {

        public pxyDocumentoArquivoRemessa()
        {

        }
        public pxyDocumentoArquivoRemessa(int id, int codigo, string nomeFilial, string nomeCliente, string situacao, string nomeFantasia, int parcela,
                                            DateTime? dataVencimento, string numeroDocumento, decimal? valor, decimal? saldo, int? qtdParcela, int? idRemessa, string tipoDocumentoCob, string tipoCobranca)
        {

            ID = id;
            Codigo = codigo;  
            Filial = nomeFilial;
            Cliente = nomeCliente;
            Situacao = situacao;
            NomeFantasia = nomeFantasia;
            Parcela = parcela;
            DataVencimento = dataVencimento;
            NumeroDocumento = numeroDocumento;
            Valor = valor;
            Saldo = saldo;
            QtdParcela = qtdParcela;
            IDRemessa = idRemessa == null ? 0 : idRemessa;
            TipoDocumentoCob = tipoDocumentoCob;
            TipoCobranca = tipoCobranca;
        }

        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Filial { get; set; }
        public string Cliente { get; set; }
        public string Situacao { get; set; }
        public string NomeFantasia { get; set; }
        public int Parcela { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Saldo { get; set; }
        public int? QtdParcela { get; set; }
        public int? IDRemessa { get; set; }
        public bool Selecionado { get; set; }
        public string TipoDocumentoCob { get; set; }
        public string TipoCobranca { get; set; }

    }
}
