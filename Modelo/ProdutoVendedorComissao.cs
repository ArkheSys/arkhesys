using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class ProdutoVendedorComissao 
    {
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ProdutoVendedorComissao))
                return false;
            ProdutoVendedorComissao other = (ProdutoVendedorComissao)obj;
            if (this.GetHashCode() != other.GetHashCode() 
                || this.ID != other.ID
                || this.IDVendedor != other.IDVendedor 
                || this.IDProduto != other.IDProduto)
                return false;
            
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        

       
    }
}
