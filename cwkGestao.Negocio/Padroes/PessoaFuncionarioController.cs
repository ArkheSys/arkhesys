using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaFuncionarioController : BaseController<PessoaFuncionario>
	{
		#region Singleton
        private static PessoaFuncionarioController instance;
        private static IRepositorioPessoaFuncionario repositorioPessoaFuncionario;

        private PessoaFuncionarioController()
        { }
		
		static PessoaFuncionarioController()
        {
            instance = new PessoaFuncionarioController();
            repositorioT = RepositorioFactory<PessoaFuncionario>.GetRepositorio();
            repositorioPessoaFuncionario = (IRepositorioPessoaFuncionario)repositorioT;
        }
			
		public static PessoaFuncionarioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaFuncionario objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public PessoaFuncionario GetVendedorAssociadoComUsuarioLogado(string login)
        {
            return repositorioPessoaFuncionario.GetVendedorAssociadoComUsuarioLogado(login);
        }
	}
}
