using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.ControleEstoque
{
    public class FechaBalanco
    {
        private readonly Balanco balanco;
        private Nota notaEntrada, notaSaida;

        public FechaBalanco(Balanco balanco)
        {
            this.balanco = balanco;
        }

        public void Fechar(DateTime dataFechamento)
        {
            balanco.Data = dataFechamento;
            GerarMovimentos();
            GerarItensMovimentos();
            SalvarTudo();
        }

        private void SalvarTudo()
        {
            if (notaEntrada.NotaItems.Count > 0)
                NotaController.Instancia.RegisterNewIntoTransaction(notaEntrada);
            if (notaSaida.NotaItems.Count > 0)
                NotaController.Instancia.RegisterNewIntoTransaction(notaSaida);

            BalancoController.Instancia.RegisterNewIntoTransaction(balanco);
            NotaController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void GerarMovimentos()
        {
            notaEntrada = MontaNota(true);
            notaSaida = MontaNota(false);

            notaSaida.Codigo = NotaController.Instancia.GetNovoCodigoNota();
            if (notaEntrada.Serie.Equals(notaSaida.Serie)) notaSaida.Numero++;
        }

        private void GerarItensMovimentos()
        {
            foreach (BalancoItem balancoItem in balanco.BalancoItems)
            {
                var saldoEstoque = NotaItemController.Instancia.SaldoEstoqueLocalEstoque(balanco.LocalEstoque.ID, balancoItem.Produto.ID);
                decimal diferenca = saldoEstoque - balancoItem.Quantidade;
                if (diferenca != 0)
                {
                    //var Prod = ProdutoController.Instancia.GetAll().Where(o => o.ID == balancoItem.Produto.ID).First();

                    balancoItem.NotaItem = new NotaItem() 
                    { 
                        Quantidade = Math.Abs(diferenca),
                        Produto = balancoItem.Produto, 
                        Dt = DateTime.Now,
                        Unidade = balancoItem.Produto.Unidade.Sigla,
                        ProdutoNome = balancoItem.Produto.Nome,
                        ProdutoDescReduzida = balancoItem.Produto.DescReduzida, 
                        BAtualizaEstoque = true,
                    };
                }
                if (diferenca > 0)
                {
                    balancoItem.NotaItem.Nota = notaSaida;
                    notaSaida.NotaItems.Add(balancoItem.NotaItem);
                }
                else if (diferenca < 0)
                {
                    balancoItem.NotaItem.Nota = notaEntrada;
                    notaEntrada.NotaItems.Add(balancoItem.NotaItem);
                }
            }
        }

        private Nota MontaNota(bool entrada)
        {
            TipoNota tipoNota = entrada
                                    ? ConfiguracaoController.Instancia.GetConfiguracao().TipoNotaBalancoEnt
                                    : ConfiguracaoController.Instancia.GetConfiguracao().TipoNotaBalancoSai;
            if (tipoNota == null) throw new NullReferenceException("Selecione um tipo de nota de balanço no menu sistema");
            tipoNota = TipoNotaController.Instancia.LoadObjectById(tipoNota.ID);
            var nota = new Nota
            {
                Codigo = NotaController.Instancia.GetNovoCodigoNota(),
                Dt = DateTime.Today,
                Ent_Sai = entrada ? InOutType.Entrada : InOutType.Saída,
                Filial = balanco.LocalEstoque.Filial,
                LocalEstoqueNota = balanco.LocalEstoque,
                NotaItems = new List<NotaItem>(),
                Serie = tipoNota.Serie,
                Status = "-1",
                TabelaPreco = TabelaPrecoController.Instancia.GetAll().First(),
                TipoNota = tipoNota,
                ModeloDocto = 0,
            };
            nota.Numero = NotaController.Instancia.NovoNumeroSerie(nota, null) + 1;
            return nota;
        }
    }
}
