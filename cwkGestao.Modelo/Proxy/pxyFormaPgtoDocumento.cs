using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyFormaPgtoDocumento
    {
        public pxyFormaPgtoDocumento(Int32 codigo, String filial, String documento, String parcela, Int32 banco,
            String tipoDocumento, String cliente, DateTime? vencimento, Decimal valorMovimento, Decimal? formaMovPgto, String formaPagamento,
            Int32 sequenciaMovimento, Int32 iDMovimento, Decimal? valorFormaPgtoMov)
        {
            Codigo = codigo;
            Filial = filial;
            Documento = documento;
            Parcela = parcela;
            Banco = banco;
            TipoDocumento = tipoDocumento;
            Cliente = cliente;
            Vencimento = vencimento;
            ValorMovimento = valorMovimento;
            FormaMovPgto = formaMovPgto;
            FormaPagamento =   formaPagamento;
            SequenciaMovimento = sequenciaMovimento;
            IDMovimento = iDMovimento;
            ValorFormaPgtoMov = valorFormaPgtoMov;
        }

        public Int32        Codigo              { get; set; }
        public String       Filial              { get; set; }
        public String       Documento           { get; set; }
        public String       Parcela             { get; set; }
        public Int32        Banco               { get; set; }
        public String       TipoDocumento       { get; set; }
        public String       Cliente             { get; set; }
        public DateTime?    Vencimento          { get; set; }
        public Decimal      ValorMovimento      { get; set; }
        public Decimal?     FormaMovPgto        { get; set; }
        public String       FormaPagamento      { get; set; }
        public Int32        SequenciaMovimento  { get; set; }
        public Int32        IDMovimento         { get; set; }
        public Decimal?     ValorFormaPgtoMov   { get; set; }
    }
}
