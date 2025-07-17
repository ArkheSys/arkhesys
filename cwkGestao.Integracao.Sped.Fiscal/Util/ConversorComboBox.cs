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

        public static int CodigoIndicadorPerfil(string cod)
        {
            switch (cod.Trim())
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
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
                case "":
                    return 0;
                case "1":
                    return 1;
                case "2":
                    return 2;
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

        public static string CodigoIndicadorCriterioEscrituracaoApuracaoAdotadoSelected(int cod)
        {
            switch (cod)
            {
                case 0:
                    return "1";
                case 1:
                    return "2";
                case 2:
                    return "9";
                default:
                    return "-1";
            }
        }

        public static int Cod_Nat_CC(string cod)
        {
            switch (cod)
            {
                case "01":
                    return 0;
                case "02":
                    return 1;
                case "03":
                    return 2;
                case "04":
                    return 3;
                case "05":
                    return 4;
                case "09":
                    return 5;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Converte o tipo de impressão da NFC-e para o Index do combobox
        /// </summary>
        /// <param name="cod">Tipo de impressão do Danfe da NFC-e 4=DANFE NFC-e; 5=DANFE NFC-e em mensagem eletrônica</param>
        /// <returns>Retorna o index do compo box</returns>
        public static int tpImpDanfeNFCe(int cod)
        {
            switch (cod)
	        {
                case 4: return 0;
                case 5: return 1;
		        default: return -1;
	        }
        }

        /// <summary>
        /// Converte o Index do combobox para o tipo de impressão da NFC-e.
        /// </summary>
        /// <param name="selectedIndex">Index do combobox</param>
        /// <returns>Retorna o tipo de impressão da NFC-e.</returns>
        public static int tpImpDanfeNFCeSelected(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0: return 4;
                case 1: return 5;
                default: return -1;
            }
        }

        public static int tpImpDanfeNFe(int cod)
        {
            switch (cod)
            {
                case 1: return 0;
                case 2: return 1;
                default: return -1;
            }
        }

        public static int tpImpDanfeNFeSelected(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0: return 1;
                case 1: return 2;
                default: return -1;
            }
        }

        public static int tpModeloNotaSelected(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0: return 55;
                case 1: return 65;
                default: return -1;
            }
        }
    }
}
