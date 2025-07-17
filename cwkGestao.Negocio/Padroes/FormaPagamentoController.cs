using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio
{
    public class FormaPagamentoController : BaseController<FormaPagamento>
    {		
        #region Singleton
        private static FormaPagamentoController instance;
        private static IRepositorioFormaPagamento repositorioFormaPagamento;

        private FormaPagamentoController()
        { }

        static FormaPagamentoController()
        {
            instance = new FormaPagamentoController();
            repositorioT = RepositorioFactory<FormaPagamento>.GetRepositorio();
            repositorioFormaPagamento = (IRepositorioFormaPagamento)repositorioT;
        }

        public static FormaPagamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(FormaPagamento objeto)
        {
            return new Dictionary<string, string>();
        }

        public FormaPagamento GetCheque()
        {
            return repositorioFormaPagamento.GetCheque();
        }

        public FormaPagamento FindByCodigoSIS(string cod)
        {
            return repositorioFormaPagamento.FindByCodigoSIS(cod);
        }

        public FormaPagamento FindByCodigo(int cod)
        {
            return repositorioFormaPagamento.FindByCodigo(cod);
        }
    }
}
