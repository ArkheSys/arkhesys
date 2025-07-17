using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Frente Caixa Nota Item")]
    public class FrenteCaixaNotaItem : ModeloBase
    {
        public FrenteCaixaNotaItem()
        {
            Quantidade = 1;
            PercDesconto = 0;
            Desconto = 0;
        }

        public override int ID { get; set; }
        public virtual FrenteCaixaNota FrenteCaixaNota { get; set; }
        public virtual int Sequencia { get; set; }
        public virtual string CodigoProduto
        {
            get
            {
                try
                {
                    return Produto.Codigo;
                }
                catch { return String.Empty; }
            }
        }
        public virtual Produto Produto { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual string QuantidadeStr
        {
            get
            {
                return String.Format(Produto.Unidade.BQtdFracionada ? "{0:N4}" : "{0:N0}", Quantidade);
            }
        }
        public virtual string Unidade
        {
            get
            {
                return Produto.Unidade.Sigla;
            }
        }
        public virtual decimal Valor { get; set; }
        public virtual decimal ValorCalculado { get; set; }
        public virtual decimal PercDesconto { get; set; }
        public virtual decimal Desconto { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string Gerente { get; set; }

        public virtual bool Cancelado { get; set; }
        public virtual string MotivoCancelamento { get; set; }

        public override string ToString()
        {
            var linha = "";
            linha = String.Format("{0:000}", Sequencia) + " " +
                    String.Format("{0,-16}", Produto.Barra) + " " +
                    String.Format("{0,-14}", String.IsNullOrEmpty(Produto.DescReduzida) ? Produto.Nome.GetNoMaximoXCaracteres(30) : Produto.DescReduzida) + " " + Environment.NewLine +
                    String.Format("{0,35}", Unidade) +
                    String.Format("{0,8}", QuantidadeStr) + " X " +
                    String.Format("{0,14}", String.Format("{0:N2}", Valor)) + " " +
                    String.Format("{0,11}", String.Format("{0:N2}", Total)) + " ";
            linha += Environment.NewLine;
            return linha;
        }
    }
}
