using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Text.RegularExpressions;

namespace cwkGestao.Negocio
{
    public class CotacaoController : BaseController<Cotacao>
    {
        #region Singleton
        private Configuracao Conf = ConfiguracaoController.Instancia.GetConfiguracao();
        private static CotacaoController instance;
        private static IRepositorioCotacao repositorioCotacao;

        private CotacaoController()
        { }

        static CotacaoController()
        {
            instance = new CotacaoController();
            repositorioT = RepositorioFactory<Cotacao>.GetRepositorio();
            repositorioCotacao = (IRepositorioCotacao)repositorioT;
        }

        public static CotacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Cotacao objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(Cotacao objeto, Acao acao)
        {
            if (objeto.TipoCotacao == TipoCotacao.Lista)
            {
                var cotacaoListaExistente = GetCotacaoLista().Where(cot => cot.Filial.ID == objeto.Filial.ID).FirstOrDefault();
                if (cotacaoListaExistente != null  && cotacaoListaExistente.ID != objeto.ID)
                {
                    AlterarCotacaoListaExistente(objeto, cotacaoListaExistente);
                    return base.Salvar(cotacaoListaExistente, Acao.Alterar);
                }
            }

            return base.Salvar(objeto, acao);
        }

        private void AlterarCotacaoListaExistente(Cotacao objeto, Cotacao cotacaoListaExistente)
        {
            foreach (var item in objeto.CotacaoProdutos)
            {
                var t = cotacaoListaExistente.CotacaoProdutos.Where(c => c.Produto.ID == item.Produto.ID);
                if (t.Count() > 0)
                {
                    var asd = t.First();
                    asd.Quantidade += item.Quantidade;
                }
                else
                {
                    item.Cotacao = cotacaoListaExistente;
                    try
                    {
                        item.Codigo = cotacaoListaExistente.CotacaoProdutos.Max(c => c.Codigo) + 1;
                    }
                    catch { item.Codigo = 1; }
                    cotacaoListaExistente.CotacaoProdutos.Add(item);
                }
            }
        }

        public IList<Cotacao> GetCotacaoLista()
        {
            return repositorioCotacao.GetCotacaoLista();
        }

        public Pedido GerarPedidoDeFornecedor(Cotacao cotacao, Pessoa fornecedor, DateTime dataBase, DateTime dataVencimento)
        {
            Pedido pedido = IniciaNovoPedido(dataBase, dataVencimento);

            string indiceRelativoFornecedor = GetIndiceFornecedor(cotacao, fornecedor);
            SetaCamposFilial(pedido, cotacao.Filial);
            SetaCamposPessoa(pedido, fornecedor);
            SetaCamposTipoPedido(pedido);
            SetaCamposCondicao(pedido, cotacao, indiceRelativoFornecedor);
            SetaCamposTabelaPreco(pedido);

            SetaItems(pedido, cotacao.CotacaoProdutos, indiceRelativoFornecedor);

            pedido.CalculaTotalProdutos();
            
            pedido.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);

            pedido.Parcelas = CondicaoController.Instancia.GerarParcelas(pedido);
            return pedido;

        }

        private Pedido IniciaNovoPedido(DateTime dataBase, DateTime dataVencimento)
        {
            Pedido pedido = new Pedido();
            pedido.Dt = dataBase;
            pedido.DtValidade = dataVencimento;
            pedido.DtPrazoEntrega = dataVencimento;
            pedido.Codigo = PedidoController.Instancia.CodigoDisponivelPedido();
            pedido.Status = StatusOrcamento.Aberto;
            return pedido;
        }

        private void SetaItems(Pedido pedido, IList<CotacaoProduto> cotacao, string indiceRelativoFornecedor)
        {
            PropertyInfo preco = typeof(CotacaoProduto).GetProperty("Preco_" + indiceRelativoFornecedor);

            int sequencia = 0;
            foreach (CotacaoProduto cotacaoProduto in cotacao)
            {
                sequencia++;
                PedidoItem pedidoItem = new PedidoItem();
                pedidoItem.Sequencia = sequencia;
                pedidoItem.bReservaEstoque = pedido.TipoPedido.BReservaEstoque;
                pedidoItem.Dt = pedido.Dt;
                pedidoItem.Ent_Sai = pedido.Ent_Sai;
                pedidoItem.Pedido = pedido;
                pedidoItem.Produto = cotacaoProduto.Produto;
                pedidoItem.PrazoEntrega = new DateTime(2000, 1, 1);
                pedidoItem.ProdutoDescReduzida = cotacaoProduto.DescricaoProduto;
                pedidoItem.ProdutoNome = cotacaoProduto.DescricaoProduto;
                pedidoItem.Quantidade = cotacaoProduto.Quantidade;
                pedidoItem.ValorCalculado = (decimal)preco.GetValue(cotacaoProduto, null);
                pedidoItem.Valor = pedidoItem.ValorCalculado;
                pedidoItem.SubTotal = Math.Round(pedidoItem.Quantidade * pedidoItem.ValorCalculado, 2);
                pedidoItem.Total = pedidoItem.SubTotal;
                pedidoItem.Unidade = pedidoItem.Produto.Unidade.Sigla;

                pedido.Items.Add(pedidoItem);

                CotacaoProdutoPedidoItemController.Instancia.RegisterNewIntoTransaction(new CotacaoProdutoPedidoItem{PedidoItem = pedidoItem, Quantidade = pedidoItem.Quantidade, CotacaoProduto = cotacaoProduto, Codigo = sequencia} );
            }
            if(pedido.Items.Sum(it => it.Valor) == 0) throw new InvalidOperationException("Não foi possível gerar algum dos pedidos porque o valor do pedido seria 0(zero)");

        }

