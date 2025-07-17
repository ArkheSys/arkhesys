namespace cwkGestao.Modelo
{
    public class OrdemServicoMaquina : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int IdOrdemServico { get; set; }
        public virtual int? IdMaquina { get; set; }

        public virtual string Maquina { get; set; }
        public virtual string Fabricante { get; set; }

        public virtual bool Novo { get; set; }
        public virtual bool Editado { get; set; }
        public virtual bool Removido { get; set; }

        public OrdemServicoMaquina() { }
    }
}