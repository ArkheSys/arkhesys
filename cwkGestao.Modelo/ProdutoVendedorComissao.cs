using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Comissão")]
    public class ProdutoVendedorComissao : ModeloBase
    {
        public override int ID { get; set; }

        private Produto produto;

        public virtual Produto Produto
        {
            get
            {
                return produto;
            }
            set
            {
                produto = value;
            }
        }

        private Servico servico;
        public virtual Servico Servico
        {
            get
            {
                return servico;
            }
            set
            {
                servico = value;
            }
        }

        private GrupoEstoque grupoEstoque;
        public virtual GrupoEstoque GrupoEstoque
        {
            get
            {
                return grupoEstoque;
            }
            set
            {
                grupoEstoque = value;
            }
        }
        public virtual PessoaVendedor Vendedor { get; set; }
        public virtual decimal ComissaoPct { get; set; }
        public virtual byte TipoComissao { get; set; }
        public virtual decimal ComissaoPctServico { get; set; }
        public virtual byte TipoComissaoServico { get; set; }
        public virtual decimal ComissaoPctGrupoEstoque { get; set; }
        public virtual byte TipoComissaoGrupoEstoque { get; set; }

        public enum TipoComissaoProdutoServicoType
        {
            Produto = 0,
            Servico = 1,
            GrupoEstoque = 2
        }

        private TipoComissaoProdutoServicoType tipoComissaoProdutoServico;
        public virtual TipoComissaoProdutoServicoType TipoComissaoProdutoServico 
        {
            get
            {
                SetaObjetosGrid();

                return tipoComissaoProdutoServico;
            }
            set
            {
                tipoComissaoProdutoServico = value;

                SetaObjetosGrid();
            }
        }

        private void SetaObjetosGrid()
        {
            switch (tipoComissaoProdutoServico)
            {
                case TipoComissaoProdutoServicoType.Produto:
                    TipoComissaoProdutoServicoStr = "Produto";
                    Descricao = Produto == null ? String.Empty : Produto.Nome;
                    ComissaoPctStr = ComissaoPct + "%";
                    break;
                case TipoComissaoProdutoServicoType.Servico:
                    TipoComissaoProdutoServicoStr = "Serviço";
                    Descricao = Servico == null ? String.Empty : Servico.Nome;
                    ComissaoPctStr = ComissaoPctServico + "%";
                    break;
                case TipoComissaoProdutoServicoType.GrupoEstoque:
                    TipoComissaoProdutoServicoStr = "Grupo de Estoque";
                    Descricao = GrupoEstoque == null ? String.Empty : GrupoEstoque.Nome;
                    ComissaoPctStr = ComissaoPctGrupoEstoque + "%";
                    break;
            }
        }
        
        [InformacaoExibicao(0, "Tipo", 110)]
        public virtual String TipoComissaoProdutoServicoStr { get; set; }

        [InformacaoExibicao(1, "Descrição", 400)]
        public virtual String Descricao { get; set; }
        
        [InformacaoExibicao(2, "Comissão", 110)]
        public virtual String ComissaoPctStr { get; set; }
    }
}
