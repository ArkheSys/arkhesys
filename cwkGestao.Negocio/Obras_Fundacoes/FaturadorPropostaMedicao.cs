using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Modelo;
using cwkGestao.Negocio.Tributacao;

namespace cwkGestao.Negocio.Obras_Fundacoes
{
    public class FaturadorPropostaMedicao
    {
        private readonly Filial empresa;
        private readonly Ofd_Proposta proposta;
        private readonly IList<pxyFaturamentoPerfuracao> perfuracoes;
        private readonly Configuracao configuracao;
        private readonly Ofd_PropostaMedicao medicao;
        private readonly DateTime dataFaturamento;

        public NotaItemServico ServicoMobilizacao { get; set; }
        public NotaItemServico ServicoProjeto { get; set; }
        public NotaItemServico ServicoHoraParada { get; set; }

        public FaturadorPropostaMedicao(Filial _empresa, Ofd_Proposta _proposta, string _medicao, IList<pxyFaturamentoPerfuracao> _perfuracoes, DateTime _dataFaturamento)
        {
            configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            ValidarConfiguracao(configuracao);
            empresa = _empresa;
            proposta = _proposta;
            perfuracoes = _perfuracoes;
            dataFaturamento = _dataFaturamento;
            if (!String.IsNullOrEmpty(_medicao) && perfuracoes.Count > 0)
                medicao = Ofd_PropostaMedicaoController.Instancia.GetByPropostaENumero(proposta, Convert.ToInt32(_medicao));
            
        }

        private void ValidarConfiguracao(Configuracao configuracao)
        {
            if (configuracao.VendedorObrasFundacoes == null
                || configuracao.TipoNotaObrasFundacoes == null
                || configuracao.CondicaoObrasFundacoes == null)
                throw new ArgumentException("Preencha os dados de faturamento na configuração do sistema antes de realizar essa operação.");
        }

        public void Faturar()
        {
            var nota = new Nota();
            PreencherTipoNota(nota);
            PreencherPessoa(nota);
            PreencherDadosBasicos(nota);
            GerarItens(nota);
            GerarImpostos(nota);
            GerarParcelas(nota);
            AtualizarMedicao(nota);
            NotaController.Instancia.RegisterNewIntoTransaction(nota);
            NotaController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void AtualizarMedicao(Nota nota)
        {
            if (medicao != null)
            {
                medicao.MedicaoFaturada = true;
                Ofd_PropostaMedicaoController.Instancia.RegisterNewIntoTransaction(medicao);
            }
        }

        private void PreencherDadosBasicos(Nota nota)
        {
            nota.Filial = empresa;
            nota.Condicao = CondicaoController.Instancia.LoadObjectById(configuracao.CondicaoObrasFundacoes.ID);
            nota.Vendedor = configuracao.VendedorObrasFundacoes;
            nota.Ent_Sai = InOutType.Saída;
            nota.Dt = dataFaturamento;
            nota.DtDigitacao = dataFaturamento;
            nota.Numero = NotaController.Instancia.NovoNumeroSerie(nota, null);
            nota.Codigo = NotaController.Instancia.GetNovoCodigoNota();
        }

        private void GerarImpostos(Nota nota)
        {
            IList<Nota> notasQuePrecisamSerAtualizadasPorCausaDeImpostos;
            TributacaoServicoOld.CalculaServicoImpostos(nota, out notasQuePrecisamSerAtualizadasPorCausaDeImpostos);
            if (notasQuePrecisamSerAtualizadasPorCausaDeImpostos.Count > 0)
                NotaController.Instancia.RegisterNewIntoTransaction(notasQuePrecisamSerAtualizadasPorCausaDeImpostos);
        }

        private void GerarItens(Nota nota)
        {
            AdicionarServicoNota(ServicoMobilizacao, nota);
            AdicionarServicoNota(ServicoHoraParada, nota);
            AdicionarServicoNota(ServicoProjeto, nota);
            CriarServicoPerfuracao(nota);
            nota.TotalServico = nota.NotaItemsServicos.Sum(n => n.SubTotal);
            nota.TotalNota = nota.TotalServico;
        }

        private void CriarServicoPerfuracao(Nota nota)
        {
            if (perfuracoes.Count > 0)
            {
                var serv = new NotaItemServico();
                serv.Valor = perfuracoes.Sum(p => p.SubTotal);
                serv.Quantidade = 1;
                serv.SubTotal = serv.Valor;
                serv.Servico = proposta.Servico;
                AdicionarServicoNota(serv, nota);
            }
        }

        private void AdicionarServicoNota(NotaItemServico servico, Nota nota)
        {
            if (servico != null)
            {
                servico.NomeServico = servico.Servico.Nome;
                nota.NotaItemsServicos.Add(servico);
                servico.Nota = nota;
            }
        }

        private static void GerarParcelas(Nota nota)
        {
            foreach (var parcela in CondicaoController.Instancia.GerarParcelas(nota))
                nota.NotaParcelas.Add(parcela);
        }

        private void PreencherPessoa(Nota nota)
        {
            var pessoa = proposta.ClienteFaturamento;
            nota.Pessoa = pessoa;
            nota.PessoaCNPJCPF = pessoa.CNPJ_CPF;
            nota.PessoaInscRG = pessoa.RG;
            nota.PessoaNome = pessoa.Nome;
            nota.PessoaTelefone = pessoa.TelefonePrincipal;
            var endereco = pessoa.EnderecoPrincipal();
            nota.PessoaBairro = endereco.Bairro;
            nota.PessoaEndereco = endereco.Endereco + (String.IsNullOrEmpty(endereco.Numero) ? "" : ", " + endereco.Numero);
            nota.PessoaCEP = endereco.CEP;
            nota.PessoaCidade = endereco.Cidade.Nome;
            nota.PessoaCidadeIBGE = endereco.Cidade.IBGE;
            nota.PessoaUF = endereco.Cidade.UF.Sigla;
            nota.Complemento = endereco.Complemento;

            if (pessoa.BCliente)
                nota.BPessoaContribuinte = pessoa.GetCliente.BContribuinte;
            else
                nota.BPessoaContribuinte = false;
        }

        private void PreencherTipoNota(Nota nota)
        {
            nota.TipoNota = configuracao.TipoNotaObrasFundacoes;
            nota.ModeloDocto = (int)configuracao.TipoNotaObrasFundacoes.ModeloDocto;
            nota.Serie = configuracao.TipoNotaObrasFundacoes.Serie;
            nota.BGeraFinanceiro = configuracao.TipoNotaObrasFundacoes.BGeraFinanceiro;
        }
    }
}
