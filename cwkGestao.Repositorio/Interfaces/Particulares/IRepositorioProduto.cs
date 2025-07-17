using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProduto : IRepositorio<Produto>
    {
        IList<Produto> GetAllAtivos();
        IList<Produto> GetAllInativos();

        List<TabelaPreco> GetTabelasPreco(Produto produto);
        TabelaPrecoProduto GetTabelaPrecoProduto(TabelaPreco tabelaPreco, Produto produto);
        Produto GetProdutoByCodigo(String codigo);
        IList<Produto> GetProdutoByCodigoString(String codigo);
        Produto GetProdutoByDescricao(String descricao);
        int AtualizaProdutos(GrupoEstoque grupo, String campo, String valor);
        IList<object> GetRelatorio(Modelo.TipoRelatorioProduto tipo, string inicial, string final, bool codigoStr, int Status);
        IList<Produto> GetRelatorioPorGrupoEstoque(string classificacao);

        IList<Produto> FindByName(string p);

        IList<Produto> FindByBarra(string p);

        IList<Produto> FindByBarraObjCompleto(string p);

        Produto FindByCodigoBarra(string barra);

        IList<ProdutoMercadoLivre> GetCodigosMercadoLivre(int idProduto);

        IList<ImpressaoEtiquetaProdutoView> GetImpressaoEtiquetaProduto(int iDProduto);

        Hashtable GetProdutosImportados();

        Hashtable GetProdutosImportadosPorCodigo();

        string GetUltimoCodigoBarrasEAN13();

        IList<pxyListagemPreco> GetListagemPorGrupoEstoqueETabelaPreco(string classificacao, TabelaPreco tabelaPreco);

        IList<pxyListagemPreco> GetListagemPorGrupoEstoqueETabelaPrecoSemInativos(string classificacao, TabelaPreco tabelaPreco);

        string MaxCodigoIntegracao();

        Produto getProdutoByIDIntegracao(string id);

        decimal BuscaCustoMedio(int idFilial, int idProduto, DateTime data);
        decimal CalculaCustoMedio(int idFilial, int idProduto, DateTime data, decimal quantidade, decimal custoUnitario, int idNota);

        decimal GetQtdEstoqueFisico(int IdLocalEstoque, int idProduto);

        IList<pxyEstoque> GetListaEstoque(int idProduto);
        IList<pxyEstoque> GetListaEstoque(int idProduto, int idNota, int idPedido);

        IList<pxyEstoqueProduto> GetListaEstoque(string filiais, GrupoEstoque grupoEstoque, string Situacao, int? IDLocalEstoque, int IdFornecedor);

        IList<pxyEstoqueProduto> GetListaEstoque(string filiais, Produto produto, int? IDLocalEstoque);

        //IList<FichaTecnicaPCP> GetFichasTecnicas();

        string GetIDIntegracaoPorID(int id);

        IList<string> GetAllCodigoBarras();

        decimal GetMargemLucroTabelaPreco(TabelaPreco tabelaPreco, Produto produto);

        IList<Grupo1> GetGrupos1Produto();

        Produto GetProdutoPorCodigoDeBarras(string Barra);

        IList<Produto> GetProdutosPorGrupo1(int IDGrupo);

        IList<Produto> GetProdutosFiltroDelivery(string Descricao);

        //Produto GetProdutoByID(int? iDProduto, int ID);

        Produto GetProdutoByCodigo(int iDProduto);

        IList<Produto> CodigoDeBarrasExistente(string p, int IDProduto);

        void AtualizarEanProduto(int ID, string Barra);

        Produto GetProdutoByID(int iD);
    }
}
