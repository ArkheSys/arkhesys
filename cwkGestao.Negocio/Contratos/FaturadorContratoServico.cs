using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System.Collections;
using cwkGestao.Negocio.Faturamento;

namespace cwkGestao.Negocio.Contratos
{
    public class FaturadorContratoServico : IFaturadorContrato<ContratoServico>
    {
        private DateTime referencia;

        private DateTime vencimento;
#pragma warning disable CS0169 // O campo "FaturadorContratoServico.contratosServico" nunca é usado
        private IList<ContratoServico> contratosServico;
#pragma warning restore CS0169 // O campo "FaturadorContratoServico.contratosServico" nunca é usado
        IList<ContratoServico> contratosServicoAgrupadoParaFatura;
#pragma warning disable CS0169 // O campo "FaturadorContratoServico.notaItensContrato" nunca é usado
        IList<NotaItem> notaItensContrato;
#pragma warning restore CS0169 // O campo "FaturadorContratoServico.notaItensContrato" nunca é usado
        private TipoNota tipoDaNotaServico;
        string ErrosContratos;
        StringBuilder sbMensagemErroFatura;
        private ContratoServico itemServ;
        Contrato objContratoParaFatura;
        Int32 diaVencimento;
        public bool diav;
        public bool GerarComDataDoDia;
        public IList<ContratoServico> ListaContratosServico { get; set; }
        public FaturadorContratoServico(string referencia, DateTime vencimento, TipoNota tipoNotaServico, bool diaVenc, bool Aumentar30Dias, bool GerarComDataDoDia)
        {
            sbMensagemErroFatura = new StringBuilder();
            //this.referencia = Convert.ToDateTime("01/" + referencia);
            this.vencimento = vencimento;
            this.tipoDaNotaServico = tipoNotaServico;
            if (diaVenc == true)
            {
                diaVencimento = Convert.ToInt32(vencimento.Day);
                this.referencia = Convert.ToDateTime("01/" + referencia); //Convert.ToDateTime(vencimento);
            }
            else
            {
                diaVencimento = 0;

                var Ref = Convert.ToDateTime("01/" + referencia);
                this.referencia = Ref;
                if (Aumentar30Dias)
                    this.referencia = new DateTime(Ref.Year, Ref.Month, DateTime.DaysInMonth(Ref.Year, Ref.Month)).AddMonths(1);
            }
            diav = diaVenc;
            this.GerarComDataDoDia = GerarComDataDoDia;
            ListaContratosServico = new List<ContratoServico>();
            ListaContratosServico = BuscarListaContratosServicoInicial(this.referencia, diaVencimento);
            ListaContratosServico = BuscaContratosParaFaturar(ListaContratosServico);

            //ListaContratosServico = BuscaContratosParaFaturar(ListaContratosServico);
        }

