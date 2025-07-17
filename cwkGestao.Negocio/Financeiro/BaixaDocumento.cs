using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Auxiliares;

namespace cwkGestao.Negocio.Financeiro
{
    public class BaixaDocumento
    {
        private IList<ParametrosBaixa> documentos;

        public Historico HistoricoTroco { get; set; }
        public string ComplementoHistTroco { get; set; }

        public BaixaDocumento(ParametrosBaixa documento)
            : this(new List<ParametrosBaixa>() { documento }) { }

        public BaixaDocumento(IList<ParametrosBaixa> documentos)
        {
            this.documentos = documentos;
           
        }

        public void CommitarTransacao()
        {
            DocumentoController.Instancia.CommitUnitOfWorkTransaction();
        }

        public void BaixarDocumentos(IList<MovCaixa> movCaixas)
        {
            decimal totalFormasPagamento = documentos.Sum(MC => (MC.ValorBaixa + MC.ValorJuros + MC.ValorMulta - MC.ValorDesconto));
            decimal valorTotal = documentos.Sum(f => f.ValorTotal);

            bool gerarTroco = movCaixas.Count > 0 && totalFormasPagamento > valorTotal;

            List<Movimento> movimentos;

            if (totalFormasPagamento > 0 && totalFormasPagamento < valorTotal)
                throw new InvalidOperationException("Soma das formas de pagamento é menor que o valor total. (R$" + String.Format("{0:0.00}", totalFormasPagamento) + "/R$" + String.Format("{0:0.00}", valorTotal) + ")");

            foreach (ParametrosBaixa documento in documentos)
            {
                movimentos = new List<Movimento>();
                
                if (movCaixas.Count > 0)
                {
                    MovCaixa x = movCaixas.First();
                    if (x.FormaPagamento == null && documento.Documento.TipoDocumento.FormaPagamento == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Forma de pagamento não definida.", "Erro.", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        return;
                    }
                }


                if (movCaixas.Count > 0)
                    BaixaComOperacaoFinanceira(movCaixas, movimentos, documento);
                else
                    BaixaComTrocaDeTitulos(movimentos, documento);

                documento.Documento.Saldo -= (documento.ValorBaixa - documento.Documento.valorImpostoRetido);
                VinculaMovimentoDocumento(documento.Documento, movimentos);


                foreach (var item in documento.Documento.Movimentos)
                {
                    if (item.MovCaixa != null)
                    {
                        item.MovCaixa.Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento();
                        MovCaixaController.Instancia.RegisterNewIntoTransaction(item.MovCaixa);


                        if (item.MovCaixa.Cheque != null)
                        {
                            item.MovCaixa.Cheque.MovCaixas = movCaixas;
                            ChequeController.Instancia.RegisterNewIntoTransaction(item.MovCaixa.Cheque);
                        }
                    }
                }

                documento.Documento.Renegociacao = documento.Renegociacao;

                AtualizarSituacao(documento.Documento); // salva no Documento se é BxP ou BxT
                documento.Documento.DtUltimaBaixa = documento.DataBaixa;
                DocumentoController.Instancia.RegisterNewIntoTransaction(documento.Documento);
            }

            if (gerarTroco)
                CriarMovCaixaTroco(movCaixas, documentos[0], totalFormasPagamento - valorTotal);
        }

        private void BaixaComTrocaDeTitulos(List<Movimento> movimentos, ParametrosBaixa parms)
        {
            foreach (var mov in parms.Documento.Movimentos)
            {
                Movimento movimentoBaixa = CriarMovimento(parms.Documento, parms);
                movimentoBaixa.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
                movimentoBaixa.Valor = mov.Valor;
                movimentoBaixa.Tipo = TipoMovimentoType.Documento;
                movimentoBaixa.PlanoConta = mov.PlanoConta;
                movimentoBaixa.Historico = parms.Documento.Historico;
                movimentoBaixa.ComplementoHist = parms.ComplementoHist;

                movimentos.Add(movimentoBaixa);
            }
        }

