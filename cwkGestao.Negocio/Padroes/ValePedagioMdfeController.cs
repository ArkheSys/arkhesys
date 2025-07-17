//using System.Collections.Generic;
//using cwkGestao.Modelo;
//using cwkGestao.Repositorio;
//using cwkGestao.Repositorio.Interfaces.Particulares;

//namespace cwkGestao.Negocio
//{
//    public class ValePedagioMdfeController : BaseController<ValePedagioMdfe>
//    {
//        #region Singleton
//        private static ValePedagioMdfeController instance;
//        private static IRepositorioValePedagioMdfe repositorioValePedagioMdfe;

//        private ValePedagioMdfeController()
//        { }

//        static ValePedagioMdfeController()
//        {
//            instance = new ValePedagioMdfeController();
//            repositorioT = RepositorioFactory<ValePedagioMdfe>.GetRepositorio();
//            repositorioValePedagioMdfe = (IRepositorioValePedagioMdfe)repositorioT;
//        }

//        public static ValePedagioMdfeController Instancia => instance;
//        #endregion

//        public override Dictionary<string, string> ValidaObjeto(ValePedagioMdfe objeto)
//        {
//            return new Dictionary<string, string>();
//        }
//    }
//}