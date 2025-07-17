using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public partial class LigacaoController : BaseController<Tel_Ligacao>
	{
		#region Singleton
        private static LigacaoController instance;
        private static IRepositorioTel_Ligacao repositorioLigacao;

        private LigacaoController()
        { }

        static LigacaoController()
        {
            instance = new LigacaoController();
            repositorioT = RepositorioFactory<Tel_Ligacao>.GetRepositorio();
            repositorioLigacao = (IRepositorioTel_Ligacao)repositorioT;
        }

        public static LigacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Tel_Ligacao objeto)
        {
            return new Dictionary<string, string>();
        }

        public Dictionary<string, Tel_Ligacao> GetHashLigacoes()
        {
            return repositorioLigacao.GetHashLigacoes();
        }

        public Dictionary<int, IList<Tel_Ligacao>> GetByReferenciaEClientes(DateTime referencia, IList<Tel_Cliente> clientes)
        {
            return repositorioLigacao.GetByReferenciaEClientes(referencia, clientes);
        }

        public IList<pxyTel_LigacaoCliente> GetByReferencia(DateTime referencia)
        {
            return repositorioLigacao.GetByReferencia(referencia);
        }

        public Dictionary<string, Tel_Ligacao> GetHashLigacoesArquivo(DateTime referencia)
        {
            return repositorioLigacao.GetHashLigacoesArquivo(referencia);
        }
	}
}
