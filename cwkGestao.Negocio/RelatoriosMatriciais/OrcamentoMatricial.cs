using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixReporter;
using System.Net.Mail;
using System.Net;

namespace cwkGestao.Negocio.RelatoriosMatriciais
{
    public class OrcamentoMatricial
    {
        public string Email { get; set; }
        public string ObservacaoEmail { get; set; }
        public Modelo.DestinoImpressao DestinoImpressao { get; set; }

        #region Impressão Comum

        public void ImprimePedidoMatricial(Modelo.Pedido objPedido, int itensPag)
        {
            EpsonCodes lCodes = new EpsonCodes();

            Reporter lPrinter = new Reporter();

            Modelo.Impressora objImpressora = objPedido.TipoPedido.Impressora;

            if (objImpressora.Caminho.IndexOf('\\') == -1)
                lPrinter.Output = objImpressora.Caminho;
            else
                lPrinter.Output = @objImpressora.Caminho;

            if (DestinoImpressao != Modelo.DestinoImpressao.Email)
            {
                objPedido.BImpressa = true;
                Negocio.PedidoController.Instancia.Salvar(objPedido, cwkGestao.Modelo.Acao.Alterar);
            }

            if (objPedido.Ent_Sai == 1)
                CriaVendedor(objPedido);

            lPrinter.NomeArquivo = "p" + objPedido.Codigo.ToString() + ".txt";

            lPrinter.StartJob();
            int count = 0;
            int pag = 1;
            ImprimeCabeca(lCodes, lPrinter, objPedido, ref count, Utilitarios.StrNumeroPagina(pag, objPedido.Items.Count, itensPag));

            string aux;
            int cItens = 0;
            foreach (Modelo.PedidoItem p in objPedido.Items)
            {
                lPrinter.PrintText(count, 0, "|" + String.Format("{0,7} ", p.Produto.Codigo.ToString()));
                aux = (p.Produto.Unidade.BQtdFracionada == true ? String.Format("{0:0.00}", p.Quantidade) : String.Format("{0:0}", p.Quantidade));
                lPrinter.PrintText(count, 9, "|" + String.Format("{0,10} ", aux));
                lPrinter.PrintText(count, 21, "| " + p.Unidade);
                lPrinter.PrintText(count, 26, "|");
                lPrinter.PrintText(count, 28, String.Format("{0:58} ", lPrinter.CleanText(p.ProdutoNome)));
                lPrinter.PrintText(count, 87, "|" + String.Format("{0,11} ", String.Format("{0:n}", p.Valor)));
                lPrinter.PrintText(count, 100, "| " + String.Format("{0,10} ", String.Format("{0:n}", p.Total)));
                lPrinter.PrintText(count, 113, "|");
                count++;
                cItens++;

                if (cItens == itensPag && cItens < objPedido.Items.Count)
                {
                    pag++;
                    ImprimeRoda(lCodes, lPrinter, objPedido, false, ref count);
                    lPrinter.PrintText(count++, 0, " ");
                    lPrinter.PrintText(count++, 0, " ");
                    lPrinter.PrintText(count++, 0, " ");
                    ImprimeCabeca(lCodes, lPrinter, objPedido, ref count, Utilitarios.StrNumeroPagina(pag, objPedido.Items.Count, itensPag));
                    cItens = 0;
                }
            }

            ImprimeRoda(lCodes, lPrinter, objPedido, true, ref count);

            if (count < 33 * pag)
            {
                lPrinter.PrintText(count++, 0, lCodes.CondensedOn);
                while (count < 33 * pag)
                {
                    lPrinter.PrintText(count++, 0, " ");
                }
                lPrinter.PrintText(count - 1, 1, lCodes.CondensedOff);
            }

            lPrinter.EndJob();

            FinalizarImpressao(objPedido, lPrinter);
        }

