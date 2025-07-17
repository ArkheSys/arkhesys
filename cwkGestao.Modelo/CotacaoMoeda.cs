using System;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cotação Moeda")]
    public class CotacaoMoeda : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int? _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int? Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private DateTime? _DataCotacao;
        [InformacaoExibicao(2, "DataCotacao", 150)]
        public virtual DateTime? DataCotacao { get { return _DataCotacao; } set { _DataCotacao = value; } }

        private int? _IDMoeda;
        [InformacaoExibicao(3, "Moeda", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int? IDMoeda { get { return _IDMoeda; } set { _IDMoeda = value; } }       
          
        private decimal _ValorCotacao;
        [InformacaoExibicao(4, "ValorCotacao", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorCotacao { get { return _ValorCotacao; } set { _ValorCotacao = value; } }

        private decimal _ValorCompra;
        [InformacaoExibicao(5, "ValorCompra", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorCompra { get { return _ValorCompra; } set { _ValorCompra = value; } }

        private decimal _ValorVenda;
        [InformacaoExibicao(6, "ValorVenda", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorVenda { get { return _ValorVenda; } set { _ValorVenda = value; } }

        private int _HabilitaCotacao;
        [InformacaoExibicao(7, "HabilitaCotacao", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual int HabilitaCotacao { get { return _HabilitaCotacao; } set { _HabilitaCotacao = value; } }

        public virtual string Moeda { get; set; }
    }
}
