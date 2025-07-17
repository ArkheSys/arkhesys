using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioRegistrosTipo60 : IRepositorio<RegistrosTipo60>
    {
        bool VerificaRegistrosDeHojeSubtipoM();

        bool VerificaRegistrosDeHojeSubtipoA();

        IList<RegistrosTipo60> PegaRegistros(DateTime DataInicial, DateTime DataFinal);

        IList<pxyRegistro60R> GetPxyRegistro60R(DateTime dtIni, DateTime dtFin);
    }
}
