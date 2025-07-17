using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
	public class Empresa : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private IList<Filial> _Filials;
        public virtual IList<Filial> Filials { get { return _Filials; } set { _Filials = value; } }
	}
}
		