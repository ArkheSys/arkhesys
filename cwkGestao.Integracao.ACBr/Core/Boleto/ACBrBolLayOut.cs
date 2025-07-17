using System.ComponentModel;

namespace cwkGestao.Integracao.ACBr.Core.Boleto
{
    public enum ACBrBolLayOut
    {
        [Description("0 - Padrão")] lPadrao = 0,
        [Description("1 - Carnê")] lCarne = 1,
        [Description("2 - Fatura")] lFatura = 2,
        [Description("3 - Padrão Entrega")] lPadraoEntrega = 3,
        [Description("4 - Recibo no Topo")] lReciboTopo = 4,
        [Description("5 - Padrão Entrega 2")] lPadraoEntrega2 = 5,
        [Description("6 - Fatura Detalhe")] lFaturaDetal = 6
    }
}