using System;

namespace cwkGestao.Integracao.ACBr.Cte.Entidades
{
    public class IdentificacaoCTe
    {
        public int cUF { get; set; }
        public int cCt { get; set; }
        public string CFOP { get; set; }
        public string natOp { get; set; }
        public int mod { get; set; }
        public int serie { get; set; }
        public int nCT { get; set; }
        public DateTime dhEmi { get; set; }
        public int tpImp { get; set; }
        public int tpEmis { get; set; }
        public int cDV { get; set; }
        public int tpAmb { get; set; }
        public int tpCTe { get; set; }
        public int cMunEnv { get; set; }
        public string xMunEnv { get; set; }
        public string UFEnv { get; set; }
        public string modal { get; set; }
        public int tpServ { get; set; }
        public int cMunIni { get; set; }
        public string xMunIni { get; set; }
        public int UFIni { get; set; }
        public int cMunFim { get; set; }
        public string xMunFim { get; set; }
        public int UFFim { get; set; }
        public int retira { get; set; }
        public int indIEToma { get; set; }
        public int toma3 { get; set; } // verficar
    }
}