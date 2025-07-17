using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class UnidadeController : BaseController<Unidade>
	{
		#region Singleton
        private static UnidadeController instance;
        private static IRepositorioUnidade repositorioUnidade;

        private UnidadeController()
        { }
		
		static UnidadeController()
        {
            instance = new UnidadeController();
            repositorioT = RepositorioFactory<Unidade>.GetRepositorio();
            repositorioUnidade = (IRepositorioUnidade)repositorioT;
        }
			
		public static UnidadeController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Unidade objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
                ret.Add("txtNome", "Campo obrigatório.");

            if (String.IsNullOrEmpty(objeto.Sigla) || objeto.Sigla.Trim() == String.Empty)
                ret.Add("txtSigla", "Campo obrigatório.");

            return ret;
        }
	}
}
