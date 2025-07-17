//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Xml.Linq;
//using OpenAC.Net.Core.Extensions;
//using OpenAC.Net.Core.Logging;
//using OpenAC.Net.NFSe;
//using OpenAC.Net.NFSe.Nota;
//using OpenAC.Net.NFSe.Providers;

//using cwkGestao.Integracao.NFSe.Classes;
//using OpenAC.Net.NFSe.DANFSe.FastReport;
//using DadosTomador = cwkGestao.Integracao.NFSe.Classes.DadosTomador;
//using System.Xml.Serialization;
//using OpenAC.Net.DFe.Core.Common;

//namespace cwkGestao.Integracao.NFSe
//{
//    public class NFSeGenerator : IOpenLog
//    {
//        private PrestadorNFSe Prestador { get; set; }
//        private readonly OpenNFSe acbrNFSe;
//        private readonly ACBrConfig config;
//        private ItemData<OpenMunicipioNFSe>[] Municipios =>
//            (from OpenMunicipioNFSe value in ProviderManager.Municipios
//             select new ItemData<OpenMunicipioNFSe>($"{value.Nome} - {value.UF}", value)).ToArray();
        
//        public NFSeGenerator(string StartupPath, PrestadorNFSe Prestador)
//        {
//            this.Prestador = Prestador;
//            acbrNFSe = new OpenNFSe();

//            config = ACBrConfig.CreateOrLoad(Path.Combine(StartupPath, "nfse.config"));
//            SaveConfig();
//            ProviderManager.Load(Prestador.ArquivoCidades);
//        }

//        #region DadosPrestadorNFSe
//        private void SaveConfig()
//        {
//            config.Set("PrestadorCPFCNPJ", Prestador.CPFCNPJ);
//            config.Set("PrestadorIM", Prestador.IM.OnlyNumbers());
//            config.Set("PrestadorRazaoSocial", Prestador.RazaoSocial);
//            config.Set("PrestadorFantasia", Prestador.Fantasia);
//            config.Set("PrestadorFone", Prestador.Fone);
//            config.Set("PrestadorCEP", Prestador.CEP);
//            config.Set("PrestadorEndereco", Prestador.Endereco);
//            config.Set("PrestadorNumero", Prestador.Numero);
//            config.Set("PrestadorComplemento", Prestador.Complemento);
//            config.Set("PrestadorBairro", Prestador.Bairro);
//            //config.Set("Municipio", Prestador.Municipio.ToString().OnlyNumbers());
//            config.Set("Ambiente", Prestador.Ambiente);
//            config.Set("Senha", Prestador.Senha);
//            config.Set("NumeroSerie", Prestador.NumeroSerie);
//            //config.Set("Logo",);
//            acbrNFSe.Configuracoes.Arquivos.PathSchemas = Prestador.DiretorioSchemas;
//            acbrNFSe.Configuracoes.Geral.Salvar = Prestador.SalvarArquivos;
//            acbrNFSe.Configuracoes.Arquivos.ArquivoServicos = Prestador.ArquivoCidades;
//            acbrNFSe.Configuracoes.Arquivos.PathSalvar = Prestador.PathXml;

//            acbrNFSe.Configuracoes.WebServices.CodigoMunicipio = Convert.ToInt32(Prestador.Municipio.ToString().OnlyNumbers());
//            acbrNFSe.Configuracoes.Certificados.Certificado = Prestador.NumeroSerie;
//            config.Save();
//        }

//        #endregion
//        public void GerarRps(IdentificacaoRps IdentificacaoRps,
//                             DadosTomador Tomador,
//                             string CodigoTributacaoMunicipio,
//                             string Discriminacao,
//                             decimal ValorCredito,
//                             int NumeroLote,
//                             List<DadosServicoItem> Servicos,
//                             TotalizadorServico Totalizadores,
//                             string Observacao)
//        {
//            var municipio = Municipios.FirstOrDefault(o => o.Content.Codigo == Prestador.Municipio);
//            if (municipio == null) return;

//            acbrNFSe.NotasServico.Clear();
//            var nfSe = acbrNFSe.NotasServico.AddNew();

