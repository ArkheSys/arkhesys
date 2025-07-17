using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioRotaDetalhe : IRepositorio<RotaDetalhe>
    {
        int getMaxCodigo();
        IList<RotaDetalhe> getListaDetalheRepetido(DateTime dtInicio, DateTime dtFim, int idResponsavel);
    }
}

