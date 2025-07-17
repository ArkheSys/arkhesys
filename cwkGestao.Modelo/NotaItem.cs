using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Nota Produto")]
    public class NotaItem : ModeloBase
    {
        public NotaItem()
        {
            QuantidadeEntrada = 0;
            Quantidade = 1;
            FatorConversao = 1;
            ValorDesconto = 0;
            TextoLei = String.Empty;
            TAG_CST = String.Empty;

            Rateio_NotaItems = new List<Rateio_NotaItem>();
            AdicoesNotaItem = new List<AdicaoNotaItem>();
            Estoques = new List<NotaItemEstoque>();
            PedidoItens = new List<NotaItemPedidoItem>();
            ImeisEntrada = new List<Imei>();
            ImeisSaida = new List<Imei>();
        }

        public override int ID { get; set; }

        public virtual Nota Nota { get; set; }

        [InformacaoExibicao(0, "Seq", 50, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Sequencia { get; set; }

        [InformacaoExibicao(1, "Produto", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string CodigoProduto
        {
            get
            {
                try
                {
                    return Produto.Codigo;
                }
                catch { return String.Empty; }
            }
        }

        public virtual Produto Produto { get; set; }
        [InformacaoExibicao(2, "Descrição", 200)]
        public virtual string ProdutoNome { get; set; }

        public virtual string ProdutoDescReduzida { get; set; }
        [InformacaoExibicao(3, "Quantidade", 75)]
        public virtual decimal QuantidadeExibicaoGrid
        {
            get
            {
                if (QuantidadeEntrada == 0)
                    return Quantidade;
                else 
                    return QuantidadeEntrada;
            }
        }

        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(4, "Unidade", 60)]
        public virtual string UnidadeExibicaoGrid
        {
            get
            {
                if (string.IsNullOrEmpty(Unidade))
                    return UnidadeEntrada;

                return Unidade;
                //if (QuantidadeEntrada == 0)
                    //return Unidade;
                //else 
                //    return UnidadeEntrada;
            }
        }

        public virtual string Unidade { get; set; }

        public virtual decimal ValorCalculado { get; set; }
        [InformacaoExibicao(5, "Valor", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }

        public virtual decimal SubTotal { get; set; }

        public virtual decimal PercDesconto { get; set; }

        public virtual decimal ValorDesconto { get; set; }

        public virtual decimal RAT_Desconto { get; set; }

        public virtual decimal RAT_Acrescimo { get; set; }
        [InformacaoExibicao(6, "Total", 130, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Total { get; set; }

        public virtual decimal PesoBruto { get; set; }

        public virtual decimal PesoLiquido { get; set; }

        public virtual DateTime Dt { get; set; }

        public virtual bool BAtualizaEstoque { get; set; }

        public virtual int Ent_Sai { get; set; }

        public virtual bool CalcCustoMedio { get; set; }

        public virtual decimal ValorCustoMedio { get; set; }

        public virtual ICMS ICMS { get; set; }

        public virtual decimal BaseICMS { get; set; }

        public virtual decimal AliqICMS { get; set; }

        public virtual decimal AliqICMSNormal { get; set; }

        public virtual decimal ValorICMS { get; set; }

        public virtual decimal ValorIsentoICMS { get; set; }

        public virtual decimal ValorOutroICMS { get; set; }

        public virtual decimal ValorRetidoICMS { get; set; }

        public virtual bool BCancelado { get; set; }

        public virtual string SitTrib { get; set; }

        public virtual decimal BaseICMSSubst { get; set; }

        public virtual decimal ValorICMSReducao { get; set; }

        public virtual decimal ValorICMSReducaoDif { get; set; }

        public virtual PedidoItem PedidoItem { get; set; }

        public virtual TabelaCFOP CFOP { get; set; }

        public virtual string TextoLei { get; set; }

        public virtual string TAG_CST { get; set; }

        public virtual int modBC_N13 { get; set; }

        public virtual int modBCST_N18 { get; set; }

        public virtual string CST_Pis { get; set; }

        public virtual decimal VBC_Q07 { get; set; }

        public virtual decimal PPIS_Q08 { get; set; }

        public virtual decimal VPIS_Q09 { get; set; }

        public virtual string CST_Cofins { get; set; }

        public virtual string CST_Ipi { get; set; }

        public virtual int orig_N11 { get; set; }

        public virtual decimal VBC_S07 { get; set; }

        public virtual decimal PCOFINS_S08 { get; set; }

        public virtual decimal VCOFINS_S11 { get; set; }

        public virtual decimal VBC_O10 { get; set; }

        public virtual decimal PIPI_O13 { get; set; }

        public virtual decimal VIPI_O14 { get; set; }

        public virtual decimal PRedBC_N14 { get; set; }

        // Valores FCP

        public virtual decimal vBCFCP_N17a { get; set; }
        
        public virtual decimal pFCP_N17b { get; set; }
        
        public virtual decimal vFCP_N17c { get; set; }
        
        public virtual decimal vBCFCPST_N23a { get; set; }
        
        public virtual decimal pFCPST_N23b { get; set; }
        
        public virtual decimal vFCPST_N23d { get; set; }

        //--------

        // Diferimento
        public virtual decimal AliquotaDiferimento { get; set; }
        public virtual decimal ValorIcmsDiferimento { get; set; }
        public virtual decimal ValorIcmsOp { get; set; }


        public virtual string cBenef_I05f { get; set; }

        public virtual int indEscala_I05d { get; set; }

        public virtual string CNPJFab_I05e { get; set; }

        public virtual decimal PICMSST_N22 { get; set; }
        
        public virtual string cEnq_O06 { get; set; }

        public virtual string InfAdicionais { get; set; }

        public virtual string AliqCupom { get; set; }
        public virtual string Cest { get; set; }

        public virtual decimal AliqInterna { get; set; }

        public virtual ProdutoFCI ProdutoFCI { get; set; }

        public virtual decimal ComissaoPorcentagem { get; set; }
        public virtual byte TipoComissao { get; set; }
        public virtual decimal PRedBCST_N20 { get; set; }
        public virtual decimal PMVAST_N19 { get; set; }
        public virtual decimal PCredSN_N29 { get; set; }
        public virtual decimal VCredICMSSN_N30 { get; set; }


        public virtual string I19_nDI { get; set; }
        public virtual DateTime? I20_dDI { get; set; }
        public virtual string I21_xLocDesemb { get; set; }
        public virtual string I22_UFDesemb { get; set; }
        public virtual DateTime? I23_dDesemb { get; set; }
        public virtual string I24_cExportador { get; set; }        
        public virtual decimal P02_vBC { get; set; }
        public virtual decimal P03_vDespAdu { get; set; }
        public virtual decimal P04_vII { get; set; }
        public virtual decimal P05_vIOF { get; set; }
        public virtual decimal RateioFrete { get; set; }
        public virtual decimal RateioCusto { get; set; }
        public virtual decimal RateioSeguro { get; set; }
        public virtual decimal RateioOutrasDespesas { get; set; }
        public virtual decimal CMVUnit { get; set; }
        public virtual decimal CustoUnit { get; set; }
        public virtual decimal UltimoCusto { get; set; }
        public virtual IList<Rateio_NotaItem> Rateio_NotaItems { get; set; }
        public virtual IList<AdicaoNotaItem> AdicoesNotaItem { get; set; }
        public virtual bool PIS_CREDEB { get; set; }
        public virtual bool COFINS_CREDEB { get; set; }
        public virtual bool IPI_CREDEB { get; set; }
        public virtual bool ICMS_CREDEB { get; set; }
        public virtual IList<NotaItemEstoque> Estoques { get; set; }
        public virtual IList<NotaItemPedidoItem> PedidoItens { get; set; }

        public virtual IList<Imei> ImeisEntrada { get; set; }
        public virtual IList<Imei> ImeisSaida { get; set; }

        public virtual decimal ReducaoImposto { get; set; }

        public virtual decimal QuantidadeEntrada { get; set; }
        public virtual string UnidadeEntrada { get; set; }
        public virtual decimal FatorConversao { get; set; }
        public virtual IList<ContratoControle> ContratoControle { get; set; }
        public virtual ContratoProduto ContratoProduto { get; set; }
        public virtual TextoLei Texto { get; set; }
        public virtual int IDOSProdutoItem { get; set; }
        public virtual decimal vICMSUFDest_NA15 { get; set; }
        public virtual decimal vICMSUFRemet_NA17 { get; set; }
        public virtual decimal pICMSInter { get; set; }
        public virtual string NumDrawBack { get; set; }

        private decimal? _TotalImpostos;
        public virtual decimal? TotalImpostos
        {
            get
            {
                return  ValorICMS + this.VPIS_Q09 + this.VCOFINS_S11 + this.VIPI_O14;
            }

            protected set
            {
                _TotalImpostos = value;
            }
        }

        //Não grava no banco
        public virtual int CSOSN { get; set; }

        public virtual decimal ValorICMSDesoneracao { get; set; }

        public virtual int MotivoICMSDesoneracao { get; set; }
        public enum MotivoDesoneracaoType20
        {
            EmBranco = -1,
            UsoNaAgropecuaria = 3,
            Outros = 9,
            OrgaoDeFomentoEDesenvolvimentoAgropecuario = 12
        }

        public enum MotivoDesoneracaoType30
        {
            EmBranco = -1,
            UtilitariosEMotocicletasDaAmazoniaOcidental = 6,
            SUFRAMA = 7,
            Outros = 9
        }

        public enum MotivoDesoneracaoType40_41_50
        {
            EmBranco = -1,
            Taxi = 1,
            ProdutorAgropecuario = 3,
            FrotistaLocadora = 4,
            DiplomaticoConsular = 5,
            UtilitariosEMotocicletasDaAmazoniaOcidental = 6,
            SUFRAMA = 7,
            VendaOrgaoPublico = 8,
            Outros = 9,
            DeficienteCondutor = 10,
            DeficienteNaoCondutor = 11
        }

        public virtual ViaTranspType ViaTransp { get; set; }
        public enum ViaTranspType
        {
            EmBranco = -1,
            Maritima = 1,
            Fluvial = 2,
            Lacustre = 3,
            Aerea = 4,
            Postal = 5,
            Ferroviaria = 6,
            Rodoviaria = 7,
            CondutoRedeTransmissao = 8,
            MeiosProprios = 9,
            EntradaSaidaficta = 10
        }

        public virtual decimal ValorFreteRenovacaoMarinhaMercante { get; set; }

        public virtual string GetReadableName(object enumvalue)
        {
            int enumint = 0;
            try
            {
                enumint = (int)enumvalue;
            }
            catch
            {
                throw new ArgumentException("valor não é um enum");
            }
            String readableName = "";

            if (enumvalue.GetType().Equals(typeof(MotivoDesoneracaoType20)))
            {
                switch (enumint)
                {
                    case (int)MotivoDesoneracaoType20.UsoNaAgropecuaria:
                        readableName = "Uso na agropecuária";
                        break;
                    case (int)MotivoDesoneracaoType20.Outros:
                        readableName = "Outros";
                        break;
                    case (int)MotivoDesoneracaoType20.OrgaoDeFomentoEDesenvolvimentoAgropecuario:
                        readableName = "Órgão de fomento e desenvolvimento agropecuário";
                        break;
                    default:
                        readableName = "";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(MotivoDesoneracaoType30)))
            {
                switch (enumint)
                {
                    case (int)MotivoDesoneracaoType30.UtilitariosEMotocicletasDaAmazoniaOcidental:
                        readableName = @"Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio 
                                        (Resolução 714/88 e 790/94 – CONTRAN e suas alterações)";
                        break;
                    case (int)MotivoDesoneracaoType30.SUFRAMA:
                        readableName = "Suframa";
                        break;
                    case (int)MotivoDesoneracaoType30.Outros:
                        readableName = "Outros";
                        break;
                    default:
                        readableName = "";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(MotivoDesoneracaoType40_41_50)))
            {
                switch (enumint)
                {
                    case (int)MotivoDesoneracaoType40_41_50.Taxi:
                        readableName = "Táxi";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.ProdutorAgropecuario:
                        readableName = "Produtor Agropecuário";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.FrotistaLocadora:
                        readableName = "Frotista/Locadora";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.DiplomaticoConsular:
                        readableName = "Diplomático/Consular";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.UtilitariosEMotocicletasDaAmazoniaOcidental:
                        readableName = @"Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio 
                                        (Resolução 714/88 e 790/94 – CONTRAN e suas alterações)";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.SUFRAMA:
                        readableName = "Suframa";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.VendaOrgaoPublico:
                        readableName = "Venda a Órgão Público";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.Outros:
                        readableName = "Outros";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.DeficienteCondutor:
                        readableName = "Deficiente Condutor (Convênio ICMS 38/12)";
                        break;
                    case (int)MotivoDesoneracaoType40_41_50.DeficienteNaoCondutor:
                        readableName = "Deficiente Não Condutor (Convênio ICMS 38/12)";
                        break;
                    default:
                        readableName = "";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(ViaTranspType)))
            {
                switch (enumint)
                {
                    case (int)ViaTranspType.Maritima:
                        readableName = "Marítima";
                        break;
                    case (int)ViaTranspType.Fluvial:
                        readableName = "Fluvial";
                        break;
                    case (int)ViaTranspType.Lacustre:
                        readableName = "Lacustre";
                        break;
                    case (int)ViaTranspType.Aerea:
                        readableName = "Aérea";
                        break;
                    case (int)ViaTranspType.Postal:
                        readableName = "Postal";
                        break;
                    case (int)ViaTranspType.Ferroviaria:
                        readableName = "Ferroviária";
                        break;
                    case (int)ViaTranspType.Rodoviaria:
                        readableName = "Rodoviária";
                        break;
                    case (int)ViaTranspType.CondutoRedeTransmissao:
                        readableName = "Conduto / Rede Transmissão";
                        break;
                    case (int)ViaTranspType.MeiosProprios:
                        readableName = "Meios Próprios";
                        break;
                    case (int)ViaTranspType.EntradaSaidaficta:
                        readableName = "Entrada / Saída ficta";
                        break;
                    default:
                        readableName = "";
                        break;
                }
            }
            else
            {
                throw new ArgumentException("valor de enum inválido");
            }
            return readableName;
        }


        public static int PegaIndiceMotivoDesoneracao(string tag, int motivoDesoneracaoSelecionado)
        {
            switch (tag)
            {
                case "20":
                    return PegaIndiceMotivoDesoneracaoType20(motivoDesoneracaoSelecionado);
                case "30":
                    return PegaIndiceMotivoDesoneracaoType30(motivoDesoneracaoSelecionado);
                case "40":
                case "41":
                case "50":
                    return PegaIndiceMotivoDesoneracaoType40_41_50(motivoDesoneracaoSelecionado);
                default:
                    return -1;
            }
        }

        private static int PegaIndiceMotivoDesoneracaoType20(int motivoDesoneracaoSelecionado)
        {
            int retorno;
            switch ((NotaItem.MotivoDesoneracaoType20)motivoDesoneracaoSelecionado)
            {
                case NotaItem.MotivoDesoneracaoType20.UsoNaAgropecuaria:
                    retorno = 0;
                    break;
                case NotaItem.MotivoDesoneracaoType20.Outros:
                    retorno = 1;
                    break;
                case NotaItem.MotivoDesoneracaoType20.OrgaoDeFomentoEDesenvolvimentoAgropecuario:
                    retorno = 2;
                    break;
                default:
                    retorno = -1;
                    break;
            }
            return retorno;
        }

        private static int PegaIndiceMotivoDesoneracaoType30(int motivoDesoneracaoSelecionado)
        {
            int retorno;
            switch ((NotaItem.MotivoDesoneracaoType30)motivoDesoneracaoSelecionado)
            {
                case NotaItem.MotivoDesoneracaoType30.UtilitariosEMotocicletasDaAmazoniaOcidental:
                    retorno = 0;
                    break;
                case NotaItem.MotivoDesoneracaoType30.SUFRAMA:
                    retorno = 1;
                    break;
                case NotaItem.MotivoDesoneracaoType30.Outros:
                    retorno = 2;
                    break;
                default:
                    retorno = -1;
                    break;
            }
            return retorno;
        }

        private static int PegaIndiceMotivoDesoneracaoType40_41_50(int motivoDesoneracaoSelecionado)
        {
            int retorno;
            switch ((NotaItem.MotivoDesoneracaoType40_41_50)motivoDesoneracaoSelecionado)
            {
                case NotaItem.MotivoDesoneracaoType40_41_50.Taxi:
                    retorno = 0;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.ProdutorAgropecuario:
                    retorno = 1;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.FrotistaLocadora:
                    retorno = 2;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.DiplomaticoConsular:
                    retorno = 3;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.UtilitariosEMotocicletasDaAmazoniaOcidental:
                    retorno = 4;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.SUFRAMA:
                    retorno = 5;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.VendaOrgaoPublico:
                    retorno = 6;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.Outros:
                    retorno = 7;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.DeficienteCondutor:
                    retorno = 8;
                    break;
                case NotaItem.MotivoDesoneracaoType40_41_50.DeficienteNaoCondutor:
                    retorno = 9;
                    break;
                default:
                    retorno = -1;
                    break;
            }
            return retorno;
        }

        public static int PegaIndiceViaTransp(NotaItem.ViaTranspType ViaTranspType)
        {
            int retorno;
            switch (ViaTranspType)
            {
                case NotaItem.ViaTranspType.Maritima:
                    retorno = 0;
                    break;
                case NotaItem.ViaTranspType.Fluvial:
                    retorno = 1;
                    break;
                case NotaItem.ViaTranspType.Lacustre:
                    retorno = 2;
                    break;
                case NotaItem.ViaTranspType.Aerea:
                    retorno = 3;
                    break;
                case NotaItem.ViaTranspType.Postal:
                    retorno = 4;
                    break;
                case NotaItem.ViaTranspType.Ferroviaria:
                    retorno = 5;
                    break;
                case NotaItem.ViaTranspType.Rodoviaria:
                    retorno = 6;
                    break;
                case NotaItem.ViaTranspType.CondutoRedeTransmissao:
                    retorno = 7;
                    break;
                case NotaItem.ViaTranspType.MeiosProprios:
                    retorno = 8;
                    break;
                case NotaItem.ViaTranspType.EntradaSaidaficta:
                    retorno = 9;
                    break;
                default:
                    retorno = -1;
                    break;
            }
            return retorno;
        }

        public static bool VerificaNotaDevolucao(Nota nota)
        {
            if (nota.TipoNota.Operacao.CFOP_DEstado.BDevolucao ||
                nota.TipoNota.Operacao.CFOP_FEstado_C.BDevolucao ||
                nota.TipoNota.Nome.Contains("DEVOLUCAO") ||
                nota.TipoNota.Nome.Contains("Devolucao"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static IList<Produto> GetProdutos(IList<NotaItem> lstNotaItens)
        {
            IList<Produto> lstProdutos = new List<Produto>();

            foreach (var notaItem in lstNotaItens ?? new List<NotaItem>())
            {
                lstProdutos.Add(notaItem.Produto);
            }

            return lstProdutos;
        }

        public static NotaItem CloneNewObject(NotaItem objNotaItem)
        {
            AutoMapper.Mapper.CreateMap<NotaItem, NotaItem>()
                    .ForMember(dest => dest.ID, o => o.Ignore())
                    .ForMember(dest => dest.ImeisEntrada, o => o.Ignore())
                    .ForMember(dest => dest.ImeisSaida, o => o.Ignore())
                    .ForMember(dest => dest.ContratoControle, o => o.Ignore());

            return AutoMapper.Mapper.Map<NotaItem, NotaItem>(objNotaItem);
        }

        public static void GetClones(ref Nota objNota)
        {
            IList<NotaItem> lstNotaItems = new List<NotaItem>(objNota.NotaItems);
            objNota.NotaItems = new List<NotaItem>();
            foreach (var NotaItem in lstNotaItems)
            {
                NotaItem Item = NotaItem.CloneNewObject(NotaItem);
                Item.Nota = objNota;
                objNota.NotaItems.Add(Item);
            }
        }

        public virtual int? IDNcm { get; set; }

    }
}
