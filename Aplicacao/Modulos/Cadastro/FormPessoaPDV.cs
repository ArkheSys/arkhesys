using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using Aplicacao.Base;
using System.Linq;
using cwkGestao.Negocio;
using Cwork.Utilitarios.Componentes;
using System.Windows.Forms;

namespace Aplicacao
{
    public partial class FormPessoaPDV : IntermediariasTela.FormManutPessoaIntermediaria
    {
        private Acao ACAO = Acao.Alterar;

        public FormPessoaPDV(Pessoa Cliente)
        {
            Selecionado = Cliente;
        }

        public FormPessoaPDV(string CPF_CNPJ)
        {
            Selecionado = new Pessoa
            {
                Codigo = PessoaController.Instancia.MaxCodigo(),
                CNPJ_CPF = CPF_CNPJ
            };

            ACAO = Acao.Incluir;
        }

        private void DiscriminaTabs()
        {
            tpPrincipal.Tag = Selecionado;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            DiscriminaTabs();

            Selecionado.PessoaVendedores = Selecionado.PessoaVendedores == null ? new List<PessoaVendedor>() : Selecionado.PessoaVendedores;
            if (Selecionado.PessoaVendedores.Count == 0)
                Selecionado.PessoaVendedores.Add(new PessoaVendedor());
        }

        protected override bool ValidacoesAdicionais()
        {
            return !dxErroProvider.HasErrors;
        }

        private bool ValidaCPFCNPJ()
        {
            string chaveBusca = txt2CNPJ_CPF.Text;

            IList<Pessoa> listaPessoasComCPFCNPJ = new List<Pessoa>();
            listaPessoasComCPFCNPJ = PessoaController.Instancia.GetAtivosByCPFeCNPJ(chaveBusca, true);

            //retira a propria pessoa da comparação, tornando qualquer registro encontrado um problema.
            listaPessoasComCPFCNPJ = listaPessoasComCPFCNPJ.Where(s => s.ID != Selecionado.ID && s.CNPJ_CPF != "00.000.000/0000-00" && s.CNPJ_CPF != "000.000.000-00").ToList();

            if (listaPessoasComCPFCNPJ.Count > 0)
            {
                dxErroProvider.SetError(txt2CNPJ_CPF, "Este CPF já está associado com outra Pessoa!");
                MessageBox.Show("Este CPF já está associado com outra Pessoa", "ATENÇÃO", MessageBoxButtons.OK);
            }

            return !dxErroProvider.HasErrors;
        }

