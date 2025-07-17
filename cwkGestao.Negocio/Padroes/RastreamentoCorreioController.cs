using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class RastreamentoCorreioController : BaseController<RastreamentoCorreio>
	{
		#region Singleton
        private static RastreamentoCorreioController instance;
        private static IRepositorioRastreamentoCorreio repositorioRastreamentoCorreio;

        private RastreamentoCorreioController()
        { }
		
		static RastreamentoCorreioController()
        {
            instance = new RastreamentoCorreioController();
            repositorioT = RepositorioFactory<RastreamentoCorreio>.GetRepositorio();
            repositorioRastreamentoCorreio = (IRepositorioRastreamentoCorreio)repositorioT;
        }
			
		public static RastreamentoCorreioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(RastreamentoCorreio objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<RastreamentoCorreio> GetCodigosRastreamentosPorQuantidadeVolume(int quantidade, int idTipoServico)
        {
            return repositorioRastreamentoCorreio.GetCodigosRastreamentosPorQuantidadeVolume(quantidade, idTipoServico);
        }

        private string GetSomenteNumeros(string texto)
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(texto, "[^\\d]"));
        }

        public void CadastraEtiquetasCorreios(string[] pCodigosRastreamentos, TipoServicoTransporte pTipoServico, Filial pFilial)
        {
            RastreamentoCorreio objRastreamento = new RastreamentoCorreio();
            
            for (int i = 0; i < pCodigosRastreamentos.Count(); i++)
			{
                objRastreamento.Letra = pCodigosRastreamentos[i].Substring(0, 2);
                objRastreamento.Numero = GetSomenteNumeros(pCodigosRastreamentos[i]);
                objRastreamento.TipoServico = pTipoServico;
                objRastreamento.SolicitadoWScorreios = true;
                objRastreamento.Filial = pFilial;

                RastreamentoCorreioController.Instancia.Salvar(objRastreamento, Acao.Incluir);

                objRastreamento = new RastreamentoCorreio();
			}
        }

        public IList<RastreamentoCorreio> GetCodigosDisponiveis()
        {
            return repositorioRastreamentoCorreio.GetCodigosDisponiveis();
        }

        public IList<RastreamentoCorreio> GetCodigosDisponiveisPorTipoServico(TipoServicoTransporte tst)
        {
            return repositorioRastreamentoCorreio.GetCodigosDisponiveisPorTipoServico(tst);
        }
	}
}
