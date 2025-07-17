namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOJA")]
    public partial class Loja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loja()
        {
            ORDEMSERVICO = new HashSet<OrdemServico>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDLOJA { get; set; }

        [StringLength(14)]
        public string CNPJ { get; set; }

        [StringLength(60)]
        public string RAZAOSOCIAL { get; set; }

        [StringLength(60)]
        public string NOMEFANTASIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdemServico> ORDEMSERVICO { get; set; }
    }
}
