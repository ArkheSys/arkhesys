using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Exceptions;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Faturamento;
using cwkGestao.Negocio.Utils;

using static cwkGestao.Modelo.ParcelaBase;

namespace cwkGestao.Negocio.Comercial
{
    public class FinalizadorVendaFrenteCaixa
    {
        private readonly FrenteCaixaNota frenteCaixaNota;
        private readonly TabelaPreco objTabelaPreco;
        private readonly Filial objFilial;
        private readonly TipoPedido objTipoOrcamento;
        private readonly TipoDocumento objTipoDocumento;
        private readonly Pessoa objPessoa;
        private readonly Pessoa objVendedor;
        private readonly Condicao objCondicao;
        private readonly List<pxyFormaPagamentoBaixaDoc> formasDePagamento;

        private readonly PedidoController controllerPedido = PedidoController.Instancia;

        public FinalizadorVendaFrenteCaixa(FrenteCaixaNota _frenteCaixaNotaFinalizar, List<pxyFormaPagamentoBaixaDoc> _formasPagamento, Configuracao _configuracao)
        {
            frenteCaixaNota = _frenteCaixaNotaFinalizar;
            var configuracao = _configuracao;
            ValideConfiguracao(configuracao);
            objTabelaPreco = configuracao.TabelaPrecoFrenteCaixa;
            
            _frenteCaixaNotaFinalizar.IDFilial = _configuracao.FilialFrenteCaixa.ID;
            int idFilial = _frenteCaixaNotaFinalizar.IDFilial.Value;

            objFilial = FilialController.Instancia.LoadObjectById(idFilial);
            objTipoOrcamento = _frenteCaixaNotaFinalizar.TipoPedido;
            objPessoa = _frenteCaixaNotaFinalizar.Pessoa ?? configuracao.ClienteFrenteCaixa;
            objVendedor = _frenteCaixaNotaFinalizar.Vendedor;
            objTipoDocumento = configuracao.TipoDocumentoFrenteCaixa;
            objCondicao = _frenteCaixaNotaFinalizar.Condicao;
            formasDePagamento = _formasPagamento;
        }

        private void ValideConfiguracao(Configuracao configuracao)
        {
            if (configuracao.TabelaPrecoFrenteCaixa == null
                || configuracao.FilialFrenteCaixa == null
                || configuracao.TipoOrcamentoFrenteCaixa == null
                || configuracao.ClienteFrenteCaixa == null
                || configuracao.TipoDocumentoFrenteCaixa == null)
                throw new ConfiguracaoIncorretaException();
        }

        public Nota FinalizeVenda(Pedido objPedido, bool VemDoDelivery, bool GerarFinanceiro, bool FaturarPedido, int? Serie)
        {
            if (objTipoOrcamento.TipoNota.ModeloDocto == TipoNota.ModeloDocumento.CupomFiscal)
                VerifiqueCupomNumeroZero();

            IList<FinanceiroParcela> financeiroParcelas = GereFinanceiroParcelas(objPedido);

            TipoPedido tipoPedido = objPedido.TipoPedido;
            TipoNota tipoNota = new TipoNota();
            if (tipoPedido != null && tipoPedido.TipoNota != null) tipoNota = TipoNotaController.Instancia.LoadObjectById(objPedido.TipoPedido.TipoNota.ID);

            var objNota = new Nota();
            if (objTipoOrcamento.Tipo == TipoPedidoType.Pedido)
            {
                objNota = TenteFaturarPedido(objPedido, tipoNota, financeiroParcelas, VemDoDelivery, GerarFinanceiro, FaturarPedido, Serie);

                if (objTipoOrcamento.TipoNota.ModeloDocto == TipoNota.ModeloDocumento.CupomFiscal)
                    ImprimaNotaCupom(objNota);
            }

            return objNota;
        }

        private void VerifiqueCupomNumeroZero()
        {
            var nota = NotaController.Instancia.GetBySerieENumero("ecf", 0);
            if (nota != null)
                throw new FechamentoFrenteCaixaException("Existe um cupom cadastrado com o número zero (0)."
                                                         + Environment.NewLine + " Corrija esse cupom para poder finalizar o processo.", null);
        }

        public Pedido GerePedidoComPedidoExistente(Pedido objPedido, bool PDVNovo)
        {
            objPedido.Items.Clear();
            CriePedidoItens(objPedido);
            CriePedidoParcelas(objPedido, PDVNovo);
            return objPedido;
        }

