using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;

namespace BLL
{
    public static class Tributacao
    {
        public static Modelo.DataClassesDataContext db { get; set; }

        private class RegraImpostoProduto
        {
            public decimal AliqContrib;
            public decimal AliqNContrib;
            public decimal AliqContribNormal;
            public decimal AliqNContribNormal;
            public decimal AliqSubstTributaria;
            public string TextoLeiContrib;
            public string TextoLeiNContrib;
            public decimal ReducaoContrib;
            public decimal ReducaoNContrib;
            public decimal modBCICMS;
            public decimal modBCST;
            public decimal LucroST;
            public decimal AliquotaCredSimples;
            public decimal PercRedBCST;
            public decimal PercRedCredSimples;
            public decimal AliqInterna;
        }

        private enum TipoST
        {
            Substituido = 0,
            Substituto = 1
        }

        public enum ModeloNota
        {
            Modelo1 = 1,
            NFe = 55
        }

        /// <summary>
        /// Método que calcula ICMS
        /// </summary>
        /// <param name="pEestadoOrigem">Estado(UF) da empresa que está vendendo</param>
        /// <param name="pEstadoDestino">Indica se a empresa é contribuinte ou não</param>
        /// <param name="pContribuinte">Qual o produto que está sendo comercializado (Venda/Compra)</param>
        /// <param name="pObjProduto">Qual o produto</param>
        /// <param name="pObjTabelaCFOP">CFOP</param>
        /// <param name="pValor">Valor total do produto comercializado</param>
        /// <param name="pAcrescimo">Valor de Acrescimo do produto</param>
        /// <param name="pDesconto">Valor de Desconto do produto</param>
        /// <param name="pEntSai">Indica se o calculo do imposto será para nota de compra ou venda</param>
        /// <returns>Retorna os valores do Imposto Calculado</returns>
        public static Modelo.ImpostoNF CalculaIcms(Modelo.Filial objFilial, string pEstadoDestino, bool pContribuinte, Modelo.Produto pObjProduto, Modelo.TabelaCFOP pObjTabelaCFOP, decimal pValor, decimal pAcrescimo, decimal pDesconto, Modelo.InOutType pEntSai, byte? pOrgaoPublico, ModeloNota modeloNota)
        {
            //atributo de retorno do imposto calculado
            Modelo.ImpostoNF imposto = new Modelo.ImpostoNF();

            //Cria o contexto para pesquisa no banco de dados
            //if (db == null)
            db = new Modelo.DataClassesDataContext();

            //Acessa o tabela de configuração
            Modelo.Configuracao objConfig = new Modelo.Configuracao();
            objConfig = (from cfg in db.Configuracaos
                         where cfg.Codigo == 1
                         select cfg).Single();

            //Atribui a aliquota ref. a Cupom Fiscal
            if (pContribuinte)
                imposto.aliquotaCupomFiscal = pObjProduto.AliqCupomContrib;
            else
                imposto.aliquotaCupomFiscal = pObjProduto.AliqCupomNContrib;

            //Cria o atributo da regra de imposto
            List<RegraImpostoProduto> regraProduto = new List<RegraImpostoProduto>();

            //Verifica se é o imposto é para calcular dentro do estado ou não
            if (objFilial.Cidade.UF.Sigla != pEstadoDestino)
            {
                string ufOrigem = (pEntSai == Modelo.InOutType.Entrada ? pEstadoDestino : objFilial.Cidade.UF.Sigla)
                    , ufDestino = (pEntSai == Modelo.InOutType.Entrada ? objFilial.Cidade.UF.Sigla : pEstadoDestino);

                //Verifica se o pruduto possui uma regra específica para venda fora do estado
                int temregra = (int)(from r in pObjProduto.Produto_Icms
                                     where r.UFOrigem.Sigla == ufOrigem &&
                                     r.UFDestino.Sigla == ufDestino
                                     select r).Count();

                if (temregra == 1)
                {
                    regraProduto = (from r in pObjProduto.Produto_Icms
                                    where r.UFOrigem.Sigla == ufOrigem &&
                                    r.UFDestino.Sigla == ufDestino
                                    select new RegraImpostoProduto
                                    {
                                        AliqContrib = r.AliqContrib.Value,
                                        AliqNContrib = r.AliqNContrib.Value,
                                        AliqContribNormal = r.AliqContribNormal.Value,
                                        AliqNContribNormal = r.AliqNContribNormal.Value,
                                        AliqSubstTributaria = r.AliqSubstTributaria.Value,
                                        TextoLeiContrib = r.TextoLeiContrib,
                                        TextoLeiNContrib = r.TextoLeiNContrib,
                                        ReducaoContrib = r.ReducaoContrib.Value,
                                        ReducaoNContrib = r.ReducaoNContrib.Value,
                                        modBCICMS = r.modBCICMS.Value,
                                        modBCST = r.modBCST.Value,
                                        LucroST = r.LucroST.Value,
                                        PercRedBCST = r.pRedBCST.Value,
                                        AliqInterna = r.AliqInterna.Value
                                    }
                                    ).ToList();
                }
                else
                {
                    var icmsInterestadual = (from r in db.ICMS_Interestaduals
                                             where r.UFOrigem.Sigla == ufOrigem &&
                                             r.UFDestino.Sigla == ufDestino
                                             select new RegraImpostoProduto
                                             {
                                                 AliqContrib = r.AliqContrib.Value,
                                                 AliqNContrib = r.AliqNContrib.Value,
                                                 AliqContribNormal = r.AliqContribNormal.Value,
                                                 AliqNContribNormal = r.AliqNContribNormal.Value,
                                                 AliqSubstTributaria = r.AliqSubstTributaria.Value,
                                                 TextoLeiContrib = r.TextoLeiContrib,
                                                 TextoLeiNContrib = r.TextoLeiNContrib,
                                                 ReducaoContrib = r.ReducaoContrib.Value,
                                                 ReducaoNContrib = r.ReducaoNContrib.Value,
                                                 modBCICMS = r.modBCICMS.Value,
                                                 modBCST = r.modBCST.Value,
                                                 LucroST = r.LucroST.Value,
                                                 PercRedBCST = r.pRedBCST.Value,
                                                 AliqInterna = r.AliqInterna.Value
                                             }
                                    );

                    if (icmsInterestadual.Count() > 0)
                        regraProduto = icmsInterestadual.ToList();
                    else
                        throw new Exception(String.Format("ICMS não está configurado para {0} -> {1}.", ufOrigem, ufDestino));
                }
            }
            else
            {
                RegraImpostoProduto objRegraImpostoProduto = new RegraImpostoProduto();

                objRegraImpostoProduto.AliqContrib = pObjProduto.AliqContrib.Value;
                objRegraImpostoProduto.AliqNContrib = pObjProduto.AliqNContrib.Value;
                objRegraImpostoProduto.AliqContribNormal = pObjProduto.AliqContribNormal.Value;
                objRegraImpostoProduto.AliqNContribNormal = pObjProduto.AliqNContribNormal.Value;
                objRegraImpostoProduto.AliqSubstTributaria = pObjProduto.AliqSubstTributaria.Value;
                objRegraImpostoProduto.TextoLeiContrib = pObjProduto.TextoLeiContrib;
                objRegraImpostoProduto.TextoLeiNContrib = pObjProduto.TextoLeiNContrib;
                objRegraImpostoProduto.ReducaoContrib = pObjProduto.ReducaoContrib.Value;
                objRegraImpostoProduto.ReducaoNContrib = pObjProduto.ReducaoNContrib.Value;
                objRegraImpostoProduto.modBCICMS = pObjProduto.modBCICMS.Value;
                objRegraImpostoProduto.modBCST = pObjProduto.modBCST.Value;
                objRegraImpostoProduto.LucroST = pObjProduto.LucroST.Value;
                objRegraImpostoProduto.PercRedBCST = pObjProduto.pRedBCST.Value;
                objRegraImpostoProduto.AliqInterna = pObjProduto.AliqInterna.Value;

                regraProduto.Add(objRegraImpostoProduto);
            }

            Tributacao.TipoST empresaST = (Tributacao.TipoST)(from e in db.Filials where objFilial.ID == e.ID select e.TipoST).Single();

            imposto.percRedBCST = regraProduto[0].PercRedBCST;

            //Atribui os parametros de Imposto
            if (pContribuinte)
            {
                AtribuirParametrosCalculoImposto(ref imposto, pObjProduto.OrigemProd.ToString(), regraProduto[0].AliqContrib, regraProduto[0].AliqContribNormal, regraProduto[0].AliqSubstTributaria, regraProduto[0].TextoLeiContrib, regraProduto[0].ReducaoContrib, pObjProduto.Tributacao.Value, regraProduto[0].modBCICMS, regraProduto[0].modBCST, regraProduto[0].LucroST, pObjProduto, modeloNota, objFilial.SimplesNacional.HasValue == true ? objFilial.SimplesNacional.Value : (byte)0);
            }
            else
            {
                AtribuirParametrosCalculoImposto(ref imposto, pObjProduto.OrigemProd.ToString(), regraProduto[0].AliqNContrib, regraProduto[0].AliqNContribNormal, regraProduto[0].AliqSubstTributaria, regraProduto[0].TextoLeiNContrib, regraProduto[0].ReducaoNContrib, pObjProduto.Tributacao.Value, regraProduto[0].modBCICMS, regraProduto[0].modBCST, regraProduto[0].LucroST, pObjProduto, modeloNota, objFilial.SimplesNacional.HasValue == true ? objFilial.SimplesNacional.Value : (byte)0);
            }

            //Prepara a base de ICMS
            imposto.baseIcms = (pValor + pAcrescimo) - pDesconto;

            //Verifica se a empresa é simples nacional
            if (objFilial.SimplesNacional == 0 || objFilial.SimplesNacional == 1 || Convert.ToBoolean(pOrgaoPublico) == true)
                CalculaIcmsSimplesNacional(ref imposto, objFilial.AliqSimplesSubst.Value, pContribuinte, pValor, pAcrescimo, pDesconto, empresaST);
            else
                CalculaIcmsEmpresaNormal(ref imposto, pContribuinte, pValor, pAcrescimo, pDesconto, pObjTabelaCFOP, empresaST);

            if (objFilial.SimplesNacional == 0 && (imposto.tagCST == "101" || imposto.tagCST == "201" || imposto.tagCST == "900"))
            {
                TabelaFaixaFatSimples tabelaFaixa = (from e in db.TabelaFaixaFatSimples where objFilial.IdTabelaFaixaFatSimples == e.ID select e).Single();
                regraProduto[0].AliquotaCredSimples = tabelaFaixa.ICMS.Value;
                regraProduto[0].PercRedCredSimples = tabelaFaixa.PercReducaoBC.Value;

                CalculaCreditoSimplesNacional(ref imposto, pValor, pAcrescimo, pDesconto, regraProduto[0].AliquotaCredSimples, regraProduto[0].PercRedCredSimples);
            }

            //Calcula o PIS
            CalculaPIS(ref imposto, pValor, pAcrescimo, pDesconto);

            //Calcula o COFINS
            CalculaCofins(ref imposto, pValor, pAcrescimo, pDesconto);

            //Calcula o IPI
            CalculaIPI(ref imposto, pValor, pAcrescimo, pDesconto);

            return (imposto);
        }

