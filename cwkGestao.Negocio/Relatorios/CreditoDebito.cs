using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Proxy;
using Modelo;
using System.Data;
using Relatorio;

namespace cwkGestao.Negocio.Relatorios
{
    public class CreditoDebito
    {
        private readonly IList<pxyCreditoDebito> valoresImpostos;

        private readonly Dictionary<int, string> mesesDoAno = new Dictionary<int, string>();

        public CreditoDebito(DateTime dtInicio, DateTime dtFim, int idEmpresa)
        {
            mesesDoAno.Add(1, "Janeiro");
            mesesDoAno.Add(2, "Fevereiro");
            mesesDoAno.Add(3, "Março");
            mesesDoAno.Add(4, "Abril");
            mesesDoAno.Add(5, "Maio");
            mesesDoAno.Add(6, "Junho");
            mesesDoAno.Add(7, "Julho");
            mesesDoAno.Add(8, "Agosto");
            mesesDoAno.Add(9, "Setembro");
            mesesDoAno.Add(10, "Outubro");
            mesesDoAno.Add(11, "Novembro");
            mesesDoAno.Add(12, "Dezembro");

            valoresImpostos = NotaController.Instancia.GetNotasCreDeb(dtInicio, dtFim, idEmpresa); 
        }

        public dsCreditoDebito.ValoresDataTable GerarDataSetCreditoDebito()
        {
            dsCreditoDebito dt = new dsCreditoDebito();

            foreach (var valor in valoresImpostos)
            {
                using (DataClassesDataContext db = new DataClassesDataContext())
                {
                    if (dt.Valores.Count < 1)
                    {
                        //Gera Linhas na Tabela dsCreditoDebito
                        if (valoresImpostos[0].dtNota.Month > valoresImpostos[valoresImpostos.Count - 1].dtNota.Month)
                        {
                            for (int mes = valoresImpostos[0].dtNota.Month; mes <= 12; mes++)
                                GerarLinhaParaNovoMes(dt, valor, mesesDoAno[mes] + "/" + valoresImpostos[0].dtNota.Year);

                            for (int mes = 1; mes <= valoresImpostos[valoresImpostos.Count - 1].dtNota.Month; mes++)
                                GerarLinhaParaNovoMes(dt, valor, mesesDoAno[mes] + "/" + valoresImpostos[valoresImpostos.Count - 1].dtNota.Year);

                            continue;
                        }

                        for (int mes = valoresImpostos[0].dtNota.Month; mes <= valoresImpostos[valoresImpostos.Count - 1].dtNota.Month; mes++)
                            GerarLinhaParaNovoMes(dt, valor, mesesDoAno[mes] + "/" + valoresImpostos[0].dtNota.Year);

                        continue;
                    }

                    //Atribui valores nas linhas geradas
                    foreach (DataRow linhaTabela in dt.Valores)
                    {
                        int indice = linhaTabela["Mes"].ToString().IndexOf("/");
                        if ((linhaTabela["Mes"].ToString().Remove(indice) == mesesDoAno[valor.dtNota.Month]))
                            AtribuirValoresEmLinhaExistente(valor, linhaTabela);
                    }
                }
            }
            return dt.Valores;
        }

        private static void AtribuirValoresEmLinhaExistente(pxyCreditoDebito valor, DataRow linhaTabela)
        {
            if (valor.Ent_Sai == cwkGestao.Modelo.InOutType.Entrada)
            {
                if (valor.Ipi_CreDeb)
                    linhaTabela["Ipi_Cre"] = (decimal)linhaTabela["Ipi_Cre"] + valor.Ipi;

                if (valor.Pis_CreDeb)
                    linhaTabela["Pis_Cre"] = (decimal)linhaTabela["Pis_Cre"] + valor.Pis;

                if (valor.Cofins_CreDeb)
                    linhaTabela["Cofins_Cre"] = (decimal)linhaTabela["Cofins_Cre"] + valor.Cofins;

                if (valor.Icms_CreDeb)
                    linhaTabela["Icms_Cre"] = (decimal)linhaTabela["Icms_Cre"] + valor.Icms;
            }
            else
            {
                if (valor.Ipi_CreDeb)
                    linhaTabela["Ipi_Deb"] = (decimal)linhaTabela["Ipi_Deb"] + valor.Ipi;

                if (valor.Pis_CreDeb)
                    linhaTabela["Pis_Deb"] = (decimal)linhaTabela["Pis_Deb"] + valor.Pis;

                if (valor.Cofins_CreDeb)
                    linhaTabela["Cofins_Deb"] = (decimal)linhaTabela["Cofins_Deb"] + valor.Cofins;

                if (valor.Icms_CreDeb)
                    linhaTabela["Icms_Deb"] = (decimal)linhaTabela["Icms_Deb"] + valor.Icms;
            }
        }

        private void GerarLinhaParaNovoMes(Relatorio.dsCreditoDebito dt, pxyCreditoDebito valor, string mes)
        {
            decimal ipi = 0, pis = 0, cofins = 0, icms = 0;

            if (valor.Ipi_CreDeb)
                ipi = valor.Ipi;

            if (valor.Pis_CreDeb)
                pis = valor.Pis;

            if (valor.Cofins_CreDeb)
                cofins = valor.Cofins;

            if (valor.Icms_CreDeb)
                icms = valor.Icms;

            if (valor.Ent_Sai == cwkGestao.Modelo.InOutType.Saída)
                dt.Valores.AddValoresRow(mes, 0, 0, 0, 0, ipi, pis, cofins, icms);
            else
                dt.Valores.AddValoresRow(mes, ipi, pis, cofins, icms, 0, 0, 0, 0);
        }

    }
}
