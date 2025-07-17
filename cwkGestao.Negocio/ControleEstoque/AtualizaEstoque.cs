using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Exceptions;

namespace cwkGestao.Negocio.ControleEstoque
{
    public abstract class AtualizaEstoque
    {
        protected Acao acao;
        protected bool permiteEstoqueNegativo;
        public virtual List<ParametroEstoque> ParametrosAtualizacao { get; private set; }
        public virtual List<Estoque> EstoquesAtualizar { get; private set; }

        public static AtualizaEstoque GetAtualizaEstoque(ModeloBase objeto)
        {
            if (objeto.GetType() == typeof(Modelo.Pedido))
            {
                return new PedidoAtualizaEstoque((Modelo.Pedido)objeto);
            }
            else if (objeto.GetType() == typeof(Modelo.Nota))
            {
                return new NotaAtualizaEstoque((Modelo.Nota)objeto);
            }
            return null;
        }

        protected virtual IEnumerable<ParametroEstoque> GetAtualizacoes()
        {
            switch (acao)
            {
                case Acao.Incluir:
                    return GetInclusaoParametros();
                case Acao.Alterar:
                    return GetAlteracaoParametros();
                case Acao.Excluir:
                    return GetExclusaoParametros();
            }
            return new List<ParametroEstoque>();
        }

        protected abstract IEnumerable<ParametroEstoque> GetInclusaoParametros();
        protected abstract IEnumerable<ParametroEstoque> GetAlteracaoParametros();
        protected abstract IEnumerable<ParametroEstoque> GetExclusaoParametros();

        public void Atualizar(Acao acao)
        {
            this.acao = acao;
            ParametrosAtualizacao = GetAtualizacoes().ToList();
            EstoquesAtualizar = new List<Estoque>();
            foreach (var item in ParametrosAtualizacao)
            {
                var estoque = GetEstoque(item);
                AtualizarEstoque(estoque, item);
            }
            VerificarEstoqueNegativo();
            EstoqueController.Instancia.RegisterNewIntoTransaction(EstoquesAtualizar);
        }

        private void VerificarEstoqueNegativo()
        {
            if (!permiteEstoqueNegativo)
            {
                StringBuilder produtosEstoqueNegativo = new StringBuilder();
                foreach (var item in EstoquesAtualizar)
                {
                    if (item.Efetivo < 0)
                        produtosEstoqueNegativo.AppendLine(item.Produto.Codigo + " - " + item.Produto.Nome);
                }
                if (produtosEstoqueNegativo.Length > 0)
                    throw new EstoqueNegativoException(produtosEstoqueNegativo.ToString());
            }
        }

        private Estoque GetEstoque(ParametroEstoque item)
        {
            var xuo = EstoquesAtualizar.Where(e => e.Produto.ID == item.produto.ID && e.LocalEstoque.ID == item.localEstoque.ID);
            if (xuo.Count() > 0)
                return xuo.First();
            else
            {
                var ret = EstoqueController.Instancia.GetEstoque(item.localEstoque, item.produto);
                EstoquesAtualizar.Add(ret);
                return ret;
            }
        }

        private void AtualizarEstoque(Estoque estoque, ParametroEstoque parametro)
        {
            AtualizarEstoqueFisico(estoque, parametro);
            AtualizarEstoqueReservado(estoque, parametro);
        }

        private static void AtualizarEstoqueReservado(Estoque estoque, ParametroEstoque parametro)
        {
            if (parametro.qtdCompra_Pedido.HasValue)
                estoque.Qtd_Compra_Pedido += parametro.qtdCompra_Pedido.Value;
            if (parametro.qtdEstoque_Pedido.HasValue)
                estoque.Qtd_Estoque_Pedido += parametro.qtdEstoque_Pedido.Value;
        }

        private static void AtualizarEstoqueFisico(Estoque estoque, ParametroEstoque parametro)
        {
            if (parametro.qtdAcrescentar_Fisico.HasValue)
            {
                if (parametro.custoUnitarioNotaItem.HasValue)
                    estoque.CustoMedioUnit = ((estoque.Qtd_Estoque_Fisico * estoque.CustoMedioUnit)
                                            + (parametro.custoUnitarioNotaItem.Value * parametro.qtdAcrescentar_Fisico.Value))
                                            / (estoque.Qtd_Estoque_Fisico + parametro.qtdAcrescentar_Fisico.Value);

                estoque.Qtd_Estoque_Fisico += parametro.qtdAcrescentar_Fisico.Value;
            }
            if (parametro.qtdDescontar_Fisico.HasValue)
                estoque.Qtd_Estoque_Fisico -= parametro.qtdDescontar_Fisico.Value;
        }
    }
}
