using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BA_Notas : Bloco
    {
        public Nota nf { get; set; }
        public Reg_A100 Nota { get; set; }
        public Reg_A110 InformacaoComplementarNF { get; set; }
        public Reg_A111 ProcessoReferenciado { get; set; }
        public Reg_A120 InformacaoComplementarOperacoesImportacao { get; set; }
        public IList<Reg_A170> NotaItens { get; set; }

        public override void GerarBloco()
        {
            Nota = GerarA100(nf);
            
            if (ParamsBloco.ContainsKey("COMP_DOC"))
            {
                IList<Reg_0450> CompDocs = (IList<Reg_0450>)ParamsBloco["COMP_DOC"];
                InformacaoComplementarNF = GerarA110(nf, CompDocs);
            }
            NotaItens = GerarItensNota(nf);

        }
        public BA_Notas(Nota n)
        {
            ParamsBloco = new Dictionary<string, object>();
            nf = n;
        }

        private Reg_A100 GerarA100(Nota nf)
        {
            Nota = new Reg_A100();
            Reg_0150 temp = new Reg_0150();
            if (nf.Ent_Sai == InOutType.Entrada)
            {
                Nota.IND_OPER = 0;
                Nota.IND_EMIT = 1;
            }
            else
            {
                Nota.IND_OPER = 0;
                Nota.IND_EMIT = 1;
            }
            temp = ((IList<Reg_0150>)ParamsBloco["COD_PART"])
                        .Where(w => w.cnpjDoParticipante == nf.Pessoa.CNPJ_CPF
                        || w.cpfDoParticipante == nf.Pessoa.CNPJ_CPF)
                        .FirstOrDefault();
            
            Nota.COD_PART = temp.codigoDeIdentificacaoDoParticipante.ToString();

            Nota.COD_SIT = nf.DtHoraCanc.HasValue ? "02" : "00";
            Nota.SER = nf.Serie;
            Nota.SUB = "";
            Nota.NUM_DOC = nf.NumeroNFSe;
            Nota.CHV_NFSE = nf.ChaveNota;
            Nota.DT_DOC = nf.DtDigitacao.HasValue ? nf.DtDigitacao.Value : nf.Dt;
            Nota.DT_EXE_SERV = nf.Dt;
            if (nf.Documentos != null)
            {
                if (nf.Documentos.Count > 0)
                {
                    Nota.IND_PGTO = nf.Condicao.CondicaoParcelas.Where(w => w.BAVista == false).Count() > 0 ? 1 : 0;
                }
                else
                {
                    Nota.IND_PGTO = 9;
                }
            }
            Nota.VL_DOC = nf.TotalNota;
            Nota.VL_DESC = nf.ValorDesconto;
            IList<NotaImpostoServico> ListaPIS = nf.NotaImpostoServicos.Where(w => w.Imposto.TipoImposto.Nome == "PIS").ToList();
            IList<NotaImpostoServico> ListaCofins = nf.NotaImpostoServicos.Where(w => w.Imposto.TipoImposto.Nome == "COFINS").ToList();
            IList<NotaImpostoServico> ListaISS = nf.NotaImpostoServicos.Where(w => w.Imposto.TipoImposto.Nome == "ISS").ToList();
            if (ListaPIS != null)
            {
                Nota.VL_BC_PIS = ListaPIS.Sum(s => s.BaseCalculo);
                Nota.VL_PIS = ListaPIS.Sum(s => s.Valor);
            }
            if (ListaCofins != null)
            {
                Nota.VL_BC_COFINS = ListaCofins.Sum(s => s.BaseCalculo);
                Nota.VL_COFINS = ListaCofins.Sum(s => s.Valor);
            }
            if (ListaISS != null)
            {
                Nota.VL_ISS = ListaISS.Sum(s => s.Valor);
            }
            return Nota;
        }

        private Reg_A110 GerarA110(Nota nf, IList<Reg_0450> CompNotas)
        {
            foreach (Reg_0450 item in CompNotas)
            {
                if (item.NotasDoTextoLei.Contains(nf))
                {
                    Reg_A110 result = new Reg_A110() 
                    { 
                        COD_INF = item.codigoDaInformacaoComplementarDoDocumento, 
                        TXT_COMPL = item.textoLivreDaInformacaoComplementarExistenteNoDocumentoFiscal 
                    };
                    return result;
                }
            }
            return null;
        }

        private IList<Reg_A170> GerarItensNota(Nota nf)
        {
            IList<Reg_A170> result = new List<Reg_A170>();
            int count = 0;
            foreach (NotaItemServico item in nf.NotaItemsServicos)
            {
                count++;
                Reg_A170 reg = new Reg_A170();
                reg.NUM_ITEM = count;
                reg.COD_ITEM = item.Servico.Codigo.ToString();
                reg.DESCR_COMPL = item.Servico.Descricao;
                reg.VL_ITEM = item.SubTotal;
                reg.VL_DESC = Math.Round(((item.Valor * item.PercDesconto)/100), 2);
                
                try
                {
                    NotaImpostoServico nisPis = nf.NotaImpostoServicos.Where(w => w.Imposto.Nome.ToLower().Contains("pis")).FirstOrDefault();
                    NotaImpostoServico nisCofins = nf.NotaImpostoServicos.Where(w => w.Imposto.Nome.ToLower().Contains("cofins")).FirstOrDefault();
                    
                    if (nisPis != null)
	                {
                        if (nisPis.Imposto.CSTImposto != null)
                        {
                            if (nisPis.Imposto.CSTImposto.Imposto.Nome == "PIS")
                            {
                                reg.CST_PIS = nisPis.Imposto.CSTImposto.CodCST;
                            }
                        }
                        reg.VL_BC_PIS = nisPis.BaseCalculo;
                        reg.ALIQ_PIS = Math.Round(nisPis.Aliquota,2);
                        reg.VL_PIS = nisPis.Valor;
	                }

                    if (nisCofins != null)
                    {
                        if (nisCofins.Imposto.CSTImposto != null)
                        {
                            if (nisCofins.Imposto.CSTImposto.Imposto.Nome == "COFINS")
                            {
                                reg.CST_COFINS = nisCofins.Imposto.CSTImposto.CodCST;
                            }
                        }
                        reg.VL_BC_COFINS = nisCofins.BaseCalculo;
                        reg.ALIQ_COFINS = Math.Round(nisCofins.Aliquota,2);
                        reg.VL_COFINS = nisCofins.Valor;
                    }
                    
                    int cstpis = Convert.ToInt32(reg.CST_PIS);
                    int cstcofins = Convert.ToInt32(reg.CST_COFINS);

                    reg.IND_ORIG_CRED = "0";

                    if ((cstpis >= 50 && cstpis <= 67) || (cstcofins >= 50 && cstcofins <= 67))
                    {
                        reg.NAT_BC_CRED = nf.TipoNota.Operacao.NatBcCred.CodCST;
                        
                    }
                    reg.COD_CTA = "";
                    reg.COD_CCUS = "";

                    result.Add(reg);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }
    }
}
