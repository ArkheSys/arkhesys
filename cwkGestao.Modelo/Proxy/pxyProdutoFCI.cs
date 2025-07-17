using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyProdutoFCI
    {
        public pxyProdutoFCI() { }

        public pxyProdutoFCI(int id, string fci, DateTime data, int idProduto, bool ativo)
        {
            ID = id;
            FCI = fci;
            Data = data;
            IDProduto = idProduto;
            bAtivo = ativo;
        }

        public virtual int ID { get; set; }
        public virtual string FCI { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual int IDProduto { get; set; }
        public virtual bool bAtivo { get; set; }
    }
}
