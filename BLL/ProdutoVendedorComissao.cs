using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio;
using Modelo;

namespace BLL
{
    public class ProdutoVendedorComissao : InterfaceBLL, IDisposable
    {

        private Modelo.DataClassesDataContext db;

        public ProdutoVendedorComissao(Modelo.DataClassesDataContext db)
        {
            this.db = db;
        }

        #region InterfaceBLL Members

        public IQueryable getLista()
        {
            throw new NotImplementedException();
        }

        public IQueryable getLista(string pFiltro)
        {
            throw new NotImplementedException();
        }

        public IQueryable getListaCombo()
        {
            throw new NotImplementedException();
        }

        public IQueryable getListaCombo(string pFiltro)
        {
            throw new NotImplementedException();
        }

        public object getObjeto(int pID)
        {
            return (from u in db.ProdutoVendedorComissaos where u.ID == pID select u).Single();
        }

        public int getID(string pValor)
        {
            throw new NotImplementedException();
        }

        public int RetornaMAXCodigo()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<Modelo.Objeto.pxyProdutoVendedorComissao> getComissoes(int vendedorID)
        {
            List<Modelo.Objeto.pxyProdutoVendedorComissao> results = new List<Modelo.Objeto.pxyProdutoVendedorComissao>();
            results = (from u in db.ProdutoVendedorComissaos
                       where u.IDVendedor == vendedorID
                       select new Modelo.Objeto.pxyProdutoVendedorComissao
                       {
                           Comissao = u.ComissaoPct,
                           Descricao = u.Produto.Nome,
                           IDProduto = u.Produto.ID,
                           IDVendedor = u.Vendedor.ID,
                           Vendedor = u.Vendedor.Pessoa.Nome,
                           TipoComissao = u.TipoComissao,

                           ID = u.ID

                       }).ToList();
            return results;
            //return null;
        }

