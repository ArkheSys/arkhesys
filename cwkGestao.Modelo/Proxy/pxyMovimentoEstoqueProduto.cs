using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyMovimentoEstoqueProduto
    {
        public pxyMovimentoEstoqueProduto(int codNota, int codFilial, string serieNota
            , int numeroNota, DateTime data, string tipo, decimal quantidade, decimal valorUnit
            , decimal custoUnit, decimal custoMedio, decimal cmvUnit, string identificador
            , decimal quantidadeIdent, decimal cmvUnitIdent)
        {
            CodNota = codNota;
            CodFilial = codFilial;
            SerieNota = serieNota;
            NumeroNota = numeroNota;
            Data = data;
            Tipo = tipo;
            Quantidade = quantidade;
            ValorUnit = valorUnit;
            CustoUnit = custoUnit;
            CustoMedio = custoMedio;
            CMVUnit = cmvUnit;
            Identificador = identificador;
            QuantidadeIdent = quantidadeIdent;
            CMVUnitIdent = cmvUnitIdent;
        }

        public int CodNota { get; set; }
        public int CodFilial { get; set; }
        public string SerieNota { get; set; }
        public int NumeroNota { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnit { get; set; }
        public decimal CustoUnit { get; set; }
        public decimal CustoMedio { get; set; }
        public decimal CMVUnit { get; set; }
        public string Identificador { get; set; }
        public decimal QuantidadeIdent { get; set; }
        public decimal CMVUnitIdent { get; set; }
    }
}
