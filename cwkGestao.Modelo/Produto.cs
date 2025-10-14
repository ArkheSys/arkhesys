using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Produto")]
    public class Produto : ModeloBase
    {
        #region Construtores

        public Produto()
        {
            TabelaPrecoProdutos = new List<TabelaPrecoProduto>();
            Produto_Icmss = new List<Produto_Icms>();
            Estoques = new List<Estoque>();
            NCM = String.Empty;
            CodigoBarrasRegistrado = true;
            ProdutoCodBarra = new List<ProdutoCodBarra>();
            ProdutoSKU = new List<ProdutoSKU>();
            Imeis = new List<Imei>();
        }

        public Produto(int id, string codigo, String nome, Unidade unidade, Unidade unidadeEntrada, String barra,
            String barraForn, String grupo, String descReduzida, String caminhoImagem, Decimal precoBase,
            String ncm, String grupo1, String grupo2, String grupo3, bool inativo, string observacao,
            string localizacao, bool utilizarIMEI, int escrelevante, string razao_fab, string cnpj_fab, string cbenef)
        {
            ID = id;
            Codigo = codigo;
            Nome = nome;
            Unidade = unidade;
            UnidadeEntrada = unidadeEntrada;
            Barra = barra;
            BarraFornecedor = barraForn;
            GrupoEstoque = new GrupoEstoque() { Nome = grupo };
            Grupo1 = new Grupo1 { Nome = grupo1 };
            Grupo2 = new Grupo2 { Nome = grupo2 };
            Grupo3 = new Grupo3 { Nome = grupo3 };
            DescReduzida = descReduzida;
            CaminhoImagem = caminhoImagem;
            PrecoBase = precoBase;
            NCM = ncm;
            Inativo = inativo;
            Observacao = observacao;
            Localizacao = localizacao;
            UtilizarIMEI = utilizarIMEI;
            EscRelevante = escrelevante;
            RAZAO_Fab = razao_fab;
            CNPJ_Fab = cnpj_fab;
            CBenef = cbenef;
        }

        public Produto(int id, string codigo, String nome, Unidade unidade, String barra
                    , String barraForn, String grupo, String descReduzida, String caminhoImagem
                    , ClassificacaoProduto classProduto, String ncm, String grupo1, String grupo2
                    , String grupo3, DateTime incData, string incUsuario, DateTime altData, string altUsuario
                    , bool inativo, string observacao, string localizacao, bool utilizarIMEI, int escrelevante
                    , string razao_fab, string cnpj_fab, string cbenef, DateTime? DtCadastroProduto = null
                    , string codigoReferencia = "", DateTime? dataValidade = null)
        {
            ID = id;
            Codigo = codigo;
            Nome = nome;
            Unidade = unidade;
            Barra = barra;
            BarraFornecedor = barraForn;
            GrupoEstoque = new GrupoEstoque() { Nome = grupo, ClassificacaoProduto = classProduto };
            Grupo1 = new Grupo1 { Nome = grupo1 };
            Grupo2 = new Grupo2 { Nome = grupo2 };
            Grupo3 = new Grupo3 { Nome = grupo3 };
            DescReduzida = descReduzida;
            CaminhoImagem = caminhoImagem;
            NCM = ncm;
            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
            Inativo = inativo;
            Observacao = observacao;
            Localizacao = localizacao;
            UtilizarIMEI = utilizarIMEI;
            EscRelevante = escrelevante;
            RAZAO_Fab = razao_fab;
            CNPJ_Fab = cnpj_fab;
            CBenef = cbenef;
            this.DtCadastroProduto = DtCadastroProduto;
            CodigoReferencia = codigoReferencia;
            DataValidade = dataValidade;
        }

        public Produto(int id, string codigo, String nome, Unidade unidade, String barra
                    , String barraForn, String grupo, String descReduzida, String caminhoImagem
                    , ClassificacaoProduto classProduto, String ncm, String grupo1, String grupo2
                    , String grupo3, DateTime incData, string incUsuario, DateTime altData, string altUsuario
                    , bool inativo, string observacao, string localizacao, bool utilizarIMEI, int escrelevante
                    , string razao_fab, string cnpj_fab, string cbenef, decimal precoBase, string fornecedor
                    , DateTime? DtCadastroProduto = null, string codigoReferencia = ""  
                    , DateTime? dataValidade = null, string codigoFabricante = "", string codigoOriginal = ""
                    , string aplicacao = "", string cest = "")
        {
            ID = id;
            Codigo = codigo;
            Nome = nome;
            Unidade = unidade;
            Barra = barra;
            BarraFornecedor = barraForn;
            GrupoEstoque = new GrupoEstoque() { Nome = grupo, ClassificacaoProduto = classProduto };
            Grupo1 = new Grupo1 { Nome = grupo1 };
            Grupo2 = new Grupo2 { Nome = grupo2 };
            Grupo3 = new Grupo3 { Nome = grupo3 };
            Fornecedor = new Pessoa { Nome = fornecedor };
            DescReduzida = descReduzida;
            CaminhoImagem = caminhoImagem;
            NCM = ncm;
            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
            Inativo = inativo;
            Observacao = observacao;
            Localizacao = localizacao;
            UtilizarIMEI = utilizarIMEI;
            EscRelevante = escrelevante;
            RAZAO_Fab = razao_fab;
            CNPJ_Fab = cnpj_fab;
            CBenef = cbenef;
            PrecoBase = precoBase;
            this.DtCadastroProduto = DtCadastroProduto;
            CodigoReferencia = codigoReferencia;
            DataValidade = dataValidade;
            CodigoFabricante = codigoFabricante;
            CodigoOriginal = codigoOriginal;
            Aplicacao = aplicacao;
            this.CEST = cest;
        }

        public Produto(int id, string codigo, String nome, Unidade unidade, String barra
                    , String barraForn, String grupo, String descReduzida, String caminhoImagem
                    , ClassificacaoProduto classProduto, String ncm, String grupo1, String grupo2
                    , String grupo3, DateTime incData, string incUsuario, DateTime altData, string altUsuario
                    , bool inativo, string observacao, string localizacao, bool utilizarIMEI, int escrelevante
                    , string razao_fab, string cnpj_fab, string cbenef, decimal precoBase, string fornecedor
                    , DateTime? DtCadastroProduto = null, string codigoReferencia = "", DateTime? dataValidade = null)
        {
            ID = id;
            Codigo = codigo;
            Nome = nome;
            Unidade = unidade;
            Barra = barra;
            BarraFornecedor = barraForn;
            GrupoEstoque = new GrupoEstoque() { Nome = grupo, ClassificacaoProduto = classProduto };
            Grupo1 = new Grupo1 { Nome = grupo1 };
            Grupo2 = new Grupo2 { Nome = grupo2 };
            Grupo3 = new Grupo3 { Nome = grupo3 };
            Fornecedor = new Pessoa { Nome = fornecedor };
            DescReduzida = descReduzida;
            CaminhoImagem = caminhoImagem;
            NCM = ncm;
            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
            Inativo = inativo;
            Observacao = observacao;
            Localizacao = localizacao;
            UtilizarIMEI = utilizarIMEI;
            EscRelevante = escrelevante;
            RAZAO_Fab = razao_fab;
            CNPJ_Fab = cnpj_fab;
            CBenef = cbenef;
            PrecoBase = precoBase;
            this.DtCadastroProduto = DtCadastroProduto;
            CodigoReferencia = codigoReferencia;
            DataValidade = dataValidade;
        }

        public override ModeloBase Clonar()
        {
            Produto objProdutoNew = new Produto();

            AutoMapper.Mapper.CreateMap<Modelo.Produto, Modelo.Produto>()
                .ForMember(dest => dest.ID, o => o.Ignore())
                .ForMember(dest => dest.LogAlteracaoPedidoItens, o => o.Ignore())
                .ForMember(dest => dest.Estoques, o => o.Ignore())
                .ForMember(dest => dest.ProdutoCodBarra, o => o.Ignore())
                .ForMember(dest => dest.ProdutoSKU, o => o.Ignore())
                .ForMember(dest => dest.FichaTecnicaPCP, o => o.Ignore());

            objProdutoNew = AutoMapper.Mapper.Map<Modelo.Produto>(this);

            Produto_Icms.GetClones(ref objProdutoNew);
            TabelaPrecoProduto.GetClones(ref objProdutoNew);
            Imei.GetClones(ref objProdutoNew);

            return objProdutoNew;
        }

        #endregion

        #region Propriedades

        public override int ID { get; set; }

        private string _Codigo;
        [InformacaoExibicao(4, "Código", 95, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string Codigo
        {
            get { return _Codigo; }
            set
            {
                _Codigo = value;

                try
                {
                    int.TryParse(value, out int codigo);
                    CodigoOrdenado = codigo;
                }
                catch (Exception)
                {
                    CodigoOrdenado = 0;
                }
            }
        }

        private string _Barra;
        [InformacaoExibicao(5, "Barra", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string Barra { get { return _Barra; } set { _Barra = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 460)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; NomeOrdenado = value; } }

        private string _DescReduzida;
        [InformacaoExibicao(8, "Desc. Reduzida", 210)]
        public virtual string DescReduzida { get { return _DescReduzida; } set { _DescReduzida = value; } }

        private Unidade _Unidade;
        [InformacaoExibicao(3, "Und", 45)]
        public virtual Unidade Unidade { get { return _Unidade; } set { _Unidade = value; } }

        private Unidade _UnidadeEntrada;
        public virtual Unidade UnidadeEntrada { get { return _UnidadeEntrada; } set { _UnidadeEntrada = value; } }

        private ClassificacaoFiscal _ClassificacaoFiscal;
        public virtual ClassificacaoFiscal ClassificacaoFiscal { get { return _ClassificacaoFiscal; } set { _ClassificacaoFiscal = value; } }

        private PerfilPisCofins _PerfilPisCofins;
        public virtual PerfilPisCofins PerfilPisCofins { get { return _PerfilPisCofins; } set { _PerfilPisCofins = value; } }

        private GrupoEstoque _GrupoEstoque;
        [InformacaoExibicao(7, "Grupo", 170)]
        public virtual GrupoEstoque GrupoEstoque { get { return _GrupoEstoque; } set { _GrupoEstoque = value; } }

        private Pessoa _Fornecedor;
        public virtual Pessoa Fornecedor { get { return _Fornecedor; } set { _Fornecedor = value; } }

        private Pessoa _Fornecedor2;
        public virtual Pessoa Fornecedor2 { get { return _Fornecedor2; } set { _Fornecedor2 = value; } }

        private Pessoa _Fornecedor3;
        public virtual Pessoa Fornecedor3 { get { return _Fornecedor3; } set { _Fornecedor3 = value; } }

        private Pessoa _Fornecedor4;
        public virtual Pessoa Fornecedor4 { get { return _Fornecedor4; } set { _Fornecedor4 = value; } }

        private Pessoa _Fornecedor5;
        public virtual Pessoa Fornecedor5 { get { return _Fornecedor5; } set { _Fornecedor5 = value; } }

        private DateTime _DtFornec1;
        public virtual DateTime DtFornec1 { get { return _DtFornec1; } set { _DtFornec1 = value; } }

        private DateTime _DtFornec2;
        public virtual DateTime DtFornec2 { get { return _DtFornec2; } set { _DtFornec2 = value; } }

        private DateTime _DtFornec3;
        public virtual DateTime DtFornec3 { get { return _DtFornec3; } set { _DtFornec3 = value; } }

        private DateTime _DtFornec4;
        public virtual DateTime DtFornec4 { get { return _DtFornec4; } set { _DtFornec4 = value; } }

        private DateTime _DtFornec5;
        public virtual DateTime DtFornec5 { get { return _DtFornec5; } set { _DtFornec5 = value; } }

        private string _BarraFornecedor2;
        public virtual string BarraFornecedor2 { get { return _BarraFornecedor2; } set { _BarraFornecedor2 = value; } }

        private string _BarraFornecedor3;
        public virtual string BarraFornecedor3 { get { return _BarraFornecedor3; } set { _BarraFornecedor3 = value; } }

        private string _BarraFornecedor4;
        public virtual string BarraFornecedor4 { get { return _BarraFornecedor4; } set { _BarraFornecedor4 = value; } }

        private string _BarraFornecedor5;
        public virtual string BarraFornecedor5 { get { return _BarraFornecedor5; } set { _BarraFornecedor5 = value; } }

        private decimal _UltimoCusto1;
        public virtual decimal UltimoCusto1 { get { return _UltimoCusto1; } set { _UltimoCusto1 = value; } }

        private decimal _UltimoCusto2;
        public virtual decimal UltimoCusto2 { get { return _UltimoCusto2; } set { _UltimoCusto2 = value; } }

        private decimal _UltimoCusto3;
        public virtual decimal UltimoCusto3 { get { return _UltimoCusto3; } set { _UltimoCusto3 = value; } }

        private decimal _UltimoCusto4;
        public virtual decimal UltimoCusto4 { get { return _UltimoCusto4; } set { _UltimoCusto4 = value; } }

        private decimal _UltimoCusto5;
        public virtual decimal UltimoCusto5 { get { return _UltimoCusto5; } set { _UltimoCusto5 = value; } }

        private string _BarraFornecedor;
        [InformacaoExibicao(6, "Barra Fornecedor", 100)]
        public virtual string BarraFornecedor { get { return _BarraFornecedor; } set { _BarraFornecedor = value; } }

        private string _DescLonga1;
        public virtual string DescLonga1 { get { return _DescLonga1; } set { _DescLonga1 = value; } }

        private string _DescLonga2;
        public virtual string DescLonga2 { get { return _DescLonga2; } set { _DescLonga2 = value; } }

        private string _DescLonga3;
        public virtual string DescLonga3 { get { return _DescLonga3; } set { _DescLonga3 = value; } }

        private decimal _PesoLiquido;
        public virtual decimal PesoLiquido { get { return _PesoLiquido; } set { _PesoLiquido = value; } }

        private decimal _PesoBruto;
        public virtual decimal PesoBruto { get { return _PesoBruto; } set { _PesoBruto = value; } }

        private decimal _PrecoFornecedor;
        public virtual decimal PrecoFornecedor { get { return _PrecoFornecedor; } set { _PrecoFornecedor = value; } }

        private decimal _DescontoFornecedor;
        public virtual decimal DescontoFornecedor { get { return _DescontoFornecedor; } set { _DescontoFornecedor = value; } }

        public virtual DateTime? DtPrecoFornecedor { get; set; }

        private decimal _UltimoCusto;
        public virtual decimal UltimoCusto { get { return _UltimoCusto; } set { _UltimoCusto = value; } }

        public virtual DateTime? DtUltimoCusto { get; set; }

        private decimal _PrecoBase;
        public virtual decimal PrecoBase { get { return _PrecoBase; } set { _PrecoBase = value; } }

        public virtual DateTime? DtPrecoBase { get; set; }

        private decimal _AliqISS;
        public virtual decimal AliqISS { get { return _AliqISS; } set { _AliqISS = value; } }

        private int _IDPlanoConta_FinAV;
        public virtual int IDPlanoConta_FinAV { get { return _IDPlanoConta_FinAV; } set { _IDPlanoConta_FinAV = value; } }

        private int _IDPlanoConta_FinAP;
        public virtual int IDPlanoConta_FinAP { get { return _IDPlanoConta_FinAP; } set { _IDPlanoConta_FinAP = value; } }

        private bool _bServico;
        public virtual bool BServico { get { return _bServico; } set { _bServico = value; } }

        private string _ClassFiscal;
        public virtual string ClassFiscal { get { return _ClassFiscal; } set { _ClassFiscal = value; } }

        private decimal _CustoFrete;
        public virtual decimal CustoFrete { get { return _CustoFrete; } set { _CustoFrete = value; } }

        private decimal _CustoIPI;
        public virtual decimal CustoIPI { get { return _CustoIPI; } set { _CustoIPI = value; } }

        private decimal _CustoExtra;
        public virtual decimal CustoExtra { get { return _CustoExtra; } set { _CustoExtra = value; } }

        private int _Tributacao;
        public virtual int Tributacao { get { return _Tributacao; } set { _Tributacao = value; } }

        private int _OrigemProd;
        public virtual int OrigemProd { get { return _OrigemProd; } set { _OrigemProd = value; } }

        private int _modBCICMS;
        public virtual int ModBCICMS { get { return _modBCICMS; } set { _modBCICMS = value; } }

        private decimal _AliqContrib;
        public virtual decimal AliqContrib { get { return _AliqContrib; } set { _AliqContrib = value; } }

        private decimal _AliqContribNormal;
        public virtual decimal AliqContribNormal { get { return _AliqContribNormal; } set { _AliqContribNormal = value; } }

        private decimal _ReducaoContrib;
        public virtual decimal ReducaoContrib { get { return _ReducaoContrib; } set { _ReducaoContrib = value; } }

        private string _TextoLeiContrib;
        public virtual string TextoLeiContrib { get { return _TextoLeiContrib; } set { _TextoLeiContrib = value; } }

        private decimal _AliqNContrib;
        public virtual decimal AliqNContrib { get { return _AliqNContrib; } set { _AliqNContrib = value; } }

        private decimal _AliqNContribNormal;
        public virtual decimal AliqNContribNormal { get { return _AliqNContribNormal; } set { _AliqNContribNormal = value; } }

        private decimal _ReducaoNContrib;
        public virtual decimal ReducaoNContrib { get { return _ReducaoNContrib; } set { _ReducaoNContrib = value; } }

        private string _TextoLeiNContrib;
        public virtual string TextoLeiNContrib { get { return _TextoLeiNContrib; } set { _TextoLeiNContrib = value; } }

        private int _modBCST;
        public virtual int ModBCST { get { return _modBCST; } set { _modBCST = value; } }

        private decimal _LucroST;
        public virtual decimal LucroST { get { return _LucroST; } set { _LucroST = value; } }

        private decimal _pRedBCST;
        public virtual decimal PRedBCST { get { return _pRedBCST; } set { _pRedBCST = value; } }

        private decimal _AliqSubstTributaria;
        public virtual decimal AliqSubstTributaria { get { return _AliqSubstTributaria; } set { _AliqSubstTributaria = value; } }

        private decimal _AliqSimplesSubst;
        public virtual decimal AliqSimplesSubst { get { return _AliqSimplesSubst; } set { _AliqSimplesSubst = value; } }

        private int _CST_Pis;
        public virtual int CST_Pis { get { return _CST_Pis; } set { _CST_Pis = value; } }

        private decimal _pPIS_Q08;
        public virtual decimal PPIS_Q08 { get { return _pPIS_Q08; } set { _pPIS_Q08 = value; } }

        private int _CST_Cofins;
        public virtual int CST_Cofins { get { return _CST_Cofins; } set { _CST_Cofins = value; } }

        private decimal _pCOFINS_S08;
        public virtual decimal PCOFINS_S08 { get { return _pCOFINS_S08; } set { _pCOFINS_S08 = value; } }

        private int _ENQ_IPI;
        public virtual int ENQ_IPI { get { return _ENQ_IPI; } set { _ENQ_IPI = value; } }

        private int _CST_IPI;
        public virtual int CST_IPI { get { return _CST_IPI; } set { _CST_IPI = value; } }

        private decimal _AliquotaIPI;
        public virtual decimal AliquotaIPI { get { return _AliquotaIPI; } set { _AliquotaIPI = value; } }

        private string _NCM;
        [InformacaoExibicao(9, "NCM", 80)]
        public virtual string NCM { get { return _NCM; } set { _NCM = value; } }

        private string _CEST;
        [InformacaoExibicao(9, "CEST", 80)]
        public virtual string CEST { get { return _CEST; } set { _CEST = value; } }

        private int _ID_NCM;
        public virtual int ID_NCM { get { return _ID_NCM; } set { _ID_NCM = value; } }

        public virtual DateTime? DtCadastroProduto { get; set; }

        private int _GENERO_NCM;
        public virtual int GENERO_NCM { get { return _GENERO_NCM; } set { _GENERO_NCM = value; } }

        private TabelaCFOP _CFOP;
        public virtual TabelaCFOP CFOP { get { return _CFOP; } set { _CFOP = value; } }

        private TabelaCFOP _CFOPForaDoEstado;
        public virtual TabelaCFOP CFOPForaDoEstado { get { return _CFOPForaDoEstado; } set { _CFOPForaDoEstado = value; } }

        private string _InfAdicionais;
        public virtual string InfAdicionais { get { return _InfAdicionais; } set { _InfAdicionais = value; } }

        private string _AliqCupomContrib;
        public virtual string AliqCupomContrib { get { return _AliqCupomContrib; } set { _AliqCupomContrib = value; } }

        private string _AliqCupomNContrib;
        public virtual string AliqCupomNContrib { get { return _AliqCupomNContrib; } set { _AliqCupomNContrib = value; } }

        private int _CSOSN;
        public virtual int CSOSN { get { return _CSOSN; } set { _CSOSN = value; } }

        public virtual PlanoNegocio PlanoNegocio { get; set; }

        public virtual PlanoConta PlanoContaEstoque { get; set; }

        public virtual string CaminhoImagem { get; set; }

        private IList<ProdutoFCI> _ProdutoFCI;
        public virtual IList<ProdutoFCI> ProdutoFCI { get { return _ProdutoFCI; } set { _ProdutoFCI = value; } }

        private IList<Estoque> _Estoques;
        public virtual IList<Estoque> Estoques { get { return _Estoques; } set { _Estoques = value; } }

        private IList<Produto_Icms> _Produto_Icmss;
        public virtual IList<Produto_Icms> Produto_Icmss { get { return _Produto_Icmss; } set { _Produto_Icmss = value; } }

        private IList<TabelaPrecoProduto> _TabelaPrecoProdutos;
        public virtual IList<TabelaPrecoProduto> TabelaPrecoProdutos { get { return _TabelaPrecoProdutos; } set { _TabelaPrecoProdutos = value; } }

        public virtual bool UtilizaIdentificadorEstoque { get; set; }

        [InformacaoExibicao(11, "Observação", 250)]
        public virtual string Observacao { get; set; }

        public virtual bool NovoProduto { get; set; }

        [InformacaoExibicao(10, "Grupo1", 100)]
        public virtual Grupo1 Grupo1 { get; set; }
        [InformacaoExibicao(12, "Grupo2", 100)]
        public virtual Grupo2 Grupo2 { get; set; }
        [InformacaoExibicao(13, "Grupo3", 100)]
        public virtual Grupo3 Grupo3 { get; set; }
        public virtual IList<LogAlteracaoPedidoItem> LogAlteracaoPedidoItens { get; set; }

        [InformacaoExibicao(14, "Data Inclusão", 100)]
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
        [InformacaoExibicao(15, "Usuário Inclusão", 100)]
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

        [InformacaoExibicao(16, "Data Alteração", 100)]
        public override DateTime? AltData
        {
            get
            {
                return base.AltData;
            }
            set
            {
                base.AltData = value;
            }
        }
        [InformacaoExibicao(17, "Usuário Alteração", 100)]
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
        public virtual int IDIntegracao { get; set; }
        public virtual bool CodigoBarrasRegistrado { get; set; }
        public virtual bool Selecionado { get; set; }
        public virtual bool ImprimirPreco { get; set; }
        public virtual int? Quantidade { get; set; } = null;
        public override string ToString()
        {
            return Nome;
        }
        private int _CodigoOrdenado;
        public virtual int CodigoOrdenado
        {
            get
            {
                return _CodigoOrdenado;
            }
            protected set
            {
                _CodigoOrdenado = value;
            }
        }

        private string _NomeOrdenado;
        public virtual string NomeOrdenado
        {
            get
            {
                return _NomeOrdenado;
            }
            protected set
            {
                _NomeOrdenado = value;
                if (_NomeOrdenado != null && _NomeOrdenado.IndexOf("/") > 1)
                {
                    Regex r1 = new Regex(@"\d+/\d+");

                    MatchCollection matches = r1.Matches(_NomeOrdenado);

                    foreach (Match match in matches)
                    {
                        try
                        {
                            var y = Convert.ToDecimal(match.Value.Split('/')[1]);

                            if (y != 0)
                            {
                                decimal resultEsse = Convert.ToInt32(match.Value.Split('/')[0]) / y;
                                _NomeOrdenado = _NomeOrdenado.Replace(match.Value, resultEsse.ToString());
                            }
                        }
                        catch (Exception erro)
                        {
                            var X = erro.Message;
                        }
                    }
                }
            }
        }

        public virtual bool Inativo { get; set; }
        public virtual bool UtilizarIMEI { get; set; }
        public virtual IList<ProdutoCodBarra> ProdutoCodBarra { get; set; }
        public virtual IList<ProdutoSKU> ProdutoSKU { get; set; }
        public virtual IList<Imei> Imeis { get; set; }
        public virtual IList<FichaTecnicaPCP> FichaTecnicaPCP { get; set; }
        public virtual decimal Largura { get; set; }
        public virtual decimal Altura { get; set; }
        public virtual decimal Comprimento { get; set; }
        public virtual decimal Peso { get; set; }
        public virtual bool BEnviadoSHL { get; set; }
        [InformacaoExibicao(18, "Localização", 200)]
        public virtual string Localizacao { get; set; }
        public virtual string LocalizacaoStr 
        {
            get { return string.IsNullOrEmpty(Localizacao) ? "" : Localizacao; } 
        }
        private NCM _TabelaNCM;
        public virtual NCM TabelaNCM { get { return _TabelaNCM; } set { _TabelaNCM = value; } }
        public virtual int EscRelevante { get; set; }
        public virtual string RAZAO_Fab { get; set; }
        public virtual string CNPJ_Fab { get; set; }
        public virtual string CBenef { get; set; }

        public virtual int? IntegrarEcommerce { get; set; }
        public virtual int? IdEcommerce { get; set; }

        public virtual string cProdANP { get; set; }
        public virtual string descANP { get; set; }
        public virtual string UFCons { get; set; }

        public virtual DateTime? DataPromocionalInicial { get; set; }
        public virtual DateTime? DataPromocionalFinal { get; set; }

        [InformacaoExibicao(9, "Cod. Ref", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string CodigoReferencia { get; set; }

        public virtual decimal PrecoBaseNovo { get; set; } // Não existe em banco, somente em tela
        public virtual bool Pesavel { get; set; }
        public virtual DateTime? DataValidade { get; set; }
        public virtual string SaiNaturezaReceita { get; set; }

        [InformacaoExibicao(19, "Código Fabricante", 200)]
        public virtual string CodigoFabricante { get; set; }
        public virtual string CodigoFabricanteStr 
        {
            get { return string.IsNullOrEmpty(CodigoFabricante) ? "" : CodigoFabricante; }  
        }
        [InformacaoExibicao(20, "Código Original", 200)]
        public virtual string CodigoOriginal { get; set; }
        public virtual string CodigoOriginalStr 
        {
            get { return string.IsNullOrEmpty(CodigoOriginal) ? "" : CodigoOriginal; } 
        }
        [InformacaoExibicao(21, "Aplicação", 200)]
        public virtual string Aplicacao { get; set; }

        public virtual string AplicacaoStr
        {
            get { return string.IsNullOrEmpty(Aplicacao) ? "" : Aplicacao; }
        }

        public virtual string NomeGrupo1
        {
            get { return (Grupo1 == null? "": (string.IsNullOrEmpty(Grupo1.Nome) ? "": Grupo1.Nome)); }
        }

        [InformacaoExibicao(22, "Local Estoque", 200)]
        public virtual string LocalEstoque
        {
            get 
            {
                if (Estoques == null)
                    return "";

                return Estoques[0]?.LocalEstoque?.Descricao; 
            }
        }
        #endregion
    }
}
