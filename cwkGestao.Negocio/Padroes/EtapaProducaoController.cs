using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class EtapaProducaoController : BaseController<EtapaProducao>
	{
		#region Singleton
        private static EtapaProducaoController instance;
        private static IRepositorioEtapaProducao repositorioEtapaProducao;

        private EtapaProducaoController()
        { }
		
		static EtapaProducaoController()
        {
            instance = new EtapaProducaoController();
            repositorioT = RepositorioFactory<EtapaProducao>.GetRepositorio();
            repositorioEtapaProducao = (IRepositorioEtapaProducao)repositorioT;
        }
			
		public static EtapaProducaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(EtapaProducao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
