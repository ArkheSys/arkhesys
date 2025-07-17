using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class PessoaEngenheiroController : BaseController<PessoaEngenheiro>
	{
		#region Singleton
        private static PessoaEngenheiroController instance;
        private static IRepositorioPessoaEngenheiro repositorioPessoaEngenheiro;

        private PessoaEngenheiroController()
        { }

        static PessoaEngenheiroController()
        {
            instance = new PessoaEngenheiroController();
            repositorioT = RepositorioFactory<PessoaEngenheiro>.GetRepositorio();
            repositorioPessoaEngenheiro = (IRepositorioPessoaEngenheiro)repositorioT;
        }
			
		public static PessoaEngenheiroController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaEngenheiro objeto)
        {
            return new Dictionary<string,string>();
        }
	}
}