        private void BaixaComOperacaoFinanceira(IList<MovCaixa> movCaixas, List<Movimento> movimentos, ParametrosBaixa parametrosBaixaParcial)
        {
            decimal totalMovCaixa = movCaixas.Sum(s => s.Valor);

            foreach (var item in movCaixas)
            {
                /* Essa variavel armazena o percentual que o movCaixa representa entre todos eles, para fazer o calculo do pagamento sobre os movimento
                 * (essa rotina tem efeito quando o pagamento é feito com mais de um tipo de pagamento, Ex: em dinheiro e em cheque, nesse caso são gerados 2 movCaixa)*/
                item.proporcaoPagamento =  (item.Valor * 100 / (totalMovCaixa ==0? 1: totalMovCaixa));
                var lista = CriarMovimentos(parametrosBaixaParcial, item);
                movimentos.AddRange(lista);
            }

            CriarMovimentoMovCaixa(movCaixas, movimentos, parametrosBaixaParcial, parametrosBaixaParcial.Documento);
            CriarMovCaixa(movCaixas, movimentos, parametrosBaixaParcial); // Gera MovCaixa dos movimentos de juros, multa e Desconto            
            CriarMovCaixaDescontoDuplicata(movCaixas, movimentos, parametrosBaixaParcial); // Gera MovCaixa dos movimentos referentes a desconto de duplicata (juros de desconto, taxa boleto e IOF)

            CorrigiArredondamentoBaixa(movimentos);
        }

        private static void CorrigiArredondamentoBaixa(List<Movimento> movimentos)
        {
            /*Método utilizado para correção de diferença de centavos entre o total dos movimentos e o valor do MovCaixa*/
            // separa os movimentos por movCaixa
            var MovimentosPorMov = movimentos.Select(i => i).GroupBy(x => x.MovCaixa);
            // Verifica para cada movCaixa
            foreach (var MPM in MovimentosPorMov)
            {
                int contMoviment = 0;
                decimal valorBaixa = 0;
                decimal diferenca = 0;
                // So o valor dos movimentos e se houver diferença corrige no ultimo movimento do movCaixa
                foreach (var moviment in MPM)
                {
                    contMoviment++;
                    if (moviment.Tipo == TipoMovimentoType.Desconto)
                        valorBaixa = valorBaixa - moviment.Valor;
                    else
                        valorBaixa = valorBaixa + moviment.Valor;
                    /* Apenas corrigi diferença de alguns centavos, ajustando o valor do ultimo movimento
                     *** cuidado ao mudar esses valores, pois na baixa em lote, essa função não funciona, o valor do mov caixa é o somatório de mais de um documento, essa função funciona apenas para as baixas que não são em lote, onde o movCaixa pertence a apenas um documento*/
                    diferenca = valorBaixa - moviment.MovCaixa.Valor;
                    if ((MPM.Count() == contMoviment) && ((diferenca > -(decimal)0.05) && (diferenca < (decimal)0.05)))
                    {
                        moviment.Valor = moviment.Valor + diferenca;
                    }

                }
            }
        }

        private void VinculaMovimentoDocumento(Documento documento, IList<Movimento> movimentos)
        {
            foreach (var item in movimentos)
            {
                documento.Movimentos.Add(item);
            }
        }