        private string GetIndiceFornecedor(Cotacao cotacao, Pessoa fornecedor)
        {
            foreach (PropertyInfo idFornecedorPropriedade in cotacao.GetType().GetProperties().Where(i => i.Name.StartsWith("IDFornecedor")))
            {
                int idFornecedor = Convert.ToInt32(idFornecedorPropriedade.GetValue(cotacao, null));
                if (idFornecedor != 0 && idFornecedor == fornecedor.ID)
                    return new Regex("[0-9]+").Match(idFornecedorPropriedade.Name).Value;
            }
            throw new Exception("O fornecedor escolhido não se encontra na cotação.");
        }

        private void SetaCamposTabelaPreco(Pedido pedido)
        {
            pedido.TabelaPreco = TabelaPrecoController.Instancia.GetAll().First();
        }

        private void SetaCamposCondicao(Pedido pedido, Cotacao cotacao, string indiceRelativoFornecedor)
        {
            int idCondicao = (int)cotacao.GetType().GetProperty("IDCondicao_" + indiceRelativoFornecedor).GetValue(cotacao, null);
            if (idCondicao == 0)
            {
                pedido.Condicao = CondicaoController.Instancia.LoadObjectById(CondicaoController.Instancia.GetAll().First().ID);
            }
            else
            {
                pedido.Condicao = CondicaoController.Instancia.LoadObjectById(idCondicao);

            }

        }

        private void SetaCamposFilial(Pedido pedido, Filial filial)
        {
            pedido.Filial = filial;
        }

        private void SetaCamposTipoPedido(Pedido pedido)
        {
            try
            {
                TipoPedido tipoPedido = TipoPedidoController.Instancia.GetAll().Where(i => i.Ent_Sai == 1 && i.Tipo == TipoPedidoType.Orçamento).First();
                pedido.Ent_Sai = tipoPedido.Ent_Sai;
                pedido.TipoPedido = tipoPedido;
                pedido.Numero = PedidoController.Instancia.MaxNumero(pedido.Filial, pedido.TipoPedido);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Não foi encontrada nenhum tipo de pedido com tipo de entrada. Favor verificar este cadastro.", e);
            }

        }

        private void SetaCamposPessoa(Pedido pedido, Pessoa fornecedor)
        {
            pedido.Pessoa = fornecedor;
            pedido.PessoaBairro = fornecedor.EnderecoPrincipal().Bairro;
            pedido.PessoaCEP = fornecedor.EnderecoPrincipal().CEP;
            pedido.PessoaCidade = fornecedor.EnderecoPrincipal().Cidade.Nome;
            pedido.PessoaCNPJCPF = fornecedor.CNPJ_CPF;
            pedido.PessoaEndereco = fornecedor.EnderecoPrincipal().Endereco;
            pedido.PessoaInscRG = fornecedor.TipoPessoa == "Física" ? fornecedor.RG : fornecedor.Inscricao;
            pedido.PessoaNome = fornecedor.Nome;
            pedido.PessoaTelefone = fornecedor.TelefonePrincipal;
            pedido.PessoaUF = fornecedor.EnderecoPrincipal().Cidade.UF.Sigla;
        }

        public Pedido GerarPedidoMenorPrecoTotal(Cotacao cotacao, DateTime dataBase, DateTime dataVencimento)
        {
            Pedido pedido = IniciaNovoPedido(dataBase, dataVencimento);

            string indiceRelativoFornecedorDoMenorOrcamento = DeterminaFornecedorDoMenorOrcamento(cotacao);
            SetaCamposFilial(pedido, cotacao.Filial);
            SetaCamposPessoa(pedido, PessoaController.Instancia.LoadObjectById((int)cotacao.GetType().GetProperty("IDFornecedor_" + indiceRelativoFornecedorDoMenorOrcamento).GetValue(cotacao, null)));
            SetaCamposTipoPedido(pedido);
            SetaCamposCondicao(pedido, cotacao, indiceRelativoFornecedorDoMenorOrcamento);
            SetaCamposTabelaPreco(pedido);

            SetaItems(pedido, cotacao.CotacaoProdutos, indiceRelativoFornecedorDoMenorOrcamento);

            pedido.CalculaTotalProdutos();
            pedido.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);

