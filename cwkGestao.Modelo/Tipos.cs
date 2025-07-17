using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public enum Acao
    {
        Incluir = 1,
        Alterar = 2,
        Excluir = 3,
        Consultar = 4,
        NaoPersistir = 30
    }

    public enum TituloType
    {
        Sim = 1,
        Não = 0
    }

    public enum OrigemMovimentoType
    {
        Financeiro = 1,
        Faturamento = 2,
        Caixa = 3
    }

    public enum TipoPrecoType
    {
        Normal = 1,
        Mínimo = 2,
        Máximo = 3
    }

    public enum FormaPagamentoTipo
    {
        Outras = 0,
        Cheque = 1,
        Cartao = 2,
        Boleto = 3
    }

    public enum OperacaoMovimentoType
    {
        Lancamento = 1,
        BxParcial = 2,
        BxTotal = 3,
        TituloDescontado = 4,
        Cancelado = 6,
        EstBxPArcial = 12,
        EstBxTotal = 13,
        Troco = 89,
        Retencao = 7,
        BxRenegociação = 8
    }

    public enum TipoMovimentoType
    {
        Documento = 1,
        Multa = 2,
        Juro = 3,
        Troco = 4,
        Desconto = 6,
        Imposto = 7,
        JurosDesconto = 8,
        TaxaBoleto = 9,
        IOF = 10,
        TaxaDescontoCartao = 11
    }

    public enum InOutType
    {
        Entrada = 1,
        Saída = 2
    }

    public enum OrcamentoStatus
    {
        Aberto = 0,
        AguardandoFaturamento = 1,
        AguardandoRevenda = 2,
        Cancelado = 3,
        Faturado = 4
    }

    public enum StatusOrcamento
    {
        Aberto = 1,
        Faturado = 2,
        Cancelado = 3,
        FaturadoParcial = 4,
        Fechado = 5,
        AguardandoExpedicao = 6,
        Expedicao = 7,
        AguardandoFaturamento = 8,
        AguardandoPagamento = 9,
        AguardandoAprovacao = 10
    }

    public enum StatusPedidoParaAprovar
    {
        AguardandoExpedicao = 1,
        AguardandoFaturamento = 2
    }

    public enum TipoDocumentoType
    {
        Receber = 1,
        Pagar = 2
    }

    public enum TipoUsuario
    {
        Administrador = 0,
        Operador = 1,
        Gerente = 2
    }

    public enum VencimentoFeriado { Nada = 0, Adiantar = 1, Adiar = 2 }

    public enum TipoDocumentoGrid { Cliente = 0, Fornecedor = 1 }

    public enum DebCreType
    {
        Entrada = 1,
        Saída = 2
    }

    public enum TipoRelatorioProduto
    {
        Codigo = 0,
        Nome = 1,
        GrupoEstoque = 2
    }

    public enum TipoRelatorioEquipamento
    {
        Codigo = 0,
        Descricao = 1,
        TipoEquipamento = 2
    }

    public enum TipoPedidoType
    {
        Orçamento = 1,
        Pedido = 2,
        Requisição = 3
    }

    public enum StatusEntregaType
    {
        Aberta = 0,
        Parcial = 1,
        Concluída = 2
    }

    public enum TipoPedidoCategoriaMovimentacao
    {
        Produtos = 0,
        Servicos = 1,
        ProdutosEServicos = 2
    }

    public enum TipoImpressao
    {
        Retrato = 0,
        Paisagem = 1
    }

    public enum DestinoImpressao
    {
        Vídeo = 0,
        Impressora = 1,
        Email = 2
    }

    public enum TipoFrete
    {
        Emitente = 0,
        Destinatario = 1,
        Terceiros = 2,
        ProprioRemetente = 3,
        ProprioDestinatario = 4,
        SemFrete = 9
    }

    public enum ConsiderarPedidos
    {
        Nenhum = 0,
        Compra = 2,
        Venda = 4,
        Ambos = 6
    }

    public enum Ent_SaiMovCaixa
    {
        Entrada = 0,
        Saída = 1
    }

    public enum TipoDataType
    {
        Dia = 1,
        Mês = 2
    }

    public enum AdesaoNFe
    {
        Total = 0,
        Parcial = 1
    }

    public enum TipoST
    {
        Substituído = 0,
        Substituto = 1
    }

    public enum OrigemNFType
    {
        Própria = 1,
        Terceiro = 0
    }

    public enum ProdutoServicoType
    {
        Produtos = 1,
        Servicos = 2,
        ProdutosEServicos = 3
    }

    public enum TipoCotacao
    {
        Lista = 0,
        Cotação = 1
    }

    public enum ModeloImpressaoGraficaType
    {
        Modelo0 = 0,
        Modelo1 = 1,
        Modelo2 = 2,
        Romaneio = 3,
        Modelo3 = 4,
        Modelo4 = 5,
        Iguatu = 6,
        Modelo5 = 7,
        Modelo6 = 8,
        Modelo7 = 9,
        DiMaggio = 10,
        Modelo8 = 11,
        Modelo9 = 12
    }

    public enum ModeloImpressaoPedidoCompraType
    {
        Modelo0 = 0,
        Modelo1 = 1,
        Modelo2 = 2
    }

    public enum ModeloImpressaoOS
    {
        Modelo0 = 0,
        Modelo1 = 1
    }

    public enum ModeloDuplicata
    {
        Modelo0 = 0,
        Modelo1 = 1
    }

    public enum TipoArquivoLigacoes
    {
        GVT = 0,
        SUMUS = 1,
        SUMUS2 = 2,
        SERCONTEL = 3,
        ALGAR = 4
    }

    public enum TipoOfd_MaquinasType
    {
        Helice = 1,
        Estacao = 2,
        Strauss = 3,
        Perfuratriz = 4
    }

    public enum StatusObraFundacao
    {
        Aberta = 0,
        Executando = 1,
        Pendente = 2,
        Medicao = 3,
        Concluida = 4,
        Cancelada = 5
    }

    public enum ServidorSHL
    {
        Homologação = 0,
        Produção = 1
    }


    public enum StatusPropostaObraFundacao
    {
        Aberta = 0,
        AguardandoAprovacao = 1,
        Cancelada = 2,
        Aprovada = 3,
        Executando = 4,
        MedicaoFinal = 5,
        Concluida = 6,
        EmBranco = 7
    }

    public enum StatusOrcamentoObraFundacao
    {
        Aberto = 0,
        EmAnalise = 1,
        Executando = 2,
        Concluído = 3,
        Enviado = 4,
        Cancelado = 5,
        EmBranco = 6,
        PropostaCriada = 7
    }



    public enum TipoPrecoObraFundacao
    {
        ValorFechado = 0,
        MetroLinear = 1,
        MetroCubico = 2,
        EmBranco = 3
    }

    public enum TipoDiametro
    {
        _0_25 = 0,
        _0_30 = 1,
        _0_32 = 2,
        _0_38 = 3,
        _0_35 = 4,
        _0_40 = 5,
        _0_45 = 6,
        _0_50 = 7,
        _0_60 = 8,
        _0_70 = 9,
        _0_80 = 10,
        _0_90 = 11,
        _1_00 = 12,
        _1_10 = 13,
        _1_20 = 14,
        _1_30 = 15,
        _1_40 = 16,
        _1_50 = 17,
        _1_60 = 18
    }

    public enum TipoArquivoObra
    {
        Word = 0,
        Excel = 1,
        Imagem = 2,
        PDF = 3,
        CAD = 4
    }

    public enum TipoImposto
    {
        Municipal = 0, Estadual = 1, Federal = 2
    }

    public enum TipoRetencao
    {
        LocalPrestacaoServico = 0, ValorMinimo = 1, LimiarMes = 2
    }

    public enum _TipoImposto
    {
        Iss = 1,
        Inss = 2,
        Ir = 3,
        Csll = 4,
        Pis = 5,
        Cofins = 6
    }

    public enum FormaEmissaoNFe
    {
        Normal = 1,
        Contigencia = 2
    }

    public enum TipoValorProduto
    {
        UltimoCusto = 0,
        CustoMedio = 1,
        PrecoBase = 2
    }

    public enum ClassificacaoProduto
    {
        MateriaPrima = 0,
        ProdutoAcabado = 1,
        Imobilizado = 2,
        Subproduto = 3,
        MercadoriaParaRevenda = 4,
        Embalagem = 5,
        ProdutoEmProcesso = 6,
        Produtointermediario = 7,
        MaterialDeusoEConsumo = 8,
        Servicos = 9,
        OutrosInsumos = 10,
        Outras = 11
    }

    public enum ModoBaixaEstoqueType
    {
        PEPS = 0,
        UEPS = 1
    }

    public enum PeriodoListagemFaturasTelefonia
    {
        MesAtual = 0,
        UltimoMes = 1,
        Ultimos2Meses = 2,
        Ultimos3Meses = 3,
        Ultimos4Meses = 4,
        Ultimos5Meses = 5,
        Todos = 6,
    }

    public enum TipoJuroType
    {
        Percentual = 0,
        Real = 1
    }

    public enum StatusPedidoDelivery
    {
        Aberto = 0,
        Pendente = 1,
        EmPreparacao = 2,
        EmEntrega = 3,
        Finalizado = 4,

        Cancelado = 5
    }
}