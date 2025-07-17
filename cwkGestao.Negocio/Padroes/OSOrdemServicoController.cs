using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio.ImpOrdemServico;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio.Financeiro;
using System.Xml;
using Microsoft.Reporting.WinForms;
using System.Data;
using cwkGestao.Negocio.Padroes;

namespace cwkGestao.Negocio
{
    public class OSOrdemServicoController : BaseController<OSOrdemServico>
    {

        #region Singleton
        private static OSOrdemServicoController instance;
        private static IRepositorioOSOrdemServico repositorioBanco;

        private OSOrdemServicoController()
        { }

        static OSOrdemServicoController()
        {
            instance = new OSOrdemServicoController();
            repositorioT = RepositorioFactory<OSOrdemServico>.GetRepositorio();
            repositorioBanco = (IRepositorioOSOrdemServico)repositorioT;
        }

        public static OSOrdemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(OSOrdemServico objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(OSOrdemServico objeto, Acao acao)
        {
            VerificarAcompanhamentos(objeto);
            if (acao == Modelo.Acao.Incluir)
            {
                OSOrdemServico osExiste = repositorioBanco.GetOSporCodigo(objeto.Codigo);
                if (osExiste != null && osExiste.ID > 0)
                {
                    objeto.Codigo = repositorioBanco.MaxCodigo();
                }
            }
            return base.Salvar(objeto, acao);
        }

        private void VerificarAcompanhamentos(OSOrdemServico osOrdemServico)
        {
            if (osOrdemServico.ID == 0)
            {
                AcompanhamentosInclusao(osOrdemServico);
            }
            else
            {
                AcompanhamentoAlteracao(osOrdemServico);
            }
        }

        private void AcompanhamentoAlteracao(OSOrdemServico osOrdemServico)
        {
            OSOrdemServico osOrdemServicoOriginal = this.LoadObjectById(osOrdemServico.ID);

            StringBuilder stringOcorrencia = new StringBuilder();
            stringOcorrencia.Append("Alteração de OS");

            StringBuilder alteracoes = new StringBuilder();
            alteracoes.Append(AlteracaoStatus(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoCliente(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoEquipamento(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoAcessorio(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoServicoSolicitado(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoResultadoTecnico(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoResultadoCliente(osOrdemServico, osOrdemServicoOriginal));
            alteracoes.Append(AlteracaoValorTotal(osOrdemServico, osOrdemServicoOriginal));

            if (alteracoes.Length > 0)
            {
                stringOcorrencia.AppendLine(Environment.NewLine + alteracoes.ToString());

                OSAcompanhamento osAcompanhamento = new OSAcompanhamento();
                osAcompanhamento.DataHora = DateTime.Now;
                osAcompanhamento.Ocorrencia = stringOcorrencia.ToString();
                osAcompanhamento.OSOrdemServico = osOrdemServico;
                osAcompanhamento.EhAutomatica = true;
                osAcompanhamento.Usuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;

                osOrdemServico.OSAcompanhamentos.Add(osAcompanhamento);
            }

        }

        public List<Object> recibo(OSOrdemServico os, decimal valorBaixa)
        {
            string Referente = " Código OS. " + os.Codigo + " Data " + String.Format("{0: dd/MM/yyyy}", os.Data);
            return recibo(os.Cliente.EnderecoPrincipal().ToString(), 
                          os.Empresa.Endereco + ", " + os.Empresa.Numero + " - " + os.Empresa.Bairro + " - " + os.Empresa.Cidade + "-" + os.Empresa.Cidade.UF, 
                          Referente, 
                          valorBaixa,
                          os.Empresa.Nome, 
                          (os.Cliente.Nome),
                          os.Empresa.CNPJ);
        }

        public List<Object> recibo(String EndCliente, String EndEmpresa, String pReferente, Decimal pValor, String pFilial, String pPessoa, String pCNPJ_CPF)
        {
            List<Object> ParametrosForm = new List<Object>();

            string ValorPorExtenso = ClsExtenso.Extenso_Valor((Decimal)pValor).ToUpper();
            DateTime DataAtual = DateTime.Now;

            var parametros = new List<ReportParameter>();
            var p1 = new ReportParameter("DataAtual", DataAtual.ToShortDateString().ToString());
            var p2 = new ReportParameter("ValorPorExtenso", ValorPorExtenso);
            var p3 = new ReportParameter("Valor", pValor.ToString());

            parametros.Add(p1);
            parametros.Add(p2);
            parametros.Add(p3);

            var p4 = new ReportParameter("Recebemos", pPessoa);
            var p5 = new ReportParameter("EnderecoDeCima", EndCliente);
            var p6 = new ReportParameter("Referente", pReferente);
            var p7 = new ReportParameter("Emitente", pFilial);
            var p8 = new ReportParameter("EnderecoDeBaixo", EndEmpresa);
            var p9 = new ReportParameter("CNPJ_CPF", pCNPJ_CPF);

            parametros.Add(p4);
            parametros.Add(p5);
            parametros.Add(p6);
            parametros.Add(p7);
            parametros.Add(p8);
            parametros.Add(p9);

            XmlDocument documento = new XmlDocument();
            documento.Load(global ::Modelo.cwkGlobal.DirApp + @"\ConfiguracaoCwork.xml");
            XmlNodeReader leitor = new XmlNodeReader(documento);

            string nomeRelatorio = "rptRecibo.rdlc";
            while (leitor.Read())
            {
                if (leitor.Value == "2 via")
                    nomeRelatorio = "rptRecibo2Vias.rdlc";
            }

            ParametrosForm.Add(@"Recibo\" + nomeRelatorio);
            ParametrosForm.Add("dsRelRecibo_Recibo");
            ParametrosForm.Add(new DataTable());
            ParametrosForm.Add(parametros);

            return ParametrosForm;
        }

        private string AlteracaoValorTotal(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.Total != osOrdemServicoOriginal.Total
               ? "Valor Total: " + String.Format("{0:C2}", osOrdemServico.Total) + Environment.NewLine
               : "";

        }

        private string AlteracaoResultadoCliente(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.ResultadoCliente != osOrdemServicoOriginal.ResultadoCliente
                           ? "Resultado Cliente: " + osOrdemServico.ResultadoCliente + Environment.NewLine
                           : "";
        }

        private string AlteracaoResultadoTecnico(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.ResultadoTecnico != osOrdemServicoOriginal.ResultadoTecnico
                           ? "Resultado Técnico: " + osOrdemServico.ResultadoTecnico + Environment.NewLine
                           : "";
        }

        private string AlteracaoAcessorio(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.Acessorios != osOrdemServicoOriginal.Acessorios
                           ? "Acessórios: " + osOrdemServico.Acessorios + Environment.NewLine
                           : "";
        }


        private string AlteracaoCliente(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.Cliente.ID != osOrdemServicoOriginal.Cliente.ID
                           ? "Cliente: " + osOrdemServico.Cliente.Codigo + " | " + osOrdemServico.Cliente.Nome + Environment.NewLine
                           : "";
        }

        private string AlteracaoServicoSolicitado(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.ProblemaRelatado != osOrdemServicoOriginal.ProblemaRelatado
                           ? "Serviço Solicitado: " + osOrdemServico.ProblemaRelatado + Environment.NewLine
                           : "";
        }

        private string AlteracaoEquipamento(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            return osOrdemServico.Equipamento.ID != osOrdemServicoOriginal.Equipamento.ID
                           ? "Equipamento: " + osOrdemServico.Equipamento.Codigo + " | " + osOrdemServico.Equipamento.ToString() + Environment.NewLine
                           : "";
        }

        private string AlteracaoStatus(OSOrdemServico osOrdemServico, OSOrdemServico osOrdemServicoOriginal)
        {
            //return osOrdemServico.Status != osOrdemServicoOriginal.Status
            //               ? "Status: " + OSOrdemServico.StatusToString(osOrdemServico.Status) + Environment.NewLine
            //               : "";
            return osOrdemServico.Status != osOrdemServicoOriginal.Status
                           ? "Status: " + osOrdemServico.Status.Descricao + Environment.NewLine
                           : "";
        }

        private void AcompanhamentosInclusao(OSOrdemServico osOrdemServico)
        {
            string ocorrencia = ConstroiAcompanhamentoInclusao(osOrdemServico);

            OSAcompanhamento osAcompanhamento = new OSAcompanhamento();
            osAcompanhamento.EhAutomatica = true;
            osAcompanhamento.DataHora = DateTime.Now;
            osAcompanhamento.OSOrdemServico = osOrdemServico;
            osAcompanhamento.Ocorrencia = ocorrencia;
            osAcompanhamento.Usuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;

            osOrdemServico.OSAcompanhamentos.Add(osAcompanhamento);
        }

        private string ConstroiAcompanhamentoInclusao(OSOrdemServico osOrdemServico)
        {
            StringBuilder stringOcorrencia = new StringBuilder();
            stringOcorrencia.AppendLine("Abertura de OS.");
            stringOcorrencia.AppendLine("Status: " + osOrdemServico.Status);
            stringOcorrencia.AppendLine("Cliente:" + osOrdemServico.Cliente.Codigo + " | " + osOrdemServico.Cliente.Nome);
            stringOcorrencia.AppendLine("Equipamento: " + osOrdemServico.Equipamento.Codigo + " | " + osOrdemServico.Equipamento.ToString());
            stringOcorrencia.AppendLine("Acessórios: " + osOrdemServico.Acessorios);
            stringOcorrencia.AppendLine("Serviço Solicitado: " + osOrdemServico.ProblemaRelatado);
            stringOcorrencia.AppendLine("Resultado Técnico: " + osOrdemServico.ResultadoTecnico);
            stringOcorrencia.AppendLine("Resultado Cliente: " + osOrdemServico.ResultadoCliente);
            stringOcorrencia.AppendLine("Valor Total: " + String.Format("{0:C2}", osOrdemServico.Total));
            return stringOcorrencia.ToString();
        }

        public IList<object> GetListagemOrdemServico(int pStatus, string pCliente, DateTime pDataInicial, DateTime pDataFinal, string pEmpresas)
        {
            return repositorioBanco.GetListagemOrdemServico(pStatus, pCliente, pDataInicial, pDataFinal, pEmpresas);
        }

        public IList<object> GetOrdemServicoImpressao(int pIDOS)
        {
            return repositorioBanco.GetOrdemServicoImpressao(pIDOS);
        }

        public OSOrdemServico GetOSporCodigo(int pCodOS)
        {
            return repositorioBanco.GetOSporCodigo(pCodOS);
        }

        public void ImprimirMatricial(OSOrdemServico osOrdemServico)
        {
            new ImpressaoMatricial(repositorioBanco.LoadFullObjectByID(osOrdemServico.ID));
        }

        public IList<Modelo.Proxy.pxyFaturamentoOS> GetFaturamento(Modelo.Pessoa pessoa, Modelo.Filial filial, DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioBanco.GetFaturamento(pessoa, filial, dataInicial, dataFinal);
        }

        public IList<Modelo.Proxy.pxyFaturamentoOS> GetFaturamento(OSOrdemServico ordemServico)
        {
            var ret = new List<Modelo.Proxy.pxyFaturamentoOS>();
            ret.Add(new Modelo.Proxy.pxyFaturamentoOS(
                ordemServico.ID,
                ordemServico.Codigo,
                ordemServico.Data,
                ordemServico.OSProdutoItems.Count,
                ordemServico.OSServicoItems.Count,
                ordemServico.ProdutoDescontoValor,
                ordemServico.ServicoDescontoValor,
                ordemServico.Total, 
                ordemServico.Equipamento == null ? "" : ordemServico.Equipamento.ToString(), 
                ordemServico.DespesasAdicionais));
            return ret;
        }

        public List<OSOrdemServico> Get(List<int> ids)
        {
            return repositorioBanco.Get(ids);
        }

        public bool EstahAgendado(OSOrdemServico selecionado)
        {
            return repositorioBanco.EstahAgendado(selecionado);
        }

        public void FaturarAtendimentos(FaturamentoAtendimento fatOK)
        {           
            ParametrosContratoProduto parms = ParametrosContratoProdutoController.Instancia.GetAll().FirstOrDefault();
            OSOrdemServico OS = CriaOSFaturamentoAtendimento(fatOK);
            IList<pxyFaturamentoOS> orcamentosServicoFaturar = GetFaturamento(OS);
            Negocio.Faturamento.OrdemServico faturamento = Negocio.Faturamento.OrdemServico
                .Get(orcamentosServicoFaturar);
            faturamento.Filial = parms.Filial;
            faturamento.Cliente = fatOK.Cliente;
            faturamento.DataFaturamento = DateTime.Now.Date;
            faturamento.Condicao = parms.Condicao;
            faturamento.TipoMovimentacao1 = parms.TipoNota;
            faturamento.TipoMovimentacao2 = parms.TipoNotaServico;
            faturamento.Observacao1 = String.Empty;
            faturamento.Observacao2 = String.Empty;
            faturamento.Observacao3 = String.Empty;
            faturamento.ItemsSelecionados = orcamentosServicoFaturar;
            Nota nota = new Nota(); ;
            try
            {
                Parcelamento parcelamento = new Financeiro.Parcelamento() { Condicao = parms.Condicao };
                faturamento.Parcelas = parcelamento.GerarParcelas(DateTime.Now.Date, fatOK.Total, 0);
            }
            catch (Exception e)
            {
                throw e;
            }
            try
            {
                faturamento.Faturar();
                foreach (Atendimento att in fatOK.AtendimentosVinculados)
                {
                    nota = faturamento.notasGeradas.Where(w => w.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos).FirstOrDefault();
                    
                    if (nota != null)
                    {
                        try
                        {
                            att.Nota = nota;
                            AtendimentoController.Instancia.Salvar(att, Acao.Alterar);
                        }
                        catch (Exception a)
                        {
                            throw a;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void FaturarAtendimentos(ref IList<FaturamentoAtendimento> fatsOK)
        {
            StringBuilder sb = new StringBuilder();
            foreach (FaturamentoAtendimento item in fatsOK)
            {
                try
                {
                    FaturarAtendimentos(item);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message);
                    if (item.OrdemServico != null)
                    {
                        if (item.OrdemServico.ID > 0)
                        {
                            try
                            {
                                OSOrdemServicoController.Instancia.Salvar(item.OrdemServico, Acao.Excluir);
                            }
                            catch (Exception a)
                            {
                                sb.AppendLine(a.Message);
                            }
                        }
                    }
                    if (item.ID > 0)
                    {
                        try
                        {
                            foreach (Atendimento att in item.AtendimentosVinculados)
                            {
                                att.FatAtendimento = null;
                                AtendimentoController.Instancia.Salvar(att, Acao.Alterar);
                            }
                            FaturamentoAtendimentoController.Instancia.Salvar(item, Acao.Excluir);
                        }
                        catch (Exception a)
                        {
                            sb.AppendLine(a.Message);
                        }
                        item.ID = 0;
                    }
                }
                if (sb.Length > 0)
                {
                    throw new Exception(sb.ToString());
                }
            }
        }

        private OSOrdemServico CriaOSFaturamentoAtendimento(FaturamentoAtendimento fat)
        {
            try
            {
                OSOrdemServico os = new OSOrdemServico();
                OSEquipamento equip = RetornaEquipamento();
                ParametrosContratoProduto parms = ParametrosContratoProdutoController.Instancia.GetAll().FirstOrDefault();
                if (parms == null)
                {
                    throw new Exception("Os Parâmetros de Faturamento de OS não foram configurados. Contate o Suporte.");
                }

                os.Codigo = MaxCodigo();
                os.Equipamento = equip;
                os.Empresa = parms.Filial;
                os.Cliente = fat.Cliente;
                os.Data = DateTime.Now.Date;

                try
                {
                    os.UsuarioLogado = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                }
                catch (Exception)
                {
                    os.UsuarioLogado = "admin";
                }
                os.ProblemaRelatado = String.Empty;
                os.Acessorios = String.Empty;
                os.ResultadoCliente = String.Empty;
                os.ResultadoTecnico = String.Empty;
                os.Condicao = parms.Condicao;
                os.Origem = OSOrdemServico.OrigemOS.Interna;
                os.NecessitaAprovacaoCliente = false;
                os.Status = StatusOSController.Instancia.GetStatusTipoFechamento();
                Pessoa Vendedor = new Pessoa();
                if (fat.Cliente.PessoaClientes != null)
                {
                    if (fat.Cliente.PessoaClientes.Count > 0)
                    {
                        Vendedor = fat.Cliente.PessoaClientes.FirstOrDefault().Vendedor;
                        if (Vendedor == null)
                        {
                            Vendedor = parms.Vendedor;
                        }
                    }
                }
                if (Vendedor.ID == 0)
                {
                    throw new Exception("Não há Vendedor configurado para o Cliente e o Vendedor padrão não foi configurado. Verifique.");
                }
                os.Vendedor = Vendedor;
                os.TabelaPreco = parms.TabelaPreco;
                os.OSServicoItems.Add(new OSServicoItem()
                {
                    OSOrdemServico = os, 
                    Tipo = TipoServico.Propria, 
                    Preco = fat.Total, 
                    Quantidade = 1,
                    PrecoOriginal = fat.Total,
                    Descricao = fat.Servico.Descricao.Length > 100 ? fat.Servico.Descricao.Substring(0, 100) : fat.Servico.Descricao, 
                    Servico = fat.Servico, 
                    Total = fat.Total, 
                    Responsavel = Vendedor, 
                    Seq = 1, 
                    PrecoTerceiro = 0, 
                    DescontoPerc = 0
                });
                os.ServicoSubTotal = fat.Total;
                os.ServicoTotal = fat.Total;
                os.ServicoDescontoValor = 0;
                os.ServicoDescontoPerc = 0;
                os.Total = fat.Total;

                OSOrdemServicoController.Instancia.Salvar(os, Acao.Incluir);
                fat.OrdemServico = os;
                FaturamentoAtendimentoController.Instancia.Salvar(fat, Acao.Alterar);
                return os;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível criar a OS para este faturamento.\r\n" + e.Message ,e);
            }
        }

        private OSOrdemServico CriaOSOrcamentoServico(IList<Pedido> pedidos, Servico servico, Condicao c)
        {
            try
            {
                OSOrdemServico os = new OSOrdemServico();

                os.Codigo = MaxCodigo();
                os.Empresa = pedidos.FirstOrDefault().Filial;
                os.Cliente = pedidos.FirstOrDefault().Pessoa;
                os.Data = DateTime.Now.Date;

                try
                {
                    os.UsuarioLogado = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                }
                catch (Exception)
                {
                    os.UsuarioLogado = "admin";
                }
                os.ProblemaRelatado = String.Empty;
                os.Acessorios = String.Empty;
                os.ResultadoCliente = String.Empty;
                os.ResultadoTecnico = String.Empty;
                os.Condicao = c;
                os.Origem = OSOrdemServico.OrigemOS.Interna;
                os.NecessitaAprovacaoCliente = false;
                os.Status = StatusOSController.Instancia.GetStatusTipoFechamento();
                Pessoa Vendedor = new Pessoa();
                if (pedidos.FirstOrDefault().Pessoa.PessoaClientes != null)
                {
                    if (pedidos.FirstOrDefault().Pessoa.PessoaClientes.Count > 0)
                    {
                        Vendedor = pedidos.FirstOrDefault().Pessoa.PessoaClientes.FirstOrDefault().Vendedor;
                        if (Vendedor == null)
                        {
                            Vendedor = pedidos.FirstOrDefault().Vendedor;
                        }
                    }
                }
                if (Vendedor.ID == 0)
                {
                    throw new Exception("Não há Vendedor configurado para o Cliente e o Vendedor padrão não foi configurado. Verifique.");
                }
                os.Vendedor = Vendedor;
                os.TabelaPreco = pedidos.FirstOrDefault().TabelaPreco;
                os.OSServicoItems.Add(new OSServicoItem()
                {
                    OSOrdemServico = os,
                    Tipo = TipoServico.Propria,
                    Preco = pedidos.Sum(s => s.ItemsServicos.Sum(t => t.Total)),
                    Quantidade = 1,
                    PrecoOriginal = pedidos.Sum(s => s.ItemsServicos.Sum(t => t.Total)),
                    Descricao = servico.Descricao.Length > 100 ? servico.Descricao.Substring(0, 100) : servico.Descricao,
                    Servico = servico,
                    Total = pedidos.Sum(s => s.ItemsServicos.Sum(t => t.Total)),
                    Responsavel = Vendedor,
                    Seq = 1,
                    PrecoTerceiro = 0,
                    DescontoPerc = 0
                });
                os.ServicoSubTotal = pedidos.Sum(s => s.ItemsServicos.Sum(t => t.Total));
                os.ServicoTotal = pedidos.Sum(s => s.ItemsServicos.Sum(t => t.Total));
                os.ServicoDescontoValor = 0;
                os.ServicoDescontoPerc = 0;
                os.Total = pedidos.Sum(s => s.TotalPedido);
                os.CodigoPedido = pedidos.First().Codigo;

                return os;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível criar a OS para este faturamento.\r\n" + e.Message, e);
            }
        }

        public Nota FaturarOrcamentoServico(IList<Pedido> pedidos, Servico servico, Condicao condicao, TipoNota tipoNota, IList<FinanceiroParcela> parcelas)
        {
            OSOrdemServico OS = CriaOSOrcamentoServico(pedidos, servico, condicao);
            IList<pxyFaturamentoOS> orcamentosServicoFaturar = GetFaturamento(OS);
            Negocio.Faturamento.OrdemServico faturamento = Negocio.Faturamento.OrdemServico
                .Get(orcamentosServicoFaturar);
            faturamento.Filial = pedidos.FirstOrDefault().Filial;
            faturamento.Cliente = pedidos.FirstOrDefault().Pessoa;
            faturamento.DataFaturamento = DateTime.Now.Date;
            faturamento.Condicao = condicao;
            faturamento.TipoMovimentacao2 = tipoNota;
            faturamento.Observacao1 = String.Empty;
            faturamento.Observacao2 = String.Empty;
            faturamento.Observacao3 = String.Empty;
            faturamento.ItemsSelecionados = orcamentosServicoFaturar;
            faturamento.Parcelas = parcelas;
            try
            {
                faturamento.FaturarOSOrcamentoServico(OS, pedidos);
                Nota nota = faturamento.notasGeradas.Where(w => w.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Servicos).FirstOrDefault();

                foreach (Pedido pedido in pedidos)
                {
                    if (pedido.Ent_Sai == 2)
                    {
                        pedido.Status = StatusOrcamento.Faturado;
                        PedidoController.Instancia.RegisterNewIntoTransaction(pedido);
                    }
                }
                PedidoController.Instancia.CommitUnitOfWorkTransaction();
                return nota;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void AtualizaStatusPedido(IList<Pedido> pedidos)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao preparar atualização dos status dos pedidos/orçamentos faturados.", e);
            }
        }

        private OSEquipamento RetornaEquipamento()
        {
            ParametrosContratoProduto parms = ParametrosContratoProdutoController.Instancia.GetAll().FirstOrDefault();
            if (parms == null)
            {
                throw new Exception("Os Parâmetros de Faturamento de OS não foram configurados. Contate o Suporte.");
            }
            if (parms.Equipamento != null)
            {
                return parms.Equipamento;
            }
            OSEquipamento equip = OSEquipamentoController.Instancia.GetAll().FirstOrDefault();
            if (equip == null)
            {

                equip = new OSEquipamento()
                {
                    OSTipoEquipamento = RetornaTipoEquipamento(),
                    Codigo = OSEquipamentoController.Instancia.MaxCodigo(),
                    Descricao = "Equipamento de Atendimento",
                    NumeroSerie = "1"
                };
                try
                {
                    OSEquipamentoController.Instancia.Salvar(equip, Acao.Incluir);
                    parms.Equipamento = equip;
                    ParametrosContratoProdutoController.Instancia.Salvar(parms, Acao.Alterar);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                parms.Equipamento = equip;
                ParametrosContratoProdutoController.Instancia.Salvar(parms, Acao.Alterar);
            }
            return equip;
        }

        private OSTipoEquipamento RetornaTipoEquipamento()
        {
            OSTipoEquipamento tipoEquip = OSTipoEquipamentoController.Instancia.GetAll().FirstOrDefault();
            if (tipoEquip == null)
            {
                tipoEquip = new OSTipoEquipamento()
                {
                    Descricao = "Equipamento de Atendimento",
                    Codigo = OSTipoEquipamentoController.Instancia.MaxCodigo()
                };
                try
                {
                    OSTipoEquipamentoController.Instancia.Salvar(tipoEquip, Acao.Incluir);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return tipoEquip;
        }

        internal TimeSpan GetTotHorasFaturadas(int idPessoa, DateTime dtInicio, DateTime dtFim)
        {
            return repositorioBanco.GetTotHorasFaturadas(idPessoa, dtInicio, dtFim);
        }



    }
}
