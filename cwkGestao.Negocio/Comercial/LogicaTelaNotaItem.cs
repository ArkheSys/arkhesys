using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Faturamento;
using cwkGestao.Negocio.Tributacao;
using cwkGestao.Negocio.Padroes;

using System;
using System.Collections.Generic;
using System.Linq;

namespace cwkGestao.Negocio
{
    public class LogicaTelaNotaItem
    {
        public NotaItem GetNotaItem { get { return notaItem; } }
        private readonly ProdutoController produtoController = ProdutoController.Instancia;
        private readonly NotaItem notaItem;
        private readonly Nota nota;
        private readonly Configuracao _configuracao;
        public InOutType Ent_Sai;
        public Acao Operacao;
        public Pessoa PessoaSelecionada;
        public Filial FilialSelecionada;
        public Produto ProdutoSelecionado;
        public DateTime DtEnvioNota;
        private ITributavel tributavel;
        public TipoNota tipoNota;

        public LogicaTelaNotaItem(NotaItem notaItem)
        {
            _configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            this.notaItem = notaItem;
            this.nota = notaItem.Nota;

            if (notaItem.Produto != null)
                tributavel = NotaBuilder.IniciaTributacaoExistente(notaItem);
        }

        public bool TributacaoLiberadaParaEdicao()
        {
            return nota.Ent_Sai == InOutType.Entrada
                                           || (notaItem.CFOP != null && nota.Ent_Sai == InOutType.Saída && notaItem.CFOP.BDevolucao)
                                           || (nota.NotaComplementada != null);
        }

        /*public string SetProduto(Produto produto)
        {
            try
            {
                SetDadosBasicos(produto);
                SetComissao(produto);
                IniciaTributacao();

                SetValorEValorCalculado();
                SetSubTotalETotal();

                if (nota.Ent_Sai == InOutType.Saída)
                    notaItem.CMVUnit = NotaItemController.Instancia.BuscaValorDoCustoMedio(nota.TipoNota.LocalEstoqueTipoNota, produto);

                if (produto.Unidade.BQtdFracionada)
                    return "N4";
                else
                    return "N0";
            }
            catch (Exception ex)
            {
                notaItem.Produto = null;
                notaItem.Valor = 0;
                throw (ex);
            }
        }*/
        /// <summary>
        /// Este método agora é responsável APENAS por carregar os dados básicos do produto no item da nota.
        /// A tributação foi movida para um método separado para melhor controle.
        /// </summary>
        public string SetProduto(Produto produto)
        {
            try
            {
                SetDadosBasicos(produto); // Transfere dados como nome, unidade, peso, etc.
                SetComissao(produto);     // Define a comissão do vendedor para o item.

                // Define o valor inicial do produto com base no tipo de nota.
                SetValorEValorCalculado();

                // Retorna a máscara de quantidade para o formulário.
                if (produto.Unidade.BQtdFracionada)
                    return "N4";
                else
                    return "N0";
            }
            catch (Exception ex)
            {
                notaItem.Produto = null;
                notaItem.Valor = 0;
                throw (ex);
            }
        }

        /// <summary>
        /// NOVO MÉTODO: Centraliza e executa a rotina de cálculo de tributos.
        /// Ele prepara os totais e invoca a rotina principal do NotaBuilder.
        /// </summary>
        public void RecalcularTributacaoCompleta()
        {
            if (notaItem.Produto == null) return;

            // 1. Garante que o SubTotal e o Total do item estejam corretos antes de calcular impostos.
            decimal totalBruto = notaItem.Quantidade * notaItem.Valor;
            notaItem.ValorDesconto = Math.Round(totalBruto * (notaItem.PercDesconto / 100), 2);
            notaItem.Total = totalBruto - notaItem.ValorDesconto;
            notaItem.SubTotal = notaItem.Total; // Ou conforme sua regra de negócio

            // 2. Inicia o processo, carregando todas as regras, alíquotas e bases.
            tributavel = NotaBuilder.IniciaTributacao(this.GetNotaItem);

            // 3. [A CORREÇÃO] EXECUTA O CÁLCULO FINAL DOS IMPOSTOS.
            // Esta é a linha que faltava. Ela pega as bases e alíquotas e calcula os valores.
            CalculaTributacao();
        }

