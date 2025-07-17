using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cwkGestao.Modelo.Proxy
{
    [InformacaoExibicao("Fatura")]
    public class pxyTel_FaturaLigacao
    {
        public pxyTel_FaturaLigacao() { }

        public pxyTel_FaturaLigacao(int idFatura, int sequencia, int tempoSegundos, decimal valorMinimo,
            decimal total, string origem, string destino, DateTime dataHora, int tipoUso, string descricaoUso,
            string cidade, string uF, string resumoFatura, bool ligacaoLocal)
        {
            IDFatura = idFatura;
            SequenciaLig = sequencia;
            ValorMinuto = valorMinimo;
            TotalLig = total;
            Origem = FormatarTelefone(origem);
            Destino = FormatarTelefone(destino);
            DataHora = dataHora;
            TipoUso = tipoUso;
            DescricaoUso = descricaoUso;
            Cidade = cidade;
            UF = uF;
            ResumoFatura = resumoFatura;
            TempoSegundos = tempoSegundos;
            LigacaoLocal = ligacaoLocal;
        }

        private string FormatarTelefone(string telefone)
        {
            return Regex.Replace(telefone, @"(\d{2})(\d{4})(\d{4})", "($1)$2-$3");
        }

        public static string FormatarTempo(int tempoSegundos)
        {
            var tempoMinutos = tempoSegundos / 60m;
            var minutos = Math.Truncate(tempoMinutos);
            var segundos = (tempoMinutos - minutos) * 60m;
            return String.Format("{0}:{1:00}", minutos, segundos);
        }

        public virtual int IDFatura { get; set; }
        [InformacaoExibicao(1, "Seq", 40)]
        public virtual int SequenciaLig { get; set; }
        public virtual int TempoSegundos { get; set; }
        [InformacaoExibicao(5, "Tempo (mm:ss)", 95, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string Tempo { get { return FormatarTempo(TempoSegundos); } }
        [InformacaoExibicao(6, "Vlr. Minuto", 85, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorMinuto { get; set; }
        [InformacaoExibicao(7, "Total", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal TotalLig { get; set; }
        [InformacaoExibicao(8, "Origem", 85)]
        public virtual string Origem { get; set; }
        [InformacaoExibicao(9, "Destino", 85)]
        public virtual string Destino { get; set; }
        [InformacaoExibicao(4, "Data/hora", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string DataHoraStr { get { return DataHora.ToString(); } }
        public virtual DateTime DataHora { get; set; }

        [InformacaoExibicao(2, "Tipo", 70)]
        public virtual int TipoUso { get; set; }
        [InformacaoExibicao(3, "Desc. de Uso", 100)]
        public virtual string DescricaoUso { get; set; }
        [InformacaoExibicao(10, "Cidade", 85)]
        public virtual string Cidade { get; set; }
        [InformacaoExibicao(11, "UF", 40)]
        public virtual string UF { get; set; }
        public virtual string ResumoFatura { get; set; }
        public virtual bool LigacaoLocal { get; set; }
    }
}
