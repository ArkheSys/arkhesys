using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo de Atendimento")]
	public class TipoAtendimento : ModeloBase
	{
        public override int ID { get;set;}

        [InformacaoExibicao(1, "Código", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get;set;}

        [InformacaoExibicao(2, "Descrição", 200)]
        public virtual string Descricao { get;set;}
	}
}
		