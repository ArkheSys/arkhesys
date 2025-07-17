using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class MovimentoController : BaseController<Movimento>
	{
		#region Singleton
        private static MovimentoController instance;
        private static IRepositorioMovimento repositorioMovimento;

        private MovimentoController()
        { }
		
		static MovimentoController()
        {
            instance = new MovimentoController();
            repositorioT = RepositorioFactory<Movimento>.GetRepositorio();
            repositorioMovimento = (IRepositorioMovimento)repositorioT;
        }
			
		public static MovimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Movimento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Movimento> GetRelatorioPorPlanoContas(DateTime dataInicial, DateTime dataFinal, string classificacao, string idsEmpresas, bool filtrarPorDataCompetencia)
        {
            return repositorioMovimento.GetRelatorioPorPlanoContas(dataInicial, dataFinal, classificacao, idsEmpresas, filtrarPorDataCompetencia);
        }

        public int GetNovoCodigoMovimento()
        {
            return repositorioMovimento.GetNovoCodigoMovimento();
        }
    }
}
