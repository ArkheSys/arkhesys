using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaRevisaoPerfuracoesController : BaseController<Ofd_PropostaRevisaoPerfuracoes>
	{
		#region Singleton
        private static Ofd_PropostaRevisaoPerfuracoesController instance;
        private static IRepositorioOfd_PropostaRevisaoPerfuracoes repositorioOfd_PropostaRevisaoPerfuracoes;

        private Ofd_PropostaRevisaoPerfuracoesController()
        { }
		
		static Ofd_PropostaRevisaoPerfuracoesController()
        {
            instance = new Ofd_PropostaRevisaoPerfuracoesController();
            repositorioT = RepositorioFactory<Ofd_PropostaRevisaoPerfuracoes>.GetRepositorio();
            repositorioOfd_PropostaRevisaoPerfuracoes = (IRepositorioOfd_PropostaRevisaoPerfuracoes)repositorioT;
        }
			
		public static Ofd_PropostaRevisaoPerfuracoesController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_PropostaRevisaoPerfuracoes objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
