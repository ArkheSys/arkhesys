using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FormatoPacoteTransporteController : BaseController<FormatoPacoteTransporte>
	{
		#region Singleton
        private static FormatoPacoteTransporteController instance;
        private static IRepositorioFormatoPacoteTransporte repositorioFormatoPacoteTransporte;

        private FormatoPacoteTransporteController()
        { }
		
		static FormatoPacoteTransporteController()
        {
            instance = new FormatoPacoteTransporteController();
            repositorioT = RepositorioFactory<FormatoPacoteTransporte>.GetRepositorio();
            repositorioFormatoPacoteTransporte = (IRepositorioFormatoPacoteTransporte)repositorioT;
        }
			
		public static FormatoPacoteTransporteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FormatoPacoteTransporte objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
