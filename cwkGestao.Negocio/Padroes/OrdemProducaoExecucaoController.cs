using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class OrdemProducaoExecucaoController : BaseController<OrdemProducaoExecucao>
	{
		#region Singleton
        private static OrdemProducaoExecucaoController instance;
        private static IRepositorioOrdemProducaoExecucao repositorioOrdemProducaoExecucao;

        private OrdemProducaoExecucaoController()
        { }
		
		static OrdemProducaoExecucaoController()
        {
            instance = new OrdemProducaoExecucaoController();
            repositorioT = RepositorioFactory<OrdemProducaoExecucao>.GetRepositorio();
            repositorioOrdemProducaoExecucao = (IRepositorioOrdemProducaoExecucao)repositorioT;
        }
			
		public static OrdemProducaoExecucaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(OrdemProducaoExecucao objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<object> GetRelatorioApontamento(DateTime dataInicial, DateTime dataFinal, OrdemProducao op, Pessoa funcionario, Servico servico)
        {
            return repositorioOrdemProducaoExecucao.GetRelatorioApontamento(dataInicial, dataFinal, op, funcionario, servico);
        }

        public IList<OrdemProducaoExecucao> GetApontamentos(PessoaFuncionario pessoaFuncionario)
        {
            return repositorioOrdemProducaoExecucao.GetApontamentos(pessoaFuncionario);
        }
	}
}
