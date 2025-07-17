using System;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Serviço Responsável")]
    public class ServicoResponsavel : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int? Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 350)]
        public virtual string Descricao { get; set; }
    }
}