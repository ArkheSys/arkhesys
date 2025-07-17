using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.ACBr.Boleto;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Modelo.Proxy;
using MO = Modelo;
using Remessa = cwkGestao.Modelo.Remessa;

namespace cwkGestao.Negocio.Financeiro
{
    public class BaixaDocumentoRetorno
    {
        private readonly List<DadosRetorno> dadosRetorno;
        private readonly Remessa remessa;
        private ParametrosBaixa parametrosBaixa;
        private Documento documento;
        public LogRetorno logRetorno;
        private StreamWriter log;
        private string nomearquivo;
        private string texto = "";
        private int codDocumento;
        private decimal totalJuros, totalValor, totalValorTaxa, totalMulta, totalDocumentos;

        public BaixaDocumentoRetorno(List<DadosRetorno> _dadosRetorno, Remessa remessa)
        {
            dadosRetorno = _dadosRetorno;
            this.remessa = remessa;
        }

        public void BaixarDocumentos(bool? UtilizaDataCredito)
        {
            AbrirArquivoLog();

            logRetorno = new LogRetorno {remessa = remessa, NomeArquivo = nomearquivo};

            LogRetornoController.Instancia.Salvar(logRetorno, Acao.Incluir);
            DateTime dtDesconto = DateTime.Now.Date;

            foreach (var doc in dadosRetorno)
            {
                if (remessa.LayoutRetorno == "SIGCB240")
                {
                    codDocumento = Convert.ToInt32(doc.NossoNumero.Substring(7, 10));
                }
                else
                {
                    if (doc.NumeroDocumento == null)
                        codDocumento = Convert.ToInt32(doc.NossoNumero.Substring(1, 9));
                    else
                        codDocumento = Convert.ToInt32(doc.NumeroDocumento);
                }

                documento = DocumentoController.Instancia.LoadObjectByCodigo(codDocumento);
                var ocorrencia = "";
                if (documento == null)
                {
                    texto = string.Format("{0:0000000000} Não Encontrado", codDocumento);
                    ocorrencia = "Não Encontrado";
                    GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                    GravaArquivoLog(texto);
                    continue;
                }

                totalJuros += Convert.ToDecimal(doc.ValorJurosPago);
                totalValor += Convert.ToDecimal(doc.ValorCredito);
                totalValorTaxa += Convert.ToDecimal(doc.ValorTaxaCobranca);
                totalMulta += Convert.ToDecimal(doc.ValorMultaPaga);
                totalDocumentos += Convert.ToDecimal(doc.ValorPago);

                documento.MovimentoRemessa = doc.CodigoOcorrencia;

                try
                {
                    // Rotina para gerar baixa e movimento de desconto de duplicatas
                    if (doc.ValorCredito != 0 && (doc.Ocorrencia == OcorrenciaType.EntradaConfirmada || doc.Ocorrencia == OcorrenciaType.TransferênciaCarteira && doc.Carteira == "4"))
                    {
                        if (doc.Ocorrencia == OcorrenciaType.TransferênciaCarteira)
                        {
                            texto = string.Format("{0:0000000000} Transferência de Documento para Carteira Descontada" + "          		        "
                                                                                                                       + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " +
                                                                                                                       doc.ValorTaxaCobranca
                                                                                                                       + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                            ocorrencia = "Transferência de Documento para Carteira Descontada";
                        }
                        else
                        {
                            texto = string.Format("{0:0000000000} Entrada Confirmada de Título Descontado" + "          		        "
                                                                                                           + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                                           + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                            ocorrencia = "Entrada Confirmada de Título Descontado";
                        }

                        GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                        GravaArquivoLog(texto);
                        if (documento.Situacao != "TDesc")
                        {
                            DateTime dataBaixa = Convert.ToDateTime(doc.DataOcorrencia);
                            decimal vlDescontoDuplicata;
                            decimal vlIOF;
                            decimal VlTaxaCobranca;
                            decimal vlBaixa;
                            verificaValoresBaixaDescontoDuplicata(doc, out vlDescontoDuplicata, out vlIOF, out VlTaxaCobranca, out vlBaixa);
                            documento.Situacao = "TDesc";
                            parametrosBaixa = new ParametrosBaixa
                            {
                                Banco = documento.Banco,
                                ComplementoHist = "Baixa Título Descontado",
                                Documento = documento,
                                ValorBaixa = vlBaixa,
                                ValorJuros = 0m,
                                ValorMulta = 0m,
                                ValorDesconto = 0m,
                                DataBaixa = dataBaixa,
                                ValorIOF = vlIOF,
                                ValorTaxaCobranca = VlTaxaCobranca,
                                ValorDescontoDuplicata = vlDescontoDuplicata,
                                remessa = remessa,
                            };
                            var listaFormaPagamentoDocumento = new List<pxyFormaPagamentoBaixaDoc>();
                            // parametros para gerar movCaixa do documento
                            listaFormaPagamentoDocumento.Add(new pxyFormaPagamentoBaixaDoc
                            {
                                FormaPagamento = documento.TipoDocumento.FormaPagamento,
                                Data = parametrosBaixa.DataBaixa,
                                Valor = parametrosBaixa.ValorTotal
                            });

                            var geradorMovCaixaDescontado = new GeradorMovCaixa(listaFormaPagamentoDocumento, documento, parametrosBaixa.DataBaixa, "Baixa de Documento Descontado");
                            parametrosBaixa.MovCaixas = geradorMovCaixaDescontado.GetMovCaixa();
                            parametrosBaixa.MovCaixas[0].Banco = parametrosBaixa.Banco;

                            DateTime? dtcredito = null;
                            if (doc.DataCredito != "00/00/0000") dtcredito = Convert.ToDateTime(doc.DataCredito);
                            documento.DataDesconto = dtcredito == null ? dataBaixa : dtcredito;
                            if (doc.Ocorrencia == OcorrenciaType.TransferênciaCarteira) documento.DtTransferenciaOuBaixaRet = documento.DataDesconto;
                            var baixaDocumentoDescontado = new BaixaDocumento(parametrosBaixa);
                            baixaDocumentoDescontado.HistoricoTroco = parametrosBaixa.HistoricoTroco;
                            baixaDocumentoDescontado.ComplementoHistTroco = parametrosBaixa.ComplementoHistTroco;
                            baixaDocumentoDescontado.BaixarDocumentos(parametrosBaixa.MovCaixas);
                            baixaDocumentoDescontado.CommitarTransacao();
                        }

                        continue;
                    }

                    // Retorno de documento que estava descontado e passou para carteira simples (Ex: por falta de pagamento do cliente)
                    if (documento.Saldo == 0 && doc.Ocorrencia == OcorrenciaType.TransferênciaCarteira && doc.Carteira == "7")
                    {
                        string erros = "";
                        if (remessa.PlanoContaEstornoTDesc == null)
                            erros = "Não foi informado o Plano de Contas";

                        if (remessa.HistoricoEstornoTDesc == null)
                        {
                            if (erros != "")
                                erros = erros + " e Histórico";
                            else
                                erros = erros + "Não foi informado o Histórico";
                        }

                        if (erros != "")
                        {
                            erros = erros + " para estorno de titulo Descontado, Verifique o parâmetro no Cadastro de Remessa na aba Dados Retorno!";
                            throw new Exception(erros);
                        }

                        texto = string.Format("{0:0000000000} Transferência/Baixa de Documento da Carteira Descontada para Carteira Simples" + "          		        "
                                                                                                                                             + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") +
                                                                                                                                             "                   " + doc.ValorTaxaCobranca
                                                                                                                                             + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago,
                            codDocumento);
                        ocorrencia = "Transferência/Baixa de Documento da Carteira Descontada para Carteira Simples";
                        GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                        GravaArquivoLog(texto);

                        DateTime dtTransferenciaOuBaixaRet = Convert.ToDateTime(doc.DataOcorrencia);
                        documento.Situacao = "Norm";
                        documento.Saldo = documento.Valor;
                        DateTime? dtOcorrencia = null;
                        if (doc.DataOcorrencia != "00/00/0000") dtOcorrencia = Convert.ToDateTime(doc.DataOcorrencia);
                        documento.DtTransferenciaOuBaixaRet = dtOcorrencia;

                        /* Retirada a geração do movCaixa pois o cliente WRA não quer que gere a movimentação 
                         * porque é gerada com o valor do documento e esse não é o valor real cobrado pelo cliente
                         *MovCaixa movCaixaEst = new MovCaixa()
                         *{
                         *    Banco = documento.Banco,
                         *    Ent_Sai = Ent_SaiMovCaixa.Saída,
                         *    Valor = documento.Saldo,
                         *    Dt = dtOcorrencia.GetValueOrDefault(DateTime.Now),
                         *    DtDigitacao = DateTime.Now,
                         *    ComplementoHist = remessa.HistoricoEstornoTDesc.Nome,
                         *    Historico = remessa.HistoricoEstornoTDesc,
                         *    Filial = documento.Filial,
                         *    FormaPagamento = documento.TipoDocumento.FormaPagamento,
                         *    NumBanco = documento.NumBanco,
                         *    NumCheque = documento.Cheque,
                         *    NumContaCorrente = documento.Conta,
                         *    NumDocumento = documento.NumDocumento,
                         *    Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento()
                        * };
                        * MovCaixaController.Instancia.Salvar(movCaixaEst,Acao.Incluir);*/

                        Movimento movimentoEstorno = new Movimento
                        {
                            Operacao = OperacaoMovimentoType.EstBxTotal,
                            PlanoConta = remessa.PlanoContaEstornoTDesc,
                            Historico = remessa.HistoricoEstornoTDesc,
                            ComplementoHist = remessa.HistoricoEstornoTDesc.Nome,
                            Valor = documento.Saldo,
                            Tipo = TipoMovimentoType.Documento,
                            Dt = dtOcorrencia.GetValueOrDefault(DateTime.Now),
                            Documento = documento,
                            Sequencia = documento.Movimentos.Max(m => m.Sequencia) + 1,
                            Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento(),
                            Origem = (int) OrigemMovimentoType.Financeiro
                        };
                        //movimentoEstorno.MovCaixa = movCaixaEst;
                        MovimentoController.Instancia.Salvar(movimentoEstorno, Acao.Incluir);
                        documento.Movimentos.Add(movimentoEstorno);
                        DocumentoController.Instancia.Salvar(documento, Acao.Alterar);
                        DocumentoController.Instancia.CommitUnitOfWorkTransaction();
                        continue;
                    }

                    if (doc.Ocorrencia != OcorrenciaType.Liquidação && doc.Ocorrencia != OcorrenciaType.LiquidaçãoBaixaTituloNaoRegistrado && doc.ValorCredito == 0)
                    {
                        switch (doc.Ocorrencia)
                        {
                            case OcorrenciaType.EntradaConfirmada:
                                texto = string.Format("{0:0000000000} Entrada Confirmada" + "          		        "
                                                                                          + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                          + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                                ocorrencia = "Entrada Confirmada";
                                break;
                            case OcorrenciaType.EntradaRejeitada:
                                texto = string.Format("{0:0000000000} Entrada Rejeitada" + "          		        "
                                                                                         + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                         + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                                ocorrencia = "Entrada Rejeitada";
                                break;
                            case OcorrenciaType.Baixa:
                                texto = string.Format("{0:0000000000} Documento Baixado" + "          		        "
                                                                                         + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                         + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                                ocorrencia = "Documento Baixado";
                                break;
                            case OcorrenciaType.Outras:
                                texto = string.Format("{0:0000000000} Outra Ocorrência" + "                     "
                                                                                        + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                        + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                                ocorrencia = "Outra Ocorrência";
                                break;
                            default:
                                texto = string.Format("{0:0000000000} Outra Ocorrência [{1}]" + "          		        "
                                                                                              + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                              + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento, doc.CodigoOcorrencia);
                                ocorrencia = "Outra Ocorrência";
                                break;
                        }

                        GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                        GravaArquivoLog(texto);

                        if (documento.Situacao == "TDesc")
                        {
                            documento.Situacao = "Norm";
                            DateTime? dtCredito = null;
                            if (doc.DataCredito != "00/00/0000")
                                dtCredito = Convert.ToDateTime(doc.DataCredito);
                            else
                                dtCredito = Convert.ToDateTime(doc.DataOcorrencia);
                            BaixaDocumento.AtualizarSituacao(documento, dtCredito, true);
                        }

                        DocumentoController.Instancia.Salvar(documento, Acao.Alterar);
                        continue;
                    }

                    if (documento.Saldo == 0)
                    {
                        texto = string.Format("{0:0000000000} Já está baixado" + "          		        "
                                                                               + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                               + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                        ocorrencia = "Já está baixado";
                        if (documento.Situacao == "TDesc")
                        {
                            documento.Situacao = "Norm";
                            DateTime? dtCredito = null;
                            if (doc.DataCredito != "00/00/0000")
                                dtCredito = Convert.ToDateTime(doc.DataCredito);
                            else
                                dtCredito = Convert.ToDateTime(doc.DataOcorrencia);
                            BaixaDocumento.AtualizarSituacao(documento, dtCredito, true);
                        }

                        GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                        GravaArquivoLog(texto);
                        DocumentoController.Instancia.Salvar(documento, Acao.Alterar);
                        continue;
                    }

                    if (documento.Saldo < Convert.ToDecimal(doc.ValorPago - doc.ValorJurosPago - doc.ValorMultaPaga))
                    {
                        texto = string.Format("{0:0000000000} Saldo é menor que o valor pago" + "          	"
                                                                                              + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                                                              + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento);
                        ocorrencia = "Saldo é menor que o valor pago";
                        GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                        GravaArquivoLog(texto);
                        continue;
                    }

                    parametrosBaixa = new ParametrosBaixa();
                    parametrosBaixa.Banco = documento.Banco;

                    parametrosBaixa.ComplementoHist = "Baixa Arquivo Retorno";
                    if (UtilizaDataCredito == true && !string.IsNullOrEmpty(doc.DataCredito))
                    {
                        parametrosBaixa.DataBaixa = Convert.ToDateTime(doc.DataCredito);
                        parametrosBaixa.ComplementoHist = "Baixa Arquivo Retorno " + "- Data de Ocorrência: " + doc.DataOcorrencia;
                    }
                    else
                    {
                        parametrosBaixa.DataBaixa = Convert.ToDateTime(doc.DataOcorrencia);
                        parametrosBaixa.ComplementoHist = "Baixa Arquivo Retorno";
                    }

                    if (doc.ValorTaxaCobranca > 0 && remessa.GeraMovTaxaBoleto)
                    {
                        string erros = "";
                        if (remessa.PlanoContaTaxaBoleto == null)
                            erros = "Não foi informado o Plano de Contas";

                        if (remessa.HistoricoTaxaBoleto == null)
                        {
                            if (erros != "")
                                erros = erros + " e Histórico";
                            else
                                erros = erros + "Não foi informado o Histórico";
                        }

                        if (erros != "")
                        {
                            erros = erros + " para Taxa Boleto, Verifique o parâmetro no Cadastro de Remessa na aba Dados Retorno!";
                            throw new Exception(erros);
                        }

                        documento.Movimentos.Add(new Movimento
                        {
                            Operacao = OperacaoMovimentoType.Lancamento,
                            PlanoConta = remessa.PlanoContaTaxaBoleto,
                            Historico = remessa.HistoricoTaxaBoleto,
                            ComplementoHist = remessa.HistoricoTaxaBoleto.Nome,
                            Valor = Convert.ToDecimal(doc.ValorTaxaCobranca),
                            Tipo = TipoMovimentoType.TaxaBoleto,
                            Dt = Convert.ToDateTime(doc.DataOcorrencia),
                            Documento = documento,
                            Sequencia = documento.Movimentos.Max(m => m.Sequencia) + 1,
                            Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento(),
                            MovCaixa = new MovCaixa
                            {
                                Banco = documento.Banco,
                                Ent_Sai = Ent_SaiMovCaixa.Saída,
                                Valor = Convert.ToDecimal(doc.ValorTaxaCobranca),
                                Dt = Convert.ToDateTime(doc.DataOcorrencia),
                                DtDigitacao = DateTime.Now,
                                ComplementoHist = remessa.HistoricoTaxaBoleto.Nome,
                                Historico = remessa.HistoricoTaxaBoleto,
                                Filial = documento.Filial,
                                FormaPagamento = documento.TipoDocumento.FormaPagamento,
                                NumBanco = documento.NumBanco,
                                NumCheque = documento.Cheque,
                                NumContaCorrente = documento.Conta,
                                NumDocumento = documento.NumDocumento,
                                Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento()
                            }
                        });
                    }

                    parametrosBaixa.Documento = documento;
                    parametrosBaixa.ValorBaixa = Convert.ToDecimal(doc.ValorPago - doc.ValorJurosPago - doc.ValorMultaPaga);
                    parametrosBaixa.ValorJuros = Convert.ToDecimal(doc.ValorJurosPago);
                    parametrosBaixa.ValorMulta = Convert.ToDecimal(doc.ValorMultaPaga);
                    parametrosBaixa.ValorDesconto = Convert.ToDecimal(doc.ValorDesconto);

                    var listaFormaPagamento = new List<pxyFormaPagamentoBaixaDoc>();
                    listaFormaPagamento.Add(new pxyFormaPagamentoBaixaDoc
                    {
                        FormaPagamento = documento.TipoDocumento.FormaPagamento,
                        Data = parametrosBaixa.DataBaixa,
                        Valor = parametrosBaixa.ValorTotal,
                    });

                    var geradorMovCaixa = new GeradorMovCaixa(listaFormaPagamento, documento, parametrosBaixa.DataBaixa, "");
                    parametrosBaixa.MovCaixas = geradorMovCaixa.GetMovCaixa();
                    parametrosBaixa.MovCaixas[0].Banco = parametrosBaixa.Banco;

                    var baixaDocumento = new BaixaDocumento(parametrosBaixa);
                    baixaDocumento.HistoricoTroco = parametrosBaixa.HistoricoTroco;
                    baixaDocumento.ComplementoHistTroco = parametrosBaixa.ComplementoHistTroco;
                    baixaDocumento.BaixarDocumentos(parametrosBaixa.MovCaixas);
                    baixaDocumento.CommitarTransacao();
                }
                catch (Exception ex)
                {
                    texto = string.Format("{0:0000000000} {1}", codDocumento, ex.Message);
                    ocorrencia = texto;
                    GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                    GravaArquivoLog(texto);
                    continue;
                }

                if (doc.ValorJurosPago + doc.ValorMultaPaga > 0)
                    texto = string.Format("{0:0000000000} {1}" + "          	"
                                                               + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                               + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento, "Baixado com Sucesso (Juros+Multa)");
                else
                    texto = string.Format("{0:0000000000} {1}" + "          		"
                                                               + doc.ValorJurosPago.ToString("F2") + "                     " + doc.ValorCredito.ToString("F2") + "                   " + doc.ValorTaxaCobranca
                                                               + "                          " + doc.ValorMultaPaga.ToString("F2") + "                    " + doc.ValorPago, codDocumento, "Baixado com Sucesso");
                ocorrencia = "Baixado com Sucesso";

                GravaLogDetalhe(logRetorno, documento, ocorrencia, doc);
                GravaArquivoLog(texto);
            }

            FecharArquivoLog();
        }

        private void verificaValoresBaixaDescontoDuplicata(DadosRetorno doc, out decimal valorDescontoDuplicata, out decimal valorIOF, out decimal vlTaxaCobranca, out decimal valorBaixa)
        {
            valorDescontoDuplicata = Convert.ToDecimal(doc.ValorJurosPago);
            valorIOF = Convert.ToDecimal(doc.ValorIOF);
            vlTaxaCobranca = Convert.ToDecimal(doc.ValorTaxaCobranca);
            valorBaixa = Convert.ToDecimal(doc.ValorCredito);

            if (remessa.GeraMovTaxaBoleto)
                valorBaixa = valorBaixa + vlTaxaCobranca;
            else vlTaxaCobranca = 0;

            if (remessa.GeraMovTaxaDesconto) // Esse valor é referente ao juro cobrado por desconto de duplicata(venda do título ao banco)
                valorBaixa = valorBaixa + valorDescontoDuplicata;
            else valorDescontoDuplicata = 0;

            if (remessa.GeraMovTaxaIOF)
                valorBaixa = valorBaixa + valorIOF;
            else valorIOF = 0;
        }

        private void GravaLogDetalhe(LogRetorno logRetorn, Documento document, string ocorrenc, DadosRetorno DadosRet)
        {
            LogRetornoDetalhe logRetDetalhe = new LogRetornoDetalhe();
            logRetDetalhe.logRetorno = logRetorn;
            logRetDetalhe.documento = document;
            logRetDetalhe.Ocorrencia = ocorrenc;
            logRetDetalhe.NumeroDocumento = DadosRet.NumeroDocumento;
            logRetDetalhe.NossoNumero = DadosRet.NossoNumero;
            logRetDetalhe.ValorCredito = (decimal) DadosRet.ValorCredito;
            logRetDetalhe.ValorDesconto = (decimal) DadosRet.ValorDesconto;
            logRetDetalhe.ValorJurosPago = (decimal) DadosRet.ValorJurosPago;
            logRetDetalhe.ValorMultaPaga = (decimal) DadosRet.ValorMultaPaga;
            logRetDetalhe.ValorIOF = (decimal) DadosRet.ValorIOF;
            logRetDetalhe.ValorOutrosAcrescimos = (decimal) DadosRet.ValorOutrosAcrescimos;
            logRetDetalhe.ValorPago = (decimal) DadosRet.ValorPago;
            logRetDetalhe.ValorTaxaCobranca = (decimal) DadosRet.ValorTaxaCobranca;
            LogRetornoDetalheController.Instancia.Salvar(logRetDetalhe, Acao.Incluir);
        }

        private void AbrirArquivoLog()
        {
            log = new StreamWriter(remessa.CaminhoLogBaixa + "\\" + $"logBaixaRetorno_{DateTime.Now:ddMMyyyy_hhmm}.txt", true, Encoding.UTF8);
            nomearquivo = dadosRetorno[0].CaminhoArquivo;
            texto = nomearquivo;
            GravaArquivoLog(texto);
            texto = "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            GravaArquivoLog(texto);
            texto = "Documento  Ocorência          				Juros R$                 Valor R$                 Valor Taxa R$                 Valor Multa R$         Total R$";
            GravaArquivoLog(texto);
            texto = "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            GravaArquivoLog(texto);
        }

        private void GravaArquivoLog(string texto)
        {
            log.WriteLine(texto);
        }

        private void FecharArquivoLog()
        {
            texto = "------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            GravaArquivoLog(texto);
            texto = "Total:                                                  " + totalJuros.ToString("F2") + "                     " + totalValor.ToString("F2") +
                    "                  " + totalValorTaxa.ToString("F2") + "                         " + totalMulta.ToString("F2") + "                    " + totalDocumentos.ToString("F2");
            GravaArquivoLog(texto);
            log.Close();
        }
    }
}