using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio
{
    public class FormaPgtoMovimentoController : BaseController<FormaPgtoMovimento>
    {
        #region Singleton
        private static FormaPgtoMovimentoController instance;
        private static IRepositorioFormaPgtoMovimento repositorioFormaPgtoMovimento;

        private FormaPgtoMovimentoController()
        { }

        static FormaPgtoMovimentoController()
        {
            instance = new FormaPgtoMovimentoController();
            repositorioT = RepositorioFactory<FormaPgtoMovimento>.GetRepositorio();
            repositorioFormaPgtoMovimento = (IRepositorioFormaPgtoMovimento)repositorioT;
        }

        public static FormaPgtoMovimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(FormaPgtoMovimento objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
    }
}
