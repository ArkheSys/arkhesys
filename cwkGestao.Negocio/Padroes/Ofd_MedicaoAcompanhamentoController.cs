using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_MedicaoAcompanhamentoController : BaseController<Ofd_MedicaoAcompanhamento>
	{
		#region Singleton
        private static Ofd_MedicaoAcompanhamentoController instance;
        private static IRepositorioOfd_MedicaoAcompanhamento repositorioOfd_MedicaoAcompanhamento;

        private Ofd_MedicaoAcompanhamentoController()
        { }

        static Ofd_MedicaoAcompanhamentoController()
        {
            instance = new Ofd_MedicaoAcompanhamentoController();
            repositorioT = RepositorioFactory<Ofd_MedicaoAcompanhamento>.GetRepositorio();
            repositorioOfd_MedicaoAcompanhamento = (IRepositorioOfd_MedicaoAcompanhamento)repositorioT;
        }
			
		public static Ofd_MedicaoAcompanhamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_MedicaoAcompanhamento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Ofd_MedicaoAcompanhamento> GetByMedicao(int IDMedicao)
        {
            return repositorioOfd_MedicaoAcompanhamento.GetByMedicao(IDMedicao);
        }
	}
}
