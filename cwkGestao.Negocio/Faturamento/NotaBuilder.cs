using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cwkGestao.Modelo;
using cwkGestao.Negocio.Faturamento.Holders;
using cwkGestao.Negocio.Tributacao;
using cwkGestao.Negocio.Tributacao.Impl;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio.Utils;

namespace cwkGestao.Negocio.Faturamento
{
    public class NotaBuilder
    {
        private List<int> IDsOrdemServico = new List<int>();
        IList<NotaItemPedidoItem> listaNotaItemPedidoItem;
        public Nota Nota { get; private set; }
        public bool CalcularTributacao { get; set; }
        public InOutType EntSai { get; set; }
        public static ICstCalculoProvider CstCalculoProvider { get; set; }
        public List<Imei> listaDeImeis;
        private readonly Configuracao _configuracao;

        public NotaBuilder()
        {
            CalcularTributacao = true;
            EntSai = InOutType.Saída;
            _configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
        }

        public NotaBuilder(Nota nota) : this()
        {
            this.Nota = nota;
        }

        private void IniciarNota()
        {
            if (Nota == null)
                Nota = new Nota();
        }

        public void PreencherDadosBasicosNota(Filial filial, DateTime dataFaturamento, string observacao1, string observacao2, string observacao3, Pessoa Transp, TipoFrete tipoFrete)
        {
            IniciarNota();

            Nota.Codigo = NotaController.Instancia.GetNovoCodigoNota();
            Nota.LoteNFSe = NotaController.Instancia.GetNovoLoteNFSe() + 1;
            Nota.Filial = filial;
            Nota.Ent_Sai = EntSai;
            Nota.Dt = dataFaturamento;
            Nota.DtEnvio = dataFaturamento;
            Nota.DtDigitacao = DateTime.Today;
            Nota.Observacao1 = observacao1.RemoverEspacosDuplosEQuebras();
            Nota.Observacao2 = observacao2.RemoverEspacosDuplosEQuebras();
            Nota.Observacao3 = observacao3.RemoverEspacosDuplosEQuebras();
            Nota.ObservacaoUsuario = String.Empty;
            Nota.ObservacaoUsuario += String.IsNullOrEmpty(Nota.Observacao1) ? "" : Nota.Observacao1 + " ";
            Nota.ObservacaoUsuario += String.IsNullOrEmpty(Nota.Observacao2) ? "" : Nota.Observacao2 + " ";
            Nota.ObservacaoUsuario += String.IsNullOrEmpty(Nota.Observacao3) ? "" : Nota.Observacao3 + " ";
            Nota.ObservacaoUsuario = Nota.ObservacaoUsuario.RemoverEspacosDuplosEQuebras();

            Nota.TipoFrete = (byte)tipoFrete;

            if (Transp != null)
            {
                Nota.TransCidade = CidadeController.Instancia.LoadObjectById(Transp.EnderecoPrincipal().Cidade.ID).Nome;
                Nota.TransCNPJCPF = Transp.CNPJ_CPF;
                Nota.TransEndereco = Transp.EnderecoPrincipal().Endereco;
                Nota.TransNome = Transp.Nome;
                Nota.TransPlaca = Transp.Placa;
                Nota.TransPlacaUF = Transp.PlacaUF;
                Nota.TransUF = Transp.Estado;
                Nota.PessoaTransportadora = Transp;
            }
        }

        public void PreencherNotaComTipoNota(TipoNota tipoNota)
        {
            IniciarNota();

            Nota.ModeloDocto = (int)tipoNota.ModeloDocto;
            Nota.Serie = tipoNota.Serie;
            Nota.BGeraFinanceiro = tipoNota.BGeraFinanceiro;
            Nota.TipoNota = tipoNota;

            var localEstoque = tipoNota.LocalEstoqueTipoNota != null && tipoNota.LocalEstoqueTipoNota?.Filial != null
                ? tipoNota.LocalEstoqueTipoNota
                : LocalEstoqueController.Instancia.LoadObjectById(tipoNota.LocalEstoqueTipoNota.ID);

            Nota.LocalEstoqueNota = localEstoque;
        }

        public void PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(Pessoa cliente, Condicao condicao, Pessoa vendedor, TabelaPreco tabelaPreco, int? Serie)
        {
            IniciarNota();

            Nota.Pessoa = cliente;
            Nota.PessoaNome = cliente.Nome.TrimEnd();
            Nota.PessoaCNPJCPF = cliente.CNPJ_CPF;
            Nota.PessoaInscRG = cliente.Inscricao;
            var endereco = Nota.Pessoa.EnderecoPrincipal();
            Nota.PessoaEndereco = endereco.Endereco;// + (String.IsNullOrEmpty(endereco.Numero) ? "" : ", " + endereco.Numero);
            Nota.PessoaNumero = endereco.Numero;
            Nota.PessoaBairro = endereco.Bairro;
            Nota.PessoaCidade = endereco.Cidade.Nome;
            Nota.PessoaUF = endereco.Cidade.UF.Sigla;
            Nota.PessoaCEP = endereco.CEP;
            Nota.PessoaTelefone = cliente.TelefonePrincipal;
            Nota.PessoaCidadeIBGE = endereco.Cidade.IBGE;
            Nota.Condicao = condicao;

            if (endereco != null && endereco.RemetenteDestinatario)
                Nota.IDEnderecoDestinatario = endereco.ID;

            if (cliente.GetCliente != null)
                Nota.BPessoaContribuinte = cliente.GetCliente.BContribuinte;
            else
                Nota.BPessoaContribuinte = false;

            if (vendedor != null)
                Nota.Vendedor = vendedor.PessoaVendedores.FirstOrDefault();

            Nota.TabelaPreco = tabelaPreco;
            if (Nota.TipoNota.NFOrigem == 0 && Nota.Ent_Sai == InOutType.Entrada)
                Nota.Numero = 0;
            else
                Nota.Numero = NotaController.Instancia.NovoNumeroSerie(Nota, Serie);
            if (Nota.ModeloDocto == 33)
            {
                Nota.Serie = "ecf";
                Nota.Numero = 0;
            }

            // Se for dentro do estado é 1, caso contrário, se for Exportação: 3 senão 2. (1 Interna, 2 Interestadual ou 3 Externa)
            bool dentroDoEstado = Nota.PessoaUF.IsNotNullOrEmpty()
                ? Nota.Filial.Cidade.UF.Sigla == Nota.PessoaUF
                : Nota.Filial.Cidade.UF.Sigla == Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;

            if (dentroDoEstado)
                Nota.idDest = 1;
            else
                Nota.idDest = Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla == "EX" || Nota.Filial.Cidade.UF.Sigla == "EX" ? 3 : 2;
        }

