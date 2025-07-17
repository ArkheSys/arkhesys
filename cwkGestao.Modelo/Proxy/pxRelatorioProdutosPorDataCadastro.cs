using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioProdutosPorDataCadastro
    {
        public int IDProduto { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }

        public pxRelatorioProdutosPorDataCadastro() {}

        public pxRelatorioProdutosPorDataCadastro(int IDProduto, string Codigo, string Nome, DateTime? DataCadastro)
        {
            this.IDProduto = IDProduto;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.DataCadastro = DataCadastro;
        }
    }
}
