using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Máquinas")]
    public class Ofd_Maquinas : ModeloBase
    {
        public Ofd_Maquinas()
        {
            Tipo = TipoOfd_MaquinasType.Helice;
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 300)]
        public virtual string Descricao { get; set; }

        [InformacaoExibicao(3, "Tipo", 90)]
        public virtual TipoOfd_MaquinasType Tipo { get; set; }

        [InformacaoExibicao(3, "Placa", 100)]
        public virtual string Placa { get; set; }

        public virtual string TipoServico { get; set; }

        [InformacaoExibicao(8, "Data de Inclusão", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public override DateTime? IncData { get; set; }

        [InformacaoExibicao(9, "Usuário de Inclusão", 120)]
        public override string IncUsuario { get; set; }

        [InformacaoExibicao(10, "Data da Alteração", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public override DateTime? AltData { get; set; }

        [InformacaoExibicao(11, "Usuário da Alteração", 120)]
        public override string AltUsuario { get; set; }



        public override string ToString()
        {
            return Descricao;
        }
    }
}
