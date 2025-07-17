using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class EstoqueNegativoException : Exception
    {
        private string produtos;

        public EstoqueNegativoException(string produtosEstoqueNegativo)
        {
            produtos = produtosEstoqueNegativo;
        }

        public override string Message
        {
            get
            {
                return "Os seguintes produtos estão com o estoque negativo:"
                       + Environment.NewLine
                       + produtos;
            }
        }
    }
}
