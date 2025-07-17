using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Aplicacao.Base;
using Aplicacao.Modulos.MercadoLivre;
using Aplicacao.Util;
using BLL;
using cwkControleUsuario;
using cwkGestao.Integracao.MercadoLivre.Util;
using CwkGestao.Integracao.PafEcf.Salc.Controle;
using cwkGestao.Integracao.SISeCommerce.Util;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Integracao;
using cwkGestao.Negocio.Utils;
using cwkGestao.Visual.Desktop;
using CworkGestaoPlus.ControleAcesso;
using Modelo.Properties;
using MO = Modelo;
using Monitor = cwkGestao.Integracao.SISeCommerce.Util.Monitor;
using Pedido = cwkGestao.Modelo.Pedido;
using Sistema = LicenceLibrary.Sistema;

namespace cwkGestao
{
    internal static class Program
    {
        private static string VERSAO;
        private static int menu;
        private static PedidoMagentoThread PedidoMagentoThread;
        private static ConsultaWSCorreios correios;
        private static bool rodarSHL = true;
        private static ThreadEnviaEmailCorreios emailCorreios;
        private static BConfiguraçãoMagento BConfgMagento = new BConfiguraçãoMagento();
        private static ThreadMercadoLivre threadML;
        private static Monitor mon;
        private static readonly Dictionary<string, Form> telasAbertas = new Dictionary<string, Form>();
        //private static readonly Mutex mutex = new Mutex(false, "352424E3-7FEC-455A-8EA7-4EB09575259");

