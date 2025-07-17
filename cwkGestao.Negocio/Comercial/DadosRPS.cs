using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Comercial
{
    public class DadosRPS
    {
        #region Atributos
        public string IdRps { get; set; }
        public int NumeroRps { get; set; }
        public string SerieRps { get; set; }
        public string TipoRps { get; set; }
        public DateTime DataEmissao { get; set; }
        public int OptanteSimplesNacional { get; set; }
        public int IncentivadorCultural { get; set; }
        public int? SituacaoNota { get; set; }
        public int TipoTributacao { get; set; }
        public int NaturezaTributacao { get; set; }
        public int RegimeEspecialTributacao { get; set; }
        public int? NumeroRpsSubstituido { get; set; }
        public string SerieRpsSubstituido { get; set; }
        public string TipoRpsSubstituido { get; set; }
        public string ExigibilidadeISS { get; set; }
        public decimal? BaseCalculo { get; set; }
        public decimal? ValorServicos { get; set; }
        public decimal? ValorDeducoes { get; set; }
        public decimal? ValorPis { get; set; }
        public decimal? ValorCofins { get; set; }
        public decimal? ValorInss { get; set; }
        public decimal? ValorIr { get; set; }
        public decimal? ValorCsll { get; set; }
        public decimal? ValorIssRetido { get; set; }
        public string IssRetido { get; set; }
        public decimal? ValorIss { get; set; }
        public decimal? ValorLiquidoNfse { get; set; }
        public decimal? DescontoIncondicionado { get; set; }
        public decimal? DescontoCondicionado { get; set; }
        public decimal? AliquotaISS { get; set; }
        public decimal? AliquotaPIS { get; set; }
        public decimal? AliquotaCOFINS { get; set; }
        public decimal? AliquotaINSS { get; set; }
        public decimal? AliquotaIR { get; set; }
        public decimal? AliquotaCSLL { get; set; }
        public string CodigoItemListaServico { get; set; }
        public int? ResponsavelRetencao { get; set; }
        public int CodigoCnae { get; set; }
        public string CodigoTributacaoMunicipio { get; set; }
        public string DiscriminacaoServico { get; set; }
        public string CodigoCidadePrestacao { get; set; }
        public string DescricaoCidadePrestacao { get; set; }
        public string RazaoSocialPrestador { get; set; }
        public string InscricaoMunicipalPrestador { get; set; }
        public string CpfCnpjPrestador { get; set; }
        public int? DDDPrestador { get; set; }
        public string TelefonePrestador { get; set; }
        public string EnderecoPrestador { get; set; }
        public string NumeroEnderecoPrestador { get; set; }
        public string ComplementoEnderecoPrestador { get; set; }
        public string BairroEnderecoPrestador { get; set; }
        public string CpfCnpjTomador { get; set; }
        public string RazaoSocialTomador { get; set; }
        public string InscricaoMunicipalTomador { get; set; }
        public string InscricaoEstadualTomador { get; set; }
        public string TipoLogradouroTomador { get; set; }
        public string EnderecoTomador { get; set; }
        public string NumeroTomador { get; set; }
        public string ComplementoTomador { get; set; }
        public string TipoBairroTomador { get; set; }
        public string BairroTomador { get; set; }
        public string CodigoCidadeTomador { get; set; }
        public string DescricaoCidadeTomador { get; set; }
        public string UfTomador { get; set; }
        public string CepTomador { get; set; }
        public int? DDDTomador { get; set; }
        public string TelefoneTomador { get; set; }
        public string EmailTomador { get; set; }
        public string RazaoSocialIntermediario { get; set; }
        public string CpfCnpjIntermediario { get; set; }
        public string InscricaoMunicipalIntermediario { get; set; }
        public int? NumeroNfseSubstituida { get; set; }
        public DateTime? DataEmissaoNfseSubstituida { get; set; }
        public int SeriePrestacao { get; set; }
        public string MotCancelamento { get; set; }
        public string Operacao { get; set; }
        public string CodigoObra { get; set; }
        public string Art { get; set; }
        public string DeducaoPor { get; set; }
        public string TipoDeducao { get; set; }
        public string CPFCNPJReferencia { get; set; }
        public int? NumeroNfReferencia { get; set; }
        public decimal? ValorTotalReferencia { get; set; }
        public decimal? PercentualDeduzir { get; set; }
        public int? QuantidadeServicos { get; set; }
        public decimal? ValorUnitarioServico { get; set; }
        public int IncentivoFiscal { get; set; }
        public DateTime Competencia { get; set; }
        public string DadosImpressao { get; set; }
        public string Logradouro { get; set; }
        
        #endregion

        #region Construtor preenchido
        public DadosRPS(string id, int numero, string serie, string tpRps,
            DateTime dtEmissao, int optSimples, int incCultural, int situacao,
            int tpTributacao, int natTributacao, int regEspecialTributacao, int numRpsSubstituido,
            string serieRpsSubstituido, string tpRpsSubstituido, decimal baseCalculo, decimal vlrServico, decimal vlrDeducoes,
            decimal vlrPis, decimal vlrCofins, decimal vlrInss, decimal vlrIr, decimal vlrCsll, decimal ValorIssRetido,
            string vlrIssRetido, decimal vlrIss, decimal vlrLiquidoNfse, decimal vlrDescontoIncondicionado,
            decimal vlrDescontoCondicionado, decimal iss, decimal pis, decimal cofins, decimal inss,
            decimal ir, decimal csll, string codItemServico, int ResponsavelRetencao, int codCnae, string CodTribMunicipio, string DiscrimServico,
            string codCidadePrestacao, string descrCidadePrestacao, string razSocialPrestador,
            int dddPrestador, string telPrestador, string cnpj, string cpf, string razSocial,
            string inscrMunicipal, string inscrEstadual, string tipoLogradouroTomador, string endereco, string vlrNumeroTomador,
            string complemento, string tpBairro, string bairro, string codCidade, string descrCidade, string uf,
            string cep, int ddd, string telefone, string email, string razSocialIntermediario,
            string cnpjIntermediario, string cpfIntermediario, string inscrMunicipalIntermed,
            int numNfseSubsituida, DateTime dtEmissaoNfseSubstituida, int seriePrestacao, string motCancelam, string operacao,
            string codObra, string art, string deducaoPor, string tipoDeducao, string cpfCnpjReferencia, int numNfReferencia,
            decimal vlrTotalReferencia, decimal percDeduzir, int qtdServicos, decimal vlrUnitServico, string tributavel,
            string identSistemaLegado, int tipoCodificacao, string assinatura, string InscricaoMunicipalPrestador, string CpfCnpjPrestador,
            string exigibilidadeIss, int IncentivoFiscal, DateTime Competencia, string vlrDadosImpressao, string logradouro)
        {
            IdRps = id;
            NumeroRps = numero;
            SerieRps = serie;
            TipoRps = tpRps;
            DataEmissao = dtEmissao;
            OptanteSimplesNacional = optSimples;
            IncentivadorCultural = incCultural;
            SituacaoNota = situacao;
            TipoTributacao = tpTributacao;
            NaturezaTributacao = natTributacao;
            RegimeEspecialTributacao = regEspecialTributacao;
            NumeroRpsSubstituido = numRpsSubstituido;
            SerieRpsSubstituido = serieRpsSubstituido;
            TipoRpsSubstituido = tpRpsSubstituido;
            BaseCalculo = baseCalculo;
            ValorServicos = vlrServico;
            ValorDeducoes = vlrDeducoes;
            ValorPis = vlrPis;
            ValorCofins = vlrCofins;
            ValorInss = vlrInss;
            ValorIr = vlrIr;
            ValorCsll = vlrCsll;
            this.ValorIssRetido = ValorIssRetido;
            IssRetido = vlrIssRetido;
            ValorIss = vlrIss;
            ValorLiquidoNfse = vlrLiquidoNfse;
            DescontoIncondicionado = vlrDescontoIncondicionado;
            DescontoCondicionado = vlrDescontoCondicionado;
            AliquotaISS = iss;
            AliquotaPIS = pis;
            AliquotaCOFINS = cofins;
            AliquotaINSS = inss;
            AliquotaIR = ir;
            AliquotaCSLL = csll;
            CodigoItemListaServico = codItemServico;
            this.ResponsavelRetencao = ResponsavelRetencao;
            CodigoCnae = codCnae;
            CodigoTributacaoMunicipio = CodTribMunicipio;
            DiscriminacaoServico = DiscrimServico;
            CodigoCidadePrestacao = codCidadePrestacao;
            DescricaoCidadePrestacao = descrCidadePrestacao;
            RazaoSocialPrestador = razSocialPrestador;
            DDDPrestador = dddPrestador;
            TelefonePrestador = telPrestador;
            CpfCnpjTomador = cnpj;
            RazaoSocialTomador = razSocial;
            InscricaoMunicipalTomador = inscrMunicipal;
            InscricaoEstadualTomador = inscrEstadual;
            TipoLogradouroTomador = tipoLogradouroTomador;
            EnderecoTomador = endereco;
            NumeroTomador = vlrNumeroTomador;
            ComplementoTomador = complemento;
            TipoBairroTomador = tpBairro;
            BairroTomador = bairro;
            CodigoCidadeTomador = codCidade;
            DescricaoCidadeTomador = descrCidade;
            UfTomador = uf;
            CepTomador = cep;
            DDDTomador = ddd;
            TelefoneTomador = telefone;
            EmailTomador = email;
            RazaoSocialIntermediario = razSocialIntermediario;
            CpfCnpjIntermediario = cnpjIntermediario;
            InscricaoMunicipalIntermediario = inscrMunicipalIntermed;
            NumeroNfseSubstituida = numNfseSubsituida;
            DataEmissaoNfseSubstituida = dtEmissaoNfseSubstituida;
            SeriePrestacao = seriePrestacao;
            MotCancelamento = motCancelam;
            Operacao = operacao;
            CodigoObra = codObra;
            Art = art;
            DeducaoPor = deducaoPor;
            TipoDeducao = tipoDeducao;
            CPFCNPJReferencia = CPFCNPJReferencia;
            NumeroNfReferencia = numNfReferencia;
            ValorTotalReferencia = vlrTotalReferencia;
            PercentualDeduzir = percDeduzir;
            QuantidadeServicos = qtdServicos;
            ValorUnitarioServico = vlrUnitServico;
            this.InscricaoMunicipalPrestador = InscricaoMunicipalPrestador;
            this.CpfCnpjPrestador = CpfCnpjPrestador;
            this.ExigibilidadeISS = exigibilidadeIss;
            this.IncentivoFiscal = IncentivoFiscal;
            this.Competencia = Competencia;
            //DadosImpressao = vlrDadosImpressao;
            Logradouro = logradouro;
        }
        #endregion

        #region Construtor padrao(vazio)
        public DadosRPS()
        {
            IdRps = "";
            NumeroRps = 0;
            SerieRps = "";
            TipoRps = "";
            DataEmissao = DateTime.Now;
            OptanteSimplesNacional = 0;
            IncentivadorCultural = 0;
            SituacaoNota = null;
            TipoTributacao = 0;
            NaturezaTributacao = 0;
            RegimeEspecialTributacao = 0;
            NumeroRpsSubstituido = null;
            SerieRpsSubstituido = null;
            TipoRpsSubstituido = null;
            BaseCalculo = 0.00m;
            ValorServicos = 0.00m;
            ValorDeducoes = 0.00m;
            ValorPis = 0.00m;
            ValorCofins = 0.00m;
            ValorInss = 0.00m;
            ValorIr = 0.00m;
            ValorCsll = 0.00m;
            ValorIssRetido = 0.00m;
            IssRetido = "1";
            ValorIss = 0.00m;
            ValorLiquidoNfse = 0.00m;
            DescontoIncondicionado = 0.00m;
            DescontoCondicionado = 0.00m;
            AliquotaISS = null;
            AliquotaPIS = null;
            AliquotaCOFINS = null;
            AliquotaINSS = null;
            AliquotaIR = null;
            AliquotaCSLL = null;
            CodigoItemListaServico = "";
            ResponsavelRetencao = null;
            CodigoCnae = 0;
            CodigoTributacaoMunicipio = "";
            DiscriminacaoServico = "";
            CodigoCidadePrestacao = "";
            DescricaoCidadePrestacao = "";
            RazaoSocialPrestador = "";
            DDDPrestador = null;
            TelefonePrestador = "";
            CpfCnpjTomador = "";
            RazaoSocialTomador = "";
            InscricaoMunicipalTomador = "";
            InscricaoEstadualTomador = null;
            TipoLogradouroTomador = "";
            EnderecoTomador = "";
            NumeroTomador = "";
            ComplementoTomador = "";
            TipoBairroTomador = "";
            BairroTomador = "";
            CodigoCidadeTomador = "";
            DescricaoCidadeTomador = "";
            UfTomador = "";
            CepTomador = "";
            DDDTomador = null;
            TelefoneTomador = "";
            EmailTomador = "";
            RazaoSocialIntermediario = "";
            CpfCnpjIntermediario = "";
            InscricaoMunicipalIntermediario = "";
            NumeroNfseSubstituida = null;
            DataEmissaoNfseSubstituida = null;
            SeriePrestacao = 0;
            MotCancelamento = "";
            Operacao = "";
            CodigoObra = "";
            Art = "";
            DeducaoPor = "";
            TipoDeducao = "";
            CPFCNPJReferencia = "";
            NumeroNfReferencia = null;
            ValorTotalReferencia = null;
            PercentualDeduzir = null;
            QuantidadeServicos = 1;
            ValorUnitarioServico = null;
            this.InscricaoMunicipalPrestador = "";
            this.CpfCnpjPrestador = "";
            this.ExigibilidadeISS = "1";
            this.IncentivoFiscal = IncentivoFiscal;
            this.Competencia = Competencia;
            DadosImpressao = "";
        }
        #endregion
    }

}
