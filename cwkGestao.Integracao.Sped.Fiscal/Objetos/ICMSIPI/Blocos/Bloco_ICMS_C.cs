using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.SubBlocos;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Blocos
{
    public class Bloco_ICMS_C : Bloco
    {
        public Reg_C001 Abertura { get; set; }
        public IList<BC_NF_01_1B_04_55> NFs { get; set; }
        public IList<BC_NF_Servicos> NFsEnergiaAgua { get; set; }
        public Reg_C990 Encerramento { get; set; }

        public IList<Nota> nfs { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }

        public Bloco_ICMS_C(IList<Nota> notas, DateTime start, DateTime end)
        {
            ParamsBloco = new Dictionary<string, object>();
            //somente notas que foram enviadas/canceladas/denegadas/etc. não-enviadas não entram.
            nfs = notas.Where(w =>
                ((w.StatusLegivel != "Não enviada" && w.TipoNota.NFOrigem == 1) ^ (w.TipoNota.NFOrigem == 0)) &&
                w.ModeloDocto != 0 &&
                w.TipoNota.ModeloDocto != TipoNota.ModeloDocumento.Balanco &&
                w.TipoNota.ModeloDocto != TipoNota.ModeloDocumento.CupomFiscal &&
                w.TipoNota.ModeloDocto != TipoNota.ModeloDocumento.NFSe &&
                w.TipoNota.ModeloDocto != TipoNota.ModeloDocumento.RPS).ToList();
            inicio = start;
            fim = end;
        }

        private IList<BC_NF_01_1B_04_55> GerarNfsNormais(IList<Nota> nfs, DateTime inicio, DateTime fim)
        {
            IList<Nota> temp = nfs.Where(w =>
                w.TipoNota.TipoDeEntrada != TipoNota.TipoEntrada.InsumoAgua &&
                w.TipoNota.TipoDeEntrada != TipoNota.TipoEntrada.InsumoEnergia &&
                w.TipoNota.TipoDeEntrada != TipoNota.TipoEntrada.InsumoGas &&
                w.ModeloDocto != 0
                ).ToList();
            IList<BC_NF_01_1B_04_55> result = new List<BC_NF_01_1B_04_55>();
            foreach (Nota nota in temp)
            {
                BC_NF_01_1B_04_55 nf = new BC_NF_01_1B_04_55(nota, inicio, fim);
                nf.ParamsBloco.Add("COMP_DOC", ParamsBloco["COMP_DOC"]);
                nf.ParamsBloco.Add("COD_PART", ParamsBloco["COD_PART"]);
                nf.GerarBloco();
                result.Add(nf);
            }
            return result;
        }

        public override void GerarBloco()
        {
            Abertura = new Reg_C001();
            Abertura.IND_MOV = nfs.Count > 0 ? 0 : 1;
            if (Abertura.IND_MOV == 0)
            {
                NFs = GerarNfsNormais(nfs, inicio, fim);
                NFsEnergiaAgua = GerarNFsServicos(nfs, inicio, fim);
            }
            Encerramento = new Reg_C990();
            Encerramento.QTD_LIN_C = GetContagemRegistros();
        }

        private IList<BC_NF_Servicos> GerarNFsServicos(IList<Nota> nfs, DateTime inicio, DateTime fim)
        {
            IList<BC_NF_Servicos> result = new List<BC_NF_Servicos>();

            IList<Nota> temp = nfs.Where(w => w.ModeloDocto != 0 &&
                (w.TipoDeEntrada == (int)TipoNota.TipoEntrada.InsumoAgua ||
                 w.TipoDeEntrada == (int)TipoNota.TipoEntrada.InsumoEnergia ||
                 w.TipoDeEntrada == (int)TipoNota.TipoEntrada.InsumoGas)).ToList();

            foreach (Nota n in temp)
            {
                BC_NF_Servicos nota = new BC_NF_Servicos(n, inicio, fim);
                nota.ParamsBloco.Add("COD_PART", ParamsBloco["COD_PART"]);
                nota.ParamsBloco.Add("COMP_DOC", ParamsBloco["COMP_DOC"]);
                nota.GerarBloco();
                result.Add(nota);
            }

            return result;
        }
    }
}
