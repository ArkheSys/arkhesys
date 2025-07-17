using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FaturamentoAtendimentoController : BaseController<FaturamentoAtendimento>
	{
		#region Singleton
        private static FaturamentoAtendimentoController instance;
        private static IRepositorioFaturamentoAtendimento repositorioFaturamentoAtendimento;

        private FaturamentoAtendimentoController()
        { }
		
		static FaturamentoAtendimentoController()
        {
            instance = new FaturamentoAtendimentoController();
            repositorioT = RepositorioFactory<FaturamentoAtendimento>.GetRepositorio();
            repositorioFaturamentoAtendimento = (IRepositorioFaturamentoAtendimento)repositorioT;
        }
			
		public static FaturamentoAtendimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FaturamentoAtendimento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<FaturamentoAtendimento> GetFaturamentoAtendimentos(IList<Atendimento> Atendimentos)
        {
            IList<FaturamentoAtendimento> faturamentos = new List<FaturamentoAtendimento>();
            IList<Atendimento> AtendimentosNaoFaturados = Atendimentos;

            AtendimentosNaoFaturados = AtendimentosNaoFaturados.Where(w => w.Nota == null).ToList();
            
            IEnumerable<IGrouping<int, Atendimento>> grupo = Atendimentos.GroupBy(g => g.Cliente.ID);

            DateTime dtInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            foreach (IGrouping<int, Atendimento> item in grupo)
            {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                if ((item.Key == null) ||
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                    (item.Key == 0))
                {
                    throw new Exception("O Cliente não está especificado no Atendimento. Verifique.");
                }
                FaturamentoAtendimento fatAtend = new FaturamentoAtendimento(){ AtendimentosVinculados = new List<Atendimento>() };
                try
                {
                    TimeSpan horasFaturamento = new TimeSpan(item.Where(w => w.Faturar == true).Sum(s => s.TotalHorasFat.Ticks));
                    TimeSpan horasJaFaturadas = OSOrdemServicoController.Instancia.GetTotHorasFaturadas(item.Key, dtInicio, dtFim);
                    TimeSpan horasContratadas;
                    TimeSpan horasrestantes;
                    
                    Pessoa cliente = PessoaController.Instancia.LoadObjectById(item.Key);

                    if ((cliente.HorasContratadas != null) &&
                        (cliente.HorasContratadas != "00:00:00"))
                    {
                        cliente.HorasContratadas = cliente.HorasContratadas == "---:--" ? null : cliente.HorasContratadas;
                        if (cliente.HorasContratadas != null)
                        {
                            var listaHoraContratada = cliente.HorasContratadas.Split(':');

                            horasContratadas = new TimeSpan(Convert.ToInt32(listaHoraContratada[0]), Convert.ToInt32(listaHoraContratada[1]), 0);
                            horasrestantes = horasContratadas - horasJaFaturadas;
                            if (horasrestantes > horasFaturamento)
                            {
                                horasFaturamento = horasrestantes;
                            }

                        }
                    }


                    fatAtend.Selecionado = true;
                    fatAtend.Cliente = cliente;
                    fatAtend.AtendimentosVinculados = item.ToList();                  
                    fatAtend.HorasAtendimento = new TimeSpan(item.Sum(s => s.TotalHoras.Ticks));
                    fatAtend.HorasFaturamento = horasFaturamento;
                    fatAtend.Quantidade = item.Count();

                    foreach (Atendimento att in fatAtend.AtendimentosVinculados)
                        att.FatAtendimento = fatAtend;
                    
                    if (fatAtend.Cliente.ServicoAtendimento != null)
                    {
                        fatAtend.Servico = fatAtend.Cliente.ServicoAtendimento;
                        decimal valorServico = fatAtend.Cliente.ValorHoraAtendimento;
                        fatAtend.Valor = Math.Round(valorServico * Convert.ToDecimal(fatAtend.HorasFaturamento.TotalHours), 2);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                if (fatAtend.Total > 0)
                {
                    faturamentos.Add(fatAtend);
                }
                
            }
            return faturamentos;
        }
    }
}
