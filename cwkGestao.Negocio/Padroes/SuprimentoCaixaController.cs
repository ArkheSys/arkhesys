using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;

namespace cwkGestao.Negocio.Padroes
{
    public partial class SuprimentoCaixaController : BaseController<SuprimentoCaixa>
    {
        #region Singleton

        private static SuprimentoCaixaController instance;
        private static IRepositorioSuprimentoCaixa repositorioSuprimentoCaixa;

        private SuprimentoCaixaController()
        { }

        static SuprimentoCaixaController()
        {
            instance = new SuprimentoCaixaController();
            repositorioT = RepositorioFactory<SuprimentoCaixa>.GetRepositorio();
            repositorioSuprimentoCaixa = (IRepositorioSuprimentoCaixa)repositorioT;
        }

        public static SuprimentoCaixaController Instancia
        {
            get
            {
                return instance;
            }
        }
        public override Dictionary<string, string> ValidaObjeto(SuprimentoCaixa objeto)
        {
            return new Dictionary<string, string>();
        }

        public object GetSangrias(DateTime now1, DateTime now2)
        {
            throw new NotImplementedException();
        }
        #endregion

        public decimal GetSaldoAtualVendasComSuprimento(int IDUsuario, int IDFluxoCaixa)
        {
            return repositorioSuprimentoCaixa.GetSuprimentoVendasComSuprimento(IDUsuario, IDFluxoCaixa);
        }

        public List<SuprimentoCaixa> GetSuprimentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioSuprimentoCaixa.GetSuprimentosPorFluxoCaixa(IDFluxoCaixa);
        }

        public SuprimentoCaixa GetSuprimento(int IDSuprimentoCaixa)
        {
            return repositorioSuprimentoCaixa.GetSuprimento(IDSuprimentoCaixa);
        }

        public IList<pxSuprimentoCaixa> GetSuprimentos(DateTime dtInicio, DateTime dtFinal){
            return repositorioSuprimentoCaixa.GetSuprimentos(dtInicio, dtFinal);
        }
    }
}
