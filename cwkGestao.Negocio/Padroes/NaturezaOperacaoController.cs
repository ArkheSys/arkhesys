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
    public class NaturezaOperacaoController : BaseController<NaturezaOperacao>
    {
        #region Singleton
        private static NaturezaOperacaoController instance;
        private static IRepositorioNaturezaOperacao repositorioNaturezaOperacao;

        private NaturezaOperacaoController()
        { }

        static NaturezaOperacaoController()
        {
            instance = new NaturezaOperacaoController();
            repositorioT = RepositorioFactory<NaturezaOperacao>.GetRepositorio();
            repositorioNaturezaOperacao = (IRepositorioNaturezaOperacao)repositorioT;
        }

        public static NaturezaOperacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(NaturezaOperacao objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
