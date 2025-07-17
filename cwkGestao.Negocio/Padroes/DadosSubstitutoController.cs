using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class DadosSubstitutoController : BaseController<DadosSubstituto>
	{
		#region Singleton
        private static DadosSubstitutoController instance;
        private static IRepositorioDadosSubstituto repositorioDadosSubstituto;

        private DadosSubstitutoController()
        { }
		
		static DadosSubstitutoController()
        {
            instance = new DadosSubstitutoController();
            repositorioT = RepositorioFactory<DadosSubstituto>.GetRepositorio();
            repositorioDadosSubstituto = (IRepositorioDadosSubstituto)repositorioT;
        }
			
		public static DadosSubstitutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(DadosSubstituto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<DadosSubstituto> GetByIDFilial(int IDFilial)
        {
            return repositorioDadosSubstituto.GetByIDFilial(IDFilial);
        }
	}
}
