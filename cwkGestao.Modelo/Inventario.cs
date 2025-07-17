using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Inventário")]
    public class Inventario : ModeloBase
	{
        public Inventario()
        {
            ListaProdutos = new List<InventarioProduto>();
        }

        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 100,InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Descrição", 300, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string Descricao { get; set; }
        [InformacaoExibicao(3, "Data", 100, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Data { get; set; }
		public virtual Filial Filial { get; set; }
        public virtual IList<InventarioProduto> ListaProdutos { get; set; }
        [InformacaoExibicao(4, "Empresa", 250, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual String FilialNome { get { return Filial != null ? Filial.Nome : ""; } }
        public virtual int CodigoProprietario { get; set; }
    }
}
