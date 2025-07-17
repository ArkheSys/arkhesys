using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Negocio.Padroes
{
    public class PisCofinsNaturezaOperacaoController : BaseController<PisCofinsNaturezaOperacao>
    {
        #region Singleton
        private static PisCofinsNaturezaOperacaoController instance;
        private static IRepositorioPisCofinsNaturezaOperacao repositorioPisCofinsNaturezaOperacao;

        private PisCofinsNaturezaOperacaoController()
        { }

        static PisCofinsNaturezaOperacaoController()
        {
            instance = new PisCofinsNaturezaOperacaoController();
            repositorioT = RepositorioFactory<PisCofinsNaturezaOperacao>.GetRepositorio();
            repositorioPisCofinsNaturezaOperacao = (IRepositorioPisCofinsNaturezaOperacao)repositorioT;
        }

        public static PisCofinsNaturezaOperacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(PisCofinsNaturezaOperacao objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
