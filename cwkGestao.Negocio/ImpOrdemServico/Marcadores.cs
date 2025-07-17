using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixReporter;

namespace cwkGestao.Negocio.ImpOrdemServico
{
    public static class Marcadores
    {
        public static string Build(bool condensado, bool negrito, bool expandido)
        {
            EpsonCodes lCodes = new EpsonCodes();
            string retorno = "";

            if (condensado)
                retorno += lCodes.CondensedOn;
            else
                retorno += lCodes.CondensedOff;

            if (negrito)
                retorno += lCodes.BoldOn;
            else
                retorno += lCodes.BoldOff;

            if (expandido)
                retorno += lCodes.ExpandedOn;
            else
                retorno += lCodes.ExpandedOff;

            return retorno;

        }
    }
}
