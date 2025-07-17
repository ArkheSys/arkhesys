using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Proposta Verificação")]
    public class Ofd_PropostaMedicao : ModeloBase
    {
        public Ofd_PropostaMedicao()
        {            
            Perfuracoes = new List<Ofd_PropostaMedicaoPerfuracao>();
            Ocorrencias = new List<Ofd_PropostaMedicaoOcorrencias>();
            InicioTrabalho = new DateTime(1802, 1, 1);
            TerminoTrabalho = new DateTime(1802, 1, 1);
            TempoTrabalho = 0;
            Produtividade = 0;
        }

        public override int ID { get; set; }
        public virtual Ofd_Proposta Proposta { get; set; }
        [InformacaoExibicao(0, "Proposta", 80)]
        public virtual string NomeProposta { get { return Proposta.NomeObra; } }
        public virtual int Sequencia { get; set; }
        [InformacaoExibicao(2, "Verificação", 90)]
        public virtual DateTime DataVerificacao { get; set; }
        [InformacaoExibicao(3, "Medição", 80)]
        public virtual int Medicao { get; set; }
        [InformacaoExibicao(1, "Faturada", 70)]
        public virtual bool MedicaoFaturada { get; set; }
        [InformacaoExibicao(4, "Relatório", 80)]
        public virtual int Relatorio { get; set; }
        [InformacaoExibicao(5, "Dt. Relatório", 100)]
        public virtual DateTime DataRelatorio { get; set; }
        public virtual Ofd_Maquinas Maquina { get; set; }        
        public virtual Pessoa Operador1 { get; set; }
        public virtual Pessoa Operador2 { get; set; }
        public virtual DateTime InicioTrabalho { get; set; }
        public virtual DateTime TerminoTrabalho { get; set; }
        public virtual DateTime? InicioHorarioAlmoco { get; set; }
        public virtual DateTime? TerminoHorarioAlmoco { get; set; }
        public virtual bool Chuva { get; set; }
        public virtual bool TerrenoMolhado { get; set; }
        public virtual bool Domingo { get; set; }
        public virtual bool Feriado { get; set; }
        public virtual bool ManutencaoConcerto { get; set; }
        public virtual IList<Ofd_PropostaMedicaoPerfuracao> Perfuracoes { get; set; }
        [InformacaoExibicao(6, "Tempo Trabalho", 100)]
        public virtual decimal TempoTrabalho { get; set; }
        [InformacaoExibicao(7, "Produtividade", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal2Casas)]
        public virtual decimal Produtividade { get; set; }
        public virtual Ofd_Medicao Ofd_Medicao { get; set; }
        public virtual IList<Ofd_PropostaMedicaoOcorrencias> Ocorrencias { get; set; }

        [InformacaoExibicao(8, "Máquina", 150)]
        public virtual string MaquinaStr { get { return Maquina != null ? Maquina.Descricao : String.Empty; } }

        [InformacaoExibicao(9, "Operador 1", 180)]
        public virtual string Operador1Str { get { return Operador1 != null ? Operador1.Nome : String.Empty; } }

        [InformacaoExibicao(10, "Operador 2", 180)]
        public virtual string Operador2Str { get { return Operador2 != null ? Operador2.Nome : String.Empty; } }

        public virtual void CalcularTempoTrabalhoEProdutividade()
        {
            CalcularTempoTrabalho();
            CalcularProdutividade();
        }

        public virtual void CalcularProdutividade()
        {
            if (TempoTrabalho > 0)
                Produtividade = Perfuracoes.Sum(p => p.Total) / TempoTrabalho;
        }

        protected virtual void CalcularTempoTrabalho()
        {
            TempoTrabalho = Convert.ToDecimal((TerminoTrabalho - InicioTrabalho).TotalHours);
            if (InicioHorarioAlmoco.HasValue && TerminoHorarioAlmoco.HasValue)
                TempoTrabalho -= Convert.ToDecimal((TerminoHorarioAlmoco.Value - InicioHorarioAlmoco.Value).TotalHours);
        }
    }
}
