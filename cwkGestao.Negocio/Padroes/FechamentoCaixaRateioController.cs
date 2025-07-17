using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using Modelo;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
	public class FechamentoCaixaController : BaseController<FechamentoCaixa>
	{
		#region Singleton
        private static FechamentoCaixaController instance;
        private static IRepositorioFechamentoCaixa repositorioFechamentoCaixa;

        private FechamentoCaixaController()
        { }
		
		static FechamentoCaixaController()
        {
            instance = new FechamentoCaixaController();
            repositorioT = RepositorioFactory<FechamentoCaixa>.GetRepositorio();
            repositorioFechamentoCaixa = (IRepositorioFechamentoCaixa)repositorioT;
        }
			
		public static FechamentoCaixaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FechamentoCaixa objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<FechamentoCaixaEmMemoria> GetFechamentoCaixaGrid(cwkGestao.Modelo.Banco banco, DateTime dtInicio, DateTime dtFinal)
        {
            IList<FechamentoCaixaEmMemoria> lResultado = new List<FechamentoCaixaEmMemoria>();
            var dtInicioBKP = dtInicio;
            var fechamento = FechamentoCaixaController.Instancia.GetBancoPeriodo(banco, dtInicio, dtFinal);
            var movcaixa = MovCaixaController.Instancia.GetPorBancoEData(banco, dtInicio, dtFinal);

            var lMovCaixa = movcaixa.GroupBy(x => x.Dt.ToShortDateString());

            while (dtInicio <= dtFinal)
            {
                if (lMovCaixa.Where(x => DateTime.Parse(x.Key) == dtInicio).Count() == 0)
                {
                    Modelo.MovCaixa movCaixaNew = new Modelo.MovCaixa();
                    movCaixaNew.Banco = banco;
                    movCaixaNew.Dt = dtInicio;
                    movCaixaNew.Ent_Sai = Ent_SaiMovCaixa.Entrada;
                    movCaixaNew.Valor = 0;
                    movcaixa.Add(movCaixaNew);
                }
                dtInicio = dtInicio.AddDays(1);
            }

            dtInicio = dtInicioBKP;
            var lMovCaixaNew = movcaixa.GroupBy(x => x.Dt.ToShortDateString()).OrderBy(x => DateTime.Parse(x.Key));

            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                decimal saldoanterior = (Decimal)db.f_SaldoAntBanco(banco.ID, dtInicio);
                foreach (var item in lMovCaixaNew)
                {
                    FechamentoCaixaEmMemoria fCaixaMemoria = new FechamentoCaixaEmMemoria();
                    fCaixaMemoria.Data = DateTime.Parse(item.Key);

                    var fec = fechamento.Where(x => DateTime.Parse(x.DtConciliacao.ToShortDateString()) == DateTime.Parse(item.Key) && x.bFechado);
                    fCaixaMemoria.UsuarioFechamento = fec.Count() > 0 ? fec.First().UsuarioFechamento : "";
                    fCaixaMemoria.Fechado = fec.Count() > 0 ? fec.First().bFechado : false;
                    fCaixaMemoria.Entrada = fec.Count() > 0 ? fec.First().ValorEntrada : item.Sum(x => x.Ent_Sai == Ent_SaiMovCaixa.Entrada ? (decimal)x.Valor : 0);
                    fCaixaMemoria.Saida = fec.Count() > 0 ? fec.First().ValorSaida : item.Sum(x => x.Ent_Sai == Ent_SaiMovCaixa.Saída ? (decimal)x.Valor : 0);
                    fCaixaMemoria.SaldoInicial = fec.Count() > 0 ? fec.First().SaldoInicial : saldoanterior;
                    saldoanterior = (decimal)saldoanterior + fCaixaMemoria.Entrada - fCaixaMemoria.Saida;
                    fCaixaMemoria.SaldoFinal = fec.Count() > 0 ? fec.First().Saldo : saldoanterior;

                    if (fec.Count() > 0)
                        fCaixaMemoria.DtFechamento = fec.First().DtFechamento;

                    lResultado.Add(fCaixaMemoria);
                }
            }
            return lResultado;
        }

        public bool PodeFecharCaixa(DateTime data, Modelo.Banco banco)
        {
            return repositorioFechamentoCaixa.HqlPodeFecharCaixa(data, banco);
        }

        public bool PodeAbrirCaixa(DateTime data, Modelo.Banco banco)
        {
            return repositorioFechamentoCaixa.HqlPodeAbrirCaixa(data, banco);
        }

        public FechamentoCaixa GetFechamentoPorData(DateTime data, Modelo.Banco banco)
        {
            return repositorioFechamentoCaixa.HqlGetFechamentoPorData(data, banco);
        }

        public IList<FechamentoCaixa> GetBancoPeriodo(Modelo.Banco banco, DateTime dtInicio, DateTime dtFinal)
        {
            return repositorioFechamentoCaixa.GetBancoPeriodo(banco, dtInicio, dtFinal);
        }

        public bool ExisteFechamentoPosterior(DateTime data, Modelo.Banco banco)
        {
            return repositorioFechamentoCaixa.HqlExisteFechamentoPosterior(data, banco);
        }
	}
}
