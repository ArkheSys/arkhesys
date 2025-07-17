using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco9;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_9 : Bloco
    {
        public Reg_9001 Abertura { get; set; }
        public IList<Reg_9900> Registros { get; set; }
        public Reg_9990 EncerramentoBloco { get; set; }
        public Reg_9999 EncerramentoArquivo { get; set; }

        public Bloco_9()
        {
            ParamsBloco = new Dictionary<string, object>();
        }

        public override void GerarBloco()
        {
            Dictionary<string, int> contagemGeral = new Dictionary<string, int>();
            if (ParamsBloco.ContainsKey("CONTAGEM"))
            {
                contagemGeral = (Dictionary<string, int>)ParamsBloco["CONTAGEM"];
            }
            Abertura = new Reg_9001();
            Abertura.IND_MOV = 0;
            contagemGeral.Add("9001", 1);

            Registros = new List<Reg_9900>();
            foreach (var item in contagemGeral)
            {
                Reg_9900 reg = new Reg_9900();
                reg.REG_BLC = item.Key;
                reg.QTD_REG_BLC = item.Value;
                Registros.Add(reg);
            }
            EncerramentoArquivo = new Reg_9999() { QTD_LIN = 0 };
            EncerramentoBloco = new Reg_9990();

            Registros.Add(new Reg_9900() { REG_BLC = "9900", QTD_REG_BLC = Registros.Count + 2 });
            Registros.Add(new Reg_9900() { REG_BLC = "9990", QTD_REG_BLC = 1 });
            Registros.Add(new Reg_9900() { REG_BLC = "9999", QTD_REG_BLC = 1 });

            EncerramentoBloco.QTD_LIN_9 = GetContagemRegistros();

            contagemGeral.Add("9900", Registros.Count);
            contagemGeral.Add("9990", 1);
            contagemGeral.Add("9999", 1);

            EncerramentoArquivo.QTD_LIN = contagemGeral.Sum(s => s.Value);
        }
    }
}