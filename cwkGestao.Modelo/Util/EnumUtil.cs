using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Util
{
    public static class EnumUtil
    {
        public static string TipoDiametroToString(this TipoDiametro diametro)
        {
            var nome = Enum.GetName(typeof(TipoDiametro), diametro);
            if (nome != null)
                return nome.Substring(1).Replace("_", ",");
            return String.Empty;
        }
    }
}