        public void IncluirProdutosNota(IList<IProdutoHolder> produtos, List<int> idOsOrdemServico)
        {
            IDsOrdemServico = idOsOrdemServico;
            IncluirProdutosNota(produtos);
        }

        public void IncluirProdutosNota(IList<IProdutoHolder> produtos)
        {
            IniciarNota();
            IncluirItensAgrupando(produtos);

            if (CalcularTributacao)
            {
                TabelaCFOP tabelaCFOP = GetTabelaCFOP();
                Parallel.ForEach(Nota.NotaItems, notaItem =>
                {
                    CalcularTributacaoNotaItem(tabelaCFOP, notaItem);
                });
            }
        }

        public void AtualizarProdutosNota()
        {
            if (CalcularTributacao)
            {
                TabelaCFOP tabelaCFOP = GetTabelaCFOP();
                foreach (var notaItem in Nota.NotaItems)
                {
                    CalcularTributacaoNotaItem(tabelaCFOP, notaItem);
                }
            }
        }

        private void IncluirItensSemAgrupar(IList<IProdutoHolder> produtos)
        {
            foreach (var pi in produtos.Distinct())
            {
                NotaItem notaItem = CriarNotaItem(pi);
                Nota.NotaItems.Add(notaItem);

            }
        }

        //private void AtualizarStatusPedido()
        //{
        //    try
        //    {
        //        IList<Pedido> pedidos = listaPedidoItens.Select(pi => pi.Pedido).GroupBy(p => p.ID).Select(p => p.First()).ToList();
        //        int qtdeItensPedido = 0;

        //        foreach (Pedido pedido in pedidos)
        //        {
        //            if (pedido.Ent_Sai == 2)
        //            {
        //                qtdeItensPedido = pedido.Items.Count;

        //                if (qtdeItensPedido > 0)
        //                {
        //                    IList<PedidoItem> itensPedidoAbertos = pedido.Items.Where(pi => pi.BFaturado == false).ToList();
        //                    pedido.Items.Where(pi => pi.BFaturado == false).ToList();
        //                    pedido.Status = itensPedidoAbertos.Count == 0 ? StatusOrcamento.Faturado : StatusOrcamento.FaturadoParcial;
        //                }

        //                PedidoController.Instancia.RegisterNewIntoTransaction(pedido);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Erro ao preparar atualização dos status dos pedidos/orçamentos faturados.", e);
        //    }
        //}

        private void IncluirItensAgrupando(IList<IProdutoHolder> produtos)
        {
            listaDeImeis = new List<Imei>();
            IList<Imei> listaDeImeisSelecionados = new List<Imei>();

            try
            {
                listaDeImeisSelecionados = produtos.Where(p => p.PedidoItem.ImeisSelecionado != null).Select(p => p.PedidoItem.ImeisSelecionado).FirstOrDefault()?.ToList();
            }
            catch (Exception ex)
            {
            }

            int indiceIDServico = 0;
            var lista = produtos.Distinct().ToList();

            foreach (var pi in lista)
            {
                NotaItem notaItem = Nota.NotaItems.FirstOrDefault(n => n.Produto.Codigo == pi.Produto.Codigo && n.Valor == pi.Preco);

                if (notaItem == null)
                {
                    notaItem = CriarNotaItem(pi);

                    if (notaItem.Produto != null)
                    {
                        Nota.NotaItems.Add(notaItem);

                        if (listaDeImeisSelecionados != null &&
                            listaDeImeisSelecionados.Select(im => im.Produto).Contains(notaItem.Produto))
                        {
                            IList<Imei> imeis = new List<Imei>();
                            imeis = listaDeImeisSelecionados.Select(im => im).Where(im => im.Produto.ID == notaItem.Produto.ID).ToList();

                            foreach (var item in imeis)
                            {
                                item.NotaItemSaida = notaItem;
                                notaItem.InfAdicionais += notaItem.InfAdicionais == "" ? "IMEI: " + item.CodigoIMEI : ", " + item.CodigoIMEI;
                            }

                            listaDeImeis.AddRange(imeis);
                        }
                    }
                }
                else
                {
                    notaItem.Quantidade += pi.Quantidade;
                    notaItem.SubTotal = notaItem.Quantidade * notaItem.Valor;
                    notaItem.Total = notaItem.SubTotal;

                    CriarNotaItemPedidoItem(pi, notaItem);
                }

                if (IDsOrdemServico.Count > 0)
                    notaItem.IDOSProdutoItem = IDsOrdemServico[indiceIDServico] == null ? 0 : IDsOrdemServico[indiceIDServico];
                else
                    notaItem.IDOSProdutoItem = 0;

                indiceIDServico++;
            }
        }

        private TabelaCFOP GetTabelaCFOP()
        {
            TabelaCFOP tabelaCFOP;
            if (Nota.Filial.Cidade.UF.Sigla != Nota.PessoaUF)
            {
                if (Nota.BPessoaContribuinte == true)
                    tabelaCFOP = Nota.TipoNota.Operacao.CFOP_FEstado_C;
                else
                    tabelaCFOP = Nota.TipoNota.Operacao.CFOP_FEstado_NC;
            }
            else
                tabelaCFOP = Nota.TipoNota.Operacao.CFOP_DEstado;
            return tabelaCFOP;
        }

        private NotaItem CriarNotaItem(IProdutoHolder pi)
        {
            NotaItem notaItem = new NotaItem();

            if (pi.PedidoItem != null)
            {
                if (pi.PedidoItem.BFaturado == false)
                    notaItem = CriaNotaItem(pi);
            }
            else
                notaItem = CriaNotaItem(pi);

            return notaItem;
        }

