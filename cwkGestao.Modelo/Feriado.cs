using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Feriado")]
	public class Feriado : ModeloBase
	{
        public Feriado()
        {
            Data = DateTime.Now.Date;
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 150)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private DateTime _Data;
        [InformacaoExibicao(3, "Data", 150)]
        public virtual DateTime Data { get { return _Data; } set { _Data = value; } }

        private string _Descricao;
        [InformacaoExibicao(4, "Descrição", 493)]
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

	}
}
		