        private void CriarMovCaixaTroco(IList<MovCaixa> movCaixas, ParametrosBaixa parametrosBaixa, decimal valorTroco)
        {
            decimal totalFormasPagamento = movCaixas.Sum(fpm => fpm.Valor);
            decimal totalFormasPagamentoNCheque = movCaixas.Where(fpm => fpm.FormaPagamento.Tipo != FormaPagamentoTipo.Cheque).Sum(fpm => fpm.Valor);
            MovCaixa troco = new MovCaixa();
            troco.Valor = valorTroco != 0 ? valorTroco : Math.Abs(totalFormasPagamento - parametrosBaixa.ValorTotal);
            troco.Banco = parametrosBaixa.Banco;
            troco.Historico = HistoricoTroco;
            troco.ComplementoHist = ComplementoHistTroco;

            troco.Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento();
            troco.Filial = parametrosBaixa.Documento.Filial;
            troco.NumDocumento = parametrosBaixa.Documento.NumDocumento;
            troco.Ent_Sai = Ent_SaiMovCaixa.Saída;
            troco.Dt = DateTime.Today.Date;

            Movimento movTroco = new Movimento();
            movTroco.Valor = troco.Valor;
            movTroco.Dt = DateTime.Today.Date;
            movTroco.Codigo = 1;
            movTroco.MovCaixa = troco;
            movTroco.Operacao = OperacaoMovimentoType.Troco;
            movTroco.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().PlanoContaTroco;
            movTroco.Sequencia = 1;
            movTroco.Tipo = TipoMovimentoType.Troco;
            movTroco.Historico = HistoricoTroco;
            movTroco.ComplementoHist = ComplementoHistTroco;
            //Os movimentos de troco devem ficar com origem no Caixa, para que o MovCaixa possa ser alterado ou excluído
            movTroco.Origem = (int)OrigemMovimentoType.Caixa;

            troco.Movimentos.Add(movTroco);

            if (totalFormasPagamentoNCheque > 0 && !parametrosBaixa.BaixarEmLote)
            {
                Movimento movSobra = new Movimento();
                movSobra.Valor = totalFormasPagamentoNCheque;
                movSobra.Dt = DateTime.Today.Date;
                movSobra.Codigo = 2;
                movSobra.MovCaixa = troco;
                movSobra.Operacao = OperacaoMovimentoType.BxTotal;
                movSobra.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().PlanoContaTroco;
                movSobra.Sequencia = 1;
                movSobra.Tipo = TipoMovimentoType.Documento;
                movSobra.Historico = HistoricoTroco;
                movSobra.ComplementoHist = ComplementoHistTroco;
                movSobra.Origem = (int)OrigemMovimentoType.Caixa;
                troco.Valor = Math.Abs(troco.Valor - (totalFormasPagamento - totalFormasPagamentoNCheque));

                troco.Movimentos.Add(movSobra);
            }

            MovCaixaController.Instancia.RegisterNewIntoTransaction(troco);
        }

        public static void AtualizarSituacao(Documento doc, DateTime? dtCredito, bool TDesc)
        {
            AtualizarSituacao(doc);
            if (TDesc && doc.Saldo <= 0 && dtCredito != null)
            {
                doc.DtUltimaBaixa = dtCredito;
            }
        }

        public static void AtualizarSituacao(Documento documento)
        {
            if (documento.Situacao != "TDesc")
            {
                if (documento.Renegociacao)
                {
                    documento.Situacao = "BxR";
                }
                else
                {
                    if (documento.Saldo == documento.Valor)
                    {
                        documento.Situacao = "Norm";
                    }
                    else if (documento.Saldo > 0)
                    {
                        documento.Situacao = "BxP";
                    }
                    else if (documento.Saldo == 0)
                    {
                        documento.Situacao = "BxT";
                    }
                    else
                    {
                        throw new Exception("Saldo do documento menor que 0");
                    }
                }
            }
        }

