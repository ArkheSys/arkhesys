using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Modelo.Proxy;
using MatrixReporter;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace cwkGestao.Negocio
{
    public partial class DocumentoController : BaseController<Documento>
    {
        #region Singleton
        private static DocumentoController instance;
        private static IRepositorioDocumento repositorioDocumento;

        private DocumentoController()
        { }

        static DocumentoController()
        {
            instance = new DocumentoController();
            repositorioT = RepositorioFactory<Documento>.GetRepositorio();
            repositorioDocumento = (IRepositorioDocumento)repositorioT;
        }

        public static DocumentoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Documento objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(Documento objeto, Acao acao)
        {
            UsuarioProvider usuario = new UsuarioProvider();
            objeto.AltUsuario = usuario.GetLoginUsuarioLogado();
            objeto.AltData = DateTime.Now;
            foreach (var item in objeto.Movimentos)
            {
                item.AltUsuario = usuario.GetLoginUsuarioLogado();
                item.AltData = DateTime.Now;
            }

            if (acao == Acao.Incluir)
            {
                objeto.IncUsuario = usuario.GetLoginUsuarioLogado();
                objeto.IncData = DateTime.Now;
                foreach (var item in objeto.Movimentos)
                {
                    item.IncUsuario = usuario.GetLoginUsuarioLogado();
                    item.IncData = DateTime.Now;
                }
            }

            return base.Salvar(objeto, acao);
        }

        protected override void Update(Documento objeto)
        {
            repositorioDocumento.Merge(objeto);
        }

        public cwkGestao.Modelo.Documento GetDocumentoPorNumero(Int32 pNumDoc)
        {
            return repositorioDocumento.GetDocumentoPorNumero(pNumDoc);
        }

        public IList<object> GetContaPorPortador(DateTime dataInicial, DateTime dataFinal, int tipoDoc, string idsEmpresas, string idsportadores, string situacao)
        {
            return repositorioDocumento.GetContaPorPortador(dataInicial, dataFinal, tipoDoc, idsEmpresas, idsportadores, situacao);
        }

        public bool NotaPossuiDocumentosBaixados(int idNota)
        {
            IList<Modelo.Documento> documentos = repositorioDocumento.GetDocumentosPorNota(idNota);
            return ExisteDocumentoBaixado(documentos);
        }

        public bool ExisteDocumentoBaixado(IList<Documento> documentos)
        {
            foreach (Modelo.Documento item in documentos)
            {
                if (item.Saldo != item.Valor)
                    return true;
            }
            return false;
        }

        public IList<Documento> GetDocumentosPorNota(int idNota)
        {
            return repositorioDocumento.GetDocumentosPorNota(idNota).Distinct().ToList();
        }

        public IList<Documento> GetDocumentosPorNumeroPedido(int numero)
        {
            return repositorioDocumento.GetDocumentosPorNumeroPedido(numero).Distinct().ToList();
        }

        public IList<pxyRemessa> GetDocumentosPorArquivoRemessa(int idArquivoRemessa)
        {
            return repositorioDocumento.GetDocumentosPorArquivoRemessa(idArquivoRemessa);
        }

        public IList<pxyDocumentoArquivoRemessa> GetPxyDocumentosArqRemessaPorArquivoRemessa(int idArquivoRemessa)
        {
            return repositorioDocumento.GetPxyDocumentosArqRemessaPorArquivoRemessa(idArquivoRemessa);
        }

        public IList<pxyDocumentoArquivoRemessa> GetDocumentosParaArquivoRemessa(Filial Filial, DateTime DataInicial, DateTime DataFinal, bool porVencimento, TipoDocumentoType tipo, int? idArquivoRemessa, int? IdTipoDocumento, int? IdPortador)
        {
            IList<pxyDocumentoArquivoRemessa> listaTotal = repositorioDocumento.GetDocumentosParaArquivoRemessa(Filial, DataInicial, DataFinal, porVencimento, tipo, idArquivoRemessa, IdTipoDocumento, IdPortador);
            if (idArquivoRemessa != null)
            {
                (listaTotal.Where(c => c.IDRemessa == idArquivoRemessa).ToList()).ForEach(s => s.Selecionado = true);
            }
            return listaTotal;
        }

        public IList<Object> GetDocumentoPorFormaPgto(DateTime dataInicial, DateTime dataFinal, String IDsEmrpresa, String IDsFormaPagamento, TipoDocumentoType Tipo, Boolean SemFormaPagto)
        {
            return repositorioDocumento.GetDocumentoPorFormaPgto(dataInicial, dataFinal, IDsEmrpresa, IDsFormaPagamento, Tipo, SemFormaPagto);
        }

        #region Cancelamento de documento

        public void CancelarDocumentoESalvar(Documento documento, Historico historico, string cancelamentoMotivo, decimal cancelamentoValor)
        {
            CancelarDocumento(documento, historico, cancelamentoMotivo, cancelamentoValor);
            Salvar(documento, Acao.Alterar);
        }

        public void CancelarDocumentos(IList<Documento> documentos)
        {
            foreach (Documento doc in documentos)
            {
                CancelarDocumento(doc, doc.Historico, "", doc.Saldo);
                RegisterNewIntoTransaction(doc);
            }
        }
        public void CancelarDocumentosNFSe(IList<Documento> documentos)
        {
            foreach (Documento doc in documentos)
            {
                CancelarDocumento(doc, doc.Historico, "", doc.Saldo);
                RegisterNewIntoTransaction(doc);
                CommitUnitOfWorkTransaction();
            }
        }

        public void CancelarDocumento(Documento documento, Historico historico, string cancelamentoMotivo, decimal cancelamentoValor)
        {
            ValidaParametrosCancelamento(documento, historico, cancelamentoMotivo, cancelamentoValor);

            IList<Movimento> movimentosCancelamento = CriaMovimentosCancelamento(documento, historico, cancelamentoMotivo, cancelamentoValor);

            documento.Saldo -= cancelamentoValor;

            AdicionaMovimentosCancelamentos(documento, movimentosCancelamento);
            if (EhDocumentoCancelado(documento))
            {
                documento.Situacao = "Canc";
            }
            else if (documento.Saldo == 0)
            {
                documento.Situacao = "BxT";
            }
        }

       

        private void AdicionaMovimentosCancelamentos(Documento documento, IList<Movimento> movimentosCancelamento)
        {
            foreach (Movimento item in movimentosCancelamento)
            {
                documento.Movimentos.Add(item);
                item.Documento = documento;
            }
        }

        private IList<Movimento> CriaMovimentosCancelamento(Documento documento, Historico historico, string cancelamentoMotivo, decimal cancelamentoValor)
        {
            IList<Movimento> retorno = new List<Movimento>();
            int sequencia = ProximoNumeroSequencia(documento.Movimentos);
            foreach (var item in ListaLancamentos(documento.Movimentos))
            {
                Movimento movimentoCancelamento = new Movimento();

                decimal percentualRateio = item.Valor / documento.Valor;
                movimentoCancelamento.PlanoConta = item.PlanoConta;
                movimentoCancelamento.Valor = cancelamentoValor * percentualRateio;
                movimentoCancelamento.Sequencia = sequencia;

                movimentoCancelamento.CentroCusto = item.CentroCusto;
                movimentoCancelamento.ComplementoHist = cancelamentoMotivo;
                movimentoCancelamento.Dt = DateTime.Today;
                movimentoCancelamento.Historico = historico;
                movimentoCancelamento.MovCaixa = null;
                movimentoCancelamento.Operacao = OperacaoMovimentoType.Cancelado;
                movimentoCancelamento.Origem = (int)OrigemMovimentoType.Financeiro; //TODO: financeiro?
                movimentoCancelamento.Tipo = TipoMovimentoType.Documento;

                retorno.Add(movimentoCancelamento);
            }


            return retorno;
        }

        private int ProximoNumeroSequencia(IList<Movimento> movimentos)
        {
            return movimentos.GroupBy(i => i.Sequencia).Count() + 1;
        }

        private IList<Movimento> ListaLancamentos(IList<Movimento> movimentos)
        {
            return movimentos.Where(i => i.Operacao == OperacaoMovimentoType.Lancamento && i.Sequencia == 1).ToList();
        }

        private bool EhDocumentoCancelado(Documento documento)
        {
            var Y = documento.Movimentos.Where(i => i.Operacao == OperacaoMovimentoType.Cancelado).Sum(i => i.Valor);
            if (documento.Valor > Y)
                return true;

            return Y  == documento.Valor;
        }

        private void ValidaParametrosCancelamento(Documento documento, Historico historico, string cancelamentoMotivo, decimal cancelamentoValor)
        {
            Exception excecaoRaiz = null;
            if (cancelamentoValor > documento.Saldo)
            {
                excecaoRaiz = new ArgumentException("Não é possível cancelar com um valor maior que o saldo do documento.", excecaoRaiz);
            }
            //if (documento.Saldo == 0)
            //{
            //    excecaoRaiz = new ArgumentException("Não é possível cancelar documento com saldo zerado.", excecaoRaiz);
            //}
            //if (cancelamentoValor == 0)
            //{
            //    excecaoRaiz = new ArgumentException("Não é possível cancelar documento com valor zero.", excecaoRaiz);
            //}

            if (excecaoRaiz != null)
                throw excecaoRaiz;
        }

        #endregion

        public IList<Documento> GetAllPossuiSaldoPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioDocumento.GetAllPossuiSaldoPorPeriodo(dataInicial, dataFinal);
        }

        public IList<Documento> GetAllPossuiSaldoPorPeriodoETipoDocumento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo, bool FlagTelefonia)
        {
            return repositorioDocumento.GetAllPossuiSaldoPorPeriodoETipoDocumento(dataInicial, dataFinal, tipo, FlagTelefonia);
        }

        public IList<Documento> GetAllBaixadoPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType tipo)
        {
            return repositorioDocumento.GetAllBaixadoPorVencimento(dataInicial, dataFinal, tipo);
        }

        public IList<Documento> GetAllAbertosPorCliente(Pessoa cliente)
        {
            return repositorioDocumento.GetAllAbertosPorCliente(cliente);
        }

        public Decimal GetValorSaldoAllAbertosPorCliente(Pessoa cliente)
        {
            return repositorioDocumento.GetValorSaldoAllAbertosPorCliente(cliente);
        }

        public IList<Documento> GetAllPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo, bool FlagTelefonia)
        {
            return repositorioDocumento.GetAllPorVencimento(dataInicial, dataFinal, tipo, FlagTelefonia);
        }

        public IList<Documento> GetAllSaldoZeroPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo, bool FlagTelefonia)
        {
            return repositorioDocumento.GetAllSaldoZeroPorVencimento(dataInicial, dataFinal, tipo, FlagTelefonia);
        }

        public IList<Documento> GetAllDocumentoPorClienteEPeriodo(Pessoa cliente, DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioDocumento.GetAllDocumentoPorClienteEPeriodo(cliente, dataInicial, dataFinal);
        }

        public long GetNumeroDocumentoValido()
        {
            return repositorioDocumento.GetNumeroDocumentoValido();
        }

        public void ImprimeDuplicata(Documento objDocumento)
        {
            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;

            lPrinter = new Reporter();
            lPrinter.Output = @"\\dev02\EPSONLX300";

            lPrinter.NomeArquivo = "dup" + objDocumento.Codigo + ".txt";

            lPrinter.StartJob();

            int count = 0;

            lPrinter.PrintText(count, 0, lCodes.Reset + lCodes.LinesInch6 + lCodes.SetPageSize(44) + lCodes.CondensedOn);
            //Local
            count = 12;
            lPrinter.PrintText(count, 97, objDocumento.Filial.Cidade.Nome);
            //Data Emissão
            count = 13;
            lPrinter.PrintText(count, 97, DateTime.Now.ToShortDateString());
            //Valor e Código - Fatura
            count = 16;
            lPrinter.PrintText(count, 0, objDocumento.Valor.ToString());
            lPrinter.PrintText(count, 20, objDocumento.Codigo.ToString());
            //Valor e Código - Duplicata
            lPrinter.PrintText(count, 38, objDocumento.Valor.ToString());
            lPrinter.PrintText(count, 63, objDocumento.Codigo.ToString());
            //Vencimento
            lPrinter.PrintText(count, 87, objDocumento.DtVencimento.ToShortDateString());
            //Nome do Sacado
            count = 20;
            lPrinter.PrintText(count, 32, objDocumento.Pessoa.Nome);
            //Endereço
            count = 21;
            PessoaEndereco end = objDocumento.Pessoa.EnderecoPrincipal();
            lPrinter.PrintText(count, 32, end.Endereco);
            //Municipio - Estado
            count = 22;
            lPrinter.PrintText(count, 32, end.Cidade.Nome);
            lPrinter.PrintText(count, 113, end.Cidade.UF.Sigla);
            //Praça de Pagamento ?
            count = 23;
            lPrinter.PrintText(count, 32, end.Cidade.Nome);
            lPrinter.PrintText(count, 113, end.Cidade.UF.Sigla);
            //CNPJ - Inscrição
            count = 24;
            lPrinter.PrintText(count, 32, objDocumento.Pessoa.CNPJ_CPF);
            if (objDocumento.Pessoa.Inscricao != null)
                lPrinter.PrintText(count, 113, objDocumento.Pessoa.Inscricao);

            //Valor Por Extenso
            count = 27;
            string extenso = ClsExtenso.Extenso_Valor(objDocumento.Valor);
            lPrinter.PrintText(count, 35, lCodes.BoldOn + extenso + lCodes.BoldOff);

            //Passa para a próxima folha
            while (count <= 44)
            {
                lPrinter.PrintText(count, 0, " ");
                count++;
            }

            lPrinter.EndJob();
            lPrinter.PreviewJob();
        }

        public List<Object> recibo(Documento doc, decimal valorBaixa)
        {
            string Referente = doc.ComplementoHist + " Nº Doc. " + doc.NumDocumento + " Parcela " + doc.Parcela.ToString() + "/" + doc.QtParcela.ToString() + " Vencimento " + String.Format("{0: dd/MM/yyyy}", doc.DtVencimento);
            return recibo(doc.Pessoa.EnderecoPrincipal().ToString(), doc.Filial.Endereco + ", " + doc.Filial.Numero + " - " + doc.Filial.Bairro + " - " + doc.Filial.Cidade + "-" + doc.Filial.Cidade.UF, Referente, valorBaixa,
                    doc.Tipo.ToString(), doc.Filial.Nome, (doc.Pessoa.Nome + " (" + doc.Pessoa.FantasiaApelido + ")"), doc.Filial.CNPJ);
        }

        public List<Object> recibo(String EndCliente, String EndEmpresa, String pReferente, Decimal pValor, String pTipoRelatorio, String pFilial, String pPessoa, String pCNPJ_CPF)
        {
            List<Object> ParametrosForm = new List<Object>();

            string ValorPorExtenso = ClsExtenso.Extenso_Valor((Decimal)pValor).ToUpper();
            DateTime DataAtual = DateTime.Now;

            var parametros = new List<ReportParameter>();
            var p1 = new ReportParameter("DataAtual", DataAtual.ToShortDateString().ToString());
            var p2 = new ReportParameter("ValorPorExtenso", ValorPorExtenso);
            var p3 = new ReportParameter("Valor", pValor.ToString());

            parametros.Add(p1);
            parametros.Add(p2);
            parametros.Add(p3);

            if (pTipoRelatorio == Modelo.TipoDocumentoType.Pagar.ToString())
            {
                var p4 = new ReportParameter("Recebemos", pFilial);
                var p5 = new ReportParameter("EnderecoDeCima", EndEmpresa);
                var p6 = new ReportParameter("Referente", pReferente);

                int indice = pPessoa.IndexOf("(");
                if (indice > 0)
                    pPessoa = pPessoa.Remove(indice);
                var p7 = new ReportParameter("Emitente", pPessoa);

                var p8 = new ReportParameter("EnderecoDeBaixo", EndCliente);
                var p9 = new ReportParameter("CNPJ_CPF", pCNPJ_CPF);

                parametros.Add(p4);
                parametros.Add(p5);
                parametros.Add(p6);
                parametros.Add(p7);
                parametros.Add(p8);
                parametros.Add(p9);
            }
            else //A Receber
            {
                var p4 = new ReportParameter("Recebemos", pPessoa);
                var p5 = new ReportParameter("EnderecoDeCima", EndCliente);
                var p6 = new ReportParameter("Referente", pReferente);
                var p7 = new ReportParameter("Emitente", pFilial);
                var p8 = new ReportParameter("EnderecoDeBaixo", EndEmpresa);
                var p9 = new ReportParameter("CNPJ_CPF", pCNPJ_CPF);

                parametros.Add(p4);
                parametros.Add(p5);
                parametros.Add(p6);
                parametros.Add(p7);
                parametros.Add(p8);
                parametros.Add(p9);
            }

            XmlDocument documento = new XmlDocument();
            documento.Load(global ::Modelo.cwkGlobal.DirApp + @"\ConfiguracaoCwork.xml");
            XmlNodeReader leitor = new XmlNodeReader( documento );

            string nomeRelatorio = "rptRecibo.r" +
                                   "dlc" +
                                   "";
            while (leitor.Read())
            {
                 if (leitor.Value == "2 via")
                    nomeRelatorio = "rptRecibo2Vias.rdlc";
            }

            ParametrosForm.Add(@"Recibo\" +nomeRelatorio);
            ParametrosForm.Add("dsRelRecibo_Recibo");
            ParametrosForm.Add(new DataTable());
            ParametrosForm.Add(parametros);

            return ParametrosForm;
        }

        public void DescancelarDocumentos(IList<Documento> documentosNota)
        {
            foreach (var doc in documentosNota)
            {
                DescancelarDocumento(doc);
                RegisterNewIntoTransaction(doc);
            }
        }

        private void DescancelarDocumento(Documento documento)
        {
            var listaMovimentos = (from m in documento.Movimentos
                                   where m.Operacao == OperacaoMovimentoType.Cancelado
                                   && m.Tipo == TipoMovimentoType.Documento
                                   select m).ToList();

            for (int i = 0; i < listaMovimentos.Count; i++)
            {
                documento.Saldo = documento.Valor;
                documento.Situacao = "Norm";
                documento.Movimentos.Remove(listaMovimentos[i]);
            }
        }

        public void DescancelarDocumentoUnico(Documento documento)
        {
            documento.Saldo = documento.Valor;
          
            var listaMovimentos = (from m in documento.Movimentos
                                   where m.Operacao == OperacaoMovimentoType.Cancelado
                                   && m.Tipo == TipoMovimentoType.Documento
                                   select m).ToList();

            for (int i = 0; i < listaMovimentos.Count; i++)
            {
                documento.Movimentos.Remove(listaMovimentos[i]);
            }

            if (documento.Saldo == documento.ValorTotal)
                documento.Situacao = "Norm";
            else if (documento.Saldo < documento.ValorTotal)
                documento.Situacao = "BxP";

            DocumentoController.Instancia.Salvar(documento, Acao.Alterar);
        }

        public IList<Modelo.Documento> GetPorProjeto(Projeto projeto)
        {
            return repositorioDocumento.GetPorProjeto(projeto);
        }

        internal IList<pxyRemessa> GetRemessa(Filial filial, DateTime dataInicial, DateTime dataFinal, bool porVencimento, TipoDocumentoType tipo, bool impresso, bool geradoRemessa, bool geradoPDF, bool enviadoEmail, bool TodosDocumentos)
        {
            return repositorioDocumento.GetRemessa(filial, dataInicial, dataFinal, porVencimento, tipo, impresso, geradoRemessa, geradoPDF, enviadoEmail, TodosDocumentos);
        }
        
        internal IList<Documento> GetGerarBoletoRemessa(List<pxyRemessa> list)
        {
            return repositorioDocumento.GetGerarBoletoRemessa(list);
        }

        internal IList<pxyRemessa> GetRemessaPorNota(Nota nota)
        {
            return repositorioDocumento.GetRemessaPorNota(nota);
        }

        public Documento LoadObjectByCodigo(int codigo)
        {
            return repositorioDocumento.LoadObjectByCodigo(codigo);
        }

        public void Refresh(Documento objDocumento)
        {
            repositorioDocumento.Refresh(objDocumento);
        }

        public IList<Documento> GetAllByID(IList<int> ids)
        {
            return repositorioDocumento.GetAllByID(ids);
        }

        public List<Documento> LoadAllByID(List<int> ids)
        {
            return repositorioDocumento.LoadAllByID(ids).ToList();
        }

        public IList<Documento> GetDocumentoPorCodigo(int codigo)
        {
            return repositorioDocumento.GetDocumentoPorCodigo(codigo);
        }

        public void AtualizaDocumento(string idsdocto, string idremessa)
        {
            repositorioDocumento.AtualizaDocumento(idsdocto, idremessa);
        }

        public bool DeletarRenegociacao(Documento documento)
        {
            IList<ReparcelamentoDoc> reparcDoc = ReparcelamentoDocController.Instancia.LoadByIdDocumento(documento.ID);
            if (reparcDoc.Count() > 0)
            {
                Reparcelamento reparc = ReparcelamentoController.Instancia.LoadObjectById(reparcDoc.FirstOrDefault().Reparcelamento.ID);
                IList<ReparcelamentoDoc> reparcelamentosDoc = new List<ReparcelamentoDoc>();
                // Parcelas dos documento que foram gerados através da renegociacao retirando o próprio documento passado como parametro (são as outras parcelas, acontece quando o documento foi renegociado parcelado)
                reparcelamentosDoc = reparc.ReparcelamentoDocs.Where(x => x.Tipo == true && x.Documento.ID != documento.ID).ToList();
                if (reparcelamentosDoc.Count() > 0)
                {
                    if (MessageBox.Show("O Documento possui outras parcelas vinculadas, ao confirmar as parcelas serão excluídas, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (ReparcelamentoDoc reparcelamentoDoc in reparcelamentosDoc)
                        {
                            if (reparcelamentoDoc.Documento.Situacao != "Norm")
                            {
                                string erro = "Não foi possível excluir o documento, pois esta relacionado com o documento número " + reparcelamentoDoc.Documento.NumDocumento + " que possui movimentações.";
                                throw new Exception(erro);
                            }
                            else
                            {
                                DocumentoController.Instancia.RegisterDeleteIntoTransaction(reparcelamentoDoc.Documento);
                            }
                        }
                    }
                    else 
                     return false;
                }
                
                // Volta status dos documento que geraram a renegociacao
                reparcelamentosDoc = reparc.ReparcelamentoDocs.Where(x => x.Tipo == false).ToList();
                foreach (ReparcelamentoDoc reparcelamentoDoc in reparcelamentosDoc)
                {
                    Documento docReparcelado = reparcelamentoDoc.Documento;
                    IList<Movimento> movimentos = docReparcelado.Movimentos.Where(x => x.Operacao == OperacaoMovimentoType.BxRenegociação).ToList();
                    List<Documento> docsAfetados = new List<Documento>() { docReparcelado };
                    List<Movimento> movimentosDeletados = new List<Movimento>();
                    List<MovCaixa> listaMovCaixa = new List<MovCaixa>();
                    foreach (Movimento movimento in movimentos)
                    {
                        if (movimento.MovCaixa != null)
                        {
                            MovCaixa objMovCaixa = MovCaixaController.Instancia.LoadObjectById(movimento.MovCaixa.ID);
                            listaMovCaixa.Add(objMovCaixa);
                            movimentosDeletados.AddRange(objMovCaixa.Movimentos);

                            docsAfetados.AddRange(objMovCaixa.Movimentos.Select(m => m.Documento).Where(d => d.ID != docReparcelado.ID).Distinct());

                            foreach (var item in docReparcelado.Movimentos.Where(w => w.Sequencia == movimento.Sequencia).Select(m => m.MovCaixa.ID).Where(m2 => m2 != objMovCaixa.ID).Distinct())
                            {
                                MovCaixa objMovCaixa1 = MovCaixaController.Instancia.LoadObjectById(item);
                                listaMovCaixa.Add(objMovCaixa1);
                                movimentosDeletados.AddRange(objMovCaixa1.Movimentos);
                            }
                        }
                        else
                        {
                            movimentosDeletados.Add(movimento);
                        }
                    }
                    foreach (var doc in docsAfetados)
                    {

                        foreach (var mov in movimentosDeletados.Where(m => m.Documento.ID == doc.ID))
                        {
                            Movimento objMovimento = new Movimento();
                            objMovimento = doc.Movimentos.Where(w => w.ID == mov.ID).Select(s => s).First();
                            objMovimento.MovCaixa = null;
                            doc.Movimentos.Remove(objMovimento);
                        }
                        doc.RecalcularSaldoESituacao();
                        if (doc.ID != documento.ID)
                            DocumentoController.Instancia.RegisterNewIntoTransaction(doc);
                    }
                    if (listaMovCaixa.Count > 0)
                    {
                        foreach (var item in listaMovCaixa)
                        {
                            item.Movimentos.Clear();
                            MovCaixaController.Instancia.RegisterDeleteIntoTransaction(item);
                        }
                    }
                }
                ReparcelamentoController.Instancia.RegisterDeleteIntoTransaction(reparc);
            }
            return true;
        }

        public int GetNovoCodigoDocumento()
        {
            return repositorioDocumento.GetNovoCodigoDocumento();
        }

        public IList<Documento> GetAllRegistro0400(DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioDocumento.GetAllRegistro0400(dataInicial, dataFinal);
        }
    }
}