        private NotaItem CriaNotaItem(IProdutoHolder pi)
        {
            var config = ConfiguracaoController.Instancia.GetConfiguracao();

            NotaItem notaItem = new NotaItem();

            notaItem.Nota = Nota;
            notaItem.Sequencia = Nota.NotaItems.Count + 1;
            notaItem.Produto = pi.Produto;
            if (pi.PedidoItem == null)
            {
                notaItem.ProdutoNome = pi.Produto.Nome.RemoverEspacosDuplosEQuebras();
            }
            else if (pi.PedidoItem.NomeMagento != null)
            {
                notaItem.ProdutoNome = pi.PedidoItem.NomeMagento;
            }
            else
            {
                notaItem.ProdutoNome = pi.Produto.Nome.RemoverEspacosDuplosEQuebras();

            }

            if (pi.PedidoItem != null && notaItem.Nota.TipoNota.BAtualizaPedido)
            {
                pi.PedidoItem.BFaturado = true;

                if (!config.BExcluirOrcamentoFaturado)
                {
                    CriarNotaItemPedidoItem(pi, notaItem);
                }
            }

            notaItem.ProdutoDescReduzida = pi.Produto.DescReduzida.RemoverEspacosDuplosEQuebras();
            notaItem.Quantidade = pi.Quantidade;
            notaItem.Unidade = pi.Unidade.RemoverEspacosDuplosEQuebras();
            if (ehNotaEntrada())
            {
                notaItem.QuantidadeEntrada = pi.Quantidade;
                notaItem.UnidadeEntrada = pi.Unidade.RemoverEspacosDuplosEQuebras();
            }
            notaItem.ValorCalculado = pi.Preco;
            notaItem.Valor = pi.Preco;
            notaItem.PercDesconto = pi.DescontoPerc;
            notaItem.SubTotal = pi.Preco * pi.Quantidade;
            notaItem.Total = pi.Total;

            if (pi.PedidoItem?.ValorDesconto > 0)
                notaItem.ValorDesconto = pi.PedidoItem?.ValorDesconto ?? 0;
            else
                notaItem.ValorDesconto = notaItem.SubTotal - notaItem.Total;

            notaItem.PesoBruto = pi.Produto.PesoBruto;
            notaItem.PesoLiquido = pi.Produto.PesoLiquido;
            notaItem.Dt = pi.Data;
            notaItem.BAtualizaEstoque = Nota.TipoNota.BAtualizaEstoque;
            notaItem.Ent_Sai = (int)Nota.Ent_Sai;
            notaItem.InfAdicionais = pi.InformacoesAdicionais == null ? String.Empty : pi.InformacoesAdicionais.RemoverEspacosDuplosEQuebras();
            notaItem.CalcCustoMedio = Nota.TipoNota.BCalcCustoMedio;
            notaItem.ValorCustoMedio = 0;
            notaItem.orig_N11 = pi.Produto.OrigemProd;
            notaItem.cEnq_O06 = pi.Produto.ENQ_IPI.ToString();
            notaItem.CMVUnit = 0; //ProdutoController.Instancia.BuscaCustoMedio(Nota.Filial.ID, pi.Produto.ID, Nota.Dt);
            notaItem.CNPJFab_I05e = pi.Produto.CNPJ_Fab;
            notaItem.indEscala_I05d = pi.Produto.EscRelevante;
            notaItem.cBenef_I05f = pi.Produto.CBenef;


            var _estoque = PedidoItemEstoqueController.Instancia.GetAll();


            if (Nota.TipoNota.BAtualizaPedido)
            {
                if (config.UtilizaIdentificadorEstoque)
                {
                    foreach (var itemEstoque in _estoque)
                    {
                        if (itemEstoque.PedidoItem?.ID == pi.PedidoItem?.ID)
                        {
                            NotaItemEstoque notaItemEstoque = new NotaItemEstoque();
                            notaItemEstoque.Identificador = itemEstoque.Identificador;
                            notaItemEstoque.Quantidade = itemEstoque.Quantidade;
                            notaItemEstoque.CMVUnit = itemEstoque.CMVUnit;
                            notaItemEstoque.NotaItem = notaItem;

                            notaItem.Estoques.Add(notaItemEstoque);
                        }

                    }

                    if (pi.PedidoItem?.Pedido.Status == StatusOrcamento.AguardandoFaturamento)
                    {
                        var identificadoresEstoque = NotaItemController.Instancia.SaldoEstoqueLocalEstoqueListaIdentificador(Nota.TipoNota.LocalEstoqueTipoNota.ID, pi.Produto.ID, Nota.ID, false);

                        if (pi.Quantidade > identificadoresEstoque.Sum(s => s.Saldo))
                        {
                            var mensagem = "Quantidade de estoque disponível é menor que a quantidade vendida para o produto\n " + pi.Produto.Nome;
                            throw new Exception(mensagem);
                        }

                        var quantidadeRemanescente = notaItem.Quantidade;
                        while (quantidadeRemanescente > 0)
                        {
                            var identificador = identificadoresEstoque.First();
                            var notaItemEstoque = new NotaItemEstoque { NotaItem = notaItem };

                            notaItemEstoque.Identificador = new Identificador { ID = identificador.IdIdentificador };
                            notaItem.Estoques.Add(notaItemEstoque);
                            notaItemEstoque.CMVUnit = identificador.CustoUnitario;
                            if (quantidadeRemanescente >= identificador.SaldoDisponivel)
                            {
                                notaItemEstoque.Quantidade = identificador.SaldoDisponivel;
                                identificadoresEstoque.Remove(identificador);
                            }
                            else
                            {
                                notaItemEstoque.Quantidade = quantidadeRemanescente;
                                identificador.QuantidadeBaixada += notaItemEstoque.Quantidade;
                            }
                            quantidadeRemanescente -= notaItemEstoque.Quantidade;
                        }
                    }
                }
            }

            byte tipoComissao; decimal comissaoPorcentagem;

            if (Nota.Vendedor != null)
            {
                var vendedor = Nota.Vendedor != null && Nota.Vendedor.Pessoa != null
                    ? Nota.Vendedor
                    : PessoaVendedorController.Instancia.LoadObjectById(Nota.Vendedor.ID);

                Nota.Vendedor = PessoaVendedorController.Instancia.LoadObjectById(vendedor.ID);
                if (Nota.Vendedor.GetComissaoProduto(notaItem.Produto, out tipoComissao, out comissaoPorcentagem))
                {
                    notaItem.TipoComissao = tipoComissao;
                    notaItem.ComissaoPorcentagem = comissaoPorcentagem;
                }
            }
            return notaItem;
        }

        private void CriarNotaItemPedidoItem(IProdutoHolder pi, NotaItem notaItem)
        {
            NotaItemPedidoItem objNiPi = new NotaItemPedidoItem();
            objNiPi.NotaItem = notaItem;
            objNiPi.PedidoItem = pi.PedidoItem;
            objNiPi.QtdRetiradaPedido = pi.Quantidade;

            if (objNiPi.PedidoItem != null)
                objNiPi.PedidoItem.BFaturado = true;

            notaItem.PedidoItens.Add(objNiPi);

            NotaItemPedidoItemController.Instancia.RegisterNewIntoTransaction(objNiPi);
        }

