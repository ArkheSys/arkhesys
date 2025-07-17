using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using System.Globalization;
using System.Text.RegularExpressions;


namespace CwkGestao.Integracao.PafEcf.Salc
{
    public static class Util
    {
        public static IList<string> PreparaObjetoPedidoDav(Pedido pedido)
        {
            // Dados do DAV  
            IList<string> listaRetorno = new List<string>();
            string quebraLinha = System.Environment.NewLine.ToString();
            string valorDescStr = pedido.ValorDesconto.ToString();
            valorDescStr = valorDescStr.Contains(",") ? valorDescStr.Replace(".", "").Replace(",", "") : valorDescStr + "00"; 
            string valorAcresStr = pedido.ValorAcrescimo.ToString();
            valorAcresStr = valorAcresStr.Contains(",") ? valorAcresStr.Replace(".", "").Replace(",", "") : valorAcresStr + "00"; 
            string dataEmissaoDav = pedido.Dt.ToString().Replace("/", "").Substring(0, 8);
            string textoComVinculacaoDeVenda = PreparaCampo(pedido.Numero.ToString(), 10, '0');
            string valorFormaPagamento = String.Format("{0:0.00}", pedido.TotalPedido).Replace(".", "").Replace(",", "");

            string row = "0" + PreparaCampo(pedido.Pessoa.Codigo.ToString(), 9, '0');//Código do cliente
            row += PreparaCampo(pedido.Codigo.ToString(), 10, '0');//úmero sequencial do DAV
            row += PreparaCampo(textoComVinculacaoDeVenda, 48, ' '); //Texto com a vinculação da Venda.
            row += PreparaCampo(valorDescStr, 9, '0');//Valor do desconto no subtotal do cupom com 2 casas decimais,
            row += PreparaCampo(valorAcresStr, 9, '0');//Valor do acréscimo no subtotal do cupom com 2 casas decimais
            row += PreparaCampo(dataEmissaoDav, 8, ' ');//Data da emissão do DAV
            row += PreparaCampo(pedido.TipoPedido.Nome, 30, ' ');//Titulo atribuído ao DAV (ex: Orçamento, Pedido, Ordem de Serviço, etc.) 
            row += PreparaCampo("", 48, ' ');//Texto livre com o tamanho equivalente a uma linha na impressora fiscal.
            row += PreparaCampo("L", 1, ' '); //Situação do DAV.
            row += PreparaCampo("", 26, ' ');//Espaços em branco. 
            row += PreparaCampo("X", 1, ' ');//“X” indica final da linha. 
            row += quebraLinha;

            //Dados dos Produtos 
            foreach (var item in pedido.Items)
            {
                string dataInclusaoItens = item.IncData.ToString().Replace("/", "").Substring(0, 8);
                string metragem = "0001000";
                string valorDescItemStr = String.Format("{0:0.00}",item.ValorDesconto).Replace(".", "").Replace(",", "");
                string quantidadeStr = String.Format("{0:0.000}", item.Quantidade).Replace(".", "").Replace(",", "");
                string valorUnitario = String.Format("{0:0.00}", item.Valor).Replace(".", "").Replace(",", "");
                row += "1" + PreparaCampo(item.Produto.Codigo.ToString(), 7, '0');//Código do Produto. Ex: 0000001 
                row += PreparaCampo(quantidadeStr, 9, '0'); //Quantidade vendida com 03 casas decimais
                row += PreparaCampo(metragem, 7, '0');//Metragem do produto com 03 casas decimais. O exemplo da quantidade ilustra também esse campo.
                row += PreparaCampo(valorUnitario, 7, '0');//Preço unitário do produto vendido com 02 casas decimais.
                row += PreparaCampo("C", 1, ' ');//Indicador de Cancelamento do Item .
                row += PreparaCampo(dataInclusaoItens, 8, ' ');//Data de inclusão do item no DAV, no formato DDMMAAAA. 
                row += PreparaCampo(valorDescItemStr, 8, '0');//Valor do Desconto no Item, com 02 casas decimais
                row += PreparaCampo("", 8, '0');//Valor do Acréscimo no Item, com 02 casas decimais
                row += PreparaCampo(item.Sequencia.ToString(), 3, '0');//Número seqüencial do item do DAV. 
                row += PreparaCampo("", 10, ' ');//Espaços em branco.
                row += PreparaCampo("X", 1, ' ');//“X” indica final da linha.
                row += quebraLinha;
            }

            //Forma de pagamento
            row += "2" + PreparaCampo(pedido.Condicao.Codigo.ToString(), 2, '0');//Código da forma de pagamento
            row += PreparaCampo(valorFormaPagamento, 13, '0');//Valor pago na forma de pagamento com 2(duas) casas decimais.
            row += PreparaCampo("", 53, ' ');// Espaços em branco. 
            row += PreparaCampo("X", 1, ' ');//“X” indica final da linha. 
            row += quebraLinha;

            // Finalizador de Arquivo 
            row += "9";// Indica final do arquivo.
 
            listaRetorno.Add(row);

            return listaRetorno;
        }

