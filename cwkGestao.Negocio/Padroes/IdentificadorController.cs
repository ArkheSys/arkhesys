using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class IdentificadorController : BaseController<Identificador>
	{
		#region Singleton
        private static IdentificadorController instance;
        private static IRepositorioIdentificador repositorioIdentificador;

        private IdentificadorController()
        { }
		
		static IdentificadorController()
        {
            instance = new IdentificadorController();
            repositorioT = RepositorioFactory<Identificador>.GetRepositorio();
            repositorioIdentificador = (IRepositorioIdentificador)repositorioT;
        }
			
		public static IdentificadorController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Identificador objeto)
        {
            return new Dictionary<string, string>();
        }

        public int GetProximaSequenciaAno(int ano)
        {
            return repositorioIdentificador.GetProximaSequenciaAno(ano);
        }

        public Identificador getByAnoSequencia(int ano, int sequencia)
        {
            return repositorioIdentificador.getByAnoSequencia(ano, sequencia);
        }


    }
}