        private bool VerificaNecessidadeFaturamentoNoMesMarcado(ContratoServico contratoServico)
        {
            switch (referencia.Month)
            {
                case 01: //Janeiro
                    if (contratoServico.BJaneiro == true)
                    {
                        return true;
                    }
                    return false;
                case 02: //Fevereiro
                    if (contratoServico.BFevereiro == true)
                    {
                        return true;
                    }
                    return false;
                case 03: //Marco
                    if (contratoServico.BMarco == true)
                    {
                        return true;
                    }
                    return false;
                case 04: //Abril
                    if (contratoServico.BAbril == true)
                    {
                        return true;
                    }
                    return false;
                case 05: //Maio
                    if (contratoServico.BMaio == true)
                    {
                        return true;
                    }
                    return false;
                case 06: //Junho
                    if (contratoServico.BJunho == true)
                    {
                        return true;
                    }
                    return false;
                case 07: //Julho
                    if (contratoServico.BJulho == true)
                    {
                        return true;
                    }
                    return false;
                case 08: //Agosto
                    if (contratoServico.BAgosto == true)
                    {
                        return true;
                    }
                    return false;
                case 09: //Setembro
                    if (contratoServico.BSetembro == true)
                    {
                        return true;
                    }
                    return false;
                case 10: //Outubro
                    if (contratoServico.BOutubro == true)
                    {
                        return true;
                    }
                    return false;
                case 11: //Novembro
                    if (contratoServico.BNovembro == true)
                    {
                        return true;
                    }
                    return false;
                case 12: //Dezembro
                    if (contratoServico.BDezembro == true)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public IList<ContratoServico> BuscaContratosParaFaturar(IList<ContratoServico> contratosServicoInicial)
        {
            IList<ContratoServico> contratosParaFaturar = new List<ContratoServico>();

            foreach (ContratoServico contratoServico in contratosServicoInicial)
            {
                IList<ContratoControle> contratosControle = BuscaContratosControle(contratoServico);
                if (VerificaSeGeraNotaNoMes(referencia, contratosControle) && VerificaNecessidadeFaturamentoNoMesMarcado(contratoServico))
                {
                    if (diaVencimento != 0)
                    {
                        contratoServico.DiaVencimento = this.vencimento.Day;

                    }
                    contratosParaFaturar.Add(contratoServico);
                }
            }

            return contratosParaFaturar;
        }
        private IList<ContratoServico> BuscarListaContratosServicoInicial(DateTime referencia, Int32 diaVencimento)
        {
            ListaContratosServico = (List<ContratoServico>)ContratoServicoController.Instancia.GetContratosParaFaturar(referencia, diaVencimento);
            return ListaContratosServico;

        }
        public void GerarFaturas(ref bool sucesso)
        {
            PrepararDados(ref sucesso);
        }
        public StringBuilder Faturar(ref bool sucesso, IList<ContratoServico> listaContratoServicoSelecionado)
        {
            ListaContratosServico = listaContratoServicoSelecionado;
            GerarFaturas(ref sucesso);

            return sbMensagemErroFatura;
        }

        private string MontaMensagemErroContrato(Contrato objContrato)
        {
            return "Não foi possível faturar o Contrato " + objContrato.Codigo;
        }

        private void PrepararDados(ref bool sucesso)
        {
            objContratoParaFatura = new Contrato();
            IEnumerable<IGrouping<Contrato, ContratoServico>> listaAgrupadaPorContrato = ListaContratosServico.GroupBy(s => s.Contrato);

            foreach (IGrouping<Contrato, ContratoServico> IGroupingContratoServico in listaAgrupadaPorContrato)
            {
                try
                {
                    contratosServicoAgrupadoParaFatura = IGroupingContratoServico.Select(s => s).ToList();
                    sucesso = GerarFaturasServicos(contratosServicoAgrupadoParaFatura, sucesso);

                    if (!sucesso)
                    {
                        sbMensagemErroFatura.AppendLine(MontaMensagemErroContrato(objContratoParaFatura));
                    }
                }
                catch (Exception)
                {
                    sbMensagemErroFatura.AppendLine(MontaMensagemErroContrato(objContratoParaFatura));
                }
            }
        }

        private bool GerarFaturasServicos(IList<ContratoServico> contratosServ, bool sucesso)
        {
            bool validador = false;
            bool existeNotaServicoNoMes = false;
            TipoNota tipoNotaPadrao;

            try
            {
                IEnumerable<IGrouping<Contrato, ContratoServico>> listaAgrupadaPorContrato = ListaContratosServico.GroupBy(s => s.Contrato);
                foreach (var itemContratos in contratosServ)
                {
                    IList<ContratoControle> contratosControle = BuscaContratosControle(itemContratos);
                    existeNotaServicoNoMes = VerificaSeGeraNotaNoMes(referencia, contratosControle);

                    if (existeNotaServicoNoMes)
                    {
                        Nota nota = new Nota();
                        nota.TipoNota = tipoDaNotaServico;
                        NotaBuilder notaBuilder = new NotaBuilder(nota);
                        ErrosContratos = "";

                        DateTime? dataVencimento = null;

                        VerificaMesesDeFaturamento(itemContratos.ID, ref dataVencimento, ref validador);

                        if (!diav)
                        {
                            dataVencimento = new DateTime(referencia.Year, referencia.Month, itemContratos.DiaVencimento);
                        }

                        if (GerarComDataDoDia)
                            dataVencimento = DateTime.Now;

                        if ((validador == true) && (ErrosContratos == ""))
                        {

                            if (itemContratos.Contrato.TipoNota != null)
                                tipoNotaPadrao = itemContratos.Contrato.TipoNota;
                            else
                                tipoNotaPadrao = tipoDaNotaServico;

                            sucesso = GerarNotas(sucesso, tipoNotaPadrao, itemContratos, nota, notaBuilder, dataVencimento);
                            SalvarContratosControle(itemContratos, nota);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sucesso = false;
                throw new Exception("Erros: " + ErrosContratos + " ou " + ex);
            }
            return sucesso;
        }


        #region Verificação de Datas

        public bool VerificaSeGeraNotaNoMes(DateTime dataDaNota, IList<ContratoControle> contratosControle)
        {
            return (contratosControle.Count() == 0);
            //bool retorno = false;

            //foreach (var contratoControle in contratosControle)
            //{
            //    if ((contratoControle.MesGerado.Month == dataDaNota.Month) &&
            //        (contratoControle.MesGerado.Year == dataDaNota.Year))
            //    {
            //        retorno = true;
            //    }
            //}
            //return retorno;
        }

        public void VerificaMesesDeFaturamento(int idItemContrato, ref DateTime? dtVencimento, ref bool validador)
        {
            itemServ = new ContratoServico();

            itemServ = ContratoServicoController.Instancia.LoadObjectById(idItemContrato);
            VerificaNecessidadeFaturamentoNoMesMarcado(ref dtVencimento, ref validador);
            if (dtVencimento == null)
                ErrosContratos += "\nCódigo: " + itemServ.ID + "\n";
        }

        private void VerificaNecessidadeFaturamentoNoMesMarcado(ref DateTime? dtVencimento, ref bool validador)
        {
            switch (referencia.Month)
            {
                case 01: //Janeiro
                    if (itemServ.BJaneiro == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 02: //Fevereiro
                    if (itemServ.BFevereiro == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 03: //Marco
                    if (itemServ.BMarco == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 04: //Abril
                    if (itemServ.BAbril == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 05: //Maio
                    if (itemServ.BMaio == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 06: //Junho
                    if (itemServ.BJunho == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 07: //Julho
                    if (itemServ.BJulho == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 08: //Agosto
                    if (itemServ.BAgosto == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 09: //Setembro
                    if (itemServ.BSetembro == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 10: //Outubro
                    if (itemServ.BOutubro == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 11: //Novembro
                    if (itemServ.BNovembro == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
                case 12: //Dezembro
                    if (itemServ.BDezembro == true)
                    {
                        dtVencimento = vencimento;
                        validador = true;
                    }
                    break;
            }
        }

        #endregion

        #region GerarNotas

        public bool GerarNotas(bool sucesso, TipoNota tipoNotaPadrao, ContratoServico itemContratos, Nota nota, NotaBuilder notaBuilder, DateTime? dataVencimento)
        {
            bool faturaServicoMesSeguinte = FaturarMesSeguinte(itemContratos.ID);

            IniciandoNotaServico(itemContratos.ID, notaBuilder, dataVencimento, true, tipoNotaPadrao);

            SetandoValoresNotaServico(itemContratos, nota);

            foreach (var item in nota.NotaImpostoServicos.Where(x => x.Imposto.LimiarPorVenda < itemContratos.Valor))
            {
                if (item.Imposto.TipoRetencao == TipoRetencao.ValorMinimo)
                    item.BaseRetencaoImposto = true;
            }

            FinalizandoNotaServico(itemContratos, nota, notaBuilder, ErrosContratos, ref sucesso, faturaServicoMesSeguinte, referencia, this.diav, this.vencimento, GerarComDataDoDia);

            return sucesso;
        }

        private bool FaturarMesSeguinte(int idContratoServico)
        {
            try
            {
                ContratoServico contratoServico = ContratoServicoController.Instancia.LoadObjectById(idContratoServico);
                if (contratoServico.DiaVencimento < DateTime.Today.Day)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void IniciandoNotaServico(int idItemContrato, NotaBuilder notaBuilder, DateTime? dataVencimento, bool pServico, TipoNota tipoNota)
        {
            Contrato contrato = new Contrato();
            ContratoServico servicoContrato = new ContratoServico();
            ContratoProduto produtoContrato = new ContratoProduto();
            servicoContrato = ContratoServicoController.Instancia.LoadObjectById(idItemContrato);
            contrato = servicoContrato.Contrato;
            Condicao condicao = CondicaoController.Instancia.LoadObjectById(contrato.Condicao.ID);
            Contrato contratoCarregado = ContratoController.Instancia.LoadObjectById(contrato.ID);
            contratoCarregado.TipoNota = tipoNota;
            notaBuilder.PreencherDadosBasicosNota(FilialController.Instancia.GetFilialPrincipal(), dataVencimento.GetValueOrDefault(),
                                                  String.Empty, String.Empty, String.Empty, null, TipoFrete.SemFrete);
            contratoCarregado.TipoNota = TipoNotaController.Instancia.LoadObjectById(contratoCarregado.TipoNota.ID);
            notaBuilder.PreencherNotaComTipoNota(contratoCarregado.TipoNota);
            notaBuilder.PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(contratoCarregado.Pessoa, condicao, contratoCarregado.PessoaVendedor, null, null);
        }

        private static void SetandoValoresNotaServico(ContratoServico itemContratos, Nota nota)
        {
            var notaServicoItem = new NotaItemServico();
            var notaImpostoServico = new NotaImpostoServico();

            notaServicoItem.Servico = itemContratos.Servico;
            notaServicoItem.NomeServico = itemContratos.Servico.Nome;
            notaServicoItem.Quantidade = itemContratos.Quantidade;
            notaServicoItem.Valor = itemContratos.Valor;
            notaServicoItem.SubTotal = itemContratos.Total;
            notaServicoItem.PercDesconto = itemContratos.PercDesconto;
            notaServicoItem.Nota = nota;
            foreach (var imposto in notaServicoItem.Servico.ServicoImpostos)
            {
                notaImpostoServico.Aliquota = imposto.Aliquota;
                notaImpostoServico.AltData = imposto.AltData;
                notaImpostoServico.AltUsuario = imposto.AltUsuario;
                notaImpostoServico.BaseCalculo = notaServicoItem.SubTotal;
                notaImpostoServico.BaseRetencaoImposto = imposto.BaseRetencaoImposto;
                notaImpostoServico.Imposto = ServicoImpostoController.Instancia.LoadObjectById(imposto.ServicoImposto.ID);
                notaImpostoServico.IncData = imposto.IncData;
                notaImpostoServico.IncUsuario = imposto.IncUsuario;
                notaImpostoServico.Nota = nota;
                notaImpostoServico.Valor = (imposto.Aliquota * notaServicoItem.SubTotal) / 100;
                nota.NotaImpostoServicos.Add(notaImpostoServico);
                notaImpostoServico = new NotaImpostoServico();
            }
            nota.NotaItemsServicos.Add(notaServicoItem);

            nota.Status = "-1";
        }

        private static void FinalizandoNotaServico(ContratoServico itemContratos, Nota nota, NotaBuilder notaBuilder, string Erros, ref bool sucesso, bool faturarServicoMesSeguinte, DateTime referencia, bool diav, DateTime vencimento, bool gerarComDataDoDia)
        {
            if (!diav)
                vencimento = new DateTime(referencia.Year, referencia.Month, itemContratos.DiaVencimento);

            if (gerarComDataDoDia)
                vencimento = DateTime.Now;

            if (nota.NotaItemsServicos.Count() == 0)
            {
                if (Erros == "")
                {
                    Erros += "\nCódigo: " + itemContratos.ID + "\n";
                }
            }
            else
            {
                notaBuilder.SetTotaisNota();
                CondicaoController.Instancia.GetContrato(itemContratos.Contrato);
                CondicaoController.Instancia.GetContratoServico(itemContratos);
                CondicaoController.Instancia.GetDadosFaturamento(referencia, diav, vencimento);
                var condicaoCarregada = CondicaoController.Instancia.GerarParcelas(nota);

                foreach (var parcela in condicaoCarregada)
                {
                    nota.NotaParcelas.Add(parcela);
                }

                NotaController.Instancia.RegisterNewIntoTransaction(nota);

                if (nota.BGeraFinanceiro)
                {
                    NotaController.Instancia.GetContrato(itemContratos.Contrato);
                    NotaController.Instancia.GetContratoServico(itemContratos);
                    NotaController.Instancia.GetDadosFaturamento(referencia, diav, vencimento);
                    NotaController.Instancia.GeraFinanceiro(nota, null);
                }

                nota.Dt = DateTime.Now;//vencimento;
                nota.DtEnvio = vencimento;
                if (!String.IsNullOrEmpty(itemContratos.Contrato.Observacao))
                {
                    if (!String.IsNullOrEmpty(nota.ObservacaoUsuario))
                        nota.ObservacaoUsuario += " - ";
                    nota.ObservacaoUsuario += itemContratos.Contrato.Observacao;
                }
                NotaController.Instancia.CommitUnitOfWorkTransaction();

                ContratoServicoController.Instancia.Salvar(itemContratos, Acao.Alterar);
                sucesso = true;

                CondicaoController.Instancia.Dispose();
            }
        }

        #endregion

        #region Contrato Controle

        public IList<ContratoControle> BuscaContratosControle(ContratoServico itemContratos)
        {
            IList<ContratoControle> contratosControle = new List<ContratoControle>();
            contratosControle = ContratoControleController.Instancia.GetRegistrosContratosFaturados(itemContratos.Contrato.ID, 1, itemContratos.ID);
            contratosControle = FiltraContratosControlePorDataGerada(itemContratos, contratosControle);
            return contratosControle;
        }
        private IList<ContratoControle> FiltraContratosControlePorDataGerada(ContratoServico itemContratos, IList<ContratoControle> contratosControle)
        {
            contratosControle = contratosControle == null ? new List<ContratoControle>() : contratosControle;
            contratosControle = contratosControle.Where(contratoControle => ValidaDatasContratoControle(referencia, contratoControle)).ToList();
            return contratosControle;
        }
        private static bool ValidaDatasContratoControle(DateTime referencia, ContratoControle contratoControle)
        {
            return ((contratoControle.MesGerado.Month == referencia.Month) && (contratoControle.MesGerado.Year == referencia.Year));
        }
        public void SalvarContratosControle(ContratoServico itemContratos, Nota nota)
        {
            foreach (var notaItemServico in nota.NotaItemsServicos)
            {
                ContratoControleController.Instancia.Salvar(new ContratoControle
                {
                    Contrato = itemContratos.Contrato,
                    MesGerado = this.referencia, //vencimento,
                    bServico = true,
                    ContratoServico = itemContratos,
                    ContratoProduto = null,
                    NotaItem = null,
                    NotaItemServico = notaItemServico
                }, Acao.Incluir);
            }
        }

        #endregion

    }
}
