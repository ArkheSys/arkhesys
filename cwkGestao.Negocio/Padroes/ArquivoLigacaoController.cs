using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ArquivoLigacaoController : BaseController<Tel_ArquivoLigacao>
	{
		#region Singleton
        private static ArquivoLigacaoController instance;
        private static IRepositorioTel_ArquivoLigacao repositorioArquivoLigacao;

        private ArquivoLigacaoController()
        { }
		
		static ArquivoLigacaoController()
        {
            instance = new ArquivoLigacaoController();
            repositorioT = RepositorioFactory<Tel_ArquivoLigacao>.GetRepositorio();
            repositorioArquivoLigacao = (IRepositorioTel_ArquivoLigacao)repositorioT;
        }
			
		public static ArquivoLigacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_ArquivoLigacao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
