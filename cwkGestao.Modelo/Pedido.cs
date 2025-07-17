using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cwkGestao.Modelo
{

    [InformacaoExibicao("Orçamento")]
    public class Pedido : ModeloBase
    {
        #region Constructors/set-gets

        #region Construtores
        public Pedido()
        {
            Items = new List<PedidoItem>();
            Parcelas = new List<PedidoParcela>();
            ItemsExpedicao = new List<ExpedicaoItem>();

            Dt = DateTime.Today;
            DtDigitacao = DateTime.Today;
            DtValidade = DateTime.Today;
            DtPrazoEntrega = DateTime.Today;
            Validade = 10;
            Status = StatusOrcamento.Aberto;
            ComplementoNumero = String.Empty;
        }

        public Pedido(int ID, int Codigo, int numero, DateTime digitacao, decimal totalProduto, long qtdProdutos, decimal totalServico, long qtdServicos,
                      int respRetencao, decimal valorFrete, decimal valorAcrescimo, decimal totalPedido, DateTime data, decimal totalST, decimal totalIPI,
                      decimal percComissao, decimal valorComissao)
        {
            this.ID = ID;
            this.Codigo = Codigo;
            this.Numero = numero;
            this.DtDigitacao = digitacao;
            this.TotalProduto = totalProduto;
            this.TotalServico = totalServico;
            this.Items = new List<PedidoItem>();
            this.ItemsServicos = new List<PedidoItemServico>();
            for (int i = 0; i < qtdProdutos; i++) Items.Add(new PedidoItem());
            for (int i = 0; i < qtdServicos; i++) ItemsServicos.Add(new PedidoItemServico());
            this.RespRetencao = respRetencao;
            this.ValorFrete = valorFrete;
            this.ValorAcrescimo = valorAcrescimo;
            this.TotalPedido = totalPedido;
            this.TotalST = totalST;
            this.Dt = data;
            this.TotalIPI = totalIPI;
            this.PercComissao = percComissao;
            this.ValorComissao = valorComissao;
        }

        public Pedido(int ID, int Codigo, DateTime digitacao, StatusOrcamento status, String cliNome, string fantasia,
                      string apelido, string nomeFilial, string tipoPessoa,
                      decimal total, String vendNome, String tipo, int numero, DateTime data, DateTime incData,
                      string incUsuario, DateTime? altData, string altUsuario, decimal percComissao, decimal valorComissao,
                      string usuarioRevogacao, string usuarioAprovacao, DateTime? dtAprovacao, DateTime? dtRevogacao, string primeiraParcela, string UsuarioCancelamento,
                      DateTime? DtCancelamento, string MotivoCancelamento, int IDPedidoMercadoLivre, string NumeroPos, string NomeMaquina, string SerialImpressora)
        {
            this.Dt = data;
            this.ID = ID;
            this.Codigo = Codigo;
            this.DtDigitacao = digitacao;
            this.Status = status;
            this.Pessoa = new Pessoa() { Fantasia = fantasia, Apelido = apelido, TipoPessoa = tipoPessoa };
            if (tipoPessoa == "Física")
                this.Pessoa = new Pessoa() { Apelido = apelido, TipoPessoa = tipoPessoa };
            else
                this.Pessoa = new Pessoa() { Apelido = fantasia, TipoPessoa = tipoPessoa };
            this.Filial = new Filial() { Nome = nomeFilial };
            this.PessoaNome = cliNome;
            this.TotalPedido = total;
            this.Vendedor = new Pessoa() { Nome = vendNome };
            this.TipoPedido = new TipoPedido() { Nome = tipo };
            this.Numero = numero;
            this.IncData = incData;
            this.IncUsuario = incUsuario;
            this.AltData = altData;
            this.AltUsuario = altUsuario;
            this.PercComissao = percComissao;
            this.ValorComissao = valorComissao;
            this.dtRevogacao = dtRevogacao;
            this.dtAprovacao = dtAprovacao;
            this.UsuarioAprovacao = usuarioAprovacao;
            this.UsuarioRevogacao = usuarioRevogacao;
            this.primeiraParcela = primeiraParcela;
            this.UsuarioCancelamento = UsuarioCancelamento;
            this.DtCancelamento = DtCancelamento;
            this.MotivoCancelamento = MotivoCancelamento;
            this.IDPedidoMercadoLivre = IDPedidoMercadoLivre;
            this.NumeroPos = NumeroPos;
            this.NomeMaquina = NomeMaquina;
            this.SerialImpressora = SerialImpressora;
        }

        public Pedido(int ID, int Codigo, DateTime digitacao, StatusOrcamento status, String cliNome,
                      decimal total, string fantasia, String vendNome, String tipo, int numero, DateTime data, DateTime incData,
                      string incUsuario, DateTime? altData, string altUsuario, decimal percComissao, decimal valorComissao, decimal TotalIPI,
                      string usuarioRevogacao, string usuarioAprovacao, DateTime? dtAprovacao, DateTime? dtRevogacao)
        {
            this.Dt = data;
            this.ID = ID;
            this.Codigo = Codigo;
            this.DtDigitacao = digitacao;
            this.Status = status;
            this.Pessoa = new Pessoa() { Fantasia = fantasia };
            this.PessoaNome = cliNome;
            this.TotalPedido = total;
            this.Vendedor = new Pessoa() { Nome = vendNome };
            this.TipoPedido = new TipoPedido() { Nome = tipo };
            this.Numero = numero;
            this.IncData = incData;
            this.IncUsuario = incUsuario;
            this.AltData = altData;
            this.AltUsuario = altUsuario;
            this.PercComissao = percComissao;
            this.ValorComissao = valorComissao;
            this.TotalIPI = TotalIPI;
            this.dtRevogacao = dtRevogacao;
            this.dtAprovacao = dtAprovacao;
            this.UsuarioAprovacao = usuarioAprovacao;
            this.UsuarioRevogacao = usuarioRevogacao;
        }



        #endregion

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;

        [InformacaoExibicao(0, "Código", 80, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }


        private Filial _Filial;
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        [InformacaoExibicao(22, "Filial", 300)]
        public virtual string FilialNome { get { return _Filial.Nome; } }

        private TipoPedido _TipoPedido;
        [InformacaoExibicao(8, "Tipo", 330)]
        public virtual TipoPedido TipoPedido { get { return _TipoPedido; } set { _TipoPedido = value; } }

        private int _Ent_Sai;
        public virtual int Ent_Sai { get { return _Ent_Sai; } set { _Ent_Sai = value; } }

        private DateTime _Dt;
        [InformacaoExibicao(1, "Data", 80)]
        public virtual DateTime Dt { get { return _Dt; } set { _Dt = value; } }

        private DateTime _DtDigitacao;
        [InformacaoExibicao(2, "Digitação", 80)]
        public virtual DateTime DtDigitacao { get { return _DtDigitacao; } set { _DtDigitacao = value; } }

        public virtual DateTime? DtValidade { get; set; }

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        [InformacaoExibicao(6, "Fantasia", 300)]
        public virtual string PessoaFantasia
        {
            get
            {
                if (Pessoa.TipoPessoa == "JURÍDICA")
                    return _Pessoa.Fantasia;
                else
                    return _Pessoa.Apelido;
            }
        }


        private string _PessoaNome;
        [InformacaoExibicao(4, "Nome", 330)]
        public virtual string PessoaNome { get { return _PessoaNome; } set { _PessoaNome = value; } }

        private string _PessoaCNPJCPF;
        public virtual string PessoaCNPJCPF { get { return _PessoaCNPJCPF; } set { _PessoaCNPJCPF = value; } }

        private string _PessoaInscRG;
        public virtual string PessoaInscRG { get { return _PessoaInscRG; } set { _PessoaInscRG = value; } }

        private string _PessoaEndereco;
        public virtual string PessoaEndereco { get { return _PessoaEndereco; } set { _PessoaEndereco = value; } }

        private string _PessoaBairro;
        public virtual string PessoaBairro { get { return _PessoaBairro; } set { _PessoaBairro = value; } }

        private string _PessoaCidade;
        public virtual string PessoaCidade { get { return _PessoaCidade; } set { _PessoaCidade = value; } }

        private string _PessoaUF;
        public virtual string PessoaUF { get { return _PessoaUF; } set { _PessoaUF = value; } }

        private string _PessoaTelefone;
        public virtual string PessoaTelefone { get { return _PessoaTelefone; } set { _PessoaTelefone = value; } }

        private Pessoa _Vendedor;
        [InformacaoExibicao(7, "Vendedor", 330)]
        public virtual Pessoa Vendedor { get { return _Vendedor; } set { _Vendedor = value; } }

        private TabelaPreco _TabelaPreco;
        public virtual TabelaPreco TabelaPreco { get { return _TabelaPreco; } set { _TabelaPreco = value; } }

        private Condicao _Condicao;
        public virtual Condicao Condicao { get { return _Condicao; } set { _Condicao = value; } }

        private decimal _TotalProduto;
        public virtual decimal TotalProduto { get { return _TotalProduto; } set { _TotalProduto = value; } }

        private decimal _TotalServico;
        public virtual decimal TotalServico { get { return _TotalServico; } set { _TotalServico = value; } }

        private int _RespRetencao;
        public virtual int RespRetencao { get { return _RespRetencao; } set { _RespRetencao = value; } }

        private decimal _PercDesconto;
        public virtual decimal PercDesconto { get { return _PercDesconto; } set { _PercDesconto = value; } }

        private decimal _PercComissao;
        public virtual decimal PercComissao { get { return _PercComissao; } set { _PercComissao = value; } }

        private decimal _ValorComissao;
        public virtual decimal ValorComissao { get { return _ValorComissao; } set { _ValorComissao = value; } }

        private decimal _ValorDesconto;
        public virtual decimal ValorDesconto { get { return _ValorDesconto; } set { _ValorDesconto = value; } }

        private decimal _ValorAcrescimo;
        public virtual decimal ValorAcrescimo { get { return _ValorAcrescimo; } set { _ValorAcrescimo = value; } }

        private decimal _TotalPedido;
        [InformacaoExibicao(5, "TotalPedido", 110, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal TotalPedido { get { return _TotalPedido; } set { _TotalPedido = value; } }

        private string _Observacao1;
        public virtual string Observacao1 { get { return _Observacao1; } set { _Observacao1 = value; } }

        private string _Observacao2;
        public virtual string Observacao2 { get { return _Observacao2; } set { _Observacao2 = value; } }

        private string _Observacao3;
        public virtual string Observacao3 { get { return _Observacao3; } set { _Observacao3 = value; } }

        private string _Observacao1Licitacao;
        public virtual string Observacao1Licitacao { get { return _Observacao1Licitacao; } set { _Observacao1Licitacao = value; } }

        private string _Observacao2Licitacao;
        public virtual string Observacao2Licitacao { get { return _Observacao2Licitacao; } set { _Observacao2Licitacao = value; } }

        private bool _bEnviadoEmail;
        public virtual bool bEnviadoEmail { get { return _bEnviadoEmail; } set { _bEnviadoEmail = value; } }

#pragma warning disable CS0169 // O campo "Pedido._PercFatProdNota" nunca é usado
        private decimal _PercFatProdNota;
#pragma warning restore CS0169 // O campo "Pedido._PercFatProdNota" nunca é usado
        public virtual decimal PercFatProdNota { get; set; }

        private StatusEntEventoCorreio _StatusEntEventoCorreio;
        public virtual StatusEntEventoCorreio StatusEntEventoCorreio { get { return _StatusEntEventoCorreio; } set { _StatusEntEventoCorreio = value; } }

        public virtual StatusOrcamento Status { get; set; }

        public virtual StatusPedidoDelivery StatusDelivery { get; set; }
        public virtual DateTime? DataSaidaEntrega { get; set; }
        public virtual DateTime? DataChegadaEntrega { get; set; }

        public virtual string ReferenciasEmpresa { get; set; }

        public virtual int StatusSHL { get; set; }

        public virtual bool CupomEnviado { get; set; }

        public virtual string StatusExibicaoSHL
        {
            get
            {
                #region switch
                switch (StatusSHL)
                {
                    case 1:
                        return "Lançado";
                    case 2:
                        return "Em separação";
                    case 3:
                        return "Em conferência";
                    case 4:
                        return "Em Expedição";
                    case 5:
                        return "Despachado incompleto";
                    case 6:
                        return "Despachado completo";
                    case 7:
                        return "Despachado (Sem análise)";
                    case 8:
                        return "Aguardando separação";
                    case 9:
                        return "Cancelado";
                    case 10:
                        return "Aguardando separação IMPRESSO";
                    case 11:
                        return "Aguardando retirada";
                    case 12:
                        return "Devolvido";
                    case 13:
                        return "Pendente (Falha na operação)";
                    case 14:
                        return "Pendente (Verificar Situação)";
                    case 15:
                        return "Em conferência(Falta material)";
                    case 17:
                        return "Aguardando Conferência";
                    case 18:
                        return "Aguardando Reenvio";
                    case 19:
                        return "Cancelado - Não extornar";
                    default:
                        return "";
                }
                #endregion
            }
        }

        [InformacaoExibicao(3, "Status", 160)]
        public virtual string StatusExibicao
        {
            get
            {
                if (Status == StatusOrcamento.AguardandoAprovacao)
                    return "Aguardando Aprovação";

                if (Status == StatusOrcamento.AguardandoExpedicao)
                    return "Aguardando Expedição";

                if (Status == StatusOrcamento.AguardandoFaturamento)
                    return "Aguardando Faturamento";

                if (Status == StatusOrcamento.AguardandoPagamento)
                    return "Aguardando Pagamento";

                if (Status == StatusOrcamento.Expedicao)
                    return "Expedição";

                if (Status == StatusOrcamento.FaturadoParcial)
                    return "Faturado Parcial";

                return Status.ToString();
            }
        }

        private bool _bImpressa;
        public virtual bool BImpressa { get { return _bImpressa; } set { _bImpressa = value; } }

        private string _PessoaCEP;
        public virtual string PessoaCEP { get { return _PessoaCEP; } set { _PessoaCEP = value; } }

        private int _Numero;
        [InformacaoExibicao(9, "Número", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Numero { get { return _Numero; } set { _Numero = value; } }

        private string _ComplementoNumero;
        public virtual string ComplementoNumero { get { return _ComplementoNumero; } set { _ComplementoNumero = value; } }

        public virtual Projeto Projeto { get; set; }

        public virtual string Complemento { get; set; }

        public virtual decimal ValorFrete { get; set; }
        public virtual TipoFrete TipoFrete { get; set; }
        public virtual bool bConsiderarFreteFaturamento { get; set; }


        public virtual bool bCaixasAcopladas { get; set; }
        public virtual string NumeroUnidades { get; set; }
        public virtual string NumeroBanheirosPUnidade { get; set; }
        public virtual string Garantia { get; set; }


        public virtual Pessoa PessoaTransportadora { get; set; }
        public virtual DateTime DtPrazoEntrega { get; set; }
        public virtual int Validade { get; set; }
        public virtual decimal TotalST { get; set; }

        public virtual decimal TotalIPI { get; set; }

        public virtual IList<PedidoHistorico> PedidoHistoricos { get; set; }
        public virtual IList<PedidoItem> Items { get; set; }
        public virtual IList<PedidoItemServico> ItemsServicos { get; set; }
        public virtual IList<PedidoParcela> Parcelas { get; set; }
        public virtual bool bRealizouRotinaCorreios { get; set; }
        public virtual int CodigoPedidoMagento { get; set; }
        public virtual string CodigoEntregaMagento { get; set; }

        private string _UsuarioAprovacao;
        [InformacaoExibicao(14, "Usuário Aprovação", 100)]
        public virtual string UsuarioAprovacao { get { return _UsuarioAprovacao; } set { _UsuarioAprovacao = value; } }

        private string _UsuarioRevogacao;
        [InformacaoExibicao(15, "Usuário Revogação", 100)]
        public virtual string UsuarioRevogacao { get { return _UsuarioRevogacao; } set { _UsuarioRevogacao = value; } }

        private DateTime? _dtAprovacao;
        [InformacaoExibicao(16, "Data da Aprovação", 80)]
        public virtual DateTime? dtAprovacao { get { return _dtAprovacao; } set { _dtAprovacao = value; } }

        private DateTime? _dtRevogacao;
        [InformacaoExibicao(17, "Data da Revogação", 80)]
        public virtual DateTime? dtRevogacao { get { return _dtRevogacao; } set { _dtRevogacao = value; } }

        private string _primeiraParcela;
        [InformacaoExibicao(18, "Primeira Parcela", 100)]
        public virtual string primeiraParcela { get { return _primeiraParcela; } set { _primeiraParcela = value; } }

        public virtual int? IDVendedor { get; set; }

        public virtual PessoaEndereco EnderecoEntrega { get; set; }

        public virtual string GerenteDesconto { get; set; }

        public virtual int IDIntegracao { get; set; }

        public virtual decimal MargemLucroMedia { get; set; }

        public virtual decimal MargemLucroMediaMenosDesconto { get; set; }

        [InformacaoExibicao(22, "ID Mercado Livre", 100)]
        public virtual int IDPedidoMercadoLivre { get; set; }

        [InformacaoExibicao(10, "Data Inclusão", 100, InformacaoExibicao.HAlinhamento.Esquerda, InformacaoExibicao.Mascaras.DataHora)]
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

        [InformacaoExibicao(11, "Usuário Inclusão", 100)]
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

        [InformacaoExibicao(12, "Data Alteração", 100, InformacaoExibicao.HAlinhamento.Esquerda, InformacaoExibicao.Mascaras.DataHora)]
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

        [InformacaoExibicao(13, "Usuário Alteração", 100)]
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

        //public virtual string NomeRelatorio { get; set; }

        public virtual LocalEstoque LocalEstoquePedido { get; set; }

        public virtual IList<ExpedicaoItem> ItemsExpedicao { get; set; }

        public virtual string UsuarioExpedicao { get; set; }

        public virtual string TransNome { get; set; }
        public virtual string TransEndereco { get; set; }
        public virtual string TransCidade { get; set; }
        public virtual string TransUF { get; set; }
        public virtual string TransCNPJCPF { get; set; }
        public virtual string TransPlaca { get; set; }
        public virtual string TransPlacaUF { get; set; }
        public virtual bool Selecionado { get; set; }
        public virtual Condicao CondicaoTransportadora { get; set; }
        public virtual int NumeroCupomFiscal { get; set; }

        [InformacaoExibicao(19, "Usuário Cancelamento", 100)]
        public virtual string UsuarioCancelamento { get; set; }

        [InformacaoExibicao(20, "Data Cancelamento", 60)]
        public virtual DateTime? DtCancelamento { get; set; }

        [InformacaoExibicao(21, "Motivo Cancelamento", 350)]
        public virtual string MotivoCancelamento { get; set; }

        public virtual PCP_Lote PCP_Lote { get; set; }

        public virtual IList<CorreiosPrecoPrazo> CorreiosPrecoPrazo { get; set; }

        public virtual IList<Entrega> Entregas { get; set; }

        public virtual StatusEntregaType StatusEntrega { get; set; }

        public virtual string StatusEntregaVisivel
        {
            get
            {
                switch (StatusEntrega)
                {
                    case StatusEntregaType.Aberta:
                        return "Aberta";
                    case StatusEntregaType.Parcial:
                        return "Parcial";
                    case StatusEntregaType.Concluída:
                        return "Concluída";
                    default:
                        return "";
                }
            }
            set { }
        }

        [InformacaoExibicao(23, "Códigos de Rastreamento", 200)]
        public virtual string CodigosRastreamento { get; set; }

        public virtual bool bTransCorreiosAR { get; set; }

        public virtual bool bTransCorreiosMP { get; set; }

        public virtual decimal TransCorreiosValorDeclarado { get; set; }

        public virtual int CodShl { get; set; }
        public virtual int IDPedidoSISeCommerce { get; set; }
        public virtual PessoaTipoServicoTransporte TipoServicoTransportePedido { get; set; }

        public virtual bool GerouPedidoCompra { get; set; }

        #region Clone

        public override ModeloBase Clonar()
        {
            Pedido objPedido = new Pedido();

            AutoMapper.Mapper.CreateMap<Pedido, Pedido>()
                .ForMember(dest => dest.ID, o => o.Ignore())
                .ForMember(dest => dest.PedidoHistoricos, o => o.Ignore())
                .ForMember(dest => dest.Status, o => o.Ignore())
                .ForMember(dest => dest.CorreiosPrecoPrazo, o => o.Ignore())
                .ForMember(dest => dest.MotivoCancelamento, o => o.Ignore())
                .ForMember(dest => dest.UsuarioCancelamento, o => o.Ignore())
                .ForMember(dest => dest.DtCancelamento, o => o.Ignore())
                .ForMember(dest => dest.StatusEntrega, o => o.Ignore())
                .ForMember(dest => dest.UsuarioAprovacao, o => o.Ignore())
                .ForMember(dest => dest.DtCancelamento, o => o.Ignore())
                .ForMember(dest => dest.Parcelas, o => o.Ignore());

            objPedido = AutoMapper.Mapper.Map<Pedido>(this);

            objPedido.Items = objPedido.Items.Select(pi =>
            {
                pi = (PedidoItem)pi.Clonar(); pi.Pedido = objPedido; return pi;
            }).ToList();

            objPedido.ItemsServicos = objPedido.ItemsServicos.Select(si =>
            {
                si = (PedidoItemServico)si.Clonar(); si.Pedido = objPedido; return si;
            }).ToList();

            return objPedido;
        }


        public virtual Pedido Clone()
        {
            Pedido pedidoSelecionado = this;
            Pedido clone = new Pedido();
            clone.Complemento = pedidoSelecionado.Complemento;
            clone.ComplementoNumero = pedidoSelecionado.ComplementoNumero;
            clone.Condicao = pedidoSelecionado.Condicao;
            clone.Dt = pedidoSelecionado.Dt;
            clone.DtDigitacao = DateTime.Now;
            clone.DtValidade = pedidoSelecionado.DtValidade;
            clone.Ent_Sai = pedidoSelecionado.Ent_Sai;
            clone.Filial = pedidoSelecionado.Filial;
            clone.Numero = pedidoSelecionado.Numero;
            clone.Observacao1 = pedidoSelecionado.Observacao1;
            clone.Observacao2 = pedidoSelecionado.Observacao2;
            clone.Observacao3 = pedidoSelecionado.Observacao3;
            clone.PercDesconto = pedidoSelecionado.PercDesconto;
            clone.Pessoa = pedidoSelecionado.Pessoa;
            clone.PessoaBairro = pedidoSelecionado.PessoaBairro;
            clone.PessoaCEP = pedidoSelecionado.PessoaCEP;
            clone.PessoaCidade = pedidoSelecionado.PessoaCidade;
            clone.PessoaCNPJCPF = pedidoSelecionado.PessoaCNPJCPF;
            clone.PessoaEndereco = pedidoSelecionado.PessoaEndereco;
            clone.PessoaInscRG = pedidoSelecionado.PessoaInscRG;
            clone.PessoaNome = pedidoSelecionado.PessoaNome;
            clone.PessoaTelefone = pedidoSelecionado.PessoaTelefone;
            clone.PessoaUF = pedidoSelecionado.PessoaUF;
            clone.Projeto = pedidoSelecionado.Projeto;
            clone.Status = pedidoSelecionado.Status;
            clone.TabelaPreco = pedidoSelecionado.TabelaPreco;
            clone.TipoFrete = pedidoSelecionado.TipoFrete;
            clone.TipoPedido = pedidoSelecionado.TipoPedido;
            clone.PessoaTransportadora = pedidoSelecionado.PessoaTransportadora;
            clone.ValorAcrescimo = pedidoSelecionado.ValorAcrescimo;
            clone.ValorDesconto = pedidoSelecionado.ValorDesconto;
            clone.ValorFrete = pedidoSelecionado.ValorFrete;
            clone.Vendedor = pedidoSelecionado.Vendedor;
            clone.IDVendedor = pedidoSelecionado.IDVendedor;
            clone.TotalST = pedidoSelecionado.TotalST;
            clone.PercComissao = pedidoSelecionado.PercComissao;
            clone.ValorComissao = pedidoSelecionado.ValorComissao;
            clone.dtAprovacao = pedidoSelecionado.dtAprovacao;
            clone.dtRevogacao = pedidoSelecionado.dtRevogacao;
            clone.UsuarioAprovacao = pedidoSelecionado.UsuarioAprovacao;
            clone.UsuarioRevogacao = pedidoSelecionado.UsuarioRevogacao;
            return clone;
        }
        #endregion
        #endregion

        #region calculos

        private void CalculaTotalIPI()
        {
            TotalIPI = 0;
            if (Items.Sum(x => x.ValorIPI) > 0)
                TotalIPI = Items.Sum(x => x.ValorIPI);
            else
                foreach (var itemIPI in Items)
                    if (itemIPI.Produto != null)
                        TotalIPI += ((itemIPI.Total + itemIPI.RAT_Acrescimo + itemIPI.RAT_Frete - itemIPI.RAT_Desconto) * (itemIPI.Produto.AliquotaIPI / 100));
        }

        public virtual void CalculaTotalProdutos()
        {
            TotalProduto = Items?.Sum(o => o.Total) ?? 0;
        }

        public virtual void CalculaTotalServicos()
        {
            TotalServico = ItemsServicos?.Sum(o => o.Total) ?? 0;
        }

        private void CalculaTotalST()
        {
            TotalST = 0;
            if (TipoPedido != null && TipoPedido.CalcularST)
                TotalST = Items?.Sum(o => o.ValorRetidoICMS) ?? 0;
        }

        public virtual void CalculaTotalPedido(bool NaoSomarFrete, int NaoSomarIpiTotalNota)
        {
            CalculaTotalProdutos();
            CalculaTotalServicos();
            CalculaTotalST();
            CalculaTotalIPI();

            if (NaoSomarFrete)
            {
                this.ValorFrete = this.ValorFrete >= 0 ? -this.ValorFrete : this.ValorFrete;
            }


            this.TotalPedido = this.TotalProduto + this.ValorFrete + this.ValorAcrescimo + (this.TotalST - this.ValorDesconto);
            this.TotalPedido += this.TotalIPI;
            this.TotalPedido += this.TotalServico;
        }

        public virtual void CalculaTotalPedidoSemImpostos()
        {
            CalculaTotalProdutos();
            CalculaTotalServicos();
            CalculaTotalST();
            CalculaTotalIPI();

            this.TotalPedido = this.TotalProduto + this.ValorFrete + this.ValorAcrescimo - this.ValorDesconto;
            this.TotalPedido += this.TotalServico;
        }

        public virtual void RatearAcrescimos()
        {
            RatearAcrescimoProduto();
            RatearAcrescimoServico();
        }

        private void RatearAcrescimoProduto()
        {
            if ((Items.Count == 0) ||
                (TotalProduto == 0)) return;

            foreach (var item in Items)
            {
                decimal Acrescimo = (ValorAcrescimo * Math.Round(item.Total / TotalProduto, 4));
                item.RAT_Acrescimo = Acrescimo;
            }
            var somaRateio = Items.Sum(i => i.RAT_Acrescimo);
            if (somaRateio < ValorAcrescimo)
            {
                var itemMaiorValor = Items.FirstOrDefault(n => n.Total == Items.Max(n2 => n2.Total));
                itemMaiorValor.RAT_Acrescimo += ValorAcrescimo - somaRateio;
            }
        }

        private void RatearAcrescimoServico()
        {
            if (ItemsServicos != null)
            {
                if ((ItemsServicos.Count == 0) ||
                    (TotalServico == 0)) return;

                foreach (var item in ItemsServicos)
                {
                    decimal Acrescimo = (ValorAcrescimo * Math.Round(item.Total / TotalServico, 4));
                    item.RAT_Acrescimo = Acrescimo;
                }
                var somaRateio = ItemsServicos.Sum(i => i.RAT_Acrescimo);
                if (somaRateio < ValorAcrescimo)
                {
                    var itemMaiorValor = ItemsServicos.FirstOrDefault(n => n.Total == Items.Max(n2 => n2.Total));
                    itemMaiorValor.RAT_Acrescimo += ValorAcrescimo - somaRateio;
                }
            }

        }

        public virtual void RatearDesconto()
        {
            RatearDescontoProduto();
            RatearDescontoServico();
        }

        private void RatearDescontoServico()
        {
            if (ItemsServicos != null)
            {
                if ((ItemsServicos.Count == 0) ||
                    (TotalServico == 0)) return;

                foreach (var item in ItemsServicos)
                {
                    item.RAT_Desconto = ValorDesconto * Math.Round(item.Total / TotalServico, 4);
                }
                var somaRateio = ItemsServicos.Sum(i => i.RAT_Desconto);
                if (somaRateio < ValorDesconto)
                {
                    var itemMaiorValor = ItemsServicos.FirstOrDefault(n => n.Total == Items.Max(n2 => n2.Total));
                    itemMaiorValor.RAT_Desconto += ValorDesconto - somaRateio;
                }
            }
        }

        private void RatearDescontoProduto()
        {
            if ((Items.Count == 0) ||
                (TotalProduto == 0)) return;

            foreach (var item in Items)
            {
                item.RAT_Desconto = ValorDesconto * Math.Round(item.Total / TotalProduto, 4);
            }
            var somaRateio = Items.Sum(i => i.RAT_Desconto);
            if (somaRateio < ValorDesconto)
            {
                var itemMaiorValor = Items.FirstOrDefault(n => n.Total == Items.Max(n2 => n2.Total));
                itemMaiorValor.RAT_Desconto += ValorDesconto - somaRateio;
            }
        }

        public virtual void RepassarDescontoPedidoParaItem()
        {
            RatearDesconto();

            RepassarDescontoPedidoParaItemProduto();
            RepassarDescontoPedidoParaItemServico();

        }

        private void RepassarDescontoPedidoParaItemServico()
        {
            if (ItemsServicos.Sum(x => x.RAT_Desconto) == 0) return;

            foreach (var item in ItemsServicos)
            {
                item.PercDesconto = Math.Round((item.RAT_Desconto * 100) / item.Valor, 2);
            }
            ValorDesconto = 0;
        }

        private void RepassarDescontoPedidoParaItemProduto()
        {
            if (Items.Sum(x => x.RAT_Desconto) == 0) return;

            foreach (var item in Items)
            {
                item.PercDesconto = Math.Round((item.RAT_Desconto * 100) / item.ValorProduto, 2);
            }
            ValorDesconto = 0;
        }

        public virtual void RatearFrete()
        {
            if (Items.Count == 0) return;
            foreach (var item in Items)
            {
                if (TotalProduto == 0)
                    item.RAT_Frete = 0;
                else
                    item.RAT_Frete = ValorFrete * Math.Round(item.Total / TotalProduto, 4);
            }

            var somaRateio = Items.Sum(i => i.RAT_Frete);

            if (somaRateio < ValorFrete)
            {
                var itemMaiorValor = Items.FirstOrDefault(n => n.Total == Items.Max(n2 => n2.Total));
                itemMaiorValor.RAT_Frete += ValorFrete - somaRateio;
            }
        }

        public virtual void RecalcularRateios(Configuracao config, bool revenda)
        {
            RatearAcrescimos();
            RatearDesconto();
            RatearFrete();
            RecalcularIPI(config, revenda);
        }

        public virtual void RecalcularIPI(Configuracao config, bool revenda)
        {
            foreach (var item in Items)
            {
                item.CalculaIPI(config, revenda);
            }
        }

        #endregion

        public virtual int? BDelivery { get; set; }

        public virtual int? IDEntregador { get; set; }

        public virtual string Entregador { get; set; }

        public virtual int? BReserva { get; set; } = 0;

        [InformacaoExibicao(24, "Numero Pos", 80, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string NumeroPos { get; set; }

        [InformacaoExibicao(25, "Máquina", 100, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string NomeMaquina { get; set; }
        [InformacaoExibicao(26, "Serial Impressora", 150, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string SerialImpressora { get; set; }
    }
}
