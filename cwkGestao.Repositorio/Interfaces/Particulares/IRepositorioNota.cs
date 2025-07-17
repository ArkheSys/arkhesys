using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioNota : IRepositorio<Nota>
    {
        Int32 GetNovoNumeroSerie(Nota nota, int? Serie);
        Int32 GetNovoNumeroNFSe(Nota nota);
        int GetNovoNumeroLoteNFSe();

        IList<Nota> GetNotasPorClienteNoPeriodo(Pessoa cliente, DateTime inicio, DateTime fim);

        IList<Nota> GetAll(InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas);
        IList<Nota> GetAllPeriodo(DateTime dataInicial, DateTime dataFinal, InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas);
        IList<Nota> GetAutorizadasPorCliente(InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas, int idPessoa, string statusNota);

        IList<Nota> GetNotaAutorizadaPorPedido(int CodigoPedido, bool Autorizada = true);

        IList<NotaItem> GetProdutoVendaPorClienteNoPeriodo(Pessoa pessoa, Produto produto, DateTime dateTime, DateTime dateTime_3, InOutType inOut);

        Nota GetByCodigo(int p);

        Nota GetByPedido(Pedido pedido);

        Nota GetNotaComplementar(Nota nota);

        IList<Nota> GetAllComplementares(InOutType inOut);

        IList<Nota> GetAllRequisicoesBaixadas(InOutType inOut);

        IList<Nota> GetNotasPorFilialNoPeriodo(Filial filial, DateTime inicio, DateTime fim);

        IList<Nota> GetNotasValidaParana(Filial filial, DateTime inicio, DateTime fim, Modelo.InOutType? pEntSai, bool cupom);

        IList<Nota> GetNotasItensValidaParana(Filial filial, DateTime inicio, DateTime fim, Modelo.InOutType? pEntSai, bool cupom);

        Nota GetBySerieENumero(string serie, int numero);

        IList<Nota> GetNotasPorImpostoENota(ServicoImposto servicoImposto, Nota nota);

        IList<pxyCreditoDebito> GetNotasCreDeb(DateTime dtInicio, DateTime dtFinal, int idEmpresa);

        IList<Nota> VerificaSeExisteNFSeComNumeroNoBD(int numRPS);

        Nota GetByNumeroETipoNota(int numero, int tipoNota);

        IList<pxMargemLucroProduto> GetMargemdeLucroProdutos(DateTime dataInicial, DateTime dataFinal, int IDEmpresa, string ativos, string modeloDocumento, string idProduto);

        Nota GetUltimoCupomEmitido();

        int GetByIdOSOrdemServico(int idNota);

        int GetByIdOSOrdemServicoProduto(int idNota);

        int GetByIdNotaPorIdOSServico(int idOSOrdemServico);
        int GetByIdNotaPorIdOSProduto(int idOSOrdemServico);
        int GetNumeroPedidoPorCodigoPedido(int codigoPedido);

        IList<Nota> LoadAllByIds(List<int> idsNotas);

        int GetNotaPelaChave(string ChaveNota);

        IList<pxRelatorioProdutosMaisvendidos> GetProdutosMaisVendidos(int IDEmpresa, int? IDPessoa, DateTime DataInicio, DateTime DataFinal, int Status = 2, string Ativos = "", string modeloDocto = "");
        // IList<pxRelatorioProdutosMaisvendidos> GetProdutosMargemDeLucro(int IDEmpresa, DateTime DataInicio, DateTime DataFinal, int Status = 2, string Ativos = "", string modeloDocto = "");

        int GetNovoCodigoNota();
        int GetNovoCodigoFrenteCaixaNota();
        IList<pxRelatorioItensCanceladosPDV> GetItensCanceladosPDV(int IDEmpresa, DateTime DataInicial, DateTime DataFinal, int? codigoInicial, int? codigoFinal);
    }
}
