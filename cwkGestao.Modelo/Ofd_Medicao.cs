using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Medição")]
    public class Ofd_Medicao : ModeloBase
	{
        public Ofd_Medicao()
        {
            MedicaoServicos = new List<Ofd_MedicaoServicos>();
        }

        public Ofd_Medicao(int id, DateTime data, int numero, int status, string proposta, decimal valorServicos
            , decimal valorMobilizacao, decimal valorProjeto, decimal valorHoraParada, decimal totalMedicao
            , DateTime? incData, string incUsuario, DateTime? altData, string altUsuario)
            : this()
        {
            ID = id;
            Data = data;
            Numero = numero;
            Status = status;
            Proposta = new Ofd_Proposta() { NomeObra = proposta };
            ValorServicos = valorServicos;
            ValorMobilizacao = valorMobilizacao;
            ValorProjeto = valorProjeto;
            ValorHoraParada = valorHoraParada;
            TotalMedicao = totalMedicao;

            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
        }

        
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Data", 90)]
		public virtual DateTime Data { get; set; }
		public virtual int Numero { get; set; }
		public virtual Ofd_Proposta Proposta { get; set; }

      
		public virtual int Status { get; set; }
        [InformacaoExibicao(8, "Status", 150)]
        public virtual string StatusExibir { 
            get
            {
                switch (Status)
	            {
                    case 0: return "Pendente";
                    case 1: return "Aguardando Faturamento";
                    case 2: return "Faturada";
                    case 3: return "Cancelada";
                    default: return "";

	            }
            }
            }
        [InformacaoExibicao(3, "Valor Servicos", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
		public virtual decimal ValorServicos { get; set; }
        [InformacaoExibicao(4, "Valor Mobilização", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
		public virtual decimal ValorMobilizacao { get; set; }
        [InformacaoExibicao(5, "Valor Projeto", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
		public virtual decimal ValorProjeto { get; set; }
        [InformacaoExibicao(6, "Valor Hora Parada", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
		public virtual decimal ValorHoraParada { get; set; }
        [InformacaoExibicao(7, "Total Medição", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
		public virtual decimal TotalMedicao { get; set; }
        public virtual IList<Ofd_MedicaoServicos> MedicaoServicos { get; set; }
        
        [InformacaoExibicao(2, "Proposta", 90)]
        public virtual string PropostaStr { get { return Proposta != null ? Proposta.NomeObra : String.Empty; } }

        [InformacaoExibicao(9, "Data Inclusão", 100)]
        public override System.DateTime? IncData
        {
            get
            {
                return base.IncData;
            }
            set
            {
                base.IncData = value;
            }
        }

        [InformacaoExibicao(10, "Usuário Inclusão", 100)]
        public override string IncUsuario
        {
            get
            {
                return base.IncUsuario;
            }
            set
            {
                base.IncUsuario = value;
            }
        }

        [InformacaoExibicao(11, "Data Alteração", 100)]
        public override DateTime? AltData
        {
            get
            {
                return base.AltData;
            }
            set
            {
                base.AltData = value;
            }
        }

        [InformacaoExibicao(12, "Usuário Alteração", 100)]
        public override string AltUsuario
        {
            get
            {
                return base.AltUsuario;
            }
            set
            {
                base.AltUsuario = value;
            }
        }
    }
}
