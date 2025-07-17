using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Blocos;
using cwkGestao.Modelo;
using System.IO;
using cwkGestao.Modelo.Util;
using System.Windows.Forms;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos
{
    public class SpedFiscal
    {
        public Bloco_0 bloco_0 { get; set; }
        public Bloco_A bloco_A { get; set; }
        public Bloco_C bloco_C { get; set; }
        public Bloco_D bloco_D { get; set; }
        public Bloco_F bloco_F { get; set; }
        public Bloco_I bloco_I { get; set; }
        public Bloco_M bloco_M { get; set; }
        public Bloco_P bloco_P { get; set; }
        public Bloco_1 bloco_1 { get; set; }
        public Bloco_9 bloco_9 { get; set; }

        public Bloco_ICMS_0 bloco_ICMS_0 { get; set; }
        public Bloco_ICMS_C bloco_ICMS_C { get; set; }

        public IList<Nota> Notas { get; set; }
        public IList<Documento> Documentos { get; set; }

        public SpedFiscal(IList<Nota> Ns, Filial filial, IList<Documento> Documentos, string caminhoArquivoSaidaSped, DateTime InicioPeriodo, DateTime FimPeriodo, 
                          IList<DateTime> DatasDispensaEnvio, IList<PlanoConta> PlanosConta, IList<CentroCusto> CentrosCusto, bool SemNota,
                          PessoaContabilista Contabilista, IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem, int tipo, int finalidade,
                          IList<ProdutoConversao> produtoConversoes, IList<LogAlteracaoParticipante> logAlteracaoDadosParticipante, IList<NCM> listaNCM, VersaoEfdIcmsIpi versaoSped)
        {
            try
            {
                switch (tipo)
                {
                    case 0:
                        GeraEFDContribuições(Ns, filial, Documentos, caminhoArquivoSaidaSped, ref InicioPeriodo, ref FimPeriodo, DatasDispensaEnvio, PlanosConta, 
                                            CentrosCusto, SemNota, Contabilista, logAlteracaoPedidoItem);
                        break;
                    case 1:
                        GeraICMSIPI(Ns, filial, Documentos, caminhoArquivoSaidaSped, ref InicioPeriodo, ref FimPeriodo, DatasDispensaEnvio, PlanosConta,
                                    CentrosCusto, SemNota, Contabilista, logAlteracaoPedidoItem, finalidade, produtoConversoes, logAlteracaoDadosParticipante
                                    , listaNCM, versaoSped);
                        break;
                    default:
                        
                        break;
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }

            //bloco_I.GerarBloco();
            //Util.EscritorSped.EscritorBloco(bloco_I, filename);

            //bloco_P = new Bloco_P();
            //Util.EscritorSped.EscritorBloco(bloco_P, filename);

            
        }

        private void GeraICMSIPI(IList<Nota> Ns, Filial filial, IList<Documento> Documentos, string caminhoArquivoSaidaSped, 
                                ref DateTime InicioPeriodo, ref DateTime FimPeriodo, IList<DateTime> DatasDispensaEnvio, 
                                IList<PlanoConta> PlanosConta, IList<CentroCusto> CentrosCusto, bool SemNota, 
                                PessoaContabilista Contabilista, IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem,
                                int finalidade, IList<ProdutoConversao> produtoConversoes, IList<LogAlteracaoParticipante> logAlteracaoDadosParticipante,
                                IList<NCM> listaNCM, VersaoEfdIcmsIpi versao)
        {
            Notas = Ns.Where(w =>
               (w.StatusLegivel == "Autorizada") ||
               (w.StatusLegivel == "Cancelada") ||
               (w.StatusLegivel == "Denegada")
               ).ToList();
            string filename = Path.Combine(caminhoArquivoSaidaSped,
                            "SPED-ICMS-" + InicioPeriodo.Month.ToString() + "-" + InicioPeriodo.Year.ToString() + ".txt");

            FormProgressBar formProg = new FormProgressBar(filename);
            if (formProg.ShowDialog() != DialogResult.OK)
            {
                return;
            } 

            Dictionary<string, int> ContagemIndividualRegistros = new Dictionary<string, int>();

            string codVersaoLeiaute = "2.8.0";//versao.CodigoSped;
            int tipoEscrituracao = 0;


            bloco_ICMS_0 = new Bloco_ICMS_0(Notas, filial, codVersaoLeiaute, tipoEscrituracao, "", InicioPeriodo, FimPeriodo, 0, DatasDispensaEnvio, Documentos,
                                            PlanosConta, CentrosCusto, SemNota, Contabilista, logAlteracaoPedidoItem, finalidade, produtoConversoes,
                                            logAlteracaoDadosParticipante, listaNCM);
            bloco_ICMS_0.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_ICMS_0, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_ICMS_0.GetContagemRegistrosIndividuais());

            bloco_ICMS_C = new Bloco_ICMS_C(Notas, InicioPeriodo, FimPeriodo);
            bloco_ICMS_C.ParamsBloco.Add("COD_PART", bloco_ICMS_0.Participantes);
            bloco_ICMS_C.ParamsBloco.Add("COMP_DOC", bloco_ICMS_0.TextosLei);

            bloco_ICMS_0 = null;
            GC.Collect();

            bloco_ICMS_C.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_ICMS_C, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_ICMS_C.GetContagemRegistrosIndividuais());
            bloco_C = null;
            GC.Collect();


        }

        private void GeraEFDContribuições(IList<Nota> Ns, Filial filial, IList<Documento> Documentos, string caminhoArquivoSaidaSped, ref DateTime InicioPeriodo, ref DateTime FimPeriodo, IList<DateTime> DatasDispensaEnvio, IList<PlanoConta> PlanosConta, IList<CentroCusto> CentrosCusto, bool SemNota, PessoaContabilista Contabilista, IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem)
        {
            Notas = Ns.Where(w =>
                (w.StatusLegivel == "Autorizada") ||
                (w.StatusLegivel == "Cancelada") ||
                (w.StatusLegivel == "Denegada")
                ).ToList();
            string filename = Path.Combine(caminhoArquivoSaidaSped,
                            "SPED-EFD-" + InicioPeriodo.Month.ToString() + "-" + InicioPeriodo.Year.ToString() + ".txt");
            //FileStream f = File.Create(filename);
            //f.Close();


            FormProgressBar formProg = new FormProgressBar(filename);
            formProg.ShowDialog();
            if (formProg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Dictionary<string, int> ContagemIndividualRegistros = new Dictionary<string, int>();

            string codVersaoLeiaute = "006";
            int tipoEscrituracao = 0;

            bloco_0 = new Bloco_0(Notas, filial, codVersaoLeiaute, tipoEscrituracao, "", InicioPeriodo, FimPeriodo, 0,
                                  DatasDispensaEnvio, Documentos, PlanosConta, CentrosCusto, SemNota, Contabilista, logAlteracaoPedidoItem);
            bloco_0.GerarBloco();

            IList<Nota> NFSes = Notas.Where(w => w.NotaItemsServicos.Count > 0 && w.ModeloDoctoString != "RPS").ToList();

            bloco_A = new Bloco_A(NFSes, InicioPeriodo, FimPeriodo);

            bloco_A.ParamsBloco.Add("COD_PART", bloco_0.Participantes);
            bloco_A.ParamsBloco.Add("COMP_DOC", bloco_0.TextosLei);

            bloco_C = new Bloco_C(Notas, InicioPeriodo, FimPeriodo);
            bloco_C.ParamsBloco.Add("COD_PART", bloco_0.Participantes);
            bloco_C.ParamsBloco.Add("COMP_DOC", bloco_0.TextosLei);

            bloco_D = new Bloco_D(Notas, InicioPeriodo, FimPeriodo);
            bloco_D.ParamsBloco.Add("COD_PART", bloco_0.Participantes);
            bloco_D.ParamsBloco.Add("COMP_DOC", bloco_0.TextosLei);

            bloco_F = new Bloco_F();
            //bloco_F.ParamsBloco.Add("", new object());

            //bloco_I = new Bloco_I();
            //bloco_I.ParamsBloco.Add("", new object());

            bloco_M = new Bloco_M();
            //bloco_M.ParamsBloco.Add("", new object());

            bloco_1 = new Bloco_1();
            //bloco_1.ParamsBloco.Add("", new object());
            Util.EscritorSped.EscritorBloco(bloco_0, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_0.GetContagemRegistrosIndividuais());
            bloco_0 = null;
            GC.Collect();

            bloco_A.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_A, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_A.GetContagemRegistrosIndividuais());
            bloco_A = null;
            GC.Collect();

            bloco_C.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_C, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_C.GetContagemRegistrosIndividuais());
            bloco_C = null;
            GC.Collect();

            bloco_D.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_D, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_D.GetContagemRegistrosIndividuais());
            bloco_D = null;
            GC.Collect();

            bloco_F.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_F, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_F.GetContagemRegistrosIndividuais());
            bloco_F = null;
            GC.Collect();

            bloco_M.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_M, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_M.GetContagemRegistrosIndividuais());
            bloco_M = null;
            GC.Collect();

            bloco_1.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_1, filename);
            ContagemIndividualRegistros = Util.EscritorSped.ConcatDictionary(ContagemIndividualRegistros, bloco_1.GetContagemRegistrosIndividuais());
            bloco_1 = null;
            GC.Collect();

            bloco_9 = new Bloco_9();
            bloco_9.ParamsBloco.Add("CONTAGEM", ContagemIndividualRegistros);
            bloco_9.GerarBloco();
            Util.EscritorSped.EscritorBloco(bloco_9, filename);
            bloco_9 = null;
            GC.Collect();
        }

    }

}