            pedido.Parcelas = CondicaoController.Instancia.GerarParcelas(pedido);
            return pedido;
        }

        private string DeterminaFornecedorDoMenorOrcamento(Cotacao cotacao)
        {
            Dictionary<string, decimal> valorTotaisOrcamentos = new Dictionary<string, decimal>();
            if (cotacao.IDFornecedor_01 != 0) valorTotaisOrcamentos.Add("01", cotacao.CotacaoProdutos.Sum(i => i.Quantidade * i.Preco_01));
            if (cotacao.IDFornecedor_02 != 0) valorTotaisOrcamentos.Add("02", cotacao.CotacaoProdutos.Sum(i => i.Quantidade * i.Preco_02));
            if (cotacao.IDFornecedor_03 != 0) valorTotaisOrcamentos.Add("03", cotacao.CotacaoProdutos.Sum(i => i.Quantidade * i.Preco_03));
            if (cotacao.IDFornecedor_04 != 0) valorTotaisOrcamentos.Add("04", cotacao.CotacaoProdutos.Sum(i => i.Quantidade * i.Preco_04));
            if (cotacao.IDFornecedor_05 != 0) valorTotaisOrcamentos.Add("05", cotacao.CotacaoProdutos.Sum(i => i.Quantidade * i.Preco_05));
            if (cotacao.IDFornecedor_06 != 0) valorTotaisOrcamentos.Add("06", cotacao.CotacaoProdutos.Sum(i => i.Quantidade * i.Preco_06));

            return valorTotaisOrcamentos.Where(i => i.Value == valorTotaisOrcamentos.Min(k => k.Value)).First().Key;
        }

        private Dictionary<string, IList<CotacaoProduto>> DeterminaItensPorOrcamento(Cotacao cotacao)
        {
            Dictionary<string, IList<CotacaoProduto>> menoresPrecosPorFornecedor = new Dictionary<string, IList<CotacaoProduto>>();

            foreach (CotacaoProduto cotacaoProduto in cotacao.CotacaoProdutos)
            {
                string indiceFornecedorMenorPreco = DeterminaFornecedorComMenorPreco(cotacaoProduto);

                try
                {
                    menoresPrecosPorFornecedor[indiceFornecedorMenorPreco].Add(cotacaoProduto);
                }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
                catch (KeyNotFoundException e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
                {
                    menoresPrecosPorFornecedor.Add(indiceFornecedorMenorPreco, new List<CotacaoProduto> { cotacaoProduto });
                }
            }

            return menoresPrecosPorFornecedor;
        }

        public IList<Pedido> GerarPedidoMenorPrecoProduto(Cotacao cotacao, DateTime dataBase, DateTime dataVencimento)
        {
            Dictionary<string, IList<CotacaoProduto>> menoresPrecosPorFornecedor = DeterminaItensPorOrcamento(cotacao);

            IList<Pedido> retorno = new List<Pedido>();
            int somarNoCodigo = 0;
            foreach (KeyValuePair<string, IList<CotacaoProduto>> par in menoresPrecosPorFornecedor)
            {

                Pedido pedido = IniciaNovoPedido(dataBase, dataVencimento);
                pedido.Codigo += somarNoCodigo;
                somarNoCodigo++;

                SetaCamposFilial(pedido, cotacao.Filial);
                SetaCamposPessoa(pedido, PessoaController.Instancia.LoadObjectById((int)cotacao.GetType().GetProperty("IDFornecedor_" + par.Key).GetValue(cotacao, null)));
                SetaCamposTipoPedido(pedido);
                SetaCamposCondicao(pedido, cotacao, par.Key);
                SetaCamposTabelaPreco(pedido);

                SetaItems(pedido, par.Value, par.Key);

                pedido.CalculaTotalProdutos();
                pedido.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);

                pedido.Parcelas = CondicaoController.Instancia.GerarParcelas(pedido);

                retorno.Add(pedido);
            }
            return retorno;
        }

        private string DeterminaFornecedorComMenorPreco(CotacaoProduto cotacaoProduto)
        {
            decimal menorPreco = cotacaoProduto.Preco_01;
            string indiceFornecedor = "01";
            if (cotacaoProduto.Preco_02 > 0 && menorPreco > cotacaoProduto.Preco_02)
            {
                menorPreco = cotacaoProduto.Preco_02;
                indiceFornecedor = "02";
            }
            if (cotacaoProduto.Preco_03 > 0 && menorPreco > cotacaoProduto.Preco_03)
            {
                menorPreco = cotacaoProduto.Preco_03;
                indiceFornecedor = "03";
            }
            if (cotacaoProduto.Preco_04 > 0 && menorPreco > cotacaoProduto.Preco_04)
            {
                menorPreco = cotacaoProduto.Preco_04;
                indiceFornecedor = "04";
            }
            if (cotacaoProduto.Preco_05 > 0 && menorPreco > cotacaoProduto.Preco_05)
            {
                menorPreco = cotacaoProduto.Preco_05;
                indiceFornecedor = "05";
            }
            if (cotacaoProduto.Preco_06 > 0 && menorPreco > cotacaoProduto.Preco_06)
            {
                menorPreco = cotacaoProduto.Preco_06;
                indiceFornecedor = "06";
            }

            return indiceFornecedor;



        }
    }
}
