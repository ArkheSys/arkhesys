using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using cwkControleUsuario;
using Modelo;
using Modelo.Objeto;

namespace BLL
{
    public class Financeiro : IDisposable
    {
        private readonly DataClassesDataContext db;
        private readonly decimal valorjuro = 0;
        public decimal ValorRecibo { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Financeiro(DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public Modelo.Documento GeraFinanceiro(List<Modelo.Objeto.FinanceiroParcela> listaParcelas, List<FinanceiroPlanoContas> listaPContas, Modelo.Objeto.Documento moDocumento)
        {
            Modelo.Documento objDocumento = new Modelo.Documento();
            Modelo.Movimento objMovimento;

            Documento bllDocumento = new Documento(db);
            Movimento bllMovimento = new Movimento(db);

            foreach (Modelo.Objeto.FinanceiroParcela parc in listaParcelas)
            {
                objDocumento = new Modelo.Documento();
                db.Documentos.InsertOnSubmit(objDocumento);

                objDocumento.Codigo = bllDocumento.RetornaMAXCodigo();
                objDocumento.Parcela = parc.Parcela;
                objDocumento.QtParcela = listaParcelas.Count;
                objDocumento.Banco_Str = moDocumento.BancoStr;
                objDocumento.IDBanco = moDocumento.IDBanco;
                objDocumento.IDAcrescimo = moDocumento.IDAcrescimo;
                objDocumento.IDCondicao = moDocumento.IDCondicao;
                objDocumento.IDFilial = moDocumento.IDFilial;
                objDocumento.IDHistorico = moDocumento.IDHistorico;
                objDocumento.IDPessoa = moDocumento.IDPessoa;
                objDocumento.IDPortador = moDocumento.IDPortador;
                objDocumento.IDTipoDocumento = parc.IDTipoDocumento;
                objDocumento.NumDocumento = moDocumento.NumDocumento.Trim();
                objDocumento.NumRequisicao = moDocumento.NumRequisicao.Trim();
                objDocumento.Tipo = moDocumento.Tipo;
                objDocumento.Dt = moDocumento.Dt;
                objDocumento.DtDigitacao = moDocumento.DtDigitacao;
                objDocumento.ValorTotal = moDocumento.ValorTotal;
                objDocumento.Valor = parc.Valor;
                objDocumento.ComplementoHist = moDocumento.ComplementoHist;
                objDocumento.DtVencimento = parc.Vencimento;
                objDocumento.DtPrevisao = parc.Vencimento;
                objDocumento.Nota = moDocumento.Nota;
                objDocumento.Saldo = objDocumento.Valor;
                objDocumento.Situacao = "Norm";
                objDocumento.MovimentoRemessa = moDocumento.MovientoRemessa;
                objDocumento.NumBanco = parc.NumBanco;
                objDocumento.Agencia = parc.NumAgencia;
                objDocumento.NomeBanco = parc.NomeBanco;
                objDocumento.Conta = parc.NumContaCorrente;
                objDocumento.CpfCnpj = parc.CpfCnpj;
                objDocumento.Cheque = parc.NumCheque;
                objDocumento.Emitente = parc.Emitente;
                objDocumento.IncData = DateTime.Now;
                objDocumento.IncUsuario = Facade.getUsuarioLogado.Login;
                objDocumento.AltData = DateTime.Now;
                objDocumento.AltUsuario = Facade.getUsuarioLogado.Login;

                decimal auxValor = 0, percentual = 0, percentualRateio = 0, auxValorRateio = 0;
                int count = 1, countRateio;
                listaPContas.Sort(new FinanceiroPContasSort(FinanceiroPContasSort.SortType.Valor));
                foreach (FinanceiroPlanoContas pcont in listaPContas)
                {
                    objMovimento = new Modelo.Movimento();
                    objMovimento.Codigo = bllMovimento.RetornaMAXCodigo(objDocumento);
                    objMovimento.Tipo = TipoMovimentoType.Documento;
                    objMovimento.IDPlanoConta = pcont.ID;
                    objMovimento.Sequencia = 1;
                    objMovimento.Operacao = OperacaoMovimentoType.Lancamento;
                    objMovimento.IDHistorico = moDocumento.IDHistorico;
                    objMovimento.ComplementoHist = moDocumento.ComplementoHist;
                    objMovimento.Dt = moDocumento.Dt;
                    objMovimento.IncData = DateTime.Now;
                    objMovimento.IncUsuario = Facade.getUsuarioLogado.Login;
                    objMovimento.AltData = DateTime.Now;
                    objMovimento.AltUsuario = Facade.getUsuarioLogado.Login;

                    try
                    {
                        percentual = Math.Round(pcont.Valor / (decimal) moDocumento.ValorTotal, 8);
                    }
                    catch (Exception)
                    {
                        percentual = 0;
                    }

                    if (count == listaPContas.Count)
                    {
                        objMovimento.Valor = objDocumento.Valor - auxValor;
                    }
                    else
                    {
                        objMovimento.Valor = AplicaPercentualValor((decimal) parc.Valor, percentual);
                        auxValor += (decimal) objMovimento.Valor;
                    }

                    //WNO - Adiciona os rateios para projeto
                    if (pcont.RateioProjeto != null)
                    {
                        Rateio_Mov objRateio;
                        countRateio = 1;
                        auxValorRateio = 0;
                        foreach (Rateio_Mov item in pcont.RateioProjeto)
                        {
                            objRateio = new Rateio_Mov();
                            objRateio.IDProjeto = item.IDProjeto;
                            objRateio.Sequencia = item.Sequencia;
                            percentualRateio = Math.Round(item.Valor / pcont.Valor, 8);
                            if (countRateio == pcont.RateioProjeto.Count)
                            {
                                objRateio.Valor = (decimal) objMovimento.Valor - auxValorRateio;
                            }
                            else
                            {
                                objRateio.Valor = AplicaPercentualValor((decimal) objMovimento.Valor, percentualRateio);
                                auxValorRateio += objRateio.Valor;
                            }

                            objMovimento.Rateio_Movs.Add(objRateio);
                            countRateio++;
                        }
                    }

                    objDocumento.Movimentos.Add(objMovimento);
                    count++;
                }

                //Modelo.HistMovimentoRemessaDoc ObjHistMovRemDoc = new Modelo.HistMovimentoRemessaDoc();

                //ObjHistMovRemDoc.Documento = objDocumento;
                //ObjHistMovRemDoc.MovimentoRemessa = objDocumento.MovimentoRemessa;
                //ObjHistMovRemDoc.Dt = DateTime.Now;

                db.SubmitChanges();
                if (parc.bEntrada == 1)
                {
                    if (objDocumento.Saldo != null)
                        ValorRecibo += (decimal) objDocumento.Saldo;
                    BaixaDocumento(objDocumento.ID, (int) objDocumento.IDBanco, (DateTime) objDocumento.DtVencimento, (DateTime) objDocumento.DtVencimento, (decimal) objDocumento.Saldo, 0, 0, OperacaoMovimentoType.BxTotal, null, false, "");
                }
            }

            return objDocumento;
        }

        /// <summary>
        /// </summary>
        /// <param name="vTotal"></param>
        /// <param name="vPConta"></param>
        /// <param name="vParc"></param>
        /// <returns></returns>
        public decimal AplicaPercentualValor(decimal vParc, decimal percentual)
        {
            return Math.Round(percentual * vParc, 2);
        }

        public void BaixaDocumento(int idDocumento, int idBanco, DateTime dataBase, DateTime dataVencimento, decimal valorDoc, decimal valorJuro, decimal valorMulta, OperacaoMovimentoType tipo, Modelo.MovCaixa pobjMovCaixa, bool pRenegociacao,
            string ComplementoHistorico)
        {
            BaixaDocumento(idDocumento, idBanco, dataBase, dataVencimento, valorDoc, valorjuro, valorMulta, tipo, pobjMovCaixa, pRenegociacao, ComplementoHistorico, new List<FormaPgtoMovimento>(), null);
            //List<Modelo.Movimento> movimentosCriados = BaixaDocumento(idDocumento, idBanco, dataBase, dataVencimento, valorDoc, valorjuro, valorMulta, tipo, pobjMovCaixa, pRenegociacao, ComplementoHistorico);

            //db.SubmitChanges();
        }

        public List<Modelo.Movimento> BaixaDocumento(int idDocumento, int idBanco, DateTime dataBase, DateTime dataVencimento, decimal valorDoc, decimal valorJuro, decimal valorMulta, OperacaoMovimentoType tipo, Modelo.MovCaixa pobjMovCaixa,
            bool pRenegociacao, string ComplementoHistorico, List<FormaPgtoMovimento> listaFormaPgtoMovimento, Modelo.Movimento movTroco)
        {
            Configuracao objConfiguracao = (from c in db.Configuracaos
                where c.Codigo == 1
                select c).Single();

            Modelo.Documento objDocumento = (from d in db.Documentos
                where d.ID == idDocumento
                select d).Single();

            decimal? saldo = db.f_SaldoDocumento(idDocumento);

            return AuxBaixaDocumento(idDocumento, idBanco, dataBase, valorDoc, valorJuro, valorMulta, tipo, ref pobjMovCaixa, pRenegociacao, ComplementoHistorico, objConfiguracao, objDocumento, saldo, listaFormaPgtoMovimento, movTroco);
        }

        private List<Modelo.Movimento> AuxBaixaDocumento(int idDocumento, int idBanco, DateTime? pDataBase, decimal valorDoc, decimal valorJuro, decimal valorMulta, OperacaoMovimentoType tipo, ref Modelo.MovCaixa pobjMovCaixa, bool pRenegociacao,
            string ComplementoHistorico, Configuracao objConfiguracao, Modelo.Documento objDocumento, decimal? saldo, List<FormaPgtoMovimento> listaFormaPgtoMovimento, Modelo.Movimento movimentoTroco)
        {
            try
            {
                decimal valorEmCheque = listaFormaPgtoMovimento.Where(u => u.FormaPagamento.Tipo == FormaPagamentoTipo.Cheque).Sum(u => u.Valor);
                decimal valorNAOCheque = listaFormaPgtoMovimento.Where(u => u.FormaPagamento.Tipo != FormaPagamentoTipo.Cheque).Sum(u => u.Valor);
                DateTime dataBase;
                if (pDataBase.HasValue)
                    dataBase = pDataBase.Value;
                else
                    dataBase = objDocumento.DtVencimento.Value;

                decimal percentual = Math.Round(valorDoc / saldo.Value * 100, 6);

                decimal valorBaixa = 0;

                MovCaixa bllMovCaixa = new MovCaixa(db);
                int maxCodigoMovCaixa = bllMovCaixa.RetornaMAXCodigo();

                if (pobjMovCaixa == null && pRenegociacao == false)
                {
                    pobjMovCaixa = new Modelo.MovCaixa();

                    pobjMovCaixa.Codigo = maxCodigoMovCaixa++;
                    pobjMovCaixa.NumDocumento = objDocumento.NumDocumento;
                    pobjMovCaixa.IDBanco = idBanco;
                    pobjMovCaixa.IDFilial = objDocumento.IDFilial;
                    pobjMovCaixa.IDHistorico = objDocumento.IDHistorico;
                    pobjMovCaixa.IncData = DateTime.Now;
                    pobjMovCaixa.IncUsuario = Facade.getUsuarioLogado.Login;
                    pobjMovCaixa.AltData = DateTime.Now;
                    pobjMovCaixa.AltUsuario = Facade.getUsuarioLogado.Login;

                    if (objDocumento.Tipo == TipoDocumentoType.Receber && ComplementoHistorico == null)
                    {
                        pobjMovCaixa.Ent_Sai = 0;
                        pobjMovCaixa.ComplementoHist = "RECEBTO. " + objDocumento.ComplementoHist;
                    }
                    else if (objDocumento.Tipo == TipoDocumentoType.Pagar && ComplementoHistorico == null)
                    {
                        pobjMovCaixa.Ent_Sai = 1;
                        pobjMovCaixa.ComplementoHist = "PAGTO. " + objDocumento.ComplementoHist;
                    }
                    else if (objDocumento.Tipo == TipoDocumentoType.Receber && ComplementoHistorico != null)
                    {
                        pobjMovCaixa.Ent_Sai = 0;
                        pobjMovCaixa.ComplementoHist = ComplementoHistorico;
                    }
                    else if (objDocumento.Tipo == TipoDocumentoType.Pagar && ComplementoHistorico != null)
                    {
                        pobjMovCaixa.Ent_Sai = 1;
                        pobjMovCaixa.ComplementoHist = ComplementoHistorico;
                    }

                    pobjMovCaixa.Dt = dataBase.Date;
                    pobjMovCaixa.DtDigitacao = DateTime.Today;
                    if (valorEmCheque + valorNAOCheque > 0)
                        pobjMovCaixa.Valor = valorNAOCheque;
                    else
                        pobjMovCaixa.Valor = valorDoc;

                    /* if (pobjMovCaixa.Valor > 0)*/
                    db.MovCaixas.InsertOnSubmit(pobjMovCaixa);
                }

                List<MovimentoBx> listaMovimentos = (from m in db.Movimentos
                    where m.IDDocumento == idDocumento
                          && m.Operacao == OperacaoMovimentoType.Lancamento
                          && m.Tipo == TipoMovimentoType.Documento
                    orderby m.Valor
                    group m by new {m.PlanoConta.ID, m.Sequencia}
                    into g
                    select new MovimentoBx
                    {
                        IdPlanoConta = g.Key.ID,
                        Sequencia = g.Key.Sequencia,
                        Valor = g.Sum(p => p.Valor)
                    }).ToList();

                int seq = 0, count = 0;
                Modelo.Movimento objMovimento;
                Movimento bllMovimento = new Movimento(db);
                Documento bllDocumento = new Documento(db);

                foreach (MovimentoBx mov in listaMovimentos)
                {
                    if (pobjMovCaixa == null && pRenegociacao)
                        //objDocumento.Movimentos.Add(new Modelo.Movimento() { 
                        //    Origem = Modelo.OrigemMovimentoType.Financeiro,

                        //});
                        continue;

                    //Modelo.Documento bla = new Modelo.Documento();
                    //bla.movim

                    count++;
                    objMovimento = new Modelo.Movimento();
                    objMovimento.Sequencia = objDocumento.Movimentos.Max(m => m.Sequencia) + 1;
                    objMovimento.Codigo = pobjMovCaixa.Movimentos.Count + 1;
                    seq = objMovimento.Sequencia.Value;
                    objMovimento.Origem = OrigemMovimentoType.Financeiro;
                    objMovimento.IDDocumento = idDocumento;

                    objMovimento.Dt = dataBase.Date;
                    objMovimento.Operacao = tipo;
                    objMovimento.IDPlanoConta = mov.IdPlanoConta;
                    objMovimento.IDCentroCusto = 0;
                    objMovimento.Tipo = TipoMovimentoType.Documento;
                    objMovimento.Historico = objDocumento.Historico;
                    objMovimento.ComplementoHist = pobjMovCaixa.ComplementoHist; //objDocumento.ComplementoHist;
                    objMovimento.IncData = DateTime.Now;
                    objMovimento.IncUsuario = Facade.getUsuarioLogado.Login;
                    objMovimento.AltData = DateTime.Now;
                    objMovimento.AltUsuario = Facade.getUsuarioLogado.Login;

                    if (count == listaMovimentos.Count())
                    {
                        objMovimento.Valor = valorDoc - valorBaixa;
                    }
                    else
                    {
                        objMovimento.Valor = Math.Round(mov.Valor.Value * percentual / 100, 2);
                        valorBaixa += objMovimento.Valor.Value;
                    }

                    if (objMovimento.Valor > 0)
                        pobjMovCaixa.Movimentos.Add(objMovimento);
                }

                if (valorJuro > 0)
                {
                    objMovimento = new Modelo.Movimento();
                    objMovimento.Sequencia = seq;
                    objMovimento.Codigo = pobjMovCaixa.Movimentos.Count + 1;
                    objMovimento.Origem = OrigemMovimentoType.Financeiro;
                    objMovimento.IDDocumento = idDocumento;

                    objMovimento.Dt = dataBase;
                    objMovimento.Operacao = tipo;
                    objMovimento.Tipo = TipoMovimentoType.Juro;

                    if (objMovimento.Documento.Nota.Ent_Sai == InOutType.Entrada)
                        objMovimento.IDPlanoConta = objConfiguracao.IDPlanoContaJurosRecebidos;
                    else
                        objMovimento.IDPlanoConta = objConfiguracao.IDPlanoContaJurosPagos;

                    objMovimento.IDCentroCusto = 0;
                    objMovimento.Historico = objDocumento.Historico;
                    objMovimento.ComplementoHist = pobjMovCaixa.ComplementoHist;
                    objMovimento.Valor = valorJuro;
                    objMovimento.IncData = DateTime.Now;
                    objMovimento.IncUsuario = Facade.getUsuarioLogado.Login;
                    objMovimento.AltData = DateTime.Now;
                    objMovimento.AltUsuario = Facade.getUsuarioLogado.Login;

                    pobjMovCaixa.Movimentos.Add(objMovimento);

                    if (valorNAOCheque + valorEmCheque == 0)
                        pobjMovCaixa.Valor = pobjMovCaixa.Valor + valorJuro;
                }

                if (valorMulta > 0)
                {
                    objMovimento = new Modelo.Movimento();
                    objMovimento.Sequencia = seq;
                    objMovimento.Codigo = pobjMovCaixa.Movimentos.Count + 1;
                    objMovimento.Origem = OrigemMovimentoType.Financeiro;
                    objMovimento.IDDocumento = idDocumento;

                    objMovimento.Dt = dataBase;
                    objMovimento.Operacao = tipo;
                    objMovimento.Tipo = TipoMovimentoType.Multa;

                    if (objMovimento.Documento.Nota.Ent_Sai == InOutType.Entrada)
                        objMovimento.IDPlanoConta = objConfiguracao.IDPlanoContaMultasRecebidas;
                    else
                        objMovimento.IDPlanoConta = objConfiguracao.IDPlanoContaMultasPagas;

                    objMovimento.IDCentroCusto = 0;
                    objMovimento.Historico = objDocumento.Historico;
                    objMovimento.ComplementoHist = pobjMovCaixa.ComplementoHist; //objDocumento.ComplementoHist;
                    objMovimento.Valor = valorMulta;
                    objMovimento.IncData = DateTime.Now;
                    objMovimento.IncUsuario = Facade.getUsuarioLogado.Login;
                    objMovimento.AltData = DateTime.Now;
                    objMovimento.AltUsuario = Facade.getUsuarioLogado.Login;

                    pobjMovCaixa.Movimentos.Add(objMovimento);
                    if (valorNAOCheque + valorEmCheque == 0)
                        pobjMovCaixa.Valor = pobjMovCaixa.Valor + valorMulta;
                }

                List<Modelo.Movimento> movimentosCriados;
                if (pobjMovCaixa != null)
                    movimentosCriados = pobjMovCaixa.Movimentos.ToList();
                else
                    movimentosCriados = new List<Modelo.Movimento>();
                decimal totalMovimento = 0;
                decimal totalFormaMovimento = 0;

                foreach (Modelo.Movimento movimento in movimentosCriados)
                    totalMovimento += movimento.Valor.Value;
                foreach (FormaPgtoMovimento formaMovimento in listaFormaPgtoMovimento)
                    totalFormaMovimento += formaMovimento.Valor;

                Reparcelamento reparcelamento = new Reparcelamento
                {
                    Data = DateTime.Now,
                    Data_Base = DateTime.Now,
                    IDPessoa = objDocumento.IDPessoa,
                    Pessoa = objDocumento.Pessoa,
                    ValorTotal = valorEmCheque,
                };

                if (listaFormaPgtoMovimento.Where(u => u.FormaPagamento.Tipo == FormaPagamentoTipo.Cheque).Count() > 0)
                {
                    new ReparcelamentoDoc
                    {
                        Documento = objDocumento,
                        Reparcelamento = reparcelamento,
                        Tipo = false
                    };
                    db.Reparcelamentos.InsertOnSubmit(reparcelamento);
                }

                //db.Reparcelamentos.InsertOnSubmit(reparcelamento);
                foreach (Modelo.Movimento movimento in movimentosCriados)
                foreach (FormaPgtoMovimento formaMovimento in listaFormaPgtoMovimento)
                {
                    FormaPgtoMovimento fpm = new FormaPgtoMovimento
                    {
                        Movimento = movimento,
                        IDFormaPagamento = formaMovimento.IDFormaPagamento,
                        Valor = movimento.Valor.Value / (totalMovimento > 0 ? totalMovimento : 1) * formaMovimento.Valor
                    };
                    if (formaMovimento.FormaPagamento.Tipo == FormaPagamentoTipo.Cheque)
                    {
                        fpm.NomeBanco = formaMovimento.NomeBanco;
                        fpm.NomeEmitente = formaMovimento.NomeEmitente;
                        fpm.NumBanco = formaMovimento.NumBanco;
                        fpm.CpfCnpjEmitente = formaMovimento.CpfCnpjEmitente;
                        fpm.Conta = formaMovimento.Conta;
                        fpm.Cheque = formaMovimento.Cheque;
                        fpm.Agencia = formaMovimento.Agencia;
                    }
                }

                int maxCodigo = bllDocumento.RetornaMAXCodigo();
                foreach (FormaPgtoMovimento formaMovimento in listaFormaPgtoMovimento)
                    //---------- Trecho de geraçao de documentos referentes aos cheques ----------
                    if (formaMovimento.FormaPagamento.Tipo == FormaPagamentoTipo.Cheque)
                    {
                        FormaPagamento formaPagamento = db.FormaPagamentos.Where(u => u.ID == formaMovimento.FormaPagamento.ID).Single();
                        Modelo.Documento docCheque = new Modelo.Documento
                        {
                            Agencia = formaMovimento.Agencia,
                            Cheque = formaMovimento.Cheque,
                            //Banco = formaMovimento.NomeBanco,
                            Banco_Str = formaMovimento.NomeBanco,
                            CodBanco = Convert.ToInt32(formaMovimento.NumBanco), // NumBanco string ou int?
                            NumBanco = formaMovimento.NumBanco,
                            NomeBanco = formaMovimento.NomeBanco,
                            CpfCnpj = formaMovimento.CpfCnpjEmitente,
                            Emitente = formaMovimento.NomeEmitente,
                            //bEntrada = 1, //Confirmar esse 1
                            Codigo = maxCodigo++, // max codigo?
                            //Filial = null, // ???
                            //Nota = null, //???
                            Dt = DateTime.Now, //confere?
                            DtDigitacao = DateTime.Now,
                            Saldo = formaMovimento.Valor,
                            Tipo = TipoDocumentoType.Receber,
                            Situacao = "Norm",
                            Conta = formaMovimento.Conta,
                            IDFilial = objDocumento.IDFilial,
                            IDPessoa = objDocumento.IDPessoa,
                            IDTipoDocumento = formaPagamento.TipoDocumento.ID,
                            IDBanco = formaPagamento.Banco.ID,
                            IDPortador = formaPagamento.Portador.ID,
                            IDAcrescimo = formaPagamento.Acrescimo.ID,
                            DtVencimento = formaMovimento.DataCheque,
                            DtPrevisao = formaMovimento.DataCheque,
                            NumDocumento = formaMovimento.Cheque,
                            IDCondicao = formaPagamento.Condicao.ID,
                            QtParcela = formaPagamento.Condicao.QtParcela,
                            Valor = formaMovimento.Valor,
                            ValorTotal = formaMovimento.Valor,
                            IDHistorico = formaPagamento.IDHistorico,
                            ComplementoHist = formaPagamento.ComplementoHist,
                            Parcela = 1,
                            IncData = DateTime.Now,
                            IncUsuario = Facade.getUsuarioLogado.Login,
                            AltData = DateTime.Now,
                            AltUsuario = Facade.getUsuarioLogado.Login
                        };

                        Modelo.Movimento movCheque = new Modelo.Movimento
                        {
                            Documento = docCheque,
                            Valor = docCheque.ValorTotal,
                            Codigo = bllMovimento.RetornaMAXCodigo(),
                            Dt = DateTime.Now,
                            Historico = docCheque.Historico,
                            ComplementoHist = docCheque.ComplementoHist,
                            Operacao = OperacaoMovimentoType.Lancamento,
                            Tipo = TipoMovimentoType.Documento,
                            Sequencia = 1,
                            IncData = DateTime.Now,
                            IncUsuario = Facade.getUsuarioLogado.Login,
                            Origem = OrigemMovimentoType.Financeiro, // Confirmar??
                            IDPlanoConta = formaPagamento.IDPlanoContas,
                            AltData = DateTime.Now,
                            AltUsuario = Facade.getUsuarioLogado.Login
                        };

                        ReparcelamentoDoc reparcDoc = new ReparcelamentoDoc
                        {
                            Documento = docCheque,
                            Reparcelamento = reparcelamento,
                            Tipo = true
                        };

                        //Modelo.Movimento movChequeBx = new Modelo.Movimento()
                        //{
                        //    IDDocumento = idDocumento,
                        //    Valor = -docCheque.ValorTotal,
                        //    Codigo = bllMovimento.RetornaMAXCodigo(),
                        //    Dt = DateTime.Now,
                        //    Historico = docCheque.Historico,
                        //    ComplementoHist = docCheque.ComplementoHist,
                        //    Operacao = (docCheque.ValorTotal.HasValue && docCheque.ValorTotal.Value >= db.Documentos.Single(doc => doc.ID == idDocumento).Saldo) ? Modelo.OperacaoMovimentoType.BxTotal : Modelo.OperacaoMovimentoType.BxParcial,

                        //    Tipo = Modelo.TipoMovimentoType.Cheque,
                        //    Sequencia = 1,

                        //    Origem = Modelo.OrigemMovimentoType.Financeiro,// Confirmar??
                        //    IDPlanoConta = formaPagamento.IDPlanoContas,
                        //};
                        //db.Movimentos.InsertOnSubmit(movChequeBx);
                    }
                //------ Fim do Trecho de geraçao de documentos referentes aos cheques -------

                //Tira essa lista do contexto
                //foreach (Modelo.FormaPgtoMovimento item in listaFormaPgtoMovimento)
                //{
                //    item.FormaPagamento = null;
                //    item.Movimento = null;
                //}

                if (totalFormaMovimento > totalMovimento && movimentoTroco != null)
                {
                    Configuracao objCfg = (from c in db.Configuracaos
                        where c.Codigo == 1
                        select c).Single();

                    Modelo.MovCaixa movCaixa = new Modelo.MovCaixa();
                    movCaixa.Ent_Sai = 1;

                    movCaixa.IDHistorico = movimentoTroco.IDHistorico;
                    movCaixa.ComplementoHist = movimentoTroco.ComplementoHist;
                    movCaixa.Valor = movimentoTroco.Valor; //totalFormaMovimento - totalMovimento;
                    movCaixa.Dt = DateTime.Now;
                    movCaixa.DtDigitacao = DateTime.Now;
                    movCaixa.NumDocumento = objDocumento.NumDocumento;
                    movCaixa.IDFilial = objDocumento.IDFilial;
                    movCaixa.Codigo = maxCodigoMovCaixa++;
                    movCaixa.ComplementoHist = movimentoTroco.ComplementoHist;
                    movCaixa.AltData = DateTime.Now;
                    movCaixa.AltUsuario = Facade.getUsuarioLogado.Login;
                    movCaixa.IncData = DateTime.Now;
                    movCaixa.IncUsuario = Facade.getUsuarioLogado.Login;

                    Modelo.Movimento movimento = new Modelo.Movimento();
                    movimento.Valor = movCaixa.Valor;
                    movimento.Tipo = TipoMovimentoType.Troco;
                    movimento.MovCaixa = movCaixa;
                    movimento.IDPlanoConta = objCfg.IDPlanoContaTroco;
                    movimento.Dt = DateTime.Now;
                    movimento.IDHistorico = movimentoTroco.IDHistorico;
                    movimento.ComplementoHist = movimentoTroco.ComplementoHist;
                    movimento.Sequencia = 1;
                    movimento.Codigo = 1;
                    movimento.Operacao = OperacaoMovimentoType.Troco;
                    movimento.IDCentroCusto = 0;
                    movimento.AltData = DateTime.Now;
                    movimento.AltUsuario = Facade.getUsuarioLogado.Login;
                    movimento.IncData = DateTime.Now;
                    movimento.IncUsuario = Facade.getUsuarioLogado.Login;

                    db.Movimentos.InsertOnSubmit(movimento);
                    db.MovCaixas.InsertOnSubmit(movCaixa);
                }

                db.SubmitChanges();

                objDocumento = (from d in db.Documentos
                    where d.ID == idDocumento
                    select d).Single();

                //objDocumento.Saldo = db.f_SaldoDocumento(idDocumento);// -valorEmCheque;
                objDocumento.Saldo -= valorDoc;
                //if (movimentoTroco != null)
                //    objDocumento.Saldo += movimentoTroco.Valor;
                if (objDocumento.Saldo == 0) objDocumento.Situacao = "BxT";
                else if (objDocumento.Saldo != objDocumento.ValorTotal) objDocumento.Situacao = "BxP";
                else
                    objDocumento.Situacao = db.f_SituacaoDocumento(idDocumento);
                objDocumento.DtUltimaBaixa = dataBase;

                //omg
                //db.MovCaixas.InsertOnSubmit(pobjMovCaixa);

                db.SubmitChanges();
                if (pobjMovCaixa != null)
                    return pobjMovCaixa.Movimentos.ToList();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void gerarRenegociacao(int pIdPessoa, decimal pValorTotal, decimal pJuros, DateTime pDataBase, List<ExtratoCliente> pListaDocumentos, List<Modelo.Objeto.FinanceiroParcela> pListaParcelas, Modelo.Objeto.Documento pobjDocumento)
        {
            Modelo.Documento objDocumento;
            Documento bllDocumento = new Documento();

            //Cria o controle do Reparcelamento
            Reparcelamento objReparcelamento = new Reparcelamento();
            db.Reparcelamentos.InsertOnSubmit(objReparcelamento);

            objReparcelamento.Data = DateTime.Today;
            objReparcelamento.Data_Base = pDataBase;
            objReparcelamento.Juros = pJuros;
            objReparcelamento.IDPessoa = pIdPessoa;
            objReparcelamento.ValorTotal = pValorTotal;
            objReparcelamento.Observacao = pobjDocumento.ComplementoHist;

            //Vincula os documentos antigos ao reparcelamento
            foreach (ExtratoCliente exc in pListaDocumentos)
            {
                ReparcelamentoDoc objReparcDoc = new ReparcelamentoDoc();
                objReparcDoc.IdDocumento = exc.IdDocumento;
                objReparcDoc.Tipo = false;

                objReparcelamento.ReparcelamentoDocs.Add(objReparcDoc);

                //Carrega o Documento
                objDocumento = (Modelo.Documento) bllDocumento.getObjeto(exc.IdDocumento);

                //Baixa o documento
                BaixaDocumento(objDocumento.ID, (int) objDocumento.IDBanco, (DateTime) objReparcelamento.Data, (DateTime) objDocumento.DtPrevisao, (decimal) objDocumento.Saldo, 0, 0, OperacaoMovimentoType.BxTotal, null, true, null);
            }

            //Cria a lista de Plano de Contas para os novos documentos
            Configuracao objConfiguracao = (from c in db.Configuracaos
                where c.Codigo == 1
                select c).Single();
            List<FinanceiroPlanoContas> listaPlanoContas = new List<FinanceiroPlanoContas>();
            FinanceiroPlanoContas objFinPlanoConta = new FinanceiroPlanoContas();
            objFinPlanoConta.ID = (int) objConfiguracao.IdPlanoContaRenegociacao;
            objFinPlanoConta.Valor = (decimal) objReparcelamento.ValorTotal;

            listaPlanoContas.Add(objFinPlanoConta);

            //Gerar os Novos Documentos
            GeraFinanceiro(pListaParcelas, listaPlanoContas, pobjDocumento);

            //Vincula os novos documentos com a reparcelamento
            foreach (Modelo.Objeto.FinanceiroParcela p in pListaParcelas)
            {
                var iddoc = (from d in db.Documentos
                    where d.IDPessoa == pobjDocumento.IDPessoa
                          && d.DtVencimento == p.Vencimento
                          && Math.Round(d.Valor.Value, 2) == Math.Round(p.Valor.Value, 2)
                    select d).Single().ID;

                ReparcelamentoDoc objReparcDoc = new ReparcelamentoDoc();
                objReparcDoc.IdDocumento = iddoc;
                objReparcDoc.Tipo = true;

                objReparcelamento.ReparcelamentoDocs.Add(objReparcDoc);
            }

            db.SubmitChanges();
        }

        public void excluirRenegociacao(int pReparcelamento)
        {
            List<int> listaIDDocumento = new List<int>();
            int? max = 0;

            using (TransactionScope trans = new TransactionScope())
            {
                Reparcelamento objReparcelamento = (from r in db.Reparcelamentos
                    where r.ID == pReparcelamento
                    select r).Single();

                var documentos = objReparcelamento.ReparcelamentoDocs.ToList();

                db.Reparcelamentos.DeleteOnSubmit(objReparcelamento);
                db.SubmitChanges();
                foreach (ReparcelamentoDoc rdoc in documentos)
                    //Excluir as baixas dos documentos antigos
                    if (rdoc.Tipo == false)
                    {
                        max = rdoc.Documento.Movimentos.Max(m => m.Sequencia);
                        foreach (Modelo.Movimento mov in rdoc.Documento.Movimentos.Where(w => w.Sequencia == max))
                        {
                            if (mov.Sequencia == 1)
                                continue;

                            db.Movimentos.DeleteOnSubmit(mov);
                        }

                        listaIDDocumento.Add(rdoc.Documento.ID);
                    }
                    else
                    {
                        db.Documentos.DeleteOnSubmit(rdoc.Documento);
                    }

                db.SubmitChanges();
                //Atualiza Saldo e Situação do Documento
                foreach (int iddoc in listaIDDocumento)
                {
                    Modelo.Documento objDocumento;
                    objDocumento = (from d in db.Documentos
                        where d.ID == iddoc
                        select d).Single();

                    objDocumento.Saldo = db.f_SaldoDocumento(iddoc);
                    objDocumento.Situacao = db.f_SituacaoDocumento(iddoc);
                }

                db.SubmitChanges();
                trans.Complete();
            }
        }

        public bool excluirmovimento(Modelo.Documento pDocumento, int pSequencia)
        {
            bool ret = true;

            //Pesquisa para verificar se a sequencia é a última sequencia nos movimentos
            int? ultimaseq = pDocumento.Movimentos.Max(m => m.Sequencia);
            if (pSequencia < ultimaseq)
            {
                MessageBox.Show("Operação não permitida.\nSomente é possível excluir a última sequencia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (pSequencia == 1)
            {
                MessageBox.Show("Operação não permitida.\nNão é possível excluir a primeira sequencia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            var excluir = (from m in pDocumento.Movimentos
                where m.Sequencia == pSequencia
                select m).ToList();

            int? idmovacaixa = excluir.First().IDMovCaixa;
            Modelo.MovCaixa objMovCaixa = null;

            if (idmovacaixa != null && idmovacaixa > 0 && db.f_QtdDocMovCaixa(idmovacaixa) > 1)
            {
                if (MessageBox.Show("Este documento foi baixado juntamente com outros documentos, deseja atualizar o saldo no caixa?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    objMovCaixa = (from mc in db.MovCaixas
                        where mc.ID == idmovacaixa
                        select mc).Single();
                else
                    return false;
            }
            else if (db.f_QtdDocMovCaixa(idmovacaixa) == 1)
            {
                objMovCaixa = (from mc in db.MovCaixas
                    where mc.ID == idmovacaixa
                    select mc).Single();

                db.MovCaixas.DeleteOnSubmit(objMovCaixa);
            }

            foreach (Modelo.Movimento mov in excluir)
            {
                if (mov.Tipo == TipoMovimentoType.Documento)
                    pDocumento.Saldo = pDocumento.Saldo + mov.Valor;

                //Atualiza o valor do movimento do caixa
                if (objMovCaixa != null && mov.Tipo != TipoMovimentoType.Desconto)
                    objMovCaixa.Valor -= mov.Valor;

                pDocumento.Movimentos.Remove(mov);

                db.Movimentos.DeleteOnSubmit(mov);
            }

            if (pDocumento.Valor == pDocumento.Saldo)
                pDocumento.Situacao = "Norm";
            else if (pDocumento.Valor > pDocumento.Saldo && pDocumento.Saldo > 0)
                pDocumento.Situacao = "BxP";

            pDocumento.DtUltimaBaixa = null;

            return ret;
        }

        public void baixarDocumentosLote(int pIdPessoa, decimal pValorTotal, decimal pJuros, DateTime pDataBase, List<ExtratoCliente> pListaDocumentos, DateTime pDataBaixa, int pIDFilial, int pIDBanco, int pIDHist, string pComplementoHist)
        {
            Modelo.Documento objDocumento;
            Documento bllDocumento = new Documento();

            //Cria o Lançamento no Caixa
            MovCaixa bllMovCaixa = new MovCaixa();

            Modelo.MovCaixa objMovCaixa = new Modelo.MovCaixa();
            db.MovCaixas.InsertOnSubmit(objMovCaixa);

            objMovCaixa.Codigo = bllMovCaixa.RetornaMAXCodigo();
            objMovCaixa.IDBanco = pIDBanco;
            objMovCaixa.IDFilial = pIDFilial;
            objMovCaixa.IDHistorico = pIDHist;
            objMovCaixa.ComplementoHist = pComplementoHist;

            if (pValorTotal > 0)
                objMovCaixa.Ent_Sai = 0;
            else
                objMovCaixa.Ent_Sai = 1;

            objMovCaixa.Dt = pDataBase.Date;
            objMovCaixa.DtDigitacao = DateTime.Today;
            objMovCaixa.Valor = pValorTotal < 0 ? pValorTotal * -1 : pValorTotal;

            //Baixa os documentos
            foreach (ExtratoCliente exc in pListaDocumentos)
            {
                //Carrega o Documento
                objDocumento = (Modelo.Documento) bllDocumento.getObjeto(exc.IdDocumento);

                //Baixa o documento
                BaixaDocumento(objDocumento.ID, (int) objDocumento.IDBanco, pDataBaixa, (DateTime) objDocumento.DtPrevisao, (decimal) objDocumento.Saldo, exc.Juro, exc.Multa, OperacaoMovimentoType.BxTotal, objMovCaixa, false, null);
            }

            db.SubmitChanges();
        }

        public void cancelarDocumento(Modelo.Documento documentoACancelar, DateTime dataCancelamento)
        {
            List<MovimentoBx> listaMovimentos;
            Modelo.Movimento objMovimento;

            decimal perc = 0;

            listaMovimentos = (from m in db.Movimentos
                where m.IDDocumento == documentoACancelar.ID
                      && m.Operacao == OperacaoMovimentoType.Lancamento
                      && m.Tipo == TipoMovimentoType.Documento
                orderby m.Valor
                group m by new {m.PlanoConta.ID, m.Sequencia}
                into g
                select new MovimentoBx
                {
                    IdPlanoConta = g.Key.ID,
                    Sequencia = g.Key.Sequencia,
                    Valor = g.Sum(p => p.Valor)
                }).ToList();

            for (int i = 0; i < listaMovimentos.Count; i++)
            {
                objMovimento = new Modelo.Movimento();

                db.Movimentos.InsertOnSubmit(objMovimento);

                //                objMovimento.Codigo = 0;
                objMovimento.Sequencia = listaMovimentos[i].Sequencia + 1;
                objMovimento.Origem = OrigemMovimentoType.Financeiro;
                objMovimento.IDDocumento = documentoACancelar.ID;

                objMovimento.IDMovCaixa = null;

                objMovimento.Dt = dataCancelamento.Date;
                objMovimento.Operacao = OperacaoMovimentoType.Cancelado; //=6
                objMovimento.Tipo = TipoMovimentoType.Documento;

                objMovimento.IDPlanoConta = listaMovimentos[i].IdPlanoConta;

                objMovimento.IDCentroCusto = 0;
                objMovimento.Historico = documentoACancelar.Historico;
                objMovimento.ComplementoHist = documentoACancelar.ComplementoHist;

                perc = Math.Round((decimal) (documentoACancelar.Saldo / documentoACancelar.Valor * 100), 4);

                objMovimento.Valor = Math.Round(listaMovimentos[i].Valor.Value * perc / 100, 2);

                documentoACancelar.Saldo = 0;
                documentoACancelar.Situacao = "Canc";
            }

            db.SubmitChanges();
        }

        public void DescancelarDocumento(Modelo.Documento documentoACancelar)
        {
            List<Modelo.Movimento> listaMovimentos;
            Modelo.Movimento objMovimento;

            listaMovimentos = (from m in db.Movimentos
                where m.IDDocumento == documentoACancelar.ID
                      && m.Operacao == OperacaoMovimentoType.Cancelado
                      && m.Tipo == TipoMovimentoType.Documento
                select m).ToList();

            for (int i = 0; i < listaMovimentos.Count; i++)
            {
                documentoACancelar.Saldo = documentoACancelar.Valor;
                documentoACancelar.Situacao = "Norm";
                objMovimento = listaMovimentos[i];
                db.Movimentos.DeleteOnSubmit(objMovimento);
            }

            db.SubmitChanges();
        }

        public void cancelarDocumentoLote(List<Modelo.Documento> listaACancelar, DateTime dataCancelamento)
        {
            foreach (Modelo.Documento itemDocumento in listaACancelar) cancelarDocumento(itemDocumento, dataCancelamento);
        }

        public void DescancelarDocumentoLote(List<Modelo.Documento> listaACancelar)
        {
            foreach (Modelo.Documento itemDocumento in listaACancelar) DescancelarDocumento(itemDocumento);
        }
    }
}