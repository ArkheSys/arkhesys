using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyProdutoVendedorComissao 
    {
        public int ID { get; set; }
        public int IDProduto { get; set; }
        public int IDVendedor { get; set; }
        public String Descricao { get; set; }
        public String Vendedor { get; set; }
        public Decimal Comissao { get; set; }
        public int TipoComissao { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(pxyProdutoVendedorComissao)) return false;
            pxyProdutoVendedorComissao other = (pxyProdutoVendedorComissao)obj;
            
            return other.ID.Equals(this.ID);

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
