using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Socio : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string Inscricao { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Bairro { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Fax { get; set; }
        public virtual IList<PessoaSocio> PessoaSocios { get; set; }

    }
}
