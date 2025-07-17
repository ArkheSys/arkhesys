using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Negocio.Padroes
{
    public class TabelaImpostosTributosExcessoesController : BaseController<TabelaImpostosTributosExcessoes>
    {
        #region Singleton
        private static TabelaImpostosTributosExcessoesController instance;
        private static IRepositorioTabelaImpostosTributosExcessoes repositorioTabelaImpostosTributosExcessoes;

        private TabelaImpostosTributosExcessoesController()
        { }

        static TabelaImpostosTributosExcessoesController()
        {
            instance = new TabelaImpostosTributosExcessoesController();
            repositorioT = RepositorioFactory<TabelaImpostosTributosExcessoes>.GetRepositorio();
            repositorioTabelaImpostosTributosExcessoes = (IRepositorioTabelaImpostosTributosExcessoes)repositorioT;
        }

        public static TabelaImpostosTributosExcessoesController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override Dictionary<string, string> ValidaObjeto(TabelaImpostosTributosExcessoes objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
