using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class CEST_SegmentoController : BaseController<CEST_SegmentoController>
    {
        #region Singleton
        private static CESTController instance;

        public static CESTController Instancia
        {
            get
            {
                return instance;
            }
        }
    }
}