        public IList<NotaItem> IncluiNotaItem(IList<ContratoProduto> cps)
        {
            NotaItem notaItem;
            //Parallel.ForEach(cps, cp =>
            //{
            foreach (var cp in cps)
            {
                //notaItem = new NotaItem();
                notaItem = CriarNotaItem(cp);
                Nota.NotaItems.Add(notaItem);
            }
            //});
            CriarFinanceiroNotaItemContratoProduto(Nota.NotaItems);
            return Nota.NotaItems;
        }

        private NotaItem CriarNotaItem(ContratoProduto cp)
        {
            NotaItem notaItem = new NotaItem();
            notaItem.Nota = Nota;
            notaItem.Sequencia = Nota.NotaItems.Count + 1;
            notaItem.Produto = cp.Produto;
            notaItem.ProdutoNome = cp.Produto.Nome;
            notaItem.ProdutoDescReduzida = cp.Produto.DescReduzida.RemoverEspacosDuplosEQuebras();
            notaItem.Quantidade = cp.Quantidade;
            notaItem.Unidade = cp.Produto.Unidade.Sigla.ToString().RemoverEspacosDuplosEQuebras();
            notaItem.ValorCalculado = cp.Valor;
            notaItem.Valor = cp.Valor;
            notaItem.PercDesconto = cp.PercDesconto;
            notaItem.SubTotal = cp.Total;
            notaItem.Total = cp.Total;
            notaItem.PesoBruto = cp.Produto.PesoBruto;
            notaItem.PesoLiquido = cp.Produto.PesoLiquido;
            notaItem.Dt = cp.DataInicioFaturamento;
            notaItem.BAtualizaEstoque = Nota.TipoNota.BAtualizaEstoque;
            notaItem.Ent_Sai = (int)Nota.Ent_Sai;
            notaItem.CalcCustoMedio = Nota.TipoNota.BCalcCustoMedio;
            notaItem.ValorCustoMedio = 0;
            notaItem.orig_N11 = cp.Produto.OrigemProd;
            notaItem.cEnq_O06 = cp.Produto.ENQ_IPI.ToString();
            //notaItem.CMVUnit = ProdutoController.Instancia.BuscaCustoMedio(Nota.Filial.ID, cp.Produto.ID, Nota.Dt);
            notaItem.InfAdicionais = cp.InfAdicionais.RemoverEspacosDuplosEQuebras();
            notaItem.ContratoProduto = cp;
            notaItem.cBenef_I05f = cp.Produto.CBenef;
            return notaItem;
        }

        public void CriarFinanceiroNotaItemContratoProduto(IList<NotaItem> notaItens)
        {
            TabelaCFOP tabelaCFOP = GetTabelaCFOP();
            foreach (var notaItem in notaItens)
            {
                CalcularTributacaoNotaItem(tabelaCFOP, notaItem);
            }
        }

        private bool ehNotaEntrada()
        {
            return (Nota.Ent_Sai == InOutType.Entrada && !ehDevolucao()) || (Nota.Ent_Sai == InOutType.Saída && ehDevolucao());
        }

        private bool ehDevolucao()
        {
            if (Nota.TipoNota.Operacao.CFOP_DEstado.BDevolucao ||
                            Nota.TipoNota.Operacao.CFOP_FEstado_C.BDevolucao ||
                            Nota.TipoNota.Nome.Contains("DEVOLUCAO") ||
                            Nota.TipoNota.Nome.Contains("Devolucao"))
            {
                return true;
            }
            else return false;
        }

        private void CalcularTributacaoNotaItem(TabelaCFOP tabelaCFOP, NotaItem notaItem)
        {
            //SetaCamposIcms(notaItem);
            //SetaCamposIpi(notaItem);
            //SetaCamposPis(notaItem);
            //SetaCamposCofins(notaItem);

            notaItem.CFOP = notaItem.Produto.CFOP ?? tabelaCFOP;

            IniciaTributacao(notaItem);

            if (notaItem.ValorICMSDesoneracao > 0 && notaItem.MotivoICMSDesoneracao == 0)
                notaItem.MotivoICMSDesoneracao = 9;

            notaItem.SitTrib = Nota.Filial.SimplesNacional != 0
                                        ? (notaItem.Produto.OrigemProd + notaItem.TAG_CST)
                                        : notaItem.TAG_CST;
        }

        public static ITributavel IniciaTributacao(NotaItem notaItem)
        {
            TextoLei textoLei = new TextoLei();

            //AtualizaCsts(notaItem, notaItem.Produto.CST_IPI, notaItem.Produto.CST_Cofins, notaItem.Produto.CST_Pis);

            if (notaItem.Texto != null)
                textoLei = TextoLeiController.Instancia.LoadObjectById(notaItem.Texto.ID);

            notaItem.TextoLei = textoLei.DescricaoTextoLei;

            var mediator = new Tributacao.Impl.NotaItemTributadaMediator(notaItem, notaItem.Nota);
            SetaCamposCofins(notaItem);
            SetaCamposIcms(notaItem);
            SetaCamposIpi(notaItem);
            SetaCamposPis(notaItem);
            SetaCamposFCP(notaItem);
            CalculaTributacao(mediator);
            return mediator;
        }

        public static ITributavel IniciaTributacaoExistente(NotaItem notaItem)
        {
            return new Tributacao.Impl.NotaItemTributadaMediator(notaItem, notaItem.Nota);
        }
        public Boolean Alterada(NotaItem notaItem)
        {
            bool alterada = false;

            return alterada;
        }

        public static void CalculaTributacao(ITributavel mediator)
        {
            var tributacao = new Tributacao.Tributacao(mediator);

            tributacao.CalculaIpi();
            tributacao.CalculaIcms();
            tributacao.CalculaPis();
            tributacao.CalculaCofins();
            tributacao.CalculaCredito();
            tributacao.CalculaFCP(mediator);
        }

        public static void AtualizaCsts(NotaItem notaItem, int indiceIpi, int indiceCofins, int indicePis)
        {
            AtualizaCstIcms(notaItem, notaItem.Produto.Tributacao);
            AtualizaCstIpi(notaItem, indiceIpi);
            AtualizaCstCofins(notaItem, indiceCofins);
            AtualizaCstPis(notaItem, indicePis);
        }

        public static void AtualizaCstPis(NotaItem notaItem, int indicePis)
        {
            SetaCamposPis(notaItem);
            notaItem.CST_Pis = ConversorCsts.IndiceParaCstPis(indicePis);
        }