        [STAThread]
        private static void Main(string[] args)
        {
            //if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false)) ==> Removido validação
            //{
            //    MessageBox.Show("O sistema já está em execução em outra janela.", "Cwork Sistemas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            try
            {
                TratarParametrosExecucao(args);
                SetarVersao();
                SetarCaminhoAplicacao();
                CriarDiretoriosNecessarios();
                ConfigurarDevExpress();
                ValidarControleUsuario();

                ExecutarAmbienteSelecionado();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("LogoEmpresa"))
                    MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FormErro.ShowDialog(ex);
            }
            finally
            {
                //if (BConfgMagento.GetConfiguracaoMagento())
                //    FinalizarPedidoMagentoThread();

                //mutex.ReleaseMutex();
                EncerrarThreadMercadoLivre();
                EncerraRotinaSISeCommerce();
                LiberarChaveDeAcesso();
                Application.ExitThread();
                Application.Exit();
            }
        }
        public static void FormataTextBoxDecimal(object sender, KeyPressEventArgs e)
        //Método para transformar o campo TetxBox como Decimal
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
            else if (e.KeyChar == 44)
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text.Contains(","))
                    e.Handled = true;
            }
        }
        private static void SetarVersao()
        {
            string versao = new Version(Application.ProductVersion).ToString();
            string[] splitVersao = versao.Split('.');
            VERSAO = string.Format("{0}.{1:00}.{2:000}", splitVersao[0], Convert.ToInt32(splitVersao[1]), Convert.ToInt32(splitVersao[2]));
        }

        private static void TratarParametrosExecucao(string[] args)
        {
            if (args.Length > 0)
                menu = Convert.ToInt32(args[0]);
        }

        private static void ConfigurarDevExpress()
        {
            AppearanceUtil.SetarLayoutECulturaNestaThread();
        }

        private static void CriarDiretoriosNecessarios()
        {
            DirectoryInfo dir = new DirectoryInfo(@"XML");
            try
            {
                if (!dir.Exists) dir.Create();

                //Cria o diretório onde salva as telas
                DirectoryInfo telas = new DirectoryInfo(@"Telas");
                if (!telas.Exists) telas.Create();
            }
            catch (IOException ex)
            {
                FormErro.ShowDialog(ex);
            }
        }

        private static void SetarCaminhoAplicacao()
        {
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            MO.cwkGlobal.DirApp = Path.Combine(Path.GetDirectoryName(arquivoExe), string.Empty);
            if (MO.cwkGlobal.DirApp[MO.cwkGlobal.DirApp.Length - 1] == '\\')
            {
                string aux = MO.cwkGlobal.DirApp;
                aux = MO.cwkGlobal.DirApp;
                MO.cwkGlobal.DirApp = string.Empty;
                MO.cwkGlobal.DirApp = aux.Substring(0, aux.Length - 1);
            }

            MO.cwkGlobal.ArquivoHelp = @"\Help\CworkGestao.chm";
        }

        private static void ValidarControleUsuario()
        {
            Facade.setBancoDados(1);
            if (!Facade.ValidaDAL())
            {
                Application.Exit();
            }
            else
            {
                Cw_Usuario bllUsuario = new Cw_Usuario();
                if (!bllUsuario.getPossuiUsuario())
                {
                    MO.cwkGlobal.objUsuarioLogado = new MO.Cw_Usuario();
                    MO.cwkGlobal.objUsuarioLogado.Tipo = 0;

                    Facade.ChamaGridUsuario();
                }
            }
        }

        private static void ExecutarAmbienteSelecionado()
        {
            // Executa o ambiente de Debug ou de produção...
            // Para abrir só ambiente de produção:
            //ExecutarAmbienteProducao();
            //return;


#if DEBUG
            ExecutarAmbienteDev();
#else
            ExecutarAmbienteProducao();
#endif
        }

        private static void ExecutarAmbienteDev()
        {
            //MO.cwkGlobal.CONTROLELICENCA = false;
            MO.cwkGlobal.objUsuarioLogado = new MO.Cw_Usuario
            {
                Id = 2,
                Codigo = 1,
                Login = "CWORK" // SISTEMAS - AMBIENTE DESENVOLVIMENTO"
            };

            //if ((Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("SincronismoMagento").Value) == 1) && BConfgMagento.GetConfiguracaoMagento())
            //    RotinaBuscaPedidoMagento();

            //VerificaTagsPafEcf();
            RotinaPafEcf();
            RotinaMercadoLivre();
            RotinaSISeCommerce();
            RodarThreadShl();
            InativarClientes();
            ExecutarSistema();
        }

        private static void InativarClientes()
        {
            Task.Factory.StartNew(() =>
            {
                ConfiguracaoController.Instancia.InativarCliente();
            });
        }

        private static void ExecutarAmbienteProducao()
        {
            if (PodeExecutarSistema())
                ExecutaMetodosSistema();

            //if (BConfgMagento.GetConfiguracaoMagento())
            //    RotinaBuscaPedidoMagento();                
            else
                Application.Exit();
        }

        private static void ExecutaMetodosSistema()
        {
            VerificaTagsPafEcf();
            RotinaPafEcf();
            RotinaMercadoLivre();
            VerificarImpressoraFiscal();
            RotinaSISeCommerce();
            RodarThreadShl();
            InativarClientes();
            ExecutarSistema();
        }

        private static void VerificarImpressoraFiscal()
        {
            //if ((Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("ImpressoraFiscal").Value) == 1))
            //{
            //    while (!CupomFiscal.ImpressoraLigada())
            //    {
            //        if (MessageBox.Show("Por favor ligue a impressora de cupom fiscal.", "Cupon Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
            //            continue;
            //    }
            //    if (CupomFiscal.VerificaReducaoZ())
            //    {
            //        using (var form = new CwkAviso("Por favor aguarde enquanto a redução Z é processada."))
            //        {
            //            ValidaPRTipo60 VPRT60 = new ValidaPRTipo60(form);
            //            Thread t = new Thread(new ThreadStart(VPRT60.ReducaoZ));
            //            t.Start();
            //            form.ShowDialog();
            //        }
            //    }
            //}
        }

        private static bool PodeExecutarSistema()
        {
            return Facade.ChamaAutenticacao(Sistema.Gestão, VERSAO, true);
        }

        private static void ExecutarSistema()
        {
            try
            {
                InicializarBancoDados();

                ////Rotina para controlar o acesso demo
                //if (!ControleAcesso.Validar())
                //{
                //    Application.Exit();
                //    return;
                //}
                ////Até aqui para Desabilitar

                Application.Run(new MenuInicial(menu, VERSAO));
            }
            catch (Exception w)
            {
                new FormErro(w).ShowDialog();
            }
        }

        private static void InicializarBancoDados()
        {
            #region Seta a string de conexão nas dlls Modelo e Relatorio

            Settings.Default.SetUserOverride("cwkGestaoConnectionString", MO.cwkGlobal.CONN_STRING);
            Settings.Default.Save();
            Relatorio.Properties.Settings.Default.SetUserOverride("cwkGestaoConnectionString", MO.cwkGlobal.CONN_STRING);
            Relatorio.Properties.Settings.Default.Save();

            #endregion

            //Cwork.Utilitarios.Componentes.Global.Diretorio = MO.cwkGlobal.DirApp;
            //Cwork.Utilitarios.Componentes.Global.Usuario = MO.cwkGlobal.objUsuarioLogado.Login;
            EmpresaController.Instancia.InicializaBase();
        }

        private static void LiberarChaveDeAcesso()
        {
            try
            {
                //TODO tratar quando ainda não fez login (referência de objeto)
                Facade.LiberarChave();
            }
            catch //(Exception ex)
            {
                //Aplicacao.Base.FormErro.ShowDialog(ex);
            }
        }

        public static string MontaMensagemErro(Exception exc)
        {
            if (exc != null)
                return exc.Message + "\n" + MontaMensagemErro(exc.InnerException);
            return string.Empty;
        }

        //public static void FinalizarPedidoMagentoThread()
        //{
        //    if (PedidoMagentoThread != null && cwkGestao.Negocio.ConfiguracaoController.Instancia.GetConfiguracao().TelaProduto == 2)
        //    {
        //        PedidoMagentoThread.Executar = false;
        //        correios.Executar = false;
        //        emailCorreios.Executar = false;
        //    }
        //}

        //public static void RotinaBuscaPedidoMagento()
        //{
        //    try
        //    {
        //        if ((Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("SincronismoCorreios").Value) == 1))
        //        {
        //            correios = new ConsultaWSCorreios();
        //            Action _Metodo = () => correios.ExecutarThread();
        //            Thread u = new Thread(new ThreadStart(_Metodo));
        //            u.Name = "ThreadCorreiosMagento";
        //            u.Start();
        //        }

        //        if ((Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("SincronismoEmailCorreios").Value) == 1))
        //        {
        //            emailCorreios = new ThreadEnviaEmailCorreios();
        //            Action _metodo = () => emailCorreios.ExecutarThread();
        //            Thread e = new Thread(new ThreadStart(_metodo));
        //            e.Name = "ThreadCorreiosEmailMagento";
        //            e.Start();
        //        }

        //        if ((Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("SincronismoMagento").Value) == 1))
        //        {
        //            PedidoMagentoThread = new PedidoMagentoThread();
        //            Action metodo = () => PedidoMagentoThread.ExecutarThread();
        //            Thread t = new Thread(new ThreadStart(metodo));
        //            t.Name = "ThreadMagento";
        //            t.Start();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public static Thread m;

        public static void RotinaMercadoLivre()
        {
            try
            {
                if (Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("SincronismoML").Value) == 1)
                {
                    MercadoLivreConfiguracao mConf = MercadoLivreConfiguracaoController.Instancia.GetAll().First();
                    if (mConf.RefreshToken == null)
                    {
                        var form = new FormCarregarToken();
                        form.ShowDialog();
                    }

                    threadML = new ThreadMercadoLivre();
                    Action met = () => threadML.ExecutarThread(mConf);
                    m = new Thread(new ThreadStart(met));
                    m.Name = "ThreadMercadoLivre";
                    m.Start();
                }
            }
            catch (Exception) { }
        }

        public static void RodarThreadShl()
        {
            Action met = () => RotinaSHLAtualizaPedido();
            m = new Thread(new ThreadStart(met));
            m.Name = "ThreadSHL";
            m.Start();
        }

        public static void RotinaSHLAtualizaPedido()
        {
            int tempo = 0;
            bool usaSHL = false;

            try
            {
                usaSHL = Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("SincronizaSHL").Value) == 1 ? true : false;
                tempo = Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("IntervalosincronismoSHL").Value) * 60000;
                if (usaSHL)
                    while (rodarSHL)
                        try
                        {
                            IList<Pedido> lPedidoShl = PedidoController.Instancia.GetPedidosSHLNaoFinalizados();
                            SHLUtil.AtualizaStatusPedido(lPedidoShl);
                        }
                        catch (Exception) { }
                        finally
                        {
                            Thread.Sleep(tempo);
                        }
            }
            catch (Exception) { }
        }

        public static void RotinaPafEcf()
        {
            try
            {
                XElement TagXml = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("EquipamentoECF");
                if (TagXml != null)
                {
                    if (Convert.ToInt32(TagXml.Value) > 0)
                    {
                        MonitorPasta monitor = new MonitorPasta();
                        monitor.MonitorarPasta(ConfiguracaoController.Instancia.GetConfiguracao().DiretorioSalc + @"\RESP", "*.*", FaturarPedidoImportado.FaturarPedidoPaf);
                        monitor.Start();
                    }
                }
                
            }
            catch (Exception ex) { }
        }

        public static void RotinaSISeCommerce()
        {
            try
            {
                XElement TagXmlSis = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("UtilizaSisEcommerce");
                if (TagXmlSis == null)
                {
                    XDocument xml = XDocument.Load("ConfiguracaoCwork.xml");
                    xml.Element("Cwork").Add(new XElement("UtilizaSisEcommerce", "0"));
                    xml.Save("ConfiguracaoCwork.xml");
                }

                TagXmlSis = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("UtilizaSisEcommerce");
                if (Convert.ToInt32(TagXmlSis.Value) == 1)
                {
                    SisECommerceConfiguracao SisConf = SisECommerceConfiguracaoController.Instancia.GetAll().FirstOrDefault();
                    if (SisConf == null)
                    {
                        MessageBox.Show("O sistema está configurado para realizar a integração com o sistema SIS e-Commerce," +
                                        " Porém a configuração deste serviço ainda não foi realizada." + "\r\n" +
                                        " Realize a configuração no menu \"Utilitários > Sis e-Commerce > Configuração\" ou contate o suporte." +
                                        "\r\n" + "Após a configuração, reinicialize o Cwork Gestão",
                            "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    string erro = "";
                    PropertyInfo[] props = SisConf.GetType().GetProperties();
                    foreach (PropertyInfo prop in props)
                        if (prop.GetType() == typeof(string))
                        {
                            if (prop.Name.Contains("Dir")) continue;
                            if (string.IsNullOrEmpty(prop.GetValue(SisConf, null).ToString())) erro += "O campo " + prop.Name + " não foi preenchido corretamente para o sincronismo SIS e-Commerce" + "\r\n";
                        }
                        else
                        {
                            if (prop.Name.Contains("Dir")) continue;
                            if (prop.GetValue(SisConf, null) == null) erro += "O campo " + prop.Name + " não foi preenchido corretamente para o sincronismo SIS e-Commerce" + "\r\n";
                        }

                    if (!string.IsNullOrEmpty(erro))
                    {
                        erro += "\r\n" + "O sincronismo não será realizado";
                        MessageBox.Show(erro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        string caminhoPre = Path.Combine(caminho, "SISeCommerce");
                        string caminhoPos = Path.Combine(caminhoPre, "SISeCommercePos");

                        if (!Directory.Exists(SisConf.DirPosSincronia))
                        {
                            Directory.CreateDirectory(caminhoPos);
                            SisConf.DirPosSincronia = caminhoPos;
                            SisECommerceConfiguracaoController.Instancia.Salvar(SisConf, Acao.Alterar);
                        }

                        if (!Directory.Exists(SisConf.DirSincronia))
                        {
                            Directory.CreateDirectory(caminhoPre);
                            SisConf.DirSincronia = caminhoPre;
                            SisECommerceConfiguracaoController.Instancia.Salvar(SisConf, Acao.Alterar);
                        }

                        ImportaPedidosSISeCommerceOffline();

                        mon = new Monitor();
                        mon.MonitorarPasta(SisConf.DirSincronia, "*.*", ConversorSisGestao.ImportarPedidoSIS);
                        mon.Start();
                    }
                }
            }
            catch (Exception a)
            {
                SISeCommerceException s = new SISeCommerceException(a);
                new FormErro(s).Show();
            }
        }

        private static void ImportaPedidosSISeCommerceOffline()
        {
            SisECommerceConfiguracao conf = SisECommerceConfiguracaoController.Instancia.GetAll().FirstOrDefault();
            IList<SincronismoPendenteSisECommerce> sincronismos = SincronismoPendenteSisECommerceController.Instancia.GetAll();
            IList<string> caminhoArquivosPendentes = Directory.GetFiles(conf.DirSincronia).ToList();

            foreach (SincronismoPendenteSisECommerce item in sincronismos)
                if (caminhoArquivosPendentes.Contains(item.CaminhoArquivo))
                    caminhoArquivosPendentes.Remove(item.CaminhoArquivo);

            foreach (string item in caminhoArquivosPendentes)
            {
                try
                {
                    int nomeArquivo = Convert.ToInt32(Path.GetFileNameWithoutExtension(item));
                    if (PedidoController.Instancia.GetAllPorIdSISeCommerce(nomeArquivo).Count > 0)
                    {
                        string arqAnt = conf.DirSincronia + "\\" + Path.GetFileName(item);
                        string arqPos = conf.DirPosSincronia + "\\" + Path.GetFileName(item);
                        File.Move(arqAnt, arqPos);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                FileSystemEventArgs fsea = new FileSystemEventArgs(
                    WatcherChangeTypes.Changed,
                    Path.GetDirectoryName(item),
                    Path.GetFileName(item));
                ConversorSisGestao.ImportarPedidoSIS(null, fsea);
            }
        }

        public static void EncerraRotinaSISeCommerce()
        {
            try
            {
                if (mon != null) mon.Stop();
            }
            catch (Exception q)
            {
                SISeCommerceException s = new SISeCommerceException(q);
                new FormErro(s).Show();
            }
        }

        public static void EncerrarThreadSHL()
        {
            rodarSHL = false;
        }

        public static void VerificaTagsPafEcf()
        {
            XElement TagXmPafEcf = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("EquipamentoECF");
            if (TagXmPafEcf == null)
            {
                XDocument xml = XDocument.Load("ConfiguracaoCwork.xml");
                xml.Element("Cwork").Add(new XElement("EquipamentoECF", "1"));
                xml.Save("ConfiguracaoCwork.xml");
            }

            TagXmPafEcf = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("BalcaoPafEcf");
            if (TagXmPafEcf == null)
            {
                XDocument xml = XDocument.Load("ConfiguracaoCwork.xml");
                xml.Element("Cwork").Add(new XElement("BalcaoPafEcf", "0"));
                xml.Save("ConfiguracaoCwork.xml");
            }
        }

        public static void EncerrarThreadMercadoLivre()
        {
            if (threadML != null)
            {
                threadML.rodar = false;
                m.Abort();
            }
        }
    }
}