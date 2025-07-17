using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioProdutoContratoValor
    {
        public int IDContrato { get; set; }
        public int ID { get; set; }
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string Pessoa { get; set; }
        public string CPF_CNPJ { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }

        public pxRelatorioProdutoContratoValor() { }

        public pxRelatorioProdutoContratoValor(int IDContrato, int ID, int Codigo, DateTime? Data, string Pessoa, string CPF_CNPJ, string Descricao, string Tipo, decimal Valor)
        {
            this.IDContrato = IDContrato;
            this.ID = ID;
            this.Codigo = Codigo;
            this.Data = Data;
            this.Pessoa = Pessoa;
            this.CPF_CNPJ = CPF_CNPJ;
            this.Descricao = Descricao;
            this.Tipo = Tipo;
            this.Valor = Valor;
        }
    }
}