        private MovCaixa CriarMovCaixa(IList<MovCaixa> movCaixas, IList<Movimento> movimentos, ParametrosBaixa parametrosBaixa)
        {
            MovCaixa movCaixa = null;

            foreach (var item in movimentos)
            {
                movCaixa = new MovCaixa();
                movCaixa.Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento();
                movCaixa.Filial = parametrosBaixa.Documento.Filial;
                movCaixa.Dt = parametrosBaixa.DataBaixa.Date;
                movCaixa.DtDigitacao = parametrosBaixa.DataBaixa;

                if (!parametrosBaixa.BaixarEmLote)
                    movCaixa.NumDocumento = parametrosBaixa.Documento.NumDocumento;

                movCaixa.Historico = parametrosBaixa.Documento.Historico;
                movCaixa.ComplementoHist = parametrosBaixa.ComplementoHist;
                movCaixa.Ent_Sai = parametrosBaixa.Documento.Tipo == TipoDocumentoType.Receber ? Ent_SaiMovCaixa.Entrada : Ent_SaiMovCaixa.Saída;
                movCaixa.Banco = parametrosBaixa.Banco;
                movCaixa.Dt = DateTime.Today.Date;
                movCaixa.Valor = item.Valor;
                movCaixa.FormaPagamento = movCaixas.First().FormaPagamento;
                if (item.MovCaixa != null)
                   item.MovCaixa.Banco = parametrosBaixa.Banco;// item.Documento.Banco;

                if (item.MovCaixa == null)
                {
                    movCaixa.Banco = parametrosBaixa.Banco;// item.Documento.Banco;
                    item.MovCaixa = movCaixa;
                }
            }

            return movCaixa;
        }

        private void CriarMovCaixaDescontoDuplicata(IList<MovCaixa> movCaixas, IList<Movimento> movimentos, ParametrosBaixa parametrosBaixa)
        {
            foreach (var item in movimentos)
            {
                if ((item.Tipo == TipoMovimentoType.IOF) || (item.Tipo == TipoMovimentoType.TaxaBoleto) || (item.Tipo == TipoMovimentoType.JurosDesconto))
                    item.MovCaixa = CriarMovCaixaMovimentosDescontoDuplicata(movCaixas, item, parametrosBaixa);                
            }
        }

        private MovCaixa CriarMovCaixaMovimentosDescontoDuplicata(IList<MovCaixa> movCaixas, Movimento movimento, ParametrosBaixa parametrosBaixa)
        {
            MovCaixa movCaixa = new MovCaixa();
            movCaixa.Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento();
            movCaixa.Filial = parametrosBaixa.Documento.Filial;
            movCaixa.Dt = parametrosBaixa.DataBaixa.Date;
            movCaixa.DtDigitacao = parametrosBaixa.DataBaixa;

            if (!parametrosBaixa.BaixarEmLote)
                movCaixa.NumDocumento = parametrosBaixa.Documento.NumDocumento;

            switch (movimento.Tipo)
            {
                case TipoMovimentoType.IOF:
                    movCaixa.Historico = parametrosBaixa.remessa.HistoricoTaxaIOF;
                    movCaixa.ComplementoHist = parametrosBaixa.remessa.HistoricoTaxaIOF.Nome.ToString();
                    break;
                case TipoMovimentoType.JurosDesconto:
                    movCaixa.Historico = parametrosBaixa.remessa.HistoricoTaxaDesconto;
                    movCaixa.ComplementoHist = parametrosBaixa.remessa.HistoricoTaxaDesconto.Nome.ToString();
                    break;
                case TipoMovimentoType.TaxaBoleto:
                    movCaixa.Historico = parametrosBaixa.remessa.HistoricoTaxaBoleto;
                    movCaixa.ComplementoHist = parametrosBaixa.remessa.HistoricoTaxaBoleto.Nome.ToString();
                    break;
            }  

            movCaixa.Ent_Sai = Ent_SaiMovCaixa.Saída;
            movCaixa.Banco = parametrosBaixa.Banco;
            movCaixa.Valor = movimento.Valor;
            movCaixa.FormaPagamento = movCaixas.First().FormaPagamento;

            return movCaixa;
        }

        private void CriarMovimentoMovCaixa(IList<MovCaixa> movCaixas, IList<Movimento> movimentos, ParametrosBaixa parms, Documento documento)
        {
            /*
             * Este método gera os movimentos de acordo com os movCaixas
             * 
             * O cálculo de rateio é realizado da seguinte forma:
             * 
             * vlrItem = Valor total do documento que foi criado
             * docto.valor = Valor total de um único plano de contas cadastrado no ato de lançamento
             * 
             * exemplo: Documento com dois planos de conta e valor total de 10000
             * 
             *      vlrItem = 10000
             *      docto.valor(1) = 600 (valor do primeiro plano de contas)
             *      docto.valor(2) = 400 (valor do primeiro plano de contas)
             *      
             * O método realiza o calculo de rateio somente sobre os LANÇAMENTOS
             * (OperacaoMovimentoType.Lancamento)
             * 
             */
            int count = 0;
                        
            var mov = parms.Documento.Movimentos.Where(mc => mc.MovCaixa == null).OrderBy(m => m.Valor).ToList();
            GeraMovimentoParcela(movCaixas, movimentos, parms, documento, count, mov);
        }

