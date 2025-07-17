using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptSubNFSeImpostos : DevExpress.XtraReports.UI.XtraReport
    {
        public rptSubNFSeImpostos(DateTime dtInicio, DateTime dtFinal, int idEmpresa, string bBaseRet, string idTipoNota, string idStatus)
        {
            InitializeComponent();
            dtNFSeImpostosTableAdapter.GetData(dtInicio, dtFinal, idEmpresa, bBaseRet, idTipoNota, idStatus);
        }

    }
}
