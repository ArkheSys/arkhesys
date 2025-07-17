using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Atendimento")]
#pragma warning disable CS0659 // "Atendimento" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public class Atendimento : ModeloBase
#pragma warning restore CS0659 // "Atendimento" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
	{
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Data de Atendimento", 120)]
		public virtual DateTime DtAtendimento { get; set; }
        [InformacaoExibicao(2, "Entrada 01", 100)]
        public virtual TimeSpan Entrada_1 { get; set; }
        [InformacaoExibicao(3, "Saída 01", 100)]
		public virtual TimeSpan Saida_1 { get; set; }
        [InformacaoExibicao(4, "Entrada 02", 100)]
		public virtual TimeSpan Entrada_2 { get; set; }
        [InformacaoExibicao(5, "Saída 02", 100)]
		public virtual TimeSpan Saida_2 { get; set; }
        [InformacaoExibicao(6, "Descrição", 300)]
		public virtual string Descricao { get; set; }
        [InformacaoExibicao(7, "Analista", 100)]
		public virtual Cw_usuario Analista { get; set; }
        private TimeSpan _TotalHoras;
        public virtual TimeSpan TotalHoras 
        {
            get
            {
                //return CalculaHorario();
                return _TotalHoras;
            }
            set
            {
                _TotalHoras = value;
            }
        }

        private TimeSpan CalculaHorario()
        {
            TimeSpan horario01 = new TimeSpan();
            TimeSpan horario02 = new TimeSpan();
            TimeSpan e1 = new TimeSpan();
            TimeSpan e2 = new TimeSpan();
            TimeSpan s1 = new TimeSpan();
            TimeSpan s2 = new TimeSpan();
            if (Entrada_1 > Saida_1)
            {
                e1 = Entrada_1 - new TimeSpan(12, 00, 00);
                s1 = Saida_1 + new TimeSpan(12, 00, 00);
                horario01 = (s1.Duration() - e1.Duration());
            }
            else
            {
                horario01 = Saida_1 - Entrada_1;
            }

            if (Entrada_2 > Saida_2)
            {
                e2 = Entrada_2 - new TimeSpan(12, 00, 00);
                s2 = Saida_2 + new TimeSpan(12, 00, 00);
                horario02 = (s2.Duration() - e2.Duration());
            }
            else
            {
                horario02 = Saida_2 - Entrada_2;
            }

            return (horario01 + horario02);
        }

        public virtual TimeSpan TotalHorasFat { get; set; }
           
        [InformacaoExibicao(9, "Total Horas Fat", 100)]
        public virtual string TotHorasFat
        {
            get
            {
                string totStr = String.Format("{0:00}", (TotalHorasFat.Days * 24) + TotalHorasFat.Hours) + ":" + String.Format("{0:00}", TotalHorasFat.Minutes);
                return totStr;
            }
            set
            {
                TimeSpan retorno;
                string dtStr = value == null ? "00:00" : value.Substring(11,5);
                if (TimeSpan.TryParse(dtStr, out retorno))
                {
                    TotalHorasFat = retorno;
                }
            }
        }

        [InformacaoExibicao(8, "Total Horas", 100)]
        public virtual string TotHoras
        {
            get
            {
                string totStr = String.Format("{0:00}", (TotalHoras.Days * 24) + TotalHoras.Hours) + ":" + String.Format("{0:00}", TotalHoras.Minutes);
                return totStr;
            }
            set
            {
            }
        }

        [InformacaoExibicao(10, "Cliente", 120)]
		public virtual Pessoa Cliente { get; set; }
        [InformacaoExibicao(11, "Data de Conferência", 120)]		
        public virtual DateTime? DtConferencia { get; set; }
        [InformacaoExibicao(12, "Faturar?", 100)]
        public virtual bool Faturar { get; set; }
        [InformacaoExibicao(13, "Observações Analista", 200)]
        public virtual string Observacoes { get; set; }
        [InformacaoExibicao(14, "Status", 100)]		
        public virtual StatusAtendimento Status { get; set; }
		public virtual Nota Nota { get; set; }
        public virtual FaturamentoAtendimento FatAtendimento { get; set; }
        [InformacaoExibicao(15, "Número da Nota", 100)]
        public virtual int? NumeroNota
        {
            get
            {
                if (this.Nota != null)
                {
                    return Nota.Numero;
                }
                else
                {
                    return null;
                }
            }
            set
            {
            }
        }

        #region Adapters de data
        
        public virtual DateTime Ent1
        {
            get
            {
                return DateTime.Now.Date.AddHours(Entrada_1.Hours).AddMinutes(Entrada_1.Minutes).AddSeconds(Entrada_1.Seconds);
            }
            set
            {
                Entrada_1 = RegraHorasPersistenciaEntrada(value);
            }
        }

        public virtual DateTime Ent2
        {
            get
            {
                return DateTime.Now.Date.AddHours(Entrada_2.Hours).AddMinutes(Entrada_2.Minutes).AddSeconds(Entrada_2.Seconds);
            }
            set
            {
                Entrada_2 = RegraHorasPersistenciaEntrada(value);
            }
        }

        public virtual DateTime Sai1
        {
            get
            {
                return DateTime.Now.Date.AddHours(Saida_1.Hours).AddMinutes(Saida_1.Minutes).AddSeconds(Saida_1.Seconds);
            }
            set
            {
                Saida_1 = RegraHorasPersistenciaSaida(value);
            }
        }

        public virtual DateTime Sai2
        {
            get
            {
                return DateTime.Now.Date.AddHours(Saida_2.Hours).AddMinutes(Saida_2.Minutes).AddSeconds(Saida_2.Seconds);
            }
            set
            {
                Saida_2 = RegraHorasPersistenciaSaida(value);
            }
        }

        #endregion

        public Atendimento()
        {
            DtAtendimento = DateTime.Now.Date;
        }

        #region Regras

        private TimeSpan RegraHorasPersistenciaSaida(DateTime value)
        {
            int horas = value.Hour;
            int minutos = value.Minute;
            int segundos = value.Second;

            if (minutos > 45 && minutos <= 59)
            {
                minutos = 0;
                horas++;
            }
            if (minutos > 30 && minutos <= 45)
            {
                minutos = 45;
            }
            if (minutos > 15 && minutos <= 30)
            {
                minutos = 30;
            }
            if (minutos > 0 && minutos <= 15)
            {
                minutos = 15;
            }
            return new TimeSpan(horas, minutos, segundos);
        }

        private TimeSpan RegraHorasPersistenciaEntrada(DateTime value)
        {
            int horas = value.Hour;
            int minutos = value.Minute;
            int segundos = value.Second;
            if (minutos >= 0 && minutos < 15)
            {
                minutos = 0;
            }
            if (minutos >= 15 && minutos < 30)
            {
                minutos = 15;
            }
            if (minutos >= 30 && minutos < 45)
            {
                minutos = 30;
            }
            if (minutos >= 45 && minutos <= 59)
            {
                minutos = 45;
            }
            return new TimeSpan(horas, minutos, segundos);
        }
        #endregion

        public override bool Equals(object obj)
        {
            Atendimento other = new Atendimento();
            try
            {
                other = (Atendimento)obj;
            }
            catch (Exception)
            {
                return false;
            }
            if (this.ID == other.ID &&
                this.DtAtendimento == other.DtAtendimento &&
                this.Entrada_1 == other.Entrada_1 &&
                this.Entrada_2 == other.Entrada_2 &&
                this.Saida_1 == other.Saida_1 &&
                this.Saida_2 == other.Saida_2 &&
                this.Descricao == other.Descricao &&
                this.Analista == other.Analista &&
                this.TotalHoras == other.TotalHoras &&
                this.Cliente == other.Cliente &&
                this.DtConferencia == other.DtConferencia &&
                this.Faturar == other.Faturar &&
                this.Observacoes == other.Observacoes &&
                this.Status == other.Status &&
                this.FatAtendimento == other.FatAtendimento &&
                this.Nota == other.Nota)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
