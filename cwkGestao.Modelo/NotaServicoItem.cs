using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Nota de Serviço Item")]
    public class NotaServicoItem : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private NotaServico _NotaServico;
        public virtual NotaServico NotaServico { get { return _NotaServico; } set { _NotaServico = value; } }

        private int _Sequencia;
        [InformacaoExibicao(1, "Sequência", 50)]
        public virtual int Sequencia { get { return _Sequencia; } set { _Sequencia = value; } }

        private Servico _Servico;
        public virtual Servico Servico { get { return _Servico; } set { _Servico = value; } }

        private string _ServicoDescricao;
        [InformacaoExibicao(2, "Descrição", 200)]
        public virtual string ServicoDescricao { get { return _ServicoDescricao; } set { _ServicoDescricao = value; } }

        private decimal _Quantidade;
        public virtual decimal Quantidade { get { return _Quantidade; } set { _Quantidade = value; } }

        public virtual decimal ValorUnitario { get; set; }

        public virtual decimal Total { get; set; }
    }
}
