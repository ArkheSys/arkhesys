using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Empresa")]
    public class Filial : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 200, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 200)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        public virtual string Fantasia { get; set; }

        public virtual int TipoAtividade { get; set; }

        public virtual string PerfilSped { get; set; }

        private string _CNPJ;
        [InformacaoExibicao(3, "CNPJ", 200)]
        public virtual string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }

        private string _Inscricao;
        public virtual string Inscricao { get { return _Inscricao; } set { _Inscricao = value; } }

        private string _Endereco;
        public virtual string Endereco { get { return _Endereco; } set { _Endereco = value; } }

        private string _Bairro;
        public virtual string Bairro { get { return _Bairro; } set { _Bairro = value; } }

        private string _Complemento;
        public virtual string Complemento { get { return _Complemento; } set { _Complemento = value; } }

        private int _IDCidade;
        public virtual int IDCidade
        {
            get { return _IDCidade; }
            set { _IDCidade = value; }
        }

        private Cidade _Cidade;
        public virtual Cidade Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }

        private string _Telefone;
        public virtual string Telefone { get { return _Telefone; } set { _Telefone = value; } }

        private string _Fax;
        public virtual string Fax { get { return _Fax; } set { _Fax = value; } }

        private int _FilialNF;
        public virtual int FilialNF { get { return _FilialNF; } set { _FilialNF = value; } }

        private int _IDEmpresa;
        public virtual int IDEmpresa { get { return _IDEmpresa; } set { _IDEmpresa = value; } }

        private Empresa _Empresa;
        public virtual Empresa Empresa { get { return _Empresa; } set { _Empresa = value; } }
        [InformacaoExibicao(4, "Empresa", 200)]
        public virtual string EmpresaNome { get { return _Empresa == null ? "" : _Empresa.Nome; } }

        private string _Tipo;
        public virtual string Tipo { get { return _Tipo; } set { _Tipo = value; } }

        private string _Numero;
        public virtual string Numero { get { return _Numero; } set { _Numero = value; } }

        private string _Responsavel;
        public virtual string Responsavel { get { return _Responsavel; } set { _Responsavel = value; } }

        private string _CEP;
        public virtual string CEP { get { return _CEP; } set { _CEP = value; } }

        private int _PortaSmtp;
        public virtual int PortaSmtp { get { return _PortaSmtp; } set { _PortaSmtp = value; } }

        private string _UsuarioEmail;

        public virtual string UsuarioEmail
        {
            get { return _UsuarioEmail; }
            set { _UsuarioEmail = value; }
        }
        private string _SenhaEmail;

        public virtual string SenhaEmail
        {
            get { return _SenhaEmail; }
            set { _SenhaEmail = value; }
        }
        private string _ServidorSMTP;

        public virtual string ServidorSMTP
        {
            get { return _ServidorSMTP; }
            set { _ServidorSMTP = value; }
        }
        public virtual string Certificado { get; set; }
        public virtual short TipoCertificadoNFe { get; set; }
        public virtual int ModeloDanfe { get; set; }
        public virtual int ModeloDanfeNFCe { get; set; }
        public virtual int FormaEmissao { get; set; }
        public virtual int Ambiente { get; set; }
        public virtual int TipoST { get; set; }
        public virtual byte SimplesNacional { get; set; }
        public virtual decimal AliqSimplesSubst { get; set; }
        //public virtual int IdTabelaFaixaFatSimples { get; set; }
        public virtual string Email { get; set; }
        public virtual string EmailRecebimentoNFe { get; set; }
        public virtual string Site { get; set; }
        public virtual TabelaFaixaFatSimples TabelaFaixaFatSimples { get; set; }
        public virtual string JustificativaScan { get; set; }
        public virtual string EmailNFe { get; set; }
        public virtual DateTime HoraScan { get; set; }
        public virtual int SerieScan { get; set; }
        public virtual string VersaoEsquema { get; set; }
        public virtual bool BEnviarCopiaEmail { get; set; }
        public virtual string DiretorioPadraoNFe { get; set; }
        public virtual string CNAE { get; set; }
        public virtual string InscricaoMunicipal { get; set; }
        public virtual int IncentivadorCultural { get; set; }
        public virtual int NaturezaTributacao { get; set; }
        public virtual string GrupoManager { get; set; }
        public virtual string DiretorioMonitoramento { get; set; }
        public virtual int RegimeEspecialTributacao { get; set; }
        public virtual bool bSTSomenteRevenda { get; set; }
        public virtual string nCdEmpresa { get; set; }
        public virtual string sDsSenha { get; set; }
        public virtual string correioCartaoPostagem { get; set; }
        public virtual string correioUsuario { get; set; }
        public virtual string correioSenha { get; set; }
        public virtual string correioContrato { get; set; }
        public virtual string correioCodigoAdministrativo { get; set; }
        public virtual string CodSuframa { get; set; }
        public virtual string TipoAtividadePreponderante { get; set; }
        public virtual string CodigoIndicadorIncidenciaTributaria { get; set; }
        public virtual string CodigoIndicadorMetodoApropCreditos { get; set; }
        public virtual string CodigoIndicadorTipoContribuicaoApuradaPeriodo { get; set; }
        public virtual string CodigoIndicadorCriterioEscrituracaoRegimeCumulativo { get; set; }
        public virtual string IND_NAT_PJ { get; set; }
        public virtual string IND_PERFIL { get; set; }
        public virtual decimal PercentualImpostoTributo { get; set; }
        public virtual string TextoTributo { get; set; }
        public virtual IList<DadosSubstituto> DadosSubstituto { get; set; }
        public virtual IList<MovimentacaoCheque> MovimentacaoCheque { get; set; }
        public virtual bool PessoaForaDoEstado(Modelo.Pessoa pessoa)
        {
            return Cidade.UF.ID != pessoa.EnderecoPrincipal().Cidade.UF.ID;
        }
        private string _PinNfe;
        public virtual string PinNfe
        {
            get { return _PinNfe; }
            set { _PinNfe = value; }
        }
        public override string ToString()
        {
            return Nome;
        }
        private bool _GMailAutenticacao;
        public virtual bool GMailAutenticacao
        {
            get { return _GMailAutenticacao; }
            set { _GMailAutenticacao = value; }
        }
        private string _ChaveValidade;
        public virtual string ChaveValidade
        {
            get { return _ChaveValidade; }
            set { _ChaveValidade = value; }
        }
        private string _UltimoAcesso;
        public virtual string UltimoAcesso
        {
            get { return _UltimoAcesso; }
            set { _UltimoAcesso = value; }
        }
        private int _ComponenteDfe;
        public virtual int ComponenteDfe { get { return _ComponenteDfe; } set { _ComponenteDfe = value; } }
	   
		private string _FileCertificado;
        public virtual string FileCertificado { get { return _FileCertificado; } set { _FileCertificado = value; } }
	
        private string _CIdToken;
        public virtual string CIdToken { get { return _CIdToken; } set { _CIdToken = value; } }

        private string _Csc;
        public virtual string Csc { get { return _Csc; } set { _Csc = value; } }

        private int _QrCode;
        public virtual int QrCode { get { return _QrCode; } set { _QrCode = value; } }
		
		private bool _CacheCertificado;
        public virtual bool CacheCertificado { get { return _CacheCertificado; } set { _CacheCertificado = value; } }
        private string _PinCert { get; set; }
		public virtual string PinCert { get { return _PinCert; } set { _PinCert = value; } }
        public virtual int? QuantDiasVencCertificado { get; set; }
        public virtual int? AmbienteNFSe { get; set; }
        public virtual string CaminhoLogoEmpresa { get; set; }
        public virtual string CaminhoLogoNfe { get; set; }
        public virtual string CaminhoLogoPrefeitura { get; set; }

        public virtual string CaminhoNFSeXML { get; set; }
        public virtual string CaminhoNFSePDF { get; set; }
    }
}
