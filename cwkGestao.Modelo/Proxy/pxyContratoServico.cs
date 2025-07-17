using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyContratoServico
    {
        public virtual Boolean Selecionado { get; set; }
        public virtual int IDContrato { get; set; }
        public virtual IList<ContratoServico> ContratoServico { get; set; }

        public virtual String Cliente { get; set; }
        public DateTime Vencimento { get; set; }
        public Decimal Valor { get; set; }
        public String Observacao { get; set; }

        public pxyContratoServico() { }

        public pxyContratoServico(int idContrato, IList<ContratoServico> contratoServico, String cliente, string vencimento, Decimal valor, String observacao)
        {
            this.IDContrato = idContrato;
            this.ContratoServico = contratoServico;
            this.Cliente = cliente;
            this.Vencimento = Convert.ToDateTime(vencimento);
            this.Valor = valor;
            this.Observacao = observacao;
        }
    }
}
