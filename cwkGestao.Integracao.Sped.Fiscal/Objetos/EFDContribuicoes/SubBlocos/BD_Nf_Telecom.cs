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
    public class BD_Nf_Telecom : Bloco
    {
        public Nota nf { get; set; }

        public Reg_D500 nota { get; set; }
        public IList<Reg_D501> CompDocTelcoPis { get; set; }
        public IList<Reg_D505> CompDocTelcoCofins { get; set; }

        public BD_Nf_Telecom(Nota n)
        {
            ParamsBloco = new Dictionary<string, object>();
            nf = n;
        }
        public override void GerarBloco()
        {
            try
            {
                nota = GerarD500(nf);
                CompDocTelcoPis = GerarD501(nf);
                CompDocTelcoCofins = GerarD505(nf);
            }
            catch (Exception e)
            {
                nota = null;
                CompDocTelcoPis = null;
                CompDocTelcoCofins = null;
                throw e;
            }
        }

        private IList<Reg_D505> GerarD505(Nota nf)
        {
            IList<Reg_D505> result = new List<Reg_D505>();
            foreach (NotaItem item in nf.NotaItems)
            {
                Reg_D505 reg = new Reg_D505();
                reg.CST_COFINS = item.CST_Pis;
                reg.VL_ITEM = item.Total;
                if (nf.TipoNota.Operacao.NatBcCred != null)
                {
                    int cstpis = Convert.ToInt32(reg.CST_COFINS);
                    if ((cstpis >= 50 && cstpis <= 67))//csts geradores de crédito
                    {
                        reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred.CodCST;
                    }
                }

                reg.VL_BC_COFINS = item.VBC_Q07;
                reg.ALIQ_COFINS = item.PPIS_Q08;
                reg.VL_COFINS = item.VPIS_Q09;
                reg.COD_CTA = "";

                result.Add(reg);
            }
            var impostos = nf.NotaImpostoServicos.Where(w => w.Imposto.Nome.ToLower().Contains("cofins")).ToList();
            if (impostos != null)
            {
                foreach (NotaImpostoServico item in impostos)
                {
                    Reg_D505 reg = new Reg_D505();
                    if (item.Imposto.CSTImposto == null)
                    {
                        throw new Exception("O serviço indicado na nota " + item.Nota.Codigo.ToString() + " não possui CST indicada. Verifique.");
                    }
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
                    reg.VL_COFINS= item.Valor;
                    reg.COD_CTA = "";
                    result.Add(reg);
                }
            }
            return result;
        }

        private IList<Reg_D501> GerarD501(Nota nf)
        {
            IList<Reg_D501> result = new List<Reg_D501>();
            foreach (NotaItem item in nf.NotaItems)
            {
                Reg_D501 reg = new Reg_D501();
                reg.CST_PIS = item.CST_Pis;
                reg.VL_ITEM = item.Total;

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
                reg.COD_CTA = "";

                result.Add(reg);
            }

            var impostos = nf.NotaImpostoServicos.Where(w => w.Imposto.Nome.ToLower().Contains("cofins")).ToList();
            if (impostos != null)
            {
                foreach (NotaImpostoServico item in impostos)
                {
                    Reg_D501 reg = new Reg_D501();
                    if (item.Imposto.CSTImposto == null)
                    {
                        throw new Exception("O serviço indicado na nota " + item.Nota.Codigo.ToString() + " não possui CST indicada. Verifique.");
                    }
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
                    reg.COD_CTA = "";
                    result.Add(reg);
                }
            }
            return result;
        }

        private Reg_D500 GerarD500(Nota nf)
        {
            Reg_D500 reg = new Reg_D500();
            reg.IND_OPER = "0";
            reg.IND_EMIT = nf.TipoNota.NFOrigem == 0 ? "1" : "0";

            if (ParamsBloco.ContainsKey("COD_PART"))
            {
                Reg_0150 part = ((IList<Reg_0150>)ParamsBloco["COD_PART"])
                    .FirstOrDefault(f =>
                        (GetStringSomenteAlfanumerico(f.cnpjDoParticipante)) ==
                        (GetStringSomenteAlfanumerico(nf.Pessoa.CNPJ_CPF)));
                if (part == null)
                {
                    throw new Exception("A pessoa " + nf.Pessoa.Nome + " CNPJ/CPF " + nf.Pessoa.CNPJ_CPF 
                        + "não consta dentre os participantes. As notas de entrada devem ser registradas "
                        + "em nome de empresa (PJ). Verifique.");
                }
                reg.COD_PART = part.codigoDeIdentificacaoDoParticipante.ToString();
            }

            reg.COD_MOD = "22";

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
            reg.DT_DOC = nf.Dt;
            reg.DT_A_P = nf.Dt;
            reg.VL_DOC = nf.TotalNota;
            reg.VL_DESC = nf.ValorDesconto;
            reg.VL_SERV = nf.NotaItems.Sum(s => s.Total);
            reg.VL_SERV_NT = nf.NotaItems.Sum(s => s.Total);
            reg.VL_TERC = 0;
            reg.VL_DA = 0;
            reg.VL_BC_ICMS = nf.NotaItems.Sum(s => s.BaseICMS);
            reg.VL_ICMS = nf.NotaItems.Sum(s => s.ValorICMS);

            Reg_0450 txtLei = ((List<Reg_0450>)ParamsBloco["COMP_DOC"]).FirstOrDefault(f => f.NotasDoTextoLei.Contains(nf));
            if (txtLei != null)
            {
                reg.COD_INF = txtLei.codigoDaInformacaoComplementarDoDocumento;
            }
            reg.VL_PIS = 0;
            reg.VL_COFINS = 0;

            return reg;
        }
    }
}
