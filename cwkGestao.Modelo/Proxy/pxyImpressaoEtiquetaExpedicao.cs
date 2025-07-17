using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyImpressaoEtiquetaExpedicao
    {
        public pxyImpressaoEtiquetaExpedicao(int id, int codigopedido, DateTime datapedido,
                                             string codigorastreamento, string cep, string notafiscal, string volume, string destinatario,
                                             string endereco, string complementobairro, string cidadeestado, string peso,
                                             string remetente, string endremetente, string complementobairroremetente, string cepremetente,
                                             string cidadeestadoremetente, string observacao, string caminhoImagem)
        {
            ID = id;
            Imprimir = false;
            QuantidadeImprimir = 1;
            CodigoPedido = codigopedido;
            DataPedido = datapedido;
            CodigoRastreamento = codigorastreamento;
            Cep = String.IsNullOrEmpty(cep) ? "" : cep.Replace("-", "");
            NotaFiscal = notafiscal;
            Volume = volume;
            Destinatario = destinatario;
            Endereco = endereco;
            ComplementoBairro = complementobairro;
            CidadeEstado = cidadeestado;
            CepFormatado = String.Format("{0:00000-000}", Convert.ToInt64(String.IsNullOrEmpty(cep) ? "0" : cep.Replace("-", "")));
            Peso = peso;
            Remetente = remetente;
            EndRemetente = endremetente;
            CompelementoBairroRemetente = complementobairroremetente;
            CepRemetente = cepremetente;
            CidadeEstadoRemetente = cidadeestadoremetente;
            Observacao = observacao;
            CaminhoImagem = caminhoImagem;
        }

        public virtual int ID { get; set; }

        [InformacaoExibicao(1, "Imprimir", 75)]
        public virtual bool Imprimir { get; set; }

        [InformacaoExibicao(2, "Qtd.", 75)]
        public virtual decimal QuantidadeImprimir { get; set; }

        [InformacaoExibicao(3, "Pedido", 100)]
        public virtual int CodigoPedido { get; set; }

        [InformacaoExibicao(4, "Data", 120)]
        public virtual DateTime DataPedido { get; set; }

        [InformacaoExibicao(5, "Rastreamento", 120)]
        public virtual string CodigoRastreamento { get; set; }
        public virtual string Cep { get; set; }

        [InformacaoExibicao(6, "NFe", 100)]
        public virtual string NotaFiscal { get; set; }
        public virtual string Volume { get; set; }

        [InformacaoExibicao(7, "Destinatário", 320)]
        public virtual string Destinatario { get; set; }

        public virtual string Endereco { get; set; }
        public virtual string ComplementoBairro { get; set; }

        [InformacaoExibicao(8, "Cidade/Estado", 220)]
        public virtual string CidadeEstado { get; set; }

        [InformacaoExibicao(9, "CEP", 100)]
        public virtual string CepFormatado { get; set; }

        [InformacaoExibicao(10, "Peso", 120)]
        public virtual string Peso { get; set; }
        public virtual string Remetente { get; set; }
        public virtual string EndRemetente { get; set; }
        public virtual string CompelementoBairroRemetente { get; set; }
        public virtual string CepRemetente { get; set; }
        public virtual string CidadeEstadoRemetente { get; set; }

        [InformacaoExibicao(11, "Observação", 400)]
        public virtual string Observacao { get; set; }

        [InformacaoExibicao(12, "Caminho da Imagem", 400)]
        public virtual string CaminhoImagem { get; set; }

    }
}
