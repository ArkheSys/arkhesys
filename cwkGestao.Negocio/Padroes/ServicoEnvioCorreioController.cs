using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ServicoEnvioCorreioController : BaseController<ServicoEnvioCorreio>
	{
		#region Singleton
        private static ServicoEnvioCorreioController instance;
        private static IRepositorioServicoEnvioCorreio repositorioServicoEnvioCorreio;

        private ServicoEnvioCorreioController()
        { }
		
		static ServicoEnvioCorreioController()
        {
            instance = new ServicoEnvioCorreioController();
            repositorioT = RepositorioFactory<ServicoEnvioCorreio>.GetRepositorio();
            repositorioServicoEnvioCorreio = (IRepositorioServicoEnvioCorreio)repositorioT;
        }
			
		public static ServicoEnvioCorreioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ServicoEnvioCorreio objeto)
        {
            return new Dictionary<string, string>();
        }

        //public IList<ServicoEnvioCorreio> GetPrecoPrazo(string CEPOrigem, string CEPDestino, 
        //    decimal Largura, decimal Altura, decimal Profundidade, decimal Peso)
	}
}
