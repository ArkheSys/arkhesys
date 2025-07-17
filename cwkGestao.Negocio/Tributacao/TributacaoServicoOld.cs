using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;


namespace cwkGestao.Negocio.Tributacao
{
    public class TributacaoServicoOld
    {
        #region Gera lançamentos de impostos no documento
        public static void Ttest(int idDocGerado)
        {
            Documento documentogerado = DocumentoController.Instancia.LoadObjectById(idDocGerado);
            if (!documentogerado.Pessoa.PessoaClientes[0].BContribuinte) return;
            foreach (var imposto in documentogerado.Nota.NotaImpostoServicos.Where(imp => imp.BaseRetencaoImposto))
            {
                Movimento lancamentoImpostoGerado;
                if ((lancamentoImpostoGerado = GeraLancamentoImposto(documentogerado, imposto)) != null)
                {
                    //DOne: Buscar qual(is) movimento(s) é(são) referente(s) aos serviços
                    Movimento movServico =
                        documentogerado.Movimentos.Where(
                            mov => mov.PlanoConta == documentogerado.Nota.TipoNota.PlanoContaServico).OrderBy(
                                mov => mov.Valor).FirstOrDefault();
                    //TODO?: Subtrair o valor do lancamento imposto gerado desse(s) movimento(s)
                    //if (movServico != null)
                    //   movServico.Valor -= lancamentoImpostoGerado.Valor;

                    //DOne: Adicionar o lancamento gerado e subtrair o valor de imposto do saldo do documento
                    documentogerado.Movimentos.Add(lancamentoImpostoGerado);
                    documentogerado.Saldo -= lancamentoImpostoGerado.Valor;
                    if (documentogerado.Saldo < 0) throw new ArithmeticException("Valor dos impostos maior que a primeira parcela!");
                }
            }


            DocumentoController.Instancia.Salvar(documentogerado, Acao.Alterar);


        }

        private static Movimento GeraLancamentoImposto(Documento documentogerado, NotaImpostoServico imposto)
        {
            /*Movimento lancamento = null;
            DateTime inicioMes = DateTime.Today.AddDays(-DateTime.Today.Day);
            DateTime fimMes = inicioMes.AddDays(DateTime.DaysInMonth(inicioMes.Year, inicioMes.Month));
            decimal valorImposto = 0;
            if (imposto.Imposto.LimiarPorMes > 0)
            {
                decimal totalServicoNoMes = NotaController.Instancia.GetNotasPorClienteNoPeriodo(documentogerado.Pessoa, inicioMes, fimMes).Sum(nota => nota.TotalServico);
                if (totalServicoNoMes >= imposto.Imposto.LimiarPorMes)
                {
                    decimal baseCalculo = Retroativo(documentogerado.Pessoa, inicioMes, fimMes, imposto.Imposto) ? totalServicoNoMes : documentogerado.Nota.NotaItemsServicos.Sum(nis => nis.SubTotal);
                    valorImposto = baseCalculo * imposto.Aliquota / 100;
                }
            }
            else if (imposto.Imposto.LimiarPorVenda > 0)
            {
                if (imposto.BaseCalculo * imposto.Aliquota / 100 >= imposto.Imposto.LimiarPorVenda)
                    valorImposto = imposto.BaseCalculo * imposto.Aliquota / 100;
            }
            if (valorImposto > 0)
            {
                
                lancamento = new Movimento
                {
                    Valor = valorImposto,
                    Tipo = TipoMovimentoType.Imposto,
                    Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento(),
                    Documento = documentogerado,
                    Dt = documentogerado.Dt,
                    Operacao = OperacaoMovimentoType.Lancamento,
                    Sequencia = 1,
                    PlanoConta = imposto.Imposto.PlanoConta
                };
            }
            return lancamento;*/
            Movimento lancamento = new Movimento();
            lancamento.Valor = imposto.Valor;
            lancamento.PlanoConta = imposto.Imposto.PlanoConta;
            lancamento.Tipo = TipoMovimentoType.Imposto;
            lancamento.Documento = documentogerado;
            lancamento.Operacao = OperacaoMovimentoType.Lancamento;
            lancamento.Dt = documentogerado.Nota.Dt;
            lancamento.Sequencia = 1;
            return lancamento;
        }
        #endregion

        #region Calcula Impostos da Nota
        public static void CalculaServicoImpostos(Nota nota, out IList<Nota> precisamAtualizar)
        {
            nota.NotaImpostoServicos.Clear();

            CalculaImpostosQueDependemApenasDoValorDestaNota(nota);

            CalculaImpostosQueDependemDoValorMensalDoCliente(nota, out precisamAtualizar);
        }

