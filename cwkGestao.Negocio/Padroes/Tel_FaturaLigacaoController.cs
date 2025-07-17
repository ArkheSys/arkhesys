using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class Tel_FaturaLigacaoController : BaseController<Tel_FaturaLigacao>
	{
		#region Singleton
        private static Tel_FaturaLigacaoController instance;
        private static IRepositorioTel_FaturaLigacao repositorioTel_FaturaLigacao;

        private Tel_FaturaLigacaoController()
        { }
		
		static Tel_FaturaLigacaoController()
        {
            instance = new Tel_FaturaLigacaoController();
            repositorioT = RepositorioFactory<Tel_FaturaLigacao>.GetRepositorio();
            repositorioTel_FaturaLigacao = (IRepositorioTel_FaturaLigacao)repositorioT;
        }
			
		public static Tel_FaturaLigacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_FaturaLigacao objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetByFaturas(IList<int> idsFaturas)
        {
            return repositorioTel_FaturaLigacao.GetByFaturas(idsFaturas);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetLigacoes(int IDFatura)
        {
            return repositorioTel_FaturaLigacao.GetLigacoes(IDFatura);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetByGrupoEReferencia(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            return repositorioTel_FaturaLigacao.GetByGrupoEReferencia(grupoCliente, referencia);
        }

        public IList<Tel_Ligacao> GetAllPorIDArquivo(int id)
        {
            return repositorioTel_FaturaLigacao.GetAllLigacoesPorIDArquivo(id);
        }


    }
}
