using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MercadoLivreConfiguracaoRepositorio : RepositorioAbstrato<MercadoLivreConfiguracao>, IRepositorioMercadoLivreConfiguracao
    {
        protected override string HqlLoadById()
        {
            return @"from MercadoLivreConfiguracao n  
                     left join fetch n.Pessoa 
                     left join fetch n.TabelaPreco 
                     left join fetch n.Condicao 
                     left join fetch n.LocalEstoque 
                     left join fetch n.TipoPedido 
                     left join fetch n.Filial 
                     where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"from MercadoLivreConfiguracao n 
                     left join fetch n.Fornecedor f
                     left join fetch n.Vendedor v
                     left join fetch n.TabelaPreco tp
                     left join fetch n.Condicao c
                     left join fetch c.CondicaoParcelas ccp
                     left join fetch n.LocalEstoque le
                     left join fetch n.TipoOrcamento tp
                     left join fetch n.Filial f";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
