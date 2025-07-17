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
    public class ClassificacaoFiscalController : BaseController<ClassificacaoFiscal>
    {
        #region Singleton
        private static ClassificacaoFiscalController instance;
        private static IRepositorioClassificacaoFiscal repositorioClassificacaoFiscal;

        private ClassificacaoFiscalController()
        { }

        static ClassificacaoFiscalController()
        {
            instance = new ClassificacaoFiscalController();
            repositorioT = RepositorioFactory<ClassificacaoFiscal>.GetRepositorio();
            repositorioClassificacaoFiscal = (IRepositorioClassificacaoFiscal)repositorioT;
        }

        public static ClassificacaoFiscalController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public override Dictionary<string, string> ValidaObjeto(ClassificacaoFiscal objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
