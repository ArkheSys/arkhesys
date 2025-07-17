namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Remessa")]
    public class Remessa : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 80)] public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 400)]
        public virtual string Descricao { get; set; }

        public virtual string ArquivoDeLicensa { get; set; }

        [InformacaoExibicao(3, "Código Agência", 100)]
        public virtual string CodigoAgencia { get; set; }

        public virtual string DigitoAgencia { get; set; }

        [InformacaoExibicao(4, "Conta Corrente", 100)]
        public virtual string ContaCorrente { get; set; }

        public virtual string DigitoConta { get; set; }

        [InformacaoExibicao(5, "Código Cedente", 100)]
        public virtual string CodigoCedente { get; set; }

        public virtual string DigitoAgenciaConta { get; set; }
        public virtual string CaminhoImagem { get; set; }
        public virtual string CaminhoPDF { get; set; }
        public virtual string LayoutBoleto { get; set; }
        public virtual string CaminhoLayoutBoleto { get; set; }
        public virtual string InstrucoesCaixa { get; set; }
        public virtual string Logotipo { get; set; }
        public virtual string LayoutRemessa { get; set; }
        public virtual string LayoutRetorno { get; set; }
        public virtual string LayoutBoletoEmail { get; set; }
        public virtual int Sequencia { get; set; }
        public virtual string CaminhoRemessa { get; set; }
        public virtual string CaminhoRetorno { get; set; }
        public virtual string NomeRemessa { get; set; }
        public virtual string NomeRetorno { get; set; }
        public virtual string VersaoLayoutArquivo { get; set; }
        public virtual string VersaoLayoutLote { get; set; }
        public virtual int? CodigoBaixaDevolucao { get; set; }
        public virtual int? NumDiasBaixaDevolucao { get; set; }
        public virtual bool BGeraPDF { get; set; }
        public virtual bool BGeraImpressao { get; set; }
        public virtual bool BGeraRemessa { get; set; }
        public virtual bool BGeraEmail { get; set; }
        public virtual string MensagemEmail { get; set; }
        public virtual string URLLogotipo { get; set; }
        public virtual string URLCodigodeBarras { get; set; }
        public virtual string AssuntoEmail { get; set; }
        public virtual string NomeDoEmail { get; set; }
        public virtual bool BancoEmiteBoleto { get; set; }
        public virtual string CodigoProtesto { get; set; }
        public virtual string NumeroDiasProtesto { get; set; }

        public virtual string EspecieTituloSicredi { get; set; }
        private string especieTitulo;

        public virtual string EspecieTitulo
        {
            get => especieTitulo;
            set
            {
                especieTitulo = value;
                switch (especieTitulo)
                {
                    case "03":
                        EspecieTituloSicredi = "DMI";
                        break;
                    case "05":
                        EspecieTituloSicredi = "DSI";
                        break;
                    case "07":
                        EspecieTituloSicredi = "LC";
                        break;
                    case "12":
                        EspecieTituloSicredi = "NP";
                        break;
                    case "13":
                        EspecieTituloSicredi = "NPR";
                        break;
                    case "16":
                        EspecieTituloSicredi = "NS";
                        break;
                    case "19":
                        EspecieTituloSicredi = "ND";
                        break;
                    default:
                        EspecieTituloSicredi = "Outros";
                        break;
                }
            }
        }

        public virtual string VariacaoCarteira { get; set; }
        public virtual string ImpressoraNome { get; set; }
        public virtual int Banco { get; set; }

        public virtual bool SicrediTipoCobranca { get; set; }

        public virtual int SicrediNumeroRemessa { get; set; }
        public virtual string CaminhoHTML { get; set; }
        public virtual string CaminhoBoleto_EmLote { get; set; }
        public virtual bool PadraoJuroBoleto { get; set; }
        public virtual int SomaNossoNumero { get; set; }
        public virtual bool UtilizaDataCredito { get; set; }
        public virtual bool GeraMovTaxaDesconto { get; set; }
        public virtual bool GeraMovTaxaBoleto { get; set; }
        public virtual bool GeraMovTaxaIOF { get; set; }
        public virtual PlanoConta PlanoContaTaxaDesconto { get; set; }
        public virtual PlanoConta PlanoContaTaxaBoleto { get; set; }
        public virtual PlanoConta PlanoContaTaxaIOF { get; set; }
        public virtual Historico HistoricoTaxaDesconto { get; set; }
        public virtual Historico HistoricoTaxaBoleto { get; set; }
        public virtual Historico HistoricoTaxaIOF { get; set; }
        public virtual PlanoConta PlanoContaEstornoTDesc { get; set; }
        public virtual Historico HistoricoEstornoTDesc { get; set; }
        public virtual bool SolicitaConfirmaEmailBoleto { get; set; }
        public virtual bool BNaoGeraMsgProtesto { get; set; }
        public virtual bool BExportaJurosMulta { get; set; }
        public virtual string Convenio { get; set; }
        public virtual string NumeroContrato { get; set; }
        public virtual string Carteira { get; set; }
        public virtual string CodigoGeracao { get; set; }
        public virtual string DataLimitePagamento { get; set; }

        public static int GetCodigoBaixaDevolucao(int indice)
        {
            return indice == 0 ? 1 : 2;
        }

        public static int GetIndiceCodigoBaixaDevolucao(int? valor)
        {
            if (valor == null)
                valor = 1;

            return valor == 1 || valor == null ? 0 : 1;
        }

        public virtual string CaminhoLogBaixa { get; set; }
    }
}