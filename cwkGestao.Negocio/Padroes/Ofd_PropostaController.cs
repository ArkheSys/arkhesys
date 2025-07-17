using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_PropostaController : BaseController<Ofd_Proposta>
	{
		#region Singleton
        private static Ofd_PropostaController instance;
        private static IRepositorioOfd_Proposta repositorioOfd_Proposta;

        private Ofd_PropostaController()
        { }
		
		static Ofd_PropostaController()
        {
            instance = new Ofd_PropostaController();
            repositorioT = RepositorioFactory<Ofd_Proposta>.GetRepositorio();
            repositorioOfd_Proposta = (IRepositorioOfd_Proposta)repositorioT;
        }
			
		public static Ofd_PropostaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_Proposta objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Ofd_Proposta> GetAllByObra(Ofd_Obra obra)
        {
            return repositorioOfd_Proposta.GetAllByObra(obra);
        }

        public Ofd_Proposta LoadObjectByIdWithMedicoes(int id)
        {
            return repositorioOfd_Proposta.LoadObjectByIdWithMedicoes(id);
        
        }

        public Object GetMaxNumMedicao(int prop)
        {
            return repositorioOfd_Proposta.GetMaxNumMedicao(prop);
        }

        public int GetUltimoID_Ofd_Proposta()
        {
            return repositorioOfd_Proposta.GetUltimoID_Ofd_Proposta();
        }
    }
}
