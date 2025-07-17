using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioRateio_Mov : IRepositorio<Rateio_Mov>
    {

        IList<Rateio_Mov> GetRateiosPorProjeto(int p);
        IList<Rateio_Mov> GetRateiosPorMovimentoID(int id);

        int MaxCodigo(Movimento m);
    }
}

