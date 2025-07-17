using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{   
    [InformacaoExibicao("Obra/Fundação Proposta")]
    public class Ofd_Proposta : ModeloBase
	{
        public Ofd_Proposta()
        {
            Arquivos = new List<Ofd_PropostaArquivos>();
            Revisoes = new List<Ofd_PropostaRevisaoPerfuracoes>();
            Medicoes = new List<Ofd_PropostaMedicao>();
            Ofd_Servicos = new List<Ofd_PropostaOrcamentoServicos>();
        }

        public override int ID { get; set; }
        [InformacaoExibicao(2, "Código", 90)]
		public virtual int Codigo { get; set; }
		public virtual string NumeroProposta { get; set; }
        public virtual StatusPropostaObraFundacao Status { get; set; }
		public virtual Pessoa Cliente { get; set; }
		public virtual Pessoa ClienteFaturamento { get; set; }
        [InformacaoExibicao(4, "Obra", 180)]
		public virtual string NomeObra { get; set; }
		public virtual TipoPrecoObraFundacao TipoPreco { get; set; }
		public virtual decimal ValorTaxaMobilizacao { get; set; }
		public virtual decimal ValorHoraMaquinaParada { get; set; }
		public virtual Servico Servico { get; set; }
        public virtual Ofd_Obra Obra { get; set; }
        public virtual int PrevisaoProjeto { get; set; }
        public virtual int QuantidadeTotal { get; set; }
        [InformacaoExibicao(3, "Serviço", 200)]
        public virtual String DescricaoServico
        {
            get
            {
                if (Servico != null)
                    return Servico.Nome;
                else               
                    return "";               
            }
        }
        public virtual IList<Ofd_PropostaArquivos> Arquivos { get; set; }
        public virtual IList<Ofd_PropostaRevisaoPerfuracoes> Revisoes { get; set; }
        public virtual IList<Ofd_PropostaMedicao> Medicoes { get; set; }
        public virtual IList<Ofd_PropostaOrcamentoServicos> Ofd_Servicos { get; set; }
        

        [InformacaoExibicao(5, "Status", 150)]
        public virtual string StatusGeral
        {
            get
            {
                if (TipoLancamento == 0)
                {
                    return StatusStr;
                }
                else
                {
                    return StatusOrcamentoStr;
                }
            }
        }

        public virtual string StatusStr
        {
            get
            {
                var nome = Enum.GetName(typeof(StatusPropostaObraFundacao), Status);
                if (nome != null)
                {
                    StringBuilder ret = new StringBuilder();
                    for (int i = 0; i < nome.Length; i++)
                    {
                        if (i > 0 && Char.IsUpper(nome[i]))
                            ret.Append(" ");
                        ret.Append(nome[i]);
                    }
                    return ret.ToString();
                }
                return String.Empty;
            }
        }

        [InformacaoExibicao(6, "Código Orçamento/Proposta Vinculado(a)", 250, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string CodOrcamentoPropostaVinculado
        {
            get
            {
                if (OrcamentoPropostaVinculada != null)
                {
                    if (OrcamentoPropostaVinculada.TipoLancamento == 0)
                    {
                        return OrcamentoPropostaVinculada.Codigo.ToString();
                    }
                    else
                    {
                        return OrcamentoPropostaVinculada.CodigoOrcamento.ToString();
                    }
                }
                return String.Empty;
            }
        }
        [InformacaoExibicao(7, "Código Orçamento", 150)]
        public virtual int CodigoOrcamento { get; set; }
        public virtual StatusOrcamentoObraFundacao StatusOrcamento { get; set; }
        public virtual string StatusOrcamentoStr
        {
            get
            {
                var nome = Enum.GetName(typeof(StatusOrcamentoObraFundacao), StatusOrcamento);
                if (nome != null)
                {
                    StringBuilder ret = new StringBuilder();
                    for (int i = 0; i < nome.Length; i++)
                    {
                        if (i > 0 && Char.IsUpper(nome[i]))
                            ret.Append(" ");
                        ret.Append(nome[i]);
                    }
                    return ret.ToString();
                }
                return String.Empty;
            }
        }

        public virtual Cidade CidadeClienteOrcamento { get; set; }
        public virtual Cidade CidadeObraOrcamento { get; set; }

        [InformacaoExibicao(8, "Cliente Orçamento", 150)]
        public virtual string NomeClienteOrcamento { get; set; }

        public virtual string NomeCidadeClienteOrcamento 
        { 
            get
            {
                return CidadeClienteOrcamento.Nome;
            }
            
        }

        public virtual string TelefoneClienteOrcamento { get; set; }
        public virtual string EmailClienteOrcamento { get; set; }
        [InformacaoExibicao(9, "Obra Orçamento", 150)]
        public virtual string NomeObraOrcamento { get; set; }
        public virtual string NomeConstrutoraOrcamento { get; set; }
        public virtual string TelefoneConstrutoraOrcamento { get; set; }

        public virtual string NomeCidadeObraOrcamento
        {
            get
            {
                return CidadeObraOrcamento.Nome;
            }
        }

        public virtual string ObservacaoObraOrcamento { get; set; }

        [InformacaoExibicao(10, "Contato Orçamento", 150)]
        public virtual string AContatoOrcamento { get; set; }
        public virtual string ACContatoOrcamento { get; set; }
        public virtual string TelefoneContatoOrcamento { get; set; }
        public virtual string EmailContatoOrcamento { get; set; }
        public virtual string ObservacaoOrcamento { get; set; }
        private int _TipoLancamento;
        public virtual int TipoLancamento
        {
            get
            {
                return _TipoLancamento;
            }
            set
            {
                _TipoLancamento = value;
            }
        }

        [InformacaoExibicao(1, "Tipo Lançamento", 150)]
        public virtual string TipoLançamentoStr
        {
            get
            {
                if (TipoLancamento == 0)
                {
                    return "Proposta";
                }
                else if (TipoLancamento == 1)
                {
                    return "Orçamento";
                }
                else
                {
                    return "";
                }
            }
        }
        public virtual string EnderecoObraOrcamento { get; set; }
        public virtual int IDCidadeClienteOrcamento{ get; set; }
       
        public virtual int IDCidadeObraOrcamento{ get; set; }
        private Ofd_Proposta _OrcamentoPropostaVinculada;
        public virtual Ofd_Proposta OrcamentoPropostaVinculada
        {
            get
            {
                return _OrcamentoPropostaVinculada;
            }
            set
            {
                _OrcamentoPropostaVinculada = value;
            }
        }
        public virtual int? CodigoOrcamentoPropostaVinculado
        {
            get
            {
                if (OrcamentoPropostaVinculada != null)
                {
                    return OrcamentoPropostaVinculada.Codigo;
                }
                return null;
            }
            set
            {
            }
        }

    }
}
