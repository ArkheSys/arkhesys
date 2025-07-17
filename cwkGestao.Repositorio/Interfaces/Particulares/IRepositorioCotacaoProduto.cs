using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCotacaoProduto : IRepositorio<CotacaoProduto>
    {

        IList<CotacaoProduto> GetByCotacao(Cotacao cotacao);
    }
}