        public static void AtualizaCstCofins(NotaItem notaItem, int indiceCofins)
        {
            SetaCamposCofins(notaItem);
            notaItem.CST_Cofins = ConversorCsts.IndiceParaCstCofins(indiceCofins);
        }

        public static void AtualizaCstIpi(NotaItem notaItem, int indiceIpi)
        {
            SetaCamposIpi(notaItem);
            notaItem.CST_Ipi = ConversorCsts.IndiceParaCstIpi(indiceIpi);
        }

        public static void AtualizaCstIcms(NotaItem notaItem, int indiceCst)
        {
            if (notaItem.CSOSN != indiceCst)
                SetaCamposIcms(notaItem);
            else
                notaItem.TAG_CST = TagCst(notaItem.Nota, indiceCst, notaItem.Nota.Filial, notaItem.Nota.Pessoa, indiceCst);
        }

        private static void SetaCamposCofins(NotaItem notaItem)
        {
            notaItem.CST_Cofins = notaItem.Produto.PerfilPisCofins.SaiPisCofinsNaturezaOperacao.TabelaCOFINS.COFINS;
            notaItem.PCOFINS_S08 = notaItem.Produto.PerfilPisCofins.AliquotaCOFINS;
            NCM ncm = null;
            try
            {
                ncm = NCMController.Instancia.LoadObjectById(notaItem.Produto.ID_NCM);
                notaItem.Cest = ncm.Cest;
            }
            catch (Exception)
            {
                ncm = null;
            }

            //if (notaItem.Nota.Ent_Sai == InOutType.Saída)
            //{
            //    if (notaItem.Produto.PCOFINS_S08 != 0)
            //    {
            //        notaItem.PCOFINS_S08 = notaItem.Produto.PCOFINS_S08;
            //    }
            //    else
            //    {
            //        if (notaItem.Produto.NCM != null && ncm != null)
            //        {
            //            notaItem.PCOFINS_S08 = ncm.Cofins;

            //            //Validação apenas para quando o cst começa com 01, que tem apenas 1 dígito por ser um inteiro.
            //            if (ncm.CST_Cofins.ToString().Length == 1)
            //                notaItem.CST_Cofins = "0" + ncm.CST_Cofins.ToString();
            //            else
            //                notaItem.CST_Cofins = ncm.CST_Cofins.ToString();
            //        }
            //        else
            //        {
            //            notaItem.PCOFINS_S08 = 0;
            //            if (ncm != null)
            //            {
            //                if (ncm.CST_Cofins.ToString().Length == 1)
            //                    notaItem.CST_Cofins = "0" + ncm.CST_Cofins.ToString();
            //                else
            //                    notaItem.CST_Cofins = ncm.CST_Cofins.ToString();
            //            }
            //            else
            //            {
            //                notaItem.CST_Cofins = notaItem.Produto.CST_Cofins.ToString(); ;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    notaItem.PCOFINS_S08 = notaItem.Produto.PCOFINS_S08;
            //}
        }

        private static void SetaCamposPis(NotaItem notaItem)
        {
            NCM ncm = null;
            try
            {
                ncm = NCMController.Instancia.LoadObjectById(notaItem.Produto.ID_NCM);
            }
            catch (Exception)
            {
                ncm = null;
            }
            notaItem.CST_Pis = notaItem.Produto.PerfilPisCofins.SaiPisCofinsNaturezaOperacao.TabelaPIS.PIS;
            notaItem.PPIS_Q08 = notaItem.Produto.PerfilPisCofins.AliquotaPIS;
            //if (notaItem.Nota.Ent_Sai == InOutType.Saída)
            //{
            //    if (notaItem.Produto.PPIS_Q08 != 0)
            //    {
            //        notaItem.PPIS_Q08 = notaItem.Produto.PPIS_Q08;
            //    }
            //    else
            //    {
            //        if (notaItem.Produto.NCM != null && ncm != null)
            //        {

            //            notaItem.PPIS_Q08 = ncm.Pis;

            //            if (ncm.CST_Pis.ToString().Length == 1)
            //                notaItem.CST_Pis = "0" + ncm.CST_Pis.ToString();
            //            else
            //                notaItem.CST_Pis = ncm.CST_Pis.ToString();

            //        }
            //        else
            //        {
            //            notaItem.PPIS_Q08 = 0;
            //            if (ncm != null)
            //            {
            //                if (ncm.CST_Pis.ToString().Length == 1)
            //                    notaItem.CST_Pis = "0" + ncm.CST_Pis.ToString();
            //                else
            //                    notaItem.CST_Pis = ncm.CST_Pis.ToString();
            //            }
            //            else
            //            {
            //                notaItem.CST_Pis = notaItem.Produto.CST_Pis.ToString(); ;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    notaItem.PPIS_Q08 = notaItem.Produto.PPIS_Q08;
            //}
        }

        private static void SetaCamposIpi(NotaItem notaItem)
        {
            NCM ncm = null;
            try
            {
                ncm = NCMController.Instancia.LoadObjectById(notaItem.Produto.ID_NCM);
            }
            catch (Exception)
            {
                ncm = null;
            }

            notaItem.CST_Ipi = ConversorCsts.IndiceParaCstIpi(notaItem.Produto.CST_IPI);
            notaItem.PIPI_O13 = notaItem.Produto.AliquotaIPI;

            //if (notaItem.Nota.Ent_Sai == InOutType.Saída)
            //{
            //    if (notaItem.Produto.AliquotaIPI != 0)
            //    {
            //        notaItem.PIPI_O13 = notaItem.Produto.AliquotaIPI;
            //    }
            //    else
            //    {
            //        if (notaItem.Produto.NCM != null && ncm != null)
            //        {

            //            notaItem.PIPI_O13 = ncm.Ipi;

            //            if (ncm.CST_Ipi.ToString().Length == 1)
            //                notaItem.CST_Ipi = "0" + ncm.CST_Ipi.ToString();
            //            else
            //                notaItem.CST_Ipi = ncm.CST_Ipi.ToString();
            //        }
            //        else
            //        {
            //            notaItem.PIPI_O13 = 0;
            //            if (ncm != null)
            //            {
            //                if (ncm.CST_Ipi.ToString().Length == 1)
            //                    notaItem.CST_Ipi = "0" + ncm.CST_Ipi.ToString();
            //                else
            //                    notaItem.CST_Ipi = ncm.CST_Ipi.ToString();
            //            }
            //            else
            //            {
            //                notaItem.CST_Ipi = notaItem.Produto.CST_IPI.ToString();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    notaItem.PIPI_O13 = notaItem.Produto.AliquotaIPI;
            //}
        }

