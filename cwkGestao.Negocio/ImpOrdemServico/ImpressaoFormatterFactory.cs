using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.ImpOrdemServico
{
    public class ImpressaoFormatterFactory
    {
        private IDictionary<string, ImpressaoTag> tags;
        private IDictionary<string, object> valores;
        private IList<IDictionary<string, object>> valoresProdutos;
        private IList<IDictionary<string, object>> valoresServicos;

        private IList<string> cabecalhoProduto;
        private IList<string> cabecalhoOrcamento;
        private IList<string> cabecalhoServico;
        private IList<string> linhasArquivoTemplate;

        private Cabecalho cabecalhoConfProduto;
        private Cabecalho cabecalhoConfServico;
        private Cabecalho cabecalhoConfOrcamento;

        private int alturaPagina;
        private int quebraPagina;

        public IList<string> Configure(OSOrdemServico osOrdemServico)
        {
            valores = MapearValores(osOrdemServico);
            valoresProdutos = MapearValoresProdutos(osOrdemServico);
            valoresServicos = MapearValoresServicos(osOrdemServico);
            linhasArquivoTemplate = AbrirArquivoTemplate().ToList();

            cabecalhoProduto = BuildCabecalhoProduto(linhasArquivoTemplate, "pC", true);
            cabecalhoServico = BuildCabecalhoProduto(linhasArquivoTemplate, "sC", true);
            cabecalhoOrcamento = BuildCabecalhoProduto(linhasArquivoTemplate, "cabecalho", false);

            tags = BuildDicionario(linhasArquivoTemplate);

            return linhasArquivoTemplate;
        }





        private IList<string> BuildCabecalhoProduto(IList<string> linhasArquivoTemplate, string tag, bool deleteAfter)
        {
            IList<string> retorno = new List<string>();
            int i, j = 0;
            for (i = 0; i < linhasArquivoTemplate.Count; i++)
            {
                string linha = linhasArquivoTemplate[i];

                int indiceInicio = linha.IndexOf("<" +tag);
                if (indiceInicio >= 0)
                {
                    for (j = i; j < linhasArquivoTemplate.Count; j++)
                    {
                        int indiceFim = linhasArquivoTemplate[j].IndexOf("</" + tag + ">");
                        if (indiceFim >= 0)
                        {
                            break;
                        }
                    }

                    for (int k = i + 1; k < j; k++)
                    {
                        retorno.Add(linhasArquivoTemplate[k]);
                    }
                    break;
                }
            }

            if (deleteAfter)
            {
                for (int k = j; k >= i; k--)
                {
                    linhasArquivoTemplate.RemoveAt(k);
                }
            }
            return retorno;
        }


        private IDictionary<string, ImpressaoTag> BuildDicionario(IList<string> linhasArquivoTemplate)
        {
            IDictionary<string, ImpressaoTag> config = ExaminarCabecalho(linhasArquivoTemplate);

            DissecarTamanhoTags(config, linhasArquivoTemplate);

            return config;
        }

        private void DissecarTamanhoTags(IDictionary<string, ImpressaoTag> config, IList<string> linhasArquivoTemplate)
        {
            IList<int> linhasConfiguracao = new List<int>();

            for (int index = 0; index < linhasArquivoTemplate.Count; index++)
            {
                string linha = linhasArquivoTemplate[index];
                if (DissecarLinha(config, linha))
                    linhasConfiguracao.Add(index);
                    
            }
            

            ExaminaConfiguracaoCabecalhos(linhasArquivoTemplate, linhasConfiguracao);

            ColocarTagTemporaria(config, linhasArquivoTemplate);
            ColocarTagTemporaria(config, cabecalhoOrcamento);
            ColocarTagTemporaria(config, cabecalhoProduto);
            ColocarTagTemporaria(config, cabecalhoServico);
        }

        private void ExaminaConfiguracaoCabecalhos(IList<string> linhasArquivoTemplate, IList<int> linhasConfiguracao)
        {
            for (int i = linhasConfiguracao.Count - 1; i >= 0; i--)
            {
                Cabecalho cabecalho = new Cabecalho();
                int indiceRepeteFolha = linhasArquivoTemplate[linhasConfiguracao[i]].IndexOf("repeteNovaFolha");
                if (indiceRepeteFolha > 0)
                {
                    if (linhasArquivoTemplate[linhasConfiguracao[i]].Substring(indiceRepeteFolha + "repeteNovaFolha".Length + 2, 3) == "sim")
                        cabecalho.RepeteNovaFolha = true;
                }
                if (linhasArquivoTemplate[linhasConfiguracao[i]].Contains("condensado"))
                    cabecalho.EhCondensado = true;
                if (linhasArquivoTemplate[linhasConfiguracao[i]].Contains("expandido"))
                    cabecalho.EhExpandido = true;
                if (linhasArquivoTemplate[linhasConfiguracao[i]].Contains("negrito"))
                    cabecalho.EhNegrito = true;

                if (linhasArquivoTemplate[linhasConfiguracao[i]].StartsWith("<cabecalho"))
                {
                    cabecalhoConfOrcamento = cabecalho;
                }
                if (linhasArquivoTemplate[linhasConfiguracao[i]].StartsWith("<servicos"))
                {
                    cabecalhoConfServico = cabecalho;
                }
                if (linhasArquivoTemplate[linhasConfiguracao[i]].StartsWith("<produtos"))
                {
                    cabecalhoConfProduto = cabecalho;
                }
                linhasArquivoTemplate.RemoveAt(linhasConfiguracao[i]);
            }
        }

        private void ColocarTagTemporaria(IDictionary<string, ImpressaoTag> config, IList<string> linhasArquivoTemplate)
        {
            for (int i = 0; i < linhasArquivoTemplate.Count; i++)
            {
                int indiceAberturaTag = linhasArquivoTemplate[i].IndexOf("<");
                while (indiceAberturaTag >= 0)
                {
                    string tag = ExtraiTag(linhasArquivoTemplate[i].Substring(indiceAberturaTag));
                    int indiceFechaTag = linhasArquivoTemplate[i].IndexOf(">");

                    linhasArquivoTemplate[i] = linhasArquivoTemplate[i].Replace(linhasArquivoTemplate[i].Substring(indiceAberturaTag, indiceFechaTag - indiceAberturaTag + 1),
                                                             "#" + tag + "#");

                    indiceAberturaTag = linhasArquivoTemplate[i].IndexOf("<", indiceAberturaTag + 1);
                }
            }
        }

        private bool DissecarLinha(IDictionary<string, ImpressaoTag> config, string linha)
        {
            int indiceAbertura = linha.IndexOf("<");
            
            if (indiceAbertura >= 0)
            {
                string tag = ExtraiTag(linha.Substring(indiceAbertura)).Trim();
                int tamanho = ExtraiTamanho(linha.Substring(indiceAbertura)) + 2;

                if (!TagAdministrativa(tag))
                {
                    try
                    {
                        config[tag].Tamanho = tamanho;
                    }
                    catch (KeyNotFoundException e)
                    {
                        throw new KeyNotFoundException("Verifique o arquivo de template de impressão." + Environment.NewLine + " Tag não reconhecida: " + tag, e);
                    }
                    DissecarLinha(config, linha.Substring(indiceAbertura + 1));

                    return false;
                }
                else
                    return true;
            }
            return false;
        }

        private bool TagAdministrativa(string tag)
        {
            return tag.Contains("cabecalho") || tag.Contains("produtos") || tag.Contains("pC") || tag.Contains("servicos") || tag.Contains("sC");
        }

        private int ExtraiTamanho(string linha)
        {
            return linha.IndexOf(">")-1;
        }
        
        private String ExtraiTag(string linha)
        {
            int indiceEspaco = linha.IndexOf("\n") - 1;
            int indiceFechaTag = linha.IndexOf(">") - 1;

            return linha.Substring(1, indiceEspaco > indiceFechaTag ? indiceEspaco : indiceFechaTag).Trim();
        }

        private IDictionary<string, ImpressaoTag> ExaminarCabecalho(IList<string> linhasArquivoTemplate)
        {
            IDictionary<string, ImpressaoTag> construcaoTags = TagsPadrao();
            if (linhasArquivoTemplate[0].StartsWith("<config"))
                ExaminarFormatos(linhasArquivoTemplate, construcaoTags);
            return construcaoTags;
        }

        private void ExaminarFormatos(IList<string> list, IDictionary<string, ImpressaoTag> construcaoTags)
        {
            XElement elementoConfig = ExtrairElementoConfig(list);
            ExtrairConfiguracaoPagina(elementoConfig);

            foreach (XElement xElement in elementoConfig.Elements())
            {
                XAttribute formato = xElement.Attribute("formato");
                if (formato != null)
                    construcaoTags[xElement.Name.LocalName].Formato = formato.Value;

                XAttribute multiplasLinhas = xElement.Attribute("multiplasLinhas");
                if (multiplasLinhas != null)
                    construcaoTags[xElement.Name.LocalName].MultiplasLinhas = multiplasLinhas.Value == "sim" ? true : false;
            }
        }

        private void ExtrairConfiguracaoPagina(XElement elementoConfig)
        {
            try
            {
                alturaPagina = Convert.ToInt32(elementoConfig.Attribute("alturaFolha").Value);
                quebraPagina = Convert.ToInt32(elementoConfig.Attribute("quebraPagina").Value);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(
                    "Não foi configurado \"alturaFolha\" ou \"quebraPagina\" no arquivo de template.", e);
            }
        }

        private XElement ExtrairElementoConfig(IList<string> list)
        {
            string configCompleto = "";
            
            do
            {
                configCompleto += list[0];
                list.RemoveAt(0);
            } while (!configCompleto.Contains("</config>"));

            return XElement.Parse(configCompleto);
        }

        private IDictionary<string, ImpressaoTag> TagsPadrao()
        {
            IDictionary<string, ImpressaoTag> retorno = new Dictionary<string, ImpressaoTag>();
            TagsPadraoOrcamento(retorno);

            TagsPadraoProdutos(retorno);

            TagsPadraoServicos(retorno);


            return retorno;
        }

        private IList<IDictionary<string, object>> MapearValoresServicos(OSOrdemServico osOrdemServico)
        {
            IList<IDictionary<string, object>> retorno = new List<IDictionary<string, object>>();
            foreach (OSServicoItem osServicoItem in osOrdemServico.OSServicoItems)
            {
                IDictionary<string, object> valoresProduto = new Dictionary<string, object>();
                valoresProduto.Add("sn", osServicoItem.Descricao);
                valoresProduto.Add("spt", osServicoItem.Total);

                retorno.Add(valoresProduto);
            }
            return retorno;
        }

        private void TagsPadraoServicos(IDictionary<string, ImpressaoTag> retorno)
        {
            retorno.Add("sn", new ImpressaoTag());
            retorno.Add("spt", new ImpressaoTag());
        }

        private IList<IDictionary<string, object>> MapearValoresProdutos(OSOrdemServico osOrdemServico)
        {
            IList<IDictionary<string, object>> retorno = new List<IDictionary<string, object>>();
            foreach (OSProdutoItem osProdutoItem in osOrdemServico.OSProdutoItems)
            {
                IDictionary<string, object> valoresProduto = new Dictionary<string, object>();
                valoresProduto.Add("ps", osProdutoItem.Seq);
                valoresProduto.Add("pc", osProdutoItem.Produto.Codigo);
                valoresProduto.Add("pn", osProdutoItem.Descricao);
                valoresProduto.Add("pq", osProdutoItem.Quantidade);
                valoresProduto.Add("ppu", osProdutoItem.Preco);
                valoresProduto.Add("ppt", osProdutoItem.Total);
                valoresProduto.Add("pd", osProdutoItem.DescontoPerc);

                retorno.Add(valoresProduto);
            }
            return retorno;
        }

        private void TagsPadraoProdutos(IDictionary<string, ImpressaoTag> retorno)
        {
            retorno.Add("ps", new ImpressaoTag());
            retorno.Add("pc", new ImpressaoTag());
            retorno.Add("pn", new ImpressaoTag());
            retorno.Add("pq", new ImpressaoTag());
            retorno.Add("ppu", new ImpressaoTag());
            retorno.Add("ppt", new ImpressaoTag());
            retorno.Add("pd", new ImpressaoTag());
        }

        private IDictionary<string, object> MapearValores(OSOrdemServico osOrdemServico)
        {
            IDictionary<string, object> retorno = new Dictionary<string, object>();
            retorno.Add("oc", osOrdemServico.Codigo);
            retorno.Add("oe", osOrdemServico.Data);
            retorno.Add("ocn", osOrdemServico.Cliente.Nome);
            retorno.Add("ocf", osOrdemServico.Cliente.TelefonePrincipal);
            retorno.Add("oce", osOrdemServico.Cliente.EnderecoPrincipal().Endereco);
            retorno.Add("ocb", osOrdemServico.Cliente.EnderecoPrincipal().Bairro);
            retorno.Add("occep", osOrdemServico.Cliente.EnderecoPrincipal().CEP);
            retorno.Add("occ", osOrdemServico.Cliente.EnderecoPrincipal().Cidade.Nome);
            retorno.Add("occe", osOrdemServico.Cliente.EnderecoPrincipal().Cidade.UF.Nome);
            retorno.Add("occpfcnpj", osOrdemServico.Cliente.CNPJ_CPF);
            retorno.Add("ocie", osOrdemServico.Cliente.Inscricao);
            retorno.Add("oss", osOrdemServico.ProblemaRelatado);

            retorno.Add("oed", osOrdemServico.Equipamento.Descricao);
            retorno.Add("oes", osOrdemServico.Equipamento.NumeroSerie);
            retorno.Add("oa", osOrdemServico.Acessorios);

            retorno.Add("ovn", osOrdemServico.Vendedor.Nome);
            retorno.Add("op", osOrdemServico.Condicao.Nome);

            retorno.Add("osst", osOrdemServico.ServicoSubTotal);
            retorno.Add("osd", osOrdemServico.ServicoDescontoValor);
            retorno.Add("opst", osOrdemServico.ProdutoSubTotal);
            retorno.Add("opd", osOrdemServico.ProdutoDescontoValor);
            retorno.Add("ot", osOrdemServico.Total);

            return retorno;
        }

        private void TagsPadraoOrcamento(IDictionary<string, ImpressaoTag> retorno)
        {
            retorno.Add("oc", new ImpressaoTag());
            retorno.Add("oe", new ImpressaoTag());
            retorno.Add("ocn", new ImpressaoTag());
            retorno.Add("ocf", new ImpressaoTag());
            retorno.Add("oce", new ImpressaoTag());
            retorno.Add("ocb", new ImpressaoTag());
            retorno.Add("occep", new ImpressaoTag());
            retorno.Add("occ", new ImpressaoTag());
            retorno.Add("occe", new ImpressaoTag());
            retorno.Add("occpfcnpj",new ImpressaoTag());
            retorno.Add("ocie", new ImpressaoTag());
            retorno.Add("oss", new ImpressaoTag());

            retorno.Add("oed", new ImpressaoTag());
            retorno.Add("oes", new ImpressaoTag());
            retorno.Add("oa", new ImpressaoTag());

            retorno.Add("ovn", new ImpressaoTag());
            retorno.Add("op", new ImpressaoTag());

            retorno.Add("osst", new ImpressaoTag());
            retorno.Add("osd", new ImpressaoTag());
            retorno.Add("opst", new ImpressaoTag());
            retorno.Add("opd", new ImpressaoTag());
            retorno.Add("ot", new ImpressaoTag());
        }

 


        private static string[] AbrirArquivoTemplate()
        {
            return File.ReadAllLines("OrcamentoServicoTemplate.txt");
        }

        public string Format(string linha)
        {
            IList<string> tagsNaLinha = TagsNaLinha(linha);
            string retorno = linha;

            try
            {
                foreach (string tag in tagsNaLinha)
                {
                    if (tag.StartsWith("p"))
                        throw new TagProdutoException();
                    else if (tag.StartsWith("s"))
                        throw new TagServicoException();
                    else
                        retorno = retorno.Replace("#" + tag + "#", tags[tag].FormatString(valores[tag]));
                }
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception("Erro na formatação da linha: "+ linha, e);
            }
            return retorno;
        }

        private IList<string> TagsNaLinha(string linha)
        {
            Regex expressao = new Regex("#[a-z]*#");
            List<string> retorno = new List<string>();
            foreach (Match matche in expressao.Matches(linha))
            {
                retorno.Add(matche.Value.Replace("#", ""));
            }
            return retorno;
        }

        public IList<string> GetImpressao()
        {
            LinhasImpressao impressao = new LinhasImpressao(alturaPagina);
            foreach (string linha in linhasArquivoTemplate)
            {
                bool trocouPagina;
                EstouroDePaginaException ultimaTrocaPagina = null;
                do
                {
                    trocouPagina = false;
                    try
                    {
                        try
                        {
                            impressao.Add(GetMarcador(cabecalhoConfOrcamento), this.Format(linha));
                        }
                        catch (TagProdutoException)
                        {
                            if (ultimaTrocaPagina == null || (ultimaTrocaPagina.ListaQueDeuPau != null && ultimaTrocaPagina.ListaQueDeuPau.Count > 0))
                                impressao.TryAddCabecalho(GetMarcador(cabecalhoConfProduto), cabecalhoProduto);

                            if (ultimaTrocaPagina != null && ultimaTrocaPagina.ListaQueDeuPau != null)
                                impressao.TryAddLista(GetMarcador(cabecalhoConfProduto), ultimaTrocaPagina.ListaQueDeuPau);
                            else
                                impressao.TryAddLista(GetMarcador(cabecalhoConfProduto), FormatProdutos(linha, valoresProdutos));
                        }
                        catch (TagServicoException)
                        {
                            if (ultimaTrocaPagina == null  || (ultimaTrocaPagina.ListaQueDeuPau != null && ultimaTrocaPagina.ListaQueDeuPau.Count > 0))
                                impressao.TryAddCabecalho(GetMarcador(cabecalhoConfServico), cabecalhoServico);

                            if (ultimaTrocaPagina != null && ultimaTrocaPagina.ListaQueDeuPau != null)
                                impressao.TryAddLista(GetMarcador(cabecalhoConfServico), ultimaTrocaPagina.ListaQueDeuPau);
                            else
                                impressao.TryAddLista(GetMarcador(cabecalhoConfServico), FormatProdutos(linha, valoresServicos));
                        }
                    }
                    catch (EstouroDePaginaException e)
                    {
                        trocouPagina = true;
                        impressao.AddQuebraPagina(quebraPagina);
                        if (cabecalhoConfOrcamento.RepeteNovaFolha)
                            foreach (string linhaOrc in cabecalhoOrcamento)
                            {
                                impressao.Add(GetMarcador(cabecalhoConfOrcamento), this.Format(linhaOrc));
                            }

                        ultimaTrocaPagina = e;

                    }
                } while (trocouPagina);

            }
            return impressao;
        }

        private string GetMarcador(Cabecalho configuracao)
        {
            string marcador;
            marcador = Marcadores.Build(configuracao.EhCondensado
                                        , configuracao.EhNegrito
                                        , configuracao.EhExpandido);
            return marcador;
        }

        private IList<string> FormatProdutos(string linha, IList<IDictionary<string, object>> produtos)
        {
            IList<string> retorno = new List<string>();
            IList<string> tagsNaLinha = TagsNaLinha(linha);

            foreach (IDictionary<string, object> valores in produtos)
            {
                string linhaFormatada = linha;
                foreach (string tag in tagsNaLinha)
                {
                    linhaFormatada = linhaFormatada.Replace("#" + tag + "#", tags[tag].FormatString(valores[tag]));    
                }
                retorno.Add(linhaFormatada);
            }
            return retorno;
        }
    }
    
    class TagProdutoException : Exception {}
    class TagServicoException : Exception { }
}