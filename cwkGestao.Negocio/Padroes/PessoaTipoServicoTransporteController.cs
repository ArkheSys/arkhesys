using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaTipoServicoTransporteController : BaseController<PessoaTipoServicoTransporte>
	{
		#region Singleton
        private static PessoaTipoServicoTransporteController instance;
        private static IRepositorioPessoaTipoServicoTransporte repositorioPessoaTipoServicoTransporte;

        private PessoaTipoServicoTransporteController()
        { }
		
		static PessoaTipoServicoTransporteController()
        {
            instance = new PessoaTipoServicoTransporteController();
            repositorioT = RepositorioFactory<PessoaTipoServicoTransporte>.GetRepositorio();
            repositorioPessoaTipoServicoTransporte = (IRepositorioPessoaTipoServicoTransporte)repositorioT;
        }
			
		public static PessoaTipoServicoTransporteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaTipoServicoTransporte objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<PessoaTipoServicoTransporte> GetAllServicosPorPessoa(int IDPessoa)
        {
            return repositorioPessoaTipoServicoTransporte.GetAllServicosPorPessoa(IDPessoa);
        }
    }
}
