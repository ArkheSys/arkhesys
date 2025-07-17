using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Status Ordem de Serviço")]
	public class StatusOrdemServico : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Descricao;
        [InformacaoExibicao(1, "Descrição", 100)]
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }
	}
}
		