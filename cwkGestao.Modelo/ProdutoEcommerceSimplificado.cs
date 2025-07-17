using System;

namespace cwkGestao.Modelo
{
    public class ProdutoEcommerceSimplificado : ModeloBase
    {
        public override int ID { get; set; }

        public virtual int? IdEcommerce { get; set; }

        public virtual int Codigo { get; set; }
        public virtual string CodigoBarra { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal PrecoNormal { get; set; }
        public virtual decimal PrecoPromocional { get; set; }
        public virtual string ImagemProduto { get; set; }
        public virtual int IntegrarEcommerce { get; set; }
        public virtual DateTime? DataPromocionalInicial { get; set; }
        public virtual DateTime? DataPromocionalFinal { get; set; }
        public virtual string Observacao { get; set; }

        public virtual string PrecoNormalStr => PrecoNormal.ToString("c2");
        public virtual string PrecoPromocionalStr => PrecoPromocional.ToString("c2");
        public virtual string DataPromocionalInicialStr => DataPromocionalInicial?.ToString("dd/MM/yyyy");
        public virtual string DataPromocionalFinalStr => DataPromocionalFinal?.ToString("dd/MM/yyyy");

        public virtual bool Promocional => DateTime.Now.Date >= DataPromocionalInicial?.Date && 
                                           DateTime.Now.Date <= DataPromocionalFinal?.Date && 
                                           PrecoPromocional != 0;
    }
}