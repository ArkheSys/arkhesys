using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cotação")]
    public class Cotacao : ModeloBase
    {
        public Cotacao()
        {
            CotacaoProdutos = new List<CotacaoProduto>();
        }

        public override int ID { get; set; }
        [InformacaoExibicao(0, "Código", 80)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Tipo", 86)]
        public virtual TipoCotacao TipoCotacao { get; set; }
        public virtual DateTime Dt { get; set; }
        [InformacaoExibicao(1, "Resumo", 610)]
        public virtual string Resumo { get; set; }
        public virtual string Observacao { get; set; }
        public virtual DateTime DtBaixa { get; set; }
        public virtual DateTime DtCanc { get; set; }
        public virtual int IDFornecedor_01 { get; set; }
        public virtual int IDFornecedor_02 { get; set; }
        public virtual int IDFornecedor_03 { get; set; }
        public virtual int IDFornecedor_04 { get; set; }
        public virtual int IDFornecedor_05 { get; set; }
        public virtual int IDFornecedor_06 { get; set; }
        public virtual int IDCondicao_01 { get; set; }
        public virtual int IDCondicao_02 { get; set; }
        public virtual int IDCondicao_03 { get; set; }
        public virtual int IDCondicao_04 { get; set; }
        public virtual int IDCondicao_05 { get; set; }
        public virtual int IDCondicao_06 { get; set; }
        public virtual decimal Frete_01 { get; set; }
        public virtual decimal Frete_02 { get; set; }
        public virtual decimal Frete_03 { get; set; }
        public virtual decimal Frete_04 { get; set; }
        public virtual decimal Frete_05 { get; set; }
        public virtual decimal Frete_06 { get; set; }
        public virtual decimal IPI_01 { get; set; }
        public virtual decimal IPI_02 { get; set; }
        public virtual decimal IPI_03 { get; set; }
        public virtual decimal IPI_04 { get; set; }
        public virtual decimal IPI_05 { get; set; }
        public virtual decimal IPI_06 { get; set; }
        public virtual decimal Desconto_01 { get; set; }
        public virtual decimal Desconto_02 { get; set; }
        public virtual decimal Desconto_03 { get; set; }
        public virtual decimal Desconto_04 { get; set; }
        public virtual decimal Desconto_05 { get; set; }
        public virtual decimal Desconto_06 { get; set; }
        public virtual string PrazoEntrega_01 { get; set; }
        public virtual string PrazoEntrega_02 { get; set; }
        public virtual string PrazoEntrega_03 { get; set; }
        public virtual string PrazoEntrega_04 { get; set; }
        public virtual string PrazoEntrega_05 { get; set; }
        public virtual string PrazoEntrega_06 { get; set; }
        public virtual string UsuarioAutorizacao { get; set; }
        public virtual DateTime DtAutorizacao { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual int Status { get; set; }
        public virtual IList<CotacaoProduto> CotacaoProdutos { get; set; }

    }
}