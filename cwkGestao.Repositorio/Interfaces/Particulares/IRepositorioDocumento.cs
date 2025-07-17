using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioDocumento : IRepositorio<Documento>
    {
        IList<Documento> LoadAllByID(List<int> idsDocumentos);

        IList<object> GetContaPorPortador(DateTime dataInicial, DateTime dataFinal, int tipoDoc, string idsEmpresas, string idsportadores, string situacao);

        cwkGestao.Modelo.Documento GetDocumentoPorNumero(Int32 pNumDoc);

        IList<Modelo.Documento> GetDocumentosPorNota(int idNota);
        IList<Modelo.Documento> GetDocumentosPorNumeroPedido(int NumeroPedido);

        IList<pxyRemessa> GetDocumentosPorArquivoRemessa(int ArquivoRemessa);

        IList<pxyDocumentoArquivoRemessa> GetPxyDocumentosArqRemessaPorArquivoRemessa(int ArquivoRemessa);

        IList<Object> GetDocumentoPorFormaPgto(DateTime dataInicial, DateTime dataFinal, String IDsEmpresa, String IDsFormasPagamentos, TipoDocumentoType Tipo, Boolean SemFormaPagto);

        IList<Documento> GetAllPossuiSaldoPorPeriodo(DateTime dataInicial, DateTime dataFinal);

        IList<Documento> GetAllPossuiSaldoPorPeriodoETipoDocumento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo, bool FlagTelefonia);

        IList<Documento> GetAllDocumentoPorClienteEPeriodo(Pessoa cliente, DateTime dataInicial, DateTime dataFinal);

        IList<Documento> GetAllBaixadoPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo);

        IList<Documento> GetAllAbertosPorCliente(Pessoa cliente);

        Decimal GetValorSaldoAllAbertosPorCliente(Pessoa cliente);

        IList<Documento> GetAllPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo, bool FlagTelefonia);

        IList<Documento> GetAllSaldoZeroPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? tipo, bool FlagTelefonia);

        IList<Documento> GetPorProjeto(Projeto projeto);

        long GetNumeroDocumentoValido();

        IList<cwkGestao.Modelo.Proxy.pxyRemessa> GetRemessa(Filial filial, DateTime dataInicial, DateTime dataFinal, bool porVencimento, TipoDocumentoType tipo, bool impresso, bool geradoRemessa, bool geradoPDF, bool enviadoEmail, bool TodosDocumentos);
 
        IList<Documento> GetGerarBoletoRemessa(List<cwkGestao.Modelo.Proxy.pxyRemessa> list);

        IList<cwkGestao.Modelo.Proxy.pxyRemessa> GetRemessaPorNota(Nota nota);

        Documento LoadObjectByCodigo(int codigo);

        IList<Documento> GetAllByID(IList<int> ids);

        IList<Documento> GetDocumentoPorCodigo(int codigo);

        void AtualizaDocumento(string idsdocto, string idremessa);

        IList<pxyDocumentoArquivoRemessa> GetDocumentosParaArquivoRemessa(Filial Filial, DateTime DataInicial, DateTime DataFinal, bool porVencimento, TipoDocumentoType tipo, int? idArquivoRemessa, int? IdTipoDocumento, int? IdPortador);

        int GetNovoCodigoDocumento();

        IList<Documento> GetAllRegistro0400(DateTime dataInicial, DateTime dataFinal);
    }
}

