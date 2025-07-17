using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioProdutoContrato
    {
        public int IDContrato { get; set; }
        public int ID { get; set; }
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string Pessoa { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }

        public pxRelatorioProdutoContrato() { }

        public pxRelatorioProdutoContrato(int IDContrato, int ID, int Codigo, DateTime? Data, string Pessoa, string Descricao, string Tipo)
        {
            this.IDContrato = IDContrato;
            this.ID = ID;
            this.Codigo = Codigo;
            this.Data = Data;
            this.Pessoa = Pessoa;
            this.Descricao = Descricao;
            this.Tipo = Tipo;
        }
    }
}