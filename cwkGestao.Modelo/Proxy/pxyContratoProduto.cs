using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyContratoProduto
    {
        public virtual Boolean Selecionado { get; set; }
        public virtual int IDContrato { get; set; }
        public virtual IList<ContratoProduto> ContratoProcuto { get; set; }

        public virtual String Cliente { get; set; }
        public DateTime Vencimento { get; set; }
        public Decimal Valor { get; set; }
        public String Observacao { get; set; }

        public pxyContratoProduto() { }

        public pxyContratoProduto(int idContrato, IList<ContratoProduto> contratoProcuto, String cliente, DateTime vencimento, Decimal valor, String observacao)
        {
            this.IDContrato = idContrato;
            this.ContratoProcuto = contratoProcuto;
            this.Cliente = cliente;
            this.Vencimento = vencimento;
            this.Valor = valor;
            this.Observacao = observacao;
        }

      
    }
}
