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
    public class ImpostosTributosExcessoesController : BaseController<ImpostosTributosExcessoes>
    {
        #region Singleton
        private static ImpostosTributosExcessoesController instance;
        private static IRepositorioImpostosTributosExcessoes repositorioImpostosTributosExcessoes;

        private ImpostosTributosExcessoesController()
        { }

        static ImpostosTributosExcessoesController()
        {
            instance = new ImpostosTributosExcessoesController();
            repositorioT = RepositorioFactory<ImpostosTributosExcessoes>.GetRepositorio();
            repositorioImpostosTributosExcessoes = (IRepositorioImpostosTributosExcessoes)repositorioT;
        }

        public static ImpostosTributosExcessoesController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ImpostosTributosExcessoes objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
