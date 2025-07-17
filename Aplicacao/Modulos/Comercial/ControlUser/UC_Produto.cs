using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;

namespace Aplicacao.Modulos.Comercial.ControlUser
{
    public partial class UC_Produto : UserControl
    {
        public Produto p = null;
        private BackgroundWorker imageLoader;

        private void HandleImageLoaderOnRunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                pictureEdit.Visible = true;
                return;
            }

            if (typeof(string) == e.Result.GetType())
                pictureEdit.ImageLocation = (string)e.Result;
            else 
                pictureEdit.Image = (Image)e.Result;

            pictureEdit.Visible = true;
        }

        private void HandleOnImageLoaderDoWork(Object sender, DoWorkEventArgs e)
        {
            if (p.IntegrarEcommerce == 1)
            {
                try
                {
                    var config = ConfiguracaoController.Instancia.GetConfiguracao();
                    var urlImagem = $"{config.CaminhoImagens}{p.Barra}.png";
                    e.Result = urlImagem;
                }
                catch { }
            }
            else
            {
                if (!string.IsNullOrEmpty(p.CaminhoImagem))
                {
                    try
                    {
                        var listaCaminhoImagens = new List<Tuple<string, bool>>();
                        var tempLista = p.CaminhoImagem.IsNullOrEmpty() ? new List<string>() : p.CaminhoImagem?.Split('|').ToList();

                        foreach (var item in tempLista)
                        {
                            var caminhoImagem = item.Split(';');
                            var imagemPrincipal = false;
                            if (caminhoImagem.Length > 1) bool.TryParse(caminhoImagem[1], out imagemPrincipal);
                            listaCaminhoImagens.Add(new Tuple<string, bool>(caminhoImagem[0], imagemPrincipal));
                        }

                        var principal = listaCaminhoImagens.FirstOrDefault(x => x.Item2);
                        var caminho = principal != null ? principal.Item1 : listaCaminhoImagens[0].Item1;

                        e.Result = Image.FromFile(caminho);
                    }
                    catch (Exception) { }
                }
            }
        }

        public UC_Produto(Produto Prod)
        {
            InitializeComponent();
            p = Prod;

            this.imageLoader = new BackgroundWorker();
            this.imageLoader.DoWork += HandleOnImageLoaderDoWork;
            this.imageLoader.RunWorkerCompleted += HandleImageLoaderOnRunWorkerCompleted;

            this.imageLoader.RunWorkerAsync();

            TXT_Produto.Text = p.Nome;
            TXT_Preco.Text = p.PrecoBase.ToString("c2");

            
        }

        private void ovTXT_Produto_Click(object sender, System.EventArgs e)
        {
            SelecionarItem();
        }

        private void ovTXT_Produto_MouseEnter(object sender, System.EventArgs e)
        {
            ColocaHover();
        }

        private void ovTXT_Produto_MouseHover(object sender, System.EventArgs e)
        {
            ColocaHover();
        }

        private void ovTXT_Produto_MouseLeave(object sender, System.EventArgs e)
        {
            RemoveHover();
        }

        private void ColocaHover()
        {
            return;

            if (TXT_Produto.BackColor == Color.LightGray)
                return;

            TXT_Produto.BackColor = Color.Gainsboro;
            TXT_Preco.BackColor = Color.DimGray;
            TXT_Preco.ForeColor = Color.White;
        }

        private void RemoveHover()
        {
            return;

            if (TXT_Produto.BackColor == Color.LightGray)
                return;

            TXT_Produto.BackColor = Color.Silver;
            TXT_Preco.BackColor = Color.Gray;
            TXT_Preco.ForeColor = Color.White;
        }

        private void SelecionarItem()
        {
            (ParentForm as FormFrenteCaixaDelivery).AdicionarProduto(p, null, null);
        }

        public void ColocaCorNormal()
        {
            return; 

            TXT_Produto.ForeColor = Color.DimGray;
            TXT_Preco.ForeColor = Color.White;

            TXT_Produto.BackColor = Color.Silver;
            TXT_Preco.BackColor = Color.Gray;
            TXT_Preco.ForeColor = Color.White;
        }

        public void ProdutoSelecionado()
        {
            return;

            TXT_Produto.ForeColor = Color.White;
            TXT_Preco.ForeColor = Color.White;

            TXT_Produto.BackColor = Color.LightGray;
            TXT_Preco.BackColor = Color.LightGray;
        }
    }
}