        private void SetComissao(Produto produto)
        {
            if (nota.Vendedor != null)
            {
                byte tipoComissao; decimal comissaoPorcentagem;
                if (nota.Vendedor.GetComissaoProduto(produto, out tipoComissao, out comissaoPorcentagem))
                {
                    notaItem.TipoComissao = tipoComissao;
                    notaItem.ComissaoPorcentagem = comissaoPorcentagem;
                }
            }
        }

        public void SetSubTotalETotal()
        {
            decimal _total = 0;
            if (notaItem.Nota.TipoNota.Truncar)
            {
                _total = notaItem.Valor * quantidade();
                _total *= 100;
                _total = Math.Truncate(_total);
                _total /= 100;
                _total = _total - notaItem.ValorDesconto;
            }
            else
            {
                _total = Math.Round((notaItem.Valor * quantidade() - notaItem.ValorDesconto), 2);
            }
            notaItem.SubTotal = _total;
            notaItem.Total = notaItem.SubTotal;
        }

        private decimal quantidade()
        {
            if (notaItem.QuantidadeEntrada > 0)
            {
                return notaItem.QuantidadeEntrada;
            }
            else
            {
                return notaItem.Quantidade;
            }
        }

        private void IniciaTributacao()
        {
            if (nota.Pessoa != null && nota.Filial != null && notaItem.Produto != null)
            {
                // =================================================================
                // =========== INÍCIO DO BLOCO DE CÓDIGO DA CORREÇÃO ==============
                // Este bloco força o sistema a recarregar a regra fiscal correta ANTES de calcular.

                var perfilCliente = nota.Pessoa.PerfilTributarioCliente;
                if (perfilCliente == null)
                    throw new InvalidOperationException($"O cliente '{nota.Pessoa.Nome}' não possui um Perfil Tributário definido.");

                var impostos = ImpostosTributosController.Instancia.GetByClassificacaoFiscal(notaItem.Produto.ClassificacaoFiscal.ID, perfilCliente.ID);
                if (impostos == null)
                    throw new InvalidOperationException($"Não foi encontrada regra de 'Impostos e Tributos' para o produto '{notaItem.Produto.Nome}'.");

                // Anexa a regra correta ao produto
                notaItem.Produto.ClassificacaoFiscal.ImpostosTributos = impostos;
                impostos.UsarEssaExcessao = null; // Limpa qualquer exceção antiga

                // Verifica e aplica a exceção para operações interestaduais
                bool dentroDoEstado = nota.Filial.Cidade.UF.Sigla == nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
                if (!dentroDoEstado && impostos.ImpostosTributosExcessoesItens?.Count > 0)
                {
                    var excecao = impostos.ImpostosTributosExcessoesItens
                                            .FirstOrDefault(e => e.UF.Sigla == nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla);
                    if (excecao != null)
                    {
                        impostos.UsarEssaExcessao = excecao;
                    }
                }
                notaItem.VBC_S07 = 0;
                notaItem.BaseICMS = 0;
                notaItem.VBC_O10 = 0;
                notaItem.VBC_Q07 = 0;
                notaItem.BaseICMSSubst = 0;
                notaItem.PMVAST_N19 = 0;
                notaItem.vICMSUFDest_NA15 = 0;
                notaItem.vICMSUFRemet_NA17 = 0;
                notaItem.vBCFCP_N17a = 0;
                notaItem.vBCFCPST_N23a = 0;
                notaItem.AliquotaDiferimento = 0;
                notaItem.ValorIcmsDiferimento = 0;
                notaItem.ValorIcmsOp = 0;

                tributavel = NotaBuilder.IniciaTributacao(notaItem);
            }
        }

