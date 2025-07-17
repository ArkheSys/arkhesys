using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class AcompanhamentoObrasMaquinas : DevExpress.XtraReports.UI.XtraReport
    {
        public AcompanhamentoObrasMaquinas(DateTime mes, int quantidadeProjetos, string nomeEmpresa)
        {
            InitializeComponent();
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            xrPictureLogo.Image = Bitmap.FromFile(caminho + "\\LogoEmpresa.jpg", true);
            xrTableCellMes.Text = GetMesPorExtenso(mes);

            xrLabel2.Text = "Total de obras = " + quantidadeProjetos.ToString();
            lblTitulo.Text = nomeEmpresa;
        }

        private string GetMesPorExtenso(DateTime mes)
        {
            switch (mes.Month)
            {
                case 1:
                    return "Janeiro de "+mes.Year;
                case 2:
                    return "Fevereiro de " + mes.Year;
                case 3:
                    return "Março de " + mes.Year;
                case 4:
                    return "Abril de " + mes.Year;
                case 5:
                    return "Maio de " + mes.Year;
                case 6:
                    return "Junho de " + mes.Year;
                case 7:
                    return "Julho de " + mes.Year;
                case 8:
                    return "Agosto de " + mes.Year;
                case 9:
                    return "Setembro de " + mes.Year;
                case 10:
                    return "Outubro de " + mes.Year;
                case 11:
                    return "Novembro de " + mes.Year;
                case 12:
                    return "Dezembro de " + mes.Year;
            }
            return String.Empty;
        }
    }
}