        protected override void OK()
        {
            if (ValidarFormulario() && ValidaCPFCNPJ())
            {
                try
                {
                    Selecionado.BDelivery = false;
                    TelaProObjeto(tpPrincipal);

                    Selecionado.CNPJ_CPF = txt2CNPJ_CPF.Text;
                    Selecionado.TipoPessoa = "Física";
                    Selecionado.bFaturarPorPadrao = false;
                    Selecionado.bSTRevenda = false;
                    Selecionado.bConsumidorFinal = true;
                    Selecionado.BAtivo = true;
                    Selecionado.BCliente = true;

                    Selecionado.DiasPadraoLeitura = 0;
                    Selecionado.SenhaRevenda = null;

                    Selecionado.Filial = FilialController.Instancia.GetFilialPrincipal();

                    Cidade CidadePadrao = null;
                    int? IDCidadePadrao = ConfiguracaoController.Instancia.GetConfiguracao().IDCidadePadraoNFCe;
                    if (IDCidadePadrao.HasValue && IDCidadePadrao.Value != 0)
                        CidadePadrao = CidadeController.Instancia.LoadObjectById(IDCidadePadrao.Value);

                    if (ACAO == Acao.Incluir)
                    {
                        Selecionado.PessoaAvisos = new List<PessoaAviso>();
                        Selecionado.PessoaClientes = new List<PessoaCliente>();
                        Selecionado.PessoaEmails = new List<PessoaEmail>();
                        Selecionado.PessoaFuncionarios = new List<PessoaFuncionario>();
                        Selecionado.PessoaSocios = new List<PessoaSocio>();
                        Selecionado.PessoaVendedores = new List<PessoaVendedor>();
                        Selecionado.ClientesRevenda = new List<ClienteRevenda>();

                        Selecionado.PessoaEnderecos = new List<PessoaEndereco>()
                        {
                            new PessoaEndereco
                            {
                                BAtivo = true,
                                BCobranca = false,
                                BEntrega = true,
                                BPrincipal = true,
                                Bairro = "",
                                CEP = "",
                                Cidade = CidadePadrao,
                                Complemento = "",
                                Contato = "",
                                Descricao = "",
                                Numero = "",
                                Endereco = "",
                                Pessoa = Selecionado,
                                Sequencia = 1,
                                Telefone = ""
                            }
                        };
                        Selecionado.PessoaTelefones = new List<PessoaTelefone>()
                        {
                            new PessoaTelefone
                            {
                                BPrincipal = true,
                                Contato = null,
                                Pessoa = Selecionado,
                                Numero = "",
                                Sequencia = 1,
                                Tipo = null
                            }
                        };
                    }
                    else
                    {
                        var PessoaEnderecoPrincipal = Selecionado.PessoaEnderecos.First();
                        PessoaEnderecoPrincipal.BAtivo = true;
                        PessoaEnderecoPrincipal.BCobranca = false;
                        PessoaEnderecoPrincipal.BEntrega = true;
                        PessoaEnderecoPrincipal.BPrincipal = true;
                        PessoaEnderecoPrincipal.Bairro = "";
                        PessoaEnderecoPrincipal.CEP = "";
                        PessoaEnderecoPrincipal.Cidade = CidadePadrao;
                        PessoaEnderecoPrincipal.Complemento = "";
                        PessoaEnderecoPrincipal.Contato = "";
                        PessoaEnderecoPrincipal.Descricao = "";
                        PessoaEnderecoPrincipal.Numero = "";
                        PessoaEnderecoPrincipal.Endereco = "";
                        PessoaEnderecoPrincipal.Pessoa = Selecionado;
                        PessoaEnderecoPrincipal.Sequencia = 1;
                        PessoaEnderecoPrincipal.Telefone = "";

                        var PessoaTelefonePrincipal = Selecionado.PessoaTelefones.First();
                        PessoaTelefonePrincipal.BPrincipal = true;
                        PessoaTelefonePrincipal.Contato = null;
                        PessoaTelefonePrincipal.Pessoa = Selecionado;
                        PessoaTelefonePrincipal.Numero = "";
                        PessoaTelefonePrincipal.Sequencia = 1;
                        PessoaTelefonePrincipal.Tipo = null;
                    }

                    AcoesAntesSalvar();
                    controller.Salvar(Selecionado, ACAO);
                    AcoesDepoisSalvar();
                    Close();
                }
                catch (Exception ex)
                {
                    new FormErro(ex).ShowDialog();
                }
            }
        }

        protected override void SetarMascaras()
        {
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (Selecionado != null)
            {
                ObjetoPraTela(tpPrincipal);
                txt2CNPJ_CPF.CwkFuncaoValidacao = new CwkBaseEditor.FuncaoValidacaoDelegate(PessoaController.ValidaCpf);
            }

            txtNome.Focus();
        }

        protected override void sbGravar_Click(object sender, EventArgs e) { base.sbGravar_Click(sender, e); }

        protected override void AcoesAntesBase_Load(object sender, EventArgs e) { }

        protected override void AcoesDepoisSalvar() { base.AcoesDepoisSalvar(); }

        protected override void AcoesAntesSalvar() { base.AcoesAntesSalvar(); }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNome.Text) && e.KeyCode == Keys.Enter)
                sbGravar.Focus();
        }
    }
}