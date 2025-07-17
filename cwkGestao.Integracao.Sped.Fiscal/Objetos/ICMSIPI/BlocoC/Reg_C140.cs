using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C140 : Registro
    {
        public string REG = "C140";
        public int IND_EMIT;// Indicador do emitente do título C 001* - O O
        public string IND_TIT;// C 002* - O O
        public string DESC_TIT;// Descrição complementar do título de crédito C - - OC OC
        public string NUM_TIT;// C - - O O
        public int QTD_PARC;// Quantidade de parcelas a receber/pagar N 002 - O O
        public decimal VL_TIT;// Valor total dos títulos de créditos N - 02 O O
    }
}
