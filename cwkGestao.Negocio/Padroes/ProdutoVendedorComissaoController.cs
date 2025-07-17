using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProdutoVendedorComissaoController : BaseController<ProdutoVendedorComissao>
	{
		#region Singleton
        private static ProdutoVendedorComissaoController instance;
        private static IRepositorioProdutoVendedorComissao repositorioProdutoVendedorComissao;

        private ProdutoVendedorComissaoController()
        { }
		
		static ProdutoVendedorComissaoController()
        {
            instance = new ProdutoVendedorComissaoController();
            repositorioT = RepositorioFactory<ProdutoVendedorComissao>.GetRepositorio();
            repositorioProdutoVendedorComissao = (IRepositorioProdutoVendedorComissao)repositorioT;
        }
			
		public static ProdutoVendedorComissaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProdutoVendedorComissao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public IList<ProdutoVendedorComissao> GetComissaoProduto(Int32[] indicesProduto, Int32 IDVendedor)
        {
            return repositorioProdutoVendedorComissao.GetComissaoProduto(indicesProduto, IDVendedor);
        }

        public IList<ProdutoVendedorComissao> GetComissaoGrupoEstoque(Int32[] indicesGrupoEstoque, Int32 IDVendedor)
        {
            return repositorioProdutoVendedorComissao.GetComissaoGrupoEstoque(indicesGrupoEstoque, IDVendedor);
        }


        public IList<ProdutoVendedorComissao> GetComissaoServico(Int32[] indicesServico, Int32 IDVendedor)
        {
            return repositorioProdutoVendedorComissao.GetComissaoServico(indicesServico, IDVendedor);
        }


	}
}
