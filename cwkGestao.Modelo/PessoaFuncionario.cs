using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
	public class PessoaFuncionario : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _IDPessoa;
        public virtual int IDPessoa { get { return _IDPessoa; } set { _IDPessoa = value; } }

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        private string _NumCarteiraTrab;
        public virtual string NumCarteiraTrab { get { return _NumCarteiraTrab; } set { _NumCarteiraTrab = value; } }

        private Cw_usuario _CwUsuario;
        public virtual Cw_usuario CwUsuario { get { return _CwUsuario; } set { _CwUsuario = value; } }
	}
}
		