        private void GeraMovimentoParcela(IList<MovCaixa> movCaixas, IList<Movimento> movimentos, ParametrosBaixa parms, Documento documento, int count, List<Movimento> mov)
        {
            foreach (var item in movCaixas)
            {
                count = 0;
                Movimento movimento = new Movimento();
                    foreach (var movto in mov)
                    {
                        movimento = CriarMovimento(parms.Documento, parms);
                        decimal vlrItem =  mov.Where(x=> x.Operacao != OperacaoMovimentoType.Retencao).Sum(v => v.Valor);
                        movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
                        var docto = documento.Movimentos.ElementAt(count);
                        if (docto.Operacao == OperacaoMovimentoType.Lancamento)
                        {
                            decimal valorRateio = vlrItem == 0 ? 0 : (docto.Valor) / (vlrItem);
                            decimal vlrFinal = parms.ValorBaixa * valorRateio;
                            vlrFinal = Math.Round(((vlrFinal * item.proporcaoPagamento) / 100), 2);
                            movimento.Valor = vlrFinal;
                            movimento.Tipo = TipoMovimentoType.Documento;
                            movimento.PlanoConta = docto.PlanoConta;
                            movimento.Historico = parms.Documento.Historico;
                            movimento.ComplementoHist = parms.ComplementoHist;
                            movimento.MovCaixa = item;
                            movimentos.Add(movimento);
                            count++;
                        }
                    }
            }
        }
        

        private Movimento CriarMovimento(Documento documento, ParametrosBaixa parametrosBaixa)
        {
            Movimento movimento = new Movimento();

            movimento.Documento = documento;
            movimento.Dt = parametrosBaixa.DataBaixa.Date;
            movimento.Historico = documento.Historico;
            movimento.ComplementoHist = parametrosBaixa.ComplementoHist;
            movimento.Origem = (int)OrigemMovimentoType.Financeiro;

            if (parametrosBaixa.Renegociacao)
            {
                movimento.Operacao = OperacaoMovimentoType.BxRenegociação;
            }
            else
            {
                if (parametrosBaixa.Documento.Saldo == parametrosBaixa.ValorBaixa - parametrosBaixa.Documento.valorImpostoRetido)
                    movimento.Operacao = OperacaoMovimentoType.BxTotal;
                else if (parametrosBaixa.Documento.Saldo > parametrosBaixa.ValorBaixa - parametrosBaixa.Documento.valorImpostoRetido)
                    movimento.Operacao = OperacaoMovimentoType.BxParcial;
                else
                    throw new InvalidOperationException("Valor da baixa maior que o saldo do documento.");
            }

            movimento.Sequencia = documento.Movimentos.GroupBy(mov => mov.Sequencia).Count() + 1;

            return movimento;
        }

        private IList<Movimento> CriarMovimentos(ParametrosBaixa parametrosBaixa, MovCaixa Movcaixa)
        {
            IList<Movimento> movimentos = new List<Movimento>();

            if (parametrosBaixa.ValorMulta > 0)
                movimentos.Add(CriarMovimentoMulta(parametrosBaixa, Movcaixa));

            if (parametrosBaixa.ValorJuros > 0)
                movimentos.Add(CriarMovimentoJuros(parametrosBaixa, Movcaixa));

            if (parametrosBaixa.ValorDesconto > 0)
                movimentos.Add(CriarMovimentoDesconto(parametrosBaixa, Movcaixa));

            if (parametrosBaixa.ValorDescontoDuplicata > 0)
                movimentos.Add(CriarMovimentoDescontoDuplicata(parametrosBaixa));

            if (parametrosBaixa.ValorIOF > 0)
                movimentos.Add(CriarMovimentoIOF(parametrosBaixa));

            if (parametrosBaixa.ValorTaxaCobranca > 0)
                movimentos.Add(CriarMovimentoTaxaCobranca(parametrosBaixa));

            return movimentos;
        }

