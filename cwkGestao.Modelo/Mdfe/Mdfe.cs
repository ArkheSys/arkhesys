using System;

namespace cwkGestao.Modelo
{
    public class Mdfe : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int TipoAmbiente { get; set; }
        public virtual int TipoEmitente { get; set; }
        public virtual int TipoTransportador { get; set; }
        public virtual int Modelo { get; set; }
        public virtual int Serie { get; set; }
        public virtual int NMDF { get; set; }
        public virtual int CMDF { get; set; }
        public virtual int ModalidadeTransporte { get; set; }
        public virtual DateTime? Emissao { get; set; }
        public virtual int TipoEmissao { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual string InformacoesAdicionais { get; set; }
        public virtual string InformacoesComplementares { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual UF UFDescarregamento { get; set; }
        public virtual Cidade CidadeDescarga { get; set; }
        public virtual int CodigoUnPesoCarga { get; set; }
        public virtual decimal PesoBrutoTotal { get; set; }

    }
}
