namespace cwkConsultaOS
{
    using cwkConsultaOS;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VENDEDOR")]
    public partial class Vendedor
    {
        [Key]
        [StringLength(4)]
        public string VEN_CODIGO { get; set; }

        [StringLength(40)]
        public string VEN_NOME { get; set; }

        [StringLength(40)]
        public string APELIDO { get; set; }

        [StringLength(20)]
        public string USUARIO { get; set; }

    }
}