        String erros = "";
        private Movimento CriarMovimentoJuros(ParametrosBaixa parametrosBaixa, MovCaixa movCaixa)
        {
            erros = "";
            Movimento movimento = CriarMovimento(parametrosBaixa.Documento, parametrosBaixa);
            movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            movimento.Valor = Math.Round((parametrosBaixa.ValorJuros * movCaixa.proporcaoPagamento) / 100, 2);
            movimento.Tipo = TipoMovimentoType.Juro;
            

            if (movimento.Documento.Tipo == TipoDocumentoType.Receber)
            {
                movimento.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().IDPlanoContaJurosRecebidos;
                if (movimento.PlanoConta == null)
                    erros = "Não foi informado o plano de contas para Juros Recebido, Verifique o parâmetro no Cadastro de Configuração do Sistema na aba Financeiro!";
            }
            else
            {
                movimento.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().IDPlanoContaJurosPagos;
                if (movimento.PlanoConta == null)
                    erros = "Não foi informado o plano de contas para Juros Pago, Verifique o parâmetro no Cadastro de Configuração do Sistema na aba Financeiro!";
            }
            movimento.Historico = parametrosBaixa.Documento.Historico;
            movimento.ComplementoHist = parametrosBaixa.ComplementoHist;
            movimento.MovCaixa = movCaixa;

            if (erros != "")
            {
                throw new Exception(erros);
            }
            return movimento;
        }

        private Movimento CriarMovimentoMulta(ParametrosBaixa parametrosBaixa, MovCaixa movCaixa)
        {
            erros = "";
            Movimento movimento = CriarMovimento(parametrosBaixa.Documento, parametrosBaixa);
            movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            movimento.Valor = Math.Round((parametrosBaixa.ValorMulta * movCaixa.proporcaoPagamento) / 100, 2);
            movimento.Tipo = TipoMovimentoType.Multa;
            if (movimento.Documento.Tipo == TipoDocumentoType.Receber)
            {  
    		     movimento.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().IDPlanoContaMultasRecebidas;
                 if (movimento.PlanoConta == null)
                     erros = "Não foi informado o plano de contas para Multas Recebida, Verifique o parâmetro no Cadastro de Configuração do Sistema na aba Financeiro!";
	        }
            else
            { 
                movimento.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().IDPlanoContaMultasPagas;
                if (movimento.PlanoConta == null)
                     erros = "Não foi informado o plano de contas para Multas Paga, Verifique o parâmetro no Cadastro de Configuração do Sistema na aba Financeiro!";
            }
               
            movimento.Historico = parametrosBaixa.Documento.Historico;
            movimento.ComplementoHist = parametrosBaixa.ComplementoHist;
            movimento.MovCaixa = movCaixa;

            if (erros != "")
            {
                throw new Exception(erros);
            }
            return movimento;
        }

