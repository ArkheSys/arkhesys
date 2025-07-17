using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyFrenteCaixaNotaItemCancelado
    {
        public int Codigo { get; set; }
        public string Operador { get; set; }
        public string Gerente { get; set; }
        public string NumeroPDV { get; set; }
        public string MotivoCancelamento { get; set; }
        public decimal TotalGeral { get; set; }

        public pxyFrenteCaixaNotaItemCancelado(int Codigo, string Operador, string Gerente, string NumeroPDV, string MotivoCancelamento, decimal TotalGeral)
        {
            this.Codigo = Codigo;
            this.Operador = Operador;
            this.Gerente = Gerente;
            this.NumeroPDV = NumeroPDV;
            this.MotivoCancelamento = MotivoCancelamento;
            this.TotalGeral = TotalGeral;
        }
    }

    public class pxyItemFrenteCaixaPorFilial
    {
        public int IDFilial { get; set; }
        public string Filial { get; set; }
        public int IDOperador { get; set; }
        public string Operador { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IDProduto { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int IDTabelaPreco { get; set; }
        public string TabelaPreco { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Total { get; set; }

        public pxyItemFrenteCaixaPorFilial()
        {
        }

        public pxyItemFrenteCaixaPorFilial(int IDFilial,
             string Filial,
             int IDOperador,
             string Operador,
             DateTime DataCadastro,
             int IDProduto,
             string Codigo,
             string Nome,
             int IDTabelaPreco,
             string TabelaPreco,
             decimal Quantidade,
             decimal Total)
        {
            this.IDFilial = IDFilial;
            this.Filial = Filial;
            this.IDOperador = IDOperador;
            this.Operador = Operador;
            this.DataCadastro = DataCadastro;
            this.IDProduto = IDProduto;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.IDTabelaPreco = IDTabelaPreco;
            this.TabelaPreco = TabelaPreco;
            this.Quantidade = Quantidade;
            this.Total = Total;
        }
    }
}