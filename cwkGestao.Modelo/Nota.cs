using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Nota")]
    public class Nota : ModeloBase
    {
        public Nota()
        {
            TipoFrete = 0;
            VolumeQuant = 0;
            VolumeEspecie = "";
            VolumeMarca = "";
            VolumePesoLiquido = 0;
            VolumePesoBruto = 0;
            ValorFrete = 0;
            ValorDesconto = 0;
            ValorAcrescimo = 0;

            NotaICMSs = new List<NotaICMS>();
            NotaItems = new List<NotaItem>();
            NotaParcelas = new List<NotaParcela>();
            NotaItemsServicos = new List<NotaItemServico>();
            NotaImpostoServicos = new List<NotaImpostoServico>();
            Cartas = new List<CartaCorrecao>();
            Custos = new List<Custo>();
            FrenteCaixaNotas = new List<FrenteCaixaNota>();
            NotaReferencias = new List<NotaReferencia>();
        }

        public Nota(int id, DateTime dt, int codigo, int modelodoct, string serie
                    , int numero, string cliente, decimal total, int idfilial
                    , string nomefilial, string tiponota, string status
                    , DateTime incData, string incUsuario, DateTime altData, string altUsuario, int NumeroNFSe, int CodigoPedido, DateTime? DataEnvioNfe
                    , string NumeroEmpenhoNota, string Statuslocal, String pNomeVendedor)
            : this()
        {
            ID = id;
            Dt = dt;
            Codigo = codigo;
            ModeloDocto = modelodoct;
            Serie = serie;
            Numero = numero;
            PessoaNome = cliente;
            TotalNota = total;
            Filial = new Filial() { Nome = nomefilial, Codigo = idfilial };
            TipoNota = new TipoNota() { Nome = tiponota };
            Status = status;
            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
            this.NumeroNFSe = NumeroNFSe;
            this.CodigoPedido = CodigoPedido;
            this.DataEnvioNfe = DataEnvioNfe;
            NumeroEmpenho = NumeroEmpenhoNota;
            this.NomeVendedor = pNomeVendedor;
            this.StatusLocal = Statuslocal;
        }

        public Nota(int id, DateTime dt, int codigo, int modelodoct, string serie
            , int numero, string cliente, decimal total, int idfilial
            , string nomefilial, string tiponota, string status
            , DateTime incData, string incUsuario, DateTime altData, string altUsuario, int NumeroNFSe, int CodigoPedido, DateTime? DataEnvioNfe
            , string NumeroEmpenhoNota, string StatusLocal)
            : this(id, dt, codigo, modelodoct, serie, numero, cliente, total, idfilial, nomefilial, tiponota, status
            , incData, incUsuario, altData, altUsuario, NumeroNFSe, CodigoPedido, DataEnvioNfe, NumeroEmpenhoNota, StatusLocal, "")
        {

        }

        public override ModeloBase Clonar()
        {
            Nota objNotaNew = new Nota();

            if (this.ModeloDocto == 2)
                this.NotaItemsServicos.Select(s => { s.ContratoControle = null; return s; }).ToList();

            AutoMapper.Mapper.CreateMap<Modelo.Nota, Modelo.Nota>()
                .ForMember(dest => dest.ID, o => o.Ignore())
                .ForMember(dest => dest.CodigoPedido, o => o.Ignore())
                .ForMember(dest => dest.NotaComplementada, o => o.Ignore())
                .ForMember(dest => dest.NotaReferenciada, o => o.Ignore())
                .ForMember(dest => dest.BImpressa, o => o.Ignore())
                .ForMember(dest => dest.ChaveNota, o => o.Ignore())
                .ForMember(dest => dest.LogEnvio, o => o.Ignore())
                .ForMember(dest => dest.LogRecibo, o => o.Ignore())
                .ForMember(dest => dest.UltimoXmlRecebido, o => o.Ignore())
                .ForMember(dest => dest.XmlDestinatarioNFe, o => o.Ignore())
                .ForMember(dest => dest.XmlLogEnvNFe, o => o.Ignore())
                .ForMember(dest => dest.XmlLogRecNFe, o => o.Ignore())
                .ForMember(dest => dest.NotaICMSs, o => o.Ignore())
                .ForMember(dest => dest.NotaParcelas, o => o.Ignore())
                .ForMember(dest => dest.Documentos, o => o.Ignore());

            objNotaNew = AutoMapper.Mapper.Map<Modelo.Nota>(this);

            NotaItem.GetClones(ref objNotaNew);
            NotaItemServico.GetClones(ref objNotaNew);
            NotaImpostoServico.GetClones(ref objNotaNew);
            CartaCorrecao.GetClones(ref objNotaNew);

            return objNotaNew;
        }

        public override int ID { get; set; }
        [InformacaoExibicao(0, "Código", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(10, "Filial", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int FilialID { get { return Filial.Codigo; } }
        [InformacaoExibicao(11, "Nome Filial", 75, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual String FilialNome { get { return Filial.Nome; } }
        public virtual Filial Filial { get; set; }
        [InformacaoExibicao(9, "Tipo", 150, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual TipoNota TipoNota { get; set; }
        public virtual InOutType Ent_Sai { get; set; }
        [InformacaoExibicao(2, "Data", 90, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Dt { get; set; }
        public virtual DateTime? DtDigitacao { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual Pessoa Intermediario { get; set; }
        [InformacaoExibicao(7, "Pessoa", 380, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string PessoaNome { get; set; }
        public virtual string PessoaNumero { get; set; }
        public virtual string PessoaCNPJCPF { get; set; }
        public virtual string PessoaInscRG { get; set; }
        public virtual string PessoaEndereco { get; set; }
        public virtual string PessoaBairro { get; set; }
        public virtual string PessoaCidade { get; set; }
        public virtual string PessoaUF { get; set; }
        public virtual string PessoaTelefone { get; set; }

        PessoaVendedor _vendedor;
        public virtual PessoaVendedor Vendedor
        {
            get
            {
                return _vendedor;
            }
            set
            {
                _vendedor = value;
                if ((_vendedor != null) && (_vendedor.Pessoa != null))
                {
                    NomeVendedor = _vendedor.Pessoa.Nome;
                }
            }
        }


        [InformacaoExibicao(9, "Vendedor", 230, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual String NomeVendedor { get; set; }

        public virtual TabelaPreco TabelaPreco { get; set; }
        public virtual Condicao Condicao { get; set; }
        public virtual decimal TotalProduto { get; set; }
        public virtual decimal PercDesconto { get; set; }
        public virtual decimal ValorDesconto { get; set; }
        public virtual decimal ValorAcrescimo { get; set; }
        [InformacaoExibicao(8, "Total", 110, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal TotalNota { get; set; }
        public virtual string Observacao1 { get; set; }
        public virtual string Observacao2 { get; set; }
        public virtual string Observacao3 { get; set; }
        public virtual Identificador identificador { get; set; }
        [InformacaoExibicao(1, "Codigo Pedido", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int CodigoPedido { get; set; }
        [InformacaoExibicao(6, "Numero NFSe", 70, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int NumeroNFSe { get; set; }
        public virtual int Handle { get; set; }
        public virtual int LoteNFSe { get; set; }
        public virtual DateTime? DtHoraCanc { get; set; }
        public virtual DateTime? DtHoraAuto { get; set; }
        public virtual int NumeroNfseSubstituida { get; set; }
        public virtual DateTime? DataEmissaoNfseSubstituida { get; set; }
        public virtual string FrasesNFSe { get; set; }
        public virtual int? ResponsavelRetencao { get; set; }
        [InformacaoExibicao(21, "Data Envio", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.DataHora)]
        public virtual DateTime? DataEnvioNfe { get; set; }
        public virtual bool NotaLimearMes { get; set; }
        
        [InformacaoExibicao(22, "Número Empenho", 110, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string NumeroEmpenho { get; set; }
        
        [InformacaoExibicao(3, "Modelo", 60, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string ModeloDoctoString
        {
            get
            {
                switch (ModeloDocto)
                {
                    case 0:
                        return "";
                    case 1:
                        return "NF";
                    case 33:
                        return "ecf";
                    case 55:
                        return "NFe";
                    case 65:
                        return "NFCe";
                    case 2:
                        return "RPS";
                    case 3:
                        return "NFSe";
                    default:
                        return ModeloDocto.ToString();

                }
            }
        }
        public virtual int ModeloDocto { get; set; }
        [InformacaoExibicao(4, "Série", 70, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string Serie { get; set; }
        [InformacaoExibicao(5, "Numero", 70, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Numero { get; set; }
        public virtual bool BGeraFinanceiro { get; set; }
        public virtual Pessoa PessoaTransportadora { get; set; }
        private string _TransNome;
        [InformacaoExibicao(13, "Tranportadora", 90, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string TransNome { get { return _TransNome; } set { _TransNome = value; } }
        public virtual string TransEndereco { get; set; }
        public virtual string TransCidade { get; set; }
        public virtual string TransUF { get; set; }
        public virtual string TransCNPJCPF { get; set; }
        public virtual string TransPlaca { get; set; }
        public virtual string TransPlacaUF { get; set; }
        public virtual decimal VolumeQuant { get; set; }
        public virtual string VolumeEspecie { get; set; }
        public virtual string VolumeMarca { get; set; }
        public virtual string VolumeNumero { get; set; }
        public virtual decimal VolumePesoBruto { get; set; }
        public virtual decimal VolumePesoLiquido { get; set; }
        [InformacaoExibicao(15, "Usuário Canc", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string CancUsuario { get; set; }
        [InformacaoExibicao(14, "Motivo Canc", 200, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string CancMotivo { get; set; }
        [InformacaoExibicao(13, "Data Canc", 90, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime? CancDt { get; set; }
        public virtual bool? BPessoaContribuinte { get; set; }
        public virtual string PessoaCidadeIBGE { get; set; }
        public virtual string PessoaCEP { get; set; }
        public virtual string ChaveNota { get; set; }
        [InformacaoExibicao(24, "Numero Recibo", 70, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string NumeroRecibo { get; set; }
        [InformacaoExibicao(24, "Numero Protocolo", 70, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string NumeroProtocolo { get; set; }
        public virtual string LogEnvio { get; set; }
        public virtual string LogRecibo { get; set; }
        public virtual string Observacao4 { get; set; }
        public virtual string Observacao5 { get; set; }
        public virtual bool BImpressa { get; set; }
        public virtual bool BImprimeTextoCredidoICMS { get; set; }
        public virtual decimal ValorFrete { get; set; }
        public virtual string ObservacaoSistema { get; set; }
        public virtual string ObservacaoUsuario { get; set; }
        public virtual byte TipoFrete { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string XmlLogEnvNFe { get; set; }
        public virtual string XmlLogRecNFe { get; set; }
        public virtual string XmlDestinatarioNFe { get; set; }
        public virtual bool EnviaTagTotalImposto { get; set; }
        public virtual bool GerouPedidoCompra { get; set; }

        public virtual decimal ValorIPI
        {
            get
            {
                try
                {

                    return this.NotaItems.Sum(i => i.VIPI_O14);
                }
                catch
                {
                    return 0;
                }
            }
            set { }
        }


        [JsonIgnore]
        public virtual IList<Documento> Documentos { get; set; }
        [JsonIgnore]
        public virtual IList<NotaICMS> NotaICMSs { get; set; }
        public virtual IList<NotaItem> NotaItems { get; set; }
        public virtual IList<CartaCorrecao> Cartas { get; set; }
        
        private IList<NotaParcela> notaParcelas;
#pragma warning disable CS0414 // O campo "Nota.valorconfins" é atribuído, mas seu valor nunca é usado
        private object valorconfins = null;
#pragma warning restore CS0414 // O campo "Nota.valorconfins" é atribuído, mas seu valor nunca é usado
        [JsonIgnore]
        public virtual IList<NotaParcela> NotaParcelas
        {
            get { return notaParcelas; }
            set
            {
                notaParcelas = value;
                foreach (NotaParcela notaParcela in value)
                {
                    notaParcela.Nota = this;
                }
            }
        }

        public virtual string UltimoXmlRecebido { get; set; }
        public virtual DateTime? DtEnvio { get; set; }

        public enum StatusTipo
        {
            NaoEnviada,
            Negada,
            Aguardando,
            Autorizada,
            Cancelada,
            Pendente,
            Enviada,
            Registrada,
            Denegada,
            Contingencia,
            ContingenciaOffline,
            Importada,
            ImportacaoManual
        }

        public virtual StatusTipo StatusNota { get; set; }
        public virtual string Status { get; set; }

        [InformacaoExibicao(12, "Status", 100)]
        public virtual string StatusLegivel
        {
            get
            {
                switch (this.Status)
                {
                    case "-1":
                        this.StatusNota = Nota.StatusTipo.NaoEnviada;
                        return "Não enviada";
                    case "0":
                        this.StatusNota = Nota.StatusTipo.Negada;
                        return "Negada";
                    case "1":
                        this.StatusNota = Nota.StatusTipo.Aguardando;
                        return "Aguardando";
                    case "2":
                        this.StatusNota = Nota.StatusTipo.Autorizada;
                        return "Autorizada";
                    case "3":
                        this.StatusNota = Nota.StatusTipo.Cancelada;
                        return "Cancelada";
                    case "4":
                        this.StatusNota = Nota.StatusTipo.Pendente;
                        return "Pendente";
                    case "5":
                        this.StatusNota = Nota.StatusTipo.Enviada;
                        return "Enviada";
                    case "6":
                        this.StatusNota = Nota.StatusTipo.Registrada;
                        return "Registrada";
                    case "7":
                        this.StatusNota = Nota.StatusTipo.Denegada;
                        return "Denegada";
                    case "8":
                        this.StatusNota = Nota.StatusTipo.Contingencia;
                        return "Contingência";
                    case "9":
                        this.StatusNota = Nota.StatusTipo.ContingenciaOffline;
                        return "Contingência OFFLine";
                    case "11":
                        return "Importada";
                    case "12":
                        return "Importação Manual";
                    default:
                        this.StatusNota = Nota.StatusTipo.NaoEnviada;
                        return "Não enviada";
                }
            }
        }

        public virtual string StatusLocal { get; set; }

        [InformacaoExibicao(23, "Status Local", 80, InformacaoExibicao.HAlinhamento.Esquerda)]
          public virtual string StatusLocalLegivel
        {
            get
            {
                if (this.Codigo == 936)
                {
                    
                }
                switch (this.StatusLocal)
                {
                    case "0":
                        return "Financeiro";
                    case "1":
                        return "Logistica";
                    default:
                        return "Financeiro";
                }
            }
        }

        public virtual string StatusMotivo { get; set; }

        [InformacaoExibicao(16, "Data Inclusão", 100)]
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

        [InformacaoExibicao(18, "Data Alteração", 100)]
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

        public virtual PessoaEndereco EnderecoEntrega { get; set; }

        public virtual IList<NotaItemServico> NotaItemsServicos { get; set; }
        [JsonIgnore]
        public virtual IList<NotaImpostoServico> NotaImpostoServicos { get; set; }

        public virtual decimal TotalServico { get; set; }

        public virtual Nota NotaComplementada { get; set; }

        public virtual Nota NotaReferenciada { get; set; }

        public virtual IList<FrenteCaixaNota> FrenteCaixaNotas { get; set; }
        public virtual IList<Custo> Custos { get; set; }

        public virtual string ZA02_UFEmbarq { get; set; }
        public virtual string ZA03_xLocEmbarq { get; set; }
        public virtual decimal W11_vII { get; set; }
        public virtual decimal ValorSeguro { get; set; }
        public virtual decimal OutrasDespesas { get; set; }
        public virtual bool ServicoRealizadoCidadeCliente { get; set; }
        public virtual int TipoDeEntrada { get; set; }
        public virtual int TipoCTe { get; set; }
        public virtual int idDest { get; set; }
        public virtual string ChaveCTe { get; set; }

        public virtual int? IDEnderecoDestinatario { get; set; }

        public virtual string EmailDestinatario { get; set; }

        public virtual decimal IcmsSubstituicao
        {
            get
            {
                try
                {
                    if (this.NotaICMSs.Sum(n => n.ValorSubst) < 0)
                        return 0;
                    return this.NotaICMSs.Sum(n => n.ValorSubst);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public virtual void RatearOutrasDespesas()
        {
            if (NotaItems.Count == 0) return;

            decimal somaProdutos = 0;

            foreach (var item in NotaItems)
                somaProdutos += item.Total;

            foreach (var item in NotaItems)
            {
                decimal rateio = (item.Total * OutrasDespesas) / somaProdutos;
                item.RateioOutrasDespesas = Math.Round(rateio, 2);
            }
            var somaRateio = NotaItems.Sum(i => i.RateioOutrasDespesas);
            if (somaRateio >= OutrasDespesas)
            {
                var itemMaiorValor = NotaItems.Where(n => n.Total == NotaItems.Max(n2 => n2.Total)).FirstOrDefault();
                itemMaiorValor.RateioOutrasDespesas += OutrasDespesas - somaRateio;
            }
        }

        public virtual void RatearFrete()
        {
            if (NotaItems.Count == 0) return;

            foreach (var item in NotaItems)
            {
                item.RateioFrete = Decimal.Round(ValorFrete * item.Total / (TotalProduto == 0 ? 1 : TotalProduto), 2);
            }

            var somaRateio = NotaItems.Sum(i => i.RateioFrete);

            if (somaRateio != ValorFrete)
            {
                var itemMaiorValor = NotaItems.Where(n => n.Total == NotaItems.Max(n2 => n2.Total)).FirstOrDefault();
                itemMaiorValor.RateioFrete += ValorFrete - somaRateio;
            }
        }

        public virtual void RatearSeguro()
        {
            if (NotaItems.Count == 0) return;

            foreach (var item in NotaItems)
            {
                if (TotalProduto == 0)
                {
                    item.RateioSeguro = 0;
                }
                else
                {
                    item.RateioSeguro = Math.Round(ValorSeguro * Math.Round(item.Total / TotalProduto, 4),2);
                }

            }
            var somaRateio = NotaItems.Sum(i => i.RateioSeguro);
            if (somaRateio != ValorSeguro)
            {
                var itemMaiorValor = NotaItems.Where(n => n.Total == NotaItems.Max(n2 => n2.Total)).FirstOrDefault();
                itemMaiorValor.RateioSeguro += ValorSeguro - somaRateio;
            }
        }

        public virtual void RatearDesconto()
        {
            if (NotaItems.Count == 0) return;

            foreach (var item in NotaItems)
            {
                if (TotalProduto == 0)
                {
                    item.RAT_Desconto = 0;
                }
                else
                {
                    item.RAT_Desconto = Math.Round(ValorDesconto * Math.Round(item.Total / TotalProduto, 4),2);
                }
            }
            var somaRateio = NotaItems.Sum(i => i.RAT_Desconto);
            if (somaRateio != ValorDesconto)
            {
                var itemMaiorValor = NotaItems.Where(n => n.Total == NotaItems.Max(n2 => n2.Total)).FirstOrDefault();
                itemMaiorValor.RAT_Desconto += ValorDesconto - somaRateio;
            }
        }

        public virtual void RecalcularRateios()
        {
            RatearFrete();
            RatearSeguro();
            RatearOutrasDespesas();
            RatearDesconto();
        }

        public virtual void CalcularCustoUnit(Configuracao config)
        {

            if (Ent_Sai != InOutType.Entrada || !config.UtilizaCustoNota)
                return;

            foreach (var item in NotaItems)
            {
                decimal valorpis = (item.PIS_CREDEB == false ? item.VPIS_Q09 : 0m);
                decimal valorcofins = (item.COFINS_CREDEB == false ? item.VCOFINS_S11 : 0m);
                decimal valoricms = (item.ICMS_CREDEB == false ? item.ValorICMS : 0m);
                decimal valoripi = (item.IPI_CREDEB == false ? item.VIPI_O14 : 0m);


                item.CustoUnit = System.Math.Round((((item.Total + valorpis + valorcofins + valoripi + valoricms + item.RateioFrete + item.RateioSeguro + item.RateioOutrasDespesas + item.P04_vII + item.P03_vDespAdu) - item.RAT_Desconto) / item.Quantidade), 4);
            }
        }
        public virtual void CalcularUltimoCusto()
        {
            if (Ent_Sai != InOutType.Entrada)
                return;

            foreach (var item in NotaItems)
            {
                decimal valorpis = (item.PIS_CREDEB == false ? item.VPIS_Q09 : 0m);
                decimal valorcofins = (item.COFINS_CREDEB == false ? item.VCOFINS_S11 : 0m);
                decimal valoricms = (item.ICMS_CREDEB == false ? item.ValorICMS : 0m);
                decimal valoripi = (item.IPI_CREDEB == false ? item.VIPI_O14 : 0m);

                item.UltimoCusto = System.Math.Round((((item.Total + valorpis + valorcofins + valoripi + valoricms + item.RateioFrete + item.RateioSeguro + item.RateioOutrasDespesas + item.P04_vII + item.P03_vDespAdu) - item.RAT_Desconto) / item.Quantidade), 4);
            }
        }

        public virtual decimal BuscaValorICMSDesoneracao(int naoSomarTotal)
        {
            if (this.NotaItems != null && naoSomarTotal == 0)
            {
                return this.NotaItems.Sum(s => s.ValorICMSDesoneracao);
            }
            else
            {
                return 0;
            }
        }
        public virtual LocalEstoque LocalEstoqueNota { get; set; }

        public virtual string EmailNota
        {
            get
            {
                try
                {
                    String retorno = String.Empty;

                    if (ModeloDocto == 2 || ModeloDocto == 3)
                        retorno = String.Join(",", Pessoa.PessoaEmails.Where(x => x.BNFSe == true).Select(s => s.Email).ToArray());
                    else
                        retorno = String.Join(",", Pessoa.PessoaEmails.Where(x => x.BNFe == true).Select(s => s.Email).ToArray());

                    if (String.IsNullOrEmpty(retorno))
                        retorno = EmailDestinatario == null ? String.Empty : EmailDestinatario;

                    return retorno;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static bool IsNullOrEmpty(Nota objNotaSelecionado)
        {
            return objNotaSelecionado == null || objNotaSelecionado == new Nota();
        }

        public virtual decimal ValorTroco { get; set; }
        public virtual IList<NotaReferencia> NotaReferencias { get; set; }
    }
}
