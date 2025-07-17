using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoD;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_D : Bloco
    {
        public Reg_D001 Abertura { get; set; }
        public IList<Reg_D010> Participantes { get; set; }
        public IList<BD_Nf_Transportes> NotasTransp { get; set; }
        public IList<BD_Nf_Telecom> NotasTelecom { get; set; }
        public Reg_D990 Fechamento { get; set; }

        public IList<Nota> nfs { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }

        public Bloco_D(IList<Nota> ns, DateTime start, DateTime end)
        {
            ParamsBloco = new Dictionary<string, object>();
            IList<Nota> temp = ns.Where(w => 
                w.TipoDeEntrada == (int)TipoNota.TipoEntrada.Transporte ||
                w.TipoDeEntrada == (int)TipoNota.TipoEntrada.Telecomunicacoes
                ).ToList();
            inicio = start;
            fim = end;
            nfs = temp;
        }

        public override void GerarBloco()
        {
            Abertura = new Reg_D001();
            Abertura.IND_MOV = nfs.Count > 0 ? 0 : 1;
            if (nfs.Count > 0)
            {
                Participantes = GerarParticipantes(nfs);
                NotasTransp = GerarNotasTransp(nfs);
                NotasTelecom = GerarNotasTelecom(nfs);
            }
            Fechamento = new Reg_D990();
            Fechamento.QTD_LIN_D = GetContagemRegistros();

        }

        //bloco_D.ParamsBloco.Add("COD_PART", bloco_0.Participantes);
        //bloco_D.ParamsBloco.Add("COMP_DOC", bloco_0.TextosLei);

        private IList<BD_Nf_Telecom> GerarNotasTelecom(IList<Nota> nfs)
        {
            try
            {
                IList<BD_Nf_Telecom> result = new List<BD_Nf_Telecom>();
                IList<Nota> temp = nfs.Where(w =>
                w.TipoDeEntrada == (int)TipoNota.TipoEntrada.Telecomunicacoes).ToList();

                if (temp != null)
                {
                    foreach (Nota nf in temp)
                    {
                        BD_Nf_Telecom bloco = new BD_Nf_Telecom(nf);
                        if (ParamsBloco.ContainsKey("COD_PART"))
                        {
                            bloco.ParamsBloco.Add("COD_PART", ParamsBloco["COD_PART"]);
                        }
                        if (ParamsBloco.ContainsKey("COMP_DOC"))
                        {
                            bloco.ParamsBloco.Add("COMP_DOC", ParamsBloco["COMP_DOC"]);
                        }
                        bloco.GerarBloco();
                        result.Add(bloco);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                string s = e.Message.ToString();
                return null;
            }
        }

        private IList<BD_Nf_Transportes> GerarNotasTransp(IList<Nota> nfs)
        {
            try
            {
                IList<BD_Nf_Transportes> result = new List<BD_Nf_Transportes>();
                IList<Nota> temp = nfs.Where(w =>
                w.TipoDeEntrada == (int)TipoNota.TipoEntrada.Transporte).ToList();

                if (temp != null)
                {
                    foreach (Nota nf in temp)
                    {
                        BD_Nf_Transportes bloco = new BD_Nf_Transportes(nf);
                        if (ParamsBloco.ContainsKey("COD_PART"))
                        {
                            bloco.ParamsBloco.Add("COD_PART", ParamsBloco["COD_PART"]);
                        }
                        if (ParamsBloco.ContainsKey("COMP_DOC"))
                        {
                            bloco.ParamsBloco.Add("COMP_DOC", ParamsBloco["COMP_DOC"]);
                        }
                        bloco.GerarBloco();
                        result.Add(bloco);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                string s = e.Message.ToString();
                return null;
            }
        }

        private IList<Reg_D010> GerarParticipantes(IList<Nota> nfs)
        {
            IList<Reg_D010> result = new List<Reg_D010>();

            var gFilial = nfs.GroupBy(g => g.Filial.CNPJ);
            foreach (var item in gFilial)
            {
                Reg_D010 reg = new Reg_D010();
                reg.CNPJ = item.Key;
                result.Add(reg);
            }

            return result;
        }
    }
}