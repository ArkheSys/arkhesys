using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using cwkGestao.Modelo.Auxiliares;
using Cwork.Utilitarios.Funcoes.Sintegra;
using cwkGestao.Repositorio.cwkGestaoFileSystem;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Financeiro;
using System.Xml;
using System.IO;
using System.Reflection;

namespace cwkGestao.Negocio
{
    public partial class NotaController : BaseController<Nota>
    {
        #region Singleton
        private static NotaController instance;
        private static IRepositorioNota repositorioNota;

        private NotaController()
        { }

        static NotaController()
        {
            instance = new NotaController();
            repositorioT = RepositorioFactory<Nota>.GetRepositorio();
            repositorioNota = (IRepositorioNota)repositorioT;
        }

        public static NotaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        private bool temContrato = false;
        private bool faturaMesSeguinte = false;
        private Contrato contrato = new Contrato();
        private ContratoServico servicoContrato = new ContratoServico();
        private bool ehServico = false;

        private DateTime referenciaFaturamento { get; set; }

        public override Dictionary<string, string> ValidaObjeto(Nota objeto)
        {
            return new Dictionary<string, string>();
        }

        private static string ns = " { http://www.portalfiscal.inf.br/nfe } ";

        public Nota GetByPedido(Pedido pedido)
        {
            return repositorioNota.GetByPedido(pedido);
        }

        public bool DadosPedidoFaturarValidos(cwkGestao.Modelo.Pessoa objPessoa, out string mensagem)
        {
            StringBuilder str = new StringBuilder();
            bool ret = true;
            if (objPessoa.PessoaEnderecos.Where(end => end.BPrincipal == true).Count() == 0)
            {
                str.AppendLine("-Pessoa sem endereço.");
                ret = false;
            }
            else
            {
                var objEndereco = objPessoa.PessoaEnderecos.First(end => end.BPrincipal == true);
                if (objEndereco.Cidade.UF.Sigla != "EX")
                {
                    if (string.IsNullOrEmpty(objEndereco.Endereco))
                    {
                        str.AppendLine("-Rua não foi informada.");
                        ret = false;
                    }

                    if (string.IsNullOrEmpty(objEndereco.Numero))
                    {
                        str.AppendLine("-Número do Endereço não foi informado.");
                        ret = false;
                    }

                    if (string.IsNullOrEmpty(objEndereco.Bairro))
                    {
                        str.AppendLine("-Bairro não foi informado.");
                        ret = false;
                    }
                }

                if (objEndereco.Cidade != null)
                {
                    if (string.IsNullOrEmpty(objEndereco.Cidade.Nome))
                    {
                        str.AppendLine("-Cidade não foi informada.");
                        ret = false;
                    }
                    if (string.IsNullOrEmpty(objEndereco.Cidade.UF.Sigla))
                    {
                        str.AppendLine("-UF não foi informada.");
                        ret = false;
                    }

                    if (objEndereco.Cidade.IBGE != "9999999" && !String.IsNullOrEmpty(objPessoa.CNPJ_CPF))
                    {
                        string erro;
                        if (objPessoa.TipoPessoa == "Física")
                        {
                            if (!ValidarCPF(objPessoa.CNPJ_CPF, out erro))
                            {
                                str.AppendLine(erro);
                                ret = false;
                            }
                        }
                        else
                        {
                            if (!ValidarCNPJ(objPessoa.CNPJ_CPF, out erro))
                            {
                                str.AppendLine(erro);
                                ret = false;
                            }
                        }
                    }
                }
                else
                {
                    str.AppendLine("-Cidade não foi informada.");
                    ret = false;
                }
            }
            mensagem = str.ToString();
            return ret;
        }

        public IList<pxMargemLucroProduto> GetMargemdeLucroProdutos(DateTime dataInicial, DateTime dataFinal, int IDEmpresa, string ativos, string modeloDocumento,
            string idProduto)
        {
            return repositorioNota.GetMargemdeLucroProdutos(dataInicial, dataFinal, IDEmpresa, ativos, modeloDocumento, idProduto);
        }

        public Nota GetByNumeroETipoNota(int numero, int tipoNota)
        {
            return repositorioNota.GetByNumeroETipoNota(numero, tipoNota);
        }

        public int GetByIdOSOrdemServico(int idNota)
        {
            return repositorioNota.GetByIdOSOrdemServico(idNota);
        }

        public int GetByIdOSOrdemServicoProduto(int idNota)
        {
            return repositorioNota.GetByIdOSOrdemServicoProduto(idNota);
        }

        public int GetByIdNotaPorIdOSServico(int idOSOrdemServico)
        {
            return repositorioNota.GetByIdNotaPorIdOSServico(idOSOrdemServico);
        }

        public string ExibeTotalTributosEmpresaSimples(ref Nota nota, ref string observacao)
        {
            try
            {
                string tagTotalImposto = "<tagtotalimposto>";
                string tagPercTotalImposto = "<tagpercentualimposto>";
                string textoTributo = nota.Filial.TextoTributo;
                decimal totalImpostoDec = Decimal.Round((nota.TotalNota * (nota.Filial.PercentualImpostoTributo / 100)), 2);
                if ((nota.TipoNota.ExibirTextoTotalTrib == true) && !String.IsNullOrEmpty(textoTributo))
                {
                    var textoTributoComValor = textoTributo.Replace(tagTotalImposto, Convert.ToString(totalImpostoDec)).Replace(tagPercTotalImposto, Convert.ToString(nota.Filial.PercentualImpostoTributo));

                    observacao = nota.ObservacaoSistema = textoTributoComValor;
                }
                else
                {
                    observacao = nota.ObservacaoSistema == null ? String.Empty : nota.ObservacaoSistema;
                }
                return nota.ObservacaoSistema;
            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
            {
                return observacao;
            }
        }

        public bool DadosNfeValidos(cwkGestao.Modelo.Pessoa objPessoa, int IDUFEmpresa, int? modeloDocto, out string mensagem, out bool possuiAviso)
        {
            bool ret = true;
            mensagem = String.Empty;
            possuiAviso = false;
            StringBuilder str = new StringBuilder();
            cwkGestao.Modelo.PessoaEndereco objEndereco = objPessoa.PessoaEnderecos.Where(e => e.BPrincipal == true).First();
            //Tipo Nfe: 0 = Total,  1 = Parcial
            string tipoNfe = ConfiguracaoLocalRepositorio.GetTipoNfe(ConfiguracaoLocalRepositorio.GetArquivoConfiguracao());
            if (modeloDocto == 55)
            {
                if (String.IsNullOrEmpty(objEndereco.Cidade.IBGE))
                {
                    str.AppendLine("-Preencha o código IBGE para a cidade (" + objEndereco.Cidade.Codigo + ") " + objEndereco.Cidade.Nome + ".");
                    ret = false;
                }
                else if (objEndereco.Cidade.IBGE.Length != 7)
                {
                    str.AppendLine("-O código IBGE para a cidade (" + objEndereco.Cidade.Codigo + ") " + objEndereco.Cidade.Nome + " é inválido.");
                    ret = false;
                }

                string ValidaIE = ConfiguracaoLocalRepositorio.GetValidaIE(ConfiguracaoLocalRepositorio.GetArquivoConfiguracao());

                if (objPessoa.TipoPessoa != "Física" && ValidaIE == "1")
                {
                    if (String.IsNullOrEmpty(objPessoa.Inscricao))
                    {
                        str.AppendLine("-Preencha a Inscrição Estadual do cliente (" + objPessoa.Codigo + ") " + objPessoa.Nome + ".");
                        ret = false;
                    }
                    else if (!InscricaoEstadual.InscricaoEstadualValida(objPessoa.Inscricao, objEndereco.Cidade.UF.Sigla))
                    {

                        str.AppendLine("-A Inscrição Estadual do cliente (" + objPessoa.Codigo + ") " + objPessoa.Nome + " é inválida.");
                        ret = false;
                    }
                }

                if (tipoNfe == "1")
                {
                    if (!objPessoa.BOrgaoPublico && objEndereco.Cidade.UF.ID == IDUFEmpresa)
                    {
                        str.AppendLine("A Nfe está configurada como adesão parcial no sistema."
                                + Environment.NewLine + "Vendas dentro do estado para clientes que não são orgãos públicos não podem emitir Nfe.");
                        ret = false;
                    }
                }
            }
            else if (tipoNfe == "1" && objEndereco.Cidade.UF.ID != IDUFEmpresa)
            {
                str.AppendLine("-A Nfe está configurada como adesão parcial no sistema."
                            + Environment.NewLine + "O modelo de documento para vendas fora do estado deve ser 55 (NFe).");
                ret = false;
            }
            mensagem = str.ToString();
            return ret;
        }

        public bool ValidarCNPJ(string pCNPJ, out string pErro)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;
            string digito;
            string tempCnpj;
            string cnpj;

            cnpj = pCNPJ;

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
            cnpj = cnpj.Trim();

            if (cnpj.Length != 14)
            {
                if (cnpj.Length == 0)
                {
                    pErro = "CNPJ não pode ficar sem valor.";
                    return false;
                }
                else
                {
                    if (cnpj.Length != 0)
                    {
                        pErro = "CNPJ Inválido.";
                        return false;
                    }
                }
            }
            else
            {
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                }

                resto = (soma % 11);
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                }

