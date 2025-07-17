using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Modelo.Exceptions
{
    public class TabelaPrecoProdutoException : Exception
    {
        List<cwkGestao.Modelo.Produto> produtos = new List<Produto>();

        public TabelaPrecoProdutoException()
        {
        }

        public TabelaPrecoProdutoException(IList<Produto> produtos)
        {
            this.produtos.AddRange(produtos);
        }

        public TabelaPrecoProdutoException(Produto produto)
        {
            this.produtos.Add(produto);
        }

        public override string Message
        {
            get
            {
                return MontarMensagem();
            }
        }

        private string MontarMensagem()
        {
            if (produtos.Count == 1)
            {
                return "O produto \"" + produtos[0].Codigo + " - " + produtos[0].Nome + "\" não está configurado na tabela de preço deste orçamento.";
            }
            else
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine("Os seguintes produtos não estão configurados na tabela de preço selecionada:");

                foreach (var item in produtos)
                {
                    msg.AppendLine(" " + item.Codigo + " - " + item.Nome);
                }
                return msg.ToString();
            }
        }

        public void AdicionarProduto(Produto produto)
        {
            produtos.Add(produto);
        }
    }
}
