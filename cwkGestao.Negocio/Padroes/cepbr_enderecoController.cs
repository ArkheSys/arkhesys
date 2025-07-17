using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class cepbr_enderecoController : BaseController<cepbr_endereco>
	{
		#region Singleton
        private static cepbr_enderecoController instance;
        private static IRepositoriocepbr_endereco repositoriocepbr_endereco;

        private cepbr_enderecoController()
        { }
		
		static cepbr_enderecoController()
        {
            instance = new cepbr_enderecoController();
            repositorioT = RepositorioFactory<cepbr_endereco>.GetRepositorio();
            repositoriocepbr_endereco = (IRepositoriocepbr_endereco)repositorioT;
        }
			
		public static cepbr_enderecoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(cepbr_endereco objeto)
        {
            return new Dictionary<string, string>();
        }

        public cepbr_endereco BuscaInfEnderecos(string cep)
        {
            return repositoriocepbr_endereco.BuscaInfEnderecos(cep);
        }
	}
}