        public Pedido GerePedido(int Ent_Sai, bool PDVNovo)
        {
            Pedido objPedido = InicializePedido();
            objPedido.Ent_Sai = Ent_Sai;

            CriePedidoItens(objPedido);
            CriePedidoParcelas(objPedido, PDVNovo);

            //Está aqui para pegar o código e o número o mais perto possível da hora de salvar
            objPedido.Codigo = controllerPedido.MaxCodigo();
            objPedido.Numero = controllerPedido.MaxNumero(objPedido.Filial, objPedido.TipoPedido);

            return objPedido;
        }

        public void SalvaPedido(Pedido objPedido, bool CarregandoPedido = false)
        {
            if (CarregandoPedido)
            {
                foreach (var Item in PedidoController.Instancia.GetItensDoPedido(objPedido.ID))
                    PedidoItemController.Instancia.Salvar(Item, Acao.Excluir);

                foreach (var Item in PedidoParcelaController.Instancia.GetParcelasPedido(objPedido))
                    PedidoParcelaController.Instancia.Salvar(Item, Acao.Excluir);
            }

            // point
            Dictionary<string, string> erros = controllerPedido.Salvar(objPedido, objPedido.ID == 0 ? Acao.Incluir : Acao.Alterar);
            if (erros.Count > 0) throw new FechamentoFrenteCaixaException(MonteMensagemErroSalvarPedido(erros), null);
        }

        private Nota TenteFaturarPedido(Pedido objPedido, TipoNota tipoNota, IList<FinanceiroParcela> listaParcelas, bool VemDoDelivery, bool GerarFinanceiro, bool FaturarPedido, int? Serie)
        {
            try
            {
                List<Pedido> listaPedido = new List<Pedido> { objPedido };
                Orcamento faturador = new Orcamento(listaPedido, listaParcelas, objPedido.Dt, objPedido.Condicao, tipoNota, "", "", "", null, formasDePagamento, true, VemDoDelivery, GerarFinanceiro, FaturarPedido, Serie);
                return faturador.Nota;
            }
            catch (Exception ex)
            {
                if (objPedido.ID > 0) controllerPedido.Salvar(objPedido, Acao.Excluir);
                throw new FechamentoFrenteCaixaException("Não foi possível faturar o pedido gerado.", ex);
            }
        }

        private void ImprimaNotaCupom(Nota objNota)
        {
            List<pxyFormaPagamentoBaixaDoc> objForma = new List<pxyFormaPagamentoBaixaDoc>();
            foreach (var item in formasDePagamento)
                objForma.Add(new pxyFormaPagamentoBaixaDoc
                {
                    FormaPagamento = item.FormaPagamento,
                    Valor = item.Valor
                });

            CupomFiscal cupomFiscal = new CupomFiscal(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));

            cupomFiscal.Imprime(objNota, objForma);
        }

        private string MonteMensagemErroSalvarPedido(Dictionary<string, string> erros)
        {
            StringBuilder mensagem = new StringBuilder("Não foi possível gerar pedido.\n");
            foreach (var item in erros) mensagem.AppendLine(item.Key + ": " + item.Value);
            return mensagem.ToString();
        }

        private List<FinanceiroParcela> GereFinanceiroParcelas(Pedido objPedido)
        {
            List<FinanceiroParcela> listaParcelas = new List<FinanceiroParcela>();
            foreach (var parcela in objPedido.Parcelas)
            {
                FinanceiroParcela objFinanceiroParcela = new FinanceiroParcela();
                objFinanceiroParcela.BEntrada = parcela.BEntrada;
                objFinanceiroParcela.DtContabil = objPedido.Dt;
                objFinanceiroParcela.Parcela = parcela.Parcela;
                objFinanceiroParcela.Valor = parcela.Valor;
                objFinanceiroParcela.Vencimento = parcela.Vencimento;

                objFinanceiroParcela.TipoDocumento = parcela.TipoDocumento;
                objFinanceiroParcela.TipoFormaPagamento = parcela.TipoFormaPagamento;
                objFinanceiroParcela.FormaPagamento = parcela.FormaPagamento;

                objFinanceiroParcela.BandeiraCartao = parcela.BandeiraCartao;
                objFinanceiroParcela.Operadora = parcela.Operadora;
                objFinanceiroParcela.CAut = parcela.CAut;
                objFinanceiroParcela.BAlterado = true;

                listaParcelas.Add(objFinanceiroParcela);
            }

            ArredondarValoresParcelas(listaParcelas, objPedido);

            return listaParcelas;
        }

        private void ArredondarValoresParcelas(List<FinanceiroParcela> listaParcelas, Pedido objPedido)
        {
            decimal diferencaValores = objPedido.TotalPedido - listaParcelas.Sum(v => Math.Round(v.Valor,2));

            if(diferencaValores !=0)
            {
                if (listaParcelas != null && listaParcelas.Count > 0)
                    listaParcelas[0].Valor += diferencaValores; // Ajusta valor na primeira parcela
            }
        }

