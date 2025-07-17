using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.SubBlocos
{
    public class BC_NF_Servicos : Bloco
    {
        public Reg_C500 Nota { get; set; }

        public Nota nf { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }


        public override void GerarBloco()
        {
            Nota = GerarNota(nf, inicio, fim);
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
            reg.IND_OPER = 0; //somente notas de entrada de energia, por enquanto.
            reg.IND_EMIT = 1; //somente notas emitidas por terceiros, por enquanto.
            reg.COD_SIT = nf.DtHoraCanc.HasValue ? "02" : "00";
            reg.SER = nf.Serie;
            // reg.SUB = 1; //gestão não trata subserie
            reg.COD_CONS = ""; //parâmetro opcional na nota de entrada;
            reg.NUM_DOC = nf.Numero;
            reg.DT_DOC = nf.Dt;
            reg.DT_E_S = nf.IncData.HasValue ? nf.IncData.Value : nf.Dt;
            reg.VL_DOC = nf.TotalNota;
            reg.VL_DESC = nf.ValorDesconto;
            reg.VL_FORN = nf.NotaItems.Sum(s => s.Valor);
            reg.VL_SERV_NT = 0;
            reg.VL_TERC = 0;
            reg.VL_DA = nf.ValorAcrescimo;

            reg.VL_BC_ICMS = Math.Round(nf.NotaICMSs.Sum(s => s.BaseICMS), 2);
            reg.VL_ICMS = Math.Round(nf.NotaICMSs.Sum(s => s.ValorICMS), 2);
            reg.VL_BC_ICMS_ST = Math.Round(nf.NotaICMSs.Sum(s => s.BaseSubst), 2);
            reg.VL_ICMS_ST = Math.Round(nf.NotaICMSs.Sum(s => s.ValorSubst), 2);

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