        public void CalculaTributacao()
        {
            // A condição !TributacaoLiberadaParaEdicao() foi removida para forçar o cálculo
            if (tributavel != null)
            {
                NotaBuilder.CalculaTributacao(tributavel);
            }
        }
        private void SetValorEValorCalculado()
        {
            decimal preco = 0M;
            if (nota.Ent_Sai == InOutType.Saída)
                if (_configuracao.ReducaoMagento == 1)
                {
                    preco = produtoController.getPreco(notaItem.Produto, nota.TabelaPreco, TipoPrecoType.Normal, nota.Condicao);
                    preco = preco - (notaItem.ValorRetidoICMS + notaItem.VIPI_O14);
                }
                else
                    preco = produtoController.getPreco(notaItem.Produto, nota.TabelaPreco, TipoPrecoType.Normal, nota.Condicao);
            else
            {
                if (nota.TipoNota.TipoValorNotaCompra == TipoValorProduto.CustoMedio)
                    preco = produtoController.BuscaCustoMedio(nota.Filial.ID, notaItem.Produto.ID, nota.Dt);
                else
                    preco = notaItem.Produto.UltimoCusto;
            }
            notaItem.Valor = Math.Round(preco, 2);
            notaItem.ValorCalculado = notaItem.Valor;
        }

        private void SetDadosBasicos(Produto produto)
        {
            notaItem.Produto = produto;
            notaItem.InfAdicionais = produto.InfAdicionais;
            notaItem.ProdutoNome = produto.Nome;
            notaItem.ProdutoDescReduzida = produto.DescReduzida;
            notaItem.orig_N11 = produto.OrigemProd;
            notaItem.cEnq_O06 = produto.ENQ_IPI.ToString();
            notaItem.PesoBruto = produto.PesoBruto;
            notaItem.PesoLiquido = produto.PesoLiquido;
            notaItem.Unidade = produto.Unidade.Sigla;
            notaItem.UnidadeEntrada = produto.UnidadeEntrada.Sigla;
            notaItem.BAtualizaEstoque = nota.TipoNota.BAtualizaEstoque;
            notaItem.cBenef_I05f = produto.CBenef;
            notaItem.indEscala_I05d = produto.EscRelevante;
            notaItem.CNPJFab_I05e = produto.CNPJ_Fab;
        }

        public bool validaUnidadeEntrada(Produto produto, string und)
        {
            IList<pxyConversaoUnidade> undsConversao = ConversaoUnidadeController.Instancia.GetConversaoUnidadeProduto(produto.ID);
            int undEncontrada = undsConversao.Where(x => x.SiglaUnEntrada == und).Count();
            if (undEncontrada > 0)
                return true;
            else
            {
                return false;
            }
        }

        public void SetQuantidade(decimal quantidade)
        {
            if (notaItem.Quantidade != quantidade)
            {
                notaItem.Quantidade = quantidade;
                RecalcularTributacaoCompleta();
            }
        }

        public void SetValor(decimal valor)
        {
            if (notaItem.Valor != valor)
            {
                notaItem.Valor = valor;
                RecalcularTributacaoCompleta();
            }
        }

        public void SetDesconto(decimal desconto)
        {
            if (notaItem.PercDesconto != desconto)
            {
                RecalcularTributacaoCompleta();
            }
        }

        public void SetBaseIcms(decimal baseIcms)
        {
            if (tributavel != null)
            {
                tributavel.IcmsBaseCalculo = baseIcms;
                CalculaTributacao();
            }
        }

        public void SetTextoLei(TextoLei texto)
        {
            notaItem.Texto = texto;
        }

        public void SetInfAdicionais(string texto)
        {
            notaItem.InfAdicionais = texto;
        }

        public void SetAliqIcms(decimal aliquota)
        {
            if (tributavel != null)
            {
                tributavel.IcmsAliquota = aliquota;
                CalculaTributacao();
            }
        }

        public void SetValorIcms(decimal valor)
        {
            if (tributavel != null)
                tributavel.IcmsValor = valor;
        }

        public void SetValorOutrosIcms(decimal valor)
        {
            if (tributavel != null)
                tributavel.IcmsValorOutros = valor;
        }

        public void SetValorIsentoIcms(decimal valor)
        {
            if (tributavel != null)
                tributavel.IcmsValorIsento = valor;
        }

        public void SetValorRetidoIcms(decimal valor)
        {
            if (tributavel != null)
                tributavel.IcmsValorRetido = valor;
        }

        public void SetBaseIcmsSubst(decimal valor)
        {
            if (tributavel != null)
                tributavel.IcmsSTBaseCalculo = valor;
        }

        public void SetPorcetagemCredito(decimal valor)
        {
            if (TributacaoLiberadaParaEdicao())
            {
                notaItem.PCredSN_N29 = valor;
                return;
            }

            if (tributavel != null)
            {
                tributavel.CreditoPercentual = valor;
                CalculaTributacao();
            }
        }

