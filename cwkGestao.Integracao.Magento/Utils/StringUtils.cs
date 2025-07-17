using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace cwkGestao.Integracao.Magento.Utils
{
    public static class StringUtils
    {
        public static decimal ValorMagentoToDecimal(this string valor)
        {
            return Convert.ToDecimal(valor, CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static double ValorMagentoToDouble(this string valor)
        {
            return Convert.ToDouble(valor, CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
