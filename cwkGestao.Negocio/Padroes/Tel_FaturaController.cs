using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public partial class Tel_FaturaController : BaseController<Tel_Fatura>
    {
        private readonly Dictionary<PeriodoListagemFaturasTelefonia, int> periodoQuantidadeMeses = new Dictionary<PeriodoListagemFaturasTelefonia, int>();

        #region Singleton
        private static Tel_FaturaController instance;
        private static IRepositorioTel_Fatura repositorioTel_Fatura;

        private Tel_FaturaController()
        {
            periodoQuantidadeMeses.Add(PeriodoListagemFaturasTelefonia.MesAtual, 0);
            periodoQuantidadeMeses.Add(PeriodoListagemFaturasTelefonia.UltimoMes, -1);
            periodoQuantidadeMeses.Add(PeriodoListagemFaturasTelefonia.Ultimos2Meses, -2);
            periodoQuantidadeMeses.Add(PeriodoListagemFaturasTelefonia.Ultimos3Meses, -3);
            periodoQuantidadeMeses.Add(PeriodoListagemFaturasTelefonia.Ultimos4Meses, -4);
            periodoQuantidadeMeses.Add(PeriodoListagemFaturasTelefonia.Ultimos5Meses, -5);
        }

        static Tel_FaturaController()
        {
            instance = new Tel_FaturaController();
            repositorioT = RepositorioFactory<Tel_Fatura>.GetRepositorio();
            repositorioTel_Fatura = (IRepositorioTel_Fatura)repositorioT;
        }

        public static Tel_FaturaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Tel_Fatura objeto)
        {
            return new Dictionary<string, string>();
        }

        internal Tel_Fatura GetByClienteEReferencia(Tel_Cliente cliente, DateTime referencia)
        {
            return repositorioTel_Fatura.GetByClienteEReferencia(cliente, referencia);
        }

        public IList<Tel_Fatura> GetByGrupoEReferencia(Tel_GrupoCliente grupo, DateTime referencia)
        {
            return repositorioTel_Fatura.GetByGrupoEReferencia(grupo, referencia);
        }

        internal IList<Documento> GetDocumentosAtrasados(Tel_GrupoCliente grupoCliente)
        {
            return repositorioTel_Fatura.GetDocumentosAtrasados(grupoCliente);
        }

        public bool ReferenciaExiste(DateTime referencia, int idPessoa, int idFatura)
        {
            return repositorioTel_Fatura.ReferenciaExiste(referencia, idPessoa, idFatura);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyPesquisaTel_Fatura> GetPesquisaFatura(DateTime dataInicio, DateTime dataFim, Tel_Cliente cliente)
        {
            return repositorioTel_Fatura.GetPesquisaFatura(dataInicio, dataFim, cliente);
        }
        
        public IList<cwkGestao.Modelo.Proxy.pxyPesquisaTel_Fatura> GetPesquisaFaturaEmLote(DateTime dataInicio, DateTime dataFim, Tel_GrupoCliente clientes)
        {
            return repositorioTel_Fatura.GetPesquisaFaturaEmLote(dataInicio, dataFim, clientes);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyPesquisaTel_Fatura> GetPesquisaFaturaCliente(Tel_Cliente cliente)
        {
            return repositorioTel_Fatura.GetPesquisaFaturaCliente(cliente);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_Fatura> GetRelatorioFaturas(IList<int> idsFaturas)
        {
            return repositorioTel_Fatura.GetRelatorioFaturas(idsFaturas);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyHistoricoFaturaCliente> GetHistoricoClientes(DateTime dataInicial, DateTime dataFinal, IList<int> idsClientes)
        {
            return repositorioTel_Fatura.GetHistoricoClientes(dataInicial, dataFinal, idsClientes);
        }

        public void DesfazerFechamentoFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            repositorioTel_Fatura.DesfazerFechamentoFaturas(grupoCliente, referencia);
        }

        public void DesfazerFechamentoFaturasPorCliente(Tel_Cliente cliente, DateTime referencia)
        {
            repositorioTel_Fatura.DesfazerFechamentoFaturasPorCliente(cliente, referencia);
        }

        public void ExcluirLoteFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            repositorioTel_Fatura.ExcluirLoteFaturas(grupoCliente, referencia);
        }

        public IList<pxyTel_Fatura> GetRelatorioFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            return repositorioTel_Fatura.GetRelatorioFaturas(grupoCliente, referencia);
        }

        public IList<pxyHistoricoFaturaCliente> GetFaturasInadimplencia(DateTime referencia, Tel_GrupoCliente grupoCliente)
        {
            return repositorioTel_Fatura.GetFaturasInadimplencia(referencia, grupoCliente);
        }

        public IList<Tel_Fatura> GetAllGridFaturas()
        {
            var periodo = Tel_ConfiguracaoController.Instancia.GetConfiguracao().PeriodoListagemFaturas;
            
            if (periodo == PeriodoListagemFaturasTelefonia.Todos)
                return GetAll();

            DateTime dataInicial = DateTime.Now.AddMonths(periodoQuantidadeMeses[periodo]);
            dataInicial = new DateTime(dataInicial.Year, dataInicial.Month, 1);
            return GetAll(dataInicial);
        }

        private IList<Tel_Fatura> GetAll(DateTime dataInicial)
        {
            return repositorioTel_Fatura.GetAll(dataInicial);
        }

        public Tel_Fatura GetFaturaByDocumento(int idDocumento)
        {
            return repositorioTel_Fatura.GetFaturaByDocumento(idDocumento);
        }
    }
}
