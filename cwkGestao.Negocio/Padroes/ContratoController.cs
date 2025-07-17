using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Negocio.Faturamento;
using cwkGestao.Negocio.Utils;
using System.Windows.Forms;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public class ContratoController : BaseController<Contrato>
	{
		#region Singleton
        private static ContratoController instance;
        private static IRepositorioContrato repositorioContrato;

        private ContratoController()
        { }
		
		static ContratoController()
        {
            instance = new ContratoController();
            repositorioT = RepositorioFactory<Contrato>.GetRepositorio();
            repositorioContrato = (IRepositorioContrato)repositorioT;
        }
			
		public static ContratoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Contrato objeto)
        {
            return new Dictionary<string, string>();
        }

        public Contrato GetContratoPorID(int id)
        {
            return repositorioContrato.GetContratoPorID(id);
        }

        public IList<NotaEmMemoriaComContrato> GetNotaComContrato(Pessoa pessoaCliente, DateTime inicio, DateTime fim)
        {
            var notasObjeto = repositorioContrato.GetNotaComContrato(pessoaCliente, inicio, fim);
            IList<NotaEmMemoriaComContrato> notasEmMemoria = new List<NotaEmMemoriaComContrato>(); 


            foreach (var itemNota in notasObjeto)
            {
                PopulaObjeto(itemNota);
                notasEmMemoria.Add(itemNota);
            }

            return notasEmMemoria;

            
        }

        private static void PopulaObjeto(NotaEmMemoriaComContrato notaEmMemoria)
        {
            var notaCarregada = NotaController.Instancia.LoadObjectById(notaEmMemoria.idNota);
            var documentoCarregado = DocumentoController.Instancia.LoadObjectById(notaEmMemoria.idDocumento);
            var contratoCarregado = ContratoController.Instancia.GetContratoPorID(notaEmMemoria.idContrato);
            var pessoaCarregada = PessoaController.Instancia.LoadObjectById(notaEmMemoria.idPessoa);
            var pessoaEmail = PessoaEmailController.Instancia.GetEmailNFSe(pessoaCarregada.ID);

            IList<string> emailString = new List<string>();

            notaEmMemoria.dataDeVencimento = documentoCarregado.DtVencimento;
            notaEmMemoria.razaoSocial = pessoaCarregada.Nome;
            notaEmMemoria.fantasia = pessoaCarregada.Fantasia;
            notaEmMemoria.valor = notaCarregada.TotalNota;
            notaEmMemoria.numDocFinanceiro = documentoCarregado.NumDocumento;

            if (pessoaEmail != null)
            {
                foreach (var item in pessoaEmail)
	            {
                    emailString.Add(item.Email);
	            }

                notaEmMemoria.emailsVinculado = emailString;
            }
            else
            {
                notaEmMemoria.emailsVinculado = null;
            }
            if (documentoCarregado.BEnviadoEmail == true)
            {
                notaEmMemoria.dataEmailEnviado = documentoCarregado.DtEmailEnviado;// == null ? DateTime.MinValue : documentoCarregado.DtEmailEnviado;
            }
            else
            {
                notaEmMemoria.dataEmailEnviado = null;    
            }

            notaEmMemoria.idDocumento = notaEmMemoria.idDocumento;
            notaEmMemoria.idNota = notaEmMemoria.idNota;
            notaEmMemoria.idContrato = notaEmMemoria.idContrato;
            if (notaCarregada.Status == null)
            {
                notaEmMemoria.status = "-1";
            }
            else
            {
                notaEmMemoria.status = notaCarregada.Status;
            }
            
            switch (notaEmMemoria.status)
            {
                case "-1":
                    notaEmMemoria.status = "Não enviada";
                    break;
                case "0":
                    notaEmMemoria.status = "Negada";
                    break;
                case "1":
                    notaEmMemoria.status = "Aguardando";
                    break;
                case "2":
                    notaEmMemoria.status = "Autorizada";
                    break;
                case "3":
                    notaEmMemoria.status = "Cancelada";
                    break;
                default:
                    notaEmMemoria.status = "";
                    break;
            }

            
        }

        public static void AtualizarDataEnvioEmail(NotaEmMemoriaComContrato item)
        {
            Documento doc = DocumentoController.Instancia.LoadObjectById(item.idDocumento);
            doc.DtEmailEnviado = DateTime.Now;
            doc.BEnviadoEmail = true;
            item.dataEmailEnviado = doc.DtEmailEnviado;
            DocumentoController.Instancia.Salvar(doc, Acao.Alterar);
        }

       

        public string GerarNotaContrato(Contrato contrato, DateTime vencimento)
        {
            Nota nota = new Nota();
            NotaBuilder notaBuilder = new NotaBuilder(nota);
            string ErrosContratos = "";

            DateTime? dataVencimento = null;

            foreach (var s in contrato.Servicos)
            {
                if (s.BJaneiro && DateTime.Now.Month == 01)
                    dataVencimento = vencimento;

                if (s.BFevereiro && DateTime.Now.Month == 02)
                    dataVencimento = vencimento;

                if (s.BMarco && DateTime.Now.Month == 03)
                    dataVencimento = vencimento;

                if (s.BAbril && DateTime.Now.Month == 04)
                    dataVencimento = vencimento;

                if (s.BMaio && DateTime.Now.Month == 05)
                    dataVencimento = vencimento;

                if (s.BJunho && DateTime.Now.Month == 06)
                    dataVencimento = vencimento;

                if (s.BJulho && DateTime.Now.Month == 07)
                    dataVencimento = vencimento;

                if (s.BAgosto && DateTime.Now.Month == 08)
                    dataVencimento = vencimento;

                if (s.BSetembro && DateTime.Now.Month == 09)
                    dataVencimento = vencimento;

                if (s.BOutubro && DateTime.Now.Month == 10)
                    dataVencimento = vencimento;

                if (s.BNovembro && DateTime.Now.Month == 11)
                    dataVencimento = vencimento;

                if (s.BDezembro && DateTime.Now.Month == 12)
                    dataVencimento = vencimento;
            }

            if (dataVencimento == null)
                ErrosContratos += "\nCódigo: " + contrato.Codigo + "\n";

            if (ErrosContratos == "")
            {
                Condicao condicao = CondicaoController.Instancia.LoadObjectById(contrato.Condicao.ID);

                notaBuilder.PreencherNotaComTipoNota(contrato.TipoNota);
                notaBuilder.PreencherDadosBasicosNota(FilialController.Instancia.GetFilialPrincipal(), dataVencimento.Value.Date, String.Empty, String.Empty, String.Empty, null, TipoFrete.SemFrete);
                notaBuilder.PreencherNotaComPessoaCondicaoVendedoreTabelaPreco(contrato.Pessoa, condicao, contrato.PessoaVendedor, null, null);
            }

            foreach (var s in contrato.Servicos)
            {
                if (s.DataCancelamento == null)
                {
                    var notaServicoItem = new Modelo.NotaItemServico();

                    notaServicoItem.Servico = s.Servico;
                    notaServicoItem.NomeServico = s.Servico.Nome;
                    notaServicoItem.Quantidade = s.Quantidade;
                    notaServicoItem.Valor = s.Valor;
                    notaServicoItem.SubTotal = s.Total;
                    notaServicoItem.PercDesconto = 0;
                    notaServicoItem.Nota = nota;
                    nota.NotaItemsServicos.Add(notaServicoItem);
                }
            }

            if (nota.NotaItemsServicos.Count() == 0)
                if (ErrosContratos == "")
                    ErrosContratos += "\nCódigo: " + contrato.Codigo + "\n";

            if (ErrosContratos == "")
            {
                notaBuilder.SetTotaisNota();

                foreach (var parcela in CondicaoController.Instancia.GerarParcelas(nota))
                {
                    nota.NotaParcelas.Add(parcela);
                }

                NotaController.Instancia.RegisterNewIntoTransaction(nota);

                if (nota.BGeraFinanceiro)
                    NotaController.Instancia.GeraFinanceiro(nota, null);

                NotaController.Instancia.CommitUnitOfWorkTransaction();

                return null;
            }
            else
                return ErrosContratos;
        }

        public IList<pxRelatorioProdutoContrato> GetDadosRelatorioProdutosEContratos(int? IDPessoa, DateTime dtInicio, DateTime dtFinal, int? ProdutoInativo)
        {
            return repositorioContrato.GetDadosRelatorioProdutosEContratos(IDPessoa, dtInicio, dtFinal, ProdutoInativo);
        }

        public IList<pxRelatorioProdutoContratoValor> GetDadosRelatorioProdutosEContratosValor(int? IDPessoa, DateTime dtInicio, DateTime dtFinal, int? ProdutoInativo, int? situacaoPessoa)
        {
            return repositorioContrato.GetDadosRelatorioProdutosEContratosValor(IDPessoa, dtInicio, dtFinal, ProdutoInativo, situacaoPessoa);
        }
    }
}
