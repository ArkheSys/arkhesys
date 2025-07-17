using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NFe_ErroController : BaseController<NFe_Erro>
	{
		#region Singleton
        private static NFe_ErroController instance;
        private static IRepositorioNFe_Erro repositorioNFe_Erro;

        private NFe_ErroController()
        { }
		
		static NFe_ErroController()
        {
            instance = new NFe_ErroController();
            repositorioT = RepositorioFactory<NFe_Erro>.GetRepositorio();
            repositorioNFe_Erro = (IRepositorioNFe_Erro)repositorioT;
        }
			
		public static NFe_ErroController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NFe_Erro objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