        private static void CalculaCreditoSimplesNacional(ref Modelo.ImpostoNF imposto, decimal pValor, decimal pAcrescimo, decimal pDesconto, decimal aliquotaCreditoSimplesExcesso, decimal percRedCredSimples)
        {
            imposto.pCredSN_N29 = Math.Round(aliquotaCreditoSimplesExcesso * (1 - (percRedCredSimples / 100)), 2, MidpointRounding.AwayFromZero);
            imposto.vCredICMSSN_N30 = Math.Round(((pValor + pAcrescimo - pDesconto) * imposto.pCredSN_N29) / 100, 2, MidpointRounding.AwayFromZero);
        }

        private static void AtribuirParametrosCalculoImposto(ref Modelo.ImpostoNF pImposto, string pOrigemProduto, decimal pAliquotaIcms, decimal pAliquotaIcmsNormal, decimal pAliquotaSubstituicaoTributaria, string pTextoLei, decimal pRedBC_N14, int pTributacao, decimal pModBC_N13, decimal pModBCST_N18, decimal pLucroSubstituicaoTributaria, Modelo.Produto pObjProduto, ModeloNota modeloNota, byte filialSimplesNacional)
        {
            string conversaoTag = ConverterTributacao(pObjProduto.Tributacao, pObjProduto.CSOSN, modeloNota, filialSimplesNacional);
            
            string sittrib = filialSimplesNacional != 0
                                            ? (pOrigemProduto + conversaoTag)
                                            : cwkGestao.Repositorio.Util.ConversorCsts.ConverterIndiceParaCSOSN(pObjProduto.CSOSN);

            pImposto.situacaoTibutaria = sittrib;
            pImposto.orig_N11 = Convert.ToInt32(pOrigemProduto);

            pImposto.aliquotaIcms = pAliquotaIcms;
            pImposto.aliquotaIcmsNormal = pAliquotaIcmsNormal;
            pImposto.textoLei = pTextoLei == null ? "" : pTextoLei;
            pImposto.pRedBC_N14 = pRedBC_N14;

            pImposto.tagCST = conversaoTag;
            pImposto.modBC_N13 = pModBC_N13;
            pImposto.modBCST_N18 = pModBCST_N18;

            pImposto.lucroSubstituicaoTributaria = pLucroSubstituicaoTributaria;
            pImposto.aliquotaSubstituicaoTributaria = pAliquotaSubstituicaoTributaria;

            pImposto.CST_Pis = NFe.Conv_CST_Pis(pObjProduto.CST_Pis.Value);
            pImposto.pPIS_Q08 = pObjProduto.pPIS_Q08.Value;

            pImposto.CST_Cofins = NFe.Conv_CST_Cofins(pObjProduto.CST_Cofins.Value);
            pImposto.pCOFINS_S08 = pObjProduto.pCOFINS_S08.Value;// pObjProduto.CST_Cofins.Value;

            pImposto.CST_Ipi = NFe.Conv_CST_IPI(pObjProduto.CST_IPI.Value);
            pImposto.pIPI_O13 = pObjProduto.AliquotaIPI.Value;
            pImposto.cEnq_O06 = pObjProduto.ENQ_IPI.Value;

            
        }

