using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoD;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BD_Nf_Transportes : Bloco
    {
        public Nota nf { get; set; }

        public Reg_D100 nota { get; set; }
        public IList<Reg_D101> CompDocTransPis { get; set; }
        public IList<Reg_D105> CompDocTransCofins { get; set; }

        public BD_Nf_Transportes(Nota n)
        {
            ParamsBloco = new Dictionary<string, object>();
            nf = n;
        }
        public override void GerarBloco()
        {
            try
            {
                nota = GerarD100(nf);
                CompDocTransPis = GerarD101(nf);
                CompDocTransCofins = GerarD105(nf);
            }
            catch (Exception e)
            {
                nota = null;
                CompDocTransPis = null;
                CompDocTransCofins = null;
                throw e; 
            }
            
        }

        private IList<Reg_D105> GerarD105(Nota nf)
        {
            IList<Reg_D105> result = new List<Reg_D105>();

            foreach (NotaItem item in nf.NotaItems)
            {
                Reg_D105 reg = new Reg_D105();
                reg.IND_NAT_FRT = AnalisaNaturezaFrete((int)nf.TipoFrete, nf.Ent_Sai, item.CST_Cofins);
                reg.VL_ITEM = item.Total;
                reg.CST_COFINS = item.CST_Cofins;

                if (nf.TipoNota.Operacao.NatBcCred != null)
                {
                    int cstcofins = Convert.ToInt32(reg.CST_COFINS);
                    if ((cstcofins >= 50 && cstcofins <= 67))
                    {
                        reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred.CodCST;
                    }
                }
                reg.VL_BC_COFINS = item.VBC_Q07;
                reg.ALIQ_COFINS = item.PPIS_Q08;
                reg.VL_COFINS = item.VPIS_Q09;
                result.Add(reg);
            }

            var impostos = nf.NotaImpostoServicos.Where(w => w.Imposto.Nome.ToLower().Contains("cofins")).ToList();
            if (impostos != null)
            {
                foreach (NotaImpostoServico item in impostos)
                {
                    Reg_D105 reg = new Reg_D105();
                    if (item.Imposto.CSTImposto == null)
                    {
                        throw new Exception("O serviço indicado na nota " + item.Nota.Codigo.ToString() + " não possui CST indicada. Verifique.");
                    }
                    reg.IND_NAT_FRT = AnalisaNaturezaFrete((int)nf.TipoFrete, nf.Ent_Sai, item.Imposto.CSTImposto.CodCST);
                    reg.VL_ITEM = item.Valor;
                    reg.CST_COFINS = item.Imposto.CSTImposto.CodCST;

                    if (nf.TipoNota.Operacao.NatBcCred != null)
                    {
                        int cstpis = Convert.ToInt32(reg.CST_COFINS);
                        if ((cstpis >= 50 && cstpis <= 67))
                        {
                            reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred.CodCST;
                        }
                    }
                    reg.VL_BC_COFINS = item.BaseCalculo;
                    reg.ALIQ_COFINS = item.Aliquota;
                    reg.VL_COFINS = item.Valor;
                    result.Add(reg);
                }
            }

            return result;
        }

        private IList<Reg_D101> GerarD101(Nota nf)
        {
            IList<Reg_D101> result = new List<Reg_D101>();

            foreach (NotaItem item in nf.NotaItems)
            {
                Reg_D101 reg = new Reg_D101();
                reg.IND_NAT_FRT = AnalisaNaturezaFrete((int)nf.TipoFrete, nf.Ent_Sai, item.CST_Pis);
                reg.VL_ITEM = item.Total;
                reg.CST_PIS = item.CST_Pis;

                if (nf.TipoNota.Operacao.NatBcCred != null)
                {
                    int cstpis = Convert.ToInt32(reg.CST_PIS);
                    if ((cstpis >= 50 && cstpis <= 67))//csts geradores de crédito
                    {
                        reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred.CodCST;
                    }
                }
                reg.VL_BC_PIS = item.VBC_Q07;
                reg.ALIQ_PIS = item.PPIS_Q08;
                reg.VL_PIS = item.VPIS_Q09;
            }

            var impostos = nf.NotaImpostoServicos.Where(w => w.Imposto.Nome.ToLower().Contains("cofins")).ToList();
            if (impostos != null)
            {
                foreach (NotaImpostoServico item in impostos)
                {
                    Reg_D101 reg = new Reg_D101();
                    if (item.Imposto.CSTImposto == null)
                    {
                        throw new Exception("O serviço indicado na nota " + item.Nota.Codigo.ToString() + " não possui CST indicada. Verifique.");
                    }
                    reg.IND_NAT_FRT = AnalisaNaturezaFrete((int)nf.TipoFrete, nf.Ent_Sai, item.Imposto.CSTImposto.CodCST);
                    reg.VL_ITEM = item.Valor;
                    reg.CST_PIS = item.Imposto.CSTImposto.CodCST;

                    if (nf.TipoNota.Operacao.NatBcCred != null)
                    {
                        int cstpis = Convert.ToInt32(reg.CST_PIS);
                        if ((cstpis >= 50 && cstpis <= 67))
                        {
                            reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred.CodCST;
                        }
                    }
                    reg.VL_BC_PIS = item.BaseCalculo;
                    reg.ALIQ_PIS = item.Aliquota;
                    reg.VL_PIS = item.Valor;
                    result.Add(reg);
                }
            }
            return result;
        }

        private int AnalisaNaturezaFrete(int tipoFrete, InOutType inOutType, string CST)
        {
            int natFrete = 0;
            int cst = Convert.ToInt32(CST);
            if (inOutType == InOutType.Saída)
            {
                switch (tipoFrete)
                {
                    case 0:
                        natFrete = 0;
                        break;
                    case 1:
                        natFrete = 1;
                        break;
                    case 2:
                        natFrete = 9;
                        break;
                    case 9:
                        natFrete = 9;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if ((cst >= 50 && cst <= 67))//csts geradores de crédito
                {
                    natFrete = 2;
                }
                else
                {
                    natFrete = 3;
                }
            }
            return natFrete;
        }

        private Reg_D100 GerarD100(Nota nf)
        {
            Reg_D100 reg = new Reg_D100();
            reg.IND_OPER = "0";
            reg.IND_EMIT = nf.TipoNota.NFOrigem == 0 ? "1" : "0";

            if (ParamsBloco.ContainsKey("COD_PART"))
            {
                Reg_0150 part = ((IList<Reg_0150>)ParamsBloco["COD_PART"])
                    .FirstOrDefault(f => 
                        (GetStringSomenteAlfanumerico(f.cnpjDoParticipante)) ==
                        (GetStringSomenteAlfanumerico(nf.Pessoa.CNPJ_CPF)));
                reg.COD_PART = part.codigoDeIdentificacaoDoParticipante.ToString();
                if (part == null)
                {
                    throw new Exception("A pessoa " + nf.Pessoa.Nome + " CNPJ/CPF " + nf.Pessoa.CNPJ_CPF
                        + "não consta dentre os participantes. As notas de entrada devem ser registradas "
                        + "em nome de empresa (PJ). Verifique.");
                }
            }

            reg.COD_MOD = "57";

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
            }

            reg.SER = nf.Serie;
            reg.SUB = "";
            reg.NUM_DOC = nf.Numero;

            if (reg.IND_EMIT.Equals("0"))
            {
                reg.CHV_CTE = nf.ChaveCTe;
            }

            reg.DT_DOC = nf.Dt;
            reg.DT_A_P = nf.Dt;
            reg.TP_CTe = nf.TipoCTe;
            reg.CHV_CTE_REF = "";
            reg.VL_DOC = nf.TotalNota;
            reg.VL_DESC = nf.ValorDesconto;
            reg.IND_FRT = nf.TipoFrete.ToString();
            reg.VL_SERV = nf.NotaItems.Sum(s => s.Total);
            reg.VL_BC_ICMS = nf.NotaItems.Sum(s => s.BaseICMS);
            reg.VL_ICMS = nf.NotaItems.Sum(s => s.ValorICMS);

            Reg_0450 txtLei = ((List<Reg_0450>)ParamsBloco["COMP_DOC"]).FirstOrDefault(f => f.NotasDoTextoLei.Contains(nf));
            if (txtLei != null)
            {
                reg.COD_INF = txtLei.codigoDaInformacaoComplementarDoDocumento;
            }
            reg.COD_CTA = "";

            return reg;
        }
    }
}
