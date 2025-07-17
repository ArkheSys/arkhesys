using cwkGestao.Modelo.Proxy;

using System.Collections.Generic;
using System.Data;

namespace Aplicacao.Modulos.Rel
{
    public partial class XtraRelatorioTabelaPrecosGeral : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraRelatorioTabelaPrecosGeral()
        {
            InitializeComponent();
        }

        public XtraRelatorioTabelaPrecosGeral(string NomeEmpresa, List<pxyProdutoTabelaPreco> listDados)
        {
            InitializeComponent();

            TXT_Empresa.Text = NomeEmpresa;

            DataTable dt = new DataTable { TableName = "DADOS" };
            dt.Columns.Add("idtabelapreco");
            dt.Columns.Add("tabelapreco");
            dt.Columns.Add("idproduto");
            dt.Columns.Add("codigo");
            dt.Columns.Add("produto");
            dt.Columns.Add("unidade");
            dt.Columns.Add("precobase");
            foreach (var item in listDados)
            {
                DataRow dr = dt.NewRow();
                dr["idtabelapreco"] = item.IDTabelaPreco;
                dr["tabelapreco"] = item.TabelaPreco;
                dr["idproduto"] = item.IDProduto;
                dr["codigo"] = item.Codigo;
                dr["produto"] = item.Produto;
                dr["unidade"] = item.Unidade;
                dr["precobase"] = item.PrecoBase;
                dt.Rows.Add(dr);
            }

            ovDS_Dados.Tables.Clear();
            ovDS_Dados.Tables.Add(dt);
        }
    }
}
