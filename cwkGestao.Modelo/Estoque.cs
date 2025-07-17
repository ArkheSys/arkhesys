using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Estoque")]
    public class Estoque : ModeloBase
    {
        public Estoque()
        {
            Identificadores = new List<EstoqueIdentificador>();
            Qtd_Estoque_Fisico = 0.0000m;
            Qtd_Estoque_Min = 0.0000m;
            Qtd_Compra_Pedido = 0.0000m;
            Qtd_Estoque_Pedido = 0.0000m;
            Localidade = String.Empty;
        }

        public override int ID { get; set; }

        public virtual Produto Produto { get; set; }

        [InformacaoExibicao(2, "Físico", 75)]
        public virtual decimal Qtd_Estoque_Fisico { get; set; }

        [InformacaoExibicao(3, "Reserva", 75)]
        public virtual decimal Qtd_Estoque_Pedido { get; set; }

        [InformacaoExibicao(5, "Compra", 75)]
        public virtual decimal Qtd_Compra_Pedido { get; set; }

        
        public virtual string Localidade { get; set; }

        [InformacaoExibicao(6, "Qtd. Mínima", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Qtd_Estoque_Min { get; set; }

        [InformacaoExibicao(4, "Efetivo", 75)]
        public virtual decimal Efetivo { get { return Qtd_Estoque_Fisico - Qtd_Estoque_Pedido; } }

        public virtual decimal CustoMedioUnit { get; set; }

        public virtual LocalEstoque LocalEstoque { get; set; }

        [InformacaoExibicao(7, "Localidade", 220)]
        public virtual string NomeLocalEstoque { get { return LocalEstoque != null ? LocalEstoque.Descricao : String.Empty; } }

        public virtual IList<EstoqueIdentificador> Identificadores { get; set; }



        public static Estoque CloneNewObject(Estoque objEstoque)
        {
            AutoMapper.Mapper.CreateMap<Estoque, Estoque>()
                    .ForMember(dest => dest.ID, o => o.Ignore());

            return AutoMapper.Mapper.Map<Estoque, Estoque>(objEstoque);
        }

        public static void GetClones(ref Produto objProduto)
        {
            IList<Estoque> lstEstoque = new List<Estoque>(objProduto.Estoques);
            objProduto.Estoques = new List<Estoque>();
            foreach (var Estoque in lstEstoque)
            {
                Estoque Item = Estoque.CloneNewObject(Estoque);
                Item.Produto = objProduto;
                objProduto.Estoques.Add(Item);
            }
        }
    }
}
