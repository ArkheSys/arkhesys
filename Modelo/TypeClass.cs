using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Modelo
{
    public enum TituloType
    {
        Sim = 1,
        Não = 0
    }

    public enum DebCreType
    {
        Entrada = 1,
        Saída = 2
    }

    public enum ClassificacaoGrupoEstoqueType
    {
        MatériaPrima = 0,  
        ProdutoAcabado = 1,
        Imobilizado = 2  
    }

    /// <summary>
    /// Entrada = Compra, Saida = Venda
    /// </summary>
    public enum InOutType
    {
        Entrada = 1,
        Saída = 2
    }

    public enum TipoPedidoType
    {
        Orçamento = 1,
        Pedido = 2,
        Requisição = 3
    }

    public enum OrigemNFType
    {
        Própria = 1,
        Terceiro = 0
    }

    public enum TipoPrecoType
    {
        Normal = 1,
        Mínimo = 2,
        Máximo = 3
    }

    public enum TipoDataType
    {
        Dia = 1,
        Mês = 2
    }

    public enum StatusOrcamento
    {
        Aberto = 1,
        Faturado = 2,
        Cancelado = 3,
        FaturadoParcial = 4,
        Fechado = 5
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
        Troco = 89
    }

    public enum OrigemMovimentoType
    {
        Financeiro = 1,
        Faturamento = 2,
        Caixa = 3
    }

    public enum TipoDocumentoType
    {
        Receber = 1,
        Pagar = 2
    }

    public enum TipoMovimentoType
    {
        Documento = 1,
        Multa = 2,
        Juro = 3,
        Troco = 4,
        Cheque = 5,
        Desconto = 6,
        Imposto = 7
    }

    public enum TipoRetorno
    {
        Codigo = 1,
        Motivo = 2
    }

    public enum TipoST
    {
        Substituído = 0,
        Substituto = 1
    }

    public enum FormaEmissaoNFe
    {
        Normal = 1,
        Contigencia = 2,
    }

    public enum Ambiente
    {
        Produção = 1,
        Homologação = 2
    }

    public enum MessageButtonFaturar
    {
        NotaFiscal = 1,
        CupomFiscal = 2,
        NFe = 3
    }

    public enum EntSaiType
    {
        Entrada = 1,
        Saida = 2,
        Ambas = 3
    }

    public struct ImpostoNF
    {
        /// <summary>
        /// Alíquota de ICMS (Efeito de cálculo)
        /// </summary>
        public decimal aliquotaIcms;

        /// <summary>
        /// Alíquota de ICMS (Impressão)
        /// </summary>
        public decimal aliquotaIcmsNormal;

        /// <summary>
        /// Alíquota para Substituição Tributária
        /// </summary>
        public decimal aliquotaSubstituicaoTributaria;

        /// <summary>
        /// Alíquota ref. a Cupom Fiscal
        /// </summary>
        public string aliquotaCupomFiscal;

        /// <summary>
        /// Base de cálculo de icms
        /// </summary>
        public decimal baseIcms;

        /// <summary>
        /// Valor do icms
        /// </summary>
        public decimal valorIcms;

        /// <summary>
        /// Diferença da Redução de ICMS
        /// </summary>
        public decimal diferencaReducaoIcms;

        /// <summary>
        /// Valor isento do icms
        /// </summary>
        public decimal valorIsentoIcms;

        /// <summary>
        /// Valor de outros icms
        /// </summary>
        public decimal valorOutrosIcms;

        /// <summary>
        /// Valor Retido de Icms
        /// </summary>
        public decimal valorRetidoIcms;

        /// <summary>
        /// Base de calculo do icms para substituicao
        /// </summary>
        public decimal baseIcmsSubst;

        /// <summary>
        /// % de lucro para Substituição Tributária
        /// </summary>
        public decimal lucroSubstituicaoTributaria;

        /// <summary>
        /// Texto Lei
        /// </summary>
        public string textoLei;

        /// <summary>
        /// Situação tributária do Produto
        /// </summary>
        public string situacaoTibutaria;

        /// <summary>
        /// Tributação do ICMS - NFe
        /// </summary>
        public string tagCST;

        /// <summary>
        /// Modalidade de determinação da BC do ICMS - NFe
        /// </summary>
        public decimal modBC_N13;

        /// <summary>
        /// Modalidade de determinação da BC do ICMS ST - NFe
        /// </summary>
        public decimal modBCST_N18;

        /// <summary>
        /// Percentual da Redução de BC - NFe
        /// </summary>
        public decimal pRedBC_N14;

        /// <summary>
        /// Código de Situação Tributária do PIS - NFe
        /// </summary>
        public string CST_Pis;

        /// <summary>
        /// Valor da Base de Cálculo do PIS - NFe
        /// </summary>
        public decimal vBC_Q07;

        /// <summary>
        /// Alíquota do PIS (em percentual) - NFe
        /// </summary>
        public decimal pPIS_Q08;

        /// <summary>
        /// Valor do PIS - NFe
        /// </summary>
        public decimal vPIS_Q09;

        /// <summary>
        /// Código de Situação Tributária do COFINS - NFe
        /// </summary>
        public string CST_Cofins;

        /// <summary>
        /// Valor da Base de Cálculo da COFINS - NFe
        /// </summary>
        public decimal vBC_S07;

        /// <summary>
        /// Alíquota da COFINS (empercentual) - NFe
        /// </summary>
        public decimal pCOFINS_S08;

        /// <summary>
        /// Valor do COFINS - NFe
        /// </summary>
        public decimal vCOFINS_S11;

        /// <summary>
        /// Código da situação tributária do IPI - NFe
        /// </summary>
        public string CST_Ipi;

        /// <summary>
        /// Valor da BC do IPI - NFe
        /// </summary>
        public decimal vBC_O10;

        /// <summary>
        /// Alíquota do IPI - NFe
        /// </summary>
        public decimal pIPI_O13;

        /// <summary>
        /// Valor do IPI - NFe
        /// </summary>
        public decimal vIPI_O14;

        /// <summary>
        /// ID da tabela de icms utilizada para calculo
        /// </summary>
        public int IdIcms;

        /// <summary>
        /// Origem da mercadoria - NFe
        /// </summary>
        public int orig_N11;

        /// <summary>
        /// Código de Enquadramento Legal do IPI - NFe
        /// </summary>
        public int cEnq_O06;

        public decimal percRedBCST;
        public decimal pCredSN_N29;
        public decimal vCredICMSSN_N30;

        public decimal AliqInterna;
        public decimal AliqFCP;
        public decimal BCFCP;
        public decimal VFCP;

    }

    public enum DestinoImpressao
    {
        Vídeo = 0,
        Impressora = 1,
        Email = 2
    }

    public enum DestinoImpressaoVendaType
    {
        [DescriptionAttribute("Cancelar")]
        Cancelar = 0,
        [DescriptionAttribute("NFe")]
        NFe = 1,
        [DescriptionAttribute("Controle Venda")]
        ControleVenda = 2
    }

    public enum TipoCotacao
    {
        Lista = 0,
        Cotação = 1
    }
    
    public enum MotivoCancelamentoProjeto
    {
        [Description("Preço")]
        Preço = 0,
        [Description("Capacidade Técnica")]
        CapacidadeTecnica = 1
    }

    public enum StatusOPType
    {
        Abertas = 0
        , Iniciadas = 1
        , Canceladas = 2
        , Fechadas = 3
    }

    public enum FormaPagamentoTipo
    {
        Outras = 0
        ,
        Cheque = 1
    
    }

    public enum VencimentoFeriado { Nada = 0, Adiar = 1, Adiantar = 2 }

    public enum TipoImpressao
    {
        Retrato = 0,
        Paisagem = 1
    }

    public delegate Modelo.DestinoImpressao SelecionaTipoImpressora();


    
}
