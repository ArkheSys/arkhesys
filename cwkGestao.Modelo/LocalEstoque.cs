using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Local Estoque")] 
    public class LocalEstoque : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Filial Filial { get; set; }
        [InformacaoExibicao(0, "Código", 90)] 
		public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Descrição", 250)] 
		public virtual string Descricao { get; set; }

        [InformacaoExibicao(1, "Empresa", 200)]
        public virtual string FilialStr { get { return Filial != null ? Filial.Nome : String.Empty; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
