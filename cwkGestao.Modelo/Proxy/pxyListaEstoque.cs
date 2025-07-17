using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyListaEstoque
    {
        public pxyListaEstoque(string codigoproduto, string nomeproduto, string unidade, decimal estoquefisico,
                               decimal qtdpedido, decimal estminimo, string codbarra, string codbarrafornecedor,
                               string grupoestoque, string ncmproduto, string grupo1, string grupo2, string grupo3)
        {
            CodigoProduto = codigoproduto;
            NomeProduto = nomeproduto;
            Unidade = unidade;
            EstoqueFisico = estoquefisico;
            QtdPedido = qtdpedido;
            EstMinimo = estminimo;
            CodBarra = codbarra;
            CodBarraFornecedor = codbarrafornecedor;
            GrupoEstoque = grupoestoque;
            NCMProduto = ncmproduto;
            Grupo1 = grupo1;
            Grupo2 = grupo2;
            Grupo3 = grupo3;
        }

        [InformacaoExibicao(1, "Código", 95)]
        public virtual string CodigoProduto { get; set; }
        [InformacaoExibicao(2, "Nome", 460)]
        public virtual string NomeProduto { get; set; }
        [InformacaoExibicao(3, "Und", 45)]
        public virtual string Unidade { get; set; }
        [InformacaoExibicao(4, "Físico", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal EstoqueFisico { get; set; }
        [InformacaoExibicao(5, "Reserva", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal QtdPedido { get; set; }
        [InformacaoExibicao(6, "Saldo", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal SaldoEfetivo 
        {
            get
            {
                return EstoqueFisico - QtdPedido;
            }
        }
        [InformacaoExibicao(7, "Est. Mínimo", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal EstMinimo { get; set; }
        [InformacaoExibicao(8, "Barra", 100)]
        public virtual string CodBarra { get; set; }
        [InformacaoExibicao(9, "Barra Fornecedor", 100)]
        public virtual string CodBarraFornecedor { get; set; }
        [InformacaoExibicao(10, "Grupo Estoque", 100)]
        public virtual string GrupoEstoque { get; set; }
        [InformacaoExibicao(11, "NCM", 100)]
        public virtual string NCMProduto { get; set; }
        [InformacaoExibicao(12, "Grupo1", 100)]
        public virtual string Grupo1 { get; set; }
        [InformacaoExibicao(13, "Grupo2", 100)]
        public virtual string Grupo2 { get; set; }
        [InformacaoExibicao(14, "Grupo3", 100)]
        public virtual string Grupo3 { get; set; }
        [InformacaoExibicao(15, "Situação", 100)]
        public virtual SituacaoEstoque Situacao
        {
            get
            {
                if (SaldoEfetivo < 0)
                    return SituacaoEstoque.AbaixoZero;
                if (SaldoEfetivo < EstMinimo)
                    return SituacaoEstoque.AbaixoMinimo;
                return SituacaoEstoque.Normal;
            }
        }

        public enum SituacaoEstoque
        {
            AbaixoZero = 0,
            AbaixoMinimo = 1,
            Normal = 2
        }
    }
}
