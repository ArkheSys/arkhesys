using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.SISeCommerce.Modelo;
using cwkGestao.Integracao.SISeCommerce.Modelo.XML;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Utils;
using cwkGestao.Negocio.Tributacao;
using cwkGestao.Negocio.Tributacao.Impl;
using System.Threading;

namespace cwkGestao.Integracao.SISeCommerce.Util
{
    public class ConversorSisGestao
    {
        private static string[] extensions = { ".txt", ".xml" };
        private static SisECommerceConfiguracao conf;
        private static Object thisLock = new Object();

        public static void ImportarPedidoSIS(object source, FileSystemEventArgs e)
        {
            Action met = () => ExecutarImportacaoSIS(e);
            Thread t = new Thread(new ThreadStart(met));
            t.Start();
        }

        private static void ExecutarImportacaoSIS(FileSystemEventArgs e)
        {
            string ext = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();
            conf = SisECommerceConfiguracaoController.Instancia.GetAll().FirstOrDefault();

            if (extensions.Any(ext.Equals))
            {
                try
                {
                    bool migrado = false;
                    if (ext.Contains("txt"))
                    {
                        IList<PedidoSISeCText> PedidosSIS = SISeCommerceTXTParser.ParseText(e.FullPath);
                        foreach (PedidoSISeCText item in PedidosSIS)
                        {
                            pedido p = FormatTXT2XML(item);
                            IncluirPedido(p, e.FullPath, out migrado);
                        }
                    }
                    if (ext.Contains("xml"))
                    {
                        try
                        {
                            pedido PedidoSIS = SISeCommerceXMLParser.ParseXml(e.FullPath);
                            IncluirPedido(PedidoSIS, e.FullPath, out migrado);
                        }
                        catch (Exception x)
                        {

                            throw new SISeCommerceException(x);
                        }
                    }
                    if (migrado)
                    {
                        try
                        {
                            if (!File.Exists(Path.Combine(conf.DirPosSincronia, Path.GetFileName(e.FullPath))))
                            {
                                File.Move(e.FullPath, Path.Combine(conf.DirPosSincronia, Path.GetFileName(e.FullPath)));
                            }
                            else
                            {
                                Thread.Sleep(10);
                                File.Replace(e.FullPath, Path.Combine(conf.DirPosSincronia, Path.GetFileName(e.FullPath)),
                                    Path.Combine(conf.DirPosSincronia, "bkp" + DateTime.Now.Millisecond + Path.GetFileName(e.FullPath)));
                            }
                            
                        }
                        catch (Exception f)
                        {
                            Exception g = NFSEUtils.Tratador(f);
                            int num = 0;
                            try
                            {
                                string arquivo = Path.GetFileNameWithoutExtension(e.FullPath);
                                num = Convert.ToInt32(arquivo);
                            }
                            catch (Exception)
                            {
                                num = 0;
                            }
                            AdicionaPedidoSincronismoPendente(null, num, g.Message + " - " + g.InnerException.Message, e.FullPath);
                        }
                    }
                }
                catch (Exception v)
                {
                    int num = 0;
                    try
                    {
                        string arquivo = Path.GetFileNameWithoutExtension(e.FullPath);
                        num = Convert.ToInt32(arquivo);
                    }
                    catch (Exception)
                    {
                        num = 0;
                    }
                    AdicionaPedidoSincronismoPendente(null, num, v.Message + v.InnerException == null ? "" :  " - " + v.InnerException.Message, e.FullPath);
                }
            }
        }

        private static void AdicionaPedidoSincronismoPendente(Pedido pedido, int codigoPedidoSIS, string erro, string caminho)
        {
            Pessoa p = pedido == null ? null : pedido.Pessoa;
            SincronismoPendenteSisECommerce sincPendente = new SincronismoPendenteSisECommerce()
            {
                Erro = erro,
                Pessoa = p,
                NumPedidoSisEcommerce = codigoPedidoSIS,
                IncData = DateTime.Now,
                CaminhoArquivo = caminho
            };
            SincronismoPendenteSisECommerceController.Instancia.Salvar(sincPendente, Acao.Incluir);
        }

