using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelFluxoCaixa
    {
        public pxyRelFluxoCaixa(DateTime data, decimal pagar, decimal receber, int numero, string pessoa, int parcela, int qtdParcelas)
        {
            Data = data;
            Pagar = pagar;
            Receber = receber;
            Acumulado = 0;
            Numero = numero.ToString();
            Tipo = "Pedido";
            Pessoa = pessoa;
            Situacao_Parcela = parcela + " de " + qtdParcelas;
        }

        public pxyRelFluxoCaixa() { }

        public DateTime Data { get; set; }
        public decimal Pagar { get; set; }
        public decimal Receber { get; set; }
        public decimal Acumulado { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Pessoa { get; set; }
        public string Situacao_Parcela { get; set; }
    }
}
