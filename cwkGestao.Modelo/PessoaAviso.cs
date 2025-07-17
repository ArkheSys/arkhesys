using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao(0, "Aviso", 100)]
	public class PessoaAviso : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _IDPessoa;
        public virtual int IDPessoa { get { return _IDPessoa; } set { _IDPessoa = value; } }

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        private int _Sequencia;
        public virtual int Sequencia { get { return _Sequencia; } set { _Sequencia = value; } }

        private DateTime _DtCadastro;
        public virtual DateTime DtCadastro { get { return _DtCadastro; } set { _DtCadastro = value; } }

        private DateTime _DtValidade;
        [InformacaoExibicao(0, "Validade", 50)]
        public virtual DateTime DtValidade { get { return _DtValidade; } set { _DtValidade = value; } }

        private string _Ordem;
        public virtual string Ordem { get { return _Ordem; } set { _Ordem = value; } }

        private string _Aviso;
        [InformacaoExibicao(0, "Aviso", 300)]
        public virtual string Aviso { get { return _Aviso; } set { _Aviso = value; } }
	}
}
		