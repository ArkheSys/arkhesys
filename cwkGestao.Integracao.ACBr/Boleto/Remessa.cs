namespace cwkGestao.Integracao.ACBr.Boleto
{
    public class Remessa
    {
        // Cedente
        public int Banco { get; set; }
        public string CodigoAgencia { get; set; }
        public string DigitoAgencia { get; set; }
        public string ContaCorrente { get; set; }
        public string DigitoConta { get; set; }
        public string CodigoCedente { get; set; }
        public string DigitoAgenciaConta { get; set; }
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string CpfCnpj { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public string NumeroResidencia { get; set; }
        public string Telefone { get; set; }
        public string UF { get; set; }
        public string Convenio { get; set; }
        public string NumeroContrato { get; set; }
        public string Carteira { get; set; }
        public string CodigoGeracao { get; set; }
        public string DataLimitePagamento { get; set; }

        // Layout e Diretorio
        public string LayoutBoleto { get; set; }
        public string LayoutRemessa { get; set; }
        public string CaminhoRemessa { get; set; }
        public string CaminhoRetorno { get; set; }
        public string NomeRemessa { get; set; }
        public string NomeRetorno { get; set; }
        public string VersaoLayoutArquivo { get; set; }
        public string VersaoLayoutLote { get; set; }

        // Email
        public string AssuntoEmail { get; set; }
        public string NomeEmail { get; set; }
        public string MensagemEmail { get; set; }
        public string ServidorSMTP { get; set; }
        public int PortaSMTP { get; set; }
        public string UsuarioEmail { get; set; }
        public string SenhaEmail { get; set; }
        public int FormaEnvio { get; set; }
        public string EmailRemetente { get; set; }

        // LAYOUT
        public string Logotipo { get; set; }
        

        #region nao utilizado no momento

        // Dados Titulo
        public string InicioNossoNumero { get; set; }
        public string FimNossoNumero { get; set; }
        public string ProximoNossoNumero { get; set; }
        public string TipoDocumentoCobranca { get; set; }
        public string InstrucoesCaixa { get; set; }
        public int TipoCarteira { get; set; } // = SicredTipoCobranca

        // Layout
        public string CaminhoImagem { get; set; }
        public string LayoutBoletoEmail { get; set; }
        public string CaminhoLayoutBoleto { get; set; }
        public int MargemSuperior { get; set; }

        #endregion
        
        // NÃO UTILIZADO
        public string URLLogotipo { get; set; }
        public string URLCodigodeBarras { get; set; }
        public string OutroDadoConfiguracao1 { get; set; }
        public int OutroDadoConfiguracao2 { get; set; }

        public string ImpressoraPDF { get; set; }
        public bool BancoEmiteBoleto { get; set; }
        public string CaminhoHTML { get; set; }
        public string CaminhoPDF { get; set; }
        public string CaminhoLogBaixa { get; set; }
    }
}