        private static void SetaCamposIcms(NotaItem notaItem)
        {
            //IcmsBase tributacaoIcmsOriginal = NotaItemController.Instancia.RecuperaIcmsOriginal(notaItem);

            var impostostributos = notaItem.Produto.ClassificacaoFiscal.ImpostosTributos;
            if (impostostributos.UsarEssaExcessao != null)
            {
                notaItem.pICMSInter = impostostributos.UsarEssaExcessao.PIcms;
                notaItem.AliqICMS = impostostributos.UsarEssaExcessao.PIcms;
                notaItem.ReducaoImposto = impostostributos.UsarEssaExcessao.PReducaoIcms;
                notaItem.PRedBC_N14 = impostostributos.UsarEssaExcessao.PReducaoIcms;


                notaItem.modBC_N13 = impostostributos.ModBaseCalculoIcms;
                notaItem.modBCST_N18 = impostostributos.ModBaseCalculoIcms;

                notaItem.AliquotaDiferimento = impostostributos.UsarEssaExcessao.PDiferimento;
                notaItem.AliqInterna = impostostributos.UsarEssaExcessao.PIcms;
                notaItem.CSOSN = Convert.ToInt32(ConversorCsts.IndiceParaTAG_CSTTributacao(impostostributos.UsarEssaExcessao.CSTCSOSNIcms));
                notaItem.PRedBCST_N20 = impostostributos.UsarEssaExcessao.PReducaoIcmsSt;
                notaItem.PMVAST_N19 = impostostributos.UsarEssaExcessao.PMva;
                notaItem.PICMSST_N22 = impostostributos.UsarEssaExcessao.PIcmsSt;

                notaItem.TAG_CST = ConversorCsts.IndiceParaTAG_CSTTributacao(impostostributos.UsarEssaExcessao.CSTCSOSNIcms);
                notaItem.CFOP = impostostributos.UsarEssaExcessao.TabelaCFOP;
                notaItem.InfAdicionais = impostostributos.UsarEssaExcessao.InfAdicionais;
            }
            else
            {

                notaItem.pICMSInter = impostostributos.PIcmsInterno;
                notaItem.AliqICMS = impostostributos.PIcmsInterno;
                notaItem.ReducaoImposto = impostostributos.PReducaoIcms;
                notaItem.PRedBC_N14 = impostostributos.PReducaoIcms;


                notaItem.modBC_N13 = impostostributos.ModBaseCalculoIcms;
                notaItem.modBCST_N18 = impostostributos.ModBaseCalculoIcms;

                notaItem.AliquotaDiferimento = impostostributos.PDeferimento;
                notaItem.AliqInterna = impostostributos.PIcmsInterno;
                notaItem.CSOSN = Convert.ToInt32(ConversorCsts.IndiceParaTAG_CSTTributacao(impostostributos.CSTCSOSNIcms));
                notaItem.PRedBCST_N20 = impostostributos.PReducaoIcmsST;
                notaItem.PMVAST_N19 = impostostributos.PMVA;
                notaItem.PICMSST_N22 = impostostributos.PReducaoIcmsST;

                notaItem.TAG_CST = ConversorCsts.IndiceParaTAG_CSTTributacao(impostostributos.CSTCSOSNIcms);
                bool dentroDoEstado = notaItem.Nota.PessoaUF.IsNotNullOrEmpty()
                    ? notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.PessoaUF
                    : notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;

                if (dentroDoEstado)
                {
                    notaItem.CFOP = impostostributos.CfopDentroDoEstado;
                }
                else
                {
                    notaItem.CFOP = impostostributos.CfopForaDoEstado;
                }

            }


            //notaItem.BaseICMS
            notaItem.vICMSUFDest_NA15 = 0;
            notaItem.vICMSUFRemet_NA17 = 0;
            //notaItem.AliqICMS = dentroDoEstado != true ? tributacaoIcmsOriginal.AliqContrib : notaItem.AliqInterna;

            //var entraDifal = notaItem.AliqICMS != 0 && notaItem.AliqICMSNormal != 0;

            //if (!dentroDoEstado && entraDifal && notaItem.Nota.Pessoa.bConsumidorFinal)
            //{
            //    notaItem.BaseICMS = notaItem.SubTotal + notaItem.RateioOutrasDespesas + notaItem.RateioFrete + notaItem.RateioSeguro + notaItem.RAT_Acrescimo - notaItem.RAT_Desconto;

            //    var difal = ((notaItem.AliqInterna / 100) * notaItem.BaseICMS) - ((notaItem.AliqICMS / 100) * notaItem.BaseICMS);
            //    if (difal < 0)
            //        difal = difal * -1;

            //    var PercentualPartilhaDest = PegarPercentualPartilhaDest();
            //    notaItem.vICMSUFDest_NA15 = (PercentualPartilhaDest * difal / 100);
            //    notaItem.vICMSUFRemet_NA17 = ((100 - PercentualPartilhaDest) * difal / 100);
            //}

            //if (tributacaoIcmsOriginal.TextoContribuinte != null)
            //{
            //    notaItem.Texto = tributacaoIcmsOriginal.TextoContribuinte;
            //}


            //if (notaItem.Nota.BPessoaContribuinte.Value)
            //{
            //    notaItem.pICMSInter = tributacaoIcmsOriginal.AliqContrib;
            //    notaItem.AliqICMS = tributacaoIcmsOriginal.AliqContrib;
            //    notaItem.ReducaoImposto = tributacaoIcmsOriginal.ReducaoImpostoCont;
            //    notaItem.PRedBC_N14 = tributacaoIcmsOriginal.ReducaoContrib;

            //    if (tributacaoIcmsOriginal.TextoContribuinte != null)
            //    {
            //        notaItem.Texto = tributacaoIcmsOriginal.TextoContribuinte;
            //    }
            //}
            //else
            //{
            //    notaItem.pICMSInter = tributacaoIcmsOriginal.AliqNContrib;
            //    notaItem.AliqICMS = tributacaoIcmsOriginal.AliqNContrib;
            //    notaItem.ReducaoImposto = tributacaoIcmsOriginal.ReducaoImpostoNCont;
            //    notaItem.PRedBC_N14 = tributacaoIcmsOriginal.ReducaoNContrib;

            //    if (tributacaoIcmsOriginal.TextoNaoContribuinte != null)
            //    {
            //        notaItem.Texto = tributacaoIcmsOriginal.TextoNaoContribuinte;
            //    }
            //}

            //notaItem.modBC_N13 = tributacaoIcmsOriginal.ModBCICMS;
            //notaItem.modBCST_N18 = tributacaoIcmsOriginal.ModBCST;

            //notaItem.AliquotaDiferimento = tributacaoIcmsOriginal.AliqDiferimento;
            //notaItem.AliqInterna = tributacaoIcmsOriginal.AliqInterna;
            //notaItem.CSOSN = tributacaoIcmsOriginal.CSOSN;
            //notaItem.PRedBCST_N20 = tributacaoIcmsOriginal.PRedBCST;
            //notaItem.PMVAST_N19 = tributacaoIcmsOriginal.LucroST;
            //notaItem.PICMSST_N22 = tributacaoIcmsOriginal.AliqSubstTributaria;

            //notaItem.TAG_CST = TagCst(notaItem.Nota, tributacaoIcmsOriginal.CSOSN, notaItem.Nota.Filial, notaItem.Nota.Pessoa, tributacaoIcmsOriginal.Tributacao);

            //SetaTabelaCfop(notaItem);

            //bool dentroDoEstado = notaItem.Nota.PessoaUF.IsNotNullOrEmpty()
            //    ? notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.PessoaUF
            //    : notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;

            //notaItem.AliqICMS = dentroDoEstado != true ? tributacaoIcmsOriginal.AliqContrib : notaItem.AliqInterna;

            //var entraDifal = notaItem.AliqICMS != 0 && notaItem.AliqICMSNormal != 0;

            //if (!dentroDoEstado && entraDifal && notaItem.Nota.Pessoa.bConsumidorFinal)
            //{
            //    notaItem.BaseICMS = notaItem.SubTotal + notaItem.RateioOutrasDespesas + notaItem.RateioFrete + notaItem.RateioSeguro + notaItem.RAT_Acrescimo - notaItem.RAT_Desconto;

            //    var difal = ((notaItem.AliqInterna / 100) * notaItem.BaseICMS) - ((notaItem.AliqICMS / 100) * notaItem.BaseICMS);
            //    if (difal < 0)
            //        difal = difal * -1;

            //    var PercentualPartilhaDest = PegarPercentualPartilhaDest();
            //    notaItem.vICMSUFDest_NA15 = (PercentualPartilhaDest * difal / 100);
            //    notaItem.vICMSUFRemet_NA17 = ((100 - PercentualPartilhaDest) * difal / 100);
            //}
        }