        #region Metodos Auxiliares

        public static void IncluirPedido(pedido p, string caminhoArquivo, out bool concluido)
        {
            Pedido pedidoGestao = null;

            lock (thisLock)
            {
                try
                {
                    IList<Pedido> pedidosEncontrados = PedidoController.Instancia.GetAllPorIdSISeCommerce(p.CodigoPedido);
                    if (pedidosEncontrados.Count > 0)
                    {
                        concluido = true;
                        return;
                    }
                    #region start pedido

                    pedidoGestao = new Pedido()
                    {
                        Observacao1 = p.ObsCliente,
                        Observacao2 = p.InfoAdicional1,
                        Observacao3 = p.InfoAdicional2,
                        Dt = p.Data,
                        DtDigitacao = DateTime.Now,
                        ValorFrete = p.TotalFrete,
                        Ent_Sai = 2, 
                        IDPedidoSISeCommerce = p.CodigoPedido
                    };
                    var PedidosPendente = new SincronismoPendenteSisECommerce();
                    bool ProdutoNaoEncontrado = false;
                    string ErroSKUNaoEncontrado = "";
                    foreach (produto item in p.PedidoItens.Produtos)
                    {
                        var listaSku = ProdutoSKUController.Instancia.GetProdutoPorSKU(item.CodigoReferencia);

                        if (listaSku.Count == 0)
                        {
                            ProdutoNaoEncontrado = true;
                            ErroSKUNaoEncontrado = ErroSKUNaoEncontrado + item.Nome + " (" + item.CodigoReferencia + "), ";
                        }
                    }
                    conf = SisECommerceConfiguracaoController.Instancia.GetAll().FirstOrDefault();
                    if (conf == null)
                    {
                        concluido = false;
                        AdicionaPedidoSincronismoPendente(pedidoGestao, p.CodigoPedido,
                            "A configuração do SIS eCommerce não foi realizada, Verifique.", caminhoArquivo);
                        return;
                    }
                    #endregion

                    #region Dados do cliente
                    pedidoGestao.Pessoa = GetDadosPessoa(p);
                    try
                    {
                        if (pedidoGestao.Pessoa.ID == 0)
                        {
                            PessoaController.Instancia.Salvar(pedidoGestao.Pessoa, Acao.Incluir);
                        }
                        else
                        {
                            PessoaController.Instancia.Salvar(pedidoGestao.Pessoa, Acao.Alterar);
                        }
                    }
                    catch (Exception b)
                    {
                        throw new SISeCommerceException(b);
                    }
                    #endregion

                    #region Dados do Pedido
                    PreencherDadosPedido(pedidoGestao, p);

                    if (ProdutoNaoEncontrado)
                    {
                        AdicionaPedidoSincronismoPendente(pedidoGestao, p.CodigoPedido,
                            "Produto(s) inexistente(s) no sistema: " + ErroSKUNaoEncontrado + ". Verifique", caminhoArquivo);
                        concluido = false;
                        return;
                    }

                    GerarItens(pedidoGestao, p);
                    GerarParcelas(pedidoGestao);
                    FormaPagamento f = FormaPagamentoController.Instancia.FindByCodigoSIS(p.CodigoPagamento);
                    if (f != null)
                    {
                        foreach (PedidoParcela item in pedidoGestao.Parcelas)
                        {
                            item.TipoDocumento.FormaPagamento = f;
                        }
                    }
                    else
                    {
                        concluido = false;
                        AdicionaPedidoSincronismoPendente(pedidoGestao, p.CodigoPedido, "Forma de pagamento " + p.CodigoPagamento
                            + " - " + p.FormaPagamento + " não foi encontrada.", caminhoArquivo);
                        return;
                    }
                    try
                    {
                        PedidoController.Instancia.Salvar(pedidoGestao, Acao.Incluir);
                        concluido = true;
                    }
                    catch (Exception x)
                    {
                        concluido = false;
                        throw new SISeCommerceException(x);
                    }
                    #endregion
                }
                catch (Exception c)
                {
                    concluido = false;
                    AdicionaPedidoSincronismoPendente(pedidoGestao, p.CodigoPedido,
                        "Ocorreu um erro na importação: " + c.Message + " | " + c.InnerException.Message, caminhoArquivo);
                }
            }
        }

