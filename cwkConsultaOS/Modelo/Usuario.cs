namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UCTABUSERS")]
    public partial class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UCIDUSER { get; set; }

        [StringLength(30)]
        public string UCUSERNAME { get; set; }

        [StringLength(30)]
        public string UCLOGIN { get; set; }

        [StringLength(30)]
        public string UCPASSWORD { get; set; }

        [StringLength(150)]
        public string UCEMAIL { get; set; }

        public int? UCPRIVILEGED { get; set; }

        [StringLength(1)]
        public string UCTYPEREC { get; set; }

        public int? UCPROFILE { get; set; }

        [StringLength(255)]
        public string UCKEY { get; set; }
    }
}
