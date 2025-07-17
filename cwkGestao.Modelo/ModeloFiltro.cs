using System.ComponentModel;

namespace cwkGestao.Modelo
{
    public enum ModeloFiltro
    {
        [Description("00 - Todos")] Todos = 0,
        [Description("01 - Dia Atual")] DiaAtual = 1,
        [Description("02 - Mês Atual")] MesAtual = 2,
        [Description("03 - Ano Atual")] AnoAtual = 3,
        [Description("04 - 1 mês Antes/Depois")] UmMesAntesDepois = 4,
        [Description("05 - 3 meses Antes/Depois")] TresMesesAntesDepois = 5,
        [Description("06 - 6 meses Antes/Depois")] SeisMesesAntesDepois = 6,
        [Description("07 - 12 meses Antes/Depois")] DozeMesesAntesDepois = 7,
        [Description("08 - Últimos 2 anos")] DoisAnosAnterior = 8,
        [Description("09 - Últimos 3 anos")] TresAnosAnterior = 9,
        [Description("10 - Últimos 4 anos")] QuatroAnosAnterior = 10,
        [Description("11 - Últimos 5 anos")] CincoAnosAnterior = 11,
    }
}