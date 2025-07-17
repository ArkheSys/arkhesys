using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using Cwork.Utilitarios.Tributacao.Servico;

namespace cwkGestao.Negocio.Tributacao
{
    public class TributacaoServico
    {
        private Nota nota;
        private readonly List<Cwork.Utilitarios.Tributacao.Servico.Imposto> impostos;
        private readonly List<NotaImpostoServico> impostosQuePrecisamSerAtualizados;

        public List<NotaImpostoServico> ImpostosQuePrecisamSerAtualizados { get { return impostosQuePrecisamSerAtualizados.Distinct().ToList(); } }
        public decimal ValorRetido { get; private set; }

        public TributacaoServico(Nota _nota)
        {
            nota = _nota;
            impostos = new List<Cwork.Utilitarios.Tributacao.Servico.Imposto>();
            impostosQuePrecisamSerAtualizados = new List<NotaImpostoServico>();
        }

        public void AtualizarImpostosServico()
        {
            DefinirImpostos();
            CalcularImpostos();
        }

        private void DefinirImpostos()
        {
            impostos.Clear();
            foreach (var item in nota.NotaItemsServicos)
            {
                foreach (var i in item.Servico.ServicoImpostos)
                {
                    Cwork.Utilitarios.Tributacao.Servico.Imposto imposto = BuscarImpostoNota(i, item);

                    if (imposto.BaseCalculo == 0)
                        imposto.BaseCalculo = item.SubTotal;
                }
            }
        }

        private void CalcularImpostos()
        {
            var calculador = new CalculadorImpostosServico();
            calculador.SetImpostos(impostos);
            var resultado = calculador.CalcularImpostos();
            ValorRetido = resultado.ValorRetidoTotal;
            IncluirImpostosNaNota(resultado);
        }

        private void IncluirImpostosNaNota(ResultadoCalculoNota resultado)
        {
            nota.NotaImpostoServicos.Clear();
            impostosQuePrecisamSerAtualizados.Clear();
            foreach (var item in resultado.Impostos)
            {
                nota.NotaImpostoServicos.Add(new NotaImpostoServico()
                {
                    Aliquota = item.Imposto.Aliquto,
                    BaseCalculo = item.Imposto.BaseCalculo,
                    Imposto = ServicoImpostoController.Instancia.LoadObjectById(item.Imposto.Id),
                    Nota = nota,
                    Valor = item.Valor,
                    BaseRetencaoImposto = item.ValorRetido > 0,
                });

            }
        }

        private Cwork.Utilitarios.Tributacao.Servico.Imposto BuscarImpostoNota(ServicoServicoImposto servicoImposto, NotaItemServico impNota)
        {
            var imposto = impostos.Where(i => i.Id == servicoImposto.ServicoImposto.ID).FirstOrDefault();
            if (imposto == null)
            {
                imposto = new Cwork.Utilitarios.Tributacao.Servico.Imposto();
                imposto.Id = servicoImposto.ServicoImposto.ID;
                imposto.TipoRetencao = (Cwork.Utilitarios.Tributacao.Servico.TipoRetencao)servicoImposto.ServicoImposto.TipoRetencao;
                imposto.LimiarMes = servicoImposto.ServicoImposto.LimiarPorMes;
                imposto.Aliquto = servicoImposto.ServicoImposto.Aliquota;
                imposto.ValorMinimo = servicoImposto.ServicoImposto.LimiarPorVenda;
                imposto.ServicoRealizadoCidadeCliente = nota.ServicoRealizadoCidadeCliente;
                imposto.TipoImposto = servicoImposto.ServicoImposto.TipoImposto.ID;
                imposto.IssRetido = servicoImposto.BaseRetencaoImposto;
                impostos.Add(imposto);

                if (servicoImposto.ServicoImposto.TipoRetencao == cwkGestao.Modelo.TipoRetencao.LimiarMes)
                {
                    IList<Nota> notasAfetadas = NotaController.Instancia.GetNotasPorImpostoEData(servicoImposto.ServicoImposto, nota);
                    bool jaExisteNotaLimearMes = notasAfetadas.Where(n => n.NotaLimearMes).Count() > 0;
                    imposto.ValorTotalNotasLimearMes = GetBaseCalculoNotasAfetadas(servicoImposto.ServicoImposto, notasAfetadas) + impNota.SubTotal;

                    if (imposto.ValorTotalNotasLimearMes > imposto.LimiarMes && !jaExisteNotaLimearMes)
                    {
                        imposto.BaseCalculo = imposto.ValorTotalNotasLimearMes;
                        impNota.Nota.NotaLimearMes = true;
                    }
                }
            }
            return imposto;
        }

        private decimal GetBaseCalculoNotasAfetadas(ServicoImposto imposto, IList<Nota> notasAfetadas)
        {
            decimal retorno = 0;
            foreach (var nota in notasAfetadas)
            {
                if (nota.NotaImpostoServicos.Where(n => n.Imposto.ID == imposto.ID).Count() > 0)
                    retorno += nota.TotalServico;
            }
            return retorno;
        }
    }
}
