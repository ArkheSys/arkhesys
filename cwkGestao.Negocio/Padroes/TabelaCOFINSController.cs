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
    public class TabelaCOFINSController : BaseController<TabelaCOFINS>
    {
        #region Singleton
        private static TabelaCOFINSController instance;
        private static IRepositorioTabelaCOFINS repositorioTabelaCOFINS;

        private TabelaCOFINSController()
        { }

        static TabelaCOFINSController()
        {
            instance = new TabelaCOFINSController();
            repositorioT = RepositorioFactory<TabelaCOFINS>.GetRepositorio();
            repositorioTabelaCOFINS = (IRepositorioTabelaCOFINS)repositorioT;
        }

        public static TabelaCOFINSController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(TabelaCOFINS objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
