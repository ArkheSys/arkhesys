using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Grupo Cliente")]
    public class Tel_GrupoCliente : ModeloBase
	{
        public Tel_GrupoCliente()
        {
            Precos = new List<Tel_GrupoClientePreco>();
            Telefones = new List<Tel_GrupoClienteTelefone>();
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Nome", 677)]
        public virtual string Nome { get; set; }        
        public virtual string Endereco { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual string CEP { get; set; }
        public virtual string Contato { get; set; }
        public virtual string Telefone { get; set; }
        public virtual IList<Tel_GrupoClientePreco> Precos { get; set; }
        public virtual IList<Tel_GrupoClienteTelefone> Telefones { get; set; }
        public virtual Acrescimo Acrescimo { get; set; }
    }
}
		