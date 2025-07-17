namespace cwkConsultaOS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUTOOS")]
    public partial class ProdutosOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPRODUTOOS { get; set; }

        [StringLength(20)]
        public string PRO_CODIGO { get; set; }

        [StringLength(50)]
        public string DESCRICAO { get; set; }

        public double? VALOR { get; set; }

        public int? TECNICO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DIA { get; set; }

        public double? VLRVENDA { get; set; }

        public int IDORDEMSERVICO { get; set; }

        public double? CUSTO { get; set; }

        public double? QTD { get; set; }

        public int? IDEQUIPOS { get; set; }

        public int? IDSERVICOOS { get; set; }

        [StringLength(100)]
        public string INFPROD { get; set; }

        [StringLength(50)]
        public string USUARIO { get; set; }

        public double? ESTOQUEEMMOVIMENTO { get; set; }

        public double? VALORDESCONTORATEADO { get; set; }

        public double? VPROD { get; set; }

        public double? VALORITEM { get; set; }

        public virtual EquipamentosOS EQUIPAMENTOOS { get; set; }

        public virtual OrdemServico ORDEMSERVICO { get; set; }
    }
}
