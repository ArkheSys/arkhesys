using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    [InformacaoExibicao("Forma de Pagamento/Recebimento")]
    public class pxyFormaPagamentoBaixaDoc : cwkGestao.Modelo.ModeloBase
    {
        public FormaPagamento FormaPagamento { get; set; }
        public DateTime Data { get; set; }
        public Boolean ChequeTerceiro { get; set; }
        public Decimal Valor { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string NumeroCheque { get; set; }
        public string CNPJCPFEmitente { get; set; }
        public string NomeEmitente { get; set; }
        public cwkGestao.Modelo.Cheque.StatusCheque Status { get; set; }

        public int Parcela { get; set; }
        public int? BandeiraCartao { get; set; }
        public string Operadora { get; set; }
        public string CAut { get; set; }

        public override int ID
        {
            get;
            set;
        }

        public Cheque Cheque { get; set; }

        public static String PegaNomeFormaPagamento(List<pxyFormaPagamentoBaixaDoc> formasPagamento)
        {
            String retorno = String.Join("|", formasPagamento.Select(s => s.FormaPagamento));
           
            return retorno;
        }

        public virtual TipoDocumento TipoDocumento { get; set; }

        public virtual int QuantidadeParcelas { get; set; }

    }
}