        private static void CalculaImpostosQueDependemDoValorMensalDoCliente(Nota nota, out IList<Nota> precisamAtualizar)
        {

            DateTime comecoMes = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            var notasClienteList = NotaController.Instancia.GetNotasPorClienteNoPeriodo(nota.Pessoa, comecoMes, comecoMes.AddMonths(1));
            while (notasClienteList.Remove(nota)) ;
            var notasCliente = notasClienteList.Distinct();

            List<NotaImpostoServico> impostos = new List<NotaImpostoServico>();
            //popula impostos iniciais só dessa nota
            foreach (NotaItemServico servico in nota.NotaItemsServicos.Distinct().Where(nis => nis.Servico.ServicoImpostos.Where(si => si.ServicoImposto.LimiarPorMes > 0).Count() > 0))
            {
                foreach (ServicoServicoImposto imposto in servico.Servico.ServicoImpostos.Distinct().Where(si => si.ServicoImposto.LimiarPorMes > 0))
                {
                    NotaImpostoServico nis =
                        impostos.FirstOrDefault(niss => niss.Imposto.Codigo == imposto.ServicoImposto.Codigo) ?? new NotaImpostoServico();
                    nis.Aliquota = imposto.ServicoImposto.Aliquota;
                    nis.BaseCalculo += servico.SubTotal;
                    nis.Nota = nota;
                    nis.Valor = nis.Aliquota * nis.BaseCalculo / 100;
                    nis.Imposto = imposto.ServicoImposto;
                    impostos.Add(nis);
                }
            }
            bool atualizaTdNotaPraTras = false;
            foreach (NotaImpostoServico notaImpostoServico in impostos)
            {
                ServicoImposto impostoSendoCalculado = notaImpostoServico.Imposto;

                decimal baseCalculoAcumulada = 0;

                foreach (var nota1 in notasCliente)
                    foreach (var imp in nota1.NotaImpostoServicos.Distinct())
                        if ((imp.Imposto.Codigo == impostoSendoCalculado.Codigo) && !imp.BaseRetencaoImposto && nota1.NotaItemsServicos.Distinct().Where(nis => nis.Servico.ServicoImpostos.Where(si => si.ServicoImposto.Codigo == imp.Imposto.Codigo && si.BaseRetencaoImposto).Count() > 0).Count() > 0)
                        {
                            baseCalculoAcumulada += imp.BaseCalculo;
                            imp.BaseRetencaoImposto = true;
                            imp.Valor = 0;
                        }

                notaImpostoServico.BaseCalculo += baseCalculoAcumulada;
                notaImpostoServico.Valor = notaImpostoServico.BaseCalculo * notaImpostoServico.Aliquota / 100;
                if (notaImpostoServico.BaseCalculo >= notaImpostoServico.Imposto.LimiarPorMes && notasCliente.Where(nt => nt.NotaImpostoServicos.Where(ns => ns.Imposto.Codigo == impostoSendoCalculado.Codigo && ns.BaseRetencaoImposto).Count() > 0).Count() > 0)
                {
                    notaImpostoServico.BaseRetencaoImposto = true;
                    atualizaTdNotaPraTras = true;

                }
            }
            if (atualizaTdNotaPraTras)
                precisamAtualizar = notasCliente.ToList();
            else
                precisamAtualizar = new List<Nota>();

            foreach (var notaImpostoServico in impostos)
            {
                nota.NotaImpostoServicos.Add(notaImpostoServico);
            }


        }

        private static void CalculaImpostosQueDependemApenasDoValorDestaNota(Nota nota)
        {

            foreach (NotaItemServico servico in nota.NotaItemsServicos.Where(nis => nis.Servico.ServicoImpostos.Where(si => si.ServicoImposto.LimiarPorVenda > 0).Count() > 0).Distinct())
            {
                foreach (ServicoServicoImposto imposto in servico.Servico.ServicoImpostos.Where(si => si.ServicoImposto.LimiarPorVenda > 0))
                {
                    decimal valorretencao = servico.SubTotal * imposto.ServicoImposto.Aliquota / 100;
                    NotaImpostoServico nisss =
                        nota.NotaImpostoServicos.FirstOrDefault(
                            nis => nis.Imposto.Codigo == imposto.ServicoImposto.Codigo);
                    if (nisss == null)
                        nota.NotaImpostoServicos.Add(new NotaImpostoServico()
                                                     {
                                                         Aliquota = imposto.ServicoImposto.Aliquota,
                                                         BaseCalculo = servico.SubTotal,
                                                         Imposto = imposto.ServicoImposto,
                                                         Nota = nota,
                                                         Valor = valorretencao,
                                                         BaseRetencaoImposto = imposto.BaseRetencaoImposto && (valorretencao >= imposto.ServicoImposto.LimiarPorVenda) //TODO verificar
                                                     });
                    else
                    {
                        if (!nisss.BaseRetencaoImposto && imposto.BaseRetencaoImposto)
                        {
                            nisss.BaseRetencaoImposto = true;
                            nisss.BaseCalculo = 0;
                        }
                        if (imposto.BaseRetencaoImposto)
                        {
                            nisss.BaseCalculo += valorretencao/imposto.ServicoImposto.Aliquota*100;
                            nisss.Valor = nisss.BaseCalculo*nisss.Aliquota/100;
                        }

                    }
                }
            }
        }


        #endregion

    }
}
