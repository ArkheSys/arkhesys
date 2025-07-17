using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Grupo Cliente Telefone")]
    public class Tel_GrupoClienteTelefone : ModeloBase
	{
        public Tel_GrupoClienteTelefone() { }
        [InformacaoExibicao(0, "Código", 100)]
        public override int ID { get; set; }
        public virtual Tel_GrupoCliente GrupoCliente { get; set; }
        [InformacaoExibicao(1, "Telefone", 682)]
        public virtual string Telefone { get; set; }

        public override string ToString()
        {
            return Telefone;
        }
    }
}
		