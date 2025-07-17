using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Utils
{
    public static class DatasUtil
    {
        public static string GetLiteralMes(int mes)
        {
            switch (mes)
            {
                case 1: return "Janeiro";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 2: return "Fevereiro";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 3: return "Março";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 4: return "Abril";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 5: return "Maio";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 6: return "Junho";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 7: return "Julho";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 8: return "Agosto";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 9: return "Setembro";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 10: return "Outubro";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 11: return "Novembro";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                case 12: return "Dezembro";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
                default: return "Não Informado";
#pragma warning disable CS0162 // Código inacessível detectado
                    break;
#pragma warning restore CS0162 // Código inacessível detectado
            }
        }

    }
}
