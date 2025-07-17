using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelatorioMedicaoMes
    {
        public pxyRelatorioMedicaoMes(IGrouping<Ofd_Obra, Ofd_PropostaMedicao> medicoes)
        {
            Obra = medicoes.Key.Descricao;
            for (int i = 1; i <= 31; i++)
			{
                this.GetType()
                    .GetProperty("Dia" + i)
                    .SetValue(this, GetMaquinasDia(i, medicoes), null);
			}
        }

        private string GetMaquinasDia(int dia, IGrouping<Ofd_Obra, Ofd_PropostaMedicao> g)
        {
            StringBuilder ret = new StringBuilder();
            foreach (var item in g.Where(f => f.DataRelatorio.Day == dia))
            {
                if (ret.Length == 0)
                    ret.Append(item.Maquina.Codigo);
                else
                    ret.Append(" e " + item.Maquina.Codigo);
            }
            return ret.ToString();
        }

        public string Obra { get; set; }
        public string Dia1 { get; set; }
        public string Dia2 { get; set; }
        public string Dia3 { get; set; }
        public string Dia4 { get; set; }
        public string Dia5 { get; set; }
        public string Dia6 { get; set; }
        public string Dia7 { get; set; }
        public string Dia8 { get; set; }
        public string Dia9 { get; set; }
        public string Dia10 { get; set; }
        public string Dia11 { get; set; }
        public string Dia12 { get; set; }
        public string Dia13 { get; set; }
        public string Dia14 { get; set; }
        public string Dia15 { get; set; }
        public string Dia16 { get; set; }
        public string Dia17 { get; set; }
        public string Dia18 { get; set; }
        public string Dia19 { get; set; }
        public string Dia20 { get; set; }
        public string Dia21 { get; set; }
        public string Dia22 { get; set; }
        public string Dia23 { get; set; }
        public string Dia24 { get; set; }
        public string Dia25 { get; set; }
        public string Dia26 { get; set; }
        public string Dia27 { get; set; }
        public string Dia28 { get; set; }
        public string Dia29 { get; set; }
        public string Dia30 { get; set; }
        public string Dia31 { get; set; }
    }
}
