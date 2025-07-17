using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class Tel_TipoLigacaoController : BaseController<Tel_TipoLigacao>
    {
        #region Singleton
        private static Tel_TipoLigacaoController instance;
        private static IRepositorioTel_TipoLigacao repositorioTipoLigacao;

        private Tel_TipoLigacaoController()
        { }

        static Tel_TipoLigacaoController()
        {
            instance = new Tel_TipoLigacaoController();
            repositorioT = RepositorioFactory<Tel_TipoLigacao>.GetRepositorio();
            repositorioTipoLigacao = (IRepositorioTel_TipoLigacao)repositorioT;
        }

        public static Tel_TipoLigacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Tel_TipoLigacao objeto)
        {
            return new Dictionary<string, string>();
        }

        internal HashSet<int> GetHashCodigos()
        {
            return repositorioTipoLigacao.GetHashCodigos();
        }

        public IList<Tel_TipoLigacao> GetIncompletos()
        {
            return repositorioTipoLigacao.GetIncompletos();
        }
    }
}
