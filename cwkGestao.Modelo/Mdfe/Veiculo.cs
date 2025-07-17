namespace cwkGestao.Modelo
{
    public class Veiculo : ModeloBase
    {
        public override int ID { get; set; }

        public virtual string Placa { get; set; }
        public virtual string Modelo { get; set; }
        public virtual string Renavam { get; set; }
        public virtual decimal TaraEmKg { get; set; }
        public virtual decimal CapacidadeEmKg { get; set; }
        public virtual decimal CapacidadeEmM3 { get; set; }
        public virtual int AnoFabricacao { get; set; }
        public virtual int AnoModelo { get; set; }
        public virtual int TipoPropriedade { get; set; }
        public virtual int TipoVeiculo { get; set; }
        public virtual int TipoRodado { get; set; }
        public virtual int TipoCarroceria { get; set; }
        public virtual int Ativo { get; set; }

        public virtual UF Uf { get; set; }
        public virtual string Marca { get; set; }
    }
}
