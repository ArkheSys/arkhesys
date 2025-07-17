using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class RotaDetalheController : BaseController<RotaDetalhe>
	{
		#region Singleton
        private static RotaDetalheController instance;
        private static IRepositorioRotaDetalhe repositorioRotaDetalhe;

        private RotaDetalheController()
        { }
		
		static RotaDetalheController()
        {
            instance = new RotaDetalheController();
            repositorioT = RepositorioFactory<RotaDetalhe>.GetRepositorio();
            repositorioRotaDetalhe = (IRepositorioRotaDetalhe)repositorioT;
        }
			
		public static RotaDetalheController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(RotaDetalhe objeto)
        {
            return new Dictionary<string, string>();
        }

        internal int getMaxCodigo()
        {
            return repositorioRotaDetalhe.getMaxCodigo();
        }

        public IList<RotaDetalhe> getListaDetalheRepetido(DateTime dtInicio, DateTime dtFim, int idResponsavel)
        {
            return repositorioRotaDetalhe.getListaDetalheRepetido(dtInicio, dtFim, idResponsavel);
        }
    }
}
