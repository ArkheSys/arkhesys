using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaPerfuracoesController : BaseController<Ofd_PropostaPerfuracoes>
	{
		#region Singleton
        private static Ofd_PropostaPerfuracoesController instance;
        private static IRepositorioOfd_PropostaPerfuracoes repositorioOfd_PropostaPerfuracoes;

        private Ofd_PropostaPerfuracoesController()
        { }
		
		static Ofd_PropostaPerfuracoesController()
        {
            instance = new Ofd_PropostaPerfuracoesController();
            repositorioT = RepositorioFactory<Ofd_PropostaPerfuracoes>.GetRepositorio();
            repositorioOfd_PropostaPerfuracoes = (IRepositorioOfd_PropostaPerfuracoes)repositorioT;
        }
			
		public static Ofd_PropostaPerfuracoesController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaPerfuracoes objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
