using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{   
    [InformacaoExibicao("Agendamento de Máquina")]
    public class Ofd_AgendamentoMaquina : ModeloBase
	{
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Data", 75)]
		public virtual DateTime Data { get; set; }
        
        [InformacaoExibicao(4, "Produtividade", 125)]
		public virtual decimal Produtividade { get; set; }
        
        [InformacaoExibicao(3, "Máquina", 100)]
        public virtual Ofd_Maquinas Ofd_Maquina { get; set; }
        
        [InformacaoExibicao(2, "Obra", 250)]
		public virtual Ofd_Obra Ofd_Obra { get; set; }
        
        [InformacaoExibicao(5, "Operador 1", 100)]
		public virtual Pessoa Operador1 { get; set; }
        
        [InformacaoExibicao(6, "Operador 2", 100)]
		public virtual Pessoa Operador2 { get; set; }
        
        [InformacaoExibicao(7, "Observação", 250)]
		public virtual string Observacao { get; set; }

        [InformacaoExibicao(8, "Hora Entrada", 150)]
        public virtual DateTime? HoraEntrada { get; set; }

        [InformacaoExibicao(9, "Hora Saída", 150)]
        public virtual DateTime? HoraSaida { get; set; }

        public virtual IList<Ofd_AgendamentoMaquinaOcorrencia> Ocorrencias { get; set; }

        public virtual string Informacao
        {
            get
            {
                string ret = "";
                TimeSpan ts = new TimeSpan();
                ret += this.Ofd_Obra == null ? "Patio/Manutenção" + Environment.NewLine : "Obra: " + this.Ofd_Obra.Descricao + Environment.NewLine;
                ret += this.Ofd_Obra == null ? "" : "Responsável: " + this.Ofd_Obra.ResponsavelNaObra + Environment.NewLine;
                ret += "Produtividade: " + this.Produtividade.ToString() + Environment.NewLine;

                if (HoraEntrada.HasValue)
                {
                    ret += "Entrada: " + HoraEntrada.Value.ToShortTimeString() + " ";
                }
                if (HoraSaida.HasValue)
                {
                    ret += "Saida: " + HoraSaida.Value.ToShortTimeString();
                }

                if (HoraEntrada.HasValue && HoraSaida.HasValue)
                {
                    ret += Environment.NewLine;
                }

                ret += String.IsNullOrEmpty(this.Observacao) ? "" : "Observação: " + this.Observacao.ToString();

                if (Ocorrencias != null)
                {
                    if (Ocorrencias.Count > 0)
                    {
                        foreach (var item in Ocorrencias)
                        {
                            ts = ts.Add(new TimeSpan(item.TempoOcorrencia.Hour, item.TempoOcorrencia.Minute, item.TempoOcorrencia.Second));
                        }
                        ret += "Tempo de Ocorrências: " + String.Format("{0:00}", (ts.Days * 24) + ts.Hours) + ":" + String.Format("{0:00}", ts.Minutes);
                    }
                }
                
                return ret;
            }
            set
            {

            }
        }

    }
}
