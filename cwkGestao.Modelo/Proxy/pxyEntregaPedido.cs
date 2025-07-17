using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEntregaPedido
    {
        public pxyEntregaPedido(int id, int codigo, int numero, string pessoaNome, StatusOrcamento status,
                                StatusEntregaType statusEntrega, decimal total, decimal qtdItens, decimal? saldoQtdItens)
        {
            this.ID = id;
            this.Codigo = codigo;
            this.Numero = numero;
            this.PessoaNome = pessoaNome;
            this.Status = status;
            this.StatusEntrega = statusEntrega;
            this.TotalPedido = total;
            this.QtdItens = qtdItens;
            this.SaldoQtdItens = saldoQtdItens == null ? qtdItens : saldoQtdItens;
        }

        public int ID { get; set; }
        public int Codigo { get; set; }
        public int Numero { get; set; }
        public string PessoaNome { get; set; }
        public StatusOrcamento Status { get; set; }
        public StatusEntregaType StatusEntrega { get; set; }
        public decimal TotalPedido { get; set; }
        public decimal QtdItens { get; set; }
        public decimal? SaldoQtdItens { get; set; }

        public virtual string StatusEntregaVisivel
        {
            get
            {
                switch (StatusEntrega)
                {
                    case StatusEntregaType.Aberta:
                        return "Aberta";
                    case StatusEntregaType.Parcial:
                        return "Parcial";
                    case StatusEntregaType.Concluída:
                        return "Concluída";
                    default:
                        return "";
                }
            }
        }

        public virtual string StatusExibicao
        {
            get
            {
                if (Status == StatusOrcamento.AguardandoAprovacao)
                    return "Aguardando Aprovação";

                if (Status == StatusOrcamento.AguardandoExpedicao)
                    return "Aguardando Expedição";

                if (Status == StatusOrcamento.AguardandoFaturamento)
                    return "Aguardando Faturamento";

                if (Status == StatusOrcamento.AguardandoPagamento)
                    return "Aguardando Pagamento";

                if (Status == StatusOrcamento.Expedicao)
                    return "Expedição";

                if (Status == StatusOrcamento.FaturadoParcial)
                    return "Faturado Parcial";

                return Status.ToString();
            }
        }

      
    }
}
