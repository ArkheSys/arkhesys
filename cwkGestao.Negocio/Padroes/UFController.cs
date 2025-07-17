using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class UFController : BaseController<UF>
    {
        #region Singleton
        private static UFController instance;
        private static IRepositorioUF repositorioUF;

        private UFController()
        { }

        static UFController()
        {
            instance = new UFController();
            repositorioT = RepositorioFactory<UF>.GetRepositorio();
            repositorioUF = (IRepositorioUF)repositorioT;
        }

        public static UFController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(UF objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(objeto.Sigla) || objeto.Sigla.Trim() == String.Empty)
                ret.Add("txtSigla", "Campo obrigatório.");

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
                ret.Add("txtNome", "Campo obrigatório.");

            if (objeto.Pais == null || objeto.Pais.Nome.Trim() == String.Empty)
                ret.Add("lkpPais", "Campo obrigatório.");

            if (String.IsNullOrEmpty(objeto.IBGE) || objeto.IBGE.Trim() == String.Empty)
                ret.Add("txtIBGE", "Campo obrigatório.");

            return ret;
        }
    }
}