        public void SetValorCsosn(decimal valor)
        {
            if (TributacaoLiberadaParaEdicao())
            {
                notaItem.VCredICMSSN_N30 = valor;
                return;
            }

            if (tributavel != null)
                tributavel.CreditoValor = valor;
        }

        public void SetBaseIpi(decimal valor)
        {
            if (tributavel != null)
            {
                tributavel.IpiBaseCalculo = valor;
                CalculaTributacao();
            }
        }

        public void SetAliqIpi(decimal valor)
        {
            if (tributavel != null)
            {
                tributavel.IpiAliquota = valor;
                CalculaTributacao();
            }
        }

        public void SetValorIpi(decimal valor)
        {
            if (tributavel != null)
                tributavel.IpiValor = valor;
        }

        public void SetBasePis(decimal valor)
        {
            if (tributavel != null)
            {
                tributavel.PisBaseCalculo = valor;
                CalculaTributacao();
            }
        }

        public void SetAliqPis(decimal valor)
        {
            if (tributavel != null)
            {
                tributavel.PisAliquota = valor;
                CalculaTributacao();
            }
        }

        public void SetValorPis(decimal valor)
        {
            if (tributavel != null)
                tributavel.PisValor = valor;
        }

        public void SetBaseCofins(decimal valor)
        {
            if (tributavel != null)
            {
                tributavel.CofinsBaseCalculo = valor;
                CalculaTributacao();
            }
        }

        public void SetAliqCofins(decimal valor)
        {
            if (tributavel != null)
            {
                tributavel.CofinsAliquota = valor;
                CalculaTributacao();
            }
        }

        public void SetValorCofins(decimal valor)
        {
            if (tributavel != null)
                tributavel.CofinsValor = valor;
        }

        public void SetCstCsosn(int cst, bool devolucao)
        {
            if (((notaItem.Nota.Ent_Sai == InOutType.Entrada) || devolucao) && notaItem.CSOSN != cst)
                notaItem.CSOSN = cst;
            NotaBuilder.AtualizaCstIcms(notaItem, cst);
            CalculaTributacao();
        }

        public void SetCfop(TabelaCFOP cfop)
        {
            notaItem.CFOP = cfop;
            CalculaTributacao();
        }

        public void SetCstIpi(int cst)
        {
            NotaBuilder.AtualizaCstIpi(notaItem, cst);
            CalculaTributacao();
        }

        public void SetCstPis(int cst)
        {
            NotaBuilder.AtualizaCstPis(notaItem, cst);
            CalculaTributacao();
        }

        public void SetCstCofins(int cst)
        {
            NotaBuilder.AtualizaCstCofins(notaItem, cst);
            CalculaTributacao();
        }

        public void SetPesoBruto(decimal pesoBruto)
        {
            notaItem.PesoBruto = pesoBruto;
        }

        public void SetPesoLiquido(decimal pesoLiquido)
        {
            notaItem.PesoLiquido = pesoLiquido;
        }

        public void SetI19_nDI(string valor)
        {
            notaItem.I19_nDI = valor;
        }

        public void SetI20_dDI(DateTime valor)
        {
            notaItem.I20_dDI = valor;
        }

        public void SetI21_xLocDesemb(string valor)
        {
            notaItem.I21_xLocDesemb = valor;
        }

        public void SetI22_UFDesemb(string valor)
        {
            notaItem.I22_UFDesemb = valor;
        }

        public void SetI23_dDesemb(DateTime valor)
        {
            notaItem.I23_dDesemb = valor;
        }

        public void SetI24_cExportador(string valor)
        {
            notaItem.I24_cExportador = valor;
        }

        public void SetP02_vBC(decimal valor)
        {
            notaItem.P02_vBC = valor;
        }

        public void SetP03_vDespAdu(decimal valor)
        {
            notaItem.P03_vDespAdu = valor;
        }

        public void SetP04_vII(decimal valor)
        {
            notaItem.P04_vII = valor;
        }

        public void SetP05_vIOF(decimal valor)
        {
            notaItem.P05_vIOF = valor;
        }

