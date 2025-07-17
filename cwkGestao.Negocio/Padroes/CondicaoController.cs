using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

using static cwkGestao.Modelo.ParcelaBase;

namespace cwkGestao.Negocio
{
    public partial class CondicaoController : BaseController<Condicao>, IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;

        #region Singleton
        private static CondicaoController instance;
        private static IRepositorioCondicao repositorioCondicao;

        private CondicaoController()
        { }

        static CondicaoController()
        {
            instance = new CondicaoController();
            repositorioT = RepositorioFactory<Condicao>.GetRepositorio();
            repositorioCondicao = (IRepositorioCondicao)repositorioT;
        }

        public static CondicaoController Instancia
        {
            get
            {
                return instance;
            }
        }


        #endregion

#pragma warning disable CS0414 // O campo "CondicaoController.temContrato" é atribuído, mas seu valor nunca é usado
        private bool temContrato = false;
#pragma warning restore CS0414 // O campo "CondicaoController.temContrato" é atribuído, mas seu valor nunca é usado
        private bool faturaMesSeguinte = false;
        private Contrato contrato = new Contrato();
        private ContratoServico servicoContrato = new ContratoServico();
        private bool ehServico = false;


        private DateTime referenciaFaturamento { get; set; }

        public override Dictionary<string, string> ValidaObjeto(Condicao objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objeto.Codigo <= 0)
                ret.Add("txtCodigo", "Valor tem que ser maior que 0 (Zero).");

            if (String.IsNullOrEmpty(objeto.Nome) || objeto.Nome.Trim() == String.Empty)
                ret.Add("txtNome", "Campo obrigatório.");
            if (objeto.CondicaoParcelas.Count <= 0)
                ret.Add("gridCondicaoParcelas", "Preenchar pelo menos uma parcela.");

