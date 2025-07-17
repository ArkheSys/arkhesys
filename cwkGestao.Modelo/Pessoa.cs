using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Pessoa")]
    public class Pessoa : ModeloBase
    {
        public Pessoa()
        {
            PessoaAvisos = new List<PessoaAviso>();
            PessoaClientes = new List<PessoaCliente>();
            PessoaEmails = new List<PessoaEmail>();
            PessoaEnderecos = new List<PessoaEndereco>();
            PessoaFuncionarios = new List<PessoaFuncionario>();
            PessoaSocios = new List<PessoaSocio>();
            PessoaTelefones = new List<PessoaTelefone>();
            PessoaVendedores = new List<PessoaVendedor>();
            ClientesRevenda = new List<ClienteRevenda>();
            DtCadastro = new DateTime(1800, 1, 1);
            DtFundacao = null;
            DtNascimento = null;
            Nome_Vendedor = "";
        }

        public Pessoa(int ID, int Codigo, string Nome, string Fantasia, string CPF
            // -- ENDEREÇO
                    , string END_Endereco, string END_NumeroCasa, string END_Complemento
                    , string END_Bairro, string END_Nomecidade, string END_Sigla
            // - ENDEREÇO
                    , string Numero, string Contato, string NomeClas
                    , bool BCliente, bool BFuncionario, bool BVendedor, bool BFornecedor
                    , string NomeVend, string tipoPessoa, string apelido
                    , DateTime incData, string incUsuario, DateTime altData, string altUsuario, DateTime dataInativacao, string motivoInativacao, int diasPadraoLeitura)
        {
            this.ID = ID;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.Fantasia = Fantasia;
            this.CNPJ_CPF = CPF;
            this.PessoaEnderecos = new List<PessoaEndereco>() { 
                new PessoaEndereco()  { BPrincipal = true, BAtivo = true, Endereco = END_Endereco, 
                    Numero = END_NumeroCasa, Complemento = END_Complemento, Bairro = END_Bairro,
                                                   Cidade = new Cidade() { UF = new UF() { Sigla = END_Sigla}, Nome = END_Nomecidade} } }; ;
            this.PessoaTelefones = new List<PessoaTelefone>() { new PessoaTelefone() { Numero = Numero, Contato = Contato } };
            this.Contato = Contato;
            this.Classificacao = new Classificacao() { Descricao = NomeClas };
            this.BCliente = BCliente;
            this.BFornecedor = BFornecedor;
            this.BFuncionario = BFuncionario;
            this.BVendedor = BVendedor;
            this.Nome_Vendedor = NomeVend;
            this.TipoPessoa = tipoPessoa;
            this.Apelido = apelido;
            this.IncData = incData;
            this.IncUsuario = incUsuario;
            this.AltData = altData;
            this.AltUsuario = altUsuario;
            this.DataInativacao = dataInativacao;
            this.MotivoInativacao = motivoInativacao;
            this.DiasPadraoLeitura = diasPadraoLeitura;
        }

        public Pessoa(int ID, int Codigo, string Nome, int diaVencimentoCartao, decimal taxaDescontoCartao, decimal taxaAntecipacaoCartao)
        {
            this.ID = ID;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.Fantasia = Fantasia;
            this.DiaVencimentoCartao = diaVencimentoCartao;
            this.TaxaDescontoCartao = taxaDescontoCartao;
            this.TaxaAntecipacaoCartao = taxaAntecipacaoCartao;
        }


        public virtual IList<PessoaAviso> PessoaAvisos { get; set; }
        public virtual IList<PessoaCliente> PessoaClientes { get; set; }
        public virtual IList<PessoaContabilista> PessoaContabilistas { get; set; }
        public virtual IList<PessoaEmail> PessoaEmails { get; set; }
        public virtual IList<PessoaEndereco> PessoaEnderecos { get; set; }
        public virtual IList<PessoaFuncionario> PessoaFuncionarios { get; set; }
        public virtual IList<PessoaSocio> PessoaSocios { get; set; }
        public virtual IList<PessoaTelefone> PessoaTelefones { get; set; }
        public virtual IList<PessoaVendedor> PessoaVendedores { get; set; }
        public virtual IList<ClienteRevenda> ClientesRevenda { get; set; }
        public virtual IList<MovimentacaoCheque> PessoaMovCheque { get; set; }
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Código", 90)]
        public virtual int Codigo { get; set; }
        public virtual Boolean BTransportadora { get; set; }
        public virtual Boolean BCliente { get; set; }
        public virtual Boolean BFornecedor { get; set; }
        public virtual Boolean BVendedor { get; set; }
        public virtual Boolean BFuncionario { get; set; }
        public virtual Boolean BRevenda { get; set; }
        public virtual Boolean BClienteRevenda { get; set; }
        public virtual Boolean BFuncionarioCliente { get; set; }
        public virtual Boolean BDelivery { get; set; }
        public virtual Boolean BEntregador { get; set; }
        public virtual Boolean BOrgaoPublico { get; set; }
        public virtual Boolean BContribuinte { get; set; }
        public virtual Boolean NaoUtilizaLimite { get; set; }
        public virtual Boolean BAtivo { get; set; }
        [InformacaoExibicao(1, "Nome", 425)]
        public virtual string Nome { get; set; }
        public virtual string TipoPessoa { get; set; }
        public virtual string Apelido { get; set; }        
        public virtual string Fantasia { get; set; }
        [InformacaoExibicao(3, "CPF/CNPJ", 110, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string CNPJ_CPF { get; set; }
        public virtual string Inscricao { get; set; }
        public virtual string RG { get; set; }
        public virtual string Sexo { get; set; }
        public virtual string EstadoCivil { get; set; }
        public virtual string Http { get; set; }
        public virtual string Email1 { get; set; }
        public virtual string Email2 { get; set; }
        public virtual DateTime? DtFundacao { get; set; }
        public virtual DateTime? DtNascimento { get; set; }
        public virtual DateTime? DtCadastro { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Classificacao Classificacao { get; set; }
        public virtual PerfilTributarioCliente PerfilTributarioCliente { get; set; }
        public virtual string Observacao { get; set; }
        public virtual short NumeroBanco { get; set; }
        public virtual string Agencia { get; set; }
        public virtual string ContaCorrente { get; set; }
        public virtual string Placa { get; set; }
        public virtual string PlacaUF { get; set; }
        public virtual int IDIntegracao { get; set; }
        public virtual string Tratamento { get; set; }
        public virtual string SenhaLojaVirtual { get; set; }
        public virtual string InscricaoMunicipal { get; set; }
        public virtual Boolean bSTRevenda { get; set; }
        public virtual bool bShlTransportadora { get; set; }
        public virtual int CodShl { get; set; }
        //public virtual Tel_Cliente ClienteTelefonia { get; set; }
        public virtual IList<Tel_Cliente> ClientesTelefonia { get; set; }

       public virtual string SenhaLojaVirtualDescriptografada
        {
            get { return global::Modelo.Criptografia.Decriptografar(SenhaLojaVirtual); }
            set { SenhaLojaVirtual = global::Modelo.Criptografia.Criptografar(value); }
        }

        [InformacaoExibicao(13, "Classificação", 150)]
        public string Nome_Classificacao
        {
            get
            {
                if (Classificacao != null)
                    return Classificacao.Descricao;
                return "";
            }
        }

        private string _Nome_Vendedor;
        [InformacaoExibicao(12, "Vendedor", 150)]
        public string Nome_Vendedor
        {
            get
            {
                if (PessoaClientes != null && PessoaClientes.Count > 0)
                {
                    if (PessoaClientes[0].Vendedor != null)
                        return PessoaClientes[0].Vendedor.Nome;
                }
                return _Nome_Vendedor;
            }
            set { _Nome_Vendedor = value; }
        }

        [InformacaoExibicao(6, "Endereço", 150)]
        public string Endereco
        {
            get
            {
                try
                {
                    return PessoaEnderecos.Where(i => i.BPrincipal).First().Endereco;
                }
                catch
                {
                    return "";
                }
            }
        }

        [InformacaoExibicao(8, "Número", 150)]
        public string Numero
        {
            get
            {
                try
                {
                    return PessoaEnderecos.Where(i => i.BPrincipal).First().Numero;
                }
                catch
                {
                    return "";
                }
            }
        }

        [InformacaoExibicao(9, "Complemento", 150)]
        public string Complemento
        {
            get
            {
                try
                {
                    return PessoaEnderecos.Where(i => i.BPrincipal).First().Complemento;
                }
                catch
                {
                    return "";
                }
            }
        }

        [InformacaoExibicao(7, "Bairro", 150)]
        public string Bairro
        {
            get
            {
                try
                {
                    return PessoaEnderecos.Where(i => i.BPrincipal).First().Bairro;
                }
                catch
                {
                    return "";
                }
            }
        }

        [InformacaoExibicao(10, "Cidade", 150)]
        public string Cidade
        {
            get
            {
                try
                {
                    return PessoaEnderecos.Where(i => i.BPrincipal).First().Cidade.Nome;
                }
                catch
                {
                    return "";
                }
            }
        }

        [InformacaoExibicao(11, "Estado", 80)]
        public string Estado
        {
            get
            {
                try
                {
                    return PessoaEnderecos.Where(i => i.BPrincipal).First().Cidade.UF.Sigla;
                }
                catch
                {
                    return "";
                }
            }
        }

        [InformacaoExibicao(5, "Telefone", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public String TelefonePrincipal
        {
            get
            {
                PessoaTelefone pt = PessoaTelefones.FirstOrDefault();
                if (pt != null)
                    return pt.Numero;
                else
                    return "";
            }
        }
        [InformacaoExibicao(4, "Contato", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public String Contato { get; set; }
       
        public PessoaEndereco EnderecoPrincipal()
        {
            try
            {
                return PessoaEnderecos.Where(i => i.BPrincipal).First();
            }
            catch
            { return new PessoaEndereco(); }
        }

        public PessoaCliente GetCliente
        {
            get
            {
                return PessoaClientes?.FirstOrDefault();
            }

        }

        public PessoaVendedor GetVendedor
        {
            get
            {
                return PessoaVendedores.FirstOrDefault();
            }
        }

        public string PrimeiroEmail(Func<PessoaEmail, bool> func)
        {
            try
            {
                return this.PessoaEmails.Where(func).First().Email;
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
                return "";
            }
        }
        public string PrimeiroTelefone(Func<PessoaTelefone, bool> func)
        {
            try
            {
                return this.PessoaTelefones.Where(func).First().Numero;
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
                return "";
            }
        }

        public override string ToString()
        {
            return Nome;
        }

        [InformacaoExibicao(2, "Fantasia", 300)]
        public string FantasiaApelido
        {
            get
            {
                if (this.TipoPessoa == "Física")
                    return this.Apelido;
                else
                    return this.Fantasia;
            }
        }

        [InformacaoExibicao(16, "Data Inclusão", 100, InformacaoExibicao.HAlinhamento.Esquerda, InformacaoExibicao.Mascaras.DataHora)]
        public override System.DateTime? IncData
        {
            get
            {
                return base.IncData;
            }
            set
            {
                base.IncData = value;
            }
        }
        [InformacaoExibicao(17, "Usuário Inclusão", 100)]
        public override string IncUsuario
        {
            get
            {
                return base.IncUsuario;
            }
            set
            {
                base.IncUsuario = value;
            }
        }

        [InformacaoExibicao(18, "Data Alteração", 100, InformacaoExibicao.HAlinhamento.Esquerda, InformacaoExibicao.Mascaras.DataHora)]
        public override DateTime? AltData
        {
            get
            {
                if (base.AltData == new DateTime())
                {
                    return null;
                } 
                return base.AltData;
            }
            set
            {
                base.AltData = value;
            }
        }
        [InformacaoExibicao(19, "Usuário Alteração", 100)]
        public override string AltUsuario
        {
            get
            {
                return base.AltUsuario;
            }
            set
            {
                base.AltUsuario = value;
            }
        }
        public virtual int IDMercadoLivre { get; set; }
        public virtual bool bCorreio { get; set; }
        public virtual bool bContabilista { get; set; }
        public virtual bool bOperadoraCartao { get; set; }

        public Int16 IntConsumidorFinal { get; set; }

        private bool _bConsumidorFinal;
        public virtual bool bConsumidorFinal {
            get
            {
                return _bConsumidorFinal; 
            }
            set
            {
                _bConsumidorFinal = value;
                IntConsumidorFinal = _bConsumidorFinal ? (Int16)1 : (Int16)0;
            }
        }
        public virtual int? IDSISeCommerce { get; set; }
        public virtual IList<PessoaTipoServicoTransporte> TiposServicosTransporte { get; set; }
        public virtual Servico ServicoAtendimento { get; set; }
        public virtual decimal ValorHoraAtendimento { get; set; }
        public virtual string HorasContratadas { get; set; }
        public virtual bool bFaturarPorPadrao { get; set; }
        public virtual int DiasPadraoLeitura { get; set; }

        #region Operadora Cartão

        public virtual int DiaVencimentoCartao { get; set; }

        public virtual decimal TaxaDescontoCartao { get; set; }
        public virtual decimal TaxaAntecipacaoCartao { get; set; } 

        #endregion


        private string _SenhaRevenda;
        public virtual string SenhaRevenda
        {
            get
            {
                return _SenhaRevenda;
            }
            set
            {
                _SenhaRevenda = value;
            }
        }

        private DateTime? _DataInativacao;
        [InformacaoExibicao(14, "Data Inativação", 100, InformacaoExibicao.HAlinhamento.Esquerda, InformacaoExibicao.Mascaras.DataHora)]
        public virtual DateTime? DataInativacao
        {
            get
            {
                if (_DataInativacao == new DateTime())
                {
                    return null;
                }
                return _DataInativacao;
            }
            set { _DataInativacao = value; }
        }

        public int MyProperty { get; private set; }
        [InformacaoExibicao(15, "Motivo Inativação", 200)]
        public virtual string MotivoInativacao { get; set; }

        public virtual int IDRevendaCentralCliente { get; set; }

        public static Boolean IsNullOrEmpty(Pessoa pessoa)
        {
            return pessoa == null || pessoa == new Pessoa();
        }

        public virtual string InscSuframa { get; set; }

        public virtual string CaminhoFoto { get; set; }
    }
}
