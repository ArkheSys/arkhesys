using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Exceptions;
using cwkGestao.Negocio.Faturamento.Holders;
using cwkGestao.Negocio.Faturamento;
#pragma warning disable CS0105 // A diretiva using para "System.Linq" apareceu anteriormente neste namespace
using System.Linq;
#pragma warning restore CS0105 // A diretiva using para "System.Linq" apareceu anteriormente neste namespace
using cwkGestao.Negocio.Padroes;
using System.Windows.Forms;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public partial class LogicaTelaNotaFiscal
    {
        private Boolean EhNotaDuplicada { get; set; }

        private Configuracao _configuracao;

        public Identificador Identificador { get; set; }
        public Nota GetNota { get { return nota; } }
        public Nota nota;
        private readonly NotaController notaController = NotaController.Instancia;
        private readonly DocumentoController documentoController = DocumentoController.Instancia;
        public bool EhNotaComplementar { get; set; }
        private Dictionary<string, decimal> valoresNota = new Dictionary<string, decimal>();
        public NotaItem nItem { get; set; }

        private readonly List<string> erros = new List<string>();
        public bool ExistemErros { get { return erros.Count > 0; } }

        /// <summary>
        /// Retorna uma lista com os erros encontrados nas operações da classe. 
        /// Após a chamada dessa propriedade todos erros armazenados são excluídos.
        /// </summary>
        public List<string> GetErros
        {
            get
            {
                var retorno = new List<string>();
                retorno.AddRange(erros.ToList());
                erros.Clear();
                return retorno;
            }
        }

        public int CaracteresRestantesObservacao { get; private set; }

        public LogicaTelaNotaFiscal(Nota nota) : this(nota, false)
        {
            //this.nota = nota;
            //tributacaoServico = new cwkGestao.Negocio.Tributacao.TributacaoServico(this.nota);
            //EhNotaComplementar = false;
            //EhNotaDuplicada = false;
        }

        public LogicaTelaNotaFiscal(Nota nota, Boolean ehNotaDuplicada)
        {
            this.nota = nota;
            tributacaoServico = new cwkGestao.Negocio.Tributacao.TributacaoServico(this.nota);
            EhNotaComplementar = false;
            EhNotaDuplicada = ehNotaDuplicada;
            _configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
        }

        #region Tipo Nota

        public void SetTipoNota(TipoNota tipoNota)
        {
            if (nota.TipoNota == null || nota.TipoNota.ID != tipoNota.ID || EhNotaDuplicada)
            {
                nota.TipoNota = tipoNota;
                nota.ModeloDocto = (int)tipoNota.ModeloDocto;
                nota.Ent_Sai = (InOutType)tipoNota.Ent_Sai;
                nota.BGeraFinanceiro = tipoNota.BGeraFinanceiro;
                nota.LocalEstoqueNota = tipoNota.LocalEstoqueTipoNota;

                foreach (var item in nota.NotaItems)
                {
                    item.Ent_Sai = tipoNota.Ent_Sai;
                    item.BAtualizaEstoque = tipoNota.BAtualizaEstoque;
                    item.CalcCustoMedio = tipoNota.BCalcCustoMedio;
                }

                SetSerieENumero();
                GerarParcelas();
            }
        }

        public bool NumeroGeradoAutomatico
        {
            get
            {
                try
                {
                    return ((nota.TipoNota.Ent_Sai == (int)InOutType.Saída) || ((nota.TipoNota.Ent_Sai == (int)InOutType.Entrada) && (nota.TipoNota.NFOrigem == (int)OrigemNFType.Própria)));
                }
                catch
                {
                    return false;
                }
            }
        }

        private bool SerieENumeroDevemSerGerados
        {
            get
            {
                return NumeroGeradoAutomatico && (String.IsNullOrEmpty(nota.Serie) || nota.Serie != nota.TipoNota.Serie);
            }
        }

        private void SetSerieENumero()
        {
            //if (SerieENumeroDevemSerGerados)
            //{
            nota.Serie = nota.TipoNota.Serie;
            nota.Numero = notaController.NovoNumeroSerie(nota, null);
            //}
        }

        #endregion

        public void SetFilial(Filial filial)
        {
            nota.Filial = filial;
        }

        public void SetPessoa(Pessoa pessoa, PessoaEndereco endereco)
        {
            nota.Pessoa = pessoa;
            nota.PessoaCNPJCPF = pessoa.CNPJ_CPF;
            nota.PessoaInscRG = pessoa.RG;
            nota.PessoaNome = pessoa.Nome;
            nota.PessoaTelefone = pessoa.TelefonePrincipal;

            nota.IDEnderecoDestinatario = endereco.ID;
            nota.PessoaBairro = endereco.Bairro;
            nota.PessoaEndereco = endereco.Endereco;
            nota.PessoaNumero = endereco.Numero;
            nota.Complemento = endereco.Complemento;
            nota.PessoaCEP = endereco.CEP;

            try
            {
                nota.PessoaCidade = endereco.Cidade.Nome;
                nota.PessoaCidadeIBGE = endereco.Cidade.IBGE;
                nota.PessoaUF = endereco.Cidade.UF.Sigla;
                nota.Complemento = endereco.Complemento;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Verifique o endereço da pessoa selecionada.", "Erro");
                return;
            }


            if (pessoa.BCliente)
                nota.BPessoaContribuinte = pessoa.GetCliente.BContribuinte;
            else
                nota.BPessoaContribuinte = false;

            if (nota.NotaItemsServicos.Count > 0)
                AtualizaImpostosServico();

            bool dentroDoEstado = nota.Filial.Cidade.UF.Sigla == endereco.Cidade.UF.Sigla;
            if (dentroDoEstado)
            {
                nota.idDest = 1;
            }
            else
            {
                if ((nota.Filial.Cidade.UF.Sigla != "EX") &&
                    (endereco.Cidade.UF.Sigla == "EX") ||
                    (nota.Filial.Cidade.UF.Sigla == "EX") &&
                    (endereco.Cidade.UF.Sigla != "EX"))
                {
                    nota.idDest = 3;
                }
                else
                {
                    nota.idDest = 2;
                }
            }

            if (nota.TipoNota == null? false: nota.TipoNota.DestinoSempreInterno)
                nota.idDest = 1;
        }

        public void SetIntermediario(Pessoa pessoa)
        {

            try
            {
                nota.Intermediario = pessoa;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Verifique o Intermediário selecionado.", "Erro");
                return;
            }


            if (pessoa.BCliente)
                nota.BPessoaContribuinte = pessoa.GetCliente.BContribuinte;
            else
                nota.BPessoaContribuinte = false;

            if (nota.NotaItemsServicos.Count > 0)
                AtualizaImpostosServico();
        }

        public void SetCondicao(Condicao condicao)
        {
            if (nota.Condicao == null || nota.Condicao.ID != condicao.ID || condicao.Variacao != nota.Condicao.Variacao)
            {
                var condicaoAntiga = nota.Condicao;
                nota.Condicao = condicao;

                if (condicaoAntiga != null && condicao.Variacao != condicaoAntiga.Variacao)
                {
                    if (nota.Ent_Sai == InOutType.Saída)
                    {

                        nota.NotaItems = nota.NotaItems.Select(ni =>
                        {
                            LogicaTelaNotaItem logica = new LogicaTelaNotaItem(ni); logica.SetProduto(ni.Produto); return ni;
                        }).ToList();
                    }
                }

                GerarParcelas();
                AtualizaSomatoriosTotais();
            }
        }

        private void GerarParcelas()
        {
            if (PodeGerarParcelas())
            {
                try
                {
                    nota.NotaParcelas.Clear();
                    foreach (var parcela in CondicaoController.Instancia.GerarParcelas(nota))
                    {
                        nota.NotaParcelas.Add(parcela);
                    }
                }
                catch (Exception ex)
                {
                    erros.Add(ex.Message);
                }
            }
        }

        public void AtualizarParcelas()
        {
            if (PodeAtualizarParcelas())
            {
                try
                {
                    CondicaoController.Instancia.AtualizaParcelasNota(nota);
                }
                catch (Exception ex)
                {
                    erros.Add(ex.Message);
                }
            }
            else
                nota.NotaParcelas.Clear();
        }

        private bool PodeGerarParcelas()
        {
            return nota.TotalNota > 0 && nota.Condicao != null && nota.Dt.Year > 1800;
        }

        private bool PodeAtualizarParcelas()
        {
            return PodeGerarParcelas() && nota.NotaParcelas.Count > 0;
        }

        public void SetTabelaPreco(TabelaPreco tabelaPreco)
        {
            nota.TabelaPreco = tabelaPreco;
        }

        public void SetVendedor(Pessoa vendedor)
        {
            nota.Vendedor = vendedor.GetVendedor;
        }

        public void AtualizaTotalNotaFiscal()
        {
            decimal icmsretido = 0;

            // VERIFICAR REGRA
            //if (nota.Filial.TipoST != (int)TipoST.Substituído)
            //{
            icmsretido = nota.NotaICMSs.Sum(n => n.ValorSubst);
            //}
            decimal ipiretido = nota.NotaItems.Sum(ni => ni.VIPI_O14);
            AtualizarTotalProduto();
            nota.TotalNota = nota.TotalProduto + nota.TotalServico + nota.ValorFrete + icmsretido + ipiretido + nota.W11_vII + nota.ValorSeguro + nota.OutrasDespesas - nota.ValorDesconto - nota.BuscaValorICMSDesoneracao(_configuracao.NaoSomarIcmsDesonTotalNota);

            TotalizarVolumesNota();

            if (nota.NotaParcelas.Count > 0)
                AtualizarParcelas();
            else
                GerarParcelas();
        }

        public void SetData(DateTime data)
        {
            var dtAnt = nota.Dt;
            nota.Dt = data;
            if (dtAnt != data)
            {
                AtualizaImpostosServico();
                GerarParcelas();
            }
        }

        public void SetHora(DateTime horaEnvio)
        {
            var dtAnt = nota.Dt;

            if (nota.Dt != null)
            {
                nota.Dt = nota.Dt.AddMinutes(-nota.Dt.Minute + horaEnvio.Minute);
                nota.Dt = nota.Dt.AddHours(-nota.Dt.Hour + horaEnvio.Hour);
            }
        }

        public void SetCodigo(int codigo)
        {
            nota.Codigo = codigo;
        }

        public void SetSerie(string serie)
        {
            nota.Serie = serie;
        }

        public void SetNumero(int numero)
        {
            nota.Numero = numero;
        }

        public void SetDataEnvio(DateTime dataEnvio)
        {
            if (dataEnvio.Year > 1800)
                nota.DtEnvio = dataEnvio;
            else
                nota.DtEnvio = null;
        }

        public void SetHoraEnvio(DateTime horaEnvio)
        {
            if (nota.DtEnvio != null)
            {
                nota.DtEnvio = nota.DtEnvio.Value.AddMinutes(-nota.DtEnvio.Value.Minute + horaEnvio.Minute);
                nota.DtEnvio = nota.DtEnvio.Value.AddHours(-nota.DtEnvio.Value.Hour + horaEnvio.Hour);
            }
        }

        public void SetValorFrete(decimal valorFrete)
        {
            AtualizaSomatoriosTotais();
            nota.ValorFrete = valorFrete;
            RefazerRateiosEAtualizarImpostos();
            AtualizaTotalNotaFiscal();
            AtualizarParcelas();
        }

        public void SetValorSeguro(decimal valorSeguro)
        {
            AtualizaSomatoriosTotais();
            nota.ValorSeguro = valorSeguro;
            RefazerRateiosEAtualizarImpostos();
            AtualizaTotalNotaFiscal();
            AtualizarParcelas();
        }

        public void SetOutrasDespesas(decimal valorOutrasDespesas)
        {
            AtualizaSomatoriosTotais();
            nota.OutrasDespesas = valorOutrasDespesas;
            RefazerRateiosEAtualizarImpostos();
            AtualizaTotalNotaFiscal();
            AtualizarParcelas();
        }

        public LogicaTelaNotaItem GetLogicaTelaNotaItem()
        {
            if (PodeInserirItens())
            {
                NotaItem notaItem = new NotaItem();
                notaItem.Nota = nota;
                notaItem.Ent_Sai = (int)nota.Ent_Sai;
                notaItem.Dt = nota.Dt;
                notaItem.CalcCustoMedio = nota.TipoNota.BCalcCustoMedio;
                int sequencia;
                try
                {
                    sequencia = nota.NotaItems.Max(i => i.Sequencia) + 1;
                }
                catch
                {
                    sequencia = 1;
                }

                notaItem.Sequencia = sequencia;
                return new LogicaTelaNotaItem(notaItem);
            }
            return null;
        }

        private bool PodeInserirItens()
        {
            FormException exc = new FormException();
            if (nota.Condicao == null)
            {
                exc.CamposErrados.Add("Condicao");
            }
            if (nota.Pessoa == null)
            {
                exc.CamposErrados.Add("Pessoa");
            }
            if (nota.Filial == null)
            {
                exc.CamposErrados.Add("Filial");
            }
            if (nota.TabelaPreco == null)
            {
                exc.CamposErrados.Add("TabelaPreco");
            }
            if (nota.TipoNota == null)
                exc.CamposErrados.Add("TipoNota");

            if (exc.CamposErrados.Count == 0)
                return true;
            else
                throw exc;
        }

        public void AdicionarItemsDoOrcamento(IList<PedidoItem> itensAdicionar)
        {
            Faturamento.NotaBuilder notaBuilder = new Faturamento.NotaBuilder(nota);
            IList<IProdutoHolder> produtos = new List<IProdutoHolder>();
            foreach (PedidoItem pedidoItem in itensAdicionar)
            {
                produtos.Add(new PedidoHolder(pedidoItem));
            }
            notaBuilder.IncluirProdutosNota(produtos);
            RefazerRateiosEAtualizarImpostos();
            AtualizaTotalNotaFiscal();

        }

        public void AtualizarTotalProduto()
        {
            nota.TotalProduto = nota.NotaItems.Sum(x => x.SubTotal); // leandro
        }

        public void AtualizarDadosTributacaoProdutos()
        {
            var builder = new NotaBuilder(nota);
            builder.AtualizarProdutosNota();
        }

        public void InserirNotaItem(NotaItem notaItem)
        {
            //ALTERADO NOTAITEMSELECIONADO
            nota.NotaItems.Add(notaItem);
            nItem = notaItem;
            AtualizarTotalProduto();
            if (notaItem.Ent_Sai == 2 && !notaItem.CFOP.BDevolucao)
            {
                RefazerRateiosEAtualizarImpostos();
            }
            else
            {
                NotaICMSController.Instancia.AtualizarICMS(nota);
            }
            AtualizaTotalNotaFiscal();
        }

        /// <summary>
        /// Método que refaz os calculos para todos os notaItems
        /// </summary>
        public void RefazerRateiosEAtualizarImpostos()
        {
            var config = ConfiguracaoController.Instancia.GetConfiguracao();

            if (nota.OutrasDespesas >= 0 || nota.ValorFrete >= 0 || nota.ValorSeguro >= 0 || nota.ValorAcrescimo >= 0 || nota.ValorDesconto >= 0)
            {
                nota.RecalcularRateios();

                if (nota.TipoNota != null)
                {
                    if (nota.TipoNota.Ent_Sai != 1)
                    {
                        if (config.NaoSomarIpiTotalNota != 1 || nItem == null)
                        {
                            foreach (var item in nota.NotaItems)
                            {
                                var tributavel = NotaBuilder.IniciaTributacao(item);
                            }
                        }
                        else
                        {
                            var tributavel = NotaBuilder.IniciaTributacao(nItem);
                        }
                    }
                }
            }
            if (nota.NotaICMSs.Count == 0)
            {
                nota.TotalNota = 0;
            }
            if (nota.TipoNota != null)
            {
                if (nota.TipoNota.Ent_Sai != 1)
                {
                    NotaICMSController.Instancia.AtualizarICMS(nota);
                }
            }
            if (nota.NotaICMSs.Count == 0)
            {
                nota.TotalNota = 0;
                GerarParcelas();
            }

        }

        public void SetTransportadora(Pessoa transportadora)
        {
            nota.PessoaTransportadora = transportadora;
            nota.TransCNPJCPF = transportadora.CNPJ_CPF;
            nota.TransCidade = transportadora.Cidade;
            nota.TransEndereco = transportadora.EnderecoPrincipal().Endereco;
            nota.TransNome = transportadora.Nome;
            nota.TransPlaca = transportadora.Placa;
            nota.TransPlacaUF = transportadora.PlacaUF;
            nota.TransUF = transportadora.Estado;
        }

        public LogicaTelaNotaItem GetLogicaTelaNotaItem(NotaItem notaItem)
        {
            return new LogicaTelaNotaItem(notaItem);
        }

        public void AtualizaSomatoriosTotais()
        {
            nota.W11_vII = nota.NotaItems.Sum(i => i.P04_vII);
            nota.TotalServico = nota.NotaItemsServicos.Sum(i => i.SubTotal);

            if (nota.TipoNota != null && nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos)
                AtualizaImpostosServico();
            AtualizaTotalNotaFiscal();
        }

        private void TotalizarVolumesNota()
        {
            if (nota.TipoNota != null && nota.TipoNota.TotalizarVolumes)
            {
                nota.VolumePesoBruto = nota.NotaItems.Sum(n => n.PesoBruto);
                nota.VolumePesoLiquido = nota.NotaItems.Sum(n => n.PesoLiquido);
                nota.VolumeQuant = nota.NotaItems.Sum(n => n.Quantidade);
            }
        }

        public void SetDescontoValor(decimal valor)
        {
            AtualizaSomatoriosTotais();
            decimal valorItens = nota.TotalProduto + nota.TotalServico;

            if (valorItens > 0 && valorItens > valor)
            {
                nota.ValorDesconto = valor;
                if (valor > 0)
                    nota.PercDesconto = Math.Round(valor / valorItens * 100, 6);
                else
                    nota.PercDesconto = 0;
                RefazerRateiosEAtualizarImpostos();
                AtualizaTotalNotaFiscal();
                AtualizarParcelas();
            }
            else
            {
                nota.ValorDesconto = 0;
                nota.PercDesconto = 0;
            }
        }

        public void SetDescontoPerc(decimal percentualDesconto)
        {
            AtualizaSomatoriosTotais();
            decimal valorItens = nota.TotalProduto + nota.TotalServico;

            if (valorItens > 0 && percentualDesconto <= 100)
            {
                nota.PercDesconto = percentualDesconto;
                nota.ValorDesconto = Math.Round(valorItens * percentualDesconto / 100, 2);

                AtualizaTotalNotaFiscal();
                AtualizarParcelas();
            }
            else
            {
                nota.ValorDesconto = 0;
                nota.PercDesconto = 0;
            }
        }

        public void SetTipoFrete(byte tipoFrete)
        {
            nota.TipoFrete = tipoFrete;
        }

        public void AtualizarObservacaoSistema()
        {
            if (nota.TipoNota != null && nota.Filial != null)
            {
                string observacao;
                CaracteresRestantesObservacao = notaController.MontarObservacao(nota, out observacao);
                nota.ObservacaoSistema = observacao;
            }
        }

        public void SetObservacaoUsuario(string observacao)
        {
            nota.ObservacaoUsuario = observacao;
        }

        public void Salvar(Modelo.Acao acao, bool importacao)
        {
            try
            {
                int idOSOrdemServico = -1;

                AtualizarObservacaoSistema();

                if (acao == Acao.Incluir && nota.ModeloDocto == 55)
                    nota.Status = "-1";

                if (acao == Acao.Excluir)
                {
                    idOSOrdemServico = PegaIDOrdemServico();

                    if (nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos)
                    {
                        AtendimentoController.Instancia.ExcluiNotaOSsFaturamentoAtendimentos(nota.ID);
                    }

                    notaController.RegisterDeleteIntoTransaction(nota);
                    if (acao == Acao.Excluir)
                    {
                        if (Identificador != null)
                        {
                            IdentificadorController.Instancia.RegisterDeleteIntoTransaction(Identificador);
                        }
                    }
                }
                else
                {
                    if (_configuracao.GerarPedidoCompra && nota.Ent_Sai == InOutType.Saída)
                    {
                        IList<Pedido> pedidosCompra = EstoqueController.Instancia.VerificarEstoqueNegativo(nota);

                        int maxCodigo = PedidoController.Instancia.MaxCodigo() + 1;
                        foreach (Pedido pedido in pedidosCompra)
                        {
                            pedido.Codigo = maxCodigo;

                            PedidoController.Instancia.Salvar(pedido, Acao.Incluir);
                            maxCodigo = maxCodigo + 1;
                        }
                    }

                    #region Validações NFE tecnospeed versão 4.5.38...

                    foreach (var email in nota.Pessoa.PessoaEmails)
                        email.Email.TrimEnd();

                    if (nota.VolumeQuant > 5000)
                        throw new Exception("Quantidade de volumes ultrapassam os 5000 (valor máximo de NFE)");
                    if (nota.VolumeEspecie != "" && nota.VolumeQuant == 0)
                        throw new Exception("Obrigatório informar a quantidade de volumes");

                    int cont = 0;
                    foreach (var parcela in nota.NotaParcelas)
                    {
                        cont++;
                        if (parcela.Valor == 0 && parcela.FormaPagamento != ParcelaBase.EnumFormaPagamento.SemPagamento)
                            throw new Exception("Insira valor na " + cont + "º parcela.");
                    }

                    #endregion

                    foreach (var item in nota.NotaItems)
                    {
                        item.BAtualizaEstoque = nota.TipoNota.BAtualizaEstoque;
                        item.CalcCustoMedio = nota.TipoNota.BCalcCustoMedio;
                        item.Ent_Sai = (int)nota.Ent_Sai;
                        item.Dt = nota.Dt;

                        //Atualiza os valores atuais do Tipo de Movimentação
                        item.PIS_CREDEB = nota.TipoNota.PIS_CREDEB;
                        item.COFINS_CREDEB = nota.TipoNota.COFINS_CREDEB;
                        item.ICMS_CREDEB = nota.TipoNota.ICMS_CREDEB;
                        item.IPI_CREDEB = nota.TipoNota.IPI_CREDEB;
                        item.CalcCustoMedio = nota.TipoNota.BCalcCustoMedio;

                        if (nota.Ent_Sai == InOutType.Entrada && _configuracao.UtilizaCustoNota == false)
                        {
                            Produto prod = ProdutoController.Instancia.LoadObjectById(item.Produto.ID);
                            item.CustoUnit = prod.PrecoFornecedor;
                        }
                    }
                    nota.CalcularUltimoCusto();
                    nota.CalcularCustoUnit(_configuracao);
                    notaController.RegisterNewIntoTransaction(nota);
                }

                NotaImpostoServicoController.Instancia.RegisterNewIntoTransaction(tributacaoServico.ImpostosQuePrecisamSerAtualizados);

                CalcularCMVUnit(acao);
                AtualizarEstoque(acao);
                AtualizarPedido(acao);

                AtualizarFinanceiro(acao);

                AtualizarCwkPedido(acao);

                if (acao == Acao.Incluir)
                    nota.Codigo = notaController.MaxCodigo();

                if (acao == Acao.Excluir)
                {
                    foreach (NotaItem item in nota.NotaItems)
                        item.PedidoItens = null;
                }

                notaController.CommitUnitOfWorkTransaction();
                if (nota.Ent_Sai == InOutType.Entrada)
                    foreach (var item in nota.NotaItems)
                    {
                        Produto prod = ProdutoController.Instancia.LoadObjectById(item.Produto.ID);
                        prod.UltimoCusto = item.UltimoCusto;
                        prod.DtUltimoCusto = item.Dt;
                        prod.PrecoFornecedor = item.CustoUnit;
                        prod.PrecoBase = ProdutoController.Instancia.getPrecoBase(prod);
                        ProdutoController.Instancia.Salvar(prod, Acao.Alterar);
                    }

                if (idOSOrdemServico != 0 && idOSOrdemServico != -1)
                {
                    AlteraStatusOSOrdemServico(idOSOrdemServico);
                }

                IList<Pedido> listaPedidos = new List<Pedido>();
                if (nota.Ent_Sai == InOutType.Entrada)
                {
                    foreach (NotaItem item in nota.NotaItems)
                    {
                        item.PedidoItens = item.PedidoItens == null ? new List<NotaItemPedidoItem>() : item.PedidoItens;
                        foreach (NotaItemPedidoItem nipi in item.PedidoItens.Where(w => w.PedidoItem != null).ToList<NotaItemPedidoItem>())
                        {
                            listaPedidos.Add(nipi.PedidoItem.Pedido);
                        }
                    }
                    Orcamento.AtualizaStatusOrcamento(listaPedidos);
                }

                if (importacao)
                {
                    nota.StatusNota = Nota.StatusTipo.Importada;
                    nota.Status = "11";
                    notaController.Salvar(nota, Acao.Alterar);
                }
                else if (nota.TipoNota.BImportacaoXmlManual)
                {
                    nota.StatusNota = Nota.StatusTipo.ImportacaoManual;
                    nota.Status = "12";
                    notaController.Salvar(nota, Acao.Alterar);
                }
            }
            catch (Exception ex)
            {
                notaController.RollBackUnitOfWorkTransaction();
                throw (ex);
            }
        }

        public int PegaIDOrdemServico()
        {
            if (nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos)
            {
                return notaController.GetByIdOSOrdemServico(nota.ID);
            }
            else if (nota.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Produtos)
            {
                return notaController.GetByIdOSOrdemServicoProduto(nota.ID);
            }

            return -1;
        }

        public void AlteraStatusOSOrdemServico(int idOSOrdemServico)
        {
            string statusNota = String.Empty;

            if (idOSOrdemServico != 0 && idOSOrdemServico != -1)
            {
                int idNota = -1;
                TipoNota.CategoriaMovimentacao categoria = nota.TipoNota.Categoria;

                switch (categoria)
                {
                    case TipoNota.CategoriaMovimentacao.Produtos:
                        idNota = notaController.GetByIdNotaPorIdOSServico(idOSOrdemServico);
                        break;
                    case TipoNota.CategoriaMovimentacao.Servicos:
                        idNota = notaController.GetByIdNotaPorIdOSProduto(idOSOrdemServico);
                        break;
                    default:
                        break;
                }

                if (idNota != 0)
                {
                    Nota objNota;
                    objNota = notaController.LoadObjectById(idNota);
                    statusNota = objNota.StatusLegivel;
                }

                if (idNota == 0 || statusNota.Contains("Cancelada"))
                {
                    if ((MessageBox.Show("Deseja alterar o Status do Orçamento de Serviço relacionado a está Nota para Aberto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                    {
                        Acao acao = Acao.Alterar;
                        OSOrdemServico osOrdemServico = OSOrdemServicoController.Instancia.LoadObjectById(idOSOrdemServico);
                        osOrdemServico.Status = StatusOSController.Instancia.GetStatusTipoFaturamento();

                        OSOrdemServicoController.Instancia.Salvar(osOrdemServico, acao);
                    }
                }
            }
        }

        private void CalcularCMVUnit(Acao acao)
        {
            if (acao == Acao.Excluir) return;

            if (nota.Ent_Sai == InOutType.Saída)
                foreach (var item in nota.NotaItems)
                    item.CMVUnit = ProdutoController.Instancia.BuscaCustoMedio(nota.Filial.ID, item.Produto.ID, nota.Dt);
            else if (nota.Ent_Sai == InOutType.Entrada)
                foreach (var item in nota.NotaItems)
                {
                    if (item.CalcCustoMedio)
                        item.ValorCustoMedio = ProdutoController.Instancia.CalculaCustoMedio(nota.Filial.ID, item.Produto.ID, nota.Dt, item.Quantidade, item.CustoUnit, nota.ID);
                    else
                        item.ValorCustoMedio = 0;
                }
        }

        private void AtualizarCwkPedido(Acao acao)
        {
            if (acao == Acao.Excluir && !String.IsNullOrEmpty(nota.ObservacaoUsuario) && nota.ObservacaoUsuario.Contains("Números de série desta nota :")) // nota gerada por cwkpedidos faturados em lote
            {
                String[] numerosSerie = nota.ObservacaoUsuario.Split(new string[] { "Números de série desta nota :" }, StringSplitOptions.None)[1].Split('.')[0].Split(',');
                foreach (var numSerie in numerosSerie.Select(n => Convert.ToInt32(n)).Distinct())
                {
                    cwkGestao.Modelo.CwkPedido modelo = new cwkGestao.Modelo.CwkPedido();
                    modelo.NumeroSerie = numSerie;
                    modelo.Status = cwkGestao.Modelo.OrcamentoStatus.Faturado;
                    IList<cwkGestao.Modelo.CwkPedido> peds = cwkGestao.Negocio.CwkPedidoController.Instancia.GetPedidos(modelo);

                    foreach (cwkGestao.Modelo.CwkPedido pedido in peds)
                    {
                        pedido.Status = cwkGestao.Modelo.OrcamentoStatus.AguardandoFaturamento;
                        cwkGestao.Negocio.CwkPedidoController.Instancia.RegisterNewIntoTransaction(pedido);
                    }
                }
            }
        }

        private void AtualizarPedido(Acao acao)
        {
            if (acao == Acao.Excluir)
            {
                if (nota.TipoNota.BAtualizaPedido)
                {
                    IList<Pedido> pedidos = new List<Pedido>();
                    IList<NotaItemPedidoItem> pedidoItens = NotaItemPedidoItemController.Instancia.GetAllNiPiPorIDNota(nota.ID);
                    int qtdeItensPedido = 0;
                    foreach (var item in pedidoItens)
                    {
                        item.PedidoItem.QtdRetirada -= nota.NotaItems.Where(ni => ni.Produto.ID == item.PedidoItem.Produto.ID).FirstOrDefault().Quantidade;
                        decimal qtdItemPedido = nota.NotaItems.Where(ni => ni.Produto.ID == item.PedidoItem.Produto.ID).FirstOrDefault().Quantidade;
                        item.PedidoItem.BFaturado = false;

                        Pedido pedido = item.PedidoItem.Pedido;
                        qtdeItensPedido = pedido.Items.Count;
                        pedidos.Add(pedido);
                    }

                    pedidos = pedidos.GroupBy(p => p.ID).Select(p => p.First()).ToList();

                    foreach (var pedido in pedidos)
                    {
                        int teste = pedido.Items.Where(pi => pi.BFaturado == false).Count();
                        qtdeItensPedido = pedido.Items.Count;

                        if (pedido.Items.Where(pi => pi.BFaturado == false).Count() == qtdeItensPedido)
                            pedido.Status = StatusOrcamento.Aberto;
                        else
                            pedido.Status = StatusOrcamento.FaturadoParcial;

                        PedidoController.Instancia.RegisterNewIntoTransaction(pedido);
                    }
                }
            }
            else
            {
                foreach (NotaItem notaItem in nota.NotaItems)
                {
                    if (notaItem.PedidoItens != null)
                    {
                        IList<NotaItemPedidoItem> temp = notaItem.PedidoItens.OrderBy(o => o.PedidoItem.Pedido.Dt).ToList();
                        IList<Pedido> pedidos = new List<Pedido>();
                        decimal qtdItemNota = notaItem.Quantidade;
                        foreach (var item in temp)
                        {
                            decimal qtdRetiradaAnteriormente = NotaItemPedidoItemController.Instancia.GetQtdRetirada(item.PedidoItem.ID, item.NotaItem.ID);
                            decimal qtdItemPedido = item.PedidoItem.Pedido.Items.Where(w => w.ID == item.PedidoItem.ID).FirstOrDefault().Quantidade;
                            decimal qtdDisponivel = qtdItemPedido - qtdRetiradaAnteriormente;
                            decimal resultado = qtdItemNota - qtdRetiradaAnteriormente - qtdItemPedido;

                            if (qtdDisponivel < qtdItemNota)
                                item.QtdRetiradaPedido = qtdDisponivel;
                            else
                                item.QtdRetiradaPedido = qtdItemNota;

                            qtdItemNota = qtdItemNota - item.QtdRetiradaPedido;
                            item.PedidoItem.QtdRetirada = qtdRetiradaAnteriormente + item.QtdRetiradaPedido;

                            pedidos.Add(item.PedidoItem.Pedido);
                        }

                        pedidos = pedidos.GroupBy(p => p.ID).Select(p => p.First()).ToList();

                        foreach (var pedido in pedidos)
                        {
                            if (pedido.Items.Where(pi => pi.BFaturado == false).Count() == 0)
                                pedido.Status = StatusOrcamento.Faturado;
                            else
                                pedido.Status = StatusOrcamento.FaturadoParcial;

                            PedidoController.Instancia.RegisterNewIntoTransaction(pedido);
                        }
                    }
                }
            }
        }

        private void AtualizarEstoque(Modelo.Acao acao)
        {
            if (EhNotaComplementar) return;

            if (acao == Acao.Alterar)
            {
                foreach (var item in nota.NotaItems)
                {
                    item.Estoques.Clear();
                }
            }

            AtualizarEstoqueComIdentificadores();
        }

        private void AtualizarEstoqueComIdentificadores()
        {
            if (!_configuracao.UtilizaIdentificadorEstoque) return;

            if (nota.Ent_Sai == InOutType.Entrada)
                IncluirIdentificadoresNotaEntrada();
            else
                IncluirIdentificadoresNotaSaida(_configuracao);
        }

        private void IncluirIdentificadoresNotaSaida(Configuracao config)
        {
            foreach (var item in from n in nota.NotaItems
                                 where n.BAtualizaEstoque && n.Produto.UtilizaIdentificadorEstoque
                                 group n by n.Produto into g
                                 select new
                                 {
                                     Produto = g.Key,
                                     Items = g.ToList()
                                 })
            {
                var identificadoresEstoque = NotaItemController.Instancia.SaldoEstoqueLocalEstoqueListaIdentificador(nota.TipoNota.LocalEstoqueTipoNota.ID, item.Produto.ID, nota.ID, false);

                if (item.Items.Sum(s => s.Quantidade) > identificadoresEstoque.Sum(s => s.Saldo))
                {
                    var mensagem = "Quantidade de estoque disponível é menor que a quantidade vendida para o produto\n " + item.Produto.Nome;
                    throw new Exception(mensagem);
                }

                foreach (var notaItem in item.Items)
                {
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

        private void IncluirIdentificadoresNotaEntrada()
        {
            foreach (var item in nota.NotaItems.Where(n => n.Produto.UtilizaIdentificadorEstoque))
            {
                var notaItemEstoque = item.Estoques.FirstOrDefault();
                if (notaItemEstoque == null)
                    notaItemEstoque = new NotaItemEstoque();
                notaItemEstoque.Identificador = Identificador;
                notaItemEstoque.NotaItem = item;
                notaItemEstoque.Quantidade = item.Quantidade;
                notaItemEstoque.CMVUnit = 0;
                if (!item.Estoques.Contains(notaItemEstoque))
                    item.Estoques.Add(notaItemEstoque);
            }
        }

        private void AtualizarFinanceiro(Modelo.Acao acao)
        {
            if (EhNotaComplementar) return;

            if (acao == Acao.Alterar || acao == Acao.Excluir)
            {
                var documentosExistentes = documentoController.GetDocumentosPorNota(nota.ID);
                foreach (var doc in documentosExistentes)
                {
                    decimal valorimpostos = doc.Movimentos.Where(mov => mov.Tipo == TipoMovimentoType.Imposto).Sum(mov => mov.Valor);
                    if (doc.Saldo + valorimpostos != doc.Valor)
                        throw new Exception("A nota não pode ser alterada\\excluída porque possui\ndocumento(s) no financeiro com alguma movimentação de baixa.");

                    if (acao == Acao.Alterar || acao == Acao.Excluir)
                    {
                        var docNota = nota.Documentos.Where(d => d.ID == doc.ID).FirstOrDefault();
                        documentoController.RegisterDeleteIntoTransaction(docNota);
                    }
                }
            }

            if (acao != Acao.Excluir && nota.TipoNota.BGeraFinanceiro)
                notaController.GeraFinanceiro(nota, null);
        }

        public bool ValidarDadosFaturamento(out string mensagem, out bool possuiAviso)
        {
            possuiAviso = false;
            mensagem = String.Empty;
            if (!NotaController.Instancia.DadosPedidoFaturarValidos(nota.Pessoa, out mensagem))
            {
                mensagem = "Antes de efetuar a fatura é necessário corrigir o cadastro do cliente:\n" + mensagem;
                return false;
            }
            else if (!EhNotaDeEntradaDeTerceiros())
            {
                if (!NotaController.Instancia.DadosNfeValidos(nota.Pessoa, nota.Filial.Cidade.UF.ID, (int)nota.TipoNota.ModeloDocto, out mensagem, out possuiAviso))
                {
                    mensagem = "Verifique anomalias:\n" + mensagem;
                    return false;
                }
            }
            return true;
        }

        private bool EhNotaDeEntradaDeTerceiros()
        {
            return (nota.TipoNota.Ent_Sai == (int)InOutType.Entrada && nota.TipoNota.NFOrigem == 0);
        }

        public void SetServicoRealizadoCidadeCliente(bool valor)
        {
            bool anterior = nota.ServicoRealizadoCidadeCliente;
            nota.ServicoRealizadoCidadeCliente = valor;
            if (anterior != valor)
                AtualizaImpostosServico();
        }

        public void GerarIdentificador(bool EhPraSalvar)
        {
            bool _EhPraSalvar = EhPraSalvar;

            Identificador = new Identificador();
            Identificador.Ano = DateTime.Now.Year;
            Identificador.Sequencia = IdentificadorController.Instancia.GetProximaSequenciaAno(Identificador.Ano);

            if (_EhPraSalvar)
            {
                IdentificadorController.Instancia.Salvar(Identificador, Acao.Incluir);
            }
        }

        public void CarregarIdentificador()
        {
            var _Nota = NotaController.Instancia.LoadObjectById(nota.ID);
            if (_Nota.identificador != null)
            {
                var _Identificador = IdentificadorController.Instancia.LoadObjectById(_Nota.identificador.ID);
                Identificador = _Identificador;
            }
        }
    }
}
