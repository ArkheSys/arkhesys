using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class RotaController : BaseController<Rota>
	{
		#region Singleton
        private static RotaController instance;
        private static IRepositorioRota repositorioRota;

        private RotaController()
        { }
		
		static RotaController()
        {
            instance = new RotaController();
            repositorioT = RepositorioFactory<Rota>.GetRepositorio();
            repositorioRota = (IRepositorioRota)repositorioT;
        }
			
		public static RotaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        IList<Rota> rotasMontadas;
        IList<RotaDetalhe> rotasDetalhesExistentes;

		public override Dictionary<string, string> ValidaObjeto(Rota objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Rota> GerarRotasEmLote(DateTime dtInicio, DateTime dtFim, Filial filial, Pessoa responsavel, string descricao)
        {
            List<Pessoa> pessoasGeradas = new List<Pessoa>();
            rotasMontadas = new List<Rota>();
            rotasDetalhesExistentes = new List<RotaDetalhe>();

            IList<RotaDetalhe> rotasDetalhe;
            Rota rota;
            RotaDetalhe rotaDetalhe; 

            DateTime dtLocal = dtInicio;

            IList<Pessoa> lstPessoas = PessoaController.Instancia.GetAllPorDiaPadraoLeitura(dtInicio.Day, dtFim.Day);
            TrataMaximoDiaMes(dtLocal.Month, dtLocal.Year, ref lstPessoas);
            try
            {
                rotasDetalhesExistentes = RotaDetalheController.Instancia.getListaDetalheRepetido(dtInicio, dtFim, responsavel.ID);
                while (dtLocal <= dtFim)
                {
                    Int32 contadorSequencia = 1;
                    IList<Pessoa> lstPessoasDoDia = new List<Pessoa>();
                    lstPessoasDoDia = lstPessoas.Where(d => d.DiasPadraoLeitura == dtLocal.Day || 
                                                      TrataSabadoEDomingo(d.DiasPadraoLeitura, dtLocal.Month, dtLocal.Year) == dtLocal.Day).OrderBy(i => i.ID).ToList();
                    IList<Pessoa> lstPessoasDoDiaNaoRepetidos = MontaListaPessoasComRotasNRepetidas(rotasDetalhesExistentes, lstPessoasDoDia);

                    if ((lstPessoasDoDiaNaoRepetidos.Except(pessoasGeradas).Count()) > 0)
                    {
                        rota = new Rota();
                        rotasDetalhe = new List<RotaDetalhe>();

                        rota.Codigo = repositorioRota.getMaxCodigo() + contadorSequencia;
                        rota.Data = TrataSabadoEDomingo(dtLocal);
                        rota.Filial = filial;
                        rota.Descricao = descricao;
                        rota.Responsavel = responsavel;

                        int maxCodigoRotaDetalhe = RotaDetalheController.Instancia.getMaxCodigo();

                        foreach (var pessoa in lstPessoasDoDiaNaoRepetidos)
                        {
                            rotaDetalhe = new RotaDetalhe();

                            rotaDetalhe.Codigo = maxCodigoRotaDetalhe + contadorSequencia;
                            rotaDetalhe.Cliente = pessoa;
                            rotaDetalhe.Sequencia = contadorSequencia;
                            rotaDetalhe.Situacao = 0; //Situação Pendente
                            rotaDetalhe.Data = TrataSabadoEDomingo(dtLocal);
                            rotaDetalhe.Rota = rota;
                            rotasDetalhe.Add(rotaDetalhe);
                            contadorSequencia++;
                        }
                        rota.RotaDetalhes = rotasDetalhe;
                        RotaController.Instancia.Salvar(rota, Acao.Incluir);
                        rotasMontadas.Add(rota);
                    }
                    dtLocal = dtLocal.AddDays(1);
                    pessoasGeradas.AddRange(lstPessoasDoDiaNaoRepetidos);
                }

                return rotasMontadas;
            }
            catch (Exception ex)
            {
                throw ex;           
            }
        }

       

        private IList<Pessoa> MontaListaPessoasComRotasNRepetidas(IList<RotaDetalhe> rotasDetalhesExistentes, IList<Pessoa> lstPessoasDoDia)
        {
            IList<Pessoa> lstPessoasDoDiaAindaNaoInseridos = new List<Pessoa>();
            if (rotasDetalhesExistentes.Count > 0)
            {
                foreach (var pessoa in lstPessoasDoDia)
                {
                    var pessoaRepetida = rotasDetalhesExistentes.Where(p => p.Data.Value.Day == TrataSabadoEDomingo(p.Data.Value.Day, p.Data.Value.Month, p.Data.Value.Year) && p.Cliente.ID == pessoa.ID).Count();
                    if (pessoaRepetida == 0)
                    {
                        lstPessoasDoDiaAindaNaoInseridos.Add(pessoa);
                    }
                }
            }
            else
            {
                lstPessoasDoDiaAindaNaoInseridos = lstPessoasDoDia;
            }
            return lstPessoasDoDiaAindaNaoInseridos;
        }



        private DateTime TrataSabadoEDomingo(DateTime dtLocal)
        {
            switch (dtLocal.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return dtLocal.AddDays(-1);
                case DayOfWeek.Sunday:
                    return dtLocal.AddDays(+1);
                default:
                    return dtLocal;
            }
        }

        private int TrataSabadoEDomingo(int dia, int mes, int ano)
        {
            DateTime dtLocal = new DateTime(ano, mes, dia);
            switch (dtLocal.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return --dia;
                case DayOfWeek.Sunday:
                    return ++dia; ;
                default:
                    return dia;
            }
        }

        private void TrataMaximoDiaMes(int mesGeracao, int anoGeracao, ref IList<Pessoa> lstPessoas)
        {
            foreach (var pessoa in lstPessoas.Where(p => p.DiasPadraoLeitura > 27).ToList())
            {
                int qtdDiasMes = DateTime.DaysInMonth(anoGeracao, mesGeracao);
                if (qtdDiasMes < pessoa.DiasPadraoLeitura)
                    pessoa.DiasPadraoLeitura = qtdDiasMes;
            }
        }
    }
}