        public void CriePedidoParcelas(Pedido objPedido, bool PDVNovo)
        {
            if (objPedido.Parcelas == null)
                objPedido.Parcelas = new List<PedidoParcela>();

            CondicaoController.Instancia.GerarParcelas(objPedido).ForEach(p => objPedido.Parcelas.Add(p));
            foreach (var Forma in formasDePagamento)
                foreach (var ParcelaPedido in objPedido.Parcelas)
                    if (Forma.Parcela == ParcelaPedido.Parcela)
                    {
                        ParcelaPedido.Valor = Forma.Valor;
                        ParcelaPedido.BandeiraCartao = Forma.BandeiraCartao;
                        ParcelaPedido.Operadora = Forma.Operadora;
                        ParcelaPedido.CAut = Forma.CAut;
                        ParcelaPedido.BAlterado = true;
                    }

            if (PDVNovo)
            {
                var formasFiltradas = formasDePagamento.Where(o => o.Valor > 0).ToList();
                var parcelas = new List<PedidoParcela>();
                foreach (var Forma in formasFiltradas)
                {
                    var parcelaEncontrada = objPedido.Parcelas.FirstOrDefault(o => o.TipoDocumento.ID == Forma.TipoDocumento.ID);
                    var condicaoParcela = objCondicao.CondicaoParcelas.FirstOrDefault(o => o.TipoDocumento.ID == parcelaEncontrada.TipoDocumento.ID);

                    if (Forma.QuantidadeParcelas > 1)
                    {
                        for (int i = 0; i < Forma.QuantidadeParcelas; i++)
                        {
                            var p = new PedidoParcela
                            {
                                Parcela = parcelas.Count + 1,
                                Pedido = objPedido,
                                Valor = Math.Round((Forma.Valor / Forma.QuantidadeParcelas),2),
                                BandeiraCartao = Forma.BandeiraCartao,
                                Operadora = Forma.Operadora,
                                CAut = Forma.CAut,
                                BAlterado = true,
                                BEntrada = condicaoParcela.BAVista,
                                FormaPagamento = ConversorFormaPagamento.GetFormaPagamento(Forma.FormaPagamento.CodigoSefaz),
                                TipoDocumento = Forma.TipoDocumento,
                                Vencimento = parcelaEncontrada?.Vencimento ?? DateTime.Now,
                                Previsao = parcelaEncontrada?.Previsao ?? DateTime.Now
                            };

                            if (condicaoParcela.TipoDt == "Dia")
                                p.Vencimento = p.Vencimento.AddDays(condicaoParcela.DiaMes * i);
                            else
                                p.Vencimento = p.Vencimento.AddMonths(condicaoParcela.DiaMes * i);

                            parcelas.Add(p);
                        }
                    }
                    else
                    {
                        // Adiciona a parcela pq não tem divisão
                        parcelas.Add(new PedidoParcela
                        {
                            Parcela = parcelas.Count + 1,
                            Pedido = objPedido,
                            Valor = Forma.Valor,
                            BandeiraCartao = Forma.BandeiraCartao,
                            Operadora = Forma.Operadora,
                            CAut = Forma.CAut,
                            BAlterado = true,
                            BEntrada = condicaoParcela.BAVista,
                            FormaPagamento = ConversorFormaPagamento.GetFormaPagamento(Forma.FormaPagamento.CodigoSefaz),
                            TipoDocumento = Forma.TipoDocumento,
                            Vencimento = parcelaEncontrada?.Vencimento ?? DateTime.Now,
                            Previsao = parcelaEncontrada?.Previsao ?? DateTime.Now
                        });
                    }
                }

                VerificarValorParcelas(ref parcelas);
                objPedido.Parcelas = parcelas;
            }
        }

        private void VerificarValorParcelas(ref List<PedidoParcela> parcelas)
        {
            decimal valorParcelas = Math.Round(formasDePagamento.Sum(p => p.Valor), 2);
            decimal valorLancado = Math.Round(parcelas.Sum(p => p.Valor), 2);
            if (valorParcelas == valorLancado)
                return;

            decimal valorDiferenca = valorParcelas - valorLancado;
            parcelas[0].Valor += valorDiferenca;
        }

