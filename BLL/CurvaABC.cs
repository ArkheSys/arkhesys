using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CurvaABC
    {
        public Modelo.ConsultaABC ConsultarCurvaABC(int idFilial, DateTime datai, DateTime dataf, Modelo.DataClassesDataContext db)
        {
            Modelo.ConsultaABC ret = new Modelo.ConsultaABC();

            var etapa1 = (from i in db.NotaItems
                          join n in db.Notas on i.IDNota equals n.ID
                          where n.IDFilial == idFilial
                          && n.Dt >= datai
                          && n.Dt <= dataf
                          group i by new { i.Produto } into g
                          select new
                          {
                              Codigo = g.Key.Produto.Codigo,
                              Nome = g.Key.Produto.Nome,
                              PrecoUnitario = g.Key.Produto.PrecoBase,
                              Quantidade = g.Sum(s => s.Quantidade),
                              ValorConsumo = g.Sum(a => a.Produto.PrecoBase)
                          }).OrderByDescending(o => o.ValorConsumo);

            List<Modelo.CurvaABC> etapa2 = new List<Modelo.CurvaABC>();
            decimal ValorConsumo_Ant = 0;
            Modelo.CurvaABC objCurvaABC;
            foreach (var item in etapa1)
            {
                objCurvaABC = new Modelo.CurvaABC();
                objCurvaABC.Codigo = item.Codigo;
                objCurvaABC.Nome = item.Nome;
                objCurvaABC.ValorConsumo = item.ValorConsumo.HasValue ? item.ValorConsumo.Value : 0;
                objCurvaABC.ValorConsumoAcumulado = objCurvaABC.ValorConsumo + ValorConsumo_Ant;
                objCurvaABC.PorcSobreTotalAcumulado = 0;
                objCurvaABC.Quantidade = item.Quantidade;
                etapa2.Add(objCurvaABC);

                ValorConsumo_Ant = objCurvaABC.ValorConsumoAcumulado;
            }

            objCurvaABC = etapa2.Count > 0 ? etapa2.Last() : new Modelo.CurvaABC();
            for (int i = 0; i < etapa2.Count; i++)
            {
                etapa2[i].PorcSobreTotalAcumulado = etapa2[i].ValorConsumoAcumulado / objCurvaABC.ValorConsumoAcumulado;
                etapa2[i].Classificacao = i + 1;
            }

            decimal quantidade = etapa2.Count
                , qtdClasseA = 0
                , qtdClasseB = 0
                , qtdClasseC = 0;

            qtdClasseA = Math.Floor(quantidade * (decimal)0.2);
            qtdClasseB = Math.Floor(quantidade * (decimal)0.3);
            qtdClasseC = quantidade - (qtdClasseA + qtdClasseB);

            if (qtdClasseA >= 1)
            {
                ret.ClasseA = new List<Modelo.CurvaABC>();
                ret.ClasseB = new List<Modelo.CurvaABC>();
                ret.ClasseC = new List<Modelo.CurvaABC>();

                for (int i = 0; i < etapa2.Count; i++)
                {
                    if (i < qtdClasseA)
                        ret.ClasseA.Add(etapa2[i]);
                    else if (i < qtdClasseA+qtdClasseB)
                        ret.ClasseB.Add(etapa2[i]);
                    else
                        ret.ClasseC.Add(etapa2[i]);
                }
            }

            return ret;
        }
    }
}
