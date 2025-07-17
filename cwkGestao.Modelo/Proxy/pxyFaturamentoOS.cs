using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyFaturamentoOS
    {
        public pxyFaturamentoOS(int id, int codigo, DateTime data, decimal qtdTotProdutos, decimal qtdTotServicos
                                , decimal descontoProdutos, decimal descontoServicos, decimal totalPedido, string descEquipamento, 
                                  decimal acrescimoServicos)
        {
            ID = id;
            Codigo = codigo;
            Data = data;
            QtdTotProdutos = qtdTotProdutos;
            QtdTotServicos = qtdTotServicos;
            DescontoProdutos = descontoProdutos;
            DescontoServicos = descontoServicos;
            TotalPedido = totalPedido;
            DescEquipamento = descEquipamento;
            AcrescimoServicos = acrescimoServicos;
        }

        [InformacaoExibicao(1, "Código", 60, InformacaoExibicao.HAlinhamento.Direita)]
        public int Codigo { get; set; }
        [InformacaoExibicao(2, "Data", 80, InformacaoExibicao.HAlinhamento.Centro)]
        public DateTime Data { get; set; }
        [InformacaoExibicao(3, "Equipamento", 195, InformacaoExibicao.HAlinhamento.Esquerda)]
        public string DescEquipamento { get; set; }        
        [InformacaoExibicao(4, "Qtd. Produtos", 95, InformacaoExibicao.HAlinhamento.Direita)]
        public decimal QtdTotProdutos { get; set; }
        [InformacaoExibicao(5, "Qtd. Serviços", 95, InformacaoExibicao.HAlinhamento.Direita)]
        public decimal QtdTotServicos { get; set; }
        [InformacaoExibicao(6, "Desconto Prod.", 95, InformacaoExibicao.HAlinhamento.Direita)]
        public decimal DescontoProdutos { get; set; }
        [InformacaoExibicao(7, "Desconto Serv.", 95, InformacaoExibicao.HAlinhamento.Direita)]
        public decimal DescontoServicos { get; set; }
        [InformacaoExibicao(8, "Acrescimo Serv.", 95, InformacaoExibicao.HAlinhamento.Direita)]
        public decimal AcrescimoServicos { get; set; }
        [InformacaoExibicao(9, "Total Pedido", 140, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public decimal TotalPedido { get; set; }

        public int ID { get; set; } 
    }
}