        private static void RemoveDadosAntigos(Pessoa p)
        {
            if (p.PessoaEmails != null)
            {
                foreach (PessoaEmail email in p.PessoaEmails)
                {
                    if (email.ID != 0)
                    {
                        try
                        {
                            PessoaEmailController.Instancia.Salvar(email, Acao.Excluir);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        private static void GerarItens(Pedido pedidoGestao, pedido p)
        {
            int sequencia = 1;

            try
            {
                foreach (produto prod in p.PedidoItens.Produtos)
                {
                    PedidoItem pedidoItem = null;

                    Produto produtoSelecionado = ProdutoSKUController.Instancia.FindProdutoPorSKU(prod.CodigoReferencia);

                    if (pedidoItem == null)
                    {
                        pedidoItem = new PedidoItem();

                        pedidoItem.Dt = pedidoGestao.Dt;
                        pedidoItem.Ent_Sai = pedidoGestao.Ent_Sai;
                        pedidoItem.Pedido = pedidoGestao;
                        pedidoItem.Sequencia = sequencia;

                        decimal produtoPreco = ProdutoController.Instancia.getPreco(produtoSelecionado, pedidoGestao.TabelaPreco, TipoPrecoType.Normal);

                        pedidoItem.ValorCalculado = produtoPreco;
                        pedidoItem.Valor = produtoPreco;
                        pedidoItem.ValorProduto = prod.Preco;
                        pedidoItem.Quantidade = 0m;
                        pedidoItem.Produto = produtoSelecionado;
                        pedidoItem.Unidade = pedidoItem.Produto.Unidade.Sigla;
                        pedidoItem.ProdutoNome = pedidoItem.Produto.Nome;
                        pedidoItem.ProdutoDescReduzida = pedidoItem.Produto.DescReduzida;

                        pedidoGestao.Items.Add(pedidoItem);
                    }

                    pedidoItem.InformacoesAdicionais = prod.InfoVariacao;

                    pedidoItem.Quantidade = prod.Quantidade;
                    pedidoItem.SubTotal = (pedidoItem.Quantidade * pedidoItem.Valor);
                    pedidoItem.Total = pedidoItem.SubTotal;

                    sequencia++;

                    pedidoGestao.TipoPedido = TipoPedidoController.Instancia.LoadObjectById(pedidoGestao.TipoPedido.ID);
                    pedidoGestao.Filial = conf.Filial;
                    if (pedidoGestao.Filial != null && pedidoGestao.TipoPedido != null && pedidoGestao.TipoPedido.CalcularST)
                    {
                        foreach (var item in pedidoGestao.Items)
                        {
                            var produto = ProdutoController.Instancia.LoadObjectById(item.Produto.ID);
                            item.Produto = produto;
                            var tributacao = new Tributacao(new PedidoItemTributadoMediator(item, pedidoGestao));
                            tributacao.CalculaIcms();
                        }
                    }
                    else
                    {
                        foreach (var item in pedidoGestao.Items)
                        {
                            item.ValorRetidoICMS = 0;
                            item.BaseICMSSubst = 0;
                        }
                    }
                    Configuracao Conf = ConfiguracaoController.Instancia.GetConfiguracao();
                    try
                    {
                        NCM ncm = NCMController.Instancia.LoadObjectById(pedidoItem.Produto.ID_NCM);
                        //pedidoItem.AliquotaIPI = pedidoItem.Produto.AliquotaIPI == 0 ? ncm.Ipi : pedidoItem.Produto.AliquotaIPI;
                    }
                    catch (Exception)
                    {
                        pedidoItem.AliquotaIPI = 0;
                    }
                    pedidoGestao.CalculaTotalPedido(Conf.NaoSomarFrete, Conf.NaoSomarIpiTotalNota);
                }

                //apos adicionar os itens atualiza os valores (rotinas chamadas para subtrair o valor do ipi do valor do produto)
                Configuracao configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
                pedidoGestao.RepassarDescontoPedidoParaItem();
                pedidoGestao.RecalcularRateios(configuracao, pedidoGestao.Pessoa.bSTRevenda);
                pedidoGestao.CalculaTotalPedido(configuracao.NaoSomarFrete, configuracao.NaoSomarIpiTotalNota);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static void PreencherDadosPedido(Pedido pedidoGestao, pedido p)
        {
            conf = SisECommerceConfiguracaoController.Instancia.GetAll().FirstOrDefault();
            int proximoCodigoDisponivel = PedidoController.Instancia.MaxCodigoIntegracao();
            int proximoNumeroDisponivel = PedidoController.Instancia.MaxNumero(conf.Filial, conf.TipoOrcamento);
            var Vendedor = PessoaVendedorController.Instancia.GetVendedorPorIDPessoa(conf.Vendedor.ID);

            pedidoGestao.Filial = conf.Filial;
            pedidoGestao.Ent_Sai = (int)InOutType.Saída;
            pedidoGestao.Dt = Convert.ToDateTime(p.Data);
            pedidoGestao.DtPrazoEntrega = pedidoGestao.Dt.AddDays(1);
            pedidoGestao.DtDigitacao = pedidoGestao.Dt;
            pedidoGestao.CodigoPedidoMagento = Convert.ToInt32(p.CodigoPedido);
            pedidoGestao.TipoPedido = conf.TipoOrcamento;
            pedidoGestao.LocalEstoquePedido = conf.TipoOrcamento.LocalEstoque;
            pedidoGestao.Vendedor = conf.Vendedor;
            pedidoGestao.PessoaUF = pedidoGestao.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
            pedidoGestao.IDVendedor = Vendedor.ID; //pedidoGestao.Vendedor.PessoaVendedores.First().ID;
            pedidoGestao.TabelaPreco = conf.TabelaPreco;
            pedidoGestao.Condicao = conf.Condicao;
            pedidoGestao.Status = (StatusOrcamento)conf.TipoOrcamento.Status;
            pedidoGestao.Codigo = proximoCodigoDisponivel;
            pedidoGestao.Numero = proximoNumeroDisponivel;
            pedidoGestao.TipoFrete = TipoFrete.Destinatario;
            pedidoGestao.ValorFrete = p.ValorFrete;
            pedidoGestao.EnderecoEntrega = pedidoGestao.Pessoa.EnderecoPrincipal();
            if (p.TotalDescontoAcrescimo.ToLower().Contains("desconto"))
            {
                pedidoGestao.ValorDesconto = p.TotalDescontoAcrescimoValor;
            }
            else
            {
                pedidoGestao.ValorAcrescimo = p.TotalDescontoAcrescimoValor;
            }
            pedidoGestao.ValorAcrescimo += p.TotalAcrescimoAdicional;
            pedidoGestao.PessoaNome = pedidoGestao.Pessoa.Nome;
        }


        public static Pessoa GetDadosPessoa(pedido p)
        {
            try
            {
                Pessoa pessoa = PessoaController.Instancia.FindByIDSISeCommerce(Convert.ToInt32(p.ClienteCodigo));
                if (pessoa == null)
                {
                    pessoa = new Pessoa();
                }
                pessoa.Nome = p.ClienteNome;
                pessoa.TipoPessoa = p.ClienteTipo;
                pessoa.Apelido = p.ClienteApelido;
                pessoa.Email1 = p.ClienteEmail;
                pessoa.CNPJ_CPF = p.ClienteDocumento;
                pessoa.BCliente = true;
                pessoa.Filial = conf.Filial;
                pessoa.IDSISeCommerce = Convert.ToInt32(p.ClienteCodigo);
                Classificacao Classificacao = ClassificacaoController.Instancia.LoadObjectById(1);
                if (Classificacao != null)
                {
                    pessoa.Classificacao = Classificacao;
                }
                pessoa.PessoaEmails = pessoa.PessoaEmails == null ? new List<PessoaEmail>() : pessoa.PessoaEmails;
                pessoa.PessoaEnderecos = pessoa.PessoaEnderecos == null ? new List<PessoaEndereco>() : pessoa.PessoaEnderecos;
                pessoa.PessoaTelefones = pessoa.PessoaTelefones == null ? new List<PessoaTelefone>() : pessoa.PessoaTelefones;
                pessoa.Observacao = p.ObsCliente;
                pessoa.BAtivo = true;
                pessoa.DtCadastro = DateTime.Now;
                pessoa.BContribuinte = false;
                pessoa.PessoaClientes = pessoa.PessoaClientes == null ? new List<PessoaCliente>() : pessoa.PessoaClientes;

                GerarPessoaCliente(pessoa, p);
                GerarPessoaEmail(pessoa, p);
                GerarPessoaEndereco(pessoa, p);
                GerarPessoaTelefone(pessoa, p);

                if (p.ClienteTipo.ToLower().Contains("fisica"))
                {
                    pessoa.TipoPessoa = "Física";
                }
                else
                {
                    pessoa.TipoPessoa = "Jurídica";
                }
                
                if (p.ClienteIeRg.ToLower().Contains("rg"))
                {
                    pessoa.RG = p.ClienteIeRgDocumento;
                }
                else
                {
                    pessoa.Inscricao = p.ClienteIeRgDocumento;
                }
                return pessoa;
            }
            catch (Exception w)
            {
                throw w;
            }

        }

        private static void GerarPessoaEmail(Pessoa pessoa, pedido p)
        {
            PessoaEmail e01 = new PessoaEmail()
            {
                Pessoa = pessoa,
                Email = p.ClienteEmail,
                BCompra = true,
                BContato = true,
                BFinanceiro = true,
                BNFe = true,
                BMsn = true, 
                BVenda = true, 
                BSugestao = true, 
                BNFSe = true
            };
            if (pessoa.PessoaEmails.Count > 0)
            {
                PessoaEmail e = pessoa.PessoaEmails.FirstOrDefault();
                e.BCompra = true;
                e.BContato = true;
                e.BFinanceiro = true;
                e.BMsn = true;
                e.BNFe = true;
                e.BNFSe = true;
                e.BSugestao = true;
                e.BVenda = true;
                e.Email = e01.Email;
            }
            else
            {
                pessoa.PessoaEmails.Add(e01);
            }
        }

        private static void GerarPessoaTelefone(Pessoa pessoa, pedido p)
        {
            PessoaTelefone tel01 = new PessoaTelefone()
            {
                IDPessoa = pessoa.ID,
                Pessoa = pessoa,
                Numero = p.ClienteTel1,
                BPrincipal = true
            };
            PessoaTelefone tel02 = null;
            if (!String.IsNullOrEmpty(p.ClienteTel2))
            {
                tel02 = new PessoaTelefone()
                {
                    IDPessoa = pessoa.ID,
                    Pessoa = pessoa,
                    Numero = p.ClienteTel2,
                    BPrincipal = false
                };
            }
            if (pessoa.PessoaTelefones.Count > 0)
            {
                foreach (PessoaTelefone tel in pessoa.PessoaTelefones)
                {
                    tel.BPrincipal = false;
                }
                PessoaTelefone t1 = pessoa.PessoaTelefones.FirstOrDefault();
                t1.Numero = tel01.Numero;
                t1.BPrincipal = true;
                if (pessoa.PessoaTelefones.Count > 1 && tel02 != null)
                {
                    pessoa.PessoaTelefones[1].Numero = tel02.Numero;
                }
            }
            else
            {
                pessoa.PessoaTelefones.Add(tel01);
                if (tel02 != null)
                {
                    if (!String.IsNullOrEmpty(tel02.Numero))
                    {
                        pessoa.PessoaTelefones.Add(tel02);
                    }
                }

            }
        }

        private static void GerarPessoaCliente(Pessoa pessoa, pedido p)
        {
            if (pessoa.PessoaClientes.Count == 0)
            {
                pessoa.PessoaClientes.Add(new PessoaCliente()
                {
                    Pessoa = pessoa,
                    BContribuinte = false,
                    AltData = DateTime.Now,
                    Condicao = conf.Condicao,
                    IDCondicao = conf.Condicao.ID,
                    IDPessoa = pessoa.ID,
                });
            }
            else
            {
                PessoaCliente pc = pessoa.PessoaClientes.FirstOrDefault();
                pc.Pessoa = pessoa;
                pc.BContribuinte = false;
                pc.AltData = DateTime.Now;
                pc.Condicao = conf.Condicao;
                pc.IDCondicao = conf.Condicao.ID;
                pc.IDPessoa = pessoa.ID;
            }
        }

        private static void GerarParcelas(Pedido pedidoGestao)
        {
            pedidoGestao.Parcelas = CondicaoController.Instancia.GerarParcelas(pedidoGestao);
        }

        private static pedido FormatTXT2XML(PedidoSISeCText txt)
        {
            pedido result = new pedido()
            {
                CodigoPedido = txt.PEDIDO,
                Data = txt.DATA,
                CodigoCanalVenda = txt.LOCAL,
                PedidoStatus = txt.STATUS,
                ClienteCodigo = txt.CLIENTE_COD.ToString(),
                ClienteEmail = txt.CLIENTE_EMAIL,
                ClienteNome = txt.CLIENTE_NOME,
                ClienteEndereco = txt.CLIENTE_ENDERECO,
                ClienteComplemento = txt.CLIENTE_COMPLEMENTO,
                ClienteBairro = txt.CLIENTE_BAIRRO,
                ClienteCidade = txt.CLIENTE_CIDADE,
                ClienteUF = txt.CLIENTE_UF,
                ClienteCep = txt.CLIENTE_CEP,
                ClienteTel1 = txt.CLIENTE_TELEFONE,
                ClienteApelido = txt.PEDIDO_APELIDO,
                ClienteTipo = txt.CLIENTE_TIPO,
                ClienteTipoDocumento = txt.CLIENTE_TIPO.ToLower().Contains("jurídica") ? "CNPJ" : "CPF",
                ClienteDocumento = txt.CLIENTE_CPF_CNPJ,
                ClienteIeRg = txt.CLIENTE_TIPO.ToLower().Contains("jurídica") ? "IE" : "RG",
                ClienteIeRgDocumento = txt.CLIENTE_RG_IE,
                FormaFrete = txt.PEDIDO_FRETE_TIPO,
                ValorFrete = txt.PEDIDO_FRETE,
                PesoFrete = txt.PEDIDO_PESO,
                DtEnvioFrete = txt.PEDIDO_ENVIO_DATA,
                CodigoRastreamento = txt.PEDIDO_COD_RASTREAMENTO,
                FormaPagamento = txt.PEDIDO_FORMA_PAGAMENTO,
                CodigoPagamento = txt.PEDIDO_FORMA_PAGAMENTO.Split('|')[1],
                ValorPagamento = txt.PEDIDO_DA_PAGAMENTO,
                TotalProduto = txt.PEDIDO_VALOR_PRODUTOS,
                TotalFrete = txt.PEDIDO_FRETE,
                SubTotal = txt.PEDIDO_SUBTOTAL,
                ObsCliente = txt.PEDIDO_INFO_AD_CLIENTE,
                ObsLoja = txt.PEDIDO_INFO_AD_LOJA,
                PedidoItens = new produtos()
            };
            result.PedidoItens.Produtos = new List<produto>();
            foreach (PedidoItemSISeCText item in txt.PEDIDO_ITENS)
            {
                result.PedidoItens.Produtos.Add(new produto()
                {
                    Codigo = item.CODIGO_PRODUTO, 
                    CodigoReferencia = item.COD_REFERENCIA, 
                    CodigoVariacao = item.OPCOES_PRODUTO, 
                    InfoVariacao = item.OPCOES_PRODUTO, 
                    Nome = item.PRODUTO, 
                    Preco = item.PRECO_UNITARIO, 
                    PrecoTotal = item.PRECO_TOTAL, 
                    PrecoCusto = item.PRECO_UNITARIO, 
                    Quantidade = item.QUANTIDADE
                });
            }

            return result;
        }

        private static void GerarPessoaEndereco(Pessoa pessoa, pedido p)
        {
            PessoaEndereco endNovo = new PessoaEndereco()
                {
                    Endereco = p.ClienteEndereco,
                    Bairro = p.ClienteBairro,
                    CEP = p.ClienteCep,
                    Complemento = p.ClienteComplemento,
                    Numero = p.ClienteEnderecoNumero == null ? "0" : p.ClienteEnderecoNumero,
                    Pessoa = pessoa,
                    BAtivo = true,
                    BCobranca = true,
                    BPrincipal = true,
                    BEntrega = true
                };
            endNovo.Cidade = GetCidade(p);
            if (pessoa.PessoaEnderecos.Count > 0)
            {
                IList<PessoaEndereco> enderecosExistentes = new List<PessoaEndereco>();
                foreach (PessoaEndereco end in pessoa.PessoaEnderecos)
                {
                    if (end.Equals(endNovo))
                    {
                        enderecosExistentes.Add(end);
                    }
                    else
                    {
                        end.BPrincipal = false;
                        end.BAtivo = false;
                        end.BCobranca = false;
                        end.BEntrega = false;
                    }
                }
                if (enderecosExistentes.Count == 0)
                {
                    pessoa.PessoaEnderecos.Add(endNovo);
                }
                else
                {
                    foreach (PessoaEndereco item in enderecosExistentes)
                    {
                        item.BPrincipal = false;
                        item.BAtivo = false;
                        item.BCobranca = false;
                        item.BEntrega = false;
                    }
                    PessoaEndereco end = enderecosExistentes.FirstOrDefault();
                    foreach (PessoaEndereco item in pessoa.PessoaEnderecos)
                    {
                        if (item.Equals(end))
                        {
                            item.BPrincipal = true;
                            item.BAtivo = true;
                            item.BCobranca = true;
                            item.BEntrega = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                pessoa.PessoaEnderecos.Add(endNovo);
            }
        }

        private static Cidade GetCidade(pedido p)
        {
            Cidade c = CidadeController.Instancia.GetByLikeName(p.ClienteCidade, p.ClienteUF);
            if ((c == null))
            {
                Cidade c2 = CidadeController.Instancia.GetByLikeName("Indefinida", "XX");
                if (c2 == null)
                {
                    UF uf = new UF()
                    {
                        Nome = "XX",
                        Sigla = "XX",
                        Pais = PaisController.Instancia.GetAll().Where(n => n.Nome.ToLower().Contains("brasil")).FirstOrDefault(),
                        Codigo = UFController.Instancia.MaxCodigo(),
                        AltData = DateTime.Now,
                        IncData = DateTime.Now,
                        IBGE = "xxxxx"

                    };
                    UFController.Instancia.Salvar(uf, Acao.Incluir);
                    Cidade nova = new Cidade()
                    {
                        IDUF = uf.ID,
                        IncData = DateTime.Now,
                        AltData = DateTime.Now,
                        Codigo = CidadeController.Instancia.MaxCodigo(),
                        IBGE = "xxxxxxxx",
                        Nome = "Indefinida",
                        UF = uf
                    };
                    CidadeController.Instancia.Salvar(nova, Acao.Incluir);
                }
                c = CidadeController.Instancia.GetByLikeName("Indefinida", "XX");
            }
            return c;
        }

        #endregion

    }

    public class SISeCommerceException : Exception
    {
        private static string MensagemInterna = "Ocorreu um erro na rotina de integração SIS e-Commerce: ";
        public SISeCommerceException(Exception inner)
            :base(MensagemInterna + inner.Message , inner)
        {
        }
    }
}
