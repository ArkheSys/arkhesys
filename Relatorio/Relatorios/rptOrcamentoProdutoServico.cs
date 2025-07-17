using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoProdutoServico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoProdutoServico(int IDPedido)
        {
            InitializeComponent();
            int totalEquipamento, totalMaterial, totalMaoObra;
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                xrPictureBox4.Image = Bitmap.FromFile(caminho, true);
            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
                return;
            }
            finally
            {

                rptOrcamentoProdutoServicoMaoObra subOrcamentoProdutoServicomaoObra = new rptOrcamentoProdutoServicoMaoObra(IDPedido, out totalMaoObra);
                SubOrcamentoProdutoServicoMaoDeObra.ReportSource = subOrcamentoProdutoServicomaoObra;

                rptOrcamentoProdutoServicoEquipamento subOrcamentoProdutoServicoEquipamento = new rptOrcamentoProdutoServicoEquipamento(IDPedido, out totalEquipamento);
                SubOrcamentoProdutoServicoEquipamento.ReportSource = subOrcamentoProdutoServicoEquipamento;

                rptOrcamentoProdutoServicoSubMaterial subOrcamentoProdutoServicoMaterial = new rptOrcamentoProdutoServicoSubMaterial(IDPedido, out totalMaterial);
                SubOrcamentoProdutoServicoMaterial.ReportSource = subOrcamentoProdutoServicoMaterial;

                rptOrcamentoProdutoServicoSubTotalizadores subOrcamentoProdutoServicoTotalizador = new rptOrcamentoProdutoServicoSubTotalizadores();
                subOrcamentoProdutoServicoTotalizador.TotalEquipamento.Value = totalEquipamento;
                subOrcamentoProdutoServicoTotalizador.TotalMaoObra.Value = totalMaoObra;
                subOrcamentoProdutoServicoTotalizador.TotalMaterial.Value = totalMaterial;
                subOrcamentoProdutoServicoTotalizador.TotalOrcamento.Value = totalEquipamento + totalMaoObra + totalMaterial;

                SubOrcamentoProdutoServicoTotalizador.ReportSource = subOrcamentoProdutoServicoTotalizador;

            }
        }
    }
}
