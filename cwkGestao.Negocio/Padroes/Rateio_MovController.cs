using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Rateio_MovController : BaseController<Rateio_Mov>
	{
		#region Singleton
        private static Rateio_MovController instance;
        private static IRepositorioRateio_Mov repositorioRateio_Mov;

        private Rateio_MovController()
        { }
		
		static Rateio_MovController()
        {
            instance = new Rateio_MovController();
            repositorioT = RepositorioFactory<Rateio_Mov>.GetRepositorio();
            repositorioRateio_Mov = (IRepositorioRateio_Mov)repositorioT;
        }
			
		public static Rateio_MovController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
        public IList<Rateio_Mov> GetRateiosPorProjeto(Projeto proj)
        {
            return repositorioRateio_Mov.GetRateiosPorProjeto(proj.ID);
        }

        public IList<Rateio_Mov> GetRateiosPorMovimentoID(int id)
        {
            return repositorioRateio_Mov.GetRateiosPorMovimentoID(id);
        }

		public override Dictionary<string, string> ValidaObjeto(Rateio_Mov objeto)
        {
            return new Dictionary<string, string>();
        }

        public int MaxCodigo(Movimento m)
        {
            return repositorioRateio_Mov.MaxCodigo(m);
        }
	}

}
