using cwkGestao.Modelo.Proxy;

using System;
using System.Collections.Generic;
using System.Data;

namespace Aplicacao.Modulos.Rel
{
    public partial class XtraRelatorioTabelaPrecosVenda : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraRelatorioTabelaPrecosVenda()
        {
            InitializeComponent();
        }

        public XtraRelatorioTabelaPrecosVenda(string NomeEmpresa, List<pxyItemFrenteCaixaPorFilial> listDados, DateTime? DataInicial, DateTime? DataFinal)
        {
            InitializeComponent();

            ovTXT_Empresa.Text = NomeEmpresa;
            txtDataInicial.Text = DataInicial?.ToString("dd/MM/yyyy");
            txtDataFinal.Text = DataFinal?.ToString("dd/MM/yyyy");

            DataTable dt = new DataTable { TableName = "DADOS" };
            dt.Columns.Add("idfilial");
            dt.Columns.Add("filial");
            dt.Columns.Add("idoperador");
            dt.Columns.Add("operador");
            dt.Columns.Add("datacadastro");
            dt.Columns.Add("idproduto");
            dt.Columns.Add("codigo");
            dt.Columns.Add("nome");
            dt.Columns.Add("idtabelapreco");
            dt.Columns.Add("tabelapreco");
            dt.Columns.Add("quantidade");
            dt.Columns.Add("total");

            foreach (var item in listDados)
            {
                DataRow dr = dt.NewRow();
                dr["idfilial"] = item.IDFilial;
                dr["filial"] = item.Filial;
                dr["idoperador"] = item.IDOperador;
                dr["operador"] = item.Operador;
                dr["datacadastro"] = item.DataCadastro;
                dr["idproduto"] = item.IDProduto;
                dr["codigo"] = item.Codigo;
                dr["nome"] = item.Nome;
                dr["idtabelapreco"] = item.IDTabelaPreco;
                dr["tabelapreco"] = item.TabelaPreco;
                dr["quantidade"] = item.Quantidade;
                dr["total"] = item.Total;
                dt.Rows.Add(dr);
            }

            ovDS_Dados.Tables.Clear();
            ovDS_Dados.Tables.Add(dt);
        }
    }
}
