using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_NF_Servicos : Bloco
    {
        public Reg_C500 Nota { get; set; }
        public IList<Reg_C501> ComplementoOpPisPasep { get; set; }
        public IList<Reg_C505> ComplementoOpCofins { get; set; }

        public Nota nf { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }


        public override void GerarBloco()
        {
            Nota = GerarNota(nf, inicio, fim);
            ComplementoOpPisPasep = GerarComplementoOperacaoPis(nf, inicio, fim);
            ComplementoOpCofins = GerarComplementoOperacaoCofins(nf, inicio, fim);
        }

        private IList<Reg_C505> GerarComplementoOperacaoCofins(Nota nf, DateTime inicio, DateTime fim)
        {
            IList<Reg_C505> regs = new List<Reg_C505>();


            foreach (NotaItem item in nf.NotaItems)
            {
                Reg_C505 reg = new Reg_C505();
                reg.CST_COFINS = item.ICMS == null ? item.CST_Cofins : item.ICMS.CST_Cofins.ToString();
                reg.VL_ITEM = item.Total;
                reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred != null ? nf.TipoNota.Operacao.NatBcCred.CodCST : "";
                reg.VL_BC_COFINS = item.VBC_S07;
                reg.ALIQ_COFINS = item.PCOFINS_S08;
                reg.VL_COFINS = item.VCOFINS_S11;
                reg.COD_CTA = "";

                regs.Add(reg);
            }

            return regs;
        }

        private IList<Reg_C501> GerarComplementoOperacaoPis(Nota nf, DateTime inicio, DateTime fim)
        {
            IList<Reg_C501> regs = new List<Reg_C501>();


            foreach (NotaItem item in nf.NotaItems)
            {
                Reg_C501 reg = new Reg_C501();
                reg.CST_PIS = item.ICMS == null ? item.CST_Pis : item.ICMS.CST_Pis.ToString();
                reg.VL_ITEM = item.Total;
                reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred != null ? nf.TipoNota.Operacao.NatBcCred.CodCST : "";
                reg.VL_BC_PIS = item.VBC_Q07;
                reg.ALIQ_PIS = item.PPIS_Q08;
                reg.VL_PIS = item.VPIS_Q09;
                reg.COD_CTA = "";

                regs.Add(reg);
            }

            return regs;
        }

        private Reg_C500 GerarNota(Nota nf, DateTime inicio, DateTime fim)
        {
            Reg_C500 reg = new Reg_C500();

            if (ParamsBloco.ContainsKey("COD_PART"))
            {
                IList<Reg_0150> conv = ((IList<Reg_0150>)ParamsBloco["COD_PART"]);
                conv = conv.Where(f => GetStringSomenteAlfanumerico(f.cnpjDoParticipante) == GetStringSomenteAlfanumerico(nf.Pessoa.CNPJ_CPF) ||
                                       GetStringSomenteAlfanumerico(f.cpfDoParticipante) == GetStringSomenteAlfanumerico(nf.Pessoa.CNPJ_CPF)).ToList();
                Reg_0150 part = conv.FirstOrDefault();
                reg.COD_PART = part.codigoDeIdentificacaoDoParticipante.ToString();
            }

            switch (nf.TipoDeEntrada)
            {
                case (int)TipoNota.TipoEntrada.InsumoEnergia:
                    reg.COD_MOD = "06";
                    break;
                case (int)TipoNota.TipoEntrada.InsumoAgua:
                    reg.COD_MOD = "29";
                    break;
                case (int)TipoNota.TipoEntrada.InsumoGas:
                    reg.COD_MOD = "28";
                    break;
                default:
                    reg.COD_MOD = "55";
                    break;
            }

            reg.COD_SIT = nf.DtHoraCanc.HasValue ? "02" : "00";

            reg.SER = nf.Serie;
            // reg.SUB = 1; //gestão não trata subserie
            reg.NUM_DOC = nf.Numero;
            reg.DT_DOC = nf.Dt;
            reg.DT_ENT = nf.IncData.HasValue ? nf.IncData.Value : nf.Dt;
            reg.VL_DOC = nf.TotalNota;
            reg.VL_ICMS = nf.NotaICMSs.Sum(s => s.ValorICMS);


            Reg_0450 txtLei = ((List<Reg_0450>)ParamsBloco["COMP_DOC"]).FirstOrDefault(f => f.NotasDoTextoLei.Contains(nf));
            if (txtLei != null)
            {
                reg.COD_INF = txtLei.codigoDaInformacaoComplementarDoDocumento;
            }

            reg.VL_PIS = nf.NotaICMSs.Sum(s => s.ValorPIS);
            reg.VL_COFINS = nf.NotaICMSs.Sum(s => s.ValorCOFINS);

            return reg;
        }

        public BC_NF_Servicos(Nota n, DateTime start, DateTime end)
        {
            ParamsBloco = new Dictionary<string, object>();
            nf = n;
            inicio = start;
            fim = end;
        }
    }
}
