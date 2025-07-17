using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCidade : IRepositorio<Cidade>
    {
        System.Collections.Hashtable GetHashNomeCidade();

        Cidade GetByLikeName(string nome, string UF);

        Cidade GetCidadePorIbge(string CodigoIbge);
    }
}
