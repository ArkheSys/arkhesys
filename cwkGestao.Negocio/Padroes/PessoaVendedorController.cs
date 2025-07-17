using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class PessoaVendedorController : BaseController<PessoaVendedor>
	{
		#region Singleton
        private static PessoaVendedorController instance;
        private static IRepositorioPessoaVendedor repositorioPessoaVendedor;

        private PessoaVendedorController()
        { }
		
		static PessoaVendedorController()
        {
            instance = new PessoaVendedorController();
            repositorioT = RepositorioFactory<PessoaVendedor>.GetRepositorio();
            repositorioPessoaVendedor = (IRepositorioPessoaVendedor)repositorioT;
        }
			
		public static PessoaVendedorController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaVendedor objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public IList<object> GetListaClientePorVendedor(string pVendedor, string pLetraInicial, string pLetraFinal, string pEmpresas)
        {
            return repositorioPessoaVendedor.GetListaClientePorVendedor(pVendedor, pLetraInicial, pLetraFinal, pEmpresas);
        }

        public PessoaVendedor GetVendedorPorIDPessoa(int id)
        {
            return repositorioPessoaVendedor.GetVendedorPorIDPessoa(id);
        }
	}
}
