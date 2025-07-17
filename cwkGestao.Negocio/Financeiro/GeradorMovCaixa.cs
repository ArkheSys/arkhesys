using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio;

namespace cwkGestao.Negocio.Financeiro
{
    public class GeradorMovCaixa
    {
        private readonly List<pxyFormaPagamentoBaixaDoc> listformaspagamento;
        private readonly Documento documentobaixar;
        private readonly DateTime databaixa;
        private readonly string historico;

        public GeradorMovCaixa(List<pxyFormaPagamentoBaixaDoc> listformaspagamento, Documento documentobaixar, DateTime databaixa, string historico)
        {
            this.listformaspagamento = listformaspagamento;
            this.documentobaixar = documentobaixar;
            this.databaixa = databaixa;
            this.historico = historico;
        }

        public List<MovCaixa> GetMovCaixa(bool MovimentoVemDeNota = false)
        {
            List<MovCaixa> movCaixas = new List<MovCaixa>();
            MovCaixaController mcc = MovCaixaController.Instancia;
            int cod = 0;
            TipoDocumentoType ent_Sai = documentobaixar.Tipo;

            foreach (var item in listformaspagamento)
            {
                var FormaPagamentoIsAvista = documentobaixar.Condicao.CondicaoParcelas.Where(o => o.TipoDocumento.FormaPagamento.ID == item.FormaPagamento.ID).FirstOrDefault()?.BAVista ?? false;
                if (!FormaPagamentoIsAvista && MovimentoVemDeNota)
                    continue;

                MovCaixa movCaixa = movCaixas.Where(m => m.FormaPagamento.ID == item.FormaPagamento.ID).FirstOrDefault();
                if (movCaixa == null)
                {
                    if (movCaixas.Count == 0)
                        cod = mcc.MaxCodigo();
                    else
                        cod++;

                    movCaixa = new MovCaixa
                    {
                        Codigo = cod,
                        Ent_Sai = documentobaixar.Tipo == TipoDocumentoType.Receber ? Ent_SaiMovCaixa.Entrada : Ent_SaiMovCaixa.Saída,
                        Dt = databaixa,
                        DtDigitacao = DateTime.Now,
                        ComplementoHist = historico,
                        Historico = documentobaixar.Historico,
                        Filial = documentobaixar.Filial,
                        FormaPagamento = item.FormaPagamento,
                        NumBanco = item.Banco,
                        NumCheque = item.NumeroCheque,
                        NumContaCorrente = item.ContaCorrente,
                        NumDocumento = documentobaixar.NumDocumento,
                        Cheque = item.Cheque
                    };
                    movCaixas.Add(movCaixa);
                }
                if (item.FormaPagamento.Tipo == FormaPagamentoTipo.Cheque)
                {
                    var chequeHistorico = CriarChequeHistorico(item, ent_Sai);
                    chequeHistorico.MovCaixa = movCaixa;
                }

                movCaixa.Valor += item.Valor;
            }

            return movCaixas;
        }

        private ChequeHistorico CriarChequeHistorico(pxyFormaPagamentoBaixaDoc item, TipoDocumentoType ent_Sai)
        {
            IList<ChequeHistorico> historicoCheque = new List<ChequeHistorico>();
            item.Cheque.Status = ent_Sai == TipoDocumentoType.Pagar ? cwkGestao.Modelo.Cheque.StatusCheque.Baixado : cwkGestao.Modelo.Cheque.StatusCheque.EmCaixa;

            ChequeHistorico chequeHistorico = new ChequeHistorico();
            chequeHistorico.Movimento = ent_Sai == TipoDocumentoType.Pagar ? "Pagamento" : "Recebimento";
            chequeHistorico.OrigemMovimento = ent_Sai == TipoDocumentoType.Pagar ? "Contas a Pagar" : "Contas a Receber";
            chequeHistorico.Cheque = item.Cheque ?? CriarCheque(item);
            chequeHistorico.Documento = documentobaixar;
            chequeHistorico.Data = DateTime.Now;
            chequeHistorico.Historico = documentobaixar.Historico;
            chequeHistorico.Status = item.Cheque.Status;
            chequeHistorico.MovCaixa = item.Cheque.MovCaixas.FirstOrDefault();

            historicoCheque.Add(chequeHistorico);
            item.Cheque.ChequeHistorico = historicoCheque;
            item.Cheque.Status = ent_Sai == TipoDocumentoType.Pagar ? cwkGestao.Modelo.Cheque.StatusCheque.Baixado : cwkGestao.Modelo.Cheque.StatusCheque.EmCaixa;

            return chequeHistorico;
        }

        private Cheque CriarCheque(pxyFormaPagamentoBaixaDoc item)
        {
            return new Cheque
            {
                Valor = item.Valor,
                Agencia = item.Agencia,
                Banco = item.Banco,
                ChTerceiro = item.ChequeTerceiro,
                CNPJCPF_Emitente = item.CNPJCPFEmitente,
                ContaCorrente = item.ContaCorrente,
                Numero = item.NumeroCheque,
                Vencimento = item.Data,
                Emitente = item.NomeEmitente,
                Status = item.Status
            };
        }

    }
}
