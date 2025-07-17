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
    public class FaturadorContratoProduto : IFaturadorContrato<ContratoProduto>
    {
        Contrato objContratoParaFatura;
        IList<ContratoProduto> contratosProdutoAgrupadoParaFatura;

        DateTime pReferencia;

        IList<NotaItem> notaItensContrato;
        Filial filial;
        TabelaPreco tabelaPreco;
        TipoNota tipoNotaProduto;
        DateTime referencia, vencimento;
        string ErrosContratos;

        StringBuilder sbMensagemErroFatura;
        private bool UsarMesAtualParaValidarGeracao = false;

        public IList<ContratoProduto> ListaContratosProduto { get; set; }
        public FaturadorContratoProduto(DateTime pVencimento, Filial pFilial, TabelaPreco pTabelaPreco, TipoNota pTipoNotaProduto, bool Aumentar30Dias, bool UsarMesAtualParaValidarGeracao)
        {
            sbMensagemErroFatura = new StringBuilder();
            this.UsarMesAtualParaValidarGeracao = UsarMesAtualParaValidarGeracao;

            objContratoParaFatura = new Contrato();
            contratosProdutoAgrupadoParaFatura = new List<ContratoProduto>();

            notaItensContrato = new List<NotaItem>();

            vencimento = pVencimento;

            filial = pFilial;
            tabelaPreco = pTabelaPreco;
            tipoNotaProduto = pTipoNotaProduto;

            ListaContratosProduto = new List<ContratoProduto>();
            ListaContratosProduto = BuscarListaContratosProdutoInicial(vencimento, out pReferencia);

            //if (Aumentar30Dias)
                //this.referencia = new DateTime(pReferencia.Year, pReferencia.Month, DateTime.DaysInMonth(pReferencia.Year, pReferencia.Month)).AddMonths(1);

            referencia = pReferencia;

            ListaContratosProduto = this.BuscaContratosParaFaturar(ListaContratosProduto);
        }

        private IList<ContratoProduto> BuscarListaContratosProdutoInicial(DateTime vencimento, out DateTime referencia)
        {
            ListaContratosProduto = (List<ContratoProduto>)ContratoProdutoController.Instancia.GetItensContratoParaFaturar(vencimento, out referencia);
            return ListaContratosProduto;
        }

        public IList<ContratoProduto> BuscaContratosParaFaturar(IList<ContratoProduto> contratosProdutoInicial)
        {
            IList<ContratoProduto> contratosParaFaturar = new List<ContratoProduto>();

            foreach (ContratoProduto contratoProduto in contratosProdutoInicial)
            {
                if (contratoProduto.Contrato.ID == 565)
                {

                }
                IList<ContratoControle> contratosControle = BuscaContratosControle(contratoProduto);
                if (VerificaSeGeraNotaNoMes(referencia, contratosControle) && VerificaNecessidadeFaturamentoNoMesMarcado(contratoProduto))
                {
                    contratoProduto.Vencimento = vencimento;
                    contratosParaFaturar.Add(contratoProduto);
                }
            }

            return contratosParaFaturar;
        }

        private bool VerificaNecessidadeFaturamentoNoMesMarcado(ContratoProduto contratoProduto)
        {
            switch (UsarMesAtualParaValidarGeracao ? DateTime.Now.Month : vencimento.Month)
            {
                case 01: //Janeiro
                    if (contratoProduto.BJaneiro == true)
                    {
                        return true;
                    }
                    return false;
                case 02: //Fevereiro
                    if (contratoProduto.BFevereiro == true)
                    {
                        return true;
                    }
                    return false;
                case 03: //Marco
                    if (contratoProduto.BMarco == true)
                    {
                        return true;
                    }
                    return false;
                case 04: //Abril
                    if (contratoProduto.BAbril == true)
                    {
                        return true;
                    }
                    return false;
                case 05: //Maio
                    if (contratoProduto.BMaio == true)
                    {
                        return true;
                    }
                    return false;
                case 06: //Junho
                    if (contratoProduto.BJunho == true)
                    {
                        return true;
                    }
                    return false;
                case 07: //Julho
                    if (contratoProduto.BJulho == true)
                    {
                        return true;
                    }
                    return false;
                case 08: //Agosto
                    if (contratoProduto.BAgosto == true)
                    {
                        return true;
                    }
                    return false;
                case 09: //Setembro
                    if (contratoProduto.BSetembro == true)
                    {
                        return true;
                    }
                    return false;
                case 10: //Outubro
                    if (contratoProduto.BOutubro == true)
                    {
                        return true;
                    }
                    return false;
                case 11: //Novembro
                    if (contratoProduto.BNovembro == true)
                    {
                        return true;
                    }
                    return false;
                case 12: //Dezembro
                    if (contratoProduto.BDezembro == true)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public bool VerificaSeGeraNotaNoMes(DateTime referencia, IList<ContratoControle> contratosControle)
        {
            return (contratosControle.Count() == 0);
        }

        public StringBuilder Faturar(ref bool sucesso, IList<ContratoProduto> listaContratoProdutoSelecionado)
        {
            ListaContratosProduto = listaContratoProdutoSelecionado;
            GerarFaturas(ref sucesso);

            return sbMensagemErroFatura;
        }

        public StringBuilder Faturar(ref bool sucesso)
        {
            GerarFaturas(ref sucesso);

            return sbMensagemErroFatura;
        }

        public void GerarFaturas(ref bool sucesso)
        {
            Nota objNota;
            NotaBuilder objNotaBuilder;

            IEnumerable<IGrouping<Contrato, ContratoProduto>> listaAgrupadaPorContrato = ListaContratosProduto.GroupBy(s => s.Contrato);

            foreach (IGrouping<Contrato, ContratoProduto> IGroupingContratoProduto in listaAgrupadaPorContrato)
            {
                try
                {
                    objNota = new Nota();
                    objNotaBuilder = new NotaBuilder();

                    contratosProdutoAgrupadoParaFatura = IGroupingContratoProduto.Select(s => s).ToList();

                    objContratoParaFatura = IGroupingContratoProduto.Key;

                    sucesso = GerarNotas(tipoNotaProduto, contratosProdutoAgrupadoParaFatura, objContratoParaFatura, objNota, objNotaBuilder, vencimento);

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

        private string MontaMensagemErroContrato(Contrato objContrato)
        {
            return "Não foi possível faturar o Contrato " + objContrato.Codigo;
        }

        public IList<ContratoControle> BuscaContratosControle(ContratoProduto itemContratos)
        {
            IList<ContratoControle> contratosControle = new List<ContratoControle>();

            contratosControle = ContratoControleController.Instancia.GetRegistrosContratosFaturados(itemContratos.Contrato.ID, 0, itemContratos.ID);
            contratosControle = FiltraContratosControlePorDataGerada(itemContratos, contratosControle);

            return contratosControle;
        }

        private IList<ContratoControle> FiltraContratosControlePorDataGerada(ContratoProduto itemContratos, IList<ContratoControle> contratosControle)
        {
            contratosControle = contratosControle == null ? new List<ContratoControle>() : contratosControle;
            contratosControle = contratosControle.Where(contratoControle => ValidaDatasContratoControle(referencia, contratoControle)).ToList();
            return contratosControle;
        }

        private static bool ValidaDatasContratoControle(DateTime referencia, ContratoControle contratoControle)
        {
            if (contratoControle.MesGerado.Year == referencia.Year)
            {

            }
            return ((contratoControle.MesGerado.Month == referencia.Month) && (contratoControle.MesGerado.Year == referencia.Year));
        }

        public void SalvarContratosControle(IList<ContratoProduto> itensContrato, Nota nota)
        {
            for (int i = 0; i < itensContrato.Count; i++)
            {
                ContratoControleController.Instancia.Salvar(new ContratoControle
                {
                    Contrato = itensContrato[i].Contrato,
                    MesGerado = referencia,
                    bServico = false,
                    ContratoProduto = itensContrato[i],
                    NotaItem = nota.NotaItems[i],
                    NotaItemServico = null
                }, Acao.Incluir);
            }
        }

        private bool GerarNotas(TipoNota tipoNotaPadrao, IList<ContratoProduto> itensContrato, Contrato contrato, Nota nota, NotaBuilder notaBuilder, DateTime? dataVencimento)
        {
            Boolean retorno = true;
            try
            {
                notaBuilder = IniciandoNotaProduto(out nota, contrato, vencimento, false, tipoNotaProduto);
                notaItensContrato = PegaNotaItens(itensContrato, notaBuilder);

                nota.DtEnvio = DateTime.Now;
                ErrosContratos = "";

                retorno = FinalizandoNotaProduto(contrato, nota, notaBuilder, ErrosContratos, dataVencimento.Value);

                SalvarContratosControle(itensContrato, nota);
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
                retorno = false;
            }

            return retorno;
        }

        private NotaBuilder IniciandoNotaProduto(out Nota nota, Contrato contrato, DateTime vencimento, bool p, TipoNota tipoDaNota)
        {
            nota = new Nota();

            Condicao condicao = CondicaoController.Instancia.LoadObjectById(contrato.Condicao.ID);
            Contrato contratoCarregado = ContratoController.Instancia.LoadObjectById(contrato.ID);


            NotaBuilder notaBuilder = new NotaBuilder(nota);
            notaBuilder.PreencherDadosBasicosNota(filial,
                                                  vencimento, String.Empty, String.Empty, String.Empty, null, TipoFrete.SemFrete);
            notaBuilder.PreencherNotaComTipoNota(tipoDaNota);
            nota.Status = "-1";
            notaBuilder.PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(contratoCarregado.Pessoa, condicao, contratoCarregado.PessoaVendedor, tabelaPreco, null);

            return notaBuilder;
        }

        private IList<NotaItem> PegaNotaItens(IList<ContratoProduto> produtosDoContrato, NotaBuilder notaBuilder)
        {
            IList<NotaItem> notaItensRetorno = notaBuilder.IncluiNotaItem(produtosDoContrato);
            foreach (var item in notaItensRetorno)
            {
                bool dentroDoEstado = notaBuilder.Nota.Filial.Cidade.UF.Sigla == notaBuilder.Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
                if (dentroDoEstado == true && item.Produto.CFOP != null)
                {
                    item.CFOP = item.Produto.CFOP;
                }
                else if (dentroDoEstado == false && item.Produto.CFOPForaDoEstado != null)
                {
                    item.CFOP = item.Produto.CFOPForaDoEstado;
                }
            }
            return notaItensRetorno;
        }

        private static bool FinalizandoNotaProduto(Contrato objContrato, Nota nota, NotaBuilder notaBuilder, string Erros, DateTime referencia)
        {
            DateTime dataFaturamento = DateTime.Now;
            if (nota.NotaItems.Count() == 0)
            {
                return false;
            }
            else
            {
                notaBuilder.SetTotaisNota();
                CondicaoController.Instancia.GetContrato(objContrato);
                CondicaoController.Instancia.GetDadosFaturamento(referencia);
                var condicaoCarregada = CondicaoController.Instancia.GerarParcelas(nota);

                foreach (var parcela in condicaoCarregada)
                {
                    nota.NotaParcelas.Add(parcela);
                }

                NotaController.Instancia.RegisterNewIntoTransaction(nota);

                if (nota.BGeraFinanceiro)
                {
                    NotaController.Instancia.GetContrato(objContrato);
                    NotaController.Instancia.GetDadosFaturamento(referencia);
                    NotaController.Instancia.GeraFinanceiro(nota, null);
                }
                nota.Dt = nota.DtEnvio == null ? DateTime.Now : (DateTime)nota.DtEnvio;


                bool dentroDoEstado = nota.Filial.Cidade.UF.Sigla == nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
                if (dentroDoEstado)
                {
                    nota.idDest = 1;
                }
                else
                {
                    if ((nota.Filial.Cidade.UF.Sigla != "EX") &&
                        (nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla == "EX") ||
                        (nota.Filial.Cidade.UF.Sigla == "EX") &&
                        (nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla != "EX"))
                    {
                        nota.idDest = 3;
                    }
                    else
                    {
                        nota.idDest = 2;
                    }
                }

                MontaObservacaoSistema(ref nota);
                if (!String.IsNullOrEmpty(objContrato.Observacao))
                {
                    if (!String.IsNullOrEmpty(nota.ObservacaoUsuario))
                    {
                        nota.ObservacaoUsuario += " - ";
                    }
                    nota.ObservacaoUsuario += objContrato.Observacao;
                }
                NotaController.Instancia.CommitUnitOfWorkTransaction();

                return true;
            }
        }

        private static void MontaObservacaoSistema(ref Nota nota)
        {
            string observacao;
            NotaController.Instancia.MontarObservacao(nota, out observacao);
            nota.ObservacaoSistema = observacao;
        }


    }
}