        private void CriePedidoItens(Pedido objPedido)
        {
            int seq = 0;
            foreach (var item in frenteCaixaNota.ListaFrenteCaixaNotaItem)
            {
                if (item.Cancelado)
                    continue;

                seq++;

                PedidoItem objPedidoItem = new PedidoItem();
                objPedidoItem.Pedido = objPedido;
                objPedidoItem.bReservaEstoque = objPedido.TipoPedido.BReservaEstoque;
                objPedidoItem.Dt = objPedido.Dt;
                objPedidoItem.Ent_Sai = objPedido.TipoPedido.Ent_Sai;
                objPedidoItem.Produto = item.Produto;
                objPedidoItem.ProdutoNome = item.Produto.Nome;
                objPedidoItem.ProdutoDescReduzida = item.Produto.DescReduzida;
                objPedidoItem.Quantidade = item.Quantidade;
                objPedidoItem.Sequencia = seq;
                objPedidoItem.SubTotal = item.Quantidade * item.Valor;
                objPedidoItem.Total = item.Total;
                objPedidoItem.Unidade = item.Produto.Unidade.Sigla;
                objPedidoItem.Valor = item.Valor;
                objPedidoItem.ValorCalculado = item.ValorCalculado;
                if (objPedidoItem.ValorCalculado == 0)
                    objPedidoItem.ValorCalculado = item.Valor;

                objPedidoItem.ValorRetidoICMS = 0;
                objPedidoItem.PercDesconto = item.PercDesconto;
                if (objPedidoItem.PercDesconto > 0)
                {
                    if (item.Desconto > 0)
                        objPedidoItem.ValorDesconto = item.Desconto;
                    else
                        objPedidoItem.ValorDesconto = item.Quantidade * (item.ValorCalculado - item.Valor); // TODO: Analisar melhor o desconto, saindo zerado quando carrega o pedido
                }
                else
                    objPedidoItem.ValorDesconto = 0;

                objPedido.Items.Add(objPedidoItem);
            }
        }

        private Pedido InicializePedido()
        {
            Pedido objPedido = new Pedido();
            AtribuaPessoa(objPedido);
            objPedido.Filial = objFilial;
            objPedido.TipoPedido = objTipoOrcamento;
            objPedido.Dt = DateTime.Now; //System.DateTime.Today;
            objPedido.DtPrazoEntrega = objPedido.Dt;
            objPedido.Validade = 0;
            objPedido.DtDigitacao = objPedido.Dt;
            objPedido.TabelaPreco = objTabelaPreco;
            objPedido.Vendedor = frenteCaixaNota.Vendedor ?? objVendedor;
            objPedido.Condicao = objCondicao;
            objPedido.TotalProduto = frenteCaixaNota.TotalProdutos;
            objPedido.ValorDesconto = frenteCaixaNota.Desconto;
            objPedido.PercDesconto = frenteCaixaNota.PercDesconto;
            objPedido.TotalPedido = frenteCaixaNota.TotalGeral;
            objPedido.LocalEstoquePedido = objPedido.TipoPedido.TipoNota.LocalEstoqueTipoNota;
            objPedido.Ent_Sai = 2;
            objPedido.TipoFrete = TipoFrete.SemFrete;
            return objPedido;
        }

        private void AtribuaPessoa(Pedido objPedido)
        {
            objPedido.Pessoa = objPessoa;
            AtribuaEnderecoPessoa(objPedido, objPedido.Pessoa);
            objPedido.PessoaCNPJCPF = objPedido.Pessoa.CNPJ_CPF;
            objPedido.PessoaInscRG = objPedido.Pessoa.TipoPessoa == "Física" ? objPedido.Pessoa.RG : objPedido.Pessoa.Inscricao;
            objPedido.PessoaNome = objPedido.Pessoa.Nome;
            objPedido.PessoaTelefone = objPedido.Pessoa.TelefonePrincipal;
        }

        private void AtribuaEnderecoPessoa(Pedido objPedido, Pessoa pessoa)
        {
            try
            {
                PessoaEndereco enderecoPrincipal = pessoa.EnderecoPrincipal();
                objPedido.PessoaBairro = enderecoPrincipal.Bairro;
                objPedido.PessoaCEP = enderecoPrincipal.CEP;
                objPedido.PessoaCidade = enderecoPrincipal.Cidade.Nome;
                objPedido.PessoaEndereco = enderecoPrincipal.Endereco;
                if (!string.IsNullOrEmpty(enderecoPrincipal.Numero))
                    objPedido.PessoaEndereco += ", " + enderecoPrincipal.Numero;
                objPedido.PessoaUF = enderecoPrincipal.Cidade.UF.Sigla;
            }
            catch
            {
                throw new Exception("Erro ao recuperar os dados do cliente, favor conferir o cadastro do endereço principal deste cliente.");
            }
        }
    }
}