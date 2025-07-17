using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProdutoVendedorComissao : IRepositorio<ProdutoVendedorComissao>
    {
        IList<ProdutoVendedorComissao> GetComissaoProduto(Int32[] indicesProduto, Int32 IDVendedor);
        IList<ProdutoVendedorComissao> GetComissaoGrupoEstoque(Int32[] indicesGrupoEstoque, Int32 IDVendedor);
        IList<ProdutoVendedorComissao> GetComissaoServico(Int32[] indicesServico, Int32 IDVendedor);
    }
}

