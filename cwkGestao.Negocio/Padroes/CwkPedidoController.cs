using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.IO;
using System.Data.SqlClient;

namespace cwkGestao.Negocio
{
    public partial class CwkPedidoController : BaseController<CwkPedido>
    {
        #region Singleton
        private static CwkPedidoController instance;
        private static IRepositorioCwkPedido repositorioCwkPedido;

        private CwkPedidoController()
        { }

        static CwkPedidoController()
        {
            instance = new CwkPedidoController();
            repositorioT = RepositorioFactory<CwkPedido>.GetRepositorio();
            repositorioCwkPedido = (IRepositorioCwkPedido)repositorioT;
        }

        public static CwkPedidoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CwkPedido objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objeto.Status == OrcamentoStatus.Cancelado && objeto.MotivoCancelamento != null && objeto.MotivoCancelamento.Trim().Length < 14)
                ret.Add("txtMotivoCancelamento", "Preencha o motivo do cancelamento.");

            if (objeto.Data == null || objeto.Data == DateTime.MinValue)
            {
                ret.Add("txtData", "Preencha a data do pedido.");
            }

            if (objeto.Hora == null || objeto.Hora == DateTime.MinValue)
            {
                ret.Add("txtHora", "Preencha a hora do pedido.");
            }

            if (objeto.Pessoa == null)
            {
                ret.Add("cbIdPessoa", "Selecione o cliente.");
            }

            if (objeto.Sistema == null)
            {
                ret.Add("cbIdSistema", "Selecione o sistema.");
            }

            if (objeto.Versao == null)
            {
                ret.Add("cbIdVersao", "Selecione a versão do sistema.");
            }

            if (String.IsNullOrEmpty(objeto.Solicitante) || objeto.Solicitante.Trim() == String.Empty)
            {
                ret.Add("txtSolicitante", "Preencha o campo solicitante.");
            }

            if (objeto.CwkPedidoEmpresas.Count == 0)
            {
                ret.Add("gcPedidoempresa", "Insira pelo menos um produto/serviço.");
            }

