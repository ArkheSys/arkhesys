using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class DescontoException : Exception
    {
        public DescontoException()
            : base()
        { }

        public DescontoException(string message) : base(message) { }

        public DescontoException(string message, DescontoException innerException) : base(message, innerException) {}

    }
}
