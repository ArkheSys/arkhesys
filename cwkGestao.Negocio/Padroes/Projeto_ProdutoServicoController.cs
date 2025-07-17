using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Projeto_ProdutoServicoController : BaseController<Projeto_ProdutoServico>
	{
		#region Singleton
        private static Projeto_ProdutoServicoController instance;
        private static IRepositorioProjeto_ProdutoServico repositorioProjeto_ProdutoServico;

        private Projeto_ProdutoServicoController()
        { }
		
		static Projeto_ProdutoServicoController()
        {
            instance = new Projeto_ProdutoServicoController();
            repositorioT = RepositorioFactory<Projeto_ProdutoServico>.GetRepositorio();
            repositorioProjeto_ProdutoServico = (IRepositorioProjeto_ProdutoServico)repositorioT;
        }
			
		public static Projeto_ProdutoServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Projeto_ProdutoServico objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
