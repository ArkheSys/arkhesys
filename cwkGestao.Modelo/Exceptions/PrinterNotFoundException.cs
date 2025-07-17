using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class PrinterNotFoundException : Exception
    {
        public PrinterNotFoundException(string message) : base(message)
        {
        }
    }
}