        private void ImprimeCabeca(EpsonCodes lCodes, Reporter lPrinter, Modelo.Pedido objPedido, ref int count, string pag)
        {
            lPrinter.PrintText(count, 1, lCodes.Reset + lCodes.LinesInch6 + DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
            lPrinter.PrintText(count, 30, lCodes.CondensedOn + lCodes.BoldOn + lCodes.ExpandedOn + "ORCAMENTO N. " + objPedido.Codigo.ToString() + lCodes.ExpandedOff + lCodes.BoldOff);

            count += 2;
            String vend = objPedido.Vendedor.Apelido;
            if (vend.Trim().Length == 0) vend = objPedido.Vendedor.Nome;
            lPrinter.PrintText(count, 1, "Vend.:" + lPrinter.CleanText(vend) + " (" + objPedido.Vendedor.Codigo.ToString() + ")");

            lPrinter.PrintText(count++, 106, pag);
            lPrinter.PrintText(count++, 0, "+----------------------------------------------------------------------------------------------------------------+");

            string aux = (objPedido.Pessoa.TipoPessoa == "Física" ? lPrinter.CleanText(objPedido.Pessoa.Apelido) : lPrinter.CleanText(objPedido.Pessoa.Fantasia)) + " (" + objPedido.Pessoa.Codigo.ToString() + ")";
            lPrinter.PrintText(count, 0, "| " + "Fantasia.....:" + lCodes.BoldOn + aux + lCodes.BoldOff);

            lPrinter.PrintText(count++, 117, "|");
            lPrinter.PrintText(count, 0, "| Razao Social.:" + lPrinter.CleanText(objPedido.PessoaNome));

            lPrinter.PrintText(count++, 113, "|");

            if (objPedido.PessoaEndereco != null)
                aux = (objPedido.PessoaEndereco.Length < 50 ? lPrinter.CleanText(objPedido.PessoaEndereco) : lPrinter.CleanText(objPedido.PessoaEndereco.Remove(50))) + ", " + (objPedido.Complemento == null ? "" : ", " + objPedido.Complemento) + " " + (objPedido.PessoaBairro != null ? (objPedido.PessoaBairro.Length <= 20 ? lPrinter.CleanText(objPedido.PessoaBairro) : lPrinter.CleanText(objPedido.PessoaBairro.Remove(20))) : "") + " - " + lPrinter.CleanText(objPedido.PessoaCidade) + ", " + lPrinter.CleanText(objPedido.PessoaUF);
            else
                aux = "";

            lPrinter.PrintText(count, 0, "| Endereco.....:" + aux);
            lPrinter.PrintText(count++, 113, "|");
            lPrinter.PrintText(count, 0, "| CNPJ/CPF.....:" + objPedido.PessoaCNPJCPF);
            lPrinter.PrintText(count, 45, "IE/RG:" + objPedido.PessoaInscRG);
            lPrinter.PrintText(count, 94, "Fone: " + objPedido.PessoaTelefone);
            lPrinter.PrintText(count++, 113, "|");
            lPrinter.PrintText(count++, 0, "+--------+-----------+----+------------------------------------------------------------+------------+------------+");
            lPrinter.PrintText(count++, 0, "|   PROD |     QUANT | UN | DESCRICAO                                                  |   PRECO UN |      TOTAL |");
            lPrinter.PrintText(count++, 0, "+--------+-----------+----+------------------------------------------------------------+------------+------------+");
        }

        private void ImprimeRoda(EpsonCodes lCodes, Reporter lPrinter, Modelo.Pedido objPedido, bool imp, ref int count)
        {
            lPrinter.PrintText(count++, 0, "+--------+-----------+----+------------------------------------------------------------+------------+------------+");
            if (imp)
            {
                lPrinter.PrintText(count, 01, lCodes.BoldOn + "TROCA OU DEVOLUCAO EM ATE 48 HORAS" + lCodes.BoldOff);
                lPrinter.PrintText(count, 88, "TOTAL.......:");
                lPrinter.PrintText(count++, 101, String.Format("{0,11} ", String.Format("{0:n}", objPedido.TotalPedido)) + lCodes.CondensedOff);
            }
            else
            {
                lPrinter.PrintText(count, 88, "TOTAL.......:");
                lPrinter.PrintText(count++, 101, String.Format("{0,11} ", "****"));
                lPrinter.PrintText(count++, 0, "------------------------------------------------------------------------------------------------------------------" + lCodes.CondensedOff);
            }
        }

        #endregion

        #region Impressão de Junção de Pedidos

        public void JuntarPedidos(List<int> pListaIdPedido, Modelo.Impressora objImpressora)
        {
            int cp = 0;
            int tam = 0;

            Modelo.Pedido objPedido = new Modelo.Pedido();

            for (int i = 0; i < pListaIdPedido.Count; i++)
            {
                cp = pListaIdPedido[i];
                Modelo.Pedido p = Negocio.PedidoController.Instancia.LoadObjectById(cp);

                if (i == 0)
                {
                    objPedido.Codigo = Negocio.PedidoController.Instancia.MaxCodigo();
                    objPedido.Condicao = p.Condicao;
                    objPedido.Dt = p.Dt;
                    objPedido.DtDigitacao = p.DtDigitacao;
                    objPedido.DtValidade = p.DtValidade;
                    objPedido.Ent_Sai = p.Ent_Sai;
                    objPedido.Filial = p.Filial;
                    objPedido.Pessoa = p.Pessoa;
                    objPedido.ID = p.ID;
                    objPedido.TabelaPreco = p.TabelaPreco;
                    objPedido.TipoPedido = p.TipoPedido;
                    objPedido.Vendedor = p.Vendedor;
                    objPedido.PessoaBairro = p.PessoaBairro;
                    objPedido.PessoaCidade = p.PessoaCidade;
                    objPedido.PessoaCNPJCPF = p.PessoaCNPJCPF;
                    objPedido.PessoaEndereco = p.PessoaEndereco;
                    objPedido.PessoaInscRG = p.PessoaInscRG;
                    objPedido.PessoaNome = p.PessoaNome;
                    objPedido.PessoaTelefone = p.PessoaTelefone;
                    objPedido.PessoaUF = p.PessoaUF;
                    objPedido.Status = p.Status;
                    objPedido.TabelaPreco = p.TabelaPreco;
                    objPedido.TipoPedido = p.TipoPedido;
                }

                foreach (Modelo.PedidoItem pi in p.Items)
                {
                    Modelo.PedidoItem objPedItem = new Modelo.PedidoItem();

                    objPedItem.bReservaEstoque = pi.bReservaEstoque;
                    objPedItem.Dt = pi.Dt;
                    objPedItem.Ent_Sai = pi.Ent_Sai;
                    objPedItem.Gerente = pi.Gerente;
                    objPedItem.Produto = pi.Produto;
                    objPedItem.Produto = pi.Produto;
                    objPedItem.PercDesconto = pi.PercDesconto;
                    objPedItem.PesoBruto = pi.PesoBruto;
                    objPedItem.PesoLiquido = pi.PesoLiquido;
                    objPedItem.PrazoEntrega = pi.PrazoEntrega;
                    objPedItem.ProdutoDescReduzida = pi.ProdutoDescReduzida;
                    tam = pi.ProdutoNome.TrimEnd().Length;
                    if (tam > 40)
                    {
                        tam = 40;
                    }
                    SetSequencia(objPedido, objPedItem);
                    objPedItem.ProdutoNome = new String(' ', 58);
                    objPedItem.ProdutoNome = objPedItem.ProdutoNome.Insert(0, pi.ProdutoNome.Substring(0, tam));
                    objPedItem.ProdutoNome = objPedItem.ProdutoNome.Insert(41, String.Format("{0:D6}", p.Codigo));
                    objPedItem.ProdutoNome = objPedItem.ProdutoNome.Insert(48, String.Format("{0:dd/MM/yyyy}", p.Dt));
                    objPedItem.ProdutoNome = objPedItem.ProdutoNome.TrimEnd();
                    objPedItem.QtdCancelada = pi.QtdCancelada;
                    objPedItem.QtdRetirada = pi.QtdRetirada;
                    objPedItem.Quantidade = pi.Quantidade;
                    objPedItem.RAT_Acrescimo = pi.RAT_Acrescimo;
                    objPedItem.RAT_Desconto = pi.RAT_Desconto;
                    objPedItem.SubTotal = pi.SubTotal;
                    objPedItem.Total = pi.Total;
                    objPedItem.Unidade = pi.Unidade;
                    objPedItem.Valor = pi.Valor;
                    objPedItem.ValorCalculado = pi.ValorCalculado;
                    objPedItem.ValorDesconto = pi.ValorDesconto;
                    objPedItem.ValorRetidoICMS = pi.ValorRetidoICMS;

                    objPedido.Items.Add(objPedItem);
                }
            }

            objPedido.TotalProduto = objPedido.Items.Sum(x => x.Total);
            objPedido.TotalPedido = objPedido.TotalProduto;
            objPedido.ValorAcrescimo = 0;
            objPedido.ValorDesconto = 0;

            ImprimePedidoJuntamento(objPedido, 46, objImpressora);
        }

        private static void SetSequencia(Modelo.Pedido objPedido, Modelo.PedidoItem objPedItem)
        {
            try
            {
                objPedItem.Sequencia = objPedido.Items.Max(p => p.Sequencia) + 1;
            }
            catch { objPedItem.Sequencia = 1; }
        }

        public void ImprimePedidoJuntamento(Modelo.Pedido objPedido, int itensPag, Modelo.Impressora objImpressora)
        {
            EpsonCodes lCodes = new EpsonCodes();

            Reporter lPrinter = new Reporter();

            if (objImpressora.Caminho.IndexOf('\\') == -1)
                lPrinter.Output = objImpressora.Caminho;
            else
                lPrinter.Output = @objImpressora.Caminho;

            lPrinter.NomeArquivo = "j" + objPedido.Codigo.ToString() + ".txt";

            lPrinter.StartJob();
            int count = 0;
            int pag = 1;
            ImprimeCabecaJuntamento(lCodes, lPrinter, objPedido, ref count, Utilitarios.StrNumeroPagina(pag, objPedido.Items.Count, itensPag));

            string aux;
            int cItens = 0;
            foreach (Modelo.PedidoItem p in objPedido.Items)
            {
                lPrinter.PrintText(count, 0, "|" + String.Format("{0,7} ", p.Produto.Codigo.ToString()));
                aux = (p.Produto.Unidade.BQtdFracionada == true ? p.Quantidade.ToString() : String.Format("{0:0}", p.Quantidade));
                lPrinter.PrintText(count, 9, "|" + String.Format("{0,10} ", aux));
                lPrinter.PrintText(count, 21, "| " + p.Unidade);
                lPrinter.PrintText(count, 26, "|");
                lPrinter.PrintText(count, 28, String.Format("{0:58} ", lPrinter.CleanText(p.ProdutoNome)));
                lPrinter.PrintText(count, 87, "|" + String.Format("{0,11} ", String.Format("{0:n}", p.Valor)));
                lPrinter.PrintText(count, 100, "| " + String.Format("{0,10} ", String.Format("{0:n}", p.Total)));
                lPrinter.PrintText(count, 113, "|");
                count++;
                cItens++;

                if (cItens == itensPag && cItens < objPedido.Items.Count)
                {
                    pag++;
                    ImprimeRodaJuntamento(lCodes, lPrinter, objPedido, false, ref count);
                    ImprimeCabecaJuntamento(lCodes, lPrinter, objPedido, ref count, Utilitarios.StrNumeroPagina(pag, objPedido.Items.Count, itensPag));
                    cItens = 0;
                }
            }

            ImprimeRodaJuntamento(lCodes, lPrinter, objPedido, true, ref count);

            while (count < 33 * pag)
            {
                lPrinter.PrintText(count++, 0, " ");
            }

            lPrinter.EndJob();
            FinalizarImpressao(objPedido, lPrinter);
        }

        private void ImprimeCabecaJuntamento(EpsonCodes lCodes, Reporter lPrinter, Modelo.Pedido objPedido, ref int count, string pag)
        {
            lPrinter.PrintText(count, 1, DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
            lPrinter.PrintText(count, 30, lCodes.CondensedOn + lCodes.BoldOn + lCodes.ExpandedOn + "RELATORIO" + lCodes.ExpandedOff + lCodes.BoldOff);

            count += 2;

            lPrinter.PrintText(count, 1, "Vend.:" + objPedido.Pessoa.Nome + " (" + objPedido.Pessoa.Codigo.ToString() + ")");

            lPrinter.PrintText(count++, 106, pag);
            lPrinter.PrintText(count++, 0, "+----------------------------------------------------------------------------------------------------------------+");

            string aux = (objPedido.Pessoa.TipoPessoa == "Física" ? objPedido.Pessoa.Apelido : objPedido.Pessoa.Fantasia) + " (" + objPedido.Pessoa.Codigo.ToString() + ")";
            lPrinter.PrintText(count, 0, "| " + "Fantasia.....:" + lCodes.BoldOn + lPrinter.CleanText(aux) + lCodes.BoldOff);

            lPrinter.PrintText(count++, 113, "|");
            lPrinter.PrintText(count, 0, "| Razão Social.:" + lPrinter.CleanText(objPedido.PessoaNome));

            lPrinter.PrintText(count++, 113, "|");

            if (objPedido.PessoaEndereco != null)
                aux = (objPedido.PessoaEndereco.Length < 50 ? objPedido.PessoaEndereco : objPedido.PessoaEndereco.Remove(50)) + ", " + (objPedido.PessoaBairro != null ? (objPedido.PessoaBairro.Length <= 20 ? objPedido.PessoaBairro : objPedido.PessoaBairro.Remove(20)) : "") + " - " + objPedido.PessoaCidade + ", " + objPedido.PessoaUF;
            else
                aux = "";

            lPrinter.PrintText(count, 0, "| Endereco.....:" + lPrinter.CleanText(aux));
            lPrinter.PrintText(count++, 113, "|");
            lPrinter.PrintText(count, 0, "| CNPJ/CPF.....:" + objPedido.PessoaCNPJCPF);
            lPrinter.PrintText(count, 45, "IE/RG:" + objPedido.PessoaInscRG);
            lPrinter.PrintText(count, 94, "Fone: " + objPedido.PessoaTelefone);
            lPrinter.PrintText(count++, 113, "|");
            lPrinter.PrintText(count++, 0, "+--------+-----------+----+------------------------------------------------------------+------------+------------+");
            lPrinter.PrintText(count++, 0, "|   PROD |     QUANT | UN | DESCRICAO                                                  |   PRECO UN |      TOTAL |");
            lPrinter.PrintText(count++, 0, "+--------+-----------+----+------------------------------------------------------------+------------+------------+");
        }

        private void ImprimeRodaJuntamento(EpsonCodes lCodes, Reporter lPrinter, Modelo.Pedido objPedido, bool imp, ref int count)
        {
            lPrinter.PrintText(count++, 0, "+--------+-----------+----+------------------------------------------------------------+------------+------------+");
            if (imp)
            {
                lPrinter.PrintText(count, 88, "TOTAL.......:");
                lPrinter.PrintText(count++, 101, String.Format("{0,11} ", String.Format("{0:n}", objPedido.TotalPedido)));
            }
            else
            {
                lPrinter.PrintText(count, 88, "TOTAL.......:");
                lPrinter.PrintText(count++, 101, String.Format("{0,11} ", "****"));
                lPrinter.PrintText(count++, 0, "------------------------------------------------------------------------------------------------------------------");
            }
            lPrinter.PrintText(count++, 0, " " + lCodes.CondensedOff + lCodes.Eject);
        }
        #endregion

        private void EnviarEmail(Modelo.Filial objFilial, string pNomeArquivo, string pEmailCliente)
        {
            try
            {
                SmtpClient cliente = new SmtpClient(objFilial.ServidorSMTP);
                cliente.Credentials = new NetworkCredential(objFilial.UsuarioEmail, objFilial.SenhaEmail);
                MailMessage mensagem = new MailMessage(new MailAddress(objFilial.UsuarioEmail), new MailAddress(pEmailCliente));
                mensagem.Subject = "Orçamento";
                cliente.Port = objFilial.PortaSmtp;

                mensagem.IsBodyHtml = true;
                mensagem.Body = ObservacaoEmail.TrimEnd() + "<br><br>Pedido em anexo";

                Attachment arqPedido = new Attachment(pNomeArquivo);
                mensagem.Attachments.Add(arqPedido);

                cliente.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar email.\n", ex);
            }
        }

        private void FinalizarImpressao(Modelo.Pedido objPedido, Reporter lPrinter)
        {
            switch (DestinoImpressao)
            {
                case Modelo.DestinoImpressao.Vídeo:
                    lPrinter.PreviewJob();
                    break;
                case Modelo.DestinoImpressao.Impressora:
                    lPrinter.PrintJob();
                    break;
                case Modelo.DestinoImpressao.Email:

                    if (String.IsNullOrEmpty(Email) || Email.Trim() == String.Empty)
                        throw new Exception("Não foi informado um email.\nVerifique.");
                    else
                        EnviarEmail(objPedido.Filial, lPrinter.NomeArquivo, Email);
                    break;
                default:
                    break;
            }
        }

        private static void CriaVendedor(cwkGestao.Modelo.Pedido pedido)
        {
            cwkGestao.Modelo.Pessoa vendedor = new cwkGestao.Modelo.Pessoa();
            vendedor.Apelido = string.Empty;
            pedido.Vendedor = vendedor;
        }
    }
}
