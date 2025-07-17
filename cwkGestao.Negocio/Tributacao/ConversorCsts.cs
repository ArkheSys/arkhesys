using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Tributacao
{
    public class ConversorCsts
    {
        public static string[] CSOSNsGeramST = new string[] { "201", "202", "203" };

        public static string IndiceParaCstPis(int aIndex)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterIndiceParaCstPis(aIndex);
        }
        public static int CstPisParaIndice(string cst)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterCstPisParaIndice(cst);
        }

        public static string IndiceParaCstCofins(int aIndex)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterIndiceParaCstCofins(aIndex);
        }
        public static int CstCofinsParaIndice(string cst)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterCstCofinsParaIndice(cst);
        }

        public static string IndiceParaCstIpi(int aIndex)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterIndiceParaCstIpi(aIndex);
        }
        public static int CstIpiParaIndice(string cst)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterCstIpiParaIndice(cst);
        }

        public enum ModeloNota { Modelo1, NFe }
        public enum STEmpresa { RegimeSimples, RegimeNormal }

        public static string IndiceParaTributacao(int? indiceCST, int? indiceCSOSN, ModeloNota modeloNota, STEmpresa simplesNacional, bool temST)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterIndiceParaTributacao(indiceCST, indiceCSOSN, ((cwkGestao.Repositorio.Util.ConversorCsts.ModeloNota)(int)modeloNota),((cwkGestao.Repositorio.Util.ConversorCsts.STEmpresa)(int)simplesNacional), temST);
        }

        public static int TributacaoParaIndice(string tributacao)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterTributacaoParaIndice(tributacao);
        }
    }
}