            return ret;
        }

        public override CwkPedido LoadObjectById(int id)
        {
            return base.LoadObjectById(id);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyCwkPedido> GetPedidos()
        {
            return repositorioCwkPedido.GetPedidos();
        }
        public IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.Pessoa pessoa)
        {
            return repositorioCwkPedido.GetPedidos(pessoa);
        }

        public IList<cwkGestao.Modelo.CwkPedido> GetPedidos(CwkPedido exemplo)
        {
            return repositorioCwkPedido.GetPedidos(exemplo);
        }

        public IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.Pessoa pessoa, DateTime inicio, DateTime fim)
        {
            return repositorioCwkPedido.GetPedidos(pessoa, inicio, fim);
        }

        public IList<cwkGestao.Modelo.CwkPedido> GetCwkPedidos()
        {
            return repositorioCwkPedido.GetCwkPedidos();
        }

        public Dictionary<string, string> GeraPontoMT(cwkGestao.Modelo.CwkPedido pedido, FileInfo fi, out string nomeBackupBanco)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            String caminhoNovo = "";
            nomeBackupBanco = "";
            List<cwkPontoMT_MD5Empresa.Modelo.Empresa> empresas = new List<cwkPontoMT_MD5Empresa.Modelo.Empresa>();

            Dictionary<int, int> IDEmpresa = new Dictionary<int, int>();
            IDEmpresa.Add(242, 1);
            IDEmpresa.Add(245, 2);
            IDEmpresa.Add(154, 3);
            IDEmpresa.Add(160, 4);
            IDEmpresa.Add(236, 5);
            IDEmpresa.Add(296, 6);
            IDEmpresa.Add(283, 7);
            IDEmpresa.Add(400, 8);
            IDEmpresa.Add(423, 9);
            IDEmpresa.Add(435, 10);
            IDEmpresa.Add(437, 11);
            IDEmpresa.Add(438, 12);
            IDEmpresa.Add(439, 13);
            IDEmpresa.Add(440, 14);
            IDEmpresa.Add(442, 15);
            IDEmpresa.Add(445, 16);
            IDEmpresa.Add(454, 17);
            IDEmpresa.Add(458, 18);
            IDEmpresa.Add(540, 3);

            if (pedido.Versao.Tipobanco == 0)
            {
                if (String.IsNullOrEmpty(pedido.Versao.Caminhobancolimpo))
                {
                    ret.Add("txtPedido", "O pedido está associado a uma versão de sistema que não possui o caminho do banco cadastrado.");
                    return ret;
                }
                else
                {
                    caminhoNovo = fi.FullName + @"\CWKPONTOMT_" + pedido.CwkPedidoEmpresas.First().ClienteRevenda.Nome + "_" + pedido.NumeroSerie + ".FDB";
                    File.Copy(pedido.Versao.Caminhobancolimpo, caminhoNovo);
                }
            }
            else if (pedido.Versao.Tipobanco == 1)
                if (pedido.Sistema.ID != 12)
                {
                    caminhoNovo = fi.FullName;    
                }
                else
                {
                    caminhoNovo = pedido.Versao.CaminhoBackup;
                }
                
            cwkPontoMT_MD5Empresa.Modelo.Empresa objEmpresa;
            int codigo = 1;

            int idRevenda = IDEmpresa[pedido.IDPessoa];
            foreach (CwkPedidoEmpresa pe in pedido.CwkPedidoEmpresas)
            {
                ClienteRevenda cv = pe.ClienteRevenda;
                String numeroSerie = String.Format("{0:000}", pedido.Sistema.Codigo) + "-" + String.Format("{0:###000}", pe.NumeroSerie);
                if (idRevenda == 540)
                {
                    idRevenda = 154;
                }
                objEmpresa = new cwkPontoMT_MD5Empresa.Modelo.Empresa()
                {
                    Id = cv.IdFirebird.HasValue ? cv.IdFirebird.Value : 0,
                    BDAlterado = false,
                    bPrincipal = cv.Principal.HasValue ? cv.Principal.Value : false,
                    Codigo = codigo++,
                    CEI = cv.Cei,
                    Cep = cv.Cep,
                    Cidade = cv.Cidade.Nome,
                    Cnpj = cv.Cnpj,
                    Cpf = cv.Cpf,
                    Endereco = cv.Endereco,
                    Estado = cv.Cidade.UF.Sigla,
                    Nome = cv.Nome,
                    Numeroserie = numeroSerie,
                    Quantidade = cv.Quantidade.HasValue ? cv.Quantidade.Value : 0,
                    TipoLicenca = Convert.ToInt32(cv.TipoLicenca),
                    Bloqueiousuarios = pedido.BloqueioUsuarios,
                    Relatorioabsenteismo = pedido.Relatorioabsenteismo,
                    ExportacaoHorasAbonadas = pedido.ExportacaoHorasAbonadas,
                    ModuloRefeitorio = pedido.ModuloRefeitorio,
                    IDRevenda = idRevenda
                };

                empresas.Add(objEmpresa);              
            }

            IncluirEmpresas(pedido.Versao, caminhoNovo, empresas, pedido.IDPessoa, out nomeBackupBanco);

            return ret;
        }

        private static void IncluirEmpresas(Modelo.Versao objVersao, string caminhoNovo, List<cwkPontoMT_MD5Empresa.Modelo.Empresa> empresasGravar, int idRevenda, out string nomeBackupBanco)
        {
            cwkPontoMT_MD5Empresa.Fachada facade = cwkPontoMT_MD5Empresa.Fachada.getInscancia();
            nomeBackupBanco = "";
            if (objVersao.Tipobanco == 0)
            {
                //Firebird
                facade.SetConexaoBancoDados(2);
                facade.SetUsuario(global::Modelo.cwkGlobal.objUsuarioLogado.Login);
                facade.SetConexao("User=SYSDBA;Password=masterkey;Database=" + caminhoNovo + ";DataSource=192.168.254.112;Dialect=3;Charset=NONE;Connection Timeout=180;Connection Lifetime=0;Pooling=false;Packet Size=8192;ServerType=0;");
                foreach (cwkPontoMT_MD5Empresa.Modelo.Empresa emp in empresasGravar)
                {
                    facade.SalvaEmpresa(emp, objVersao.Numero);
                }
            }
            else if (objVersao.Tipobanco == 1)
            {
                //MSSQL Server
                string senha;

                facade.SetConexaoBancoDados(1);
                facade.SetUsuario(global::Modelo.cwkGlobal.objUsuarioLogado.Login);
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.InitialCatalog = objVersao.Banco;
                builder.DataSource = objVersao.Servidor;
                builder.UserID = objVersao.Usuario;
                builder.Password = objVersao.Senha;
                string connString = builder.ConnectionString;
                facade.SetConexao(connString);

                facade.LimparBase();
                cwkPontoMT_MD5Empresa.Modelo.Empresa objEmpresaPrincipal = new cwkPontoMT_MD5Empresa.Modelo.Empresa();
                foreach (cwkPontoMT_MD5Empresa.Modelo.Empresa emp in empresasGravar)
                {
                    facade.SalvaEmpresa(emp, objVersao.Numero);
                    if (emp.bPrincipal)
                        objEmpresaPrincipal = emp;
                }

                AdicionaUsuarioRevenda(idRevenda, facade, out senha);
                string nomeTratado = "";
                if (objVersao.Sistema.ID == 12)
                {
                   nomeTratado = objEmpresaPrincipal.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                   if (String.IsNullOrEmpty(nomeTratado))
                   {
                       nomeTratado = objEmpresaPrincipal.Cpf.Replace(".", "").Replace("/", "").Replace("-", "");
                   }
                   
                }
                else
                {
                   nomeTratado = objEmpresaPrincipal.Nome.Replace("\\", "").Replace("/", "").Replace(":", "")
                                                                                 .Replace("*", "").Replace("\'", "").Replace("\"", "")
                                                                                 .Replace("?", "").Replace("|", "").Replace("<", "").Replace(">", "");
                }
                nomeBackupBanco = nomeTratado;
                facade.GerarBackupBanco(caminhoNovo, nomeTratado);
                facade.SetaSenhaPadraoUsuarioRevenda("revenda");
            }
        }

        private static void AdicionaUsuarioRevenda(int idRevenda, cwkPontoMT_MD5Empresa.Fachada facede, out string senha)
        {
            senha = String.Empty;
            Pessoa revenda = PessoaController.Instancia.LoadObjectById(idRevenda);
            if ((revenda != null) && (revenda.ID > 0))
            {
                senha = cwkControleUsuario.Facade.Cifrar(global::Modelo.Criptografia.Decriptografar(revenda.SenhaRevenda));
                if (!String.IsNullOrEmpty(revenda.SenhaRevenda))
                    facede.AlteraUsuarioExistente("revenda", senha);
            }
        }

        private static bool EnviarEmail(cwkGestao.Modelo.Pedido objPedido, cwkGestao.Modelo.Sistema objSistema, string pNumeroSerie)
        {
            //try
            //{
            //    cwkOrdemServicoBLL.Configuracao bllConfiguracao = new Configuracao();
            //    Modelo.Configuracao objConfiguracao = bllConfiguracao.LoadPrimeiro();
            //    SmtpClient cliente = new SmtpClient(objConfiguracao.SMTP);
            //    cliente.Credentials = new NetworkCredential(objConfiguracao.Usuario, objConfiguracao.Senha);
            //    MailMessage mensagem = new MailMessage(new MailAddress(objConfiguracao.Usuario), new MailAddress(objConfiguracao.EmailAdministrativo));
            //    mensagem.Subject = "Nova Licença";
            //    mensagem.Body = MontaMensagem(objPedido, objSistema, pNumeroSerie);
            //    cliente.Send(mensagem);
            //    return true;
            //}
            //catch (Exception)
            //{
            //    //TODO: Tratar Exceção quando não conseguir enviar email
            //    return false;
            //}
            throw new NotImplementedException();
        }

        private static string MontaMensagem(Modelo.Pedido objPedido, Modelo.Sistema objSistema, string pNumeroSerie)
        {
            //Modelo.Versao objVersao;
            //var aux = objSistema.Versaos.Where(v => v.Id == objPedido.Idversao);
            //objVersao = aux.First();
            //Modelo.Pessoa objRevenda = bllPessoa.LoadObject(objPedido.Idpessoa);

            //StringBuilder str = new StringBuilder();
            //str.AppendLine("Nova Licença " + objSistema.Nome);
            //str.AppendLine("Versão: " + objVersao.Numero);
            //if (objVersao.Tipobanco == 0)
            //    str.AppendLine("Banco de Dados: Firebird");
            //else if (objVersao.Tipobanco == 1)
            //    str.AppendLine("Banco de Dados: SQL Server");
            //str.AppendLine();
            //str.AppendLine("Data: " + objPedido.Data.Value.ToShortDateString() + " Hora: " + objPedido.Hora.Value.ToShortTimeString());
            //str.AppendLine("Número de série: " + pNumeroSerie);
            //str.AppendLine();
            //str.AppendLine("Revenda: " + objRevenda.Nome);
            //str.AppendLine("Solicitante: " + objPedido.Solicitante);
            //str.AppendLine();
            //str.AppendLine("Liberado por: " + objPedido.Liberado);
            //str.AppendLine("Observação: " + objPedido.Observacao);
            //str.AppendLine();
            //str.AppendLine("---");
            //str.AppendLine();

            //foreach (Modelo.Pedidoempresa emp in objPedido.Empresas)
            //{
            //    Modelo.ClienteRevenda objClienteRevenda = bllClienteRevenda.LoadObject(emp.Idclienterevenda);
            //    Modelo.Servico objServico = bllServico.LoadObject(emp.IdServico);

            //    str.AppendLine("Empresa: " + objClienteRevenda.Nome + " / " + objClienteRevenda.Cnpj + (objClienteRevenda.BPrincipal == 1 ? " / Empresa Principal" : ""));
            //    str.AppendLine("Serviço: " + objServico.Nome);
            //    str.AppendLine();
            //}

            //return str.ToString();
            throw new NotImplementedException();
        }

        public bool TermoResponsabilidade(int idCwkpedido)
        {
            cwkGestao.Modelo.CwkPedido pedido = LoadObjectById(idCwkpedido);
            if (pedido.Sistema.Nome.Equals("CWORK PONTO MT", StringComparison.CurrentCultureIgnoreCase) || pedido.Sistema.Nome.Equals("CWORK PONTO PLUS", StringComparison.CurrentCultureIgnoreCase))
            {
                String dirApp = new System.IO.FileInfo(".").FullName;
                String nomeEmpresa = "";
                String cnpj = "";
                String numeroSerie = "001-" + pedido.NumeroSerie.ToString();
                String dataEmissao = DateTime.Now.ToShortDateString();


                var auxPrincipal = pedido.CwkPedidoEmpresas.Where(p => p.ClienteRevenda.Principal == true);

                if (auxPrincipal != null && auxPrincipal.Count() > 0)
                {
                    cwkGestao.Modelo.CwkPedidoEmpresa principal = auxPrincipal.First();
                    nomeEmpresa = principal.ClienteRevenda.Nome;
                    cnpj = !String.IsNullOrEmpty(principal.ClienteRevenda.Cnpj) ? principal.ClienteRevenda.Cnpj : principal.ClienteRevenda.Cpf;
                }

                foreach (cwkGestao.Modelo.CwkPedidoEmpresa pe in pedido.CwkPedidoEmpresas)
                {
                    cwkGestao.Modelo.ClienteRevenda cv = pe.ClienteRevenda;
                    if (cv.Principal.Value)
                    {
                        CwkPedidoController.PreencherPorReplace(dirApp + "\\Termo_Ponto.doc", nomeEmpresa, cnpj, numeroSerie, dataEmissao);
                        return true;
                    }
                }
            }
            return false;
        }

        private static void PreencherPorReplace(string CaminhoDocMatriz, string nomeEmpresa, string cnpj, string numeroSerie, string dataEmissao)
        {
            //Objeto a ser usado nos parâmetros opcionais
            object missing = System.Reflection.Missing.Value;

            //Abre a aplicação Word e faz uma cópia do documento mapeado
            var oApp = new Microsoft.Office.Interop.Word.Application();
            object template = CaminhoDocMatriz;
            Microsoft.Office.Interop.Word.Document oDoc = oApp.Documents.Add(ref template, ref missing, ref missing, ref missing);

            //Troca o conteúdo de alguns tags
            Microsoft.Office.Interop.Word.Range oRng = oDoc.Range(ref missing, ref missing);
            object FindText = "[nomeempresa]";
            object ReplaceWith = nomeEmpresa;
            object MatchWholeWord = true;
            object Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward,
            ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "[cnpjempresa]";
            ReplaceWith = cnpj;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward,
            ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "[numeroseriepedido]";
            ReplaceWith = numeroSerie;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward,
            ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "[dataemissao]";
            ReplaceWith = dataEmissao;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward,
            ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oApp.Visible = true;
        }

        public void ReordenaPedidosEmpresa(IList<CwkPedidoEmpresa> pEmpresas)
        {
            int count = 1;
            foreach (Modelo.CwkPedidoEmpresa cp in pEmpresas)
            {
                cp.Seq = count++;
            }
        }

        public int GeraNumeroSerie(int pIdSistema)
        {
            int num = repositorioCwkPedido.UltimoNumeroSerie(pIdSistema);
            return num + 1;
        }

        public IList<object> GetPedidosFaturamento(List<int> idsRevendas, int tipoFiltro, DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioCwkPedido.GetPedidosFaturamento(idsRevendas, tipoFiltro, dataInicial, dataFinal);
        }

        public void GerarLicenca(int idCwkPedido,int quantidade)
        {
            CwkPedidoEmpresaController cwkPedidoEmpresaController = CwkPedidoEmpresaController.Instancia;
            CwkPedido cwkPedido = LoadObjectById(idCwkPedido);
            if (cwkPedido.CwkPedidoEmpresas == null)
            {
                throw new Exception("Verifique se foi informado cliente revenda para a licença selecionada!");
            }
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    CwkPedido novoCwkPedido = new CwkPedido();
                    novoCwkPedido.Codigo = MaxCodigo();
                    novoCwkPedido.BDemonstracao = cwkPedido.BDemonstracao;
                    novoCwkPedido.BloqueioUsuarios = cwkPedido.BloqueioUsuarios;
                    novoCwkPedido.Data = cwkPedido.Data;
                    novoCwkPedido.ExportacaoHorasAbonadas = cwkPedido.ExportacaoHorasAbonadas;
                    novoCwkPedido.Hora = cwkPedido.Hora;
                    novoCwkPedido.IDPessoa = cwkPedido.IDPessoa;
                    novoCwkPedido.Idrevenda = cwkPedido.Idrevenda;
                    novoCwkPedido.IDSistema = cwkPedido.IDSistema;
                    novoCwkPedido.IDVersao = cwkPedido.IDVersao;
                    novoCwkPedido.Liberado = cwkPedido.Liberado;
                    novoCwkPedido.ModuloRefeitorio = cwkPedido.ModuloRefeitorio;
                    novoCwkPedido.LicencaReservada = cwkPedido.LicencaReservada;
                    novoCwkPedido.Observacao = cwkPedido.Observacao;
                    novoCwkPedido.Pessoa = cwkPedido.Pessoa;
                    novoCwkPedido.Relatorioabsenteismo = cwkPedido.Relatorioabsenteismo;
                    novoCwkPedido.Sistema = cwkPedido.Sistema;
                    novoCwkPedido.Solicitante = cwkPedido.Solicitante;
                    novoCwkPedido.Status = OrcamentoStatus.Aberto;
                    novoCwkPedido.Versao = cwkPedido.Versao;
                    IList<CwkPedidoEmpresa> lCwkPedidoEmpresas = new List<CwkPedidoEmpresa>();
                    novoCwkPedido.CwkPedidoEmpresas = lCwkPedidoEmpresas;
                    foreach (CwkPedidoEmpresa ccwkPedidoEmpresas in cwkPedido.CwkPedidoEmpresas)
                    {
                        CwkPedidoEmpresa novoCwkPedidoEmpresas = new CwkPedidoEmpresa();
                        novoCwkPedidoEmpresas.ClienteRevenda = ccwkPedidoEmpresas.ClienteRevenda;
                        novoCwkPedidoEmpresas.IDClienteRevenda = ccwkPedidoEmpresas.IDClienteRevenda;
                        novoCwkPedidoEmpresas.IDProduto = ccwkPedidoEmpresas.IDProduto;
                        novoCwkPedidoEmpresas.IDServico = ccwkPedidoEmpresas.IDServico;
                        novoCwkPedidoEmpresas.InfMaquina = ccwkPedidoEmpresas.InfMaquina;
                        novoCwkPedidoEmpresas.Observacao = ccwkPedidoEmpresas.Observacao;
                        novoCwkPedidoEmpresas.Produto = ccwkPedidoEmpresas.Produto;
                        novoCwkPedidoEmpresas.Quantidade = ccwkPedidoEmpresas.Quantidade;
                        novoCwkPedidoEmpresas.Servico = ccwkPedidoEmpresas.Servico;
                        novoCwkPedidoEmpresas.TipoLicenca = ccwkPedidoEmpresas.TipoLicenca;
                        novoCwkPedidoEmpresas.TipoServico = ccwkPedidoEmpresas.TipoServico;
                        novoCwkPedidoEmpresas.ValorProduto = ccwkPedidoEmpresas.ValorProduto;
                        novoCwkPedidoEmpresas.Seq = ccwkPedidoEmpresas.Seq;
                        cwkPedidoEmpresaController.GerarNumeroSerie(novoCwkPedido, novoCwkPedidoEmpresas);
                        novoCwkPedido.CwkPedidoEmpresas.Add(novoCwkPedidoEmpresas);
                    }
                    Dictionary<string, string> ret = new Dictionary<string, string>();
                    ret = Salvar(novoCwkPedido, Acao.Incluir);
                    if (ret.Count() > 0)
                    {
                        string erro = String.Join(",",ret.Select(x => x.Key+" - "+x.Value).ToArray());
                        System.Windows.Forms.MessageBox.Show("Erro ao gerar licenças! Erro: "+erro);
                    }
                    else
                    {
                        CommitUnitOfWorkTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                RollBackUnitOfWorkTransaction();
                throw ex;
            }
        }
    }
}
