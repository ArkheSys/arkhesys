using System;
using System.Drawing;
using System.IO;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;

using DevExpress.XtraReports.UI;

namespace Aplicacao.Modulos.Rel
{
    public partial class XtraImpressaoBoletoD8 : DevExpress.XtraReports.UI.XtraReport
    {
        private Tel_FaturaD8 _faturaD8;
        public XtraImpressaoBoletoD8(Tel_FaturaD8 faturaD8)
        {
            InitializeComponent();
            _faturaD8 = faturaD8;

            Configuracao objconfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();

            try { PB_IMAGEM.Image = Image.FromFile(faturaD8.Filial?.CaminhoLogoEmpresa ?? objconfiguracao.Filial?.CaminhoLogoEmpresa); }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("O Logotipo da Empresa não está definido", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            #region Empresa

            var filial = faturaD8.Filial;
            TXT_EMPRESA.Text = filial.Nome.ToUpper();
            TXT_ENDERECO_EMPRESA.Text = $"{filial.Endereco}, {filial.Numero}, {filial.Bairro}, Cep: {filial.CEP} - {filial.Cidade} {filial.Cidade?.UF.Sigla}".ToUpper();
            TXT_CNPJ_EMPRESA.Text = $"CNPJ: {filial.CNPJ}";

            #endregion

            #region Cliente

            var enderecoPrincipal = faturaD8.Cliente.Pessoa.EnderecoPrincipal();
            var endereco = faturaD8.Cliente.EnderecoProp.IsNotNullOrEmpty()
                ? $"{faturaD8.Cliente.EnderecoProp}, {faturaD8.Cliente.NumeroEnderecoProp}, {faturaD8.Cliente.BairroProp}, {faturaD8.Cliente.CidadeProp?.Nome} {faturaD8.Cliente.CidadeProp?.UF?.Sigla}".ToUpper()
                : $"{enderecoPrincipal.Endereco}, {enderecoPrincipal.Numero}, {enderecoPrincipal.Bairro}, {enderecoPrincipal.Cidade?.Nome} {enderecoPrincipal.Cidade?.UF?.Sigla}".ToUpper();

            TXT_CLIENTE_RAZAOSOCIAL.Text = faturaD8.Cliente.PessoaNome;
            TXT_CLIENTE_ENDERECO.Text = endereco;
            TXT_CLIENTE_CNPJ.Text = faturaD8.Cliente.CPF_CNPJ;
            TXT_CLIENTE_NUMERODOCUMENTO.Text = faturaD8.NumeroDocumento;

            #endregion

            #region Fatura

            TXT_NUMERO_DOCUMENTO.Text = faturaD8.NumeroDocumento;
            TXT_NUMERO_DOCUMENTO2.Text = faturaD8.NumeroDocumento;
            TXT_NUMERO_DOCUMENTO3.Text = faturaD8.NumeroDocumento;
            TXT_NUMERO_DOCUMENTO4.Text = faturaD8.NumeroDocumento;

            TXT_ESP.Text = faturaD8.EspecieDocumento;

            TXT_CODIGOBARRAS.Text = faturaD8.LinhaDigitavelComDv;
            barCode_LinhaDigitavel.Text = faturaD8.LinhaDigitavel.OnlyNumbers();

            TXT_VENCIMENTO.Text = faturaD8.DataVencimento.ToString("dd/MM/yyyy");
            TXT_VENCIMENTO2.Text = faturaD8.DataVencimento.ToString("dd/MM/yyyy");
            TXT_DT_DOC.Text = faturaD8.DataCriacao.ToString("dd/MM/yyyy");

            TXT_VALOR_COBRADO.Text = faturaD8.Valor.ToString("c2");
            TXT_VALOR_COBRADO_2.Text = faturaD8.Valor.ToString("c2");
            TXT_VALOR_DOCUMENTO.Text = faturaD8.Valor.ToString("c2");
            TXT_VALOR_DOCUMENTO2.Text = faturaD8.Valor.ToString("c2");

            TXT_OBSERVACAO.Text = faturaD8.Observacao;

            TXT_EMPRESA02.Text = filial.Nome.ToUpper();
            TXT_EMPRESA2.Text = filial.Nome.ToUpper();

            TXT_CLIENTE_RAZAOSOCIAL02.Text = faturaD8.Cliente.PessoaNome;
            TXT_CLIENTE_ENDERECO02.Text = endereco;

            DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator teste = new DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator
            {
                CalcCheckSum = false,
                WideNarrowRatio = 6F
            };

            TXT_LINHA_DIGITAVEL_COM_DV.Text = faturaD8.LinhaDigitavelComDv;

            barCode_LinhaDigitavel.Symbology = teste;
            #endregion

        }

        public Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        private void qrCodeImage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (_faturaD8.Base64Image == null)
            {
                (sender as XRPictureBox).Visible = false;
                e.Cancel = true;
            }
            else
            {
                (sender as XRPictureBox).Visible = true;
                (sender as XRPictureBox).Image = Base64ToImage(_faturaD8.Base64Image);
            }
        }
    }
}