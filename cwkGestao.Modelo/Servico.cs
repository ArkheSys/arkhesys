using cwkGestao.Modelo.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{

    [InformacaoExibicao("Serviço")]

	public class Servico : ModeloBase
	{
        public Servico()
        {
            ServicoImpostos = new List<ServicoServicoImposto>();
            Ofd_Servicos = new List<Ofd_PropostaOrcamentoServicos>();
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        [InformacaoExibicao(1, "Código", 150)]
        public virtual int Codigo { get; set;}

        [InformacaoExibicao(2, "Nome", 480)]
        public virtual string Nome { get; set;}

        [InformacaoExibicao(3, "Valor", 150, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }

        public virtual string Descricao { get; set;}

        public virtual bool? GeraBD { get; set; }

        public virtual DateTime? Incdata { get; set; }

        public virtual string Incusuario { get; set; }

        public virtual DateTime? Altdata { get; set; }

        public virtual string Altusuario { get; set; }

        public virtual bool Tipo { get; set; }

        public virtual bool BaseRetencaoImposto { get; set; }

        public virtual IList<ServicoServicoImposto> ServicoImpostos {get;set;}

        public virtual string CodigoItemListaServico { get; set; }

        public virtual int TipoTributacao { get; set; }

        public virtual int ExigibilidadeISS { get; set; }

        public virtual string Art { get; set; }

        public virtual string CodigoObra { get; set; }

        public virtual int IncentivoFiscal { get; set; }

        public virtual PlanoConta PlanoDeContas { get; set; }

        public virtual IList<Ofd_PropostaOrcamentoServicos> Ofd_Servicos { get; set; }

        public virtual string InformacoesAdicionais { get; set; }

        public virtual IList<ProdutoServico> Materiais { get; set; }

        public virtual string Imagem { get; set; }

        public virtual IList<LogAlteracaoPedidoItem> LogAlteracaoPedidoItens { get; set; }


    }
}
		