        public static IList<string> PreparaObjetoClienteDav(Pessoa pessoa)
        {
            try
            {
                pessoa.RG = pessoa.TipoPessoa == "Física" ? pessoa.RG.Replace(".", "").Replace("-", "") : "";
            }
            catch (Exception)
            {
                pessoa.RG = "";
            }
            try
            {
                pessoa.Inscricao = pessoa.TipoPessoa != "Física" ? pessoa.Inscricao.Replace(".", "").Replace("-", "").Replace("/", "") : "";
            }
            catch (Exception)
            {
                pessoa.Inscricao = "";
            }
            
            string fax;
            string telefone;
            try
            {
                fax = pessoa.PessoaTelefones.Where(x => x != null && x.Tipo == "Fax").First() != null ?
                pessoa.PessoaTelefones.Where(x => x != null && x.Tipo == "Fax").First().Numero.Replace("(", "").Replace(")", "").Replace("-", "") : "";
            }
            catch (Exception)
            {
                fax = "";
            }
            try
            {
                telefone = pessoa.PessoaTelefones.Where(x => x != null && x.BPrincipal == true).First().Numero.Replace("(", "").Replace(")", "").Replace("-", "");
            }
            catch (Exception)
            {
                telefone = "";
            }

            IList<string> lRetorno = new List<string>();
            string row = "";

            row += "1"; //Existem dados
            row += PreparaCampo(pessoa.Codigo.ToString(), 9, '0'); // Código do cliente. 9 dígitos, comp 0 esquerda.
            try
            {
                row += PreparaCampo(pessoa.Fantasia.Length > 40 ? pessoa.Fantasia.Substring(0, 39) : pessoa.Fantasia, 40, ' '); //Fantasia cliente, 40 dígitos, comp espaço direita.
            }
            catch (Exception)
            {
                row += PreparaCampo("", 40, ' ');
            }
            
            row += PreparaCampo(pessoa.Nome.Length > 40 ? pessoa.Nome.Substring(0, 39) : pessoa.Nome, 40, ' '); //Razao Social cliente, 40 dígitos, comp espaço direita.
            row += PreparaCampo(pessoa.EnderecoPrincipal().Endereco, 40, ' '); //Endereço cliente, 40 dígitos, comp espaço direita.
            try{ row += PreparaCampo(pessoa.EnderecoPrincipal().Bairro.Length > 20 ? pessoa.EnderecoPrincipal().Bairro.Substring(0, 19) : pessoa.EnderecoPrincipal().Bairro, 20, ' ');}
            catch (Exception){row += PreparaCampo("", 20, ' ');}
            try
            {
                row += PreparaCampo(pessoa.EnderecoPrincipal().CEP.Replace(".", "").Replace("-", ""), 10, ' ');
            }
            catch (Exception)
            {
                row += PreparaCampo("", 10, ' ');
            }
            
            row += PreparaCampo(pessoa.EnderecoPrincipal().Cidade.Nome, 30, ' ');
            row += PreparaCampo(pessoa.EnderecoPrincipal().Cidade.UF.Sigla, 2, ' ');
            row += PreparaCampo(telefone, 14, ' ');
            row += PreparaCampo(fax, 14, ' '); // FAX. Passando em branco.
            row += PreparaCampo(pessoa.CNPJ_CPF.Replace(".", "").Replace("-","").Replace("/",""), 14, ' ');
            row += PreparaCampo(pessoa.Inscricao, 15, ' ');
            row += PreparaCampo("", 2, '0'); //Código da forma de pagamento. Default 0. Se o cliente usar forma de pagamento específica, deve ser informado.
            row += PreparaCampo("N", 1, ' '); //Restrição para venda a prazo. 
            row += PreparaCampo(pessoa.EnderecoPrincipal().Cidade.IBGE, 7, '0'); //Código do Município (SPED FISCAL)    
            row += PreparaCampo("", 9, '0'); //Código do SUFRAMA (SPED FISCAL)
            row += PreparaCampo("1058", 5, '0'); //Código do País (SPED FISCAL)
            row += PreparaCampo("", 8, ' '); //Data de alteração no cadastro (SPED FISCAL)
            row += PreparaCampo(pessoa.EnderecoPrincipal().Numero, 10, ' '); //Numero Imovel (SPED FISCAL)
            row += PreparaCampo(pessoa.EnderecoPrincipal().Complemento, 60, ' '); //Dados complementares do endereço (SPED FISCAL)
            row += PreparaCampo(pessoa.RG, 14, ' '); //Numero identidade
            row += PreparaCampo("N", 1, ' '); //Preenche com s ou n Indicador de cliente utilizado para caracterizar uma administradora de cartão (SPED FISCAL)
            row += PreparaCampo("", 33, ' '); //Espaço em branco
            row += PreparaCampo("X", 1, ' '); //Final da linha

            lRetorno.Add(row);

            var endBlock = "";
            endBlock += PreparaCampo("9", 1, ' '); // Final do arquivo]
            lRetorno.Add(endBlock);

            return lRetorno;
        }