//            nfSe.NumeroLote = NumeroLote;
//            nfSe.IdentificacaoNFSe.DataEmissao = DateTime.Now;
//            nfSe.IdentificacaoRps.DataEmissao = DateTime.Now;
//            nfSe.Competencia = DateTime.Now;
//            nfSe.IdentificacaoNFSe.Numero = IdentificacaoRps.Numero;
//            nfSe.IdentificacaoRps.Numero = IdentificacaoRps.Numero;
//            nfSe.IdentificacaoRps.Serie = IdentificacaoRps.Serie;
//            nfSe.IdentificacaoRps.Tipo = IdentificacaoRps.Tipo;
//            nfSe.IdentificacaoRps.DataEmissao = IdentificacaoRps.DataEmissao;
//            nfSe.Situacao = SituacaoNFSeRps.Normal;
//            nfSe.NaturezaOperacao = NaturezaOperacao.ABRASF.TributacaoNoMunicipio;
//            nfSe.RegimeEspecialTributacao = Servicos.FirstOrDefault()?.RegimeEspecialTributacao ?? RegimeEspecialTributacao.Nenhum;
//            nfSe.IncentivadorCultural = NFSeSimNao.Nao;

//            #region DadosTomadorNfse

//            nfSe.Tomador.CpfCnpj = Tomador.CpfCnpj;
//            nfSe.Tomador.InscricaoMunicipal = Tomador.InscricaoMunicipal;
//            nfSe.Tomador.RazaoSocial = Tomador.RazaoSocial;
//            nfSe.Tomador.Endereco.TipoLogradouro = Tomador.Endereco.TipoLogradouro;
//            nfSe.Tomador.Endereco.Logradouro = Tomador.Endereco.Logradouro;
//            nfSe.Tomador.Endereco.Numero = Tomador.Endereco.Numero;
//            nfSe.Tomador.Endereco.Complemento = Tomador.Endereco.Complemento;
//            nfSe.Tomador.Endereco.Bairro = Tomador.Endereco.Bairro;
//            nfSe.Tomador.Endereco.CodigoMunicipio = Tomador.Endereco.CodigoMunicipio;
//            nfSe.Tomador.Endereco.Municipio = Tomador.Endereco.Municipio;
//            nfSe.Tomador.Endereco.Uf = Tomador.Endereco.Uf;
//            nfSe.Tomador.Endereco.Cep = Tomador.Endereco.Cep;
//            nfSe.Tomador.Endereco.CodigoPais = Tomador.Endereco.CodigoPais;
//            nfSe.Tomador.Endereco.Pais = Tomador.Endereco.Pais;
//            nfSe.Tomador.DadosContato.DDD = Tomador.DadosContato.DDD;
//            nfSe.Tomador.DadosContato.Telefone = Tomador.DadosContato.Telefone;
//            nfSe.Tomador.DadosContato.Email = Tomador.DadosContato.Email;

//            nfSe.Prestador.RazaoSocial = Prestador.RazaoSocial;
//            nfSe.Prestador.CpfCnpj = Prestador.CPFCNPJ;
//            nfSe.Prestador.NomeFantasia = Prestador.Fantasia;
//            nfSe.Prestador.Endereco.Logradouro = Prestador.Endereco;
//            nfSe.Prestador.Endereco.Numero = Prestador.Numero;
//            nfSe.Prestador.Endereco.Complemento = Prestador.Complemento;
//            nfSe.Prestador.Endereco.Bairro = Prestador.Bairro;
//            nfSe.Prestador.Endereco.CodigoMunicipio = Prestador.Municipio;
//            nfSe.Prestador.Endereco.CodigoPais = Tomador.Endereco.CodigoPais;
//            nfSe.Prestador.Endereco.Pais = Tomador.Endereco.Pais;
//            nfSe.Prestador.InscricaoMunicipal = Prestador.IM;
//            nfSe.Prestador.DadosContato.Telefone = Prestador.Fone;

//            #endregion

//            #region DadosServiçoNFSe

