namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDEMSERVICO")]
    public partial class OrdemServico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdemServico()
        {
            EQUIPAMENTOOS = new HashSet<EquipamentosOS>();
            PRODUTOOS = new HashSet<ProdutosOS>();
            SERVICOOS = new HashSet<ServicoOS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDORDEMSERVICO { get; set; }

        public int IDSITUACAO { get; set; }

        //public string VEN_CODIGO { get; set; }

        public int? CLI_CODIGO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ENTRADA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GARANTIA { get; set; }

        public double? V_MAO { get; set; }

        public double? V_PECAS { get; set; }

        public double? V_DESLOCA { get; set; }

        public double? V_TERCEIRO { get; set; }

        public double? V_OUTROS { get; set; }

        public double? V_SEGURO { get; set; }

        public double? V_FRETE { get; set; }

        public int? OPERADOR { get; set; }

        public double? DESCONTO { get; set; }

        public TimeSpan? HORACADASTRO { get; set; }

        [StringLength(30)]
        public string FECHADAPORUSUARIO { get; set; }

        public DateTime? DATAHORAENCERRAMENTO { get; set; }

        [StringLength(6)]
        public string TOTALHORAREALIZADO { get; set; }

        [StringLength(6)]
        public string TOTALHORAPREVISAO { get; set; }

        public int? IDTABPRECO { get; set; }

        public double? TOTALOS { get; set; }

        public double? SUBTOTALSERV { get; set; }

        public double? DESCONTOSERV { get; set; }

        [StringLength(20)]
        public string CANCELADOPOR { get; set; }

        public DateTime? DATAHORACANCELAMENTO { get; set; }

        public DateTime? DATAOPERACAO { get; set; }

        [StringLength(2000)]
        public string OBS1 { get; set; }

        [StringLength(2000)]
        public string OBS2 { get; set; }

        public int IDLOJA { get; set; }

        public DateTime? PRONTO { get; set; }

        public DateTime? SAIDA { get; set; }

        public virtual Clientes CLIENTES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipamentosOS> EQUIPAMENTOOS { get; set; }

        public virtual Loja LOJA { get; set; }

        public virtual Situacao SITUACAO { get; set; }

        //public virtual Vendedor VENDEDOR { get; set; }

        public EquipamentosOS EQUIPAMENTO
        {
            get
            {
                foreach (var item in EQUIPAMENTOOS)
                {
                    return item;
                }

                return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutosOS> PRODUTOOS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicoOS> SERVICOOS { get; set; }

    }
}
