using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo.Auxiliares;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo de Movimentação")]
    public class TipoNota : ModeloBase, IEnumInterpreter
    {
        public static string trataNull(object obj)
        {
            if (obj.ToString() != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }
        public enum CategoriaMovimentacao
        {
            Produtos = 0,
            Servicos = 1,
            ProdutosEServicos = 2
        }

        public enum TipoEntrada
        {
            Saida = -1,
            OutrasEntradas = 0,
            InsumoEnergia = 1,
            InsumoAgua = 2,
            InsumoGas = 3,
            Transporte = 4,
            Telecomunicacoes = 5
        }

        public enum IndPres
        {
            OperacaoPresencial = 1,
            OperacaoNaoPresencialInternet = 2,
            OperacaoNaoPresencialAtendimento = 3,
            OperacaoNaoPresencialOutros = 9
        }

        public enum IndInter
        {
            OperacaoSemIntermediador = 0,
            OperacaoSitePlataformaTerceiros = 1
        }

        public virtual IndPres IdentificadorDestinatario { get; set; }

        public virtual IndInter IndIntermed { get; set; }

        public enum ModeloDocumento
        {
            Balanco = 0,
            ModeloUm = 1,
            CupomFiscal = 33,
            NFe = 55,
            RPS = 2,
            NFSe = 3,
            NFCe = 65
        }

        public TipoNota()
        {
            Ent_Sai = 1;
            Categoria = CategoriaMovimentacao.Produtos;
            ModeloDocto = ModeloDocumento.ModeloUm;
        }

        public enum EnumTipoFrete
        {
            Emitente = 0,
            Destinatario = 1,
            Terceiros = 2,
            ProprioRemetente = 3,
            ProprioDestinatario = 4,
            SemFrete = 9
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(0, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }


        [InformacaoExibicao(10, "Filial", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int FilialID { get { return Filial == null? 1: Filial.Codigo; } }
        [InformacaoExibicao(11, "Nome Filial", 75, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual String FilialNome { get { return Filial == null? "" : Filial.Nome; } }
        public virtual Filial Filial { get; set; }


        private string _Ordem;
        [InformacaoExibicao(2, "Ordem", 100)]
        public virtual string Ordem { get { return _Ordem; } set { _Ordem = value; } }

        private int _Ent_Sai;
        [InformacaoExibicao(3, "Tipo", 100)]
        public virtual string Ent_Sai_Str { get { return _Ent_Sai == 1 ? "Entrada" : "Saída"; } set { Ent_Sai_Str = value; } }
        public virtual int Ent_Sai { get { return _Ent_Sai; } set { _Ent_Sai = value; } }

        private string _Nome;
        [InformacaoExibicao(1, "Nome", 550, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private bool _bAtualizaEstoque;
        public virtual bool BAtualizaEstoque { get { return _bAtualizaEstoque; } set { _bAtualizaEstoque = value; } }

        private bool _bAtualizaPedido;
        public virtual bool BAtualizaPedido { get { return _bAtualizaPedido; } set { _bAtualizaPedido = value; } }

        private bool _bPermiteEstqueNegativo;
        public virtual bool BPermiteEstqueNegativo { get { return _bPermiteEstqueNegativo; } set { _bPermiteEstqueNegativo = value; } }

        private bool _GeraCredito;
        public virtual bool GeraCredito { get { return _GeraCredito; } set { _GeraCredito = value; } }

        private bool _bGeraFinanceiro;
        public virtual bool BGeraFinanceiro { get { return _bGeraFinanceiro; } set { _bGeraFinanceiro = value; } }

        private bool _bTransferencia;
        public virtual bool BTransferencia { get { return _bTransferencia; } set { _bTransferencia = value; } }

        private int _TransfHandle;
        public virtual int TransfHandle { get { return _TransfHandle; } set { _TransfHandle = value; } }

        private int _NFOrigem;
        public virtual int NFOrigem { get { return _NFOrigem; } set { _NFOrigem = value; } }

        private string _Serie;
        public virtual string Serie { get { return _Serie; } set { _Serie = value; } }

        private ModeloDocumento _ModeloDocto;
        public virtual ModeloDocumento ModeloDocto { get { return _ModeloDocto; } set { _ModeloDocto = value; } }

        private bool _bCalcCustoMedio;
        public virtual bool BCalcCustoMedio { get { return _bCalcCustoMedio; } set { _bCalcCustoMedio = value; } }

        private Banco _Banco;
        public virtual Banco Banco { get { return _Banco; } set { _Banco = value; } }

        private Portador _Portador;
        public virtual Portador Portador { get { return _Portador; } set { _Portador = value; } }

        private Acrescimo _Acrescimo;
        public virtual Acrescimo Acrescimo { get { return _Acrescimo; } set { _Acrescimo = value; } }

        private Historico _Historico;
        public virtual Historico Historico { get { return _Historico; } set { _Historico = value; } }

        private string _ComplementoHist;
        public virtual string ComplementoHist { get { return _ComplementoHist; } set { _ComplementoHist = value; } }

        private PlanoConta _PlanoConta;
        public virtual PlanoConta PlanoConta { get { return _PlanoConta; } set { _PlanoConta = value; } }

        private Operacao _Operacao;
        public virtual Operacao Operacao { get { return _Operacao; } set { _Operacao = value; } }

        private bool _bImportacao;
        public virtual bool BImportacao { get { return _bImportacao; } set { _bImportacao = value; } }

        private bool _bDevolucao;
        public virtual bool BDevolucao { get { return _bDevolucao; } set { _bDevolucao = value; } }

        private Impressora _Impressora;
        public virtual Impressora Impressora { get { return _Impressora; } set { _Impressora = value; } }

        private string _Observacao;
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private Nullable<int> _TipoFrete;
        public virtual Nullable<int> TipoFrete { get { return _TipoFrete; } set { _TipoFrete = value; } }

        public virtual bool SemPagamento { get; set; }

        public virtual bool BImportacaoXmlManual { get; set; }

        public virtual EnumTipoFrete enumTipoFreteSelecionado
        {
            get
            {
                return (EnumTipoFrete)_TipoFrete;
                /*
                switch ((EnumTipoFrete)_TipoFrete)
                {
                    case EnumTipoFrete.Emitente:
                        return EnumTipoFrete.Emitente;
                    case EnumTipoFrete.Destinatario:
                        return EnumTipoFrete.Destinatario;
                    case EnumTipoFrete.Terceiros:
                        return EnumTipoFrete.Terceiros;
                    default:
                        return EnumTipoFrete.SemFrete;
                }
                */
            }
        }

        public virtual void CriaDicionarioTipoFrete()
        {
            _dicionarioTipoFrete = new Dictionary<EnumTipoFrete, Int32>();
            _dicionarioTipoFrete.Add(EnumTipoFrete.Emitente, 0);
            _dicionarioTipoFrete.Add(EnumTipoFrete.Destinatario, 1);
            _dicionarioTipoFrete.Add(EnumTipoFrete.Terceiros, 2);
            _dicionarioTipoFrete.Add(EnumTipoFrete.ProprioRemetente, 3);
            _dicionarioTipoFrete.Add(EnumTipoFrete.ProprioDestinatario, 4);
            _dicionarioTipoFrete.Add(EnumTipoFrete.SemFrete, 5);
        }

        private Dictionary<EnumTipoFrete, Int32> _dicionarioTipoFrete;
        public virtual Dictionary<EnumTipoFrete, Int32> dicionarioTipoFrete
        {
            get
            {
                return _dicionarioTipoFrete;
            }
            set
            {
                CriaDicionarioTipoFrete();
            }
        }

        private IList<TipoPedido> _TipoPedidos;
        public virtual IList<TipoPedido> TipoPedidos { get { return _TipoPedidos; } set { _TipoPedidos = value; } }

        public virtual PlanoConta PlanoContaFrete { get; set; }

        public virtual PlanoConta PlanoContaIPI { get; set; }

        public virtual PlanoConta PlanoContaServico { get; set; }

        public virtual CategoriaMovimentacao Categoria { get; set; }

        public virtual bool ValorSTPrimeiraParcela { get; set; }

        public virtual bool TotalizarVolumes { get; set; }

        public virtual bool AgruparItensFaturamentoLote { get; set; }

        public virtual LocalEstoque LocalEstoqueTipoNota { get; set; }

        public virtual TipoEntrada TipoDeEntrada { get; set; }

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
            if (enumvalue.GetType().Equals(typeof(ModeloDocumento)))
            {
                switch (enumint)
                {
                    case (int)ModeloDocumento.CupomFiscal:
                        readableName = "Cupom Fiscal";
                        break;
                    case (int)ModeloDocumento.ModeloUm:
                        readableName = "Modelo Um";
                        break;
                    case (int)ModeloDocumento.NFe:
                        readableName = "NFe";
                        break;
                    case (int)ModeloDocumento.Balanco:
                        readableName = "Sem Valor Fiscal";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(CategoriaMovimentacao)))
            {
                switch (enumint)
                {
                    case (int)CategoriaMovimentacao.Produtos:
                        readableName = "Produtos";
                        break;
                    case (int)CategoriaMovimentacao.ProdutosEServicos:
                        readableName = "Produtos e Serviços";
                        break;
                    case (int)CategoriaMovimentacao.Servicos:
                        readableName = "Serviços";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(TipoEntrada)))
            {
                switch (enumint)
                {
                    case (int)TipoEntrada.OutrasEntradas:
                        readableName = "Outras Entradas";
                        break;
                    case (int)TipoEntrada.InsumoEnergia:
                        readableName = "Conta de Energia";
                        break;
                    case (int)TipoEntrada.InsumoAgua:
                        readableName = "Conta de Água - Insumo de Produção/Prestação de Serviço";
                        break;
                    case (int)TipoEntrada.InsumoGas:
                        readableName = "Conta de Gás - Insumo de Produção/Prestação de Serviço";
                        break;
                    case (int)TipoEntrada.Transporte:
                        readableName = "Fatura de Transporte - Nota emitida por Transportadora";
                        break;
                    case (int)TipoEntrada.Telecomunicacoes:
                        readableName = "Fatura de Telecomunicações";
                        break;
                    default:
                        readableName = "";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(IndPres)))
            {
                switch (enumint)
                {
                    case (int)IndPres.OperacaoNaoPresencialInternet:
                        readableName = "Operação não presencial, pela Internet";
                        break;
                    case (int)IndPres.OperacaoNaoPresencialAtendimento:
                        readableName = "Operação não presencial, tele atendimento";
                        break;
                    case (int)IndPres.OperacaoNaoPresencialOutros:
                        readableName = "Operação não presencial, outros";
                        break;
                    default:
                        readableName = "Operação presencial";
                        break;
                }
            }
            else if (enumvalue.GetType().Equals(typeof(IndInter)))
            {
                switch (enumint)
                {
                    case (int)IndInter.OperacaoSemIntermediador:
                        readableName = "Operação sem intermediador";
                        break;
                    case (int)IndInter.OperacaoSitePlataformaTerceiros:
                        readableName = "Operação em site ou plataforma de terceiros";
                        break;
                    default:
                        readableName = "Operação sem intermediador";
                        break;
                }
            }
            else
            {
                throw new ArgumentException("valor de enum inválido");
            }
            return readableName;
        }

        public virtual bool PIS_CREDEB { get; set; }
        public virtual bool COFINS_CREDEB { get; set; }
        public virtual bool IPI_CREDEB { get; set; }
        public virtual bool ICMS_CREDEB { get; set; }

        public virtual TipoValorProduto TipoValorNotaCompra { get; set; }
        public virtual bool Truncar { get; set; }

        public virtual bool ExibirTextoTotalTrib { get; set; }

        public virtual bool DestinoSempreInterno { get; set; }

    }
}
