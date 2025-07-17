using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.ControleEstoque
{
    public class GeradorLogBalanco
    {
        private readonly Balanco balanco;
        private readonly IList<LogBalancoItem> movimentacoes = new List<LogBalancoItem>();

        public GeradorLogBalanco(Balanco balanco)
        {
            this.balanco = balanco;
        }

        public IList<LogBalancoItem> Gerar()
        {
            movimentacoes.Clear();

            if (String.IsNullOrEmpty(balanco.FechadoEm))
                throw new Exception("Só é possível emitir relatório de balancos fechados.");

            foreach (var item in balanco.BalancoItems)
            {
                var logItem = new LogBalancoItem()
                {
                    ObjProduto = item.Produto,
                    QtdAtual = item.Quantidade,
                };

                if (item.NotaItem == null)
                {
                    logItem.Diferenca = 0;
                    logItem.QtdAnterior = item.Quantidade;
                }
                else
                {
                    logItem.Diferenca = item.NotaItem.Quantidade;
                    logItem.QtdAnterior = item.Quantidade;
                    if (item.NotaItem.Nota.Ent_Sai == InOutType.Entrada)
                        logItem.QtdAnterior -= item.NotaItem.Quantidade;
                    else
                        logItem.QtdAnterior += item.NotaItem.Quantidade;
                }

                movimentacoes.Add(logItem);
            }

            return movimentacoes;
        }
    }

    public class LogBalancoItem
    {
        public string Produto { get; set; }
        public Produto objProduto { get; set; }
        public Produto ObjProduto
        {
            get
            {
                return objProduto;
            }
            set
            {
                objProduto = value;
                Produto = objProduto.Nome;
            }
        }
        public decimal QtdAtual { get; set; }
        public decimal QtdAnterior { get; set; }
        private decimal diferenca;
        public decimal Diferenca
        {
            get { return Math.Abs(diferenca); }
            set { diferenca = value; }
        }
        public string Codigo
        {
            get
            {
                return ObjProduto.Codigo;
            }
        }
        public InOutType Sentido { get { return diferenca > 0 ? InOutType.Saída : InOutType.Entrada; } }
        public override string ToString()
        {
            return this.ObjProduto.Nome + " " + Sentido + " " + Diferenca;
        }
    }
}
