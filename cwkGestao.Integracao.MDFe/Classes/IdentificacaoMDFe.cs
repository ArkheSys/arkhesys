using System;

namespace cwkGestao.Integracao.MDFe.Classes
{
    public class IdentificacaoMDFe
    {
        public int Codigo { get; set; }
        public int DocumentoFiscal { get; set; }
        public int Serie { get; set; }
        public int Veiculo { get; set; }
        public DateTime DataEmissao { get; set; } 
        public int Hora { get; set; }
        public int Condutor { get; set; }
        public int CidadeCarga { get; set; }
        public int CidadeDescarga { get; set; }
        public int TipoEmitente { get; set; }
        public int TipoEmissao { get; set; }
        public int TipoTransportador { get; set; }
        public int Modalidade { get; set; }
        public string CIOT { get; set; }
        //Documento
        public string Numero { get; set; }
        public int Destinatario { get; set; }
        public decimal Total { get; set; }
        //Reboque - Pode utilizar o Cadastro de Veículo como Reboque
        //UF Percurso
        public int UFSigla { get; set; }
        public string UFNome { get; set; }
        //Vale Pedágio
        public string Fornecedora { get; set; }
        public string NumeroComprovante { get; set; }
        public decimal Valor { get; set; }
        public string Responsvel { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        //Seguros
        public string Responsavel { get; set; }
        public string NomeResponsavel { get; set; }
        public string NomeSeguradora { get; set; }
        public string NumeroAverbacao { get; set; }
        public string NumeroApolice { get; set; }
        //Contratantes
        public string Contratante { get; set; }
        //Outras
        public string InformacoesComplementares { get; set; }
        public string InformacoesInteresseFisco { get; set; }
        //Valores da MDFe
        public string QuantidadeNfe { get; set; }
        public string QuantidadeCte { get; set; }
        public decimal ValorTotalCarga { get; set; }
        public decimal PesoTotalCarga { get; set; }
    }
}
