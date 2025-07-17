using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Acompanhamento de OS")]
    public class OSAcompanhamento : ModeloBase
    {
        public override int ID { get; set; }
        public virtual OSOrdemServico OSOrdemServico { get; set; }
        public virtual DateTime DataHora { get; set; }
        public virtual string Ocorrencia { get; set; }
        public virtual string Usuario { get; set; }
        public virtual bool EhAutomatica { get; set; }
    }
}
