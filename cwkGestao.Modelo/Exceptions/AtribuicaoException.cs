using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class AtribuicaoException : Exception
    {
        
        public Object Origem { get; set; }
        public Object Valor { get; set; }
        public Object Destino{ get;set; }
        public Exception Excecao { get; set; }

        public AtribuicaoException()
        { }

        public AtribuicaoException(Exception inner)
        {
            Excecao = inner;
        }
        
    }
}
