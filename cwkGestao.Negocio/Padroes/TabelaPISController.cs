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
    public class TabelaPISController : BaseController<TabelaPIS>
    {
        #region Singleton
        private static TabelaPISController instance;
        private static IRepositorioTabelaPIS repositorioTabelaPIS;

        private TabelaPISController()
        { }

        static TabelaPISController()
        {
            instance = new TabelaPISController();
            repositorioT = RepositorioFactory<TabelaPIS>.GetRepositorio();
            repositorioTabelaPIS = (IRepositorioTabelaPIS)repositorioT;
        }

        public static TabelaPISController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(TabelaPIS objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