        public IList<object> getComissoesRel(int vendedorID, List<int> tiposNota, List<int> empresaIDs, DateTime dataInicio, DateTime dataFim)
        {
            List<Modelo.Objeto.pxyComissaoVendedorRel> results = new List<Modelo.Objeto.pxyComissaoVendedorRel>();
            var configuracao = ConfiguracaoController.Instancia.GetAll()[0];
                      
                if (vendedorID > 0)
                { 
                    results = (from u in db.NotaItems
                               join nip in db.NotaItemPedidoItems on u.ID equals nip.IDNotaItem into nip2
                                 from nip in nip2.DefaultIfEmpty()
                                 join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID into itp2
                                   from itp in itp2.DefaultIfEmpty()
                                   join ped in db.Pedidos on itp.IDPedido equals ped.ID into ped2
                                     from ped in ped2.DefaultIfEmpty()
                               where u.Nota.Vendedor.Pessoa.ID == vendedorID && u.Nota.CancDt == null
                                   && tiposNota.Contains(u.Nota.TipoNota.ID)
                                   && empresaIDs.Contains(u.Nota.Filial.ID)
                                   && u.Nota.Dt.Date >= dataInicio
                                   && u.Nota.Dt.Date <= dataFim
                               select new Modelo.Objeto.pxyComissaoVendedorRel
                               {
                                   Cliente = u.Nota.Pessoa.Nome,
                                                 //Caso parametro esteja selecionado para utilizar comissão comissão pedido e existir comissão no pedido, pega comissão do pedido
                                   ComissaoPct = (configuracao.ComissaoPedido == 1 && ped.PercComissao > 0 ? null :
                                                 // Se não pegar comissão pedido, verifica se existe comissão por produto
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                  (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() != null ?
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                         (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() :
                                                 //Caso não exista comissão no pedido nem por produto, pega comissão pelo item da nota
                                                 u.ComissaoPorcentagem != 0 ? u.ComissaoPorcentagem :
                                                 //Se não existir comissão no pedido, comissão por produto, nem comissão no item da venda, busca a comissão por vendedor
                                                 u.Nota.Vendedor.ComissaoVendedor
                                                 ),

                                   ComissaoValor = ((configuracao.ComissaoPedido == 1 && ped.PercComissao > 0 ? null :
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                     (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() != null ?
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                         (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() :
                                                     u.ComissaoPorcentagem != 0 ? u.ComissaoPorcentagem:
                                                     u.Nota.Vendedor.ComissaoVendedor
                                                    )* u.Valor * u.Quantidade) / 100,
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                   PercPedido = ped.PercComissao == null ? 0m : ped.PercComissao,
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                   ComissaoPedido = configuracao.ComissaoPedido == 1 && (ped.PercComissao == null ? 0m : ped.PercComissao) > 0 ? ped.ValorComissao : 0m,
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                   ComissaoVendedor = u.Nota.Vendedor.ComissaoVendedor,
                                   Data = u.Nota.Dt,
                                   Nota = u.Nota.Numero,
                                   Produto = u.Produto.Nome,
                                   Valor = u.Valor,
                                   Vendedor = u.Nota.Vendedor.Pessoa.Nome,
                                   Quantidade = u.Quantidade,
                                   DescGeral = u.Nota.PercDesconto,

                                   DescUnitario = (

                                        from nipi in db.NotaItemPedidoItems
                                        join pi in db.PedidoItems on nipi.IDPedidoItem equals pi.ID
                                        where u.PedidoItem.ID == nipi.IDPedidoItem
                                        select pi.PercDesconto).First(),
                                   Hora = u.Nota.IncData,
                                   ValorTotalNota = u.Nota.TotalNota
                               }).ToList();
                }
                else
                {
                    results = (from u in db.NotaItems
                               join nip in db.NotaItemPedidoItems on u.ID equals nip.IDNotaItem into nip2
                                 from nip in nip2.DefaultIfEmpty()
                                 join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID into itp2
                                   from itp in itp2.DefaultIfEmpty()
                                   join ped in db.Pedidos on itp.IDPedido equals ped.ID into ped2
                                     from ped in ped2.DefaultIfEmpty()
                               where u.Nota.CancDt == null
                                   && tiposNota.Contains(u.Nota.TipoNota.ID)
                                   && empresaIDs.Contains(u.Nota.Filial.ID)
                                   && u.Nota.Dt.Date >= dataInicio
                                   && u.Nota.Dt.Date <= dataFim
                               select new Modelo.Objeto.pxyComissaoVendedorRel
                               {
                                   Cliente = u.Nota.Pessoa.Nome,

                                       //Caso parametro esteja selecionado para utilizar comissão comissão pedido e existir comissão no pedido, pega comissão do pedido
                                   ComissaoPct = (configuracao.ComissaoPedido == 1 && ped.PercComissao > 0 ? null :
                                       // Se não pegar comissão pedido, verifica se existe comissão por produto
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                  (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() != null ?
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                         (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() :
                                       //Caso não exista comissão no pedido nem por produto, pega comissão pelo item da nota
                                                 u.ComissaoPorcentagem != 0 ? u.ComissaoPorcentagem :
                                       //Se não existir comissão no pedido, comissão por produto, nem comissão no item da venda, busca a comissão por vendedor
                                                 u.Nota.Vendedor.ComissaoVendedor
                                                 ),
                                   ComissaoValor = ((configuracao.ComissaoPedido == 1 && ped.PercComissao > 0 ? null :
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                     (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() != null ?
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                                                         (from c in db.ProdutoVendedorComissaos where c.IDProduto == u.Produto.ID && c.IDVendedor == u.Nota.Vendedor.Pessoa.ID select c.ComissaoPct).First() :
                                                     u.ComissaoPorcentagem != 0 ? u.ComissaoPorcentagem :
                                                     u.Nota.Vendedor.ComissaoVendedor
                                                    ) * u.Valor * u.Quantidade) / 100,
                                   ComissaoPedido = configuracao.ComissaoPedido == 1 && ped.PercComissao > 0 ? ped.ValorComissao : 0m,
                                   ComissaoVendedor = u.Nota.Vendedor.ComissaoVendedor,
                                   Data = u.Nota.Dt,
                                   Nota = u.Nota.Numero,
                                   Produto = u.Produto.Nome,
                                   Valor = u.Valor,
                                   Vendedor = u.Nota.Vendedor.Pessoa.Nome,
                                   Quantidade = u.Quantidade,
                                   DescGeral = u.Nota.PercDesconto,
                                   DescUnitario = (

                                          from nipi in db.NotaItemPedidoItems
                                          join pi in db.PedidoItems on nipi.IDPedidoItem equals pi.ID
                                          where u.PedidoItem.ID == nipi.IDPedidoItem
                                          select pi.PercDesconto).First(),
                                   Hora = u.Nota.IncData,
                                   ValorTotalNota = u.Nota.TotalNota

                               }).ToList();
                }
            return results.Cast<object>().ToList();
        }

        public IList<object> getComissoesRelFinanceiro(int vendedorID, List<int> tiposNota, List<int> empresaIDs, DateTime dataInicio, DateTime dataFim)
        {
            var configuracao = ConfiguracaoController.Instancia.GetConfiguracao();

            List<Modelo.Objeto.pxyComissaoVendedorRel> results = new List<Modelo.Objeto.pxyComissaoVendedorRel>();
            Func<Modelo.NotaItem, decimal> somador = delegate(Modelo.NotaItem notItem)
            {
                return (notItem.ComissaoPorcentagem * notItem.Valor * notItem.Quantidade) / 100;
            };
            if (vendedorID > 0)
                results = (from u in db.Movimentos
                           where u.Documento.Nota.Vendedor.Pessoa.ID == vendedorID && u.Documento.Nota.CancDt == null
                               && tiposNota.Contains(u.Documento.Nota.TipoNota.ID)
                               && empresaIDs.Contains(u.Documento.Nota.Filial.ID)
                               && u.Dt >= dataInicio
                               && u.Dt <= dataFim
                               && (u.Operacao == Modelo.OperacaoMovimentoType.BxParcial
                               || u.Operacao == Modelo.OperacaoMovimentoType.BxTotal)
                               && u.Tipo == Modelo.TipoMovimentoType.Documento

                           select new Modelo.Objeto.pxyComissaoVendedorRel
                           {
                               Cliente = u.Documento.Nota.Pessoa.Nome,

                               ComissaoPct = (u.Documento.Nota.NotaItems.Sum(ux => (((configuracao.ComissaoPedido == 1 && (from doc in db.Documentos
                                                                                                                           join n in db.Notas on doc.IDNota equals n.ID
                                                                                                                           join it in db.NotaItems on n.ID equals it.IDNota
                                                                                                                           join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                                                           join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                                                           join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                                                           where doc.ID == u.IDDocumento
                                                                                                                           select ped.PercComissao).FirstOrDefault() > 0) ?
                                                                                       (from doc in db.Documentos
                                                                                        join n in db.Notas on doc.IDNota equals n.ID
                                                                                        join it in db.NotaItems on n.ID equals it.IDNota
                                                                                        join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                        join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                        join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                        where doc.ID == u.IDDocumento
                                                                                        select ped.PercComissao).FirstOrDefault() :
                                                                                     ux.ComissaoPorcentagem == 0 ?
                                                                                       u.Documento.Nota.Vendedor.ComissaoVendedor : ux.ComissaoPorcentagem
                                                                                    ) * ux.Valor * ux.Quantidade) / 100) / u.Documento.Nota.TotalProduto.Value) * 100,


                               ComissaoValor = (u.Documento.Nota.NotaItems.Sum(ux => (((configuracao.ComissaoPedido == 1 && (from doc in db.Documentos
                                                                                                                             join n in db.Notas on doc.IDNota equals n.ID
                                                                                                                             join it in db.NotaItems on n.ID equals it.IDNota
                                                                                                                             join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                                                             join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                                                             join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                                                             where doc.ID == u.IDDocumento
                                                                                                                             select ped.PercComissao).FirstOrDefault() > 0) ?
                                                                                       (from doc in db.Documentos
                                                                                        join n in db.Notas on doc.IDNota equals n.ID
                                                                                        join it in db.NotaItems on n.ID equals it.IDNota
                                                                                        join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                        join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                        join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                        where doc.ID == u.IDDocumento
                                                                                        select ped.PercComissao).FirstOrDefault() :
                                                                                     ux.ComissaoPorcentagem == 0 ?
                                                                                       u.Documento.Nota.Vendedor.ComissaoVendedor : ux.ComissaoPorcentagem
                                                                                    ) * ux.Valor * ux.Quantidade) / 100) / u.Documento.Nota.TotalProduto.Value) * u.Valor.Value,
                               ComissaoVendedor = u.Documento.Nota.Vendedor.ComissaoVendedor,
                               Data = u.Dt.Value,
                               Nota = u.Documento.Nota.Numero,
                               //Produto = u.Produto.Nome,
                               Valor = u.Valor.Value,
                               Vendedor = u.Documento.Nota.Vendedor.Pessoa.Nome,
                               Quantidade = u.Documento.Nota.TotalNota.Value,
                               ValorDocumento = u.Documento.Valor.Value,
                               DocFin = u.Documento.Codigo,
                               Parcela = u.Documento.Parcela + " de " + u.Documento.QtParcela
                           }).ToList();
            else
            {
                results = (from u in db.Movimentos
                           where u.Documento.Nota.CancDt == null
                               && tiposNota.Contains(u.Documento.Nota.TipoNota.ID)
                               && empresaIDs.Contains(u.Documento.Nota.Filial.ID)
                               && u.Dt >= dataInicio
                               && u.Dt <= dataFim
                               && (u.Operacao == Modelo.OperacaoMovimentoType.BxParcial
                               || u.Operacao == Modelo.OperacaoMovimentoType.BxTotal)
                               && u.Tipo == Modelo.TipoMovimentoType.Documento

                           select new Modelo.Objeto.pxyComissaoVendedorRel
                           {
                               Cliente = u.Documento.Nota.Pessoa.Nome,
                               
                               ComissaoPct = (u.Documento.Nota.NotaItems.Sum(ux => (((configuracao.ComissaoPedido == 1 && (from doc in db.Documentos
                                                                                                                           join n in db.Notas on doc.IDNota equals n.ID
                                                                                                                           join it in db.NotaItems on n.ID equals it.IDNota
                                                                                                                           join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                                                           join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                                                           join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                                                           where doc.ID == u.IDDocumento
                                                                                                                           select ped.PercComissao).FirstOrDefault() > 0) ? 
                                                                                       (from   doc in db.Documentos
                                                                                        join   n   in db.Notas on doc.IDNota equals n.ID
                                                                                        join   it  in db.NotaItems on n.ID equals it.IDNota
                                                                                        join   nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                        join   itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                        join   ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                        where  doc.ID == u.IDDocumento
                                                                                        select ped.PercComissao).FirstOrDefault():
                                                                                     ux.ComissaoPorcentagem == 0 ?
                                                                                       u.Documento.Nota.Vendedor.ComissaoVendedor : ux.ComissaoPorcentagem
                                                                                    ) * ux.Valor * ux.Quantidade) / 100) / u.Documento.Nota.TotalProduto.Value) * 100 ,


                               ComissaoValor = (u.Documento.Nota.NotaItems.Sum(ux => (((configuracao.ComissaoPedido == 1 && (from doc in db.Documentos
                                                                                                                             join n in db.Notas on doc.IDNota equals n.ID
                                                                                                                             join it in db.NotaItems on n.ID equals it.IDNota
                                                                                                                             join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                                                             join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                                                             join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                                                             where doc.ID == u.IDDocumento
                                                                                                                             select ped.PercComissao).FirstOrDefault() > 0) ?
                                                                                       (from doc in db.Documentos
                                                                                        join n in db.Notas on doc.IDNota equals n.ID
                                                                                        join it in db.NotaItems on n.ID equals it.IDNota
                                                                                        join nip in db.NotaItemPedidoItems on it.ID equals nip.IDNotaItem
                                                                                        join itp in db.PedidoItems on nip.IDPedidoItem equals itp.ID
                                                                                        join ped in db.Pedidos on itp.IDPedido equals ped.ID
                                                                                        where doc.ID == u.IDDocumento
                                                                                        select ped.PercComissao).FirstOrDefault() :
                                                                                     ux.ComissaoPorcentagem == 0 ?
                                                                                       u.Documento.Nota.Vendedor.ComissaoVendedor : ux.ComissaoPorcentagem
                                                                                    ) * ux.Valor * ux.Quantidade) / 100) / u.Documento.Nota.TotalProduto.Value) * u.Valor.Value,
                               ComissaoVendedor = u.Documento.Nota.Vendedor.ComissaoVendedor,
                               Data = u.Dt.Value,
                               Nota = u.Documento.Nota.Numero,
                               //Produto = u.Produto.Nome,
                               Valor = u.Valor.Value,
                               Vendedor = u.Documento.Nota.Vendedor.Pessoa.Nome,
                               Quantidade = u.Documento.Nota.TotalNota.Value,
                               ValorDocumento = u.Documento.Valor.Value,
                               DocFin = u.Documento.Codigo,
                               Parcela = u.Documento.Parcela + " de " + u.Documento.QtParcela
                           }).ToList();
            }
            return results.Cast<object>().ToList();
        }

    }
}
