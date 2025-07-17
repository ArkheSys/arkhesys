using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Relatorios
{
    public class FluxoDeCaixa
    {
        private DateTime dataInicial, dataFinal;
        private decimal valorInformado;
        private ConsiderarPedidos filtroPedidos;
        private IList<Modelo.Banco> bancos;
        private IList<Modelo.Proxy.pxyRelFluxoCaixa> pedidoParcelas;
        private IList<Modelo.Documento> documentos;
        private Dictionary<DateTime, pxyRelFluxoCaixa> itemsRelatorioResumido;
        private List<pxyRelFluxoCaixa> itemsRelatorioDetalhado;
        public IList<Modelo.Proxy.pxySaldoBanco> SaldoBancos { get; private set; }
        public decimal SaldoInicial
        {
            get
            {
                if (SaldoBancos != null && SaldoBancos.Count > 0)
                    return SaldoBancos.Sum(s => s.Saldo) + valorInformado;
                else
                    return 0m;
            }
        }

        public FluxoDeCaixa()
        {
            filtroPedidos = ConsiderarPedidos.Ambos;
        }

        public void GerarDados(DateTime dataInicial, DateTime dataFinal, decimal valorInformado, IList<Modelo.Banco> bancos, ConsiderarPedidos filtroPedidos)
        {
            SetParametros(dataInicial, dataFinal, valorInformado, bancos, filtroPedidos);
            GetSaldoBancos();
            GetPedidoParcelas();
            GetDocumentos();
        }

        private void SetParametros(DateTime dataInicial, DateTime dataFinal, decimal valorInformado, IList<Modelo.Banco> bancos, ConsiderarPedidos filtroPedidos)
        {
            this.dataInicial = dataInicial;
            this.dataFinal = dataFinal;
            this.valorInformado = valorInformado;
            this.bancos = bancos;
            this.filtroPedidos = filtroPedidos;
        }

        private void GetSaldoBancos()
        {
            SaldoBancos = Negocio.MovCaixaController.Instancia.GetSaldoBancos(bancos, dataInicial);
        }

        private void GetDocumentos()
        {
            documentos = Negocio.DocumentoController.Instancia.GetAllPossuiSaldoPorPeriodo(dataInicial, dataFinal);
        }

        private void GetPedidoParcelas()
        {
            pedidoParcelas = Negocio.PedidoParcelaController.Instancia.GetItemsFluxoDeCaixa(dataInicial, dataFinal, filtroPedidos);
        }

        #region Resumido

        public IList<pxyRelFluxoCaixa> GetDadosResumido()
        {
            InicializaItemsRelatorioResumido();
            TotalizaPedidoParcelasResumido();
            TotalizaDocumentosResumido();
            TotalizaAcumuladosResumido();
            return itemsRelatorioResumido.Values.OrderBy(i=> i.Data).ToList();
        }

        private void InicializaItemsRelatorioResumido()
        {
            itemsRelatorioResumido = new Dictionary<DateTime, pxyRelFluxoCaixa>();
        }

        private void TotalizaPedidoParcelasResumido()
        {
            pxyRelFluxoCaixa dados;
            foreach (var item in pedidoParcelas.OrderBy(p => p.Data))
            {
                dados = GetItemRelatorioResumido(itemsRelatorioResumido, item.Data);
                dados.Pagar += item.Pagar;
                dados.Receber += item.Receber;
                itemsRelatorioResumido[item.Data] = dados;
            }
        }

        private void TotalizaDocumentosResumido()
        {
            pxyRelFluxoCaixa dados;
            foreach (var item in documentos.OrderBy(p => p.DtVencimento))
            {
                dados = GetItemRelatorioResumido(itemsRelatorioResumido, item.DtVencimento);
                if (item.Tipo == Modelo.TipoDocumentoType.Pagar)
                    dados.Pagar += item.Saldo;
                else
                    dados.Receber += item.Saldo;
                itemsRelatorioResumido[item.DtVencimento] = dados;
            }
        }

        private void TotalizaAcumuladosResumido()
        {
            DateTime dataAnt = DateTime.MinValue;
            foreach (var data in itemsRelatorioResumido.Keys.OrderBy(d => d))
            {
                var dados = itemsRelatorioResumido[data];
                if (dataAnt == DateTime.MinValue)
                    dados.Acumulado = SaldoInicial;
                else
                    dados.Acumulado = itemsRelatorioResumido[dataAnt].Acumulado;
                dados.Acumulado += dados.Receber - dados.Pagar;
                itemsRelatorioResumido[data] = dados;
                dataAnt = data;
            }
        }

        private pxyRelFluxoCaixa GetItemRelatorioResumido(Dictionary<DateTime, pxyRelFluxoCaixa> itemsRelatorio, DateTime data)
        {
            pxyRelFluxoCaixa dados;
            if (!itemsRelatorio.ContainsKey(data))
            {
                dados = new pxyRelFluxoCaixa() { Data = data };
                itemsRelatorio.Add(data, dados);
            }
            else
                dados = itemsRelatorio[data];
            return dados;
        }

        #endregion

        #region Detalhado

        public IList<pxyRelFluxoCaixa> GetDadosDetalhado()
        {
            InicializaItemsRelatorioDetalhado();
            TotalizaPedidoParcelasDetalhado();
            TotalizaDocumentosDetalhado();
            TotalizaAcumuladosDetalhado();
            return itemsRelatorioDetalhado;
        }

        private void InicializaItemsRelatorioDetalhado()
        {
            itemsRelatorioDetalhado = new List<pxyRelFluxoCaixa>();
        }

        private void TotalizaPedidoParcelasDetalhado()
        {
            itemsRelatorioDetalhado.AddRange(pedidoParcelas);
        }

        private void TotalizaDocumentosDetalhado()
        {
            foreach (var item in documentos.OrderBy(p => p.DtVencimento))
            {
                var dados = new pxyRelFluxoCaixa()
                {
                    Data = item.DtVencimento.Date,
                    Pessoa = item.Pessoa.Nome,
                    Numero = item.NumDocumento,
                    Situacao_Parcela = item.Situacao + " / " + item.Parcela + " de " + item.QtParcela,
                    Tipo = "Documento",
                };
                if (item.Tipo == Modelo.TipoDocumentoType.Pagar)
                    dados.Pagar += item.Saldo;
                else
                    dados.Receber += item.Saldo;
                itemsRelatorioDetalhado.Add(dados);
            }
        }

        private void TotalizaAcumuladosDetalhado()
        {
            decimal acumulado = SaldoInicial;
            pxyRelFluxoCaixa itemAnt = null;
            itemsRelatorioDetalhado = itemsRelatorioDetalhado.OrderBy(d => d.Data).ToList();
            foreach (var item in itemsRelatorioDetalhado)
            {
                if (itemAnt != null && itemAnt.Data != item.Data)
                    itemAnt.Acumulado = acumulado;
                acumulado += item.Receber - item.Pagar;
                itemAnt = item;
            }
            if (itemAnt != null)
                itemAnt.Acumulado = acumulado;
        }

        #endregion        
    }
}
