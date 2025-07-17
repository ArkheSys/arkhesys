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
    public partial class OpcaoTributacaoController : BaseController<OpcaoTributacao>
    {
        #region Singleton
        private static OpcaoTributacaoController instance;
        private static IRepositorioOpcaoTributacao repositorioOpcaoTributacao;

        private OpcaoTributacaoController()
        { }

        static OpcaoTributacaoController()
        {
            instance = new OpcaoTributacaoController();
            repositorioT = RepositorioFactory<OpcaoTributacao>.GetRepositorio();
            repositorioOpcaoTributacao = (IRepositorioOpcaoTributacao)repositorioT;
        }

        public static OpcaoTributacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion




        public override Dictionary<string, string> ValidaObjeto(OpcaoTributacao objeto)
        {

            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objeto.Codigo <= 0)
                ret.Add("txtCodigo", "Valor tem que ser maior que 0 (Zero).");

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
                ret.Add("txtNome", "Campo obrigatório.");

            return ret;
        }

    }
}
