using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositoriocepbr_endereco : IRepositorio<cepbr_endereco>
    {
        cepbr_endereco BuscaInfEnderecos(string cep);
    }
}

