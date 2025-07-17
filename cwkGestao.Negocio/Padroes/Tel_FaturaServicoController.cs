using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class Tel_FaturaServicoController : BaseController<Tel_FaturaServico>
	{
		#region Singleton
        private static Tel_FaturaServicoController instance;
        private static IRepositorioTel_FaturaServico repositorioTel_FaturaServico;

        private Tel_FaturaServicoController()
        { }
		
		static Tel_FaturaServicoController()
        {
            instance = new Tel_FaturaServicoController();
            repositorioT = RepositorioFactory<Tel_FaturaServico>.GetRepositorio();
            repositorioTel_FaturaServico = (IRepositorioTel_FaturaServico)repositorioT;
        }
			
		public static Tel_FaturaServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_FaturaServico objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Tel_FaturaServico> GetByGrupoEReferencia(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            return repositorioTel_FaturaServico.GetByGrupoEReferencia(grupoCliente, referencia);
        }

        public IList<Tel_FaturaServico> GetByFaturas(IList<int> idsFaturas)
        {
            return repositorioTel_FaturaServico.GetByFaturas(idsFaturas);
        }

        public IList<cwkGestao.Modelo.Tel_FaturaServico> GetServicos(int IDFatura)
        {
            return repositorioTel_FaturaServico.GetServicos(IDFatura);
        }
    }
}
