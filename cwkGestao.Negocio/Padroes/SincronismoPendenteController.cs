using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SincronismoPendenteController : BaseController<SincronismoPendente>
	{
		#region Singleton
        private static SincronismoPendenteController instance;
        private static IRepositorioSincronismoPendente repositorioSincronismoPendente;

        private SincronismoPendenteController()
        { }
		
		static SincronismoPendenteController()
        {
            instance = new SincronismoPendenteController();
            repositorioT = RepositorioFactory<SincronismoPendente>.GetRepositorio();
            repositorioSincronismoPendente = (IRepositorioSincronismoPendente)repositorioT;
        }
			
		public static SincronismoPendenteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(SincronismoPendente objeto)
        {
            return new Dictionary<string, string>();
        }

        public void GravarExcecao(Exception excecao, string tipoEntidade, int ID, string Nome)
        {
            Salvar(new SincronismoPendente
            {
                NomeCadastro = tipoEntidade,
                IDCadastro = ID,
                Descricao = Nome,
                Erro = excecao.ToString()
            }, Acao.Incluir);
        }

        public IList<SincronismoPendente> GetPedidosPendentes()
        {
            return repositorioSincronismoPendente._GetPedidosPendentes();
        }

        public SincronismoPendente GetPedidoPendente(int IDIntegracao)
        {
            return repositorioSincronismoPendente._GetPedidoPendente(IDIntegracao);
        }
	}
}