//            nfSe.Servico.ItemListaServico = Servicos.First().ItemListaServico; //municipio.Content.Provedor.IsIn(NFSeProvider.Betha, NFSeProvider.ISSe) ? "0107" : "01.07";
//                                                                               //nfSe.Servico.Descricao = Discriminacao;
//                                                                               // nfSe.Servico.Descricao = Descricao;
//            nfSe.Servico.CodigoTributacaoMunicipio = CodigoTributacaoMunicipio; //"01.07.00 / 00010700";
//            nfSe.Servico.CodigoCnae = "";
//            nfSe.Servico.CodigoMunicipio = municipio.Content.Codigo;
//            nfSe.Servico.Discriminacao = Discriminacao; //"MANUTENCAO TÉCNICA / VOCÊ PAGOU APROXIMADAMENTE R$ 41,15 DE TRIBUTOS FEDERAIS, R$ 8,26 DE TRIBUTOS MUNICIPAIS, R$ 256,57 PELOS PRODUTOS/SERVICOS, FONTE: IBPT.";
//            nfSe.Servico.ExigibilidadeIss = ExigibilidadeIss.Exigivel;
//            nfSe.Servico.Valores.ValorServicos = Totalizadores.ValorServicos;
//            nfSe.Servico.Valores.ValorDeducoes = Totalizadores.ValorDeducoes;
//            nfSe.Servico.Valores.ValorPis = Totalizadores.ValorPis;
//            nfSe.Servico.Valores.ValorCofins = Totalizadores.ValorCofins;
//            nfSe.Servico.Valores.ValorInss = Totalizadores.ValorInss;
//            nfSe.Servico.Valores.ValorIr = Totalizadores.ValorIr;
//            nfSe.Servico.Valores.ValorCsll = Totalizadores.ValorCsll;
//            //nfSe.Servico.Valores.IssRetido = Totalizadores.IssRetido;
//            nfSe.Servico.Valores.BaseCalculo = Totalizadores.BaseCalculo;
//            nfSe.Servico.Valores.Aliquota = Totalizadores.Aliquota;
//            nfSe.Servico.Valores.ValorIss = Totalizadores.ValorIss;
//            nfSe.Servico.Valores.ValorLiquidoNfse = Totalizadores.ValorLiquidoNfse;
//            nfSe.Servico.Valores.ValorOutrasRetencoes = Totalizadores.ValorOutrasRetencoes;
//            //nfSe.Servico.Valores.ValorIssRetido = Totalizadores.ValorIssRetido;
//            //nfSe.Servico.Valores.DescontoCondicionado = Totalizadores.DescontoCondicionado;
//            //nfSe.Servico.Valores.DescontoIncondicionado = Totalizadores.DescontoIncondicionado;
//            //nfSe.ValorCredito = ValorCredito;

//            nfSe.OutrasInformacoes = Observacao;

//            #endregion

//            #region DiscriminaçãoServiços

//            #endregion

//            #region ValoresDaNota

//            foreach (var ItemServico in Servicos)
//            {
//                nfSe.RegimeEspecialTributacao = ItemServico?.RegimeEspecialTributacao ?? RegimeEspecialTributacao.Nenhum;

//                var Serv = nfSe.Servico.ItensServico.AddNew();
//                Serv.Codigo = ItemServico.Codigo;

//                Serv.AlicotaIssst = ItemServico.AlicotaIssst;
//                Serv.Unidade = ItemServico.Unidade;
//                Serv.CodLcServ = ItemServico.CodLcServ;
//                Serv.CodServ = ItemServico.CodServ;

//                Serv.ValorCsll = ItemServico.ValorCsll;
//                Serv.ValorIr = ItemServico.ValorIr;
//                Serv.ValorInss = ItemServico.ValorInss;
//                Serv.ValorCofins = ItemServico.ValorCofins;
//                Serv.ValorPis = ItemServico.ValorPis;

//                Serv.Tributavel = ItemServico.Tributavel;
//                Serv.Discriminacao = ItemServico.Discriminacao;
//                Serv.DescontoIncondicionado = ItemServico.DescontoIncondicionado;
//                Serv.DescontoCondicionado = ItemServico.DescontoCondicionado;

//                Serv.BaseCalculo = ItemServico.BaseCalculo;

//                if (nfSe.RegimeEspecialTributacao == RegimeEspecialTributacao.SociedadeProfissionais)
//                {
//                    Serv.Aliquota = 0;
//                    Serv.ValorIss = 0;
//                }
//                else
//                {
//                    Serv.Aliquota = ItemServico.Aliquota;
//                    Serv.ValorIss = ItemServico.ValorIss;
//                }

//                Serv.ValorDeducoes = ItemServico.ValorDeducoes;
//                Serv.ValorServicos = ItemServico.ValorServicos;
//                Serv.ValorTotal = ItemServico.ValorTotal;
//                Serv.ValorUnitario = ItemServico.ValorUnitario;
//                Serv.ItemListaServico = ItemServico.ItemListaServico;
//                Serv.Quantidade = ItemServico.Quantidade;
//                Serv.Descricao = ItemServico.Descricao;
//                Serv.ValorIssst = ItemServico.ValorIssst;
//            }

//            #endregion

//            #region OutrasInformações

//            #endregion

