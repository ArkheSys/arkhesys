using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Documento")]
    public class Documento : ModeloBase
    {
        public Documento(int codigo, DateTime vencimento, DateTime previsao, string documento, string nossoNumero,
         string cliente, decimal valor, decimal saldo, string situacao, DateTime? ultimabaixa,
         string requisicao, string filial, string fantasia, string tipodoc,
         string portador, int parc, int totalparc, DateTime data, int id, int tipodddd,
         DateTime incData, string incUsuario, DateTime altData, string altUsuario, DateTime? dataDesconto, int numeroPedido, DateTime? dtTransferenciaOuBaixaRet, string historicoNome, string complementoHistorico) :
            this(codigo, vencimento, previsao, documento, nossoNumero, cliente, valor, saldo, situacao, ultimabaixa, requisicao, filial, fantasia, tipodoc, portador, parc, totalparc, data, id, tipodddd, incData,
          incUsuario, altData, altUsuario, dataDesconto, numeroPedido, dtTransferenciaOuBaixaRet, historicoNome, complementoHistorico, 0)
        {

        }

        public Documento(int codigo, DateTime vencimento, DateTime previsao, string documento, string nossoNumero,
            string cliente, decimal valor, decimal saldo, string situacao, DateTime? ultimabaixa,
            string requisicao, string filial, string fantasia, string tipodoc,
            string portador, int parc, int totalparc, DateTime data, int id, int tipodddd,
            DateTime incData, string incUsuario, DateTime altData, string altUsuario, DateTime? dataDesconto, int numeroPedido, DateTime? dtTransferenciaOuBaixaRet, string historicoNome, string complementoHistorico, decimal juros)

            //doc.Codigo, doc.DtVencimento, doc.DtPrevisao,doc.NumDocumento, pes.Nome,
            //doc.Valor,doc.Saldo,doc.Situacao, doc.DtUltimaBaixa, doc.NumRequisicao, fil.Nome, pes.Fantasia, 
            //td.Nome, por.Nome, doc.Parcela, doc.QtParcela, doc.Dt,doc.ID,doc.Tipo,
            //doc.IncData, doc.IncUsuario, doc.AltData, doc.AltUsuario
        {
            Codigo = codigo;
            DtVencimento = vencimento;
            DtPrevisao = previsao;
            NumDocumento = documento;
            NossoNumero = nossoNumero;
            Dt = data;
            Valor = valor;
            Saldo = saldo;
            Situacao = situacao;
            DtUltimaBaixa = ultimabaixa;
            NumRequisicao = requisicao;
            Filial = new Filial() { Nome = filial };
            TipoDocumento = new TipoDocumento() { Nome = tipodoc };
            Portador = new Portador() { Nome = portador };
            Pessoa = new Pessoa() { Nome = cliente, Fantasia = fantasia };
            Parcela = parc;
            QtParcela = totalparc;
            ID = id;
            Tipo = (TipoDocumentoType)tipodddd;
            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
            DataDesconto = dataDesconto;
            NumeroPedido = numeroPedido;
            DtTransferenciaOuBaixaRet = dtTransferenciaOuBaixaRet;
            HistoricoNome = historicoNome;
            ComplementoHist = complementoHistorico;
            Juros = juros;
        }

        public Documento(int codigo, DateTime vencimento, DateTime previsao, string documento, string nossoNumero,
            string cliente, decimal valor, decimal saldo, string situacao, DateTime? ultimabaixa,
            string requisicao, string filial, string fantasia, string tipodoc,
            string portador, int parc, int totalparc, DateTime data, int id, int tipodddd,
            DateTime incData, string incUsuario, DateTime altData, string altUsuario, DateTime? dataDesconto, string grupoClienteTel, int numeroPedido, DateTime? dtTransferenciaOuBaixaRet, string historicoNome, string complementohistorico, decimal juros)
            : this(codigo, vencimento, previsao, documento, nossoNumero, cliente, valor, saldo, situacao, ultimabaixa, requisicao, filial, fantasia, tipodoc,
            portador, parc, totalparc, data, id, tipodddd, incData, incUsuario, altData, altUsuario, dataDesconto, numeroPedido, dtTransferenciaOuBaixaRet, historicoNome, complementohistorico, juros)
        {
            GrupoClienteTel = grupoClienteTel;
        }

        public Documento(int id, string numdocumento, int parcela, int qtparcela, DateTime dt, DateTime dtvencimento, decimal valor, decimal saldo, int tipo)
        {
            ID = id;
            NumDocumento = numdocumento;
            Parcela = parcela;
            QtParcela = qtparcela;
            Dt = dt;
            DtVencimento = dtvencimento;
            Valor = valor;
            Saldo = saldo;
            Tipo = (TipoDocumentoType)tipo;
        }

        public Documento()
        {
            Movimentos = new List<Movimento>();
            ReparcelamentoDocs = new List<Documento>();
            Parcela = 1;
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(0, "Código", 75)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private TipoDocumentoType _Tipo;
        [InformacaoExibicao(1, "Tipo", 75)]
        public virtual TipoDocumentoType Tipo { get { return _Tipo; } set { _Tipo = value; } }

        private string _NossoNumero;
        [InformacaoExibicao(11, "Nosso Número", 100)]
        public virtual string NossoNumero { get { return _NossoNumero; } set { _NossoNumero = value; } }

        private DateTime? _DtEmailEnviado;
        public virtual DateTime? DtEmailEnviado { get { return _DtEmailEnviado; } set { _DtEmailEnviado = value; } }

        private Contrato _Contrato;
        public virtual Contrato Contrato { get { return _Contrato; } set { _Contrato = value; } }

        private ArquivoRemessa _ArquivoRemessa;
        public virtual ArquivoRemessa ArquivoRemessa { get { return _ArquivoRemessa; } set { _ArquivoRemessa = value; } }
        
        private Filial _Filial;
        [InformacaoExibicao(12, "Filial", 75)]
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        [InformacaoExibicao(5, "Pessoa", 264)]
        public virtual String PessoaNome { get   { return Pessoa.Nome; } }

        [InformacaoExibicao(13, "Fantasia", 264)]
        public virtual String PessoaFantasia { get { return Pessoa.Fantasia; } }

        private TipoDocumento _TipoDocumento;
        [InformacaoExibicao(14, "Tipo Docto", 75)]
        public virtual TipoDocumento TipoDocumento { get { return _TipoDocumento; } set { _TipoDocumento = value; } }

        private Banco _Banco;
        public virtual Banco Banco { get { return _Banco; } set { _Banco = value; } }

        private Portador _Portador;
        [InformacaoExibicao(16, "Portador", 150)]
        public virtual Portador Portador { get { return _Portador; } set { _Portador = value; } }

        private Acrescimo _Acrescimo;
        public virtual Acrescimo Acrescimo { get { return _Acrescimo; } set { _Acrescimo = value; } }

        private DateTime _Dt;
        [InformacaoExibicao(15, "Emissão", 85, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Dt { get { return _Dt; } set { _Dt = value; } }

        private DateTime _DtDigitacao;
        public virtual DateTime DtDigitacao { get { return _DtDigitacao; } set { _DtDigitacao = value; } }

        private DateTime _DtVencimento;
        [InformacaoExibicao(2, "Vencimento", 85, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime DtVencimento { get { return _DtVencimento; } set { _DtVencimento = value; } }

        private DateTime _DtPrevisao;
        [InformacaoExibicao(3, "Previsão", 85, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime DtPrevisao { get { return _DtPrevisao; } set { _DtPrevisao = value; } }

        private string _NumDocumento;
        [InformacaoExibicao(4, "Documento", 75)]
        public virtual string NumDocumento { get { return _NumDocumento; } set { _NumDocumento = value; } }

        private string _NumRequisicao;
        [InformacaoExibicao(10, "Requisição", 75)]
        public virtual string NumRequisicao { get { return _NumRequisicao; } set { _NumRequisicao = value; } }

        private Condicao _Condicao;
        public virtual Condicao Condicao { get { return _Condicao; } set { _Condicao = value; } }

        private int _Parcela;
        public virtual int Parcela { get { return _Parcela; } set { _Parcela = value; } }

        private int _QtParcela;
        public virtual int QtParcela { get { return _QtParcela; } set { _QtParcela = value; } }

        private decimal _Valor;
        [InformacaoExibicao(6, "Valor", 131, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get { return _Valor; } set { _Valor = value; } }

        private decimal _ValorTotal;
        public virtual decimal ValorTotal { get { return _ValorTotal; } set { _ValorTotal = value; } }

        private Historico _Historico;
        public virtual Historico Historico { get { return _Historico; } set { _Historico = value; } }


        [InformacaoExibicao(24, "Histórico", 100)]
        public virtual string HistoricoNome { get;set; }

        private string _ComplementoHist;

        [InformacaoExibicao(23, "Complemento Histórico", 150)]
        public virtual string ComplementoHist { get { return _ComplementoHist; } set { _ComplementoHist = value; } }

        private int _CodBanco;
        public virtual int CodBanco { get { return _CodBanco; } set { _CodBanco = value; } }

        private string _Banco_Str;
        public virtual string Banco_Str { get { return _Banco_Str; } set { _Banco_Str = value; } }

        private string _Agencia;
        public virtual string Agencia { get { return _Agencia; } set { _Agencia = value; } }

        private string _Conta;
        public virtual string Conta { get { return _Conta; } set { _Conta = value; } }

        private string _Cheque;
        public virtual string Cheque { get { return _Cheque; } set { _Cheque = value; } }

        private string _Emitente;
        public virtual string Emitente { get { return _Emitente; } set { _Emitente = value; } }

        private bool _BEntrada;
        public virtual bool BEntrada { get { return _BEntrada; } set { _BEntrada = value; } }

        private Nota _Nota;
        public virtual Nota Nota { get { return _Nota; } set { _Nota = value; } }

        private decimal _Saldo;
        [InformacaoExibicao(7, "Saldo", 131, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Saldo { get { return _Saldo; } set { _Saldo = value; } }

        private decimal _Juros;
        [InformacaoExibicao(8, "Juros", 131, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Juros { get { return _Juros; } set { _Juros = value; } }

        private string _Situacao;
        [InformacaoExibicao(8, "Situação", 71, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string Situacao { get { return _Situacao; } set { _Situacao = value; } }

        private DateTime? _DtUltimaBaixa;       
        [InformacaoExibicao(9, "Última Baixa", 85, InformacaoExibicao.HAlinhamento.Centro)]       
        public virtual DateTime? DtUltimaBaixa { get { return _DtUltimaBaixa; } set { _DtUltimaBaixa = value; } }

        //OBJETO APENAS EM MEMÓRIA
        public virtual decimal valorImpostoRetido { get; set; }
        public virtual bool Renegociacao { get; set; }

        [InformacaoExibicao(17, "Parc.", 50, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string ParcelasString { get { return Parcela + " / " + QtParcela; } }

        private bool _BImpresso;
        public virtual bool BImpresso { get { return _BImpresso; } set { _BImpresso = value; } }

        public virtual string NumBanco { get; set; }

        public virtual string CpfCnpj { get; set; }

        public virtual string NomeBanco { get; set; }

        public virtual bool BGeradoPDF { get; set; }
        
        public virtual bool BGeradoRemessa { get; set; }

        public virtual bool BEnviadoEmail { get; set; }

        public virtual string MovimentoRemessa { get; set; }

        public virtual string IdPedidoMagento { get; set; }

//        public virtual string nossoNumero { get; set; }
        private string _GrupoClienteTel;
        public virtual string GrupoClienteTel
        {
            get
            {
                if (Pessoa.ClientesTelefonia != null)
                {
                    try
                    {
                        return Pessoa.ClientesTelefonia.FirstOrDefault().GrupoNome;  
                    }
                    catch (Exception)
                    {                       
                         return "";
                    }
                }
                else if (!String.IsNullOrEmpty(_GrupoClienteTel))
                {
                    return _GrupoClienteTel;
                }
                else
                {
                    return "";
                }
                //if (!String.IsNullOrEmpty(_GrupoClienteTel))
                //{
                //    return _GrupoClienteTel;
                //}
                //else if (Pessoa.ClienteTelefonia != null)
                //{
                //    return Pessoa.ClienteTelefonia.GrupoNome;
                //}
                //else
                //{
                //    return "";
                //}
            }
            set
            {
                _GrupoClienteTel = value;
            }
        }

        [InformacaoExibicao(19, "Data Inclusão", 100)]
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
        [InformacaoExibicao(20, "Usuário Inclusão", 100)]
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

        [InformacaoExibicao(21, "Data Alteração", 100)]
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
        [InformacaoExibicao(22, "Usuário Alteração", 100)]
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

        private IList<Movimento> _Movimentos;
        public virtual IList<Movimento> Movimentos
        {
            get { return _Movimentos; }
            set
            {
                _Movimentos = value;
                RecalcularSaldoESituacao();
            }
        }

        public virtual void RecalcularSaldoESituacao()
        {
            try
            {
                Saldo =
                    Movimentos.Where(mov => mov.Operacao == OperacaoMovimentoType.Lancamento).Sum(
                        mov => Math.Round(mov.Valor, 2)) -
                    Movimentos.Where(
                        mov =>
                        (mov.Tipo != TipoMovimentoType.Juro && mov.Tipo != TipoMovimentoType.Multa && mov.Tipo != TipoMovimentoType.IOF && mov.Tipo != TipoMovimentoType.TaxaBoleto && mov.Tipo != TipoMovimentoType.JurosDesconto)
                        &&
                        (mov.Operacao == OperacaoMovimentoType.BxParcial ||
                        mov.Operacao == OperacaoMovimentoType.BxTotal ||
                        mov.Operacao == OperacaoMovimentoType.Cancelado ||
                        mov.Operacao == OperacaoMovimentoType.Retencao) ||
                        mov.Operacao == OperacaoMovimentoType.BxRenegociação
                        ).
                        Sum(mov => Math.Round(mov.Valor, 2)) +
                    Movimentos.Where(mov => mov.Operacao == OperacaoMovimentoType.EstBxPArcial || mov.Operacao == OperacaoMovimentoType.EstBxTotal).Sum(
                        mov => Math.Round(mov.Valor, 2));

                string situcaoAnterior = Situacao;
                if (Saldo == 0 || Saldo <= 0)
                {
                    Situacao = Movimentos.Where(mov => mov.Operacao == OperacaoMovimentoType.Cancelado).Sum(mov => Math.Round(mov.Valor, 2)) == Valor ? "Canc" : "BxT";
                    Situacao = Movimentos.Where(mov => mov.Operacao == OperacaoMovimentoType.BxRenegociação).Sum(mov => Math.Round(mov.Valor, 2)) == Valor ? "BxR" : "BxT";
                    if (situcaoAnterior == "TDesc" && Situacao == "BxT")
                    {
                        Situacao = situcaoAnterior;
                    }
                    Saldo = 0;

                }
                else if (Saldo == Valor || Saldo == Valor - Movimentos.Where(mov => mov.Operacao == OperacaoMovimentoType.Cancelado).Sum(mov => Math.Round(mov.Valor, 2)))
                    Situacao = "Norm";
                else if (Saldo == Valor - Movimentos.Where(mov => mov.Operacao == OperacaoMovimentoType.Retencao).Sum(mov => Math.Round(mov.Valor, 2)))
                    Situacao = situcaoAnterior == "TDesc" ? "TDesc" : "Norm";
                else
                    Situacao = situcaoAnterior == "TDesc" ? "TDesc" : "BxP";
            }
            catch { }
        }

        private IList<Documento> _ReparcelamentoDocs;

        public virtual IList<Documento> ReparcelamentoDocs { get { return _ReparcelamentoDocs; } set { _ReparcelamentoDocs = value; } }

        public virtual void SubistituaTagsComplementoHistorico()
        {
            var valorAnt = ComplementoHist;
            try
            {
                ComplementoHist = ComplementoHist.Replace("*NomePessoa", PessoaNome)
                                                        .Replace("*QtdTotalParc", QtParcela.ToString())
                                                        .Replace("*Parcela", Parcela.ToString())
                                                        .Replace("*MesAnoContabil", String.Format("{0:00}/{1:0000}", Dt.Month, Dt.Year));
                if (TipoDocumento != null)
                    ComplementoHist = ComplementoHist.Replace("*TipoDocto", TipoDocumento.DescReduzida);
            }
            catch
            {
                ComplementoHist = valorAnt;
            }
        }

        
        private DateTime? _DataDesconto;
        [InformacaoExibicao(18, "Data Desconto", 100)]
        public virtual DateTime? DataDesconto
        {
            get
            {
                return _DataDesconto;
            }
            set
            {
                _DataDesconto = value;
            }
        }

        private DateTime? _DtTransferenciaOuBaixaRet;
        [InformacaoExibicao(18, "Data Transferência/Baixa", 100)]
        public virtual DateTime? DtTransferenciaOuBaixaRet
        {
            get
            {
                return _DtTransferenciaOuBaixaRet;
            }
            set
            {
                _DtTransferenciaOuBaixaRet = value;
            }
        }

        public virtual bool Selecionado { get; set; }

        [InformacaoExibicao(19, "Número Pedido", 100)]

        public virtual int NumeroPedido { get; set; }

#pragma warning disable CS0414 // O campo "Documento._BDocReceberBaixado" é atribuído, mas seu valor nunca é usado
        private static bool _BDocReceberBaixado;
#pragma warning restore CS0414 // O campo "Documento._BDocReceberBaixado" é atribuído, mas seu valor nunca é usado
        public virtual bool BDocReceberBaixado 
        { 
            get
            {
                if (Saldo == 0 && Tipo == TipoDocumentoType.Receber)
                {
                    _BDocReceberBaixado = true;
                    return true;
                }
                return false;
            }
        }

        public static Documento CloneNewObject(Documento objDocumento)
        {
            AutoMapper.Mapper.CreateMap<Modelo.Documento, Modelo.Documento>().ForMember(dest => dest.ID, o => o.Ignore());
            return  AutoMapper.Mapper.Map<Documento, Documento>(objDocumento);
        }


        public static void GetClones(ref Nota objNota)
        {
            IList<Documento> lstDocumento = new List<Documento>(objNota.Documentos);
            objNota.Documentos = new List<Documento>();
            foreach (var Doc in lstDocumento)
            {
                Documento objDocumento = Documento.CloneNewObject(Doc);
                objDocumento.Nota = objNota;

                objNota.Documentos.Add(objDocumento);
            }
        }
    }
}
