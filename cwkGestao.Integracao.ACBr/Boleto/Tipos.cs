namespace cwkGestao.Integracao.ACBr.Boleto
{
    public enum OcorrenciaType
    {
        EntradaConfirmada = 2,
        EntradaRejeitada = 3,
        Liquidação = 6,
        Baixa = 9,
        LiquidaçãoBaixaTituloNaoRegistrado = 17,
        Outras = 99,
        TransferênciaCarteira = 4
    }

    public enum TipoJuroType
    {
        Percentual = 0,
        Real = 1
    }
}