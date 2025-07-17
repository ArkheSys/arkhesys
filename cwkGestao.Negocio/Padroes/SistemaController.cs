using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class SistemaController : BaseController<Sistema>
    {
        #region Singleton
        private static SistemaController instance;
        private static IRepositorioSistema repositorioSistema;

        private SistemaController()
        { }

        static SistemaController()
        {
            instance = new SistemaController();
            repositorioT = RepositorioFactory<Sistema>.GetRepositorio();
            repositorioSistema = (IRepositorioSistema)repositorioT;
        }

        public static SistemaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Sistema objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objeto.Codigo == 0)
            {
                ret.Add("txtCodigo", "Valor tem que ser maior do que zero (0).");
            }

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
            {
                ret.Add("txtNome", "Campo obrigatório.");
            }
            return ret;
        }

        public void ReordenaVersoes(IList<Versao> pVersoes)
        {
            int count = 1;
            foreach (Modelo.Versao cp in pVersoes)
            {
                cp.Codigo = count++;
            }
        }
    }
}
