using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioConversaoUnidade : IRepositorio<ConversaoUnidade>
    {
        IList<pxyConversaoUnidade> GetConversaoUnidadeProduto(int idProduto);

        bool ValidaExistenciaConversao(Unidade unidadeEntrada, Unidade unidadeSaida);
    }
}

