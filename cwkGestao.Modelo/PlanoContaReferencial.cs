using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class PlanoContaReferencial : ModeloBase
    {

        public override int ID {get; set;}
        [InformacaoExibicao(1, "Código", 70)]
        public virtual  int codigo {get; set;}
        [InformacaoExibicao(2, "Classificação", 70)]
        public virtual string classificacao { get; set; }
        [InformacaoExibicao(3, "Nome", 100)]
        public virtual string nome { get; set; }
        [InformacaoExibicao(4, "Orientação", 200)]
        public virtual string orientacoes { get; set; }
        [InformacaoExibicao(5, "Inicio da validade", 70)]
        public virtual DateTime? inicioValidade { get; set; }
        [InformacaoExibicao(6, "Fim da validade", 70)]
        public virtual DateTime? fimValidade { get; set; }
        [InformacaoExibicao(7, "Tipo", 50)]
        public virtual string tipo { get; set; }
    }
}
