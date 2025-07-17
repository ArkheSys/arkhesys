using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class PessoaClienteController : BaseController<PessoaCliente>
	{
		#region Singleton
        private static PessoaClienteController instance;
        private static IRepositorioPessoaCliente repositorioPessoaCliente;

        private PessoaClienteController()
        { }
		
		static PessoaClienteController()
        {
            instance = new PessoaClienteController();
            repositorioT = RepositorioFactory<PessoaCliente>.GetRepositorio();
            repositorioPessoaCliente = (IRepositorioPessoaCliente)repositorioT;
        }
			
		public static PessoaClienteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaCliente objeto)
        {
            return new Dictionary<string,string>();
        }

        public IList<Modelo.Pessoa> GetRevendas()
        {
            return repositorioPessoaCliente.GetRevendas();
        }
	}
}
