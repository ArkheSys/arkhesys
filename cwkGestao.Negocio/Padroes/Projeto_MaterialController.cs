using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Projeto_MaterialController : BaseController<Projeto_Material>
	{
		#region Singleton
        private static Projeto_MaterialController instance;
        private static IRepositorioProjeto_Material repositorioProjeto_Material;

        private Projeto_MaterialController()
        { }
		
		static Projeto_MaterialController()
        {
            instance = new Projeto_MaterialController();
            repositorioT = RepositorioFactory<Projeto_Material>.GetRepositorio();
            repositorioProjeto_Material = (IRepositorioProjeto_Material)repositorioT;
        }
			
		public static Projeto_MaterialController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Projeto_Material objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
