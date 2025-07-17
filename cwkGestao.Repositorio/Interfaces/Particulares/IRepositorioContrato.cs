using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioContrato : IRepositorio<Contrato>
    {
        IList<NotaEmMemoriaComContrato> GetNotaComContrato(Pessoa pessoaCliente, DateTime inicio, DateTime fim);
        Contrato GetContratoPorID(int id);

        IList<pxRelatorioProdutoContrato> GetDadosRelatorioProdutosEContratos(int? IDPessoa, DateTime dtInicio, DateTime dtFinal, int? ProdutoInativo);
        IList<pxRelatorioProdutoContratoValor> GetDadosRelatorioProdutosEContratosValor(int? IDPessoa, DateTime dtInicio, DateTime dtFinal, int? ProdutoInativo, int? situacaoPessoa);
    }
}

