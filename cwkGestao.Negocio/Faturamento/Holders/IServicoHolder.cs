using System;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Faturamento.Holders
{
    public interface IServicoHolder
    {
        Servico Servico { get;}
        decimal Quantidade { get; }
        decimal ValorSvc { get;  }
        decimal ValorSvcOriginal { get; }
        decimal DescontoSvc { get;  }
        decimal TotalSvc { get; }
        DateTime DataSvc { get; }
        PedidoItemServico PedidoItemServico { get; }
    }
}