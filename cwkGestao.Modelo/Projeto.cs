using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Centro de Custo")]
    public class Projeto : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Nome", 200)]
        public virtual string Nome { get; set; }
        [InformacaoExibicao(1, "Descrição", 400)]
        public virtual string Descricao { get; set; }
        public virtual int Status { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual decimal MarLucroMaoObra { get; set; }
        public virtual decimal MarLucroProduto { get; set; }
        public virtual decimal PercDescontoMaoObra { get; set; }
        public virtual decimal ValorDescontoMaoObra { get; set; }
        public virtual decimal PercDescontoProduto { get; set; }
        public virtual decimal ValorDescontoProduto { get; set; }
        public virtual decimal TotalProdutos { get; set; }
        public virtual decimal TotalServicos { get; set; }
        public virtual decimal TotalGeral { get; set; }
        public virtual string CaminhoDocumento { get; set; }
        public virtual string InstrucoesProducao { get; set; }
        public virtual bool BCentroDeCusto { get; set; }
        public virtual short MotivoCancelamento { get; set; }
        public virtual IList<OrdemProducao> OrdemProducaos { get; set; }
        public virtual IList<Pedido> Pedidos { get; set; }

        public virtual IList<Projeto_Material> Projeto_Materials { get; set; }

        public virtual IList<Projeto_ProdutoServico> Projeto_ProdutoServicos { get; set; }

        public virtual IList<ProjetoHistorico> ProjetoHistoricos { get; set; }

        public virtual IList<Rateio_Mov> Rateio_Movs { get; set; }

        public virtual IList<Rateio_NotaItem> Rateio_NotaItems { get; set; }


        public virtual string ComplementoHist { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual Condicao Condicao { get; set; }
        public virtual Portador Portador { get; set; }
        public virtual Historico Historico { get; set; }
        public virtual PlanoConta PlanoContaServico { get; set; }
        public virtual PlanoConta PlanoConta { get; set; }
        public virtual Acrescimo Acrescimo { get; set; }
        public virtual string NumeroProposta { get; set; }
       

    }
}