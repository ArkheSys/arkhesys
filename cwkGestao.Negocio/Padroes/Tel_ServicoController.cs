using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_ServicoController : BaseController<Tel_Servico>
	{
		#region Singleton
        private static Tel_ServicoController instance;
        private static IRepositorioTel_Servico repositorioTel_Servico;

        private Tel_ServicoController()
        { }
		
		static Tel_ServicoController()
        {
            instance = new Tel_ServicoController();
            repositorioT = RepositorioFactory<Tel_Servico>.GetRepositorio();
            repositorioTel_Servico = (IRepositorioTel_Servico)repositorioT;
        }
			
		public static Tel_ServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_Servico objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
