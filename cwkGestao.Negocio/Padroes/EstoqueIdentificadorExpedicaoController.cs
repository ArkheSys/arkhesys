using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios;

namespace cwkGestao.Negocio.Padroes
{
    public partial class EstoqueIdentificadorExpedicaoController : BaseController<EstoqueIdentificadorExpedicao>
    {
        #region Singleton
        private static EstoqueIdentificadorExpedicaoController instance;
        private static IRepositorioEstoqueIdentificadorExpedicao repositorioEstoqueIdentificadorExpedicao;

        private EstoqueIdentificadorExpedicaoController()
        { }

        static EstoqueIdentificadorExpedicaoController()
        {
            instance = new EstoqueIdentificadorExpedicaoController();
            repositorioT = RepositorioFactory<EstoqueIdentificadorExpedicao>.GetRepositorio();
            repositorioEstoqueIdentificadorExpedicao = (IRepositorioEstoqueIdentificadorExpedicao)repositorioT;
        }
			
		public static EstoqueIdentificadorExpedicaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(EstoqueIdentificadorExpedicao objeto)
        {
            return new Dictionary<string, string>();
        }

        public EstoqueIdentificadorExpedicao GetPorIDIdentificador(int IDidentificador)
        {
            return repositorioEstoqueIdentificadorExpedicao.GetPorIdIdentificador(IDidentificador);
        }
    }
}
