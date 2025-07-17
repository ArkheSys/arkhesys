using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPessoaVendedor : IRepositorio<PessoaVendedor>
    {
        IList<object> GetListaClientePorVendedor(string pVendedor, string pLetraInicial, string pLetraFinal, string pEmpresas);

        PessoaVendedor GetVendedorPorIDPessoa(int id);
    }
}

