using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo de Equipamento")]
    public class OSTipoEquipamento : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Descrição", 300)]
        public virtual string Descricao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
