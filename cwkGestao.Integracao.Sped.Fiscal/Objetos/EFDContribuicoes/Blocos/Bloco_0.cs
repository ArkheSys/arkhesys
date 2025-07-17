using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_0 : Bloco
    {
        public Reg_0000 AberturaArquivo { get; set; }
        public Reg_0001 AberturaBloco { get; set; }
        public Reg_0100 DadosContabilista { get; set; }
        public Reg_0110 RegimesDeApuracaoDaContribuicaoSocial { get; set; }
        public IList<Reg_0120> IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscalList { get; set; }
        public Reg_0140 TabelaCadastroEstabelecimento { get; set; }
        public IList<Reg_0150> Participantes { get; set; }
        public IList<Reg_0190> UnidadesMedidaReg { get; set; }
        public IList<Reg_0200> NotaItensReg { get; set; }
        public IList<Reg_0205> AltNotaItensReg { get; set; }
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

        private void CriarRegistro0000(string CodVersaoLeiaute, int TipoEscrituracao, string Num_Rec_Anterior, DateTime DT_INIInfArq, DateTime DT_FIMInfArq,
            Filial Filial)
        {
            try
            {
                AberturaArquivo = new Reg_0000();

                AberturaArquivo.codigoVersaoLeiaute = CodVersaoLeiaute;
                AberturaArquivo.tipoDeEscrituracao = TipoEscrituracao;
                if (TipoEscrituracao == 1)
                    AberturaArquivo.numeroDoReciboDeEscrituracaoAnteriorASerRetificada = Num_Rec_Anterior;
                AberturaArquivo.dtInicioInformacoesArquivo = DT_INIInfArq;
                AberturaArquivo.dtFimInformacoesArquivo = DT_FIMInfArq;
                AberturaArquivo.nomeEmpresarialPessoaJuridica = Filial.Nome;
                AberturaArquivo.numeroDeInscriçãoDaMatrizPessoaJurídicaCNPJ = Filial.CNPJ;
                AberturaArquivo.siglaDaUnidadeDaFederaçãoDaPessoaJuridica = Filial.Cidade.UF.Sigla;
                AberturaArquivo.codigoDoMunicipioDoDomicilioFiscalDaPessoaJuridica = Filial.Cidade.IBGE;
                AberturaArquivo.inscricaoDaPessoaJuridicaNaSuframa = Filial.CodSuframa;
                AberturaArquivo.indicadorDaNaturezaDaPessoaJuridica = Filial.IND_NAT_PJ;
                AberturaArquivo.indicadorDeTipoDeAtividadePreponderante = Filial.TipoAtividadePreponderante;
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

                        pessoaEndereco = cont.Pessoa.PessoaEnderecos.Count > 0 ? cont.Pessoa.PessoaEnderecos.FirstOrDefault() : null;
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

        private void CriarRegistro0110(Filial Filial)
        {
            try
            {
                RegimesDeApuracaoDaContribuicaoSocial = new Reg_0110();
                RegimesDeApuracaoDaContribuicaoSocial.codigoIndicadorIncidenciaTributaria = Filial.CodigoIndicadorIncidenciaTributaria;
                RegimesDeApuracaoDaContribuicaoSocial.codigoIndicadorDeMetodoDeApropriacaoCreditosComuns = Filial.CodigoIndicadorMetodoApropCreditos;
                if (Filial.CodigoIndicadorTipoContribuicaoApuradaPeriodo == "-1")
                {
                    RegimesDeApuracaoDaContribuicaoSocial.codigoIndicadorTipoContribuicaoApuradaPeriodo = "";
                }
                else
                {
                    RegimesDeApuracaoDaContribuicaoSocial.codigoIndicadorTipoContribuicaoApuradaPeriodo = Filial.CodigoIndicadorTipoContribuicaoApuradaPeriodo;
                }

                if (Filial.CodigoIndicadorCriterioEscrituracaoRegimeCumulativo == "-1")
                {
                    RegimesDeApuracaoDaContribuicaoSocial.codigoIndicadorCriterioEscrituracaoRegimeCumulativo = "";
                }
                else
                {
                    RegimesDeApuracaoDaContribuicaoSocial.codigoIndicadorCriterioEscrituracaoRegimeCumulativo = Filial.CodigoIndicadorCriterioEscrituracaoRegimeCumulativo;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CriarRegistro0120(IList<DateTime> MesesDispensa)
        {
            try
            {
                Reg_0120 IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscal;
                IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscalList = new List<Reg_0120>();
                foreach (var mesDispensa in MesesDispensa)
                {
                    IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscal = new Reg_0120();
                    IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscal.mesReferênciaAnoDaEscrituracaoDispensadaEntrega = mesDispensa;
                    IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscalList.Add(IdentificacaoDePeriodosDispensadosDaEscrituracaoFiscal);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CriarRegistro0140(Filial Filial)
        {
            try
            {
                TabelaCadastroEstabelecimento = new Reg_0140();
                TabelaCadastroEstabelecimento.codigoDeIdentificacaoDoEstabelecimento = filial.Codigo;
                TabelaCadastroEstabelecimento.nomeEmpresarialDoEstabelecimento = filial.Nome;
                TabelaCadastroEstabelecimento.numeroDeInscricaoDoEstabelecimentoNoCNPJ = Filial.CNPJ;
                TabelaCadastroEstabelecimento.siglaDaUnidadeDaFederaçãoDoEstabelecimento = Filial.Cidade.UF.Sigla;
                TabelaCadastroEstabelecimento.codigoDoMunicipioDoDomicilioFiscalDoEstabelecimento = Filial.Cidade.IBGE;
                TabelaCadastroEstabelecimento.inscricaoDoEstabelecimentoNaSuframa = Filial.CodSuframa;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" || cpf == "11111111111")
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private void CriarRegistro0150(IList<Nota> Notas, Filial f)
        {
            Participantes = new List<Reg_0150>();
            PessoaEndereco enderecoPessoa;
            Reg_0150 Participante;
            IEnumerable<IGrouping<int, Nota>> notasPorPessoa = Notas
                .Where(o => !string.IsNullOrEmpty(o.Pessoa?.CNPJ_CPF))
                .GroupBy(p => p.Pessoa.ID);

            foreach (var pessoa in notasPorPessoa)
            {
                Participante = new Reg_0150();
                enderecoPessoa = new PessoaEndereco();
                enderecoPessoa = pessoa.FirstOrDefault().Pessoa.PessoaEnderecos.Count > 0 ? pessoa.FirstOrDefault().Pessoa.PessoaEnderecos.FirstOrDefault() : null;
                Participante.codigoDeIdentificacaoDoParticipante = pessoa.FirstOrDefault().Pessoa.Codigo;
                Participante.nomePessoalOuEmpresarialDoParticipante = pessoa.FirstOrDefault().Pessoa.Nome;
                Participante.codigoDoPaisDoParticipante = enderecoPessoa.Cidade.UF.Pais.IBGE;
                if (pessoa.FirstOrDefault().Pessoa.TipoPessoa == "Física")
                {
                    Participante.cpfDoParticipante = pessoa.FirstOrDefault().Pessoa.CNPJ_CPF;
                    if (!IsCpf(Participante.cpfDoParticipante.OnlyNumbers()))
                        continue;
                }
                else
                {
                    Participante.cnpjDoParticipante = pessoa.FirstOrDefault().Pessoa.CNPJ_CPF;
                    if (!IsCnpj(Participante.cnpjDoParticipante.OnlyNumbers()))
                        continue;
                }

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

        private void CriarRegistro0200(IList<Nota> notas)
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
                            NotaItemReg.aliquotaDeIcmsAplicavel = prod.FirstOrDefault().ICMS == null ? 0 : prod.FirstOrDefault().ICMS.AliqContrib;
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

        private void CriarRegistro0400()
        {
            try
            {
                OperacoesPrestacoes = new List<Reg_0400>();
                Reg_0400 OperacaoPrestacao;

                List<Condicao> Condicao = CondicaoController.Instancia.GetAll().ToList();
                foreach (var cond in Condicao)
                {
                    OperacaoPrestacao = new Reg_0400();
                    OperacaoPrestacao.codigoDaNaturezaDaOperacaoOuPrestacao =  cond.Codigo;
                    OperacaoPrestacao.descricaoDaNaturezaDaOperacaoOuPrestacao = cond.Nome;
                    OperacoesPrestacoes.Add(OperacaoPrestacao);
                }
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
                    if (string.IsNullOrEmpty(item.Key))
                        continue;

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
                    PlanoDeContaReg.codigoDaContaCorrelacionadaNoPlanoDeContasReferenciado = plConta.PlanoContaReferencial.codigo;
                    PlanoDeContaReg.cnpjDoEstabelecimento = String.Empty;
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

        public Bloco_0(IList<Nota> Notas, Filial Filial, string CodVersaoLeiaute, int TipoEscrituracao, string Num_Rec_Anterior, DateTime DT_INIInfArq,
                       DateTime DT_FIMInfArq, int IndicadorMovimento, IList<DateTime> MesesDispensa, IList<Documento> Documentos,
                       IList<PlanoConta> PlanosConta, IList<CentroCusto> CentroDeCustos, bool SemNota, PessoaContabilista Contabilista,
                       IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem)
        {
            try
            {
                notas = Notas;
                documentos = Documentos;
                filial = Filial;
                planosConta = PlanosConta;
                centroDeCustos = CentroDeCustos;
                semNota = SemNota;

                this.GerarBloco(notas, documentos, filial, planosConta, CodVersaoLeiaute, TipoEscrituracao, IndicadorMovimento, Num_Rec_Anterior,
                                DT_INIInfArq, DT_FIMInfArq, centroDeCustos, planosConta, MesesDispensa, Contabilista, logAlteracaoPedidoItem);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void GerarBloco(IList<Nota> notas, IList<Documento> documentos, Filial filial, IList<PlanoConta> planosConta, string CodVersaoLeiaute,
                                int TipoEscrituracao, int indicadorMovimento, string Num_Rec_Anterior, DateTime DT_INIInfArq, DateTime DT_FIMInfArq,
                                IList<CentroCusto> centroDeCustos, IList<PlanoConta> planosConta_11, IList<DateTime> MesesDispensa,
                                PessoaContabilista Contabilista, IList<LogAlteracaoPedidoItem> logAlteracaoPedidoItem)
        {
            CriarRegistro0000(CodVersaoLeiaute, TipoEscrituracao, Num_Rec_Anterior, DT_INIInfArq, DT_FIMInfArq, filial);
            CriarRegistro0001(indicadorMovimento);
            CriarRegistro0100(Contabilista);
            CriarRegistro0110(filial);
            if (MesesDispensa.Count > 0)
                CriarRegistro0120(MesesDispensa);
            CriarRegistro0140(filial);
            CriarRegistro0150(notas, filial);
            CriarRegistro0190(notas);
            CriarRegistro0200(notas);
            CriarRegistro0205(logAlteracaoPedidoItem);
            CriarRegistro0400();
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