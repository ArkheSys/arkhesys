using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioIdentificador : IRepositorio<Identificador>
    {
        int GetProximaSequenciaAno(int ano);
        Identificador getByAnoSequencia(int ano, int sequencia);
    }
}

