using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class PaisController : BaseController<Pais>
    {
        #region Singleton
        private static PaisController instance;
        private static IRepositorioPais repositorioPais;

        private PaisController()
        { }

        static PaisController()
        {
            instance = new PaisController();
            repositorioT = RepositorioFactory<Pais>.GetRepositorio();
            repositorioPais = (IRepositorioPais)repositorioT;
        }

        public static PaisController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Pais objeto)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            if (objeto.Codigo < 1)
                erros.Add("txtCodigo", "Código inválido;");
            if (objeto.IBGE == null || objeto.IBGE.Trim().Length < 0)
                erros.Add("txtIBGE", "IBGE inválido.");
            if (objeto.Nome == null || objeto.Nome.Trim().Length == 0)
                erros.Add("txtNome", "Campo obrigatório.");
            return erros;
        }
    }
}
