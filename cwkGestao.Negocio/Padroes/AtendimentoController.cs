using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class AtendimentoController : BaseController<Atendimento>
	{
		#region Singleton
        private static AtendimentoController instance;
        private static IRepositorioAtendimento repositorioAtendimento;

        private AtendimentoController()
        { }
		
		static AtendimentoController()
        {
            instance = new AtendimentoController();
            repositorioT = RepositorioFactory<Atendimento>.GetRepositorio();
            repositorioAtendimento = (IRepositorioAtendimento)repositorioT;
        }
			
		public static AtendimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Atendimento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Atendimento> GetAllByAnalistaPeriodoEFaturados(Cw_usuario usuario, DateTime start, DateTime end, bool ConsiderarFaturados)
        {
            IList<Atendimento> retorno = repositorioAtendimento.GetAllByAnalistaPeriodoEFaturados(usuario, start, end, ConsiderarFaturados);
            return retorno;
        }

        public IList<Atendimento> GetAllPorIDNota(int IDNota)
        {
            return repositorioAtendimento.GetAllPorIDNota(IDNota);
        }

        public void ExcluiNotaOSsFaturamentoAtendimentos(int IDNota)
        {
            IList<Atendimento> atendimentos = AtendimentoController.Instancia.GetAllPorIDNota(IDNota);
            IList<OSOrdemServico> OSs = new List<OSOrdemServico>();
            IList<FaturamentoAtendimento> fats = new List<FaturamentoAtendimento>();
            try
            {
                foreach (Atendimento item in atendimentos)
                {
                    if (item.FatAtendimento != null)
                    {
                        if (item.FatAtendimento.OrdemServico != null)
                        {
                            if (!OSs.Contains(item.FatAtendimento.OrdemServico))
                            {
                                OSs.Add(item.FatAtendimento.OrdemServico);
                            }
                        }
                        if (!fats.Contains(item.FatAtendimento))
                        {
                            fats.Add(item.FatAtendimento);
                        }
                    }
                    item.FatAtendimento = null;
                    item.Nota = null;
                    Salvar(item, Acao.Alterar);
                }
                foreach (FaturamentoAtendimento item in fats)
                {
                    FaturamentoAtendimentoController.Instancia.Salvar(item, Acao.Excluir);
                }
                foreach (OSOrdemServico item in OSs)
                {
                    OSOrdemServicoController.Instancia.Salvar(item, Acao.Excluir);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
