using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace cwkGestao.Importacao.ArquivosNFe
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> notasAtualizarXmlDest;
        Dictionary<string, string> notasAtualizarLogEnv;
        Dictionary<string, string> notasAtualizarLogRec;

        public Form1()
        {
            InitializeComponent();
        }

        private void sbImportar_Click(object sender, EventArgs e)
        {
            if (ArquivoDALValido() && DiretoriosInformadosValidos())
                ImportarArquivos();
        }

        private void ImportarArquivos()
        {
            cwkGestao.Negocio.EmpresaController.Instancia.InicializaBase();
            PreencheXmlDestinatario();
            PreencheLogEnvio();
            PreencheLogRecibo();
            Salvar();
        }

        private void Salvar()
        {
            using (var session = SessionProvider.SessionFactory.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in notasAtualizarLogEnv)
                            session.CreateQuery("update Nota n set n.XmlLogEnvNFe = :log where n.ChaveNota = :chave")
                                   .SetParameter("log", item.Value)
                                   .SetString("chave", item.Key)
                                   .ExecuteUpdate();
                        foreach (var item in notasAtualizarLogRec)
                            session.CreateQuery("update Nota n set n.XmlLogRecNFe = :log where n.ChaveNota = :chave")
                                   .SetParameter("log", item.Value)
                                   .SetString("chave", item.Key)
                                   .ExecuteUpdate();

                        foreach (var item in notasAtualizarXmlDest)
                            session.CreateQuery("update Nota n set n.XmlDestinatarioNFe = :xmlDest where n.ChaveNota = :chave")
                                   .SetParameter("xmlDest", item.Value)
                                   .SetString("chave", item.Key)
                                   .ExecuteUpdate();

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void PreencheLogRecibo()
        {
            notasAtualizarLogRec = new Dictionary<string, string>();
            foreach (var caminho in Directory.GetFiles(txtDiretorioLog.Text, "*-pro-rec.xml"))
            {
                var conteudo = AbrirArquivo(caminho);
                var chave = conteudo.Substring(conteudo.IndexOf("<chNFe>") + 7);
                chave = chave.Remove(chave.IndexOf("</chNFe>"));
                if (notasAtualizarLogRec.ContainsKey(chave))
                    notasAtualizarLogRec[chave] = conteudo;
                else
                    notasAtualizarLogRec.Add(chave, conteudo);
            }
        }

        private void PreencheLogEnvio()
        {
            notasAtualizarLogEnv = new Dictionary<string, string>();
            foreach (var caminho in Directory.GetFiles(txtDiretorioLog.Text, "*-env-lot.xml"))
            {
                var conteudo = AbrirArquivo(caminho);
                var chave = conteudo.Substring(conteudo.IndexOf("<infNFe Id=\"NFe") + 15);
                chave = chave.Remove(chave.IndexOf("\""));
                if (notasAtualizarLogEnv.ContainsKey(chave))
                    notasAtualizarLogEnv[chave] = conteudo;
                else
                    notasAtualizarLogEnv.Add(chave, conteudo);
            }
        }

        private void PreencheXmlDestinatario()
        {
            notasAtualizarXmlDest = new Dictionary<string, string>();
            foreach (var caminho in Directory.GetFiles(txtDiretorioXmlDestinatario.Text, "*-nfe.xml"))
            {
                var chave = caminho.Split(new char[] { '\\' }).LastOrDefault();
                chave = chave.Remove(chave.IndexOf("-"));
                notasAtualizarXmlDest.Add(chave, AbrirArquivo(caminho));
            }
        }

        public static string AbrirArquivo(string arquivo)
        {
            StringBuilder arq_ret = new StringBuilder();
            StreamReader objReader = new StreamReader(arquivo);
            string sLine = "";

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arq_ret.Append(sLine);
            }
            objReader.Close();

            return arq_ret.ToString();
        }

        private bool DiretoriosInformadosValidos()
        {
            if (!Directory.Exists(txtDiretorioLog.Text))
            {
                MessageBox.Show("Informe um diretório de Logs existente");
                return false;
            }

            if (!Directory.Exists(txtDiretorioXmlDestinatario.Text))
            {
                MessageBox.Show("Informe um diretório de XmlDestinatario existente");
                return false;
            }

            return true;
        }

        private bool ArquivoDALValido()
        {
            cwkControleUsuario.Facade.setBancoDados(1);
            if (!cwkControleUsuario.Facade.ValidaDAL())
            {
                MessageBox.Show("Verifique o arquivo DAL.xml");
                return false;
            }
            return true;
        }
    }
}
