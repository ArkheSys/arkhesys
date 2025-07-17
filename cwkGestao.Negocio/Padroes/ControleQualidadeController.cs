using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ControleQualidadeController : BaseController<ControleQualidade>
	{
		#region Singleton
        private static ControleQualidadeController instance;
        private static IRepositorioControleQualidade repositorioControleQualidade;

        private ControleQualidadeController()
        { }
		
		static ControleQualidadeController()
        {
            instance = new ControleQualidadeController();
            repositorioT = RepositorioFactory<ControleQualidade>.GetRepositorio();
            repositorioControleQualidade = (IRepositorioControleQualidade)repositorioT;
        }
			
		public static ControleQualidadeController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ControleQualidade objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
