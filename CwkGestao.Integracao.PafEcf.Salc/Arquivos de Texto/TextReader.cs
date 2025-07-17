using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using CwkGestao.Integracao.PafEcf.Salc.Controle;
using cwkGestao.Negocio;
using System.IO;
using System.Threading;

namespace CwkGestao.Integracao.PafEcf.Salc.Arquivos_de_Texto
{
    public static class TextReader
    {
        public static void LerRetornoIdPedido()
        {
            string primeiraLinha = "";
            string idPedido = "";
            string numCupomFiscal = "";

           //14 N 2 Código da forma de pagamento utilizada na emissão do cupom.
            Condicao formaPagamento = new Condicao();
            string codigoCondicao;
            

            string caminhoArquivo = ConfiguracaoController.Instancia.GetConfiguracao().DiretorioSalc + @"\RESP";

            DirectoryInfo Dir = new DirectoryInfo(caminhoArquivo);
            FileInfo[] Files = Dir.GetFiles();

            foreach (FileInfo file in Files)
            {
                if (file.FullName.Contains(".ECF"))
                {
                    StreamReader reader = file.OpenText();
                    primeiraLinha = reader.ReadLine();
                    idPedido = primeiraLinha.Substring(25, 10);
                    numCupomFiscal = primeiraLinha.Substring(49, 6);
                    
                    string c = "";
                    string linha = "";
                   
                    try
                    {
                        while (c != "2")
                        {
                            linha = reader.ReadLine();
                            c = linha.Substring(0, 1);                            
                           
                        }
                        codigoCondicao = linha.Substring(113, 2);
                    } 
                    catch (Exception)
                    {
                        codigoCondicao = "0";
                    }                   

                    reader.Close();

                    try
                    {
                        Pedido p = cwkGestao.Negocio.PedidoController.Instancia.GetByCodigo(Int32.Parse(idPedido));
                        p.Condicao = CondicaoController.Instancia.LoadObjectById(p.Condicao.ID);
                        p.NumeroCupomFiscal = Int32.Parse(numCupomFiscal);
                        if (p.Condicao.Codigo != Int32.Parse(codigoCondicao) &&
                            (Int32.Parse(codigoCondicao) != 0))
                        {
                            p.Parcelas = PedidoParcelaController.Instancia.GetParcelasPedido(p);
                            foreach (var item in p.Parcelas)
                            {
                                PedidoParcelaController.Instancia.Salvar(item, Acao.Excluir);
                            }
                            p.Condicao = CondicaoController.Instancia.GetByCodigo(Int32.Parse(codigoCondicao));
                            p.CalculaTotalProdutos();
                            p.CalculaTotalPedido(false, 0);
                            p.Parcelas = CondicaoController.Instancia.GerarParcelas(p);
                        }
                        p.Status = StatusOrcamento.Faturado;
                        PedidoController.Instancia.Salvar(p, Acao.Alterar);

                        try
                        {
                            FaturarPedidoImportado.FaturarPedido(p);
                            MoverArquivoFaturado(file.FullName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            p.Status = StatusOrcamento.Aberto;
                            PedidoController.Instancia.Salvar(p, Acao.Alterar);
                        }
                    }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                    catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                    {
                    }
                }
            }

            Dir = new DirectoryInfo(caminhoArquivo);
            Files = Dir.GetFiles();

            foreach (FileInfo file in Files)
            {
                if (file.FullName.Contains(".DEL"))
                {
                    StreamReader reader = file.OpenText();
                    primeiraLinha = reader.ReadLine();
                    string numeroCompleto = primeiraLinha.Substring(1, 10);
                    string numero = numeroCompleto.Substring(4, 6);
                    reader.Close();
                    try
                    {
                        Nota nota = NotaController.Instancia.GetByNumeroETipoNota(Int32.Parse(numero), 33);
                        nota = NotaController.Instancia.LoadObjectById(nota.ID);
                        foreach (var item in nota.Documentos)
                        {
                            int tamanho = item.Movimentos.Count;
                            for (int i = 0; i < tamanho; i++)
                            {
                                if (item.Movimentos[i].Operacao == OperacaoMovimentoType.BxTotal)
                                {
                                    MovimentoController.Instancia.Salvar(item.Movimentos[i], Acao.Excluir);
                                    item.Movimentos.Remove(item.Movimentos[i]);
                                }
                            }
                        }

                        foreach (var item in nota.Documentos)
                        {
                            item.RecalcularSaldoESituacao();
                            DocumentoController.Instancia.Salvar(item, Acao.Alterar);
                        }

                        NotaController.Instancia.CancelarNF(nota, "Cancelamento de Cupom Fiscal. Integração SALC");
                        MoverArquivoFaturado(file.FullName);
                    }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                    catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                    {
                    }
                }
            }
        }

        public static void MoverArquivoFaturado(string caminho)
        {
            string sourceFile = caminho;
            string destinationFile = "";
            if (caminho.Contains(".DEL"))
            {
                destinationFile = ConfiguracaoController.Instancia.GetConfiguracao().DiretorioSalc + @"\BAIX\" + DateTime.Now.Day + DateTime.Now.Month
                    + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Second + DateTime.Now.Millisecond + ".DEL";
            }
            else
            {
                destinationFile = ConfiguracaoController.Instancia.GetConfiguracao().DiretorioSalc + @"\BAIX\" + DateTime.Now.Day + DateTime.Now.Month
                    + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Second + DateTime.Now.Millisecond + ".ECF";
            }
            // To move a file or folder to a new location:
            System.IO.File.Move(sourceFile, destinationFile);
        }
    }
}
