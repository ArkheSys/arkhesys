using cwkGestao.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = cwkGestao.Modelo; 

namespace cwkGestao.Negocio.Financeiro
{
    public class MovimentacaoCheque
    {
        private MO.MovimentacaoCheque objMovCheque;
        private MO.Cheque objCheque;

        public MovimentacaoCheque(MO.MovimentacaoCheque objMovCheque, MO.Cheque objCheque)
        {
            this.objMovCheque = objMovCheque;
            this.objCheque = objCheque;
        }

        public void GeraMovimentosCheque()
        {
            List<MovCaixa> movCaixa = new List<MovCaixa>();

            if (objMovCheque != null)
            {
                switch (objMovCheque.Movimentacao)
                {
                    case MO.MovimentacaoCheque.TipoMovimentacao.Deposito:
                    case MO.MovimentacaoCheque.TipoMovimentacao.Devolucao:
                        movCaixa.Add(CriarMovCaixa(Ent_SaiMovCaixa.Saída, objMovCheque.Banco, movCaixa));
                        movCaixa.Add(CriarMovCaixa(Ent_SaiMovCaixa.Entrada, objMovCheque.BancoDestino, movCaixa));
                        objCheque.Conta = objMovCheque.BancoDestino;
                        objCheque.MovCaixas = movCaixa;
                        break;
                }

                AlteraStatusCheque(objMovCheque.Movimentacao);
                CriarChequeHistorico();
            }
        }
        
        public void AlteraStatusCheque(MO.MovimentacaoCheque.TipoMovimentacao tipoMovimentacao)
        {
            switch (tipoMovimentacao)
            {
                case MO.MovimentacaoCheque.TipoMovimentacao.Deposito:
                    if (objCheque.Status == Cheque.StatusCheque.Devolvido)
                        objCheque.Status = Cheque.StatusCheque.Redepositado;
                    else
                        objCheque.Status = Cheque.StatusCheque.Depositado;
                    break;
                case MO.MovimentacaoCheque.TipoMovimentacao.Devolucao:
                    objCheque.Status = Cheque.StatusCheque.Devolvido;
                    break;
                case MO.MovimentacaoCheque.TipoMovimentacao.Receber:
                    objCheque.Status = Cheque.StatusCheque.Pago;
                    break;
            }
        }

        public MovCaixa CriarMovCaixa(Ent_SaiMovCaixa ent_saida, Banco banco, List<MovCaixa> ListaMovimentos)
        {
            MovCaixaController mcc = MovCaixaController.Instancia;
            Movimento mov = new Movimento();
            mov.Historico = objMovCheque.Historico;
            mov.ComplementoHist = objMovCheque.ComplementoHist;
            mov.PlanoConta = objMovCheque.PlanoConta;
            mov.Valor = objMovCheque.Valor;
            mov.Codigo = 1;
            mov.Dt = objMovCheque.Dt;

            List<Movimento> Movimentos = new List<Movimento>();
            Movimentos.Add(mov);
            IList<Cheque> Cheques = new List<Cheque>();
            Cheques.Add(objCheque);

            int cod = 0;

            MovCaixa movCaixa = null;
            cod = mcc.MaxCodigo();

            movCaixa = new MovCaixa
            {
                Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento(),
                Ent_Sai = ent_saida,
                Dt = objMovCheque.Dt,
                DtDigitacao = DateTime.Now,
                ComplementoHist = objMovCheque.ComplementoHist,
                Historico = objMovCheque.Historico,
                Filial = objMovCheque.Filial,
                Valor = objMovCheque.Valor,
                Banco = banco,
                Movimentos = Movimentos,
                FormaPagamento = FormaPagamentoController.Instancia.GetCheque(),
                Cheque = objCheque
            };
            mov.MovCaixa = movCaixa;

            MovCaixaController.Instancia.RegisterNewIntoTransaction(movCaixa);

            return movCaixa;
        }

        private void CriarChequeHistorico()
        {
            IList<ChequeHistorico> historicosCheque = new List<ChequeHistorico>();

            ChequeHistorico chequeHistorico = new ChequeHistorico();
            chequeHistorico.Movimento = objMovCheque.MovimentacaoLegivel;
            chequeHistorico.OrigemMovimento = "Movimentação de Cheque";
            chequeHistorico.Cheque = objCheque;
            chequeHistorico.Data = DateTime.Now;
            chequeHistorico.Historico = objMovCheque.Historico;
            chequeHistorico.Status = objCheque.Status;
            chequeHistorico.MovCaixa = objCheque.MovCaixas.FirstOrDefault();

            historicosCheque.Add(chequeHistorico);
            objCheque.ChequeHistorico = historicosCheque;
        }
    }
}