        private static void SetaCamposFCP(NotaItem notaItem)
        {
            IcmsBase tributacaoIcmsOriginal = NotaItemController.Instancia.RecuperaIcmsOriginal(notaItem);

            //Não Contribuinte
            notaItem.pFCP_N17b = tributacaoIcmsOriginal.AliqFCPNormal;
        }
        private static decimal PegarPercentualPartilhaDest()
        {
            DateTime dataAgora = DateTime.Now;
            int ano = dataAgora.Year;

            switch (ano)
            {
                case 2016: return 40;
                case 2017: return 60;
                case 2018: return 80;
                case 2019: return 100;
                default: return 100;
            }
        }

        private static void SetaTabelaCfop(NotaItem notaItem)
        {
            //bool dentroDoEstado = notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;

            bool dentroDoEstado = notaItem.Nota.PessoaUF.IsNotNullOrEmpty()
                ? notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.PessoaUF
                : notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;

            bool produtoComST = EhParaCalcularSubstTributaria(notaItem.TAG_CST);
            var _TipoNota = TipoNotaController.Instancia.LoadObjectById(notaItem.Nota.TipoNota.ID);

            TabelaCFOP cfopCorrespondente = dentroDoEstado ? notaItem.Produto.CFOP : notaItem.Produto.CFOPForaDoEstado;

            if (cfopCorrespondente != null)
            {
                notaItem.CFOP = cfopCorrespondente;
            }
            else
            {
                if (dentroDoEstado)
                {
                    notaItem.CFOP = produtoComST ? _TipoNota.Operacao.CFOP_DEstado_ST : _TipoNota.Operacao.CFOP_DEstado;
                }
                else
                {
                    if (notaItem.Nota.BPessoaContribuinte.Value)
                    {
                        notaItem.CFOP = produtoComST ? _TipoNota.Operacao.CFOP_FEstado_C_ST : _TipoNota.Operacao.CFOP_FEstado_C;
                    }
                    else
                    {
                        notaItem.CFOP = produtoComST ? _TipoNota.Operacao.CFOP_FEstado_NC_ST : _TipoNota.Operacao.CFOP_FEstado_NC;
                    }
                }

                if (notaItem.TAG_CST == "500")
                {
                    if (notaItem.Nota.Filial.Cidade.UF.Sigla == notaItem.Nota.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla)
                    {
                        notaItem.CFOP = _TipoNota.Operacao.CFOP_DEstado_ST;
                    }
                    else if (notaItem.Nota.Pessoa.BContribuinte)
                    {
                        notaItem.CFOP = _TipoNota.Operacao.CFOP_FEstado_C_ST;
                    }
                    else
                    {
                        notaItem.CFOP = _TipoNota.Operacao.CFOP_FEstado_NC_ST;
                    }
                }
            }
        }

        private static string TagCst(Nota nota, Produto produto, Filial filial, Pessoa cliente)
        {
            return TagCst(nota, produto.CSOSN, filial, cliente, produto.Tributacao);
        }

        private static string TagCst(Nota nota, int csosn, Filial filial, Pessoa cliente, int indiceCst)
        {
            bool dentroDoEstado = nota.PessoaUF.IsNotNullOrEmpty()
                ? filial.Cidade.UF.Sigla == nota.PessoaUF
                : filial.Cidade.UF.Sigla == cliente.EnderecoPrincipal().Cidade.UF.Sigla;

            bool cadastroRapido = string.IsNullOrEmpty(cliente.EnderecoPrincipal()?.Endereco);

            bool adesaoParcial = ConfiguracaoController.Instancia.GetTipoNfe() == AdesaoNFe.Parcial;

            if (!(nota.TipoNota.Ent_Sai == (int)InOutType.Entrada && nota.TipoNota.NFOrigem == 0))
            {
                if (cliente.BOrgaoPublico && nota.ModeloDocto != 55 && nota.ModeloDocto != 0)
                {
                    throw new Exception("Não é possível faturar uma nota para orgão público que não seja NFe");
                }
                if (!dentroDoEstado && !cadastroRapido && nota.ModeloDocto != 55 && nota.ModeloDocto != 0)
                {
                    throw new Exception("Não é possível faturar uma nota interestadual que não seja NFe");
                }
            }

            ConversorCsts.ModeloNota modeloNota = (nota.ModeloDocto != 55 && ((adesaoParcial && dentroDoEstado) || //venda dentro do estado
                                                    filial.SimplesNacional != 0)) //empresa normal 
                                                    ? ConversorCsts.ModeloNota.Modelo1
                                                    : ConversorCsts.ModeloNota.NFe;

            ConversorCsts.STEmpresa stEmpresa = filial.SimplesNacional == 0 ? ConversorCsts.STEmpresa.RegimeSimples : ConversorCsts.STEmpresa.RegimeNormal;

            return ConversorCsts.IndiceParaTributacao(indiceCst, csosn, modeloNota, stEmpresa, EhParaCalcularSubstTributaria(nota.NotaItems.Count > 0 ? nota.NotaItems[0].TAG_CST : "00"));
        }

