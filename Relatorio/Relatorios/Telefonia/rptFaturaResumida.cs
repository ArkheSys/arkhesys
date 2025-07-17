using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios.Telefonia
{
    public partial class rptFaturaResumida : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFaturaResumida() { }

        public rptFaturaResumida(ParametroFatura parametro)
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
            lblData.Text = parametro.Data;
            lblDataVencimento.Text = parametro.DataVencimento;
            lblNumeroPiloto.Text = parametro.NumeroPiloto;
            lblValorPagar.Text = parametro.ValorPagar;
            lblCodigoFatura.Text = parametro.CodigoFatura;
            
        }

        public struct ParametroFatura
        {
            public string TelefoneEmpresa { get; set; }
            public string EmailEmpresa { get; set; }
            public string Fantasia { get; set; }
            public string NomeCliente { get; set; }
            public string ContatoCliente { get; set; }
            public string EnderecoCliente { get; set; }
            public string NumeroEnderecoCliente { get; set; }
            public string BairroCliente { get; set; }
            public string ComplementoCliente { get; set; }
            public string CepCliente { get; set; }
            public string CidadeCliente { get; set; }
            public string Data { get; set; }
            public string DataVencimento { get; set; }
            public string NumeroPiloto { get; set; }
            public string ValorPagar { get; set; }
            public string CodigoFatura { get; set; }
            public string EstadoCidadeCliente { get; set; }
        }
    }
}
