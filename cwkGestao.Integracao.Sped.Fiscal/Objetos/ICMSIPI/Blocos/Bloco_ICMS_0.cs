using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Blocos
{
    public class Bloco_ICMS_0 : Bloco
    {
        public Reg_0000 AberturaArquivo { get; set; }
        public Reg_0001 AberturaBloco { get; set; }
        public Reg_0005 DadosComplementaresEntidade { get; set; }
        public Reg_0015 DadosContribuinteSubstituto { get; set; }
        public Reg_0100 DadosContabilista { get; set; }
        public IList<Reg_0150> Participantes { get; set; }
        public IList<Reg_0175> AlteracaoParticipantes { get; set; }
        public IList<Reg_0190> UnidadesMedidaReg { get; set; }
        public IList<Reg_0200> NotaItensReg { get; set; }
        public IList<Reg_0205> AltNotaItensReg { get; set; }
        public IList<Reg_0220> FatoresDeConversao { get; set; }
        public IList<Reg_0400> OperacoesPrestacoes { get; set; }
        public IList<Reg_0450> TextosLei { get; set; }
        public IList<Reg_0500> PlanosDeContaReg { get; set; }
        public IList<Reg_0600> CentrosDeCustoReg { get; set; }
        public Reg_0990 FechamentoBloco { get; set; }

        IList<Nota> notas = new List<Nota>();
        IList<Documento> documentos = new List<Documento>();
        IList<PlanoConta> planosConta = new List<PlanoConta>();
        IList<CentroCusto> centroDeCustos = new List<CentroCusto>();
        Filial filial = new Filial();
        public bool semNota = false;
        private int finalidade;
        IList<ProdutoConversao> produtoConversoes = new List<ProdutoConversao>();

        private void CriarRegistro0000(string CodVersaoLeiaute, DateTime DT_INIInfArq, DateTime DT_FIMInfArq, 
            Filial Filial, int finalidade)
        {
            try
            {
                AberturaArquivo = new Reg_0000();

                AberturaArquivo.codigoVersaoLeiaute = CodVersaoLeiaute;
                AberturaArquivo.codigoFinalidadeArquivo = finalidade;
                AberturaArquivo.dtInicioInformacoesArquivo = DT_INIInfArq;
                AberturaArquivo.dtFimInformacoesArquivo = DT_FIMInfArq;
                AberturaArquivo.nomeEmpresarialPessoaJuridica = Filial.Nome;
                AberturaArquivo.numeroDeInscriçãoDaMatrizPessoaJurídicaCNPJ = Filial.CNPJ;
                AberturaArquivo.numeroDeInscriçãoDaMatrizPessoaJurídicaCPF = String.Empty;
                AberturaArquivo.siglaDaUnidadeDaFederaçãoDaPessoaJuridica = Filial.Cidade.UF.Sigla;
                AberturaArquivo.inscricaoEstadualDaEntidade = Filial.Inscricao;
                AberturaArquivo.codigoDoMunicipioDoDomicilioFiscalDaPessoaJuridica = Filial.Cidade.IBGE;
                AberturaArquivo.inscricaoMunicipalEntidade = Filial.InscricaoMunicipal;
                AberturaArquivo.inscricaoDaPessoaJuridicaNaSuframa = Filial.CodSuframa;
                AberturaArquivo.perfilDeApresentacaoDoArquivoFiscal = Filial.IND_PERFIL;
                AberturaArquivo.indicadorDeTipoDeAtividadePreponderante = Convert.ToInt32(Filial.TipoAtividadePreponderante) > 0 ? "1" : Filial.TipoAtividadePreponderante; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CriarRegistro0001(int IND_MOV)
        {
            try
            {
                AberturaBloco = new Reg_0001();
                AberturaBloco.indicadorMovimento = IND_MOV;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CriarRegistro0005(Filial Filial)
        {
            DadosComplementaresEntidade = new Reg_0005();
            DadosComplementaresEntidade.fantasia = Filial.Fantasia;
            DadosComplementaresEntidade.cep = Filial.CEP;
            DadosComplementaresEntidade.endereco = Filial.Endereco;
            DadosComplementaresEntidade.numImovel = Filial.Numero;
            DadosComplementaresEntidade.complemento = String.Empty;
            DadosComplementaresEntidade.bairro = Filial.Bairro;
            DadosComplementaresEntidade.fone = Filial.Telefone;
            DadosComplementaresEntidade.fax = Filial.Fax;
            DadosComplementaresEntidade.email = Filial.Email;
        }

        private void CriarRegistro0015(Filial filial)
        {
            try
            {
                var dadosST = filial.DadosSubstituto.GroupBy(c => c.Filial.ID);
                foreach (var item in dadosST)
                {
                    DadosContribuinteSubstituto = new Reg_0015();
                    DadosContribuinteSubstituto.uf_st = item.FirstOrDefault().UF_ST;
                    DadosContribuinteSubstituto.ie_st = item.FirstOrDefault().IE_ST;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void CriarRegistro0100(PessoaContabilista PessoaContabilista)
        {
            PessoaContabilista contabilista = PessoaContabilista;
            DadosContabilista = new Reg_0100();
            IList<PessoaContabilista> Contabilistas;
            PessoaEndereco pessoaEndereco;
            PessoaEmail pessoaEmail;
            try
            {
                    Contabilistas = new List<PessoaContabilista>();
                    Contabilistas.Add(contabilista);

                    foreach (var cont in Contabilistas)
                    {
                        if (cont != null)
                        {
                            pessoaEndereco = new PessoaEndereco();
                            pessoaEmail = new PessoaEmail();

                            pessoaEndereco =  cont.Pessoa.PessoaEnderecos.Count > 0 ? cont.Pessoa.PessoaEnderecos.FirstOrDefault() : null;
                            pessoaEmail = cont.Pessoa.PessoaEmails.Count > 0 ? cont.Pessoa.PessoaEmails.FirstOrDefault() : null;

                            DadosContabilista.nomeDoContabilista = cont.Pessoa.Nome;
                            DadosContabilista.numeroInscricaoContabilistaNoCPF = cont.Pessoa.CNPJ_CPF;
                            DadosContabilista.numeroInscricaoContabilistaNoConselhoContabilista = cont.CRC;
                            DadosContabilista.numeroDeInscricaoDoEscritorioDeContabilidadeNoCNPJ = cont.CNPJEscritorio;

                            DadosContabilista.codigoEnderecamentoPostal = pessoaEndereco == null ? "" : pessoaEndereco.CEP;
                            DadosContabilista.logradouroEEnderecoDoImovel = pessoaEndereco == null ? "" : pessoaEndereco.Endereco;
                            DadosContabilista.numDoImovel = pessoaEndereco == null ? "" : pessoaEndereco.Numero;
                            DadosContabilista.dadosComplementaresDoEndereco = pessoaEndereco == null ? "" : pessoaEndereco.Complemento;


                            DadosContabilista.bairroEmQueOImovelEstaSituado = pessoaEndereco == null ? "" : pessoaEndereco.Bairro;
                            DadosContabilista.numeroDoTelefone = pessoaEndereco == null ? "" : pessoaEndereco.Telefone;
                            DadosContabilista.enderecoDoCorreioEletronico = pessoaEmail == null ? "" : pessoaEmail.Email;
                            DadosContabilista.codMunicipioIBGE = pessoaEndereco == null ? "" : pessoaEndereco.Cidade.IBGE;
                        }
                    }
                
            }
            catch (Exception a)
            {               
                throw a;
            }
        }

        private void CriarRegistro0150(IList<Nota> Notas, Filial f)
        {
            Participantes = new List<Reg_0150>();
            PessoaEndereco enderecoPessoa;
            Reg_0150 Participante;
            IEnumerable<IGrouping<int, Nota>> notasPorPessoa = Notas.GroupBy(p => p.Pessoa.ID);

            foreach (var pessoa in notasPorPessoa)
            {
                Participante = new Reg_0150();
                enderecoPessoa = new PessoaEndereco();
                enderecoPessoa = pessoa.FirstOrDefault().Pessoa.PessoaEnderecos.Count > 0 ? pessoa.FirstOrDefault().Pessoa.PessoaEnderecos.FirstOrDefault() : null;
                Participante.codigoDeIdentificacaoDoParticipante = pessoa.FirstOrDefault().Pessoa.Codigo;
                Participante.nomePessoalOuEmpresarialDoParticipante = pessoa.FirstOrDefault().Pessoa.Nome;
                Participante.codigoDoPaisDoParticipante = enderecoPessoa.Cidade.UF.Pais.IBGE;
                if (pessoa.FirstOrDefault().Pessoa.TipoPessoa == "Física")
                    Participante.cpfDoParticipante = pessoa.FirstOrDefault().Pessoa.CNPJ_CPF;
                else
                    Participante.cnpjDoParticipante = pessoa.FirstOrDefault().Pessoa.CNPJ_CPF;
                Participante.codigoDoMunicipio = enderecoPessoa.Cidade.IBGE;
                Participante.logradouroEEnderecoMovel = enderecoPessoa.Endereco;
                Participante.numeroDoImovel = enderecoPessoa.Numero;
                Participante.dadosComplementaresDoEndereco = enderecoPessoa.Complemento;
                Participante.bairroEmQueOImovelEstaSituado = enderecoPessoa.Bairro;
                Participantes.Add(Participante);
            }
            Reg_0150 Empresa = new Reg_0150();
            Empresa.codigoDeIdentificacaoDoParticipante = f.Codigo;
            Empresa.nomePessoalOuEmpresarialDoParticipante = f.Nome;
            Empresa.codigoDoPaisDoParticipante = f.Cidade.UF.Pais.IBGE;
            Empresa.cnpjDoParticipante = f.CNPJ;
            Empresa.codigoDoMunicipio = f.Cidade.IBGE;
            Empresa.logradouroEEnderecoMovel = f.Endereco;
            Empresa.numeroDoImovel = f.Numero;
            Empresa.bairroEmQueOImovelEstaSituado = f.Bairro;
            Participantes.Add(Empresa);
        }

        private void CriarRegistro0175(IList<LogAlteracaoParticipante> logAlteracaoDadosParticipante)
        {
            AlteracaoParticipantes = new List<Reg_0175>();
            Reg_0175 altParticipante;

            foreach (var item in logAlteracaoDadosParticipante)
            {
                altParticipante = new Reg_0175();
                altParticipante.DT_ALT = item.DataAlteracao;
                altParticipante.NR_CAMPO = item.NumeroCampoAlterado;
                altParticipante.CONT_ANT = item.ConteudoAnterior;
                AlteracaoParticipantes.Add(altParticipante);
            }
        }

        private void CriarRegistro0190(IList<Nota> Notas)
        {
            try
            {
                UnidadesMedidaReg = new List<Reg_0190>();
                Reg_0190 UnidadeMedidoReg;
                List<NotaItem> itemProdNotas = new List<NotaItem>();

                foreach (Nota n in Notas.Where(w => w.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Produtos))
                {
                    itemProdNotas.AddRange(n.NotaItems);
                }

                var gprod = itemProdNotas.GroupBy(g => g.Produto.UnidadeEntrada.ID);

                foreach (var item in gprod)
                {
                    UnidadeMedidoReg = new Reg_0190();

                 
                    UnidadeMedidoReg.codigoDeIUnidadeDeMedida = item.FirstOrDefault().Produto.UnidadeEntrada.Sigla;
                    UnidadeMedidoReg.descricaoDaUnidadeDeMedida = item.FirstOrDefault().Produto.UnidadeEntrada.Nome;
                    UnidadesMedidaReg.Add(UnidadeMedidoReg);                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        private void CriarRegistro0200(IList<Nota> notas, IList<NCM> listaNCM)
        {
            var notaItensIE = (notas.SelectMany(ni => ni.NotaItems));
            var notaServicosIE = (notas.SelectMany(ns => ns.NotaItemsServicos));
            try
            {
                NotaItensReg = new List<Reg_0200>();
                Reg_0200 NotaItemReg;

                IEnumerable<IGrouping<int, NotaItem>> produtos;
                IEnumerable<IGrouping<int, NotaItemServico>> servicos;

                if (notaItensIE.Count() > 0)
                    produtos = (notaItensIE.ToList()).GroupBy(p => p.Produto.ID);
                else
                    produtos = null;
                if (notaServicosIE.Count() > 0)
                    servicos = notaServicosIE.ToList().GroupBy(s => s.Servico.ID);
                else
                    servicos = null;
                if (produtos != null)
                {
                    foreach (var prod in produtos)
                    {
                        NotaItem p = prod.FirstOrDefault();
                        try
                        {
                            NotaItemReg = new Reg_0200();
                            NotaItemReg.codigoDoIten = prod.FirstOrDefault().CodigoProduto;
                            NotaItemReg.descricaoDoItem = prod.FirstOrDefault().Produto.Nome;
                            NotaItemReg.representacaoAlfanumericoDoCodigoDeBarraDoProduto = prod.FirstOrDefault().Produto.ProdutoCodBarra.FirstOrDefault() == null
                                                                                            ? "" : prod.FirstOrDefault().Produto.ProdutoCodBarra.FirstOrDefault().CodigoBarra;
                            NotaItemReg.unidadeDeMedidaUtilizadaNaQuantificacaoDeEstoques = prod.FirstOrDefault().Produto.UnidadeEntrada.Sigla;
                            NotaItemReg.codigoDaNomenclaturaComumDoMercosul = prod.FirstOrDefault().Produto.NCM;
                            NotaItemReg.tipoDoItem = prod.FirstOrDefault().Produto.GrupoEstoque.CodClassProduto;
                            NotaItemReg.aliquotaDeIcmsAplicavel = BuscaAliquota(prod, listaNCM);
                            NotaItemReg.codigoDoGeneroDoItem = "";
                            NotaItemReg.codigoDoServico = "";
                            NotaItensReg.Add(NotaItemReg);
                        }
                        catch (Exception e)
                        {

                            throw e;
                        }
                    }
                }
                if (servicos != null)
                {
                    foreach (var serv in servicos)
                    {
                        try
                        {
                            NotaItemReg = new Reg_0200();
                            NotaItemReg.descricaoDoItem = serv.FirstOrDefault().Servico.Descricao;
                            NotaItemReg.codigoDoIten = Convert.ToString(serv.FirstOrDefault().CodigoServico);
                            NotaItemReg.tipoDoItem = "09";
                            NotaItemReg.codigoDoGeneroDoItem = "00";
                            NotaItemReg.codigoDoServico = "";
                            NotaItensReg.Add(NotaItemReg);
                        }
                        catch (Exception e)
                        {

                            throw e;
                        }
                    }
                }
                
            }
            catch (Exception x)
            {
                
                throw x;
            }
        }

        private decimal BuscaAliquota(IGrouping<int,NotaItem> prod, IList<NCM> listaNCM)
        {
            try
            {
                NCM ncm = listaNCM.Where(n => n.Ncm == prod.FirstOrDefault().Produto.NCM).FirstOrDefault();
                NCMItem ncmItem = ncm.NCMItens.Where(c => ((c.UFOrigem == c.UFDestino) && 
                                                    (c.UFOrigem == prod.FirstOrDefault().Nota.Filial.Cidade.UF))).FirstOrDefault();
                return ncmItem.AliqContrib;
            }
            catch (Exception)
            {
                return 0;
            }          
        }



        private void CriarRegistro0205(IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem)
        {
            try
            {
                AltNotaItensReg = new List<Reg_0205>();
                Reg_0205 AltNotaItemReg;
                var Alteracoes = logAlteracaoPedidoItem.GroupBy(c => c.Codigo);
                foreach (var logAlt in Alteracoes)
                {
                    AltNotaItemReg = new Reg_0205();
                    AltNotaItemReg.descricaoAnteriorDoItem = logAlt.FirstOrDefault().DescricaoAnterior;
                    AltNotaItemReg.codigoAnteriorDoItemComRelacaoAUltimaInformacaoApresentada = (Convert.ToString(logAlt.FirstOrDefault().CodigoAnterior));
                    AltNotaItemReg.dataInicialUtilizacaoItem = logAlt.FirstOrDefault().DataInicialDesc;
                    AltNotaItemReg.dataFinalUtilizacaoItem = logAlt.FirstOrDefault().DataFinalDesc;
                    AltNotaItensReg.Add(AltNotaItemReg);
                }
            }
            catch (Exception ex)
            {              
                throw ex;
            }           
        }

        private void CriarRegistro0220(IList<Nota> Notas, IList<ProdutoConversao> produtoConversoes)
        {
            try
            {
                FatoresDeConversao = new List<Reg_0220>();
                Reg_0220 FatorConversao;
                List<ProdutoConversao> conversoesDoProduto = new List<ProdutoConversao>();

                foreach (Nota n in Notas.Where(w => w.TipoNota.Categoria == TipoNota.CategoriaMovimentacao.Produtos))
                {                    
                    foreach (var item in n.NotaItems)
                    {
                        conversoesDoProduto.AddRange(produtoConversoes.Where(c => c.Produto.ID == item.Produto.ID));
                    }
                }

                var gprod = conversoesDoProduto.GroupBy(g => g.Produto.UnidadeEntrada.ID);

                foreach (var item in gprod)
                {
                    FatorConversao = new Reg_0220();

                    FatorConversao.unidadeASerConvertida = item.FirstOrDefault().ConversaoUnidade.UnidadeEnt.Sigla;
                    FatorConversao.fatorConversao = (Convert.ToString(item.FirstOrDefault().ConversaoUnidade.FatorConversao));
                    FatoresDeConversao.Add(FatorConversao);                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        private void CriarRegistro0400(IList<Documento> documentos)
        {
            try
            {
                OperacoesPrestacoes = new List<Reg_0400>();
                //Reg_0400 OperacaoPrestacao;
                //var documentosPorCondicao = documentos.GroupBy(cond => cond.Condicao.ID);
                //foreach (var documento in documentosPorCondicao)
                //    {
                //        OperacaoPrestacao = new Reg_0400();
                //        OperacaoPrestacao.codigoDaNaturezaDaOperacaoOuPrestacao = documento.FirstOrDefault().Condicao.Codigo;
                //        OperacaoPrestacao.descricaoDaNaturezaDaOperacaoOuPrestacao = documento.FirstOrDefault().Condicao.Nome;
                //        OperacoesPrestacoes.Add(OperacaoPrestacao);
                //    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void CriarRegistro0450(IList<Nota> Notas)
        {
            try
            {
                TextosLei = new List<Reg_0450>();
                var NotasPorObs = Notas.GroupBy(g => g.ObservacaoSistema);
                int counter = 0;
                foreach (var item in NotasPorObs)
                {
                    counter++;
                    TextosLei.Add(new Reg_0450()
                    {
                        codigoDaInformacaoComplementarDoDocumento = counter.ToString(),
                        textoLivreDaInformacaoComplementarExistenteNoDocumentoFiscal = item.Key,
                        NotasDoTextoLei = item.ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void CriarRegistro0500(IList<PlanoConta> PlanosConta)
        {
            try
            {
                PlanosDeContaReg = new List<Reg_0500>();
                Reg_0500 PlanoDeContaReg;

                foreach (var plConta in PlanosConta.Where(r => r.PlanoContaReferencial != null))
                {
                    PlanoDeContaReg = new Reg_0500();
                    PlanoDeContaReg.datainclusaoOuAlteracao = Convert.ToDateTime(plConta.IncData);
                    PlanoDeContaReg.codigoDaNaturezaDaContaOuGrupoDeContas = plConta.Cod_Nat_CC;
                    PlanoDeContaReg.indicadorTipoConta = plConta.PlanoContaReferencial.tipo.Trim();
                    PlanoDeContaReg.nivelContaAnaliticaOuGrupoDeContas = (plConta.Classificacao.Split('.')).Length;
                    PlanoDeContaReg.codigoContaAnaliticaOuGrupoDeContas = plConta.Codigo;
                    PlanoDeContaReg.nomeDaContaAnaliticaOuGrupoDeContas = plConta.Nome;
                    PlanosDeContaReg.Add(PlanoDeContaReg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        private void CriarRegistro0600(IList<CentroCusto> CentroDeCustos)
        {
            try
            {
                CentrosDeCustoReg = new List<Reg_0600>();
                Reg_0600 CentroDeCustoReg;

                foreach (var item in CentroDeCustos)
                {
                    CentroDeCustoReg = new Reg_0600();
                    CentroDeCustoReg.datainclusaoOuAlteracao = Convert.ToDateTime(item.IncData);
                    CentroDeCustoReg.codigoCentroDeCustos = item.Codigo;
                    CentroDeCustoReg.nomeCentroDeCustos = item.Nome;
                    CentrosDeCustoReg.Add(CentroDeCustoReg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        private void CriarRegistro0990()
        {
            try
            {
                FechamentoBloco = new Reg_0990();
                FechamentoBloco.QuantidadeTotalDeLinhasDoBloco0 = GetContagemRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public Bloco_ICMS_0(IList<Nota> Notas, Filial Filial, string CodVersaoLeiaute, int TipoEscrituracao, string Num_Rec_Anterior, DateTime DT_INIInfArq,
                       DateTime DT_FIMInfArq, int IndicadorMovimento, IList<DateTime> MesesDispensa, IList<Documento> Documentos,
                       IList<PlanoConta> PlanosConta, IList<CentroCusto> CentroDeCustos, bool SemNota, PessoaContabilista Contabilista,
                       IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem, int Finalidade, IList<ProdutoConversao> ProdutoConversoes, 
                       IList<LogAlteracaoParticipante> logAlteracaoDadosParticipante, IList<NCM> listaNCM)
        {
            try
            {
                notas = Notas;
                documentos = Documentos;
                filial = Filial;
                planosConta = PlanosConta;
                centroDeCustos = CentroDeCustos;
                semNota = SemNota;
                finalidade = Finalidade;
                produtoConversoes = ProdutoConversoes;
                this.GerarBloco(notas, documentos, filial, planosConta, CodVersaoLeiaute, TipoEscrituracao, IndicadorMovimento, Num_Rec_Anterior,
                                DT_INIInfArq, DT_FIMInfArq, centroDeCustos, planosConta, MesesDispensa, Contabilista, logAlteracaoPedidoItem,
                                finalidade, ProdutoConversoes, logAlteracaoDadosParticipante, listaNCM); 
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        private void GerarBloco(IList<Nota> notas, IList<Documento> documentos, Filial filial, IList<PlanoConta> planosConta, string CodVersaoLeiaute, 
                                int TipoEscrituracao, int indicadorMovimento, string Num_Rec_Anterior, DateTime DT_INIInfArq, DateTime DT_FIMInfArq,
                                IList<CentroCusto> centroDeCustos, IList<PlanoConta> planosConta_11, IList<DateTime> MesesDispensa,
                                PessoaContabilista Contabilista, IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem, int finalidade, 
                                IList<ProdutoConversao> produtoConversoes, IList<LogAlteracaoParticipante> logAlteracaoDadosParticipante,
                                IList<NCM> listaNCM)
        {
            CriarRegistro0000(CodVersaoLeiaute, DT_INIInfArq, DT_FIMInfArq, filial, finalidade);
            CriarRegistro0001(indicadorMovimento);
            CriarRegistro0005(filial);
            CriarRegistro0015(filial);
            CriarRegistro0100(Contabilista);
            CriarRegistro0150(notas, filial);
            CriarRegistro0175(logAlteracaoDadosParticipante);
            CriarRegistro0190(notas);
            CriarRegistro0200(notas, listaNCM);
            CriarRegistro0205(logAlteracaoPedidoItem);
            CriarRegistro0220(notas, produtoConversoes);
            CriarRegistro0400(documentos);
            CriarRegistro0450(notas);
            CriarRegistro0500(planosConta);
            CriarRegistro0600(centroDeCustos);
            CriarRegistro0990();
        }


        public override void GerarBloco()
        {

        }

    }
}