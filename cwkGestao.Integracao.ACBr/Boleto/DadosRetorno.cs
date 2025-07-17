using System;
using System.Collections.Generic;

namespace cwkGestao.Integracao.ACBr.Boleto
{
    public class DadosRetorno
    {
        public string Agencia { get; set; }
        public string Banco { get; set; }
        public string Carteira { get; set; }
        public string CodigoCedente { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string ContaCorrente { get; set; }
        public string DataCredito { get; set; }
        public string DataOcorrencia { get; set; }
        public string NossoNumero { get; set; }
        public string NumeroControle { get; set; }
        public string NumeroDocumento { get; set; }
        public bool Pagamento { get; set; }
        public decimal ValorCredito { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorJurosPago { get; set; }
        public decimal ValorMultaPaga { get; set; }
        public decimal ValorOutrosAcrescimos { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorTaxaCobranca { get; set; }
        public Dictionary<string, string> MotivoOcorrencia { get; set; }
        public List<DadosOcorrencia> DadoOcorrencia { get; set; }
        public string CaminhoArquivo { get; set; }
        public decimal ValorIOF { get; set; }
        public OcorrenciaType Ocorrencia => (OcorrenciaType) Convert.ToInt32(CodigoOcorrencia);
    }
}