using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio.Financeiro.BoletoRemessa;

namespace cwkGestao.Negocio.Telefonia
{
    public class FechadorFaturas
    {
        private readonly IList<Tel_Fatura> faturas;
        private readonly Tel_GrupoCliente grupoCliente;
        private readonly Tel_Cliente cliente;
        private readonly Banco banco;
        private readonly Filial filial;
        private readonly Tel_Configuracao configuracao;
        private readonly Remessa remessa;
        private readonly string referenciaStr;

        public FechadorFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia, Banco banco, Remessa remessa, Tel_Cliente cliente)
        {
            this.banco = banco;
            this.grupoCliente = grupoCliente;
            this.cliente = cliente;
            this.remessa = remessa;
            if (cliente == null)
                this.faturas = Tel_FaturaController.Instancia.GetByGrupoEReferencia(grupoCliente, referencia);
            else
            {
                this.faturas = new List<Tel_Fatura>();
                var fatura = Tel_FaturaController.Instancia.GetByClienteEReferencia(cliente, referencia);
                if (fatura != null)
                    this.faturas.Add(fatura);
            }
            this.filial = FilialController.Instancia.GetFilialPrincipal();
            this.configuracao = Tel_ConfiguracaoController.Instancia.GetConfiguracao();
            this.referenciaStr = String.Format("{0:00}{1:0000}", referencia.Month, referencia.Year);
        }

        public void Fechar()
        {
            if (faturas.Count == 0)
                throw new Exception("Não foram encontradas faturas para o filtro selecionado.");
            foreach (var fatura in faturas)
            {
                MontarDocumentoDaFatura(fatura);
                DocumentoController.Instancia.RegisterNewIntoTransaction(fatura.Documento);
                Tel_FaturaController.Instancia.RegisterNewIntoTransaction(fatura);
            }
            Tel_FaturaController.Instancia.CommitUnitOfWorkTransaction();
        }

        public LogicaImpressaoBoletoRemessa GetLogicaBoleto()
        {
            LogicaImpressaoBoletoRemessa logicaBoleto = new LogicaImpressaoBoletoRemessa();
            logicaBoleto.Remessa = remessa;
            logicaBoleto.Documentos = GetDocumentosParaBoletos();

            return logicaBoleto;
        }

        private List<cwkGestao.Modelo.Proxy.pxyRemessa> GetDocumentosParaBoletos()
        {
            var ret = new List<cwkGestao.Modelo.Proxy.pxyRemessa>();
            foreach (var fat in faturas)
            {
                ret.Add(new cwkGestao.Modelo.Proxy.pxyRemessa
                {
                    ID = fat.Documento.ID,
                    Selecionado = true
                });
            }
            return ret;
        }

        private void MontarDocumentoDaFatura(Tel_Fatura fatura)
        {
            Documento documento = new Documento();
            documento.Parcela = 1;
            documento.Tipo = TipoDocumentoType.Receber;
            documento.Situacao = "Norm";
            documento.DtDigitacao = DateTime.Today.Date;
            documento.Codigo = DocumentoController.Instancia.GetNovoCodigoDocumento();
            documento.NumDocumento = referenciaStr;
            documento.Pessoa = fatura.Cliente.Pessoa;
            documento.Valor = fatura.Valor;
            documento.ValorTotal = fatura.Valor;
            documento.Saldo = documento.Valor;
            documento.DtVencimento = fatura.Vencimento;
            documento.DtPrevisao = fatura.Vencimento;
            documento.Banco = banco;
            documento.Banco_Str = banco.Nome;
            documento.Dt = fatura.Data; //??
            Tel_Configuracao config = Tel_ConfiguracaoController.Instancia.GetConfiguracao();
            if (config.Filial != null)
            {
                documento.Filial = config.Filial;
            }
            else
            {
                documento.Filial = filial;
            }
            documento.Condicao = configuracao.Condicao;
            documento.Historico = configuracao.Historico;
            documento.ComplementoHist = configuracao.Historico.Nome;
            documento.Portador = configuracao.Portador;
            documento.TipoDocumento = configuracao.TipoDocumento;
            documento.Acrescimo = BuscarAcrescimo(fatura);

            documento.SubistituaTagsComplementoHistorico();
            IncluirMovimento(documento);
            fatura.Documento = documento;
        }

        private Acrescimo BuscarAcrescimo(Tel_Fatura fatura)
        {
            if (fatura.Cliente.Acrescimo != null)
                return fatura.Cliente.Acrescimo;

            if (grupoCliente.Acrescimo != null)
                return grupoCliente.Acrescimo;

            if (configuracao.Acrescimo != null)
                return configuracao.Acrescimo;

            return null;
        }

        private void IncluirMovimento(Documento documento)
        {
            var objMovimento = new Modelo.Movimento();
            objMovimento.Sequencia = 1;
            objMovimento.Tipo = TipoMovimentoType.Documento;
            objMovimento.Operacao = OperacaoMovimentoType.Lancamento;
            objMovimento.Codigo = MovimentoController.Instancia.GetNovoCodigoMovimento();
            objMovimento.PlanoConta = configuracao.PlanoConta; 
            objMovimento.Documento = documento;
            objMovimento.Historico = documento.Historico;
            objMovimento.ComplementoHist = documento.ComplementoHist;
            objMovimento.Dt = documento.Dt;
            objMovimento.Valor = documento.ValorTotal;

            documento.Movimentos.Add(objMovimento);
        }
    }
}
