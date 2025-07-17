namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            ORDEMSERVICO = new HashSet<OrdemServico>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CLI_CODIGO { get; set; }

        [StringLength(11)]
        public string CPF { get; set; }

        [StringLength(11)]
        public string TELEFONE_1 { get; set; }

        [StringLength(11)]
        public string CELULAR_1 { get; set; }

        [StringLength(150)]
        public string CLI_NOME { get; set; }

        [StringLength(50)]
        public string CLI_EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdemServico> ORDEMSERVICO { get; set; }
    }
}