        public void SetTotaisNota()
        {
            IniciarNota();
            var config = ConfiguracaoController.Instancia.GetConfiguracao();

            NotaICMSController.Instancia.AtualizarICMS(Nota);

            if (config.ReducaoMagento == 1)
            {
                decimal valorImpostos = 0;
                foreach (var itemNota in Nota.NotaItems)
                {
                    decimal quantidade = 0;
                    if (itemNota.QuantidadeEntrada > 0)
                        quantidade = itemNota.QuantidadeEntrada;
                    else
                        quantidade = itemNota.Quantidade;

                    valorImpostos = (itemNota.VIPI_O14 + itemNota.ValorRetidoICMS) / quantidade;
                    itemNota.ValorCalculado = itemNota.ValorCalculado - (valorImpostos);
                    itemNota.Valor = itemNota.Valor - (valorImpostos);
                    itemNota.Total = itemNota.Total - (itemNota.VIPI_O14 + itemNota.ValorRetidoICMS);
                }
            }

            Nota.TotalProduto = Nota.NotaItems.Sum(n => n.SubTotal);

            Nota.TotalServico = Nota.NotaItemsServicos.Sum(n => n.SubTotal);
            Nota.ValorAcrescimo = 0;
            Nota.PercDesconto = ((Nota.TotalProduto > 0) ? Nota.ValorDesconto / Nota.TotalProduto * 100 : 0);
            Nota.PercDesconto = Math.Round(Nota.PercDesconto, 6);

            decimal icmsretido, valorIPI;
            Nota.RecalcularRateios();
            if (Nota.Filial.TipoST == (int)TipoST.Substituído)
            {
                icmsretido = 0;
            }
            else
            {
                if (Nota.NotaICMSs.Sum(n => n.ValorSubst) < 0)
                    icmsretido = 0;
                else
                    icmsretido = Nota.NotaICMSs.Sum(n => n.ValorSubst);
            }

            valorIPI = Nota.NotaICMSs.Sum(n => n.ValorIPI);

            Nota.TotalNota = Nota.TotalProduto + Nota.TotalServico + icmsretido + valorIPI - Nota.ValorDesconto + Nota.ValorFrete + Nota.ValorSeguro + Nota.OutrasDespesas + Nota.ValorAcrescimo - Nota.BuscaValorICMSDesoneracao(_configuracao.NaoSomarIcmsDesonTotalNota);
            Nota.CalcularCustoUnit(config);
        }

        public void SetParcelas(IList<FinanceiroParcela> parcelas)
        {
            decimal valorIpi = Nota.NotaItems.Sum(i => i.VIPI_O14);
            int qtdParcelasNaoAlteradas = parcelas.Where(i => i.BAlterado == false).Count();
            decimal valorIpiPorParcela = qtdParcelasNaoAlteradas > 0 ? Math.Round(valorIpi / qtdParcelasNaoAlteradas, 2) : valorIpi;

            foreach (FinanceiroParcela financeiroParcela in parcelas)
            {
                NotaParcela notaParcela = new NotaParcela();

                notaParcela.BAlterado = financeiroParcela.BAlterado;
                notaParcela.Banco = financeiroParcela.Banco;
                notaParcela.BEntrada = financeiroParcela.BEntrada;
                notaParcela.CpfCnpj = financeiroParcela.CpfCnpj;
                notaParcela.Emitente = financeiroParcela.Emitente;
                notaParcela.NomeBanco = financeiroParcela.NomeBanco;
                notaParcela.Nota = Nota;
                notaParcela.NumAgencia = financeiroParcela.NumAgencia;
                notaParcela.NumBanco = financeiroParcela.NumBanco;
                notaParcela.NumCheque = financeiroParcela.NumCheque;
                notaParcela.NumContaCorrente = financeiroParcela.NumContaCorrente;
                notaParcela.Parcela = financeiroParcela.Parcela;
                notaParcela.TipoDocumento = financeiroParcela.TipoDocumento;

                notaParcela.FormaPagamento = financeiroParcela.FormaPagamento;
                notaParcela.TipoFormaPagamento = financeiroParcela.TipoFormaPagamento;

                notaParcela.Valor = financeiroParcela.Valor;
                notaParcela.Vencimento = financeiroParcela.Vencimento;

                notaParcela.BandeiraCartao = financeiroParcela.BandeiraCartao;
                notaParcela.Operadora = financeiroParcela.Operadora;
                notaParcela.CAut = financeiroParcela.CAut;

                if (!financeiroParcela.BAlterado)
                    notaParcela.Valor += valorIpiPorParcela;

                Nota.NotaParcelas.Add(notaParcela);
            }

            decimal somaParcelas = Nota.NotaParcelas.Sum(i => Math.Round(i.Valor, 2));
            if (somaParcelas != Nota.TotalNota)
            {
                var Diferenca = Nota.TotalNota - somaParcelas;

                if (somaParcelas - Nota.TotalNota > 0)
                    foreach (var n in Nota.NotaParcelas)
                        if (n.FormaPagamento == ParcelaBase.EnumFormaPagamento.Dinheiro)
                            n.ValorTroco = somaParcelas - Nota.TotalNota;

                if (Nota.NotaParcelas.Count > 0)
                    Nota.NotaParcelas[0].Valor += Diferenca;
            }
        }

        private static bool EhParaCalcularSubstTributaria(string cstIcms)
        {
            try
            {
                return CstCalculoProvider.SubstituicaoTributariaCsts.Contains(cstIcms);
            }
            catch
            {
                CstCalculoProvider = new CstCalculoProviderFromXml();
            }
            return CstCalculoProvider.SubstituicaoTributariaCsts.Contains(cstIcms);
        }
    }
}
