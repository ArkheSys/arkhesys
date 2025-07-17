using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Conta Bancária")]
	public class Banco : ModeloBase
	{
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Nome", 710)]
        public virtual string Nome { get; set; }
        public virtual string Ordem { get; set; }
        public virtual string ArquivoDeLicenca { get; set; }
        public virtual string CodigoAgencia { get; set; }
        public virtual string ContaCorrente { get; set; }
        public virtual string CodigoCedente { get; set; }
        public virtual string CaminhoImagens { get; set; }
        public virtual string LayoutBoleto { get; set; }
        public virtual string CaminhoLayoutBoleto { get; set; }
        public virtual string InstrucoesCaixa { get; set; }
        public virtual string Logotipo { get; set; }
        public virtual string LayoutRemessa { get; set; }
        public virtual string LayoutRetorno { get; set; }
        public virtual string LayoutBoletoEmail { get; set; }

        public virtual bool UtilizaFechamentoCaixa { get; set; }
        public virtual IList<MovimentacaoCheque> MovimentacaoCheque { get; set; }
        public virtual IList<MovimentacaoCheque> MovimentacaoChequeDestino { get; set; }
    }
}
		