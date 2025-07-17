using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using System.Threading;
using System.IO;

namespace CwkGestao.Integracao.PafEcf.Salc.Controle
{
    public static class FaturarPedidoImportado
    {
        public static void FaturarPedido(Pedido p)
        {
            List<Pedido> lPedido = new List<Pedido>();
            List<FinanceiroParcela> lParcelas = new List<FinanceiroParcela>();
            DateTime dataFaturamento = DateTime.Now;
            Condicao condicao = p.Condicao;
            TipoNota tNota = ConfiguracaoController.Instancia.GetConfiguracao().TipoNotaCupom;
            string Obs1 = "";
            string Obs2 = "";
            string Obs3 = "";
            PessoaEndereco endEntrega = p.Pessoa.PessoaEnderecos.First();
            List<cwkGestao.Modelo.Proxy.pxyFormaPagamentoBaixaDoc> lPxy = new List<cwkGestao.Modelo.Proxy.pxyFormaPagamentoBaixaDoc>();

            lPedido.Add(p);

            p = PedidoController.Instancia.LoadObjectById(p.ID);


            foreach (var item in p.Parcelas)
            {
                FinanceiroParcela parcela = new FinanceiroParcela();
                parcela.Banco = tNota.Banco;
                parcela.BEntrada = item.BEntrada;
                parcela.CpfCnpj = "";
                parcela.DtContabil = p.Dt;
                parcela.Emitente = "";
                parcela.NomeBanco = "";
                parcela.Parcela = 1;
                parcela.TipoDocumento = item.TipoDocumento;
                parcela.Vencimento = item.Vencimento;
                parcela.NumAgencia = "";
                parcela.NumBanco = "";
                parcela.NumCheque = "";
                parcela.NumContaCorrente = "";
                parcela.Valor = item.Valor;

                lParcelas.Add(parcela);
            }

            foreach (var item in lParcelas)
            {
                cwkGestao.Modelo.Proxy.pxyFormaPagamentoBaixaDoc pxy = new cwkGestao.Modelo.Proxy.pxyFormaPagamentoBaixaDoc();
                pxy.Agencia = item.NumAgencia;
                pxy.Banco = "";
                pxy.Cheque = new Cheque();
                pxy.ChequeTerceiro = false;
                pxy.CNPJCPFEmitente = item.CpfCnpj;
                pxy.ContaCorrente = item.NumContaCorrente;
                pxy.Data = item.DtContabil;
                pxy.FormaPagamento = item.TipoDocumento.FormaPagamento;
                pxy.NomeEmitente = item.Emitente;
                pxy.NumeroCheque = item.NumCheque;
                pxy.Valor = item.Valor;

                lPxy.Add(pxy);
            }

            var nota = new cwkGestao.Negocio.Faturamento.Orcamento(lPedido, lParcelas, dataFaturamento, condicao, tNota, Obs1, Obs2, Obs3, endEntrega, lPxy, false, false, false, false, null);
        }

        public static void FaturarPedidoPaf(object source, FileSystemEventArgs e)
        {
            ((FileSystemWatcher)source).EnableRaisingEvents = false;

            Action met = () => CwkGestao.Integracao.PafEcf.Salc.Arquivos_de_Texto.TextReader.LerRetornoIdPedido();
            Thread t = new Thread(new ThreadStart(met));
            Thread.Sleep(500);
            t.Start();

            ((FileSystemWatcher)source).EnableRaisingEvents = true;
        }

        public static void CancelarPedido(Pedido p)
        {
            
        }
    }
}

