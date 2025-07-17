using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Exceptions
{
    public class FormException : Exception
    {
        private IList<string> camposErrados;

        public FormException()
        {
            camposErrados = new List<string>();
        }

        public IList<string> CamposErrados
        {
            get { return camposErrados; }
        }
    }
}
