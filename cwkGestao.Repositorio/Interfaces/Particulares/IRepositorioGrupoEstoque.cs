using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioGrupoEstoque : IRepositorio<GrupoEstoque>
    {
        IList<GrupoEstoque> GetGruposEstoquePorClassificacao(string classificacao);
    }
}

