using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ControleQualidade : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime Hora { get; set; }
        public virtual int Bloco { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int IDProduto2 { get; set; }
        public virtual decimal QtdLaminas { get; set; }
        public virtual Pessoa Responsavel { get; set; }
        public virtual decimal QtdChapasProd { get; set; }
        public virtual IList<CQEquipamento> CQEquipamentos { get; set; }

        public virtual IList<CQFuncionario> CQFuncionarios { get; set; }

        public virtual IList<CQProduto> CQProdutos { get; set; }

    }
}
