using System;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Faturamento
{
    public class OrdemServico_Produto : OrdemServico
    {
        protected override void GerarNotas()
        {
            if (ordensServicoFaturar.Sum(s => s.OSProdutoItems.Count) == 0)
            {
                return;
            }
            var nota = new Modelo.Nota();
            var ordemServico = ordensServicoFaturar.First();
            PreencherDadosBasicosNota(nota);
            PreencherNotaComTipoNota(nota, TipoMovimentacao1);
            PreencherNotaComOrdemServico(nota, ordemServico);

            foreach (var os in ordensServicoFaturar)
            {
                IncluirProdutosNota(nota, os);
                nota.ValorDesconto += os.ProdutoDescontoValor;
            }

            SetTotaisNota(nota);
            notasGeradas.Add(nota);
            CriarParcelasNotasGeradas();
        }

        protected override void ValidarDados()
        {
            StringBuilder mensagemErro = new StringBuilder();

            if (ordensServicoFaturar != null)
            {
                if (ordensServicoFaturar.Sum(s => s.OSProdutoItems.Count) > 0)
                {
                    if (ConfiguracaoController.Instancia.GetTipoNfe() == Modelo.AdesaoNFe.Total || 
                       (Cliente.BOrgaoPublico && Filial.PessoaForaDoEstado(Cliente)))
                    {
                        if ((TipoMovimentacao1.ModeloDocto != Modelo.TipoNota.ModeloDocumento.NFe) &&
                            (TipoMovimentacao1.ModeloDocto != cwkGestao.Modelo.TipoNota.ModeloDocumento.Balanco))
                        {
                            mensagemErro.AppendLine("- O modelo de documento do Tipo de Movimentação 1 deve ser 55 (NFe) ou Balanço.");
                        }
                    }
                    else
                    {
                        if (!((TipoMovimentacao1.ModeloDocto == cwkGestao.Modelo.TipoNota.ModeloDocumento.NFe) ||
                             (TipoMovimentacao1.ModeloDocto == cwkGestao.Modelo.TipoNota.ModeloDocumento.CupomFiscal) ||
                             (TipoMovimentacao1.ModeloDocto == cwkGestao.Modelo.TipoNota.ModeloDocumento.Balanco)))
                        {
                            mensagemErro.AppendLine("- O modelo de documento do Tipo de Movimentação 1 deve ser \"NFe\", \"Cupom Fiscal\" ou  ou \"Balanço\".");
                        }
                    }

                    if (TipoMovimentacao1.Categoria != Modelo.TipoNota.CategoriaMovimentacao.Produtos)
                    {
                        mensagemErro.AppendLine("- A categoria do Tipo de Movimentação 1 deve ser Produtos.");
                    }

                    if (mensagemErro.Length > 0)
                    {
                        throw new Exception("Verificar anomalias: " + Environment.NewLine + mensagemErro);
                    }
                    if (mensagemErro.Length > 0)
                    {
                        throw new Exception("Verificar anomalias: " + Environment.NewLine + mensagemErro.ToString());
                    }
                }
            }
        }
    }
}
