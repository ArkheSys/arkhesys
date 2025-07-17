namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPAMENTOOS")]
    public partial class EquipamentosOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipamentosOS()
        {
            PRODUTOOS = new HashSet<ProdutosOS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDEQUIPOS { get; set; }

        public int? IDEQUIPAMENTO { get; set; }

        public int? IDORDEMSERVICO { get; set; }

        [StringLength(1000)]
        public string DEFEITO { get; set; }

        public DateTime? DATA_COMPRA { get; set; }

        [StringLength(150)]
        public string REVENDA { get; set; }

        [StringLength(50)]
        public string NUM_NF { get; set; }

        [StringLength(150)]
        public string NUM_CERTGAR { get; set; }

        public int? OS_GARANTIDOR { get; set; }

        [StringLength(50)]
        public string OS_GARANTIDOR_SN { get; set; }

        public int? OS_NF_REMESSA { get; set; }

        public double? OS_NF_VALOR { get; set; }

        public int? OF_NF_EMIT { get; set; }

        public int? OS_OUTROS { get; set; }

        [StringLength(1000)]
        public string LAUDO { get; set; }

        public int? OS_OUTROS_EMIT { get; set; }

        public DateTime? DTENTRADA { get; set; }

        public DateTime? DTPRONTO { get; set; }

        public DateTime? DTENTREGA { get; set; }

        [StringLength(2000)]
        public string OBS { get; set; }

        [StringLength(2000)]
        public string ACESSORIO { get; set; }

        public virtual Equipamento EQUIPAMENTO { get; set; }

        public virtual OrdemServico ORDEMSERVICO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual HashSet<ProdutosOS> PRODUTOOS { get; set; }
    }
}
