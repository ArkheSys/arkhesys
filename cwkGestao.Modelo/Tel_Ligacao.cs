using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Ligação")]
#pragma warning disable CS0659 // "Tel_Ligacao" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
	public class Tel_Ligacao : ModeloBase
#pragma warning restore CS0659 // "Tel_Ligacao" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
	{
        public Tel_Ligacao() { }

        public Tel_Ligacao(int id, string origem, string destino, DateTime dataHora)
        {
            ID = id;
            Origem = origem;
            Destino = destino;
            DataHora = dataHora;
        }

        public Tel_Ligacao(int id, int tipoUso, string descricaoUso, string origem, string destino, string cidade, string uf, DateTime dataHora, int tempoSeg, string horario, string valorComImposto, int gerado)
        {
            ID = id;
            TipoUso = tipoUso;
            DescricaoUso = descricaoUso;
            Origem = origem;
            Destino = destino;
            Cidade = cidade;
            UF = uf;
            DataHora = dataHora;
            TempoSegundos = tempoSeg;
            Horario = horario;
            ValorComImposto = valorComImposto;
            GeradoFatura = Convert.ToBoolean(gerado);
        }

        public override int ID { get; set; }
        [InformacaoExibicao(1, "Tipo de Uso", 90)]
        public virtual int TipoUso { get; set; }
        [InformacaoExibicao(2,"Descrição de Uso",200)]
        public virtual string DescricaoUso { get; set; }
        [InformacaoExibicao(3,"Origem",90)]
        public virtual string Origem { get; set; }
        [InformacaoExibicao(4,"Destino",90)]
        public virtual string Destino { get; set; }
        [InformacaoExibicao(5,"Cidade",90)]
        public virtual string Cidade { get; set; }
        [InformacaoExibicao(6,"UF",40)]
        public virtual string UF { get; set; }
        [InformacaoExibicao(7,"Data",80)]
        public virtual DateTime DataHora { get; set; }
        [InformacaoExibicao(8,"Duração",60)]
        public virtual int TempoSegundos { get; set; }
        [InformacaoExibicao(9,"Horário",45)]
        public virtual string Horario { get; set; }
        [InformacaoExibicao(10, "ValorcomImposto", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal2Casas)]
        public virtual string ValorComImposto { get; set; }
        public virtual Tel_ArquivoLigacao ArquivoLigacao { get; set; }
        private IList<Tel_FaturaLigacao> _FaturaLigacao;
        public virtual IList<Tel_FaturaLigacao> FaturaLigacao
        { 
            get
            {
                return _FaturaLigacao;
            }
            set
            {
                _FaturaLigacao = value;
            }
        }
        [InformacaoExibicao(11, "Gerado Fatura?", 110)]
        public virtual bool GeradoFatura { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Tel_Ligacao)
            {
                var lig = (Tel_Ligacao)obj;
                return GetChaveUnica() == lig.GetChaveUnica();
            }
            return false;
        }

        public virtual string GetChaveUnica()
        {
            return (Origem + Destino + DataHora.ToString());
        }
    }
}
		