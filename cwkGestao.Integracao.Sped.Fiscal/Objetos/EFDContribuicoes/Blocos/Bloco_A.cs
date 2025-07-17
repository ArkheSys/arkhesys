using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_A : Bloco
    {
        private IList<Nota> notas;

        public Reg_A001 Abertura { get; set; }
        public IList<Reg_A010> IdentificacaoEstabelecimento { get; set; }
        public IList<BA_Notas> SubBlocoNotasServico { get; set; }
        public Reg_A990 Fechamento { get; set; }

        public override void GerarBloco()
        {
            Abertura = new Reg_A001();
            if (notas.Count == 0)
            {
                Abertura.IND_MOV = 1;
            }
            if (Abertura.IND_MOV == 0)
            {
                IdentificacaoEstabelecimento = new List<Reg_A010>();
                SubBlocoNotasServico = new List<BA_Notas>();

                var FiliaisEnvolvidas = notas.GroupBy(g => g.Filial.CNPJ);
                foreach (var item in FiliaisEnvolvidas)
                {
                    IdentificacaoEstabelecimento.Add(new Reg_A010() { CNPJ = new string(item.Key.ToCharArray().Where((c => char.IsLetterOrDigit(c))).ToArray())});
                    foreach (Nota n in item)
                    {
                        BA_Notas nota = new BA_Notas(n);
                        if (ParamsBloco.ContainsKey("COMP_DOC"))
                        {
                            nota.ParamsBloco.Add("COMP_DOC", ParamsBloco["COMP_DOC"]);
                        }
                        if (ParamsBloco.ContainsKey("COD_PART"))
                        {
                            nota.ParamsBloco.Add("COD_PART", ParamsBloco["COD_PART"]);
                        }

                        nota.GerarBloco();
                        SubBlocoNotasServico.Add(nota);
                    }
                }
            }
            Fechamento = new Reg_A990();
            Fechamento.QTD_LIN_A = GetContagemRegistros();
        }

        public Bloco_A(IList<Nota> notasServico, DateTime inicio, DateTime fim)
        {
            ParamsBloco = new Dictionary<string, object>();
            notas = new List<Nota>();
            foreach (Nota item in notasServico)
            {
                DateTime dt = item.DtHoraAuto.HasValue ? item.DtHoraAuto.Value : item.DtHoraCanc.HasValue ? item.DtHoraCanc.Value : item.Dt;
                if (dt > inicio.Date && dt < fim.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                {
                    notas.Add(item);
                }
            }
        }
    }
}