using System;

namespace cwkGestao.Modelo
{
    public class MovimentoFiscalMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Mdfe Mdfe { get; set; }
        public virtual int Serie { get; set; }
        public virtual string XmlEnvio { get; set; }
        public virtual string XmlRetorno { get; set; }
        public virtual int Emitida { get; set; }
        public virtual int Cancelada { get; set; }
        public virtual DateTime? DataEmissao { get; set; }
        public virtual string Chave { get; set; }
        public virtual int Cstat { get; set; }
        public virtual string Motivo { get; set; }
        public virtual DateTime? Recebimento { get; set; }
        public virtual int Ambiente { get; set; }
        public virtual int Numero { get; set; }
        public virtual int TipoDocumento { get; set; }
        public virtual int Encerrada { get; set; }
    }
}
