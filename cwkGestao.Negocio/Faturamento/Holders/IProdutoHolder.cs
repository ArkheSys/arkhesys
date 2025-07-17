using System;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Faturamento.Holders
{
    public interface IProdutoHolder
    {
        Produto Produto { get;}
        decimal Quantidade { get; }
        string Unidade { get;  }
        decimal Preco { get;  }
        decimal PrecoOriginal { get;  }
        decimal DescontoPerc { get;  }
        decimal Total { get; }
        DateTime Data { get; }
        PedidoItem PedidoItem { get; }
        string InformacoesAdicionais { get; }
    }
}