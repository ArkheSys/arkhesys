using System;

namespace cwkGestao.Modelo.Cte
{
    public class CteIdentificacao : ModeloBase
    {
        public override int ID { get; set; }

        public virtual int CCTE { get; set; }
        public virtual int CFOP { get; set; }
        public virtual string NaturezaOperacao { get; set; }
        public virtual int FormaPagamento { get; set; }
        public virtual int Serie { get; set; }
        public virtual int NumeroCte { get; set; }
        public virtual DateTime DataEmissao { get; set; }
        public virtual int TipoAmbiente { get;set; }
        public virtual int Modal { get; set; }
        public virtual int TipoServico { get; set; }

        public virtual Cidade CidadeSaida { get; set; }
        public virtual Cidade CidadeChegada { get; set; }
        public virtual int Retira { get; set; }
        public virtual int ContribuinteICMS { get; set; }
        public virtual int Tomador { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Pessoa Remetente { get;set; }
        public virtual Pessoa Destinatario { get; set; }
    }
}
