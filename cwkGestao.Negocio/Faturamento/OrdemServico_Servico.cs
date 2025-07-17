using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Faturamento
{
    public class OrdemServico_Servico : OrdemServico
    {
        protected override void GerarNotas()
        {
            if (ordensServicoFaturar.Sum(s => s.OSServicoItems.Count) == 0)
            {
                return;
            }
            var nota = new Modelo.Nota();
            var ordemServico = ordensServicoFaturar.First();
            PreencherDadosBasicosNota(nota);
            PreencherNotaComTipoNota(nota, TipoMovimentacao2);
            PreencherNotaComOrdemServico(nota, ordemServico);

            foreach (var os in ordensServicoFaturar)
            {
                IncluirServicosNota(nota, os);
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
                if (ordensServicoFaturar.Sum(s => s.OSServicoItems.Count) > 0)
                {
                    if (!((TipoMovimentacao2.ModeloDocto == cwkGestao.Modelo.TipoNota.ModeloDocumento.RPS) ||
                         (TipoMovimentacao2.ModeloDocto == cwkGestao.Modelo.TipoNota.ModeloDocumento.NFSe) ||
                         (TipoMovimentacao2.ModeloDocto == cwkGestao.Modelo.TipoNota.ModeloDocumento.Balanco)))
                    {
                        mensagemErro.AppendLine("- O modelo de documento do Tipo de Movimentação 2 deve ser \"RPS\" ou \"NFSe\".");
                    }

                    if (TipoMovimentacao2.Categoria != cwkGestao.Modelo.TipoNota.CategoriaMovimentacao.Servicos)
                    {
                        mensagemErro.AppendLine("- A categoria do Tipo de Movimentação 2 deve ser \"Serviços\".");
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
