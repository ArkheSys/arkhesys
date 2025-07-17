using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FaturamentoAtendimento : ModeloBase
	{

        public virtual bool Selecionado { get; set; }

        [InformacaoExibicao("Faturamento de Atendimentos")]
        public override int ID { get; set; }
        [InformacaoExibicao(1,"Cliente",250)]
		public virtual Pessoa Cliente { get; set; }
        [InformacaoExibicao(2, "Serviço", 200)]
		public virtual Servico Servico { get; set; }
        [InformacaoExibicao(3, "Quantidade", 125)]
		public virtual int Quantidade { get; set; }
        
        private TimeSpan _HorasAtendimento;
		public virtual TimeSpan HorasAtendimento {
            get
            {
                return _HorasAtendimento;
            }
            set
            {
                _HorasAtendimento = value;
            }
        }
        [InformacaoExibicao(4, "Horas de Atendimento", 150)]
        public virtual string HorasDeAtendimento
        {
            get
            {
                return String.Format("{0:00}", (HorasAtendimento.Days * 24) + HorasAtendimento.Hours) + ":" + String.Format("{0:00}", HorasAtendimento.Minutes);
            }
            set
            {
            }
        }

        private TimeSpan _HorasFaturamento;
		public virtual TimeSpan HorasFaturamento
        {
            get
            {
                return _HorasFaturamento;
            }
            set
            {
                _HorasFaturamento = value;
            }
        }
        [InformacaoExibicao(5, "Horas Faturamento", 150)]
        public virtual string HorasDeFaturamento
        {
            get
            {
                return String.Format("{0:00}", (HorasFaturamento.Days * 24) + HorasFaturamento.Hours) + ":" + String.Format("{0:00}", HorasFaturamento.Minutes);
            }
            set
            {
            }
        }

        private decimal _Valor;
        [InformacaoExibicao(6, "Valor", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal Valor
        {
            get
            {
                return _Valor;
            }
            set
            {
                _Valor = value;
            }
        }

        private decimal _Desconto;
        [InformacaoExibicao(7, "Desconto", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal Desconto
        {
            get
            {
                return _Desconto;
            }
            set
            {
                _Desconto = value;
            }
        }

        private decimal _DespesasAdicionais;
        [InformacaoExibicao(8, "Adicionais (Desp. Viagens e outros)", 250, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal DespesasAdicionais
        {
            get
            {
                return _DespesasAdicionais;
            }
            set
            {
                _DespesasAdicionais = value;
            }
        }

        private decimal _Total;
        [InformacaoExibicao(9, "Total", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal Total
        {
            get
            {
                if ((Valor - Desconto + DespesasAdicionais) <= decimal.Zero)
                {
                    return decimal.Zero;
                }
                return Valor - Desconto + DespesasAdicionais;
            }
            set
            {
                _Total = value;
            }
        }

        public virtual OSOrdemServico OrdemServico { get; set; }

        public virtual IList<Atendimento> AtendimentosVinculados { get; set; }
        public virtual string Observacao { get; set; }

    }
}
