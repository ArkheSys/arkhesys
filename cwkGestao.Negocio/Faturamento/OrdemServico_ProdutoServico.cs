using System;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Faturamento
{
    public class OrdemServico_ProdutoServico : OrdemServico
    {
        protected override void GerarNotas()
        {
            if (TipoMovimentacao1.ModeloDocto != Modelo.TipoNota.ModeloDocumento.NFe
                && ConfiguracaoController.Instancia.GetConfiguracao().NotaConjugada)
            {
                GerarNotaConjugada();
            }
            else
            {
                GerarDuasNotas();
            }
            CriarParcelasNotasGeradas();
        }

        private void GerarNotaConjugada()
        {
            var nota = new Modelo.Nota();
            var ordemServico = ordensServicoFaturar.First();
            PreencherDadosBasicosNota(nota);
            PreencherNotaComTipoNota(nota, TipoMovimentacao1);
            PreencherNotaComOrdemServico(nota, ordemServico);

            foreach (var os in ordensServicoFaturar)
            {
                IncluirProdutosNota(nota, os);
                IncluirServicosNota(nota, os);
                nota.ValorDesconto += os.ProdutoDescontoValor;
            }
            SetTotaisNota(nota);
            notasGeradas.Add(nota);
        }

        private void GerarDuasNotas()
        {
            var notaProduto = new Modelo.Nota();
            var notaServico = new Modelo.Nota();
            var ordemServico = ordensServicoFaturar.First();
            PreencherDadosBasicosNota(notaProduto);
            PreencherNotaComTipoNota(notaProduto, TipoMovimentacao1);
            PreencherNotaComOrdemServico(notaProduto, ordemServico);

            PreencherDadosBasicosNota(notaServico);
            PreencherNotaComTipoNota(notaServico, TipoMovimentacao2);
            PreencherNotaComOrdemServico(notaServico, ordemServico);

            // Nota de serviço e nota de produtos, devem ser numerações diferentes. Não devem seguir numerações iguais.
            if (notaServico.Serie == notaProduto.Serie)
            {
                if (notaServico.Numero == notaProduto.Numero)
                    notaServico.Numero++;
            }

            foreach (var os in ordensServicoFaturar)
            {
                IncluirProdutosNota(notaProduto, os);
                IncluirServicosNota(notaServico, os);
                notaProduto.ValorDesconto += os.ProdutoDescontoValor;
            }
            SetTotaisNota(notaProduto);
            SetTotaisNota(notaServico);

            notasGeradas.Add(notaProduto);
            notasGeradas.Add(notaServico);
        }

        protected override void ValidarDados()
        {
            bool notaConjugada = ConfiguracaoController.Instancia.GetConfiguracao().NotaConjugada;
            bool deveGerarNFe = ConfiguracaoController.Instancia.GetTipoNfe() == Modelo.AdesaoNFe.Total
                                || (Cliente.BOrgaoPublico || Filial.PessoaForaDoEstado(Cliente));
            bool gerarDuasNotas = !notaConjugada || deveGerarNFe;

            StringBuilder mensagemErro = new StringBuilder();

            if (deveGerarNFe)
            {
                if (TipoMovimentacao1.Categoria != Modelo.TipoNota.CategoriaMovimentacao.Produtos)
                    mensagemErro.AppendLine("- A categoria do Tipo de Movimentação 1 deve ser Produtos.");
            }
            else if (notaConjugada)
            {
                if (TipoMovimentacao1.ModeloDocto != Modelo.TipoNota.ModeloDocumento.ModeloUm)
                    mensagemErro.AppendLine("- O modelo de documento do Tipo de Movimentação 1 deve ser 1 (Nota Fiscal).");

                if (TipoMovimentacao1.Categoria != Modelo.TipoNota.CategoriaMovimentacao.ProdutosEServicos)
                    mensagemErro.AppendLine("- A categoria do Tipo de Movimentação 1 deve ser Produtos e Serviços.");
            }

            if (gerarDuasNotas)
            {
                if (TipoMovimentacao2 == null)
                    mensagemErro.AppendLine("- Selecione o Tipo de Movimentação 2.");
                else
                {
                    if (TipoMovimentacao2.Categoria != Modelo.TipoNota.CategoriaMovimentacao.Servicos)
                        mensagemErro.AppendLine("- A categoria do Tipo de Movimentação 2 deve ser Serviços.");
                }
            }

            if (mensagemErro.Length > 0)
                throw new Exception("Verificar anomalias: " + Environment.NewLine + mensagemErro);
        }
    }
}
