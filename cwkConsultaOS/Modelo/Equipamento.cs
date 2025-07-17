namespace cwkConsultaOS
{
    using cwkConsultaOS;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPAMENTO")]
    public partial class Equipamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipamento()
        {
            EQUIPAMENTOOS = new HashSet<EquipamentosOS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDEQUIPAMENTO { get; set; }

        [StringLength(80)]
        public string DESCRICAO { get; set; }

        [StringLength(80)]
        public string MARCA { get; set; }

        [StringLength(80)]
        public string MODELO { get; set; }

        [StringLength(80)]
        public string SERIE { get; set; }

        [StringLength(80)]
        public string PATRIMONIIO { get; set; }

        public DateTime? DATA_COMPRA { get; set; }

        [StringLength(50)]
        public string REVENDA { get; set; }

        [StringLength(50)]
        public string NUM_NF { get; set; }

        [StringLength(50)]
        public string NUM_CERTGAR { get; set; }

        [StringLength(1)]
        public string CAMPO_BOOL { get; set; }

        public double? CAMPO_DOUBLE { get; set; }

        [StringLength(20)]
        public string IPEM { get; set; }

        [StringLength(10)]
        public string VOLTAGEM { get; set; }

        [StringLength(80)]
        public string INMETRO { get; set; }

        [StringLength(80)]
        public string ETIQUETA_REPARO { get; set; }

        [StringLength(80)]
        public string CAP_MAXIMA { get; set; }

        [StringLength(30)]
        public string SITUACAO_EQUIP { get; set; }

        [StringLength(1)]
        public string ATIVO { get; set; }

        public DateTime? DTCADASTRO { get; set; }

        [StringLength(2000)]
        public string OBSERVACOES { get; set; }

        [StringLength(2000)]
        public string ACESSORIO { get; set; }

        public int? CLI_CODIGO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual HashSet<EquipamentosOS> EQUIPAMENTOOS { get; set; }
    }
}
