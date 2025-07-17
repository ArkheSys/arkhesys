using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Relatorios
{
    public class FluxoCaixaRealizado
    {
        public static IList<object> GetRelatorioPorPlanoContas(DateTime dataInicial, DateTime dataFinal, PlanoConta planoConta, string idsEmpresas, bool incluirPlanosContaZerados, bool filtrarPorDataCompetencia)
        {
            if (planoConta == null)
                planoConta = new PlanoConta() { Classificacao = String.Empty };

            IList<Movimento> movimentos = MovimentoController.Instancia.GetRelatorioPorPlanoContas(dataInicial, dataFinal, planoConta.Classificacao, idsEmpresas, filtrarPorDataCompetencia);
            IList<PlanoConta> planosContas = PlanoContaController.Instancia.GetPlanoContasPorClassificacao(planoConta.Classificacao);

            IList<object> retorno = new List<object>();
            string[] niveisPlanoContas = new string[16];
            PlanoConta planoContaMov;

            if (incluirPlanosContaZerados)
            {
                foreach (var pc in planosContas.Where(p => p.BTitulo == false && p.PlanoContaFilhos.Count == 0))
                {
                    IncluirPlanoContaZerado(planoConta, retorno, niveisPlanoContas, pc);
                }
            }

            foreach (Movimento mov in movimentos.OrderBy(m1 => m1.Dt).OrderBy(m2 => m2.PlanoConta.Classificacao))
            {
                planoContaMov = planosContas.Where(p => p.ID == mov.PlanoConta.ID).First();
                LimpaNiveisPlanoContas(niveisPlanoContas);
                short indiceFinal = Convert.ToInt16(niveisPlanoContas.Length - 1);
                PreencheNiveisPlanoContas(planoConta.ID, planoContaMov, niveisPlanoContas, ref indiceFinal);
                AdicionaRegistroRelatorio(retorno, niveisPlanoContas, mov, filtrarPorDataCompetencia);

                if (planoContaMov.PlanoContaPai != null)
                {
                    IncluirFilhosZerados(planoConta, retorno, niveisPlanoContas, planoContaMov.PlanoContaPai);
                }
            }
            return retorno;
        }

        private static void IncluirFilhosZerados(PlanoConta planoConta, IList<object> retorno, string[] niveisPlanoContas, PlanoConta planoContaMov)
        {
            try
            {
                
                foreach (var item in planoContaMov.PlanoContaFilhos)
                {
                    IncluirPlanoContaZerado(planoConta, retorno, niveisPlanoContas, item);
                }
            }
            catch (Exception z)
            {
                throw z;
            }

        }

        private static void IncluirPlanoContaZerado(PlanoConta planoConta, IList<object> retorno, string[] niveisPlanoContas, PlanoConta item)
        {
            LimpaNiveisPlanoContas(niveisPlanoContas);
            short indiceFinal = Convert.ToInt16(niveisPlanoContas.Length - 1);
            PreencheNiveisPlanoContas(planoConta.ID, item, niveisPlanoContas, ref indiceFinal);

            AdicionaRegistroRelatorio(retorno, niveisPlanoContas, 0, "", "", "", 0, "");
        }

        private static void AdicionaRegistroRelatorio(IList<object> retorno, string[] niveisPlanoContas, Movimento mov, bool filtrarPorDataCompetencia)
        {
            if (mov.Documento == null)
            {
                AdicionaRegistroRelatorio(retorno, niveisPlanoContas, mov.ID, ""
                                          , mov.Dt.ToShortDateString(), "H: " + mov.MovCaixa.ComplementoHist
                                          , (mov.MovCaixa.Ent_Sai == Ent_SaiMovCaixa.Entrada ? mov.Valor : -mov.Valor)
                                          , "1/1");
            }
            else
            {
                string data;
                if (filtrarPorDataCompetencia)
                    data = mov.Documento.Dt.ToShortDateString();
                else
                    data = mov.Dt.ToShortDateString();

                AdicionaRegistroRelatorio(retorno, niveisPlanoContas, mov.ID, mov.Documento.NumBanco
                                          , data, "P: " + mov.Documento.Pessoa.Nome + " | H: " + mov.Documento.ComplementoHist
                                          , (mov.Documento.Tipo == TipoDocumentoType.Receber ? mov.Valor : -mov.Valor)
                                          , mov.Documento.Parcela + "/" + mov.Documento.QtParcela);
            }
        }

        private static void AdicionaRegistroRelatorio(IList<object> retorno, string[] niveisPlanoContas, int id, string numDocumento, string dt, string pessoaNome, decimal valor, string parcela)
        {
            retorno.Add(new
            {
                MovimentoID = id,
                DocumentoNumero = numDocumento,
                MovimentoData = dt,
                DocumentoPessoa = pessoaNome,
                MovimentoValor = valor,
                Nivel0 = niveisPlanoContas[0],
                Nivel1 = niveisPlanoContas[1],
                Nivel2 = niveisPlanoContas[2],
                Nivel3 = niveisPlanoContas[3],
                Nivel4 = niveisPlanoContas[4],
                Nivel5 = niveisPlanoContas[5],
                Nivel6 = niveisPlanoContas[6],
                Nivel7 = niveisPlanoContas[7],
                Nivel8 = niveisPlanoContas[8],
                Nivel9 = niveisPlanoContas[9],
                Nivel10 = niveisPlanoContas[10],
                Nivel11 = niveisPlanoContas[11],
                Nivel12 = niveisPlanoContas[12],
                Nivel13 = niveisPlanoContas[13],
                Nivel14 = niveisPlanoContas[14],
                Nivel15 = niveisPlanoContas[15],
                ParcelaDocumento = parcela
            });
        }

        private static void LimpaNiveisPlanoContas(string[] niveisPlanoContas)
        {
            for (int i = 0; i < niveisPlanoContas.Length; i++)
            {
                niveisPlanoContas[i] = String.Empty;
            }
        }

        private static void PreencheNiveisPlanoContas(int idPaiDeTodos, PlanoConta planoConta, string[] niveisPlanoContas, ref short indiceFinal)
        {
            if (planoConta == null)
                return;

            if (planoConta.ID != idPaiDeTodos && planoConta.PlanoContaPai != null
                && planoConta.PlanoContaPai.GetType() == typeof(Modelo.PlanoConta))
                PreencheNiveisPlanoContas(idPaiDeTodos, planoConta.PlanoContaPai, niveisPlanoContas, ref indiceFinal);
            if (indiceFinal >= 0)
            {
                if (planoConta.ID == idPaiDeTodos || planoConta.PlanoContaPai != null || idPaiDeTodos == 0)
                    niveisPlanoContas[indiceFinal--] = planoConta.Classificacao + " - " + planoConta.Nome;
            }
            else
                throw new Exception("O relatório não suporta a quantidade de planos de contas utilizada.");
        }
    }
}
