using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo60
    {
#pragma warning disable CS0414 // O campo "Tipo60._tipo" é atribuído, mas seu valor nunca é usado
        private string _tipo;
#pragma warning restore CS0414 // O campo "Tipo60._tipo" é atribuído, mas seu valor nunca é usado
        private string _Registro;

        public Tipo60()
        {
            this._tipo = "60";
        }

        public string Registro
        {
            get { return _Registro; }
            set { _Registro = value; }
        }
    }
}
