using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyCreditoDebito
    {
        public pxyCreditoDebito()
        {

        }

        public pxyCreditoDebito(DateTime dtNota, InOutType ent_Sai, bool pis_CreDeb,
            bool cofins_CreDeb, bool ipi_CreDeb, bool icms_CreDeb,
            decimal pis, decimal ipi, decimal cofins, decimal icms)
        {
            this.dtNota = dtNota;
            this.Ent_Sai = ent_Sai;

            this.Pis_CreDeb = pis_CreDeb;
            this.Ipi_CreDeb = ipi_CreDeb;
            this.Cofins_CreDeb = cofins_CreDeb;
            this.Icms_CreDeb = icms_CreDeb;

            this.Pis = pis;
            this.Ipi = ipi;
            this.Cofins = cofins;
            this.Icms = icms;
        }

        public DateTime dtNota { get; set; }
        public InOutType Ent_Sai { get; set; }

        public bool Pis_CreDeb { get; set; }
        public bool Ipi_CreDeb { get; set; }
        public bool Cofins_CreDeb { get; set; }
        public bool Icms_CreDeb { get; set; }

        public decimal Pis { get; set; }
        public decimal Ipi { get; set; }
        public decimal Cofins { get; set; }
        public decimal Icms { get; set; }

    }
}
