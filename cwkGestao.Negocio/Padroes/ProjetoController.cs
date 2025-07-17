using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProjetoController : BaseController<Projeto>
	{
		#region Singleton
        private static ProjetoController instance;
        private static IRepositorioProjeto repositorioProjeto;

        private ProjetoController()
        { }
		
		static ProjetoController()
        {
            instance = new ProjetoController();
            repositorioT = RepositorioFactory<Projeto>.GetRepositorio();
            repositorioProjeto = (IRepositorioProjeto)repositorioT;
        }
			
		public static ProjetoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Projeto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ProjetoOP> GetCentrosDeCustoEProjetosAprovados()
        {
            IList<ProjetoOP> ret = new List<ProjetoOP>();
            foreach (var proj in repositorioProjeto.GetCentrosDeCustoEProjetosAprovados())
            {
                if(proj.OrdemProducaos.Count == 0)
                    ret.Add(new ProjetoOP(proj.Nome, proj.Pessoa!= null ? proj.Pessoa.Nome : "", null, proj.Status, proj.ID, proj.Data));
                else foreach (var op in proj.OrdemProducaos)
                    ret.Add(new ProjetoOP(proj.Nome, proj.Pessoa.Nome, op.Codigo, proj.Status, proj.ID, proj.Data));
            }
            return ret;
        }

        public IList<Projeto> GetCentrosDeCusto()
        {
            return repositorioProjeto.GetCentrosDeCusto();
        }
    }
}
