using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BLL
{
    public class Recibo
    {
        protected string nomerel, ds, TipoRel, ValorPorExtenso;
        protected List<Microsoft.Reporting.WinForms.ReportParameter> parametros;
        protected DataTable Dt;
        protected DateTime DataAtual;
        protected DataTableReader dtr;

        public List<Object> recibo(String EndCliente, String EndEmpresa, String pReferente, Decimal pValor, String pTipoRelatorio, String pFilial, String pPessoa, String pCNPJ_CPF) 
        {
            List<Object> ParametrosForm = new List<Object>();

            ValorPorExtenso = ClsExtenso.Extenso_Valor((Decimal)pValor).ToUpper();
            DataAtual = DateTime.Now;

            List<Microsoft.Reporting.WinForms.ReportParameter> parametros = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("DataAtual", DataAtual.ToShortDateString().ToString());
            Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("ValorPorExtenso", ValorPorExtenso);
            Microsoft.Reporting.WinForms.ReportParameter p3 = new Microsoft.Reporting.WinForms.ReportParameter("Valor", pValor.ToString());

            parametros.Add(p1);
            parametros.Add(p2);
            parametros.Add(p3);

            if (pTipoRelatorio == Modelo.TipoDocumentoType.Pagar.ToString())
            {
                Microsoft.Reporting.WinForms.ReportParameter p4 = new Microsoft.Reporting.WinForms.ReportParameter("Recebemos", pFilial);
                Microsoft.Reporting.WinForms.ReportParameter p5 = new Microsoft.Reporting.WinForms.ReportParameter("EnderecoDeCima", EndEmpresa);
                Microsoft.Reporting.WinForms.ReportParameter p6 = new Microsoft.Reporting.WinForms.ReportParameter("Referente", pReferente);
                Microsoft.Reporting.WinForms.ReportParameter p7 = new Microsoft.Reporting.WinForms.ReportParameter("Emitente", pPessoa);
                Microsoft.Reporting.WinForms.ReportParameter p8 = new Microsoft.Reporting.WinForms.ReportParameter("EnderecoDeBaixo", EndCliente);
                Microsoft.Reporting.WinForms.ReportParameter p9 = new Microsoft.Reporting.WinForms.ReportParameter("CNPJ_CPF", pCNPJ_CPF);

                parametros.Add(p4);
                parametros.Add(p5);
                parametros.Add(p6);
                parametros.Add(p7);
                parametros.Add(p8);
                parametros.Add(p9);
            }
            else //A Receber
            {
                Microsoft.Reporting.WinForms.ReportParameter p4 = new Microsoft.Reporting.WinForms.ReportParameter("Recebemos", pPessoa);
                Microsoft.Reporting.WinForms.ReportParameter p5 = new Microsoft.Reporting.WinForms.ReportParameter("EnderecoDeCima", EndCliente);
                Microsoft.Reporting.WinForms.ReportParameter p6 = new Microsoft.Reporting.WinForms.ReportParameter("Referente", pReferente);
                Microsoft.Reporting.WinForms.ReportParameter p7 = new Microsoft.Reporting.WinForms.ReportParameter("Emitente", pFilial);
                Microsoft.Reporting.WinForms.ReportParameter p8 = new Microsoft.Reporting.WinForms.ReportParameter("EnderecoDeBaixo", EndEmpresa);
                Microsoft.Reporting.WinForms.ReportParameter p9 = new Microsoft.Reporting.WinForms.ReportParameter("CNPJ_CPF", pCNPJ_CPF);

                parametros.Add(p4);
                parametros.Add(p5);
                parametros.Add(p6);
                parametros.Add(p7);
                parametros.Add(p8);
                parametros.Add(p9);
            }

            nomerel = @"Recibo\rptRecibo.rdlc";

            ds = "dsRelRecibo_Recibo";

            Dt = new DataTable();
            
            ParametrosForm.Add(nomerel);
            ParametrosForm.Add(ds);
            ParametrosForm.Add(Dt);
            ParametrosForm.Add(parametros);

            return ParametrosForm;
        }
    }
}