        private static void CalculaIcmsSimplesNacional(ref Modelo.ImpostoNF pImposto, decimal pAliquotaSimples, bool pContribuinte, decimal pValor, decimal pAcrescimo, decimal pDesconto, Tributacao.TipoST empresaST)
        {
            pImposto.valorIcms = pImposto.baseIcms * pAliquotaSimples / 100;

            CalculaIcmsSubstituicaoTributaria(ref pImposto, pContribuinte, pValor, pAcrescimo, pDesconto, empresaST);

            pImposto.valorOutrosIcms = pImposto.baseIcms;
            pImposto.baseIcms = 0;
            pImposto.valorIcms = 0;
            pImposto.aliquotaIcms = 0;
            pImposto.aliquotaIcmsNormal = 0;
        }

        private static void CalculaIcmsEmpresaNormal(ref Modelo.ImpostoNF pImposto, bool pContribuinte, decimal pValor, decimal pAcrescimo, decimal pDesconto, Modelo.TabelaCFOP pObjTabelaCFOP, Tributacao.TipoST empresaST)
        {
            //Verifica se o ICMS é isento ou não
            bool icmsIsento = false;
            if (pObjTabelaCFOP.bCalcICMS == false)
            {
                icmsIsento = true;
            }
            else
            {
                string[] tagCST = new string[] { "30", "40", "41", "50", "60", "90" };

                if (tagCST.Contains(pImposto.tagCST))
                    icmsIsento = true;
                else
                    icmsIsento = false;
            }

            //Verifica se o valor de ICMS é isento
            if (icmsIsento)
            {
                string[] tagCST = new string[] { "00", "10", "20" };

                if (!tagCST.Contains(pImposto.tagCST))
                {
                    pImposto.valorIcms = pImposto.baseIcms * pImposto.aliquotaIcms / 100;

                    CalculaIcmsSubstituicaoTributaria(ref pImposto, pContribuinte, pValor, pAcrescimo, pDesconto, empresaST);
                }

                pImposto.valorIsentoIcms = pImposto.baseIcms;
                pImposto.valorOutrosIcms = 0;
                pImposto.baseIcms = 0;
                pImposto.valorIcms = 0;
                pImposto.aliquotaIcms = 0;
                pImposto.aliquotaIcmsNormal = 0;
            }
            else
            {
                //Verifica se possui redução na base de icms
                if (pImposto.pRedBC_N14 > 0)
                {
                    if (pImposto.pRedBC_N14 > 100)
                        pImposto.pRedBC_N14 = 100;

                    pImposto.valorOutrosIcms = pImposto.baseIcms;
                    pImposto.baseIcms = pImposto.baseIcms * (1 - (pImposto.pRedBC_N14 / 100));
                    pImposto.valorOutrosIcms = pImposto.valorOutrosIcms - pImposto.baseIcms;
                }
                else
                {
                    pImposto.valorOutrosIcms = 0;
                }

                //Calcula o icms
                pImposto.valorIcms = pImposto.baseIcms * pImposto.aliquotaIcms / 100;
                pImposto.diferencaReducaoIcms = (pImposto.baseIcms * pImposto.aliquotaIcmsNormal / 100) - pImposto.valorIcms;

                if (pImposto.textoLei.IndexOf("*valor") != -1)
                {
                    pImposto.textoLei = pImposto.textoLei.Replace("*valor", String.Format("{0:C2}", pImposto.diferencaReducaoIcms));
                }

                //Chama o método que calcula a substituição tributária
                CalculaIcmsSubstituicaoTributaria(ref pImposto, pContribuinte, pValor, pAcrescimo, pDesconto, empresaST);
            }
        }

