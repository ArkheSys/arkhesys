using System;

namespace cwkGestao.Modelo
{
    public class EventoMdfe : ModeloBase
    {
        public override int ID { get; set; }
        public virtual MovimentoFiscalMdfe MovimentoFiscalMdfe { get; set; }
        public virtual int NseqEvento { get; set; }
        public virtual DateTime DhEvento { get; set; }
        public virtual string DescEvento { get; set; }
        public virtual string NProt { get; set; }
        public virtual string XMotivo { get; set; }
        public virtual int Cstat { get; set; }
        public virtual int TipoEvento { get; set; }
    }
}
