using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CQFuncionarioController : BaseController<CQFuncionario>
	{
		#region Singleton
        private static CQFuncionarioController instance;
        private static IRepositorioCQFuncionario repositorioCQFuncionario;

        private CQFuncionarioController()
        { }
		
		static CQFuncionarioController()
        {
            instance = new CQFuncionarioController();
            repositorioT = RepositorioFactory<CQFuncionario>.GetRepositorio();
            repositorioCQFuncionario = (IRepositorioCQFuncionario)repositorioT;
        }
			
		public static CQFuncionarioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CQFuncionario objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