        private static void CalculaPIS(ref Modelo.ImpostoNF pImposto, decimal pValor, decimal pAcrescimo, decimal pDesconto)
        {
            string[] tagCST = new string[] { "07", "08", "09", "99" };

            if (tagCST.Contains(pImposto.CST_Pis))
            {
                pImposto.vBC_Q07 = 0;
                pImposto.pPIS_Q08 = 0;
                pImposto.vPIS_Q09 = 0;
            }
            else
            {
                pImposto.vBC_Q07 = (pValor + pAcrescimo) - pDesconto;
                pImposto.vPIS_Q09 = pImposto.vBC_Q07 * pImposto.pPIS_Q08 / 100;
            }
        }

        private static void CalculaCofins(ref Modelo.ImpostoNF pImposto, decimal pValor, decimal pAcrescimo, decimal pDesconto)
        {
            string[] tagCST = new string[] { "07", "08", "09", "99" };

            if (tagCST.Contains(pImposto.CST_Cofins))
            {
                pImposto.vBC_S07 = 0;
                pImposto.pCOFINS_S08 = 0;
                pImposto.vCOFINS_S11 = 0;
            }
            else
            {
                pImposto.vBC_S07 = (pValor + pAcrescimo) - pDesconto;
                pImposto.vCOFINS_S11 = pImposto.vBC_S07 * pImposto.pCOFINS_S08 / 100;
            }
        }

