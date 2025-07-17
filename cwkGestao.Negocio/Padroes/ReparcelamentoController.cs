using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ReparcelamentoController : BaseController<Reparcelamento>
    {
        #region Singleton
        private static ReparcelamentoController instance;
        private static IRepositorioReparcelamento repositorioReparcelamento;

        private ReparcelamentoController()
        { }

        static ReparcelamentoController()
        {
            instance = new ReparcelamentoController();
            repositorioT = RepositorioFactory<Reparcelamento>.GetRepositorio();
            repositorioReparcelamento = (IRepositorioReparcelamento)repositorioT;
        }

        public static ReparcelamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Reparcelamento objeto)
        {
            return new Dictionary<string, string>();
        }

        public void GerarReparcelamentoIndividual(cwkGestao.Modelo.Auxiliares.ParametrosReparcelamento parametros)
        {
            Reparcelamento reparcelamento = new Reparcelamento();
            reparcelamento.Data_Base = parametros.DataBase;
            reparcelamento.Data = DateTime.Today;
            reparcelamento.Juros = parametros.Juros == 0 ? parametros.Acrescimo.PercJuro : parametros.Juros;
            reparcelamento.Pessoa = parametros.Cliente;

            foreach (var documento in parametros.Documentos)
            {
                ReparcelamentoDoc repDoc = new ReparcelamentoDoc();

                repDoc.Tipo = false;
                repDoc.Documento = documento;
                repDoc.Reparcelamento = reparcelamento;

                reparcelamento.ReparcelamentoDocs.Add(repDoc);
                reparcelamento.ValorTotal += documento.Saldo;

                Financeiro.BaixaDocumento baixaDocumento = new Financeiro.BaixaDocumento(new ParametrosBaixa() { ValorBaixa = documento.Saldo, DataBaixa = DateTime.Today, Documento = documento, Renegociacao = parametros.Renegociacao });
                baixaDocumento.BaixarDocumentos(new List<MovCaixa>());
            }

            foreach (var parcela in parametros.Parcelas)
            {
                ReparcelamentoDoc doc = GeraDocumentoReparcelamento(parcela, parametros);
                doc.Reparcelamento = reparcelamento;
                reparcelamento.ReparcelamentoDocs.Add(doc);
            }

            ReparcelamentoController.Instancia.RegisterNewIntoTransaction(reparcelamento);
            ReparcelamentoController.Instancia.CommitUnitOfWorkTransaction();
        }

        private ReparcelamentoDoc GeraDocumentoReparcelamento(PedidoParcela parcela, ParametrosReparcelamento parametros)
        {
            Documento novoDoc = new Documento();
            ReparcelamentoDoc repDoc = new ReparcelamentoDoc();
            repDoc.Documento = novoDoc;
            repDoc.Tipo = true;

            novoDoc.DtDigitacao = DateTime.Today;
            novoDoc.DtPrevisao = parcela.Vencimento;
            novoDoc.DtUltimaBaixa = null;
            novoDoc.Acrescimo = parametros.Acrescimo;
            novoDoc.Portador = parametros.Portador;
            novoDoc.TipoDocumento = parametros.TipoDocumento;
            novoDoc.Banco = parametros.Banco;
            novoDoc.Codigo = DocumentoController.Instancia.GetNovoCodigoDocumento();
            novoDoc.NumDocumento = "R" + novoDoc.Codigo.ToString();
            novoDoc.ComplementoHist = parametros.ComplementoHistorico;
            novoDoc.Condicao = parametros.Condicao;
            novoDoc.Dt = DateTime.Today;
            novoDoc.DtVencimento = parcela.Vencimento;
            novoDoc.Filial = parametros.Fililal;
            novoDoc.ValorTotal = parametros.Parcelas.Sum(parc => parc.Valor);
            novoDoc.Historico = parametros.Historico;
            novoDoc.Movimentos = new List<Movimento>()
                                     {
                                         new Movimento()
                                             {
                                                 Documento = novoDoc,
                                                 Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento(),
                                                 Dt = DateTime.Today.Date,
                                                 Operacao = OperacaoMovimentoType.Lancamento,
                                                 Sequencia = 1,
                                                 Tipo = TipoMovimentoType.Documento,
                                                 Valor = parcela.Valor,
                                                 PlanoConta = ConfiguracaoController.Instancia.GetConfiguracao().PlanoContaRenegociacao,
                                                 Historico = parametros.Historico,
                                                 ComplementoHist = parametros.ComplementoHistorico,
                                             }
                                     };
            novoDoc.Situacao = "Norm";
            novoDoc.Parcela = parcela.Parcela;
            novoDoc.QtParcela = parametros.Condicao.QtParcela;
            novoDoc.Pessoa = parametros.Cliente;
            novoDoc.ReparcelamentoDocs = parametros.Documentos;
            novoDoc.Tipo = TipoDocumentoType.Receber;
            novoDoc.Valor = parcela.Valor;
            return repDoc;
        }

        public IList<Reparcelamento> LoadByIdReparcelamentoDoc(int idReparcelamentoDoc)
        {
            return repositorioReparcelamento.LoadByIdReparcelamentoDoc(idReparcelamentoDoc);
        }

        public IList<Reparcelamento> LoadListByIdReparcelamentoDoc(List<int> idsReparcelamentoDoc)
        {
            return repositorioReparcelamento.LoadListByIdReparcelamentoDoc(idsReparcelamentoDoc);
        }

        public IList<Reparcelamento> LoadListByIdReparcelamento(List<int> idsReparcelamentos)
        {
            return repositorioReparcelamento.LoadListByIdReparcelamento(idsReparcelamentos);
        }
    }
}
