using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.Sped.Fiscal
{
    public class ConversorComboBox
    {
        public static int IND_NAT_PJ(string cod)
        {
            switch (cod)
            {
                case "00":
                    return 0;
                default:
                    return -1;
            }
        }

        public static int TipoAtividadePreponderante(string cod)
        {
            switch (cod)
            {
                case "0":
                    return 0;
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "9":
                    return 5;
                default:
                    return -1;
            }
        }


        public static int CodigoIndicadorIncidenciaTributaria(string cod)
        {
            switch (cod)
            {
                case "1":
                    return 0;
                case "2":
                    return 1;
                case "3":
                    return 2;
                default:
                    return -1;
            }
        }


        public static int CodigoIndicadorMetodoApropriacaoCreditosComuns(string cod)
        {    
            switch (cod)
            {
                case "1":
                    return 0;
                case "2":
                    return 1;
                default:
                    return -1;
            }
        }


        public static int CodigoIndicadorTipoContribuicaoApurada(string cod)
        {
            switch (cod)
            {
                case "1":
                    return 0;
                case "2":
                    return 1;
                default:
                    return -1;
            }
        }


        public static int CodigoIndicadorCriterioEscrituracaoApuracaoAdotado(string cod)
        {
            switch (cod)
            {
                case "1":
                    return 0;
                case "2":
                    return 1;
                case "9":
                    return 2;
                default:
                    return -1;
            }
        }

        public static int TipoItemSPED(string cod)
        {
            switch (cod)
            {
                case "00":
                    return 0;
                case "01":
                    return 1;
                case "02":
                    return 2;
                case "03":
                    return 3;
                case "04":
                    return 4;
                case "05":
                    return 5;
                case "06":
                    return 6;
                case "07":
                    return 7;
                case "08":
                    return 8;
                case "09":
                    return 9;
                case "10":
                    return 10;
                case "99":
                    return 11;
                default:
                    return -1;
            }
        }
    }
}
