using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.ControleEstoque
{
    public class GeradorInventario
    {
        public Filial Filial { get; set; }
        public DateTime Data { get; set; }
        public TipoValorProduto TipoValor { get; set; }

        public GeradorInventario(Filial filial, DateTime data, TipoValorProduto tipoValor)
        {
            Filial = filial;
            Data = data;
            TipoValor = tipoValor;
        }

        public void Gerar()
        {
            var estoques = FilialController.Instancia.GetEstoquesInventario(Filial, Data, TipoValor);
            var inventario = new Inventario();
            inventario.Codigo = InventarioController.Instancia.MaxCodigo();
            inventario.Data = Data;
            inventario.Filial = Filial;
            inventario.Descricao = String.Empty;
            foreach (var item in estoques)
            {
                var prod = new InventarioProduto
                {
                    Inventario = inventario,
                    Produto = new Produto { ID = item.IDProduto },
                    Quantidade = item.Quantidade,
                    Valor = item.Valor,
                    Total = item.Total
                };
                inventario.ListaProdutos.Add(prod);
            }
            InventarioController.Instancia.RegisterNewIntoTransaction(inventario);
        }

        public void CommitarTransacao()
        {
            InventarioController.Instancia.CommitUnitOfWorkTransaction();
        }
    }
}
