using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_Fatura : IRepositorio<Tel_Fatura>
    {
        Tel_Fatura GetByClienteEReferencia(Tel_Cliente cliente, DateTime referencia);

        IList<Tel_Fatura> GetByGrupoEReferencia(Tel_GrupoCliente grupo, DateTime referencia);

        void DesfazerFechamentoFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia);

        void DesfazerFechamentoFaturasPorCliente(Tel_Cliente cliente, DateTime referencia);

        IList<Documento> GetDocumentosAtrasados(Tel_GrupoCliente grupoCliente);

        void ExcluirLoteFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia);

        bool ReferenciaExiste(DateTime referencia, int idPessoa, int idFatura);

        IList<pxyTel_Fatura> GetRelatorioFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia);

        IList<pxyPesquisaTel_Fatura> GetPesquisaFatura(DateTime dataInicio, DateTime dataFim, Tel_Cliente cliente);

        IList<pxyPesquisaTel_Fatura> GetPesquisaFaturaEmLote(DateTime dataInicio, DateTime dataFim, Tel_GrupoCliente grupoCliente);

        IList<pxyPesquisaTel_Fatura> GetPesquisaFaturaCliente(Tel_Cliente cliente);

        IList<pxyTel_Fatura> GetRelatorioFaturas(IList<int> idsFaturas);

        IList<pxyHistoricoFaturaCliente> GetHistoricoClientes(DateTime dataInicial, DateTime dataFinal, IList<int> idsClientes);

        IList<pxyHistoricoFaturaCliente> GetFaturasInadimplencia(DateTime referencia, Tel_GrupoCliente grupoCliente);

        IList<Tel_Fatura> GetAll(DateTime dataInicial);

        Tel_Fatura GetFaturaByDocumento(int idDocumento);
    }
}