        public void SetFatorConversao(decimal fatorConversao)
        {
            if (notaItem.FatorConversao != fatorConversao)
            {
                notaItem.FatorConversao = fatorConversao;
                recalculaQuantidade();
            }
        }

        private void recalculaQuantidade()
        {
            decimal fator;
            decimal qtdEntrada;

#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
            if ((notaItem.FatorConversao == 0) || (notaItem.FatorConversao == null))
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                fator = 1;
            else
                fator = notaItem.FatorConversao;

            if (notaItem.QuantidadeEntrada == 0)
                qtdEntrada = 1;
            else
                qtdEntrada = notaItem.QuantidadeEntrada;

            notaItem.Quantidade = (qtdEntrada / fator);
            SetSubTotalETotal();
        }

        public void SetQuantidadeEntrada(decimal qtdEntrada)
        {
            if (notaItem.QuantidadeEntrada != qtdEntrada)
            {
                notaItem.QuantidadeEntrada = qtdEntrada;
                recalculaQuantidade();
            }
        }

        public void SetUnidadeEntrada(string undEntrada)
        {
            if (notaItem.UnidadeEntrada != undEntrada)
            {
                notaItem.UnidadeEntrada = undEntrada;
            }
        }

        public void SetUnidade(string unidade)
        {
            notaItem.Unidade = unidade;
        }

        public string MascaraUnidadeEntrada(Produto produto)
        {
            if (produto.UnidadeEntrada.BQtdFracionada)
                return "N4";
            else
                return "N0";
        }

        public void SetNumDrawBack(string valor)
        {
            notaItem.NumDrawBack = valor;
        }

        public void SetMotivoDesoneracao(int indice)
        {

            switch (notaItem.TAG_CST)
            {
                case "20":
                    switch (indice)
                    {
                        case 0:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType20.UsoNaAgropecuaria;
                            break;
                        case 1:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType20.Outros;
                            break;
                        case 2:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType20.OrgaoDeFomentoEDesenvolvimentoAgropecuario;
                            break;
                        default:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType20.EmBranco;
                            break;
                    }
                    break;
                case "30":
                    switch (indice)
                    {
                        case 0:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType30.UtilitariosEMotocicletasDaAmazoniaOcidental;
                            break;
                        case 1:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType30.SUFRAMA;
                            break;
                        case 2:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType30.Outros;
                            break;
                        default:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType30.EmBranco;
                            break;
                    }
                    break;
                case "40":
                case "41":
                case "50":
                    switch (indice)
                    {
                        case 0:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.Taxi;
                            break;
                        case 1:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.ProdutorAgropecuario;
                            break;
                        case 2:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.FrotistaLocadora;
                            break;
                        case 3:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.DiplomaticoConsular;
                            break;
                        case 4:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.UtilitariosEMotocicletasDaAmazoniaOcidental;
                            break;
                        case 5:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.SUFRAMA;
                            break;
                        case 6:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.VendaOrgaoPublico;
                            break;
                        case 7:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.Outros;
                            break;
                        case 8:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.DeficienteCondutor;
                            break;
                        case 9:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.DeficienteNaoCondutor;
                            break;
                        default:
                            notaItem.MotivoICMSDesoneracao = (int)NotaItem.MotivoDesoneracaoType40_41_50.EmBranco;
                            break;
                    }
                    break;
            }
        }

        public void SetValorDeson(decimal valor)
        {
            notaItem.ValorICMSDesoneracao = valor;
        }

        public void SetViaTransp(int indice)
        {
            switch (indice)
            {
                case 0:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Maritima;
                    break;
                case 1:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Fluvial;
                    break;
                case 2:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Lacustre;
                    break;
                case 3:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Aerea;
                    break;
                case 4:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Postal;
                    break;
                case 5:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Ferroviaria;
                    break;
                case 6:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.Rodoviaria;
                    break;
                case 7:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.CondutoRedeTransmissao;
                    break;
                case 8:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.MeiosProprios;
                    break;
                case 9:
                    notaItem.ViaTransp = NotaItem.ViaTranspType.EntradaSaidaficta;
                    break;
            }

        }

        public void SetvAFRMM(decimal valor)
        {
            notaItem.ValorFreteRenovacaoMarinhaMercante = valor;
        }
    }
}