        private Movimento CriarMovimentoDesconto(ParametrosBaixa parametrosBaixa, MovCaixa movCaixa)
        {
            Movimento movimento = CriarMovimento(parametrosBaixa.Documento, parametrosBaixa);
            movimento.Operacao = OperacaoMovimentoType.Lancamento;
            movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            movimento.Tipo = TipoMovimentoType.Desconto;
            movimento.Historico = parametrosBaixa.Documento.Historico;
            movimento.ComplementoHist = parametrosBaixa.ComplementoHist;
            movimento.MovCaixa = movCaixa;
            movimento.Valor = Math.Round((parametrosBaixa.ValorDesconto * movCaixa.proporcaoPagamento) / 100, 2);

            if (parametrosBaixa.Documento.Tipo == TipoDocumentoType.Pagar)
            {
                movimento.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().PlanoContaDescontoPagar;
                if (movimento.PlanoConta == null)
                    erros = "Não foi informado o plano de contas para Desconto Recebido, Verifique o parâmetro no Cadastro de Configuração do Sistema na aba Financeiro!";
            }
            else
            {
                movimento.PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().PlanoContaDescontoReceber;
                if (movimento.PlanoConta == null)
                    erros = "Não foi informado o plano de contas para Desconto Pagar, Verifique o parâmetro no Cadastro de Configuração do Sistema na aba Financeiro!";
            }

            if (erros != "")
            {
                throw new Exception(erros);
            }

            return movimento;
        }

        private Movimento CriarMovimentoDescontoDuplicata(ParametrosBaixa parametrosBaixa)
        {
            Movimento movimento = CriarMovimento(parametrosBaixa.Documento, parametrosBaixa);
            movimento.Operacao = OperacaoMovimentoType.BxParcial;
            movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            movimento.Tipo = TipoMovimentoType.JurosDesconto;
            movimento.Historico = parametrosBaixa.remessa.HistoricoTaxaDesconto;
            movimento.ComplementoHist = parametrosBaixa.remessa.HistoricoTaxaDesconto.Nome;
            movimento.Valor = Math.Round(parametrosBaixa.ValorDescontoDuplicata, 2);
            movimento.PlanoConta = parametrosBaixa.remessa.PlanoContaTaxaDesconto;
            if (movimento.PlanoConta == null)
                erros = "Não foi informado o plano de contas para Taxa Desconto de Duplicata, Verifique o parâmetro no Cadastro de Remessa na aba Dados Retorno!";

            if (erros != "")
            {
                throw new Exception(erros);
            }

            return movimento;
        }

        private Movimento CriarMovimentoIOF(ParametrosBaixa parametrosBaixa)
        {
            Movimento movimento = CriarMovimento(parametrosBaixa.Documento, parametrosBaixa);
            movimento.Operacao = OperacaoMovimentoType.BxParcial;
            movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            movimento.Tipo = TipoMovimentoType.IOF;
            movimento.Historico = parametrosBaixa.remessa.HistoricoTaxaIOF;
            movimento.ComplementoHist = parametrosBaixa.remessa.HistoricoTaxaIOF.Nome;
            movimento.Valor = Math.Round(parametrosBaixa.ValorIOF, 2);
            movimento.PlanoConta = parametrosBaixa.remessa.PlanoContaTaxaIOF;
            if (movimento.PlanoConta == null)
                erros = "Não foi informado o plano de contas para IOF, Verifique o parâmetro no Cadastro de Remessa na aba Dados Retorno!";

            if (erros != "")
            {
                throw new Exception(erros);
            }

            return movimento;
        }

        private Movimento CriarMovimentoTaxaCobranca(ParametrosBaixa parametrosBaixa)
        {
            Movimento movimento = CriarMovimento(parametrosBaixa.Documento, parametrosBaixa);
            movimento.Operacao = OperacaoMovimentoType.BxParcial;
            movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            movimento.Tipo = TipoMovimentoType.TaxaBoleto;
            movimento.Historico = parametrosBaixa.remessa.HistoricoTaxaBoleto;
            movimento.ComplementoHist = parametrosBaixa.remessa.HistoricoTaxaBoleto.Nome;
            movimento.Valor = Math.Round(parametrosBaixa.ValorTaxaCobranca, 2);
            movimento.PlanoConta = parametrosBaixa.remessa.PlanoContaTaxaBoleto;
            if (movimento.PlanoConta == null)
                erros = "Não foi informado o plano de contas para Taxa Boleto, Verifique o parâmetro no Cadastro de Remessa na aba Dados Retorno!";

            if (erros != "")
            {
                throw new Exception(erros);
            }
            return movimento;
        }
    }
}
