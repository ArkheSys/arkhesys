namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SERVICOOS")]
    public partial class ServicoOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDSERVICOOS { get; set; }

        [StringLength(20)]
        public string PRO_CODIGO { get; set; }

        public int IDORDEMSERVICO { get; set; }

        [StringLength(50)]
        public string DESCRICAO { get; set; }

        public double? TOTAL { get; set; }

        public DateTime? INICIO { get; set; }

        public DateTime? FIM { get; set; }

        public int? TECNICO { get; set; }

        public int? TIPO { get; set; }

        public double? VALOR { get; set; }

        public double? CUSTO { get; set; }

        [StringLength(1)]
        public string FORMACOBRANCA { get; set; }

        public int? IDEQUIPOS { get; set; }

        public double? QTDE { get; set; }

        public double? ALIQINSS { get; set; }

        public double? VLRINSS { get; set; }

        public double? ALIQIRRF { get; set; }

        public double? VLRIRRF { get; set; }

        public double? DESCONTORATEADO { get; set; }

        public virtual OrdemServico ORDEMSERVICO { get; set; }
    }
}
