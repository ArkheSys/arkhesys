using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios.Telefonia
{
    public partial class rptFaturaDetalhada : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFaturaDetalhada() { }
        public rptFaturaDetalhada(rptFaturaResumida.ParametroFatura parametro)
        {
            InitializeComponent();
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            xrPictureBoxEsquerda.Image = Bitmap.FromFile(caminho + "\\ImagemFaturaEsquerda.jpg", true);
            xrPictureBoxDireita.Image = Bitmap.FromFile(caminho + "\\ImagemFaturaDireita.jpg", true);

            lblTelefoneEmpresa.Text = parametro.TelefoneEmpresa;
            lblEmailEmpresa.Text = parametro.EmailEmpresa;
            lblFantasia.Text = parametro.Fantasia;
            lblNomeCliente.Text = parametro.NomeCliente;
            lblEnderecoCliente.Text = parametro.EnderecoCliente;
            if (!String.IsNullOrEmpty(parametro.NumeroEnderecoCliente))
                lblEnderecoCliente.Text += ", " + parametro.NumeroEnderecoCliente;
            lblBairroCliente.Text = parametro.BairroCliente;
            lblComplementoCliente.Text = parametro.ComplementoCliente;
            lblCepCliente.Text = parametro.CepCliente;
            lblCidadeCliente.Text = parametro.CidadeCliente + " - " + parametro.EstadoCidadeCliente; 
            lblDataVencimento.Text = parametro.DataVencimento;
            lblNumeroPiloto.Text = parametro.NumeroPiloto;
            lblValorPagar.Text = parametro.ValorPagar;
            lblCodigoFatura.Text = parametro.CodigoFatura;
            
        }

    }
}
