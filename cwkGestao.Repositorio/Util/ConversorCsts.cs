using System;

namespace cwkGestao.Repositorio.Util
{
    public static class ConversorCsts
    {
        public static string ConverterIndiceParaCstPis(int aIndex)
        {
            switch (aIndex)
            {
                case 0: return "01";
                case 1: return "02";
                case 2: return "03";
                case 3: return "04";
                case 4: return "06";
                case 5: return "07";
                case 6: return "08";
                case 7: return "09";
                case 8: return "99";
                default: return "";
            }
        }

        public static int ConverterCstPisParaIndice(string cst)
        {
            switch (cst)
            {
                case "01": return 0;
                case "02": return 1;
                case "03": return 2;
                case "04": return 3;
                case "06": return 4;
                case "07": return 5;
                case "08": return 6;
                case "09": return 7;
                case "99": return 8;
                default: return -1;
            }
        }

        public static string ConverterIndiceParaCstCofins(int aIndex)
        {
            switch (aIndex)
            {
                case 0: return "01";
                case 1: return "02";
                case 2: return "03";
                case 3: return "04";
                case 4: return "06";
                case 5: return "07";
                case 6: return "08";
                case 7: return "09";
                case 8: return "99";
                default: return "";
            }
        }

        public static int ConverterCstCofinsParaIndice(string cst)
        {
            switch (cst)
            {
                case "01": return 0;
                case "02": return 1;
                case "03": return 2;
                case "04": return 3;
                case "06": return 4;
                case "07": return 5;
                case "08": return 6;
                case "09": return 7;
                case "99": return 8;
                default: return -1;
            }
        }

        public static string ConverterIndiceParaCstIpi(int aIndex)
        {
            switch (aIndex)
            {
                case 0: return "00";
                case 1: return "01";
                case 2: return "02";
                case 3: return "03";
                case 4: return "04";
                case 5: return "05";
                case 6: return "49";
                case 7: return "50";
                case 8: return "51";
                case 9: return "52";
                case 10: return "53";
                case 11: return "54";
                case 12: return "55";
                case 13: return "99";
                default: return "";
            }
        }

        public static int ConverterCstIpiParaIndice(string cst)
        {
            switch (cst)
            {
                case "00": return 0;
                case "01": return 1;
                case "02": return 2;
                case "03": return 3;
                case "04": return 4;
                case "05": return 5;
                case "49": return 6;
                case "50": return 7;
                case "51": return 8;
                case "52": return 9;
                case "53": return 10;
                case "54": return 11;
                case "55": return 12;
                case "99": return 13;
                default: return -1;
            }
        }

        public enum ModeloNota
        {
            Modelo1,
            NFe
        }

        public enum STEmpresa
        {
            RegimeSimples,
            RegimeNormal
        }

        public static string ConverterIndiceParaTributacao(int? indiceCST, int? indiceCSOSN, ModeloNota modeloNota, STEmpresa simplesNacional, bool temST)
        {
            try
            {
                switch (modeloNota)
                {
                    case ModeloNota.Modelo1: return ConverterIndiceParaCST(indiceCST);
                    case ModeloNota.NFe:
                        if (temST && simplesNacional != STEmpresa.RegimeSimples)
                            return ConverterIndiceParaCST(indiceCST);

                        if (simplesNacional == STEmpresa.RegimeSimples)
                            return ConverterIndiceParaCSOSN(indiceCSOSN);

                        return ConverterIndiceParaCST(indiceCST);
                    default: throw new Exception();
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Erro na conversão do campo de tributação, verifique no cadastro deste produto os campos de tributação CST e CSOSN e a situação tributária da empresa.", exc);
            }
        }

        private static string ConverterIndiceParaCST(int? indiceCST)
        {
            try
            {
                switch (indiceCST.Value)
                {
                    case 0: return "00";
                    case 1: return "10";
                    case 2: return "20";
                    case 3: return "30";
                    case 4: return "40";
                    case 5: return "41";
                    case 6: return "50";
                    case 7: return "51";
                    case 8: return "60";
                    case 9: return "70";
                    case 10: return "90";
                    default: throw new Exception();
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Erro na conversão do campo CST.", exc);
            }
        }

        public static string ConverterIndiceParaCSOSN(int? indiceCSOSN)
        {
            try
            {
                switch (indiceCSOSN.Value)
                {
                    case 0: return "101";
                    case 1: return "102";
                    case 2: return "103";
                    case 3: return "201";
                    case 4: return "202";
                    case 5: return "203";
                    case 6: return "300";
                    case 7: return "400";
                    case 8: return "500";
                    case 9: return "900";
                    default: throw new Exception();
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Erro na conversão do campo CSOSN.", exc);
            }
        }

        public static int ConverterTributacaoParaIndice(string tributacao)
        {
            if (tributacao.Length == 2)
                return ConverterCstParaIndice(tributacao);
            return ConverterCsosnParaIndice(tributacao);
        }

        private static int ConverterCsosnParaIndice(string tributacao)
        {
            switch (tributacao)
            {
                case "101": return 0;
                case "102": return 1;
                case "103": return 2;
                case "201": return 3;
                case "202": return 4;
                case "203": return 5;
                case "300": return 6;
                case "400": return 7;
                case "500": return 8;
                case "900": return 9;
                default: throw new Exception();
            }
        }

        private static int ConverterCstParaIndice(string tributacao)
        {
            switch (tributacao)
            {
                case "00": return 0;
                case "10": return 1;
                case "20": return 2;
                case "30": return 3;
                case "40": return 4;
                case "41": return 5;
                case "50": return 6;
                case "51": return 7;
                case "60": return 8;
                case "70": return 9;
                case "90": return 10;
                default: throw new Exception();
            }
        }
    }
}