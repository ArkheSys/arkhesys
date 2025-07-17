using System.ComponentModel;

namespace cwkGestao.Integracao.ACBr.Core.Boleto
{
    public enum ACBrTipoCarteira
    {
        [Description("0 - Carteira Simples")] tctSimples = 0,
        [Description("1 - Carteira Registrada")] tctRegistrada = 1,
        [Description("2 - Carteira Eletronica")] tctEletronica = 2
    }
}