            return ret;
        }

        public void GetContrato(Contrato pContrato)
        {
            contrato = pContrato;
            temContrato = true;
        }

        public void GetContratoServico(ContratoServico contratoServico)
        {
            servicoContrato = contratoServico;
            ehServico = true;
        }

        public void GetDadosFaturamento(DateTime referencia, bool diav, DateTime dataVenc)
        {
            if (diav == false)
            {
                referenciaFaturamento = referencia;
            }
            else
            {
                referenciaFaturamento = dataVenc;
            }
        }

        public void GetDadosFaturamento(DateTime referencia)
        {
            referenciaFaturamento = referencia;
        }

        public Condicao GetByCodigo(int codigo)
        {
            return repositorioCondicao.GetByCodigo(codigo);
        }

        public Condicao GetAVista(int id)
        {
            return repositorioCondicao.GetAVista(id);
        }

        public void AtualizaParcelas(Pedido pedido)
        {
            List<int> indicesAtualizadosManualmente = new List<int>();
            decimal valorAtualizadoManualmente = 0;
            DateTime vencimentoAlterado = DateTime.Today;
            DateTime previsaoAlterada = DateTime.Today;
            for (int i = 0; i < pedido.Parcelas.Count; i++)
            {
                if (pedido.Parcelas[i].BAlterado)
                {
                    indicesAtualizadosManualmente.Add(i);
                    valorAtualizadoManualmente += pedido.Parcelas[i].Valor;
                    vencimentoAlterado = pedido.Parcelas[i].Vencimento;
                    previsaoAlterada = pedido.Parcelas[i].Previsao;
                }
            }

            Condicao condicao = CondicaoController.Instancia.LoadObjectById(pedido.Condicao.ID);
            //Ordena as parcelas dentro da condição
            condicao.CondicaoParcelas = condicao.CondicaoParcelas.OrderBy(o => o.Parcela).ToList();
            if (condicao.CondicaoParcelas.Count >= 1)
            {
                if (condicao.CondicaoParcelas.Count > 1)
                {
                    indicesAtualizadosManualmente.Reverse();
                    foreach (var item in indicesAtualizadosManualmente)
                    {
                        condicao.QtParcela--;
                        condicao.CondicaoParcelas.RemoveAt(item);
                    }
                }

                if (pedido.TotalPedido == 0)
                {
                    pedido.Parcelas.Clear();
                }
                if ((pedido.TotalPedido - valorAtualizadoManualmente) != 0)
                {
                    DateTime dataBase = pedido.TipoPedido.BaseCalculoParcelas == TipoPedido.BaseCalculoParcela.Data ? pedido.Dt : pedido.DtPrazoEntrega;

                    decimal totalST = 0m;

                    if (pedido.TipoPedido != null && TipoNotaController.Instancia.LoadObjectById(pedido.TipoPedido.TipoNota.ID).ValorSTPrimeiraParcela)
                    {
                        totalST = pedido.TotalST;
                    }

                    List<PedidoParcela> listaNova = new List<PedidoParcela>();
                    if (pedido.Parcelas.Count > 1)
                        listaNova = GerarParcelasPedido<PedidoParcela>(condicao, (pedido.TotalPedido - valorAtualizadoManualmente - totalST), dataBase);
                    else
                        listaNova = GerarParcelasPedido<PedidoParcela>(condicao, (pedido.TotalPedido - totalST), vencimentoAlterado);

                    for (int i = 0; i < pedido.Parcelas.Count; i++)
                    {
                        if (!pedido.Parcelas[i].BAlterado || (condicao.CondicaoParcelas.Count == 1 && pedido.Parcelas.Count == 1))
                        {
                            var parcAlterada = pedido.Parcelas[i].BAlterado;
                            var venc = pedido.Parcelas[i].Vencimento;

                            pedido.Parcelas[i] = listaNova[0];
                            pedido.Parcelas[i].BAlterado = parcAlterada;

                            if (parcAlterada)
                                pedido.Parcelas[i].Vencimento = venc;

                            listaNova.RemoveAt(0);
                        }

                        if (i == 0)
                            pedido.Parcelas[i].Valor += totalST;

                        pedido.Parcelas[i].Parcela = i + 1;
                        pedido.Parcelas[i].Pedido = pedido;
                    }
                }
            }
        }

        public void AtualizaParcelas<T>(IList<T> parcelas, Condicao condicao, DateTime dataBase, decimal total) where T : Modelo.ParcelaBase, new()
        {
            //Ordena as parcelas dentro da condição
            condicao.CondicaoParcelas = condicao.CondicaoParcelas.OrderBy(o => o.Parcela).ToList();

            List<int> indicesAtualizadosManualmente = new List<int>();

            decimal valorAtualizadoManualmente = 0;
            for (int i = 0; i < parcelas.Count; i++)
            {
                if (parcelas[i].BAlterado)
                {
                    indicesAtualizadosManualmente.Add(i);
                    valorAtualizadoManualmente += parcelas[i].Valor;
                }
            }

            if (condicao.CondicaoParcelas.Count > 1 || (condicao.CondicaoParcelas.Count == 1 && !parcelas[0].BAlterado))
            {
                indicesAtualizadosManualmente.Reverse();
                foreach (var item in indicesAtualizadosManualmente)
                {
                    condicao.QtParcela--;
                    condicao.CondicaoParcelas.RemoveAt(item);
                }

                if (total - valorAtualizadoManualmente > 0)
                {

                    List<T> listaNova = GerarParcelasPedido<T>(condicao, total - valorAtualizadoManualmente, dataBase);

                    for (int i = 0; i < parcelas.Count; i++)
                    {
                        if (!parcelas[i].BAlterado)
                        {
                            parcelas[i] = listaNova[0];
                            listaNova.RemoveAt(0);
                        }
                        parcelas[i].Parcela = i + 1;
                    }
                }
            }
        }

        public List<PedidoParcela> GerarParcelas(Pedido pedido)
        {
            DateTime dataBase = pedido.TipoPedido.BaseCalculoParcelas == TipoPedido.BaseCalculoParcela.Data ? pedido.Dt : pedido.DtPrazoEntrega;

            decimal totalST = 0m;

            if (pedido.TipoPedido != null)
            {
                var tipoNota = pedido.TipoPedido.TipoNota != null ? pedido.TipoPedido.TipoNota : TipoNotaController.Instancia.LoadObjectById(pedido.TipoPedido.TipoNota.ID);
                if (tipoNota.ValorSTPrimeiraParcela)
                    totalST = pedido.TotalST;
            }

            //if (pedido.TipoPedido != null && TipoNotaController.Instancia.LoadObjectById(pedido.TipoPedido.TipoNota.ID).ValorSTPrimeiraParcela)
            //{
            //    totalST = pedido.TotalST;
            //}

            List<PedidoParcela> parcelas = GerarParcelasPedido<PedidoParcela>(pedido.Condicao, (pedido.TotalPedido - totalST), dataBase);

            for (int i = 0; i < parcelas.Count; i++)
            {
                parcelas[i].Pedido = pedido;

                if (i == 0)
                    parcelas[i].Valor += totalST;
            }

            return parcelas;
        }


        public List<T> GerarParcelasPedido<T>(Condicao condicao, Decimal valor, DateTime inicio) where T : Modelo.ParcelaBase, new()
        {
            List<T> parcelas = new List<T>();
            try
            {
                if (valor == 0 || condicao == null || inicio == null)
                    throw new ArgumentException();

                IList<CondicaoParcela> condparcelas = condicao.CondicaoParcelas.ToList();
                if (condparcelas != null)
                {
                    condparcelas = condparcelas.Where(parc => parc.TipoVlr == "Valor").ToList();

                    decimal totalFixo = condparcelas.Sum(parc => parc.VlrPerc);
                    if (totalFixo >= valor)
                        throw new InvalidOperationException("Valor das parcelas excede o valor a ser pago.");

                    decimal total = Math.Round(valor - totalFixo, 2);

                    IList<Feriado> feriados = FeriadoController.Instancia.GetAll();
                    int parcelaNumero = 1;
                    var condparcs = condicao.CondicaoParcelas.OrderBy(cond => cond.DiaMes);
                    foreach (CondicaoParcela parcela in condparcs)
                    {
                        DateTime vencimento = inicio;
                        T pedParcela = new T();
                        pedParcela.Parcela = parcelaNumero++;
                        pedParcela.TipoDocumento = parcela.TipoDocumento;
                        pedParcela.BEntrada = parcela.BAVista;
                        pedParcela.Previsao = vencimento;

                        /* Foletto */
                        var Forma = ConversorFormaPagamento.GetFormaPagamento(parcela.TipoDocumento.FormaPagamento.CodigoSefaz);

                        pedParcela.FormaPagamento = Forma;
                        pedParcela.TipoFormaPagamento = ConversorFormaPagamento.GetFormaPagamento(Forma);

                        if (parcela.TipoDt == "Dia")
                            vencimento = vencimento.AddDays(parcela.DiaMes);
                        else
                            vencimento = vencimento.AddMonths(parcela.DiaMes);

                        while (condicao.VencimentoFeriado != VencimentoFeriado.Nada && (vencimento.DayOfWeek == DayOfWeek.Saturday || vencimento.DayOfWeek == DayOfWeek.Sunday || feriados.Where(feriado => feriado.Data.Day == vencimento.Day && feriado.Data.Month == vencimento.Month).Count() > 0))
                            switch (condicao.VencimentoFeriado)
                            {
                                case VencimentoFeriado.Adiantar:
                                    vencimento = vencimento.AddDays(-1);
                                    break;
                                case VencimentoFeriado.Adiar:
                                    vencimento = vencimento.AddDays(1);
                                    break;
                            }

                        pedParcela.Vencimento = vencimento;

                        if (condicao.Divide.Value == 1)
                        {
                            if (condicao.QtParcela > 0)
                                pedParcela.Valor = Math.Round((total / condicao.QtParcela), 2);
                            else
                                throw new InvalidOperationException("Condição não especifica quantidade de parcelas");
                        }
                        else
                        {
                            if (parcela.TipoVlr == "Percentual")
                                pedParcela.Valor = Math.Round(total * parcela.VlrPerc / 100, 2);
                            else
                                pedParcela.Valor = parcela.VlrPerc;
                        }
                        pedParcela.TipoDocumento = parcela.TipoDocumento;
                        parcelas.Add(pedParcela);

                    }
                    if (parcelas.Count > 0)
                        parcelas[0].Valor += valor - parcelas.Sum(parc => parc.Valor);
                }
            }
            catch (Exception z)
            {
                throw z;
            }

            return parcelas;
        }

        public void AtualizaParcelasNota(Nota nota)
        {
            List<int> parcelasAtualizadasManualmente = new List<int>();
            decimal valorAtualizadoManualmente = 0;
            for (int i = 0; i < nota.NotaParcelas.Count; i++)
            {
                if (nota.NotaParcelas[i].BAlterado)
                {
                    parcelasAtualizadasManualmente.Add(nota.NotaParcelas[i].Parcela);
                    valorAtualizadoManualmente += nota.NotaParcelas[i].Valor;
                }
            }

            Condicao condicao = CondicaoController.Instancia.LoadObjectById(nota.Condicao.ID);
            //Ordena as parcelas dentro da condição
            condicao.CondicaoParcelas = condicao.CondicaoParcelas.OrderBy(o => o.Parcela).ToList();

            if (condicao.CondicaoParcelas.Count > 1 || (condicao.CondicaoParcelas.Count == 1 && !nota.NotaParcelas[0].BAlterado))
            {
                foreach (var item in parcelasAtualizadasManualmente)
                {
                    var parc = condicao.CondicaoParcelas.Where(c => c.Parcela == item).FirstOrDefault();
                    if (parc != null)
                        condicao.CondicaoParcelas.Remove(parc);
                }
                condicao.QtParcela = condicao.CondicaoParcelas.Count;

                if (nota.TotalNota - valorAtualizadoManualmente > 0)
                {
                    DateTime dataBase = nota.Dt;
                    List<NotaParcela> listaNova = GerarParcelasNota(condicao, nota.TotalNota - valorAtualizadoManualmente, dataBase);

                    if (listaNova.Count == nota.NotaParcelas.Count)
                        for (int i = 0; i < nota.NotaParcelas.Count; i++)
                        {
                            if (!nota.NotaParcelas[i].BAlterado)
                            {
                                nota.NotaParcelas[i] = listaNova[0];
                                listaNova.RemoveAt(0);
                            }
                            nota.NotaParcelas[i].Parcela = i + 1;
                            nota.NotaParcelas[i].Nota = nota;
                        }
                }
            }
        }

        public List<NotaParcela> GerarParcelas(Nota nota)
        {
            decimal totalST = 0m;
            if (nota.TipoNota != null && nota.TipoNota.ValorSTPrimeiraParcela && nota.NotaICMSs.Count > 0)
            {
                if (nota.NotaICMSs.Sum(n => n.ValorSubst) < 0)
                    totalST = 0;
                else
                    totalST = nota.NotaICMSs.Sum(n => n.ValorSubst);
            }
            decimal total = nota.TotalNota - totalST;

            List<NotaParcela> parcelas = GerarParcelasNota(nota.Condicao, total, nota.Dt);

            for (int i = 0; i < parcelas.Count; i++)
            {
                parcelas[i].Nota = nota;

                if (i == 0)
                    parcelas[i].Valor += totalST;
            }

            return parcelas;
        }

        public List<NotaParcela> GerarParcelasNota(Condicao condicao, Decimal valor, DateTime inicio)
        {
            DateTime dataFaturamento;
            if (valor == 0 || condicao == null || inicio == null)
                throw new ArgumentException();

            List<NotaParcela> parcelas = new List<NotaParcela>();

            decimal totalFixo = condicao.CondicaoParcelas.Where(parc => parc.TipoVlr == "Valor").Sum(parc => parc.VlrPerc);
            if (totalFixo > valor)
                throw new InvalidOperationException("Erro na geração de parcelas: Valor das parcelas excede o valor a ser pago.");

            decimal total = Math.Round(valor - totalFixo, 2);
            DateTime vencimento;
            IList<Feriado> feriados = FeriadoController.Instancia.GetAll();
            int parcelaNumero = 1;
            foreach (CondicaoParcela parcela in condicao.CondicaoParcelas.OrderBy(c => c.DiaMes))
            {
                NotaParcela pedParcela = new NotaParcela();
                pedParcela.Parcela = parcelaNumero++;
                pedParcela.TipoDocumento = parcela.TipoDocumento;

                var Forma = ConversorFormaPagamento.GetFormaPagamento(parcela.TipoDocumento.FormaPagamento.CodigoSefaz);
                pedParcela.FormaPagamento = Forma;
                pedParcela.TipoFormaPagamento = ConversorFormaPagamento.GetFormaPagamento(Forma);

                pedParcela.BEntrada = parcela.BAVista;

                if (pedParcela.TipoFormaPagamento == null)
                {
                    pedParcela.FormaPagamento = 0;
                }

                if (parcela.TipoDt == "Dia")
                    vencimento = inicio.AddDays(parcela.DiaMes);
                else
                    vencimento = inicio.AddMonths(parcela.DiaMes);

                while (condicao.VencimentoFeriado != VencimentoFeriado.Nada && (vencimento.DayOfWeek == DayOfWeek.Saturday || vencimento.DayOfWeek == DayOfWeek.Sunday || feriados.Where(feriado => feriado.Data.Day == vencimento.Day && feriado.Data.Month == vencimento.Month).Count() > 0))
                    switch (condicao.VencimentoFeriado)
                    {
                        case VencimentoFeriado.Adiantar:
                            vencimento = vencimento.AddDays(-1);
                            break;
                        case VencimentoFeriado.Adiar:
                            vencimento = vencimento.AddDays(1);
                            break;
                    }

                pedParcela.Vencimento = vencimento;

                if (condicao.Divide.Value == 1)
                {
                    int qtParcelasPct = condicao.QtParcela;
                    if (qtParcelasPct > 0)
                    {
                        pedParcela.Valor = Math.Round((total / qtParcelasPct), 2);
                    }
                    else
                        throw new InvalidOperationException("Erro na geração de parcelas: Condição não especifica quantidade de parcelas");
                }
                else
                {
                    if (parcela.TipoVlr == "Percentual")
                        pedParcela.Valor = Math.Round(total * parcela.VlrPerc / 100, 2);
                    else
                        pedParcela.Valor = parcela.VlrPerc;
                }

                if (ehServico)
                {
                    if (servicoContrato != null)
                    {
                        if (servicoContrato.DiaVencimento != 0)
                        {
                            if (faturaMesSeguinte)
                            {
                                DateTime dataMesSeguinte = referenciaFaturamento.AddMonths(1);
                                dataFaturamento = new DateTime(dataMesSeguinte.Year, dataMesSeguinte.Month, servicoContrato.DiaVencimento);
                            }
                            else
                            {
                                DateTime dataMesSeguinte = referenciaFaturamento;
                                dataFaturamento = new DateTime(dataMesSeguinte.Year, dataMesSeguinte.Month, servicoContrato.DiaVencimento);
                            }
                            pedParcela.Vencimento = dataFaturamento;

                        }
                    }
                }
                else
                {
                    if (parcela.TipoDt == "Dia")
                        pedParcela.Vencimento = inicio.AddDays(parcela.DiaMes);
                    else
                        pedParcela.Vencimento = inicio.AddMonths(parcela.DiaMes);
                }

                if (pedParcela.TipoFormaPagamento == null)
                    pedParcela.TipoFormaPagamento = "01";

                parcelas.Add(pedParcela);

            }

            if (parcelas.Count > 0)
                parcelas[0].Valor += valor - parcelas.Sum(parc => parc.Valor);

            return parcelas;
        }

        public void AtualizaParcelasComJurosCustom(List<PedidoParcela> parcelasOriginais, decimal juros, DateTime dataBase)
        {
            if ((juros > 0) && (parcelasOriginais.Count > 0))
            {
                decimal total = parcelasOriginais.Sum(parc => parc.Valor);
                decimal totalDividido = total / parcelasOriginais.Count;

                foreach (PedidoParcela parcelasOriginal in parcelasOriginais)
                {
                    decimal jurosCalculado = parcelasOriginal.Valor * (juros / 100);
                    parcelasOriginal.Valor = totalDividido + jurosCalculado;

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                instance = new CondicaoController();
            }

            if (disposed)
                return;

            disposed = true;
        }
    }
}