//        }
//        public RetornoWebservice EnviarRPS(int Numero)
//        {
//            var ret = acbrNFSe.Enviar(Numero, true);
//            return ret;
//        }

//        public T DeserializeObject<T>(string xml) where T : class
//        {
//            if (string.IsNullOrEmpty(xml))
//                throw new InvalidOperationException("Empty XML ERROR");

//            using (var stringReader = new StringReader(xml))
//            {
//                var serializer = new XmlSerializer(typeof(T));
//                return (T)serializer.Deserialize(stringReader);
//            }
//        }

//        public RetornoCancelar Cancelar(string CodigoCancelamento, string NumeroNFSe, string Motivo)
//        {
//            return acbrNFSe.CancelarNFSe(CodigoCancelamento, NumeroNFSe, Motivo);
//        }

//        public void Imprimir(bool ImprimirPDF)
//        {
//            var arquivoExe = Assembly.GetEntryAssembly().Location;
//            var caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
//            var d = new DANFSeFastReport(acbrNFSe.Configuracoes);
//            d.Configuracoes.MostrarPreview = true;
//            d.Configuracoes.Logo = Image.FromFile(caminho + "\\LogoEmpresa.jpg", true);
//            d.Configuracoes.LogoPrefeitura = Image.FromFile(caminho + "\\LogoPrefeitura.jpg", true);

//            if (ImprimirPDF)
//                d.Imprimir(acbrNFSe.NotasServico.ToArray());

//            var ms = new MemoryStream();

//            d.ImprimirPDF(acbrNFSe.NotasServico.ToArray(), ms);
//            File.WriteAllBytes(caminho + $@"\PdfNFSe\NFSe{acbrNFSe.NotasServico.First().IdentificacaoNFSe.Numero}.pdf", ms.ToArray());
//        }

//        public void Imprimir(string vsFile, string MunicipioTomador, bool AmbienteProdutivo)
//        {
//            acbrNFSe.NotasServico.Clear();
//            acbrNFSe.NotasServico.Load(vsFile);

//            foreach (var item in acbrNFSe.NotasServico)
//                item.Tomador.Endereco.Municipio = MunicipioTomador;

//            var arquivoExe = Assembly.GetEntryAssembly().Location;
//            var caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
//            var d = new DANFSeFastReport(acbrNFSe.Configuracoes);
//            d.Configuracoes.MostrarPreview = true;
//            d.Configuracoes.Logo = Image.FromFile(caminho + "\\LogoEmpresa.jpg", true);
//            d.Configuracoes.LogoPrefeitura = Image.FromFile(caminho + "\\LogoPrefeitura.jpg", true);

//            //d.Imprimir(acbrNFSe.NotasServico.ToArray());

//            var ms = new MemoryStream();

//            // Aqui
//            if (acbrNFSe.NotasServico[0].Prestador.Endereco.CodigoMunicipio == 4115200)
//            {
//                using (var client = new System.Net.WebClient())
//                {
//                    var caminhoArquivo = caminho + $@"\PdfNFSe\NFSe{acbrNFSe.NotasServico.First().IdentificacaoNFSe.Numero}.pdf";

//                    if (AmbienteProdutivo)
//                        client.DownloadFile($"https://nfse.ecity.maringa.pr.gov.br/ImprimirNfse/{acbrNFSe.NotasServico[0].IdentificacaoNFSe.Numero}/{acbrNFSe.NotasServico[0].Prestador.CpfCnpj}/{acbrNFSe.NotasServico[0].IdentificacaoNFSe.Chave}", caminhoArquivo);
//                    else
//                        client.DownloadFile($"https://nfse.hom-ecity.maringa.pr.gov.br/ImprimirNfse/{acbrNFSe.NotasServico[0].IdentificacaoNFSe.Numero}/{acbrNFSe.NotasServico[0].Prestador.CpfCnpj}/{acbrNFSe.NotasServico[0].IdentificacaoNFSe.Chave}", caminhoArquivo);

//                    if (File.Exists(caminhoArquivo))
//                        System.Diagnostics.Process.Start(caminhoArquivo);
//                }
//            }
//            else
//            {
//                d.ImprimirPDF(acbrNFSe.NotasServico.ToArray(), ms);
//                File.WriteAllBytes(caminho + $@"\PdfNFSe\NFSe{acbrNFSe.NotasServico.First().IdentificacaoNFSe.Numero}.pdf", ms.ToArray());
//            }
//        }
//    }
//}
