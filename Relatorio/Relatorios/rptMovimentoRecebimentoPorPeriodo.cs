using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;
using Relatorio.dsMovimentoRecebimentoTableAdapters;

namespace Relatorio.Relatorios
{
    public partial class rptMovimentoRecebimentoPorPeriodo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMovimentoRecebimentoPorPeriodo(string Considera, DateTime DtInicio, DateTime DtFim, string IDFilial, string IDOperacoes, string IDMovimentoTipos)
        {
            InitializeComponent();
            movimentoRecebimentoPorPeríodoTableAdapter.GetData(Considera, DtInicio, DtFim, IDFilial, IDOperacoes, IDMovimentoTipos);            
        }
    }
}
