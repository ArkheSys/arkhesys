using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioNotaItem : IRepositorio<NotaItem>
    {
        IcmsBase RecuperaIcmsOriginal(string siglaUfOrigem, string siglaUfDestino, Produto produto, NotaItem _notaitem);

        decimal SaldoEstoque(string identificador, int id, int idProduto);
        decimal SaldoEstoqueFilial(int idFilial, int idProduto);
        decimal SaldoEstoqueFilialData(int idFilial, int idProduto, DateTime data, int idNota);
        decimal SaldoEstoqueFilialIdentificador(int idFilial, int idProduto, string idIdentificador);
        decimal SaldoEstoqueFilialIdentificadorData(int idFilial, int idProduto, string idIdentificador, DateTime data);
        decimal SaldoEstoqueLocalEstoque(int idLocalEstoque, int idProduto);
        decimal SaldoEstoqueLocalEstoqueData(int idLocalEstoque, int idProduto, DateTime data);
        decimal SaldoEstoqueLocalEstoqueIdentificador(int idLocalEstoque, int idProduto, int idIdentificador);
        decimal SaldoEstoqueLocalEstoqueIdentificadorData(int idLocalEstoque, int idProduto, int idIdentificador, DateTime data);
        IList<object> SaldoEstoqueFilialListaIdentificador(int idFilial, int idProduto);
        IList<object> SaldoEstoqueFilialListaIdentificadorData(int idFilial, int idProduto, DateTime data);
        IList<pxyEstoqueIdentificador> SaldoEstoqueLocalEstoqueListaIdentificador(int idLocalEstoque, int idProduto, int idNota);
        IList<object> SaldoEstoqueLocalEstoqueListaIdentificadorData(int idLocalEstoque, int idProduto, DateTime data);
        IList<pxyMovimentoEstoqueProduto> GetMovimentosEstoque(int idProduto, int idLocalEstoque);
    }
}

