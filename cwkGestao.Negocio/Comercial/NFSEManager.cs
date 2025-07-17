using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Permissions;
using cwkGestao.Negocio.Utils;
using cwkGestao.Modelo;
using cwkGestao.Negocio.Padroes;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace cwkGestao.Negocio.Comercial
{
    public class NFSEManager
    {

        public static void Leitor(string path)
        {
            try
            {
                string[] arquivo = File.ReadAllLines(path);
                for (int i = 0; i < arquivo.Length; i++)
                {
                    arquivo[i] = NFSEUtils.Sanitizador(arquivo[i]);
                }

            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
            }
        }
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Escritor(string path, string nomeArquivo, IList<string> textoConstruido)
        {
            try
            {
                path = path + "\\" + nomeArquivo + ".tx2";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    foreach (var item in textoConstruido)
                    {
                        file.WriteLine(item);
                        //System.Console.WriteLine("Escrito: " + item + " no arquivo " + path);
                    }
                    file.Close();
                }

            }
            catch (Exception e)
            {
                e = NFSEUtils.Tratador(e);
                //System.Console.WriteLine(e.Message);
            }

        }

        public static void Lancador(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("Arquivo: " +  e.FullPath + " " + e.ChangeType);
            Leitor(e.FullPath);

        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            //Console.WriteLine("Arquivo: {0} renomeado como {1}", e.OldFullPath, e.FullPath);
        }

        public static NFSE ConstrutorNFSE(Nota n)
        {
            var configManager = ConfiguracaoManagerController.Instancia.GetAll().FirstOrDefault();

            HeaderControleRPS controle = new HeaderControleRPS();
            //controle.Grupo = configManager.grupo;
            //controle.CNPJ = n.Filial.CNPJ;
            controle.NomeCidade = n.Filial.Cidade.Nome.Replace(" ", "");

            HeaderRPS cabecalho = new HeaderRPS();
            cabecalho.IdLote = "Lote" + n.LoteNFSe;
            cabecalho.NumeroLote = n.LoteNFSe;
            cabecalho.CpfCnpjRemetente = n.Filial.CNPJ;
            cabecalho.InscricaoMunicipalRemetente = n.Filial.InscricaoMunicipal;
            cabecalho.QuantidadeRPS = 1;
            if (n.Filial.Cidade.IBGE == "3550308")
            {
                cabecalho.Versao = "1";
            }
            else
            {
                cabecalho.Versao = "2.01";
            }
            //cabecalho.CodigoCidadeRemetente = n.Filial.Cidade.IBGE;
            //cabecalho.Transacao = ;
            //cabecalho.DataInicio = DateTime.Now;
            //cabecalho.DataFim = DateTime.Now;
            //cabecalho.Versao = ;
            //cabecalho.MetodoEnvio = ;
            if (n.Filial.Cidade.IBGE == "3550308")
            {
                cabecalho.ValorTotalServicos = n.TotalServico;
                cabecalho.ValorTotalDeducoes = 0;
            }

            //cabecalho.ValorTotalBaseCalculo = ;

            IList<DadosRPS> ListaRPS = new List<DadosRPS>();

            foreach (NotaItemServico item in n.NotaItemsServicos)
            {
                var itemServico = NotaItemServicoController.Instancia.LoadObjectById(item.ID);

                #region preenchimento de dados
                DadosRPS dados = new DadosRPS();
                dados.IdRps = "RPS" + n.Numero;
                dados.NumeroRps = n.Numero;
                dados.SerieRps = n.Serie;
                dados.TipoRps = "1";
                dados.DataEmissao = n.DtEnvio == null ? DateTime.Now : (DateTime)n.DtEnvio;
                dados.OptanteSimplesNacional = Convert.ToInt32(n.Filial.SimplesNacional) == 0 ? 1 : 2;
                dados.IncentivadorCultural = n.Filial.IncentivadorCultural;
                dados.SituacaoNota = 1;
                dados.TipoTributacao = item.Servico.TipoTributacao;
                dados.NaturezaTributacao = n.Filial.NaturezaTributacao;
                dados.RegimeEspecialTributacao = n.NotaImpostoServicos.FirstOrDefault()?.Imposto.RegimeEspecialTributacao ?? 0;
                //dados.NumeroRpsSubstituido = ;
                //dados.SerieRpsSubstituido = ;
                //dados.TipoRpsSubstituido = ;
                dados.ExigibilidadeISS = item.Servico.ExigibilidadeISS.ToString();
                dados.ValorServicos = item.SubTotal;

                foreach (var imposto in n.NotaImpostoServicos)
                {
                    switch (imposto.Imposto.TipoImposto.ID)
                    {
                        case 1:
                            dados.AliquotaISS = imposto.Aliquota;
                            dados.ValorIss = imposto.Valor;
                            dados.BaseCalculo = imposto.BaseCalculo;

                            if (imposto.BaseRetencaoImposto)
                            {
                                dados.IssRetido = "1";
                                dados.ValorIssRetido = imposto.Valor;
                            }
                            else
                            {
                                dados.IssRetido = "2";
                            }
                            break;

                        case 2:
                            dados.AliquotaINSS = imposto.Aliquota;
                            dados.ValorInss = imposto.Valor;
                            break;

                        case 3:
                            dados.AliquotaIR = imposto.Aliquota;
                            dados.ValorIr = imposto.Valor;
                            break;

                        case 4:
                            dados.AliquotaCSLL = imposto.Aliquota;
                            dados.ValorCsll = imposto.Valor;
                            break;

                        case 5:
                            dados.AliquotaPIS = imposto.Aliquota;
                            dados.ValorPis = imposto.Valor;
                            break;

                        case 6:
                            dados.AliquotaCOFINS = imposto.Aliquota;
                            dados.ValorCofins = imposto.Valor;
                            break;
                    }
                }

                //dados.ValorDeducoes = ;
                dados.ValorLiquidoNfse = n.TotalNota;
                dados.DescontoIncondicionado = (item.PercDesconto * item.Valor) / 100;
                dados.DescontoCondicionado = n.ValorDesconto;
                dados.CodigoItemListaServico = item.Servico.CodigoItemListaServico;
                dados.ResponsavelRetencao = n.ResponsavelRetencao == 0 ? null : n.ResponsavelRetencao;
                //dados.CodigoCnae = Convert.ToInt32(n.Filial.CNAE);
                dados.CodigoTributacaoMunicipio = n.Filial.Cidade.CodigoTributacaoMunicipio;
                dados.CpfCnpjPrestador = n.Filial.CNPJ;
                dados.InscricaoMunicipalPrestador = n.Filial.InscricaoMunicipal;

                dados.RazaoSocialPrestador = n.Filial.Nome;
                dados.TelefonePrestador = n.Filial.Telefone.Substring(4);
                dados.EnderecoPrestador = n.Filial.Endereco;
                dados.NumeroEnderecoPrestador = n.Filial.Numero;
                dados.ComplementoEnderecoPrestador = n.Filial.Complemento;
                dados.BairroEnderecoPrestador = n.Filial.Bairro;
                dados.DescricaoCidadePrestacao = n.Filial.Cidade.Nome;

                //item.PegaDescricao(conf.UtilizaServicoPorExtenso, item.Servico.Descricao, item.Quantidade);

                var Discriminacao = $"{item.Servico?.Descricao ?? ""} - {itemServico.Descricao?.Replace("\n", " ")}";

                dados.DiscriminacaoServico = Discriminacao;

                dados.CodigoCidadePrestacao = n.Filial.Cidade.IBGE;
                dados.DescricaoCidadePrestacao = n.Filial.Cidade.Nome;

                dados.CpfCnpjTomador = n.Pessoa.CNPJ_CPF;
                dados.RazaoSocialTomador = n.Pessoa.Nome;
                dados.InscricaoMunicipalTomador = n.Pessoa.InscricaoMunicipal ?? "";

                //dados.TipoLogradouroTomador = n.Pessoa.EnderecoPrincipal().Endereco;
                dados.EnderecoTomador = n.Pessoa.EnderecoPrincipal().Endereco;
                dados.NumeroTomador = n.Pessoa.EnderecoPrincipal().Numero;
                dados.ComplementoTomador = n.Pessoa.EnderecoPrincipal().Complemento == "" ? "-" : n.Pessoa.EnderecoPrincipal().Complemento;
                dados.TipoBairroTomador = n.Pessoa.EnderecoPrincipal().Bairro;
                dados.BairroTomador = n.Pessoa.EnderecoPrincipal().Bairro;
                dados.CodigoCidadeTomador = n.Pessoa.EnderecoPrincipal().Cidade.IBGE;
                dados.DescricaoCidadeTomador = n.Pessoa.EnderecoPrincipal().Cidade.Nome;
                dados.UfTomador = n.Pessoa.EnderecoPrincipal().Cidade.UF.Sigla;
                dados.CepTomador = n.Pessoa.EnderecoPrincipal().CEP;
                dados.TelefoneTomador = n.Pessoa.TelefonePrincipal;
                dados.EmailTomador = n.Pessoa.PessoaEmails.FirstOrDefault()?.Email ?? "";

                if (n.Intermediario != null)
                {
                    dados.RazaoSocialIntermediario = n.Intermediario.Nome;
                    dados.CpfCnpjIntermediario = n.Intermediario.CNPJ_CPF;
                    dados.InscricaoMunicipalIntermediario = n.Intermediario.InscricaoMunicipal;
                }
                //dados.NumeroNfseSubstituida = ;
                //dados.DataEmissaoNfseSubstituida = ;
                dados.SeriePrestacao = 99;
                //dados.MotCancelamento = ;
                //dados.Operacao = ;
                dados.CodigoObra = item.Servico.CodigoObra;
                dados.Art = item.Servico.Art;
                //dados.DeducaoPor = "";
                //dados.TipoDeducao = "";
                //dados.CPFCNPJReferencia = ;
                //dados.NumeroNfReferencia = ;
                //dados.ValorTotalReferencia = ;
                //dados.PercentualDeduzir = ;
                dados.QuantidadeServicos = Convert.ToInt32(item.Quantidade);
                //dados.ValorUnitarioServico = ;
                //dados.Tributavel = ;
                //dados.IdentificadorSistemaLegado = ;
                //dados.TipoCodificacao = ;
                //dados.Assinatura = ;
                dados.Competencia = n.DtEnvio ?? DateTime.Now;
                dados.IncentivoFiscal = item.Servico.IncentivoFiscal;

                if (n.Filial.Cidade.IBGE == "3550308")
                {
                    switch (n.ResponsavelRetencao)
                    {
                        case 0:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario;
                            break;
                        case 1:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario;
                            break;
                        case 2:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario;
                            break;
                        default:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario;
                            break;
                    }
                }
                else
                {
                    switch (n.ResponsavelRetencao)
                    {
                        case 0:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario + @"|Recolhimento=Prestador";
                            break;
                        case 1:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario + @"|Recolhimento=Tomador";
                            break;
                        case 2:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario + @"|Recolhimento=Intermediário";
                            break;
                        default:
                            dados.DadosImpressao = "OutrasInformacoes=" + n.ObservacaoSistema + " " + n.ObservacaoUsuario + @"|Recolhimento=Prestador";
                            break;
                    }
                }
                //dados.DiscriminacaoServico = $"{dados.DiscriminacaoServico}{Environment.NewLine}{dados.DadosImpressao}";
                ListaRPS.Add(dados);
            }
            #endregion
            return new NFSE(controle, cabecalho, ListaRPS);
        }
    }
}
