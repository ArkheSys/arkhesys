using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C141 : Registro
    {
        public string REG = "C141";// C 004 - O O
        public int NUM_PARC;// Número da parcela a receber/pagar N 002 - O O
        public DateTime DT_VCTO;// Data de vencimento da parcela N 008* - O O
        public decimal VL_PARC;// Valor da parcela a receber/pagar N - 02 O O
    }
}
