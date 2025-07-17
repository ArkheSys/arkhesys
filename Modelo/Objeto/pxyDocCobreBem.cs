using System;

namespace Modelo.Objeto
{
    public class pxyDocCobreBem
    {
        public int ID { get; set; } //Documento
        public int Codigo { get; set; } //Documento
        public string Filial { get; set; } //Pessoa
        public string Requisicao { get; set; } //Documento
        public string Cliente { get; set; } //Pessoa
        public string Situacao { get; set; } //Documento
        public string NomeFantasia { get; set; } //Pessoa
        public string Portador { get; set; } //Portatdor
        public int? Parcela { get; set; } //Documento
        
        public string Convenio { get; set; }
        public string Banco { get; set; } //Banco
        public string ArquivoDeLicensa { get; set; } //Remessa
        public string CodigoAgencia { get; set; } //Remessa
        public string DigitoAgencia { get; set; } //Remessa
        public string ContaCorrente { get; set; } //Remessa
        public string DigitoConta { get; set; }
        public string CodigoCedente { get; set; } //Remessa
        public string DigitoAgenciaConta { get; set; } //Remessa
        public string InicioNossoNumero { get; set; }
        public string FimNossoNumero { get; set; }
        public string ProximoNossoNumero { get; set; }
        public string Logotipo { get; set; } //Remessa
        public string CaminhoImagem { get; set; } //Remessa
        public string LayoutBoleto { get; set; } //Remessa
        public string CaminhoLayoutBoleto { get; set; } //Remessa
        public int MargemSuperior { get; set; }
        public string Nome { get; set; } //Pessoa
        public string CPF_CNPJ { get; set; } //Pessoa
        public string Rua { get; set; } //Pessoa Endereço
        public string NumeroResidencia { get; set; } // Pessoa Endereco
        public string Bairro { get; set; } //Pessoa Endereço
        public string Cidade { get; set; } //Cidade
        public string Estado { get; set; } //Pessoa Endereço
        public string CEP { get; set; } //Pessoa Endereço


        public int CodigoDocumento { get; set; } //Documento
        public string TipoDocumentoCobranca { get; set; } // DM
        public DateTime? DataDocumento { get; set; } //Documento
        public DateTime? DataVencimento { get; set; } //Documento
        public string NumeroDocumento { get; set; } //Documento
        public decimal? Valor { get; set; } //Documento
        public decimal? ValorTotal { get; set; } //Documento
        public decimal? Acrescimo { get; set; } //Documento
        public decimal? Saldo { get; set; } //Documento
        public decimal? Juro { get; set; } //Acrecimo
        public int? DiaProtesto { get; set; } //Acrescimo
        public string InstrucoesCaixa { get; set; } //Remessa
        public string Telefone { get; set; } //Pessoa Endereço 
        public int? QtdParcela { get; set; } //Documento
        public string LayoutRemessa { get; set; } // Remessa
        public string AgenciaDebito { get; set; } //Remessa
        public string Empresa { get; set; }
        public string ContaCorrenteDebito { get; set; }
        public int CodigoPessoa { get; set; }
        public string TipoDocumento { get; set; } //Tipo Documento
        public string CaminhoRemessa { get; set; } //Remessa
        public string CaminhoRetorno { get; set; } //Remessa
        public string NomeRemessa { get; set; } //Remessa
        public string NomeRetorno { get; set; } //Remessa
        public string VersaoLayoutArquivo { get; set; } // remessa
        public string VersaoLayoutLote { get; set; } // remessa
        public int? InstrucaoCobranca3 { get; set; } //Remessa
        public int? ValorInstrucaoCobranca { get; set; } //Remessa
        public bool? bGeraImpressao { get; set; } //Remessa
        public bool? bGeraPDF { get; set; } //Remessa
        public bool? bGeraRemessa { get; set; } //Remessa
        public int? Sequencia { get; set; } //Remessa
        public bool? bGeraEmail { get; set; } //Remessa
        public string MensagemEmail { get; set; } //Remessa
        public string URLLogotipo { get; set; } //Remessa
        public string URLCodigoBarras { get; set; } //Remessa
        public string AssuntoEmail { get; set; } //Remessa
        public string NomeEmail { get; set; } //Remessa
        public string ServidorSMTP { get; set; } //Empresa
        public int PortaSMPT { get; set; } //Empresa
        public string UsuarioEmail { get; set; } //Empresa
        public string SenhaEmail { get; set; } //Empresa
        public int FormaEnvio { get; set; } //Remessa
        public string EmailRemetente { get; set; } //Remessa
        public string LayoutBoletoEmail { get; set; } //Remessa
        public bool? BancoEmiteBoleto { get; set; } //Remessa
        public string CodigoProtesto { get; set; } //Remessa
        public string NumeroDiasProtesto { get; set; } //Remessa
        public string EspecieTitulo { get; set; } //Remessa
        public string VariacoCarteira { get; set; } //Remessa
        public string ImpressoraPDF { get; set; } //Remessa
        public int? BancoRemessa { get; set; } //Remessa
        public byte? SicrediTipoCobranca { get; set; } //Remessa
        public int? SicredinumeroRemessa { get; set; } //Remessa
        public int IDRemessa { get; set; } //Remessa
        public string MovimentoRemessa { get; set; } //Documento
        public int? DiaJuro { get; set; } //acrescimo
    }
}