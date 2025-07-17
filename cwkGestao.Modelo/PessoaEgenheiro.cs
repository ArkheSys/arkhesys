using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Engenheiro")]
    public class PessoaEngenheiro : ModeloBase
    {
        public PessoaEngenheiro()
        {
            Pessoa = new Pessoa { TipoPessoa = "Física" };
        }

        public override int ID { get; set; }
        [InformacaoExibicao(0, "Nome", 400)]
        public virtual Pessoa Pessoa { get; set; }
        [InformacaoExibicao(2, "CREA", 100)]
        public virtual string CREA { get; set; }

        [InformacaoExibicao(1, "CPF", 150)]
        public virtual string CPF_CNPJ { get { return Pessoa != null ? Pessoa.CNPJ_CPF : String.Empty; } }
    }
}
