using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tabela de Preço")]
    public class TabelaPrecoProduto : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private TabelaPreco _TabelaPreco;
        public virtual TabelaPreco TabelaPreco 
        { 
            get 
            { 
                return _TabelaPreco; 
            } 
            set 
            { 
                _TabelaPreco = value;
                if (_TabelaPreco != null)
                    NomeTabelaPreco = _TabelaPreco.Nome;
                else
                    NomeTabelaPreco = String.Empty;
            } 
        }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private Produto _Produto;
        public virtual Produto Produto { get { return _Produto; } set { _Produto = value; } }

        public virtual DateTime? Dt { get; set; }

        private decimal _MargemLucro;
        [InformacaoExibicao(2, "Lucro", 220)]
        public virtual decimal MargemLucro { get { return _MargemLucro; } set { _MargemLucro = value; } }

        private decimal _PDesconto;
        [InformacaoExibicao(4, "Desconto", 230)]
        public virtual decimal PDesconto { get { return _PDesconto; } set { _PDesconto = value; } }

        private decimal _PAcrescimo;
        [InformacaoExibicao(3, "Acréscimo", 230)]
        public virtual decimal PAcrescimo { get { return _PAcrescimo; } set { _PAcrescimo = value; } }

        public virtual string NomeTabelaPreco  { get; set; }

        private decimal valorDeVendaProduto;
        public virtual decimal ValorDeVendaProduto
        {
            get
            {
                if (_TabelaPreco != null && _Produto != null)
                {
                    decimal lucro = _Produto.PrecoBase * _MargemLucro / 100;
                    valorDeVendaProduto = _Produto.PrecoBase +lucro;
                }
                else
                {
                    valorDeVendaProduto = 0M;
                }

                return valorDeVendaProduto;
            }
        }


        public static TabelaPrecoProduto CloneNewObject(TabelaPrecoProduto objTabelaPrecoProduto)
        {
            AutoMapper.Mapper.CreateMap<TabelaPrecoProduto, TabelaPrecoProduto>()
                    .ForMember(dest => dest.ID, o => o.Ignore());

            return AutoMapper.Mapper.Map<TabelaPrecoProduto, TabelaPrecoProduto>(objTabelaPrecoProduto);
        }

        public static void GetClones(ref Produto objProduto)
        {
            IList<TabelaPrecoProduto> lstTabelaPrecoProduto = new List<TabelaPrecoProduto>(objProduto.TabelaPrecoProdutos);
            objProduto.TabelaPrecoProdutos = new List<TabelaPrecoProduto>();
            foreach (var TabelaPrecoProduto in lstTabelaPrecoProduto)
            {
                TabelaPrecoProduto Item = TabelaPrecoProduto.CloneNewObject(TabelaPrecoProduto);
                Item.Produto = objProduto;
                objProduto.TabelaPrecoProdutos.Add(Item);
            }
        }

        public override string ToString()
        {
            return TabelaPreco?.Nome;
        }
    }
}
