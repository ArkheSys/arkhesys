using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Item de Balanço")]
    public class BalancoItem : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Balanco Balanco { get; set; }
        [InformacaoExibicao(1, "Produto", 300)]
        public virtual Produto Produto { get; set; }
        public virtual NotaItem NotaItem { get; set; }
        [InformacaoExibicao(2, "Quantidade", 110, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(3, "Grupo1", 200)]
        public virtual string Grupo1 { get { return Produto != null && Produto.Grupo1 != null ? Produto.Grupo1.Nome : String.Empty; } }
        [InformacaoExibicao(4, "Grupo2", 200)]
        public virtual string Grupo2 { get { return Produto != null && Produto.Grupo2 != null ? Produto.Grupo2.Nome : String.Empty; } }
        [InformacaoExibicao(5, "Grupo3", 200)]
        public virtual string Grupo3 { get { return Produto != null && Produto.Grupo3 != null ? Produto.Grupo3.Nome : String.Empty; } }
        [InformacaoExibicao(6, "Código Marca", 100)]
        public virtual string CodMarca
        {
            get { return Produto != null && Produto.Grupo1 != null ? Produto.Grupo1.Codigo.ToString() : String.Empty; }
        }
    }
}