        private static void CalculaIPI(ref Modelo.ImpostoNF pImposto, decimal pValor, decimal pAcrescimo, decimal pDesconto)
        {
            string[] tagCST = new string[] { "00", "49", "50", "99" };

            if (!tagCST.Contains(pImposto.CST_Ipi))
            {
                pImposto.vBC_O10 = 0;
                pImposto.pIPI_O13 = 0;
                pImposto.vIPI_O14 = 0;
            }
            else
            {
                pImposto.vBC_O10 = (pValor + pAcrescimo) - pDesconto;
                pImposto.vIPI_O14 = pImposto.vBC_O10 * pImposto.pIPI_O13 / 100;
            }
        }

        private static void CalculaIcmsSubstituicaoTributaria(ref Modelo.ImpostoNF imposto, bool pContribuinte, decimal pValor, decimal pAcrescimo, decimal pDesconto, Tributacao.TipoST empresaST)
        {
            if (pContribuinte && empresaST == Tributacao.TipoST.Substituto &&
                (imposto.tagCST == "30" ||
                 imposto.tagCST == "60" ||
                 imposto.tagCST == "10" ||
                 imposto.tagCST == "70" ||
                 imposto.tagCST == "201" ||
                 imposto.tagCST == "202" ||
                 imposto.tagCST == "203"))
            {
                imposto.baseIcmsSubst = (pValor + pAcrescimo) - pDesconto;
                imposto.baseIcmsSubst = imposto.baseIcmsSubst - (imposto.baseIcmsSubst * imposto.percRedBCST / 100);
                imposto.baseIcmsSubst = imposto.baseIcmsSubst + (imposto.baseIcmsSubst * imposto.lucroSubstituicaoTributaria / 100);

                imposto.valorRetidoIcms = imposto.baseIcmsSubst * imposto.aliquotaSubstituicaoTributaria / 100;
                imposto.valorRetidoIcms = imposto.valorRetidoIcms - imposto.valorIcms;
                //percentual de redução da BC de ST foi setada no início do método público

            }
            else
            {
                imposto.baseIcmsSubst = 0;
                imposto.valorRetidoIcms = 0;
                imposto.aliquotaSubstituicaoTributaria = 0;
            }
        }

        private static string ConverterTributacao(int? indiceCST, int? indiceCSOSN, ModeloNota modeloNota, int simplesNacional)
        {
            try
            {
                switch (modeloNota)
                {
                    case ModeloNota.Modelo1: return ConverterCST(indiceCST);
                    case ModeloNota.NFe: if (simplesNacional == 0)
                            return ConverterCSOSN(indiceCSOSN);
                        else
                            return ConverterCST(indiceCST);
                    default: throw new Exception();
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Erro na conversão do campo de tributação, verifique no cadastro deste produto os campos de tributação CST e CSOSN e a situação tributária da empresa.", exc);
            }
        }

        private static string ConverterCST(int? indiceCST)
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

        private static string ConverterCSOSN(int? indiceCSOSN)
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
    }
}
