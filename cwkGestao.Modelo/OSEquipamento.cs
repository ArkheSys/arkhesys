using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Equipamento")]
    public class OSEquipamento : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Descrição", 400)]
        public virtual string Descricao { get; set; }
        [InformacaoExibicao(3, "Número de Série", 100)]
        public virtual string NumeroSerie { get; set; }
        [InformacaoExibicao(4, "Tipo de Equipamento", 120)]
        public virtual OSTipoEquipamento OSTipoEquipamento { get; set; }

        public virtual string Marca { get; set; }
        public virtual string Modelo { get; set; }

        public override string ToString()
        {
            return this.Descricao + "[" + NumeroSerie + "]";
        }

    }
}
