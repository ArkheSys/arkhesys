using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Cw_grupoController : BaseController<Cw_grupo>
	{
		#region Singleton
        private static Cw_grupoController instance;
        private static IRepositorioCw_grupo repositorioCw_grupo;

        private Cw_grupoController()
        { }
		
		static Cw_grupoController()
        {
            instance = new Cw_grupoController();
            repositorioT = RepositorioFactory<Cw_grupo>.GetRepositorio();
            repositorioCw_grupo = (IRepositorioCw_grupo)repositorioT;
        }
			
		public static Cw_grupoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Cw_grupo objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

         public bool PodeVerPrecoMinimoMaximo(Cw_grupo grupo)
        {
            return grupo.bAutorizadoVerPrecosMinMax;
        }
    }
}
