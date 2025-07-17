using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.SubBlocos
{
    public class BC_NF_01_1B_04_55 : Bloco
    {
        public Nota nf { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }

        public Reg_C100 Nota { get; set; }
        public Reg_C110 ComplementoNF { get; set; }
        public Reg_C120 InfCompImportacao { get; set; }
        public IList<Reg_C170> NotaItens { get; set; }
        public IList<Reg_C190> RegistrosAnaliticos { get; set; }

        public override void GerarBloco()
        {
            Nota = Gerar_C100(nf);
            ComplementoNF = Gerar_C110();
            NotaItens = new List<Reg_C170>();
            NotaItens = Gerar_ItensNota(nf.NotaItems);
            RegistrosAnaliticos = Gerar_C190(nf);
            // Descomentar este trecho de código quando Operações de Importação
            // forem Suportadas pelo sistema.

            //InfCompImportacao = Gerar_C120(nf, Nota, NotaItens);

        }

        private IList<Reg_C170> Gerar_ItensNota(IList<NotaItem> itens)
        {
            IList<Reg_C170> result = new List<Reg_C170>();

            if (nf.Status != "2")
            {
                return null;
            }

            foreach (NotaItem item in itens)
            {
                Reg_C170 reg = new Reg_C170();

                reg.NUM_ITEM = item.Sequencia;
                reg.COD_ITEM = item.CodigoProduto;
                reg.DESCR_COMPL = item.ProdutoDescReduzida;
                reg.QTD = item.Quantidade;
                reg.UNID = item.Unidade;
                reg.VL_ITEM = item.Total;
                reg.VL_DESC = item.ValorDesconto;
                reg.IND_MOV = 0;
                reg.CST_ICMS = item.TAG_CST;
                reg.CFOP = item.CFOP.CFOP;
                reg.COD_NAT = item.Nota.Condicao.Codigo.ToString();
                reg.VL_BC_ICMS = Math.Round(item.BaseICMS, 2);
                reg.ALIQ_ICMS = Math.Round(item.ICMS == null ? item.AliqICMSNormal : item.ICMS.AliqContrib, 2);
                reg.VL_ICMS = Math.Round(item.ValorICMS, 2);
                reg.VL_BC_ICMS_ST = Math.Round(item.BaseICMSSubst, 2);
                reg.ALIQ_ST = Math.Round(item.ICMS == null ? item.AliqICMS : item.ICMS.AliqSubstTributaria, 2);
                reg.VL_ICMS_ST = Math.Round(item.ValorRetidoICMS, 2);
                //reg.IND_APUR = 
                //reg.COD_ENQ = 

                reg.CST_IPI = item.CST_Ipi;
                reg.VL_BC_IPI = Math.Round(item.VBC_O10, 2);
                reg.ALIQ_IPI = Math.Round(item.PIPI_O13, 2);
                reg.VL_IPI = Math.Round(item.VIPI_O14, 2);

                reg.CST_PIS = item.CST_Pis;
                reg.VL_BC_PIS = Math.Round(item.VBC_Q07, 2);
                reg.ALIQ_PIS = Math.Round(item.PPIS_Q08, 2);
                reg.VL_PIS = Math.Round(item.VPIS_Q09, 2);

                reg.CST_COFINS = item.CST_Cofins;
                reg.VL_BC_COFINS = Math.Round(item.VBC_S07, 2);
                reg.ALIQ_COFINS = Math.Round(item.PCOFINS_S08, 2);
                reg.VL_COFINS = Math.Round(item.VCOFINS_S11, 2);
                reg.COD_CTA = "";


                result.Add(reg);
            }

            return result;
        }

        private Reg_C110 Gerar_C110()
        {
            Reg_0450 txtLei = ((List<Reg_0450>)ParamsBloco["COMP_DOC"]).FirstOrDefault(f => f.NotasDoTextoLei.Contains(nf));
            if (txtLei != null && nf.Status == "2")
            {
                Reg_C110 reg = new Reg_C110();
                reg.COD_INF = txtLei.codigoDaInformacaoComplementarDoDocumento;
                reg.TXT_COMPL = txtLei.textoLivreDaInformacaoComplementarExistenteNoDocumentoFiscal;
                return reg;
            }
            return null;
        }

        private Reg_C100 Gerar_C100(Nota nf)
        {
            Reg_C100 reg = new Reg_C100();
            reg.IND_OPER = nf.Ent_Sai == InOutType.Entrada ? 0 : 1;
            reg.IND_EMIT = nf.Ent_Sai == InOutType.Saída ? 0 : 1;
            reg.COD_MOD = nf.ModeloDocto == 0 ? "55" : nf.ModeloDocto.ToString();
            switch (nf.Status)
            {
                case "2":
                    reg.COD_SIT = "00";
                    break;
                case "3":
                    reg.COD_SIT = "02";
                    break;
                case "7":
                    reg.COD_SIT = "04";
                    break;
                default:
                    throw new Exception("Situação de nota não permitida para o SPED EFD-Contribuições. Código da Nota: " + nf.Codigo);
            }
            reg.SER = nf.Serie;
            reg.NUM_DOC = nf.Numero;
            reg.CHV_NFE = nf.ChaveNota;
            if (reg.COD_SIT == "00")
            {
                if (ParamsBloco.ContainsKey("COD_PART"))
                {
                    Reg_0150 part = ((IList<Reg_0150>)ParamsBloco["COD_PART"])
                        .FirstOrDefault(
                        f => GetStringSomenteAlfanumerico(f.cnpjDoParticipante) == GetStringSomenteAlfanumerico(nf.Pessoa.CNPJ_CPF) ||
                             GetStringSomenteAlfanumerico(f.cpfDoParticipante) == GetStringSomenteAlfanumerico(nf.Pessoa.CNPJ_CPF));
                    reg.COD_PART = part.codigoDeIdentificacaoDoParticipante.ToString();
                }
                reg.DT_DOC = nf.Dt;
                reg.DT_E_S = nf.Dt;
                reg.VL_DOC = Math.Round(nf.TotalNota, 2);

                if (nf.Documentos != null)
                {
                    if (nf.Documentos.Count > 0)
                    {
                        reg.IND_PGTO = nf.Condicao.CondicaoParcelas.Where(w => w.BAVista == false).Count() > 0 ? 1 : 0;
                    }
                    else
                    {
                        reg.IND_PGTO = 2;
                    }
                }

                reg.VL_DESC = Math.Round(nf.ValorDesconto, 2);
                reg.VL_ABAT_NT = decimal.Zero;
                reg.VL_MERC = Math.Round(nf.TotalProduto, 2);

                switch (nf.TipoFrete)
                {
                    case 0:
                        reg.IND_FRT = 0;
                        break;
                    case 1:
                        reg.IND_FRT = 1;
                        break;
                    case 2:
                        reg.IND_FRT = 2;
                        break;
                    case 9:
                        reg.IND_FRT = 9;
                        break;
                    default:
                        break;
                }

                reg.VL_FRT = Math.Round(nf.ValorFrete, 2);
                reg.VL_SEG = Math.Round(nf.ValorSeguro, 2);
                reg.VL_OUT_DA = Math.Round(nf.ValorAcrescimo, 2);
                if (nf.NotaICMSs != null)
                {
                    reg.VL_BC_ICMS = Math.Round(nf.NotaICMSs.Sum(s => s.BaseICMS), 2);
                    reg.VL_ICMS = Math.Round(nf.NotaICMSs.Sum(s => s.ValorICMS), 2);
                    reg.VL_BC_ICMS_ST = Math.Round(nf.NotaICMSs.Sum(s => s.BaseSubst), 2);
                    reg.VL_ICMS_ST = Math.Round(nf.NotaICMSs.Sum(s => s.ValorSubst), 2);
                    reg.VL_IPI = Math.Round(nf.NotaICMSs.Sum(s => s.ValorIPI), 2);
                    reg.VL_PIS = Math.Round(nf.NotaICMSs.Sum(s => s.ValorPIS), 2);
                    reg.VL_COFINS = Math.Round(nf.NotaICMSs.Sum(s => s.ValorCOFINS), 2);
                    //Pesquisar situação onde ocorre PIS/COFINS retido por ST
                    reg.VL_PIS_ST = Decimal.Zero;
                    reg.VL_COFINS_ST = Decimal.Zero;
                }
            }

            return reg;
        }

        private IList<Reg_C190> Gerar_C190(Nota nf)
        {
            IList<Reg_C190> regs = new List<Reg_C190>();
            foreach (var item in nf.NotaItems)
            {
                if (!String.IsNullOrEmpty(item.SitTrib))
                {
                    if (item.SitTrib.Length <= 2)
                    {
                        if (item.TAG_CST.Length == 3)
                        {
                            item.SitTrib = item.TAG_CST;
                        }
                        else
                        {
                            item.SitTrib = item.orig_N11.ToString() + item.TAG_CST;
                        }
                    }
                }
                else
                {
                    if (item.TAG_CST.Length == 3)
                    {
                        item.SitTrib = item.TAG_CST;
                    }
                    else
                    {
                        item.SitTrib = item.orig_N11.ToString() + item.TAG_CST;
                    }
                }
                
            }
            var grp = nf.NotaItems.GroupBy(g => new { cfop = g.CFOP.CFOP, cst = g.SitTrib, g.AliqICMS });
            foreach (var item in grp)
            {
                Reg_C190 reg = new Reg_C190();
                reg.CST_ICMS = item.Key.cst;
                reg.CFOP = item.Key.cfop;
                reg.ALIQ_ICMS = item.Key.AliqICMS;
                reg.VL_OPR = nf.TotalNota;
                reg.VL_BC_ICMS = Math.Round(item.Sum(s => s.BaseICMS), 2);
                reg.VL_ICMS = Math.Round(item.Sum(s => s.ValorICMS), 2);
                reg.VL_BC_ICMS_ST = Math.Round(item.Sum(s => s.BaseICMSSubst), 2);
                reg.VL_ICMS_ST = Math.Round(item.Sum(s => s.ValorRetidoICMS), 2);
                reg.VL_RED_BC = Math.Round(item.Sum(s => s.ValorICMSReducao), 2);
                reg.VL_IPI = Math.Round(item.Sum(s => s.VIPI_O14), 2);
                reg.COD_OBS = "";
                regs.Add(reg);
            }
            return regs;
        }

        public BC_NF_01_1B_04_55(Nota nota, DateTime start, DateTime end)
        {
            ParamsBloco = new Dictionary<string, object>();
            nf = nota;
            inicio = start;
            fim = end;
        }
    }
}
