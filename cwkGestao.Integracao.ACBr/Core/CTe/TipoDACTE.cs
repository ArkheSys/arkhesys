using System.ComponentModel;

namespace cwkGestao.Integracao.ACBr.Core.CTe
{
    public enum TipoDACTE
    {
        [Description("Sem Impressão")]
        tiSemGeracao = 0,

        [Description("Normal Retrato")]
        tiRetrato = 1,

        [Description("Normal Paisagem")]
        tiPaisagem = 2
    }
}