        public static IList<string> PreparaObjetoProdutoDav(IList<PedidoItem> pItem)
        {
            IList<string> lRetorno = new List<string>();
            foreach (var p in pItem)
            {

                string valorP = String.Format("{0:N2}", p.Valor);
                valorP = valorP.Contains(",") ? valorP : valorP += "00";
                valorP = valorP.Replace(",", "").Replace(".", "");
                string row = "";
                row += "1"; //Existem dados;
                row += PreparaCampo(p.Produto.Codigo, 7, '0');
                row += PreparaCampo(p.Produto.Nome.Length > 40 ? p.Produto.Nome.Substring(0, 35) : p.Produto.Nome, 40, ' ');
                row += PreparaCampo(p.Produto.Unidade.Sigla, 5, ' ');
                row += PreparaCampo("", 15, ' '); // Referência => referência poderá ser utilizada pelo funcionário para busca de produtos.  
                row += PreparaCampo(valorP, 12, '0'); // Preço unitário de venda.
                row += PreparaCampo(NCMController.Instancia.LoadObjectById(p.Produto.ID_NCM).AliqCupom, 2, '0'); // Código do tributo. 
                row += PreparaCampo(p.Produto.Barra != null ? p.Produto.Barra.Length > 13 ? p.Produto.Barra.Substring(0, 12) : p.Produto.Barra : "", 13, '0'); // Código de barras.
                row += PreparaCampo("S", 1, ' '); // Situação. Ativo ou Inativo.
                row += PreparaCampo("S", 1, ' '); // Aceita desconto ou acrescimo após o registro de um item no cupom
                row += PreparaCampo("N", 1, ' '); // Produto utilizado em postos de combustível. 
                row += PreparaCampo("N", 1, ' '); // Utiliza 3 casas decimais após a vírgula.
                row += PreparaCampo(p.Produto.Unidade.BQtdFracionada ? "S" : "N", 1, ' '); // Produto fracionável.
                row += PreparaCampo("N", 1, ' '); // Produto pesável.
                row += PreparaCampo("A", 1, ' '); // Arredondar ou truncar.
                row += PreparaCampo("T", 1, ' '); // Produção própria ou terceiros.
                row += PreparaCampo("", 2, ' '); // Tipo do item (SPED FISCAL)
                row += PreparaCampo(p.Produto.NCM, 9, ' '); // NCM (SPED FISCAL)
                row += PreparaCampo("", 3, ' '); // Excessão do IPI (SPED FISCAL)
                row += PreparaCampo("", 2, '0'); // Código do genero do item (SPED FISCAL)
                row += PreparaCampo("", 4, '0'); // Código do serviço. (SPED FISCAL)
                row += PreparaCampo("030", 3, '0'); // CST VER COM O VICTOR
                row += PreparaCampo("", 73, ' '); // Espaço em branco
                row += PreparaCampo("X", 1, ' '); // Final da linha
                lRetorno.Add(row);
            }

            var endBlock = "";
            endBlock += PreparaCampo("9", 1, ' '); // Final do arquivo]
            lRetorno.Add(endBlock);

            return lRetorno;
        }

        public static IList<string> PreparaObjetoFormaPagamentoDav(Pedido p)
        {
            IList<string> lRetorno = new List<string>();
            string row = "";
            var listaAvista = p.Parcelas.Where(x => x.BEntrada == true).ToList();
            var totalAVista = listaAvista.Sum(x => x.Valor);
            var percentualAvista = String.Format("{0:00}",((totalAVista / p.TotalPedido) * 10000));
            var diasPrimeiraParcela = p.Parcelas.First().Vencimento - p.Dt;
            var listaAPrazo = p.Parcelas.Where(x => x.BEntrada != true).ToList();
            int diasParcelasAPrazo = 0;
            string diasParcelasAPrazoStr = "";
            string nomeFormapagamento = p.Condicao.Nome;
            string coeficienteJurosDesconto = String.Format("{0:0.0000}", CalculaCoeficienteJurosDesconto(p));
            string coeficienteJurosDescontoTratado = (coeficienteJurosDesconto).Replace(".", "").Replace(",", "");
            if ((p.Condicao.Nome.Length) > 30)
            {
                nomeFormapagamento = p.Condicao.Nome.Substring(0, 29);
            }

            int numParcelasAPrazo = listaAvista.Count() == 1 && listaAPrazo.Count == 0 ? 0 : listaAPrazo.Count; 
            
            for (int i = 0; i < listaAPrazo.Count-1; i++)
            {
                diasParcelasAPrazo = diasParcelasAPrazo + (listaAPrazo[i+1].Vencimento - (listaAPrazo[i].Vencimento)).Days;
                if (i == (listaAPrazo.Count - 2))
                {
                    diasParcelasAPrazoStr = (diasParcelasAPrazo / (i+1)).ToString(); 
                }
            }
          
            row += "1"; //Existem dados;
            row += PreparaCampo(p.Condicao.Codigo.ToString().Length > 2 ? p.Condicao.Codigo.ToString().Substring(0, 2) : p.Condicao.Codigo.ToString(), 2, '0');//Código da forma de pagamento
            row += PreparaCampo(nomeFormapagamento, 30, ' ');//Nome da Forma de Pagamento
            row += PreparaCampo(p.Parcelas.First().TipoDocumento.FormaPagamento.Nome, 20, ' ');//Nome que irá sair na impressora fiscal.

            row += PreparaCampo(percentualAvista, 5, '0'); // Percentual de entrada, caso esta forma de pagamento exija entrada (a vista), informar o percentual
            row += PreparaCampo(numParcelasAPrazo.ToString(), 2, '0');//Número de parcelas a prazo.
            row += PreparaCampo("0", 10, '0'); // Valor adicional por cada parcela em R$.
            row += PreparaCampo(diasPrimeiraParcela.Days.ToString(), 2, '0'); //Número de dias a contar da data da venda será efetuado a primeira cobrança
            row += PreparaCampo("1", 1, '0'); //1º. Vencimento. O número de dias do campo anterior respeitando a ordem: 1 – Fora o dia da compra 2 – Fora a semana da compra 3 – Fora a quinzena da compra 4 – Fora o mês da compra.   
            row += PreparaCampo(diasParcelasAPrazoStr, 2, '0'); //Número de dias das demais parcelas
            row += PreparaCampo("0", 4, '0'); //Percentual da “Taxa Administrativa” cobrado normalmente pelas administradoras de cartão de crédito
            row += PreparaCampo(coeficienteJurosDescontoTratado, 5, '0'); //Coeficiente de Juros/Desconto por venda a prazo com 4 decimais.
            row += PreparaCampo("0", 9, '0'); //Código do cliente a ser cobrado nas vendas a prazo.
            row += PreparaCampo("N", 1, ' '); //Forma de Pagamento por TEF 
            row += PreparaCampo("N", 1, ' '); //TEF é TEC-BAN.
            row += PreparaCampo("N", 1, ' '); //TEC-BAN faz consulta de cheques.
            row += PreparaCampo("N", 1, ' '); //REDECARD faz consulta de cheques.
            row += PreparaCampo("N", 1, ' '); //TEF é HIPERCARD.
            row += PreparaCampo("N", 1, ' '); //Termo de Compromisso. Caso esta forma de pagamento não seja TEF e seja necessário que se imprima um texto com os vencimentos das parcelas, preencha este campo com “S”, caso contrário preencha com “N”. 
            row += PreparaCampo("", 12, ' '); //Nome do arquivo TXT que contém o texto para ser apresentado no campo anterior.
            row += PreparaCampo("0", 1, '0'); //Número de cópias do termo de compromisso
            row += PreparaCampo("N", 1, ' '); //Solicita do Usuário que seja feito o cadastro do cheque no ato da venda.  
            row += PreparaCampo("", 9, ' ');  //Código da Administradora de cartão utilizado 
            row += PreparaCampo("", 76, ' '); //Espaços em branco
            row += PreparaCampo("X", 1, ' '); //Fim da linha
            lRetorno.Add(row);

            var endBlock = "";
            endBlock += PreparaCampo("9", 1, ' '); // Final do arquivo
            lRetorno.Add(endBlock);

            return lRetorno;
        }

        private static decimal CalculaCoeficienteJurosDesconto(Pedido pedido)
        {
            decimal valorSemAcrescimosDecrescimos = 0;
            decimal coeficienteJuros = 0;
            decimal coeficienteDesconto = 0;
            
            foreach (var item in pedido.Items)
                valorSemAcrescimosDecrescimos += item.ValorCalculado;
            
            coeficienteDesconto = ((pedido.TotalPedido - pedido.ValorAcrescimo) / (valorSemAcrescimosDecrescimos));
            coeficienteJuros = ((pedido.TotalPedido - pedido.ValorDesconto) / (valorSemAcrescimosDecrescimos));
            return (coeficienteJuros / coeficienteDesconto);
        }

        public static string PreparaCampo(string campo, int tamanho, char preenche)
        {
            try
            {
                string retorno = "";
                campo = RemoverAcentos(campo);

                if (campo.ToUpper() != "X")
                {
                    if (preenche == '0')
                        retorno = campo.PadLeft(tamanho, preenche);
                    else
                        retorno = campo.PadRight(tamanho, preenche);
                }
                else
                {
                    retorno = campo;
                }
                return retorno;      
            }
            catch (Exception)
            {
                campo = "";
                if (preenche == '0')
                    return campo.PadLeft(tamanho, preenche);
                else
                    return campo.PadRight(tamanho, preenche);
            }
           
        }

        public static string RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }

            return LimparCaracteres(sb.ToString());
        }

        static string LimparCaracteres(string strIn)
        {
            strIn = strIn.Replace("%", "Perc");
            strIn = strIn.Replace("@", "");
            strIn = strIn.Replace("*", "");
            strIn = strIn.Replace("$", "");
            strIn = strIn.Replace("º", "");
            
            return strIn;
        }
    }
}