                resto = (soma % 11);
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = digito + resto.ToString();

                if (cnpj.EndsWith(digito))
                {
                    pErro = "";
                }
                else
                {
                    pErro = "CNPJ Inválido.";
                    return false;
                }
            }

            pErro = "";
            return true;
        }

        public bool ValidarCPF(string pCPF, out string pErro)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            string cpf;
            int soma;
            int resto;

            cpf = pCPF;

            cpf = cpf.Replace(".", "").Replace("-", "").Replace("_", "");
            cpf = cpf.Trim();

            if (cpf.Length != 11)
            {
                if (cpf.Length == 0)
                {
                    pErro = "CPF não pode ficar sem valor.";
                    return false;
                }
                else
                {
                    if (cpf.Length != 0)
                    {
                        pErro = "CPF Inválido.";
                        return false;
                    }
                }
            }
            else
            {
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }

                resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }
                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }
                digito = digito + resto.ToString();

                if (cpf.EndsWith(digito))
                {
                    pErro = "";
                }
                else
                {
                    pErro = "CPF Inválido.";
                    return false;
                }
            }

            pErro = "";
            return true;
        }

        public Int32 NovoNumeroSerie(Nota nota, int? Serie)
        {
            if (nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Produtos)
            {
                return repositorioNota.GetNovoNumeroSerie(nota, Serie) + 1;
            }
            else if (nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos)
            {
                return repositorioNota.GetNovoNumeroNFSe(nota) + 1;
            }
            else
            {
                return repositorioNota.GetNovoNumeroSerie(nota, Serie) + 1;
            }
        }

        public Documento GeraFinanceiroEGrava(Nota nota)
        {
            var ret = GeraFinanceiro(nota, null);
            DocumentoController.Instancia.CommitUnitOfWorkTransaction();
            return ret;
        }

        public void GetContrato(Contrato pContrato)
        {
            contrato = pContrato;
            temContrato = true;
        }

        public void GetContratoServico(ContratoServico contratoServico)
        {
            servicoContrato = contratoServico;
            ehServico = true;
        }

        public void GetDadosFaturamento(DateTime referencia, bool diav, DateTime dataVenc)
        {
            if (diav == false)
            {
                referenciaFaturamento = referencia;
            }
            else
            {
                referenciaFaturamento = dataVenc;
            }
        }

        public void GetDadosFaturamento(DateTime referencia)
        {
            referenciaFaturamento = referencia;
        }

        public Documento GeraFinanceiro(Nota nota, List<pxyFormaPagamentoBaixaDoc> formasDePagamento)
        {
            Configuracao conf = ConfiguracaoController.Instancia.GetConfiguracao();
            Documento documento = new Documento();

            List<FinanceiroParcela> listaParcelas = GeraParcelasFinanceiro(nota);
            String mensagem = "";
            List<FinanceiroPlanoContas> listaPlanoContas = GeraPlanosContasFinanceiro(nota, out mensagem);
            if (listaPlanoContas.Count == 0)
                listaPlanoContas = GeraPlanosContasFinanceiroServico(nota, out mensagem);

            if (temContrato == true)
                documento = MontaDocumentoFinanceiro(nota, contrato);
            else
                documento = MontaDocumentoFinanceiro(nota, null);

            VerificaFaturamentoRetroativo(ref documento, nota, conf);

            Documento subDocumento;
            decimal ValorRecibo = 0;
            int countdoc = 0;

            Pessoa operadoraCartao = new Pessoa();
            List<NotaParcela> Notasparcelascartao = new List<NotaParcela>();
            var contador = 0;
            foreach (var item in nota.NotaParcelas)
            {
                if (item.TipoDocumento.FormaPagamento.OperadoraCartao != null)
                {
                    contador++;
                }
            }
            if (contador > 0)
            {
                if (nota.NotaParcelas.Where(x => "Operadora" == x.TipoDocumento.FormaPagamento.OperadoraCartao?.TipoPessoa).Any())
                {
                    Notasparcelascartao = nota.NotaParcelas.Where(x => "Operadora" == x.TipoDocumento.FormaPagamento.OperadoraCartao?.TipoPessoa).ToList();
                }
                bool GeraParcelasOperadora = false;
                if ((conf.UtilizaRotinaCartao) &&
       (nota.Ent_Sai == InOutType.Saída) &&
       (operadoraCartao != null && operadoraCartao != new Pessoa()))
                {
                    GeraParcelasOperadora = true;
                    if (Notasparcelascartao.Count() > 0)
                    {
                        FinanceiroParcela parcelacartao = new FinanceiroParcela();
                        parcelacartao.FormaPagamento = new ParcelaBase.EnumFormaPagamento();
                        parcelacartao.Nota = nota;

                        parcelacartao.TipoDocumento = nota.NotaParcelas.First().TipoDocumento;
                        parcelacartao.TipoFormaPagamento = nota.NotaParcelas.First().TipoFormaPagamento;
                        parcelacartao.Valor = nota.NotaParcelas.Sum(x => x.Valor);
                        parcelacartao.ParcelaClienteCartao = true;
                        parcelacartao.Vencimento = DateTime.Now;
                        parcelacartao.Previsao = DateTime.Now;
                        listaParcelas.Add(parcelacartao);
                    }
                    foreach (FinanceiroParcela parcela in listaParcelas)
                    {
                        if (parcela.ParcelaClienteCartao)
                        {
                            documento.Pessoa = nota.Pessoa;
                            Configuracao config = ConfiguracaoController.Instancia.GetConfiguracao();

                            FinanceiroPlanoContas f = new FinanceiroPlanoContas
                            {
                                Descricao = config.IdPlanoContaBaixaCartao.Nome,
                                ID = config.IdPlanoContaBaixaCartao.ID,
                                PlanoConta = config.IdPlanoContaBaixaCartao,
                                Valor = parcela.Valor,
                                planocontaparcelaclientecartao = true
                            };
                            listaPlanoContas.Add(f);

                            List<FinanceiroParcela> lista = new List<FinanceiroParcela>();
                            foreach (var item in listaParcelas)
                            {
                                if (item.ParcelaClienteCartao)
                                {
                                    lista.Add(item);
                                }
                            }
                            subDocumento = GeraDocumentoNormal(nota, formasDePagamento, conf, documento, lista, listaPlanoContas, ref ValorRecibo, ref countdoc, parcela, true);
                            subDocumento.Saldo = 0;
                        }
                        else
                        {
                            if (GeraParcelasOperadora)
                            {
                                documento.Pessoa = nota.NotaParcelas.Where(x => x.TipoDocumento.FormaPagamento.Tipo == FormaPagamentoTipo.Cartao).FirstOrDefault().TipoDocumento.FormaPagamento.OperadoraCartao;
                                subDocumento = GeraDocumentoNormal(nota, formasDePagamento, conf, documento, listaParcelas, listaPlanoContas, ref ValorRecibo, ref countdoc, parcela, false);
                            }
                            else
                            {
                                subDocumento = GeraDocumentoNormal(nota, formasDePagamento, conf, documento, listaParcelas, listaPlanoContas, ref ValorRecibo, ref countdoc, parcela, false);
                            }

                        }
                    }
                }
            }

            else
            {
                foreach (FinanceiroParcela parcela in listaParcelas)
                {
                    subDocumento = GeraDocumentoNormal(nota, formasDePagamento, conf, documento, listaParcelas, listaPlanoContas, ref ValorRecibo, ref countdoc, parcela, false);
                }
            }


            return documento;
        }

        private Documento GeraDocumentoNormal(Nota nota, List<pxyFormaPagamentoBaixaDoc> formasDePagamento, Configuracao conf, Documento documento,
            List<FinanceiroParcela> listaParcelas, List<FinanceiroPlanoContas> listaPlanoContas, ref decimal ValorRecibo, ref int countdoc, FinanceiroParcela parcela, bool BaixaCartao)
        {
            Documento subDocumento;
            if ((nota.TipoNota.ModeloDocto == TipoNota.ModeloDocumento.RPS) ||
                (nota.TipoNota.ModeloDocto == TipoNota.ModeloDocumento.NFSe) ||
                (nota.TipoNota.ModeloDocto == TipoNota.ModeloDocumento.Balanco))
            {
                subDocumento = MontaDocumentoFinanceiroServico(documento, parcela, nota);
            }
            else
            {
                subDocumento = MontaDocumentoFinanceiro(documento, parcela);
            }

            VerificaFaturamentoRetroativo(ref subDocumento, nota, conf);

            if (temContrato == false)
                subDocumento.Contrato = null;

            //subDocumento.Codigo = DocumentoController.Instancia.GetNovoCodigoDocumento();
            subDocumento.QtParcela = listaParcelas.Count;
            subDocumento.SubistituaTagsComplementoHistorico();

#pragma warning disable CS0219 // A variável "auxValor" é atribuída, mas seu valor nunca é usado
            decimal auxValor = 0, percentual = 0, percentualRateio = 0, auxValorRateio = 0;
#pragma warning restore CS0219 // A variável "auxValor" é atribuída, mas seu valor nunca é usado
            int count = 1, countRateio;
            Movimento objMovimento;

            foreach (FinanceiroPlanoContas pcont in listaPlanoContas)
            {
                objMovimento = new Modelo.Movimento();
                objMovimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();

                if (nota.NotaItemsServicos.Count > 0 && nota.NotaItemsServicos[0].Servico.PlanoDeContas != null)
                    objMovimento.PlanoConta = nota.NotaItemsServicos[0].Servico.PlanoDeContas;
                else
                {
                    var planoConta = pcont.PlanoConta != null
                        ? pcont.PlanoConta
                        : PlanoContaController.Instancia.LoadObjectById(pcont.ID);

                    objMovimento.PlanoConta = planoConta;
                }


                objMovimento.Tipo = TipoMovimentoType.Documento;
                objMovimento.Sequencia = 1;
                objMovimento.Documento = subDocumento;
                objMovimento.Operacao = OperacaoMovimentoType.Lancamento;
                objMovimento.Historico = documento.Historico;
                objMovimento.ComplementoHist = documento.ComplementoHist;
                objMovimento.Dt = documento.Dt;
                try
                {
                    percentual = Math.Round(pcont.Valor / (decimal)documento.ValorTotal, 8);
                }
                catch (Exception)
                {
                    percentual = 0;
                }

                objMovimento.Valor = subDocumento.Valor;

                if (parcela.ParcelaClienteCartao)
                {
                    subDocumento.ValorTotal = parcela.Valor;
                }
                if (pcont.planocontaparcelaclientecartao)
                {
                    objMovimento.Operacao = OperacaoMovimentoType.BxTotal;
                }
                //WNO - Adiciona os rateios para projeto
                if (pcont.RateioProjeto != null)
                {
                    Modelo.Rateio_Mov objRateio;
                    countRateio = 1;
                    auxValorRateio = 0;
                    foreach (Modelo.Rateio_Mov item in pcont.RateioProjeto)
                    {
                        objRateio = new Modelo.Rateio_Mov();
                        objRateio.Projeto = item.Projeto;
                        objRateio.Sequencia = item.Sequencia;
                        percentualRateio = pcont.Valor == 0 ? 0 : Math.Round(item.Valor / pcont.Valor, 4);
                        if (countRateio == pcont.RateioProjeto.Count)
                            objRateio.Valor = (decimal)objMovimento.Valor - auxValorRateio;
                        else
                        {
                            objRateio.Valor = Math.Round(objMovimento.Valor * percentualRateio, 2);
                            auxValorRateio += objRateio.Valor;
                        }
                        objMovimento.Rateio_Movs.Add(objRateio);
                        countRateio++;
                    }
                }
                subDocumento.Movimentos.Add(objMovimento);
                count++;
            }

            GerarBaixasParaImpostosDeServico(nota, documento, subDocumento);

            DocumentoController.Instancia.RegisterNewIntoTransaction(subDocumento);

            if (parcela.BEntrada)
            {
                if (subDocumento.TipoDocumento.FormaPagamento == null)
                    throw new Exception("Erro ao realizar a baixa do documento gerado. Verifique se o cadastro do tipo de documento contém uma forma de pagamento.");

                ValorRecibo += (Decimal)subDocumento.Saldo;

                var movCaixasBaixa = GerarMovCaixasBaixa(formasDePagamento, subDocumento);

                Financeiro.BaixaDocumento baixaDocumento = new Financeiro.BaixaDocumento(new ParametrosBaixa()
                {
                    BaixarEmLote = false,
                    Banco = subDocumento.Banco,
                    ComplementoHist = subDocumento.ComplementoHist,
                    ComplementoHistTroco = subDocumento.ComplementoHist,
                    DataBaixa = subDocumento.Dt,
                    Documento = subDocumento,
                    MovCaixas = movCaixasBaixa,
                    Historico = subDocumento.Historico,
                    HistoricoTroco = subDocumento.Historico,
                    ValorBaixa = subDocumento.Valor,
                    ValorDesconto = 0,
                    ValorJuros = 0,
                    ValorMulta = 0,
                });
                baixaDocumento.BaixarDocumentos(movCaixasBaixa);
            }
            return subDocumento;
        }

        private void VerificaFaturamentoRetroativo(ref Documento documento, Nota nota, Configuracao conf)
        {
            if (conf.PermiteFatDtRetroativa)
            {
                documento.Dt = nota.Dt;
            }
        }

        private void GerarBaixasParaImpostosDeServico(Nota nota, Documento documento, Documento subDocumento)
        {
            List<Movimento> movimentosBaixa = new List<Movimento>();
            foreach (var impostoNota in nota.NotaImpostoServicos.Where(n => n.BaseRetencaoImposto))
            {
                //var movimento = movimentosBaixa.Where(m => m.PlanoConta.ID == impostoNota.Imposto.PlanoConta.ID).FirstOrDefault();

                var movimento = new Modelo.Movimento();
                movimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
                movimento.Tipo = TipoMovimentoType.Documento;
                movimento.PlanoConta = impostoNota.Imposto.PlanoConta;
                movimento.Sequencia = 1;
                movimento.Operacao = OperacaoMovimentoType.Retencao;
                movimento.Origem = (int)OrigemMovimentoType.Financeiro;
                movimento.ComplementoHist = "Retenção de imposto";
                movimento.Dt = documento.Dt;
                movimento.Documento = subDocumento;
                movimento.Valor = impostoNota.Valor / subDocumento.QtParcela;
                subDocumento.valorImpostoRetido += movimento.Valor;
                subDocumento.Saldo -= movimento.Valor;
                subDocumento.Movimentos.Add(movimento);
                movimentosBaixa.Add(movimento);
                movimento = new Movimento();
            }
        }

        private static List<MovCaixa> GerarMovCaixasBaixa(List<pxyFormaPagamentoBaixaDoc> formasDePagamento, Documento subDocumento)
        {
            if (formasDePagamento == null)
            {
                var movCaixaFormaPagamento = new MovCaixa
                {
                    Codigo = MovCaixaController.Instancia.GetNovoCodigoMovimento(),
                    FormaPagamento = subDocumento.TipoDocumento.FormaPagamento,
                    Valor = subDocumento.Valor,
                    Banco = subDocumento.Banco,
                    Dt = subDocumento.Dt,
                    DtDigitacao = DateTime.Now,
                    ComplementoHist = subDocumento.ComplementoHist,
                    Historico = subDocumento.Historico,
                    Filial = subDocumento.Filial,
                    NumBanco = subDocumento.NumBanco,
                    NumDocumento = subDocumento.NumDocumento,
                    Ent_Sai = subDocumento.Tipo == TipoDocumentoType.Receber ? Ent_SaiMovCaixa.Entrada : Ent_SaiMovCaixa.Saída
                };
                return new List<MovCaixa>() { movCaixaFormaPagamento };
            }
            else
            {
                var gerador = new GeradorMovCaixa(formasDePagamento, subDocumento, subDocumento.Dt, subDocumento.ComplementoHist);
                return gerador.GetMovCaixa(true);
            }
        }

        private Documento MontaDocumentoFinanceiroCartao(Nota nota, Pessoa operadoraCartao)
        {
            DateTime? dataFaturamento = null;
            Documento documento = new Documento();
            documento.Banco = nota.TipoNota.Banco;
            documento.Acrescimo = nota.TipoNota.Acrescimo;
            documento.Condicao = nota.Condicao;
            documento.Filial = nota.Filial;
            documento.Historico = nota.TipoNota.Historico;
            documento.Pessoa = operadoraCartao;
            documento.Portador = nota.TipoNota.Portador;
            documento.NumDocumento = nota.Numero.ToString();
            documento.NumRequisicao = ""; //TODO
            documento.Tipo = TipoDocumentoType.Receber;
            dataFaturamento = DateTime.Now;
            documento.DtVencimento = dataFaturamento.Value;
            documento.Dt = DateTime.Now;
            documento.DtDigitacao = nota.DtDigitacao.Value;
            documento.ValorTotal = nota.TotalNota;
            documento.ComplementoHist = nota.TipoNota.ComplementoHist;
            documento.Nota = nota;
            documento.Contrato = contrato;
            return documento;
        }

        private Documento MontaDocumentoFinanceiro(Nota nota, Contrato contrato)
        {
            DateTime? dataFaturamento = null;
            Documento documento = new Documento();
            documento.Banco = nota.TipoNota.Banco;
            documento.Acrescimo = nota.TipoNota.Acrescimo;
            documento.Condicao = nota.Condicao;
            documento.Filial = nota.Filial;
            documento.Historico = nota.TipoNota.Historico;
            documento.Pessoa = nota.Pessoa;
            documento.Portador = nota.TipoNota.Portador;
            documento.NumDocumento = nota.Numero.ToString();
            documento.NumRequisicao = ""; //TODO
            documento.Tipo = (nota.Ent_Sai == InOutType.Saída) ? TipoDocumentoType.Receber : TipoDocumentoType.Pagar;

            if (ehServico)
            {
                if (servicoContrato != null)
                {
                    if (servicoContrato.DiaVencimento != 0)
                    {
                        if (faturaMesSeguinte)
                        {
                            DateTime dataMesSeguinte = referenciaFaturamento.AddMonths(1);
                            dataFaturamento = new DateTime(dataMesSeguinte.Year, dataMesSeguinte.Month, servicoContrato.DiaVencimento);
                        }
                        else
                        {
                            DateTime dataMesSeguinte = referenciaFaturamento;
                            dataFaturamento = new DateTime(dataMesSeguinte.Year, dataMesSeguinte.Month, servicoContrato.DiaVencimento);
                        }
                        documento.DtVencimento = dataFaturamento.Value;
                    }
                }
            }
            else
            {
                dataFaturamento = referenciaFaturamento;
                documento.DtVencimento = dataFaturamento.Value;
            }

            documento.Dt = DateTime.Now;
            documento.DtDigitacao = nota.DtDigitacao.Value;
            documento.ValorTotal = nota.TotalNota;
            documento.ComplementoHist = nota.TipoNota.ComplementoHist;
            documento.Nota = nota;
            documento.Contrato = contrato;
            return documento;
        }

        private Documento MontaDocumentoFinanceiroCartao(Nota nota, Contrato contrato)
        {
            Documento documento = new Documento();
            documento.Banco = nota.TipoNota.Banco;
            documento.Acrescimo = nota.TipoNota.Acrescimo;
            documento.Condicao = CondicaoController.Instancia.GetAVista(1);
            documento.Filial = nota.Filial;
            documento.Historico = nota.TipoNota.Historico;
            documento.Pessoa = nota.Pessoa;
            documento.Portador = nota.TipoNota.Portador;
            documento.NumDocumento = nota.Numero.ToString();
            documento.NumRequisicao = ""; //TODO
            documento.Tipo = TipoDocumentoType.Receber;

            documento.DtVencimento = DateTime.Now;

            documento.Dt = DateTime.Now;
            documento.DtDigitacao = nota.DtDigitacao.Value;
            documento.ValorTotal = nota.TotalNota;
            documento.ComplementoHist = nota.TipoNota.ComplementoHist;
            documento.Nota = nota;
            documento.Contrato = contrato;
            return documento;
        }


        private List<FinanceiroPlanoContas> GeraPlanosContasFinanceiro(Nota objNota, out String mensagemOut)
        {
            string mensagem = String.Empty;
            List<NotaItem> itensSemPlanoConta = new List<Modelo.NotaItem>();
            List<FinanceiroPlanoContas> listaPContas = new List<FinanceiroPlanoContas>();
            if (objNota.Ent_Sai == Modelo.InOutType.Entrada)
            {
                foreach (Modelo.NotaItem item in objNota.NotaItems)
                {
                    if (item.Produto.PlanoContaEstoque != null)
                    {
                        var existente = listaPContas.Where(p => p.ID == item.Produto.PlanoContaEstoque.ID);
                        if (existente.Count() == 1)
                            existente.Single().Valor += item.Total - (item.RAT_Desconto);
                        else
                        {
                            FinanceiroPlanoContas fPlanoConta = new FinanceiroPlanoContas();

                            fPlanoConta.ID = item.Produto.PlanoContaEstoque.ID;
                            var PlanoContaEstoque = PlanoContaController.Instancia.LoadObjectById(item.Produto.PlanoContaEstoque.ID);
                            item.Produto.PlanoContaEstoque = PlanoContaEstoque;

                            fPlanoConta.PlanoConta = item.Produto.PlanoContaEstoque;
                            fPlanoConta.Descricao = item.Produto.PlanoContaEstoque.Nome;
                            fPlanoConta.Valor = item.Total - (item.RAT_Desconto);
                            listaPContas.Add(fPlanoConta);
                        }
                    }
                    else
                        itensSemPlanoConta.Add(item);
                }
            }
            else
                itensSemPlanoConta.AddRange(objNota.NotaItems.ToList());

            FinanceiroPlanoContas planoconta;
            if ((itensSemPlanoConta.Count > 0) || (objNota.NotaItemsServicos.Count > 0))
            {
                var existente = listaPContas.Where(p => p.ID == objNota.TipoNota.PlanoConta.ID);
                if (existente.Count() == 1)
                    planoconta = existente.Single();
                else
                {
                    planoconta = new FinanceiroPlanoContas();
                    if (objNota.TipoNota.PlanoConta == null)
                    {
                        throw new Exception("O tipo de nota escolhido não possui um plano de contas vinculado. Verifique");
                    }
                    planoconta.ID = objNota.TipoNota.PlanoConta.ID;
                    planoconta.PlanoConta = objNota.TipoNota.PlanoConta;
                    planoconta.Descricao = objNota.TipoNota.PlanoConta.Nome;
                    planoconta.Valor = 0;
                    listaPContas.Add(planoconta);
                }

                if (objNota.Ent_Sai == Modelo.InOutType.Entrada)
                {
                    planoconta.Valor += itensSemPlanoConta.Sum(p => p.Total);
                    StringBuilder str = new StringBuilder();
                    str.AppendLine("Foi utilizado o plano de contas configurado no tipo de movimentação para os seguintes produtos:");
                    foreach (Modelo.NotaItem item in itensSemPlanoConta)
                    {
                        str.AppendLine(" -" + item.Produto.Nome);
                    }
                    mensagem = str.ToString();
                }
                else
                    planoconta.Valor = objNota.TotalProduto - itensSemPlanoConta.Sum(p => p.RAT_Desconto);
            }

            //Plano conta do frete
            if (objNota.ValorFrete > 0)
            {
                planoconta = new FinanceiroPlanoContas();
                Modelo.PlanoConta objPlanoConta = objNota.TipoNota.PlanoContaFrete;
                planoconta.ID = objPlanoConta.ID;
                planoconta.PlanoConta = objPlanoConta;
                planoconta.Descricao = objPlanoConta.Nome;
                planoconta.Valor = objNota.ValorFrete;
                listaPContas.Add(planoconta);
            }

            //Plano conta do IPI
            decimal valorIPI = 0;
            if (objNota.NotaICMSs != null)
                valorIPI = objNota.NotaICMSs.Sum(n => n.ValorIPI);
            if (valorIPI > 0)
            {
                planoconta = new FinanceiroPlanoContas();
                Modelo.PlanoConta objPlanoConta = objNota.TipoNota.PlanoContaIPI;
                planoconta.ID = objPlanoConta.ID;
                planoconta.PlanoConta = objPlanoConta;
                planoconta.Descricao = objPlanoConta.Nome;
                planoconta.Valor = valorIPI;
                listaPContas.Add(planoconta);
            }
            mensagemOut = mensagem;
            return listaPContas;
        }

        private List<FinanceiroPlanoContas> GeraPlanosContasFinanceiroServico(Nota objNota, out string mensagemOut)
        {
            string mensagem = String.Empty;
            List<NotaItemServico> itensSemPlanoConta = new List<Modelo.NotaItemServico>();
            List<FinanceiroPlanoContas> listaPContas = new List<FinanceiroPlanoContas>();

            FinanceiroPlanoContas planoconta;
            if (itensSemPlanoConta.Count > 0)
            {
                var existente = listaPContas.Where(p => p.ID == objNota.TipoNota.PlanoConta.ID);
                if (existente.Count() == 1)
                    planoconta = existente.Single();
                else
                {
                    planoconta = new FinanceiroPlanoContas();
                    planoconta.ID = objNota.TipoNota.PlanoConta.ID;
                    planoconta.PlanoConta = objNota.TipoNota.PlanoConta;
                    planoconta.Descricao = objNota.TipoNota.PlanoConta.Nome;
                    planoconta.Valor = 0;
                    listaPContas.Add(planoconta);
                }
                planoconta.Valor = objNota.TotalProduto;
            }


            mensagemOut = mensagem;
            return listaPContas;
        }

        private List<FinanceiroParcela> GeraParcelasFinanceiro(Nota nota)
        {
            DateTime? dataContabil = null;
            List<FinanceiroParcela> listaParcelas = new List<FinanceiroParcela>();

            foreach (NotaParcela parc in nota.NotaParcelas)
            {
                FinanceiroParcela parcela = new FinanceiroParcela();

                parcela.Parcela = parc.Parcela;
                parcela.Vencimento = parc.Vencimento;
                parcela.Valor = parc.Valor;

                if (ehServico)
                {
                    if (servicoContrato != null)
                    {
                        if (servicoContrato.DiaVencimento != 0)
                        {
                            if (faturaMesSeguinte)
                            {
                                DateTime dataMesSeguinte = referenciaFaturamento.AddMonths(1);
                                dataContabil = new DateTime(dataMesSeguinte.Year, dataMesSeguinte.Month, servicoContrato.DiaVencimento);
                                parcela.DtContabil = (DateTime)dataContabil;
                                parcela.Vencimento = (DateTime)dataContabil;
                            }
                            else
                            {
                                DateTime dataMesSeguinte = referenciaFaturamento;
                                dataContabil = new DateTime(dataMesSeguinte.Year, dataMesSeguinte.Month, servicoContrato.DiaVencimento);
                                parcela.DtContabil = (DateTime)dataContabil;
                                parcela.Vencimento = (DateTime)dataContabil;
                            }
                        }
                    }
                }
                else
                {
                    dataContabil = nota.Dt;
                    parcela.DtContabil = (DateTime)dataContabil;
                    parcela.Vencimento = parc.Vencimento;
                }

                if (dataContabil == null)
                    parcela.DtContabil = nota.Dt;

                parcela.BAlterado = false;

                parcela.TipoDocumento = parc.TipoDocumento;
                var Forma = ConversorFormaPagamento.GetFormaPagamento(parcela.TipoDocumento.FormaPagamento.CodigoSefaz);
                parcela.FormaPagamento = Forma;
                parcela.TipoFormaPagamento = ConversorFormaPagamento.GetFormaPagamento(Forma);

                parcela.BEntrada = parc.BEntrada;
                parcela.NomeBanco = parc.NomeBanco;
                parcela.NumAgencia = parc.NumAgencia;
                parcela.NumBanco = parc.NumBanco;
                parcela.NumCheque = parc.NumCheque;
                parcela.NumContaCorrente = parc.NumContaCorrente;
                parcela.Emitente = parc.Emitente;
                parcela.CpfCnpj = parc.CpfCnpj;
                listaParcelas.Add(parcela);

            }
            return listaParcelas;
        }

        private Documento MontaDocumentoFinanceiro(Documento documento, FinanceiroParcela parcela)
        {
            Documento subDocumento = new Documento();
            subDocumento.Codigo = DocumentoController.Instancia.GetNovoCodigoDocumento();
            subDocumento.Parcela = parcela.Parcela;
            subDocumento.Banco_Str = documento.Banco_Str;
            subDocumento.Banco = documento.Banco;
            subDocumento.Acrescimo = documento.Acrescimo;
            subDocumento.Condicao = documento.Condicao;
            subDocumento.Filial = documento.Filial;
            subDocumento.Historico = documento.Historico;
            subDocumento.Pessoa = documento.Pessoa;
            subDocumento.Portador = documento.Portador;
            subDocumento.TipoDocumento = parcela.TipoDocumento;
            subDocumento.NumDocumento = documento.NumDocumento.Trim();
            subDocumento.NumRequisicao = documento.NumRequisicao.Trim();
            subDocumento.Tipo = documento.Tipo;
            subDocumento.Dt = documento.Dt;
            subDocumento.DtDigitacao = documento.DtDigitacao;
            subDocumento.ValorTotal = documento.ValorTotal;
            subDocumento.Valor = parcela.Valor;
            subDocumento.ComplementoHist = documento.ComplementoHist;
            subDocumento.DtVencimento = parcela.Vencimento;
            subDocumento.DtPrevisao = parcela.Vencimento;
            subDocumento.Nota = documento.Nota;
            subDocumento.Saldo = subDocumento.Valor;
            subDocumento.Situacao = "Norm";
            subDocumento.MovimentoRemessa = documento.MovimentoRemessa;
            subDocumento.NumBanco = parcela.NumBanco;
            subDocumento.Agencia = parcela.NumAgencia;
            subDocumento.NomeBanco = parcela.NomeBanco;
            subDocumento.Conta = parcela.NumContaCorrente;
            subDocumento.CpfCnpj = parcela.CpfCnpj;
            subDocumento.Cheque = parcela.NumCheque;
            subDocumento.Emitente = parcela.Emitente;
            subDocumento.Contrato = contrato;
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            if (subDocumento.Nota.CodigoPedido != null && subDocumento.Nota.CodigoPedido > 0)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            {
                subDocumento.NumeroPedido = GetNumeroPedidoPorCodigoPedido(subDocumento.Nota.CodigoPedido);
            }
            return subDocumento;
        }

        private Documento MontaDocumentoFinanceiroServico(Documento documento, FinanceiroParcela parcela, Nota nota)
        {
            Documento subDocumento = new Documento();
            subDocumento.Codigo = DocumentoController.Instancia.GetNovoCodigoDocumento();
            subDocumento.Parcela = parcela.Parcela;
            subDocumento.Banco_Str = documento.Banco_Str;
            subDocumento.Banco = documento.Banco;
            subDocumento.Acrescimo = documento.Acrescimo;
            subDocumento.Condicao = documento.Condicao;
            subDocumento.Filial = documento.Filial;
            subDocumento.Historico = documento.Historico;
            subDocumento.Pessoa = documento.Pessoa;
            subDocumento.Portador = documento.Portador;
            subDocumento.TipoDocumento = parcela.TipoDocumento;
            subDocumento.NumDocumento = documento.NumDocumento.Trim();
            subDocumento.NumRequisicao = documento.NumRequisicao.Trim();
            subDocumento.Tipo = documento.Tipo;
            subDocumento.Dt = documento.Dt;
            subDocumento.DtDigitacao = documento.DtDigitacao;
            subDocumento.ValorTotal = documento.ValorTotal;
            subDocumento.Valor = parcela.Valor;
            subDocumento.Saldo = subDocumento.Valor;
            subDocumento.ComplementoHist = documento.ComplementoHist;
            subDocumento.DtVencimento = parcela.Vencimento;
            subDocumento.DtPrevisao = parcela.Vencimento;
            subDocumento.Nota = documento.Nota;
            subDocumento.Situacao = "Norm";
            subDocumento.MovimentoRemessa = documento.MovimentoRemessa;
            subDocumento.NumBanco = parcela.NumBanco;
            subDocumento.Agencia = parcela.NumAgencia;
            subDocumento.NomeBanco = parcela.NomeBanco;
            subDocumento.Conta = parcela.NumContaCorrente;
            subDocumento.CpfCnpj = parcela.CpfCnpj;
            subDocumento.Cheque = parcela.NumCheque;
            subDocumento.Emitente = parcela.Emitente;
            subDocumento.Contrato = contrato;
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            if (documento.Nota.CodigoPedido != null && documento.Nota.CodigoPedido > 0)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
            {
                subDocumento.NumeroPedido = GetNumeroPedidoPorCodigoPedido(documento.Nota.CodigoPedido);
            }

            return subDocumento;
        }


        #region PreDanfe

        public static cwkGestao.Modelo.Auxiliares.PreDanfe CarregarPreDanfe(string xmlDanfe)
        {
            var predanfe = new PreDanfe();

            XDocument xdoc = XDocument.Load(new XmlTextReader(new System.IO.MemoryStream(Encoding.ASCII.GetBytes(xmlDanfe))));

            XElement root = xdoc.Root.Element(ns + "infNFe");

            XElement emitente = root.Element(ns + "emit");
            XElement destinatario = root.Element(ns + "dest");
            XElement transportador = root.Element(ns + "transp");
            XElement parcelas = root.Element(ns + "cobr");
            XElement itens = root.Element(ns + "det");
            XElement totais = root.Element(ns + "total");
            XElement dadosNfe = root.Element(ns + "ide");

            if (dadosNfe != null)
                CarregarPreDanfeDadosNfe(predanfe, dadosNfe, root.Element(ns + "infAdic"));
            if (emitente != null)
                CarregaPreDanfeDadosEmitente(predanfe, emitente);
            if (destinatario != null && dadosNfe != null)
                CarregaPreDanfeDadosDestinatario(predanfe, destinatario, dadosNfe);
            if (transportador != null)
                CarregaPreDanfeDadosTransportador(predanfe, transportador);
            if (itens != null)
                CarregarPreDanfeItens(predanfe, itens);
            if (parcelas != null)
                CarregarPreDanfeParcelas(predanfe, parcelas);
            if (totais != null)
                CarregarPreDanfeTotais(predanfe, totais);

            // ReSharper restore PossibleNullReferenceException
            return predanfe;
        }

        private static void CarregarPreDanfeDadosNfe(PreDanfe predanfe, XElement dadosNfe, XElement infoAdic)
        {
            predanfe.NaturezaOperacao = ExtraiValor(dadosNfe.Element(ns + "natOp"));
            predanfe.EntradaSaida = ExtraiValor(dadosNfe.Element(ns + "tpNF"));
            predanfe.Numero = ExtraiValor(dadosNfe.Element(ns + "nNF"));
            predanfe.Serie = ExtraiValor(dadosNfe.Element(ns + "serie"));
            if (infoAdic != null)
                predanfe.InformacoesComplementares = ExtraiValor(infoAdic.Element(ns + "infCpl"));

        }

        private static void CarregarPreDanfeTotais(PreDanfe predanfe, XElement totais)
        {
            totais = totais.Element(ns + "ICMSTot");

            if (totais != null)
            {
                predanfe.CalculoImpostoBaseCalculoICMS = ExtraiValor(totais.Element(ns + "vBC"));
                predanfe.CalculoImpostoBaseCalculoIcmsst = ExtraiValor(totais.Element(ns + "vBCST"));
                predanfe.CalculoImpostoDesconto = ExtraiValor(totais.Element(ns + "vDesc"));
                predanfe.CalculoImpostoOutrasDespesas = ExtraiValor(totais.Element(ns + "vOutro"));
                predanfe.CalculoImpostoValorFrete = ExtraiValor(totais.Element(ns + "vFrete"));
                predanfe.CalculoImpostoValorICMS = ExtraiValor(totais.Element(ns + "vICMS"));
                predanfe.CalculoImpostoValorICMSSubstituicao = ExtraiValor(totais.Element(ns + "vST"));
                predanfe.CalculoImpostoValorIPI = ExtraiValor(totais.Element(ns + "vIPI"));
                predanfe.CalculoImpostoValorSeguro = ExtraiValor(totais.Element(ns + "vSeg"));
                predanfe.CalculoImpostoValorTotalNota = ExtraiValor(totais.Element(ns + "vNF"));
                predanfe.CalculoImpostoValorTotalProdutos = ExtraiValor(totais.Element(ns + "vProd"));
            }
        }

        private static void CarregarPreDanfeParcelas(PreDanfe predanfe, XElement parcelas)
        {
            PreDanfe.Parcela parcela = new PreDanfe.Parcela();
            XElement fatura = parcelas.Element(ns + "fat");
            XElement duplicata = parcelas.Element(ns + "dup");

            while (duplicata != null && duplicata.Name.LocalName.Equals("dup"))
            {
                parcela.DataVcto1 = ExtraiValor(duplicata.Element(ns + "dVenc"));
                parcela.Numero1 = ExtraiValor(duplicata.Element(ns + "nDup"));
                parcela.Valor1 = ExtraiValor(duplicata.Element(ns + "vDup"));
                duplicata = duplicata.ElementsAfterSelf().FirstOrDefault();

                if (duplicata != null && duplicata.Name.LocalName.Equals("dup"))
                {


                    parcela.DataVcto2 = ExtraiValor(duplicata.Element(ns + "dVenc"));
                    parcela.Numero2 = ExtraiValor(duplicata.Element(ns + "nDup"));
                    parcela.Valor2 = ExtraiValor(duplicata.Element(ns + "vDup"));
                    duplicata = duplicata.ElementsAfterSelf().FirstOrDefault();

                }

                if (duplicata != null && duplicata.Name.LocalName.Equals("dup"))
                {
                    parcela.DataVcto3 = ExtraiValor(duplicata.Element(ns + "dVenc"));
                    parcela.Numero3 = ExtraiValor(duplicata.Element(ns + "nDup"));
                    parcela.Valor3 = ExtraiValor(duplicata.Element(ns + "vDup"));
                    duplicata = duplicata.ElementsAfterSelf().FirstOrDefault();
                }

                if (duplicata != null && duplicata.Name.LocalName.Equals("dup"))
                {
                    parcela.DataVcto4 = ExtraiValor(duplicata.Element(ns + "dVenc"));
                    parcela.Numero4 = ExtraiValor(duplicata.Element(ns + "nDup"));
                    parcela.Valor4 = ExtraiValor(duplicata.Element(ns + "vDup"));
                    duplicata = duplicata.ElementsAfterSelf().FirstOrDefault();
                }

                predanfe.DanfeParcelas.Add(parcela);
                parcela = new PreDanfe.Parcela();

            }




        }

        private static void CarregarPreDanfeItens(PreDanfe predanfe, XElement itens)
        {
            PreDanfe.Item item = new PreDanfe.Item();
            XElement produto = itens.Element(ns + "prod");
            XElement imposto;
            XElement icms = null, ipi = null;

            while (produto != null && produto.Name.LocalName.Equals("prod"))
            {

                imposto = itens.Element(ns + "imposto");
                if (imposto != null)
                {
                    icms = imposto.Element(ns + "ICMS");
                    if (icms != null) icms = icms.Elements().FirstOrDefault();
                    ipi = imposto.Element(ns + "IPI");
                    if (ipi != null) ipi = ipi.Element(ns + "IPITrib");
                }
                if (icms != null)
                {
                    item.BaseCalculoICMS = ExtraiValor(icms.Element(ns + "vBC"));
                    item.AliquotaICMS = ExtraiValor(icms.Element(ns + "pICMS"));
                    item.ValorICMS = ExtraiValor(icms.Element(ns + "vICMS"));
                    item.Origem = ExtraiValor(icms.Element(ns + "orig"));
                    item.CST = ExtraiValor(icms.Element(ns + "CST"));
                    if (item.CST == null)
                        item.CST = ExtraiValor(icms.Element(ns + "CSOSN"));
                }
                if (ipi != null)
                {
                    item.AliquotaIPI = ExtraiValor(ipi.Element(ns + "pIPI"));
                    item.ValorIPI = ExtraiValor(ipi.Element(ns + "vIPI"));
                }
                item.CFOP = ExtraiValor(produto.Element(ns + "CFOP"));
                item.CodigoProduto = ExtraiValor(produto.Element(ns + "cProd"));

                item.DescricaoProduto = ExtraiValor(produto.Element(ns + "xProd"));
                item.NCMSH = ExtraiValor(produto.Element(ns + "NCM"));
                item.Quantidade = ExtraiValor(produto.Element(ns + "qCom"));
                item.Unidade = ExtraiValor(produto.Element(ns + "uCom"));
                item.ValorUnitario = ExtraiValor(produto.Element(ns + "vUnCom"));
                item.ValorTotal = ExtraiValor(produto.Element(ns + "vProd"));
                item.InfAdicional = ExtraiValor(itens.Element(ns + "infAdProd"));

                predanfe.DanfeItems.Add(item);

                itens = itens.ElementsAfterSelf().FirstOrDefault();
                produto = itens.Element(ns + "prod");
                item = new PreDanfe.Item();

            }
        }

        private static void CarregaPreDanfeDadosTransportador(PreDanfe predanfe, XElement transporte)
        {

            XElement transportador = transporte.Element(ns + "transporta");
            XElement veiculo = transporte.Element(ns + "veicTransp");
            XElement volumes = transporte.Element(ns + "vol");
            predanfe.TransportadorFretePorConta = ExtraiValor(transporte.Element(ns + "modFrete"));

            if (volumes != null)
            {
                predanfe.TransportadorNumeracao = ExtraiValor(volumes.Element(ns + "nVol"));
                predanfe.TransportadorPesoBruto = ExtraiValor(volumes.Element(ns + "pesoB"));
                predanfe.TransportadorPesoLiquido = ExtraiValor(volumes.Element(ns + "pesoL"));
                predanfe.TransportadorQuantidade = ExtraiValor(volumes.Element(ns + "qVol"));
                predanfe.TransportadorEspecie = ExtraiValor(volumes.Element(ns + "esp"));
                predanfe.TransportadorMarca = ExtraiValor(volumes.Element(ns + "marca"));
            }

            if (veiculo != null)
            {
                predanfe.TransportadorCodigoAntt = ExtraiValor(veiculo.Element(ns + "RNTC"));
                predanfe.TransportadorPlacaUF = ExtraiValor(veiculo.Element(ns + "UF"));
                predanfe.TransportadorPlacaVeiculo = ExtraiValor(veiculo.Element(ns + "placa"));
            }

            if (transportador != null)
            {
                predanfe.TransportadorCNPJ = ExtraiValor(transportador.Element(ns + "CNPJ")) ?? ExtraiValor(transportador.Element(ns + "CPF"));
                if (predanfe.TransportadorCNPJ != null)
                    predanfe.TransportadorCNPJ = predanfe.TransportadorCNPJ.Length == 11 ? FormataCPF(predanfe.TransportadorCNPJ) : FormataCNPJ(predanfe.TransportadorCNPJ);
                predanfe.TransportadorRazaoSocial = ExtraiValor(transportador.Element(ns + "xNome"));
                predanfe.TransportadorUF = ExtraiValor(transportador.Element(ns + "UF"));
                predanfe.TransportadorEndereco = ExtraiValor(transportador.Element(ns + "xEnder"));
                predanfe.TransportadorInscricaoEstadual = ExtraiValor(transportador.Element(ns + "IE"));
                predanfe.TransportadorMunicipio = ExtraiValor(transportador.Element(ns + "xMun"));
            }


        }

        private static string FormataCNPJ(string p)
        {
            return Regex.Replace(p, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
        }

        private static string FormataCPF(string p)
        {
            return Regex.Replace(p, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }

        private static string FormataCEP(string p)
        {
            return Regex.Replace(p, @"(\d{2})(\d{3})(\d{3})", "$1.$2-$3");
        }

        private static void CarregaPreDanfeDadosDestinatario(PreDanfe predanfe, XElement destinatario, XElement dadosNfe)
        {
            predanfe.DestinatarioCNPJ = ExtraiValor(destinatario.Element(ns + "CNPJ")) ??
                                        ExtraiValor(destinatario.Element(ns + "CPF"));
            if (predanfe.DestinatarioCNPJ != null)
                predanfe.DestinatarioCNPJ = predanfe.DestinatarioCNPJ.Length == 11 ? FormataCPF(predanfe.DestinatarioCNPJ) : FormataCNPJ(predanfe.DestinatarioCNPJ);
            predanfe.DestinatarioNome = ExtraiValor(destinatario.Element(ns + "xNome"));
            predanfe.DestinatarioInscricaoEstadual = ExtraiValor(destinatario.Element(ns + "IE"));
            predanfe.DestinatarioDataEmissao = ExtraiValor(dadosNfe.Element(ns + "dEmi"));
            predanfe.DestinatarioDataSaida = ExtraiValor(dadosNfe.Element(ns + "dSaiEnt"));
            predanfe.DestinatarioHoraSaida = ExtraiValor(dadosNfe.Element(ns + "hSaiEnt"));

            XElement endereco = destinatario.Element(ns + "enderDest");
            if (endereco != null)
            {

                predanfe.DestinatarioEndereco = ExtraiValor(endereco.Element(ns + "xLgr")) + (endereco.Element(ns + "nro") == null ? ", " + ExtraiValor(endereco.Element(ns + "nro")) : "");
                predanfe.DestinatarioBairro = ExtraiValor(endereco.Element(ns + "xBairro"));
                predanfe.DestinatarioCEP = FormataCEP(ExtraiValor(endereco.Element(ns + "CEP")));
                predanfe.DestinatarioMunicipio = ExtraiValor(endereco.Element(ns + "xMun"));
                predanfe.DestinatarioUF = ExtraiValor(endereco.Element(ns + "UF"));
                predanfe.DestinatarioFone = ExtraiValor(endereco.Element(ns + "fone"));
            }
        }

        private static void CarregaPreDanfeDadosEmitente(PreDanfe predanfe, XElement emitente)
        {
            predanfe.EmitenteCNPJ = ExtraiValor(emitente.Element(ns + "CNPJ"));
            if (predanfe.EmitenteCNPJ != null)
                predanfe.EmitenteCNPJ = FormataCNPJ(predanfe.EmitenteCNPJ);
            predanfe.EmitenteNome = ExtraiValor(emitente.Element(ns + "xNome"));
            predanfe.EmitenteInscricaoEstadual = ExtraiValor(emitente.Element(ns + "IE"));
            predanfe.EmitenteInscricaoEstadualSubTributaria = ExtraiValor(emitente.Element(ns + "IEST"));

            XElement endereco = emitente.Element(ns + "enderEmit");
            if (endereco != null)
            {

                predanfe.EmitenteEndereco = ExtraiValor(endereco.Element(ns + "xLgr")) + (endereco.Element(ns + "nro") != null ? ", " + ExtraiValor(endereco.Element(ns + "nro")) : "");
                predanfe.EmitenteComplemento = ExtraiValor(endereco.Element(ns + "xCpl"));
                predanfe.EmitenteBairro = ExtraiValor(endereco.Element(ns + "xBairro"));
                predanfe.EmitenteCEP = FormataCEP(ExtraiValor(endereco.Element(ns + "CEP")));
                predanfe.EmitenteCidade = ExtraiValor(endereco.Element(ns + "xMun"));
                predanfe.EmitenteEstado = ExtraiValor(endereco.Element(ns + "UF"));
                predanfe.EmitenteTelefone = ExtraiValor(endereco.Element(ns + "fone"));
            }

        }



        #endregion

        private static string ExtraiValor(XElement element)
        {
            return element == null ? null : element.Value;
        }

        public IList<Nota> GetNotasPorClienteNoPeriodo(Pessoa cliente, DateTime inicio, DateTime fim)
        {
            return repositorioNota.GetNotasPorClienteNoPeriodo(cliente, inicio, fim);
        }

        public IList<Nota> GetAll(InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas)
        {
            return repositorioNota.GetAll(entSai, produtoServico, notasCanceladas);
        }

        public IList<Nota> GetAllPeriodo(DateTime dataInicial, DateTime dataFinal, InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas)
        {
            return repositorioNota.GetAllPeriodo(dataInicial, dataFinal, entSai, produtoServico, notasCanceladas);
        }

        public IList<Nota> GetAutorizadasPorCliente(InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas, int idPessoa, string statusNota)
        {
            return repositorioNota.GetAutorizadasPorCliente(entSai, produtoServico, notasCanceladas, idPessoa, statusNota);
        }

        public IList<NotaItem> GetProdutoVendaPorClienteNoPeriodo(Pessoa pessoa, Produto produto, DateTime dateTime, DateTime dateTime_3, InOutType inOut)
        {
            return repositorioNota.GetProdutoVendaPorClienteNoPeriodo(pessoa, produto, dateTime, dateTime_3, inOut);
        }

        public int GetNumeroPedidoPorCodigoPedido(int codigoPedido)
        {
            return repositorioNota.GetNumeroPedidoPorCodigoPedido(codigoPedido);
        }

        public void CancelarNF(Nota nota, string motivo)
        {
            var documentosNota = DocumentoController.Instancia.GetDocumentosPorNota(nota.ID);
            if (DocumentoController.Instancia.ExisteDocumentoBaixado(documentosNota))
            {
                throw new Exception("Nota não pode ser cancelada porque possui documentos no financeiro já baixados.");
            }
            nota.CancDt = DateTime.Now;
            nota.CancMotivo = motivo;
            nota.CancUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            RegisterNewIntoTransaction(nota);
            DocumentoController.Instancia.CancelarDocumentos(documentosNota);
            CommitUnitOfWorkTransaction();
        }

        public void CancelarCupomFiscal()
        {
            try
            {
                Nota Nota = repositorioNota.GetUltimoCupomEmitido();
                var documentosNota = DocumentoController.Instancia.GetDocumentosPorNota(Nota.ID);
                if (DocumentoController.Instancia.ExisteDocumentoBaixado(documentosNota))
                {
                    throw new Exception("Nota não pode ser cancelada porque possui documentos no financeiro já baixados.");
                }
                Nota.CancDt = DateTime.Now;
                Nota.CancMotivo = "Cancelamento de Cupom Fiscal";
                Nota.CancUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                Nota.Status = "3";
                RegisterNewIntoTransaction(Nota);
                DocumentoController.Instancia.CancelarDocumentos(documentosNota);
                // Não colocar commit nesse método, se vai commitar ou não esta sendo tratado no método que chama esse. (Negocio.CupomFiscal.CancelaCupom)
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int MontarObservacao(Nota nota, out string observacaoSistema)
        {
            string obs = GetObservacaoConfiguracao();
            if (nota.TipoNota != null && !String.IsNullOrEmpty(nota.TipoNota.Observacao))
                obs += String.IsNullOrEmpty(obs) ? nota.TipoNota.Observacao : " - " + nota.TipoNota.Observacao;

            foreach (Modelo.NotaItem ntai in nota.NotaItems)
            {
                TextoLei textolei = new TextoLei();
                if (ntai.Texto != null)
                    textolei = TextoLeiController.Instancia.LoadObjectById(ntai.Texto.ID);
                if (ntai.Texto == null || String.IsNullOrEmpty(textolei.DescricaoTextoLei) || obs.IndexOf(textolei.DescricaoTextoLei) >= 0)
                    continue;

                obs += String.IsNullOrEmpty(obs) ? ntai.TextoLei : " - " + ntai.TextoLei;
            }

            obs += String.IsNullOrEmpty(obs) ? MontaObservacaoCredito(nota.NotaItems) : " - " + MontaObservacaoCredito(nota.NotaItems);

            if (((nota.TipoNota != null) &&
                (nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Produtos)) &&
                (nota.TipoNota.ExibirTextoTotalTrib == true))
                obs += ExibeTotalTributosEmpresaSimples(ref nota, ref obs);

            observacaoSistema = obs.TrimEnd();
            return (5000 - obs.Length);
        }

        private string GetObservacaoConfiguracao()
        {
            var configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            return configuracao.Observacao;
        }

        private string MontaObservacaoCredito(IList<NotaItem> pListaNotaItem)
        {
            if (pListaNotaItem.Count > 0)
            {
                bool ehSimples = pListaNotaItem[0].Nota.Filial.SimplesNacional == 0;

                if (ehSimples)
                {
                    decimal valorCredito = 0;
                    decimal percCredito = 0;

                    foreach (Modelo.NotaItem item in pListaNotaItem)
                    {
                        if (item.TAG_CST == "101" || item.TAG_CST == "201" || item.TAG_CST == "900")
                        {
                            valorCredito += item.VCredICMSSN_N30;
                            percCredito = item.PCredSN_N29;
                        }
                    }

                    string textoLeiCredito = RecuperarTextoLeiCredito();
                    int indiceVCred = textoLeiCredito.IndexOf("*vCred");
                    int indicePCred = textoLeiCredito.IndexOf("*pCred");

                    if ((indicePCred > 0 && percCredito > 0) && (indiceVCred > 0 && valorCredito > 0))
                    {
                        textoLeiCredito = textoLeiCredito.Replace("*vCred", String.Format("{0:C}", valorCredito));
                        textoLeiCredito = textoLeiCredito.Replace("*pCred", String.Format("{0:#0.00%}", percCredito / 100));

                        return textoLeiCredito;
                    }
                }
            }
            return string.Empty;
        }

        private static string _textoLeiCredito = null;
        private string RecuperarTextoLeiCredito()
        {
            if (_textoLeiCredito == null)
            {
                string cam = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                XDocument xmlConfiguracao = XDocument.Load(cam + "\\ConfiguracaoCwork.xml");

                if (xmlConfiguracao.Root.Elements().Where(i => i.Name.LocalName == "TextoLeiCredito").Count() == 1)
                {
                    _textoLeiCredito = (from nohTextoLei in xmlConfiguracao.Root.Elements() where nohTextoLei.Name.LocalName == "TextoLeiCredito" select nohTextoLei.Value).Single();
                }
                else
                {
                    throw new Exception("Texto Lei de Crédito não está presente na configuração do sistema.");
                }
                return _textoLeiCredito;
            }
            else
                return _textoLeiCredito;

        }

        public void DescancelarNF(Nota nota)
        {
            nota.CancDt = null;
            nota.CancMotivo = null;
            nota.CancUsuario = null;
            RegisterNewIntoTransaction(nota);

            var documentosNota = DocumentoController.Instancia.GetDocumentosPorNota(nota.ID);
            DocumentoController.Instancia.DescancelarDocumentos(documentosNota);

            CommitUnitOfWorkTransaction();
        }

        public Nota GetByCodigo(int p)
        {
            return repositorioNota.GetByCodigo(p);
        }

        public Nota GetNotaComplementar(Nota nota)
        {
            Nota notaComplementar = repositorioNota.GetNotaComplementar(nota);
            if (notaComplementar.ID == 0)
            {
                nota = repositorioNota.LoadObjectById(nota.ID);
                Clonar(nota, notaComplementar);
                notaComplementar.ID = 0;
                notaComplementar.Codigo = GetNovoCodigoNota();
                notaComplementar.Status = "-1";
                notaComplementar.Numero = NovoNumeroSerie(notaComplementar, null);
                notaComplementar.Vendedor = null;
                notaComplementar.NotaComplementada = nota;
                notaComplementar.NotaICMSs = new List<NotaICMS>();
                notaComplementar.NotaItems = new List<NotaItem>();
                notaComplementar.NotaParcelas = new List<NotaParcela>();
                notaComplementar.NotaImpostoServicos = new List<NotaImpostoServico>();
                notaComplementar.NotaItemsServicos = new List<NotaItemServico>();
                notaComplementar.ValorDesconto = 0;
                notaComplementar.PercDesconto = 0;
                notaComplementar.ValorFrete = 0;
                notaComplementar.TotalProduto = 0;
                notaComplementar.TotalNota = 0;
                notaComplementar.ChaveNota = "";
                notaComplementar.NumeroProtocolo = "";
                notaComplementar.NumeroRecibo = "";
            }
            return notaComplementar;
        }

        public IList<Nota> GetAllComplementares(InOutType inOut)
        {
            return repositorioNota.GetAllComplementares(inOut);
        }

        public IList<Nota> GetAllRequisicoesBaixadas(InOutType inOut)
        {
            return repositorioNota.GetAllRequisicoesBaixadas(inOut);
        }

        public IList<Nota> GetNotasPorFilialNoPeriodo(Filial filial, DateTime inicio, DateTime fim)
        {
            return repositorioNota.GetNotasPorFilialNoPeriodo(filial, inicio, fim);
        }

        public IList<Nota> GetNotasValidaParana(Filial filial, DateTime inicio, DateTime fim, bool cupom)
        {
            return repositorioNota.GetNotasValidaParana(filial, inicio, fim, null, cupom);
        }

        public IList<Nota> GetNotasValidaParana(Filial filial, DateTime inicio, DateTime fim, Modelo.InOutType pEntSai, bool cupom)
        {
            return repositorioNota.GetNotasValidaParana(filial, inicio, fim, pEntSai, cupom);
        }

        public IList<Nota> GetNotasItensValidaParana(Filial filial, DateTime inicio, DateTime fim, Modelo.InOutType pEntSai, bool cupom)
        {
            return repositorioNota.GetNotasItensValidaParana(filial, inicio, fim, pEntSai, cupom);
        }

        public Nota GetBySerieENumero(string serie, int numero)
        {
            return repositorioNota.GetBySerieENumero(serie, numero);
        }

        internal IList<Nota> GetNotasPorImpostoEData(ServicoImposto servicoImposto, Nota nota)
        {
            return repositorioNota.GetNotasPorImpostoENota(servicoImposto, nota);
        }

        public IList<pxyCreditoDebito> GetNotasCreDeb(DateTime dtInicio, DateTime dtFinal, int idEmpresa)
        {
            return repositorioNota.GetNotasCreDeb(dtInicio, dtFinal, idEmpresa);
        }

        public IList<Nota> VerificaSeExisteNFSePeloNumeroBD(int numero)
        {
            return repositorioNota.VerificaSeExisteNFSeComNumeroNoBD(numero);
        }

        public int GetNovoLoteNFSe()
        {
            return repositorioNota.GetNovoNumeroLoteNFSe();
        }

        public static void AtualizarDataEnvioEmail(Nota nota)
        {
            nota.DataEnvioNfe = DateTime.Now;
            Instancia.Salvar(nota, Acao.Alterar);
        }


        internal int GetByIdNotaPorIdOSProduto(int idOSOrdemServico)
        {
            return repositorioNota.GetByIdNotaPorIdOSProduto(idOSOrdemServico);
        }

        public IList<Nota> LoadAllByIds(List<int> idsNotas)
        {
            return repositorioNota.LoadAllByIds(idsNotas);
        }

        public void BuscaParametrosImpressaoControleVenda(Nota nota, out Relatorio.dsImpressaoImeis.dtImpressaoControleVendaA5DataTable Dados,
            out List<Microsoft.Reporting.WinForms.ReportParameter> parametros)
        {
            Configuracao objConfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();
            Dados = new Relatorio.dsImpressaoImeis.dtImpressaoControleVendaA5DataTable();
            parametros = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            if (nota != null)
            {
                Nota objNotaSelecionado = NotaController.Instancia.LoadObjectById(nota.ID);
                Dados = new Relatorio.dsImpressaoImeisTableAdapters.dtImpressaoControleVendaA5TableAdapter().GetData(objNotaSelecionado.ID);

                Filial objFilial = objNotaSelecionado.Filial;
                Cidade objCidadeNota = objFilial.Cidade == null ? new Cidade() : objFilial.Cidade;
                UF objEstadoNota = objCidadeNota.UF == null ? new UF() : objCidadeNota.UF;

                Microsoft.Reporting.WinForms.ReportParameter p1 =
                    new Microsoft.Reporting.WinForms.ReportParameter("Empresa", objFilial.Nome ?? String.Empty);
                parametros.Add(p1);

                Microsoft.Reporting.WinForms.ReportParameter p2 =
                    new Microsoft.Reporting.WinForms.ReportParameter("EnderecoEmpresa", objFilial.Endereco ?? String.Empty);
                parametros.Add(p2);

                Microsoft.Reporting.WinForms.ReportParameter p3 =
                    new Microsoft.Reporting.WinForms.ReportParameter("BairroEmpresa", objFilial.Bairro ?? String.Empty);
                parametros.Add(p3);

                Microsoft.Reporting.WinForms.ReportParameter p4 =
                    new Microsoft.Reporting.WinForms.ReportParameter("CidadeEmpresa", objCidadeNota.Nome);
                parametros.Add(p4);

                Microsoft.Reporting.WinForms.ReportParameter p5 =
                    new Microsoft.Reporting.WinForms.ReportParameter("EstadoEmpresa", objEstadoNota.Sigla);
                parametros.Add(p5);

                Microsoft.Reporting.WinForms.ReportParameter p6 =
                    new Microsoft.Reporting.WinForms.ReportParameter("CepEmpresa", objFilial.CEP ?? String.Empty);
                parametros.Add(p6);

                Microsoft.Reporting.WinForms.ReportParameter p7 =
                    new Microsoft.Reporting.WinForms.ReportParameter("TelefoneEmpresa", objFilial.Telefone ?? String.Empty);
                parametros.Add(p7);

                Microsoft.Reporting.WinForms.ReportParameter p8 =
                    new Microsoft.Reporting.WinForms.ReportParameter("NumeroNota", Convert.ToString(objNotaSelecionado.Numero));
                parametros.Add(p8);

                Microsoft.Reporting.WinForms.ReportParameter p9 =
                    new Microsoft.Reporting.WinForms.ReportParameter("Image", objFilial.CaminhoLogoEmpresa);
                parametros.Add(p9);

                Microsoft.Reporting.WinForms.ReportParameter p10 =
                    new Microsoft.Reporting.WinForms.ReportParameter("CnpjEmpresa", objFilial.CNPJ ?? String.Empty);
                parametros.Add(p10);

                Microsoft.Reporting.WinForms.ReportParameter p11 =
                    new Microsoft.Reporting.WinForms.ReportParameter("CondicaoFrenteCaixa", objConfiguracao.CondicaoRelatorioControleVenda ?? String.Empty);
                parametros.Add(p11);

            }
        }

        public int GetNotaChave(string Chave)
        {
            return repositorioNota.GetNotaPelaChave(Chave);
        }

        public IList<Nota> GetNotaAutorizadaPorPedido(int CodigoPedido, bool Autorizada = true)
        {
            return repositorioNota.GetNotaAutorizadaPorPedido(CodigoPedido, Autorizada);
        }

        public IList<pxRelatorioProdutosMaisvendidos> GetProdutosMaisVendidos(int IDEmpresa, int? IDPessoa, DateTime DataInicio, DateTime DataFinal, int Status = 2, string Ativos = "", string modeloDocto = "")
        {
            return repositorioNota.GetProdutosMaisVendidos(IDEmpresa, IDPessoa, DataInicio, DataFinal, Status, Ativos, modeloDocto);
        }


        public IList<pxRelatorioItensCanceladosPDV> GetItensCanceladosPDV(int IDEmpresa, DateTime DataInicial, DateTime DataFinal, int? codigoInicial, int? codigoFinal)
        {
            return repositorioNota.GetItensCanceladosPDV(IDEmpresa, DataInicial, DataFinal, codigoInicial, codigoFinal);
        }

        public int GetNovoCodigoNota()
        {
            return repositorioNota.GetNovoCodigoNota();
        }

        public int GetNovoCodigoFrenteCaixaNota()
        {
            return repositorioNota.GetNovoCodigoFrenteCaixaNota();
        }
    }
}
