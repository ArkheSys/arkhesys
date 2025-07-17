using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CustoController : BaseController<Custo>
	{
		#region Singleton
        private static CustoController instance;
        private static IRepositorioCusto repositorioContratoProduto;

        private CustoController()
        { }

        static CustoController()
        {
            instance = new CustoController();
            repositorioT = RepositorioFactory<Custo>.GetRepositorio();
            repositorioContratoProduto = (IRepositorioCusto)repositorioT;
        }

        public static CustoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Custo objeto)
        {
            return new Dictionary<string, string>();
        }


        public IList<Custo> GetAllByIDNota(int idNota)
        {
            return repositorioContratoProduto.GetAllByIDNota(idNota);
        }

	}
}
