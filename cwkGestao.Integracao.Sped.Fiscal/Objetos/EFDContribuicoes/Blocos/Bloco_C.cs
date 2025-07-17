using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_C : Bloco
    {
        public IList<Nota> nfs { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }

        public Reg_C001 Header { get; set; }
        public IList<Reg_C010> IdentEstabelecimentos { get; set; }
        public IList<BC_NF_01_1B_04_55> NfsNormais { get; set; } //C100 E FILHOS

        #region Futuras implementações(LEIA PRIMEIRO)

        // OS REGISTROS À SEREM IMPLEMENTADOS FUTURAMENTE DEVEM SER COLOCADOS EM ORDEM
        // DENTRO DAS CLASSES. EXEMPLO: O SUB-BLOCO DO REGISTRO C600 E FILHOS DEVE 
        // OBRIGATORIAMENTE SER DECLARADO APÓS O SUB-BLOCO DO REGISTRO C500 E FILHOS,
        // ASSIM COMO ESTÁ NO MANUAL DO SPED EFD-CONTRIBUIÇÕES.

        // AS REGRAS DE GERAÇÃO DO BLOCO DEVEM PREFERENCIALMENTE SEREM COLOCADAS NO MÉTODO
        // "GerarBloco()" DE CADA SUB-BLOCO.
        
        // Implementar futuramente os sub-blocos comentados abaixo, para permitir
        // a escrituração de notas por consolidação

        //public IList<BC_ConsolidacaoNf55> ConsolidacaoNFs55 { get; set; } //C180 E FILHOS
        //public IList<BC_ConsolidacaoNf55Cred> ConsolidacaoNfs55Cred { get; set; } //C190 E FILHOS
        //public IList<BC_NF_02_NfVendaConsumidor> ConsolidacaoNFsVendaConsumidor { get; set; } //C380 E FILHOS

        // implementar futuramente para escrituração de ECF

        //public IList<BC_NFC_02_2D_2E_59> NfConsumidorEntComCredito { get; set; } //C395 E FILHOS
        //public IList<BC_ECF> SubBlocoECF { get; set; } //C400 E FILHOS
        //public IList<BC_ECFConsolidacao> SubBlocoECFCOnsolidacao { get; set; } //C490 E FILHOS

        //Consolidação de NFs de Agua/energia/gás usados como insumo de produção/prestação de serviços

        //public IList<BC_Consolidacao_NF_Servicos> ConsolidacaoNfSvc { get; set; } //C600 E FILHOS

        #endregion

        public IList<BC_NF_Servicos> SvcAguaEnergiaGas { get; set; } //C500 E FILHOS
        public Reg_C990 Fechamento { get; set; }

        public Bloco_C(IList<Nota> notas, DateTime start, DateTime end)
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

        private IList<Reg_C010> GerarC010(IList<Nota> nfs)
        {
            IList<Reg_C010> result = new List<Reg_C010>();

            var gcliente = nfs.GroupBy(t => t.Pessoa.CNPJ_CPF);

            //foreach (var item in gcliente)
            //{
            //    Reg_C010 reg = new Reg_C010();

            //    reg.IND_ESCRI = 2;
            //    reg.CNPJ = item.Key;
            //    result.Add(reg);
            //}

            Reg_C010 filial = new Reg_C010()
            {
                CNPJ = nfs.FirstOrDefault().Filial.CNPJ,
                IND_ESCRI = 2
            };
            result.Add(filial);
            return result;
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
            Header = new Reg_C001();
            Header.IND_MOV = nfs.Count > 0 ? 0 : 1;
            if (Header.IND_MOV == 0)
            {
                IdentEstabelecimentos = GerarC010(nfs);
                NfsNormais = GerarNfsNormais(nfs, inicio, fim);
                SvcAguaEnergiaGas = GerarNFsServicos(nfs, inicio, fim);
            }
            Fechamento = new Reg_C990();
            Fechamento.QTD_LIN_C = GetContagemRegistros();
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