using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Aplicacao.Modulos.Financeiro;

using cwkGestao.Integracao.ACBr.Boleto;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Financeiro;

using DBUtils.Classes;

using DevExpress.XtraEditors;

using Remessa = cwkGestao.Modelo.Remessa;

namespace Aplicacao
{
    public partial class FormRetorno : Form
    {
        private readonly StringBuilder Err = new StringBuilder();
        private StringBuilder Mensagem = new StringBuilder();
        private DocumentoController DocumentoController = DocumentoController.Instancia;
        private readonly RemessaController RemessaController = RemessaController.Instancia;
        public Documento DocumentoABaixar { get; set; }

        public FormRetorno()
        {
            InitializeComponent();

            lkRemessa.Sessao = RemessaController.Instancia.getSession();
            lkRemessa.Exemplo = new Remessa();
        }

        private void SelecionaArquivo(TextEdit txt, string tipoArquivo)
        {
            openFileDialog1.Filter = tipoArquivo;
            if (!string.IsNullOrEmpty(txt.Text)) openFileDialog1.FileName = txt.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) txt.Text = openFileDialog1.FileName;
        }

        private void btnArqRetorno_Click(object sender, EventArgs e)
        {
            SelecionaArquivo(txtArqRetorno, "(*.*)|*.*");
        }
        
        private void sbGravar_Click(object sender, EventArgs e)
        {
            Remessa remessaselecionada = (Remessa)lkRemessa.Selecionado;
            if (MessageBox.Show("Deseja Baixar os Documentos?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Baixar(remessaselecionada.UtilizaDataCredito);
        }

        private Boleto SetDadosRemessa(Boleto boletoController, Remessa _remessa, Filial _filial)
        {
            if (boletoController == null)
                boletoController = new Boleto();

            boletoController.Remessa.Banco = _remessa.Banco;
            boletoController.Remessa.CodigoAgencia = _remessa.CodigoAgencia;
            boletoController.Remessa.DigitoAgencia = _remessa.DigitoAgencia;
            boletoController.Remessa.ContaCorrente = _remessa.ContaCorrente;
            boletoController.Remessa.DigitoConta = _remessa.DigitoConta;
            boletoController.Remessa.CodigoCedente = _remessa.CodigoCedente;
            boletoController.Remessa.DigitoAgenciaConta = _remessa.DigitoAgenciaConta;
            boletoController.Remessa.TipoCarteira = Convert.ToInt32(_remessa.SicrediTipoCobranca);
            boletoController.Remessa.Carteira = _remessa.VariacaoCarteira;
            boletoController.Remessa.LayoutBoleto = _remessa.LayoutBoleto;
            boletoController.Remessa.LayoutRemessa = _remessa.LayoutRemessa;
            boletoController.Remessa.CaminhoRemessa = _remessa.CaminhoRemessa;
            boletoController.Remessa.NomeRemessa = _remessa.NomeRemessa;
            boletoController.Remessa.CaminhoRetorno = _remessa.CaminhoRetorno;
            boletoController.Remessa.NomeRetorno = _remessa.NomeRetorno;
            boletoController.Remessa.VersaoLayoutArquivo = _remessa.VersaoLayoutArquivo;
            boletoController.Remessa.VersaoLayoutLote = _remessa.VersaoLayoutLote;
            boletoController.Remessa.Convenio = _remessa.Convenio;
            boletoController.Remessa.NumeroContrato = _remessa.NumeroContrato;
            boletoController.Remessa.Carteira = _remessa.Carteira;
            boletoController.Remessa.CodigoGeracao = _remessa.CodigoGeracao;
            boletoController.Remessa.DataLimitePagamento = _remessa.DataLimitePagamento;

            boletoController.Remessa.CpfCnpj = _filial.CNPJ.OnlyNumbers();
            boletoController.Remessa.Nome = _filial.Nome;
            boletoController.Remessa.Logradouro = _filial.Endereco;
            boletoController.Remessa.NumeroResidencia = _filial.Numero;
            boletoController.Remessa.Bairro = _filial.Bairro;
            boletoController.Remessa.Cidade = _filial.Cidade.Nome;
            boletoController.Remessa.UF = _filial.Cidade.UF.Sigla;
            boletoController.Remessa.Cep = _filial.CEP.OnlyNumbers();

            boletoController.Remessa.Logotipo = _remessa.Logotipo;
            boletoController.Remessa.InstrucoesCaixa = _remessa.InstrucoesCaixa;
            boletoController.Remessa.InicioNossoNumero = "00001";
            boletoController.Remessa.FimNossoNumero = "99999";
            boletoController.Remessa.MensagemEmail = _remessa.MensagemEmail;
            boletoController.Remessa.AssuntoEmail = _remessa.AssuntoEmail;
            boletoController.Remessa.NomeEmail = _remessa.NomeDoEmail;
            boletoController.Remessa.BancoEmiteBoleto = _remessa.BancoEmiteBoleto;
            boletoController.Remessa.CaminhoPDF = _remessa.CaminhoPDF;
            boletoController.Remessa.ImpressoraPDF = _remessa.ImpressoraNome;
            boletoController.Remessa.CaminhoHTML = _remessa.CaminhoHTML;

            // Variação Carteira x Modalidade
            boletoController.Remessa.OutroDadoConfiguracao1 = _remessa.VariacaoCarteira;
            boletoController.Remessa.TipoDocumentoCobranca = "DM";

            // Não utilizado
            //boletoController.Remessa.CaminhoImagem = _remessa.CaminhoImagem;
            //boletoController.Remessa.CaminhoLayoutBoleto = _remessa.CaminhoLayoutBoleto;
            //boletoController.Remessa.MargemSuperior = 15;
            //boletoController.Remessa.LayoutBoletoEmail = _remessa.LayoutBoletoEmail;
            //boletoController.Remessa.URLLogotipo = _remessa.URLLogotipo;
            //boletoController.Remessa.URLCodigodeBarras = _remessa.URLCodigodeBarras;
            //if (_remessa.Banco == 8)
            //{
            //    boletoController.Remessa.TipoDocumentoCobranca = _remessa.EspecieTituloSicredi;
            //    boletoController.Remessa.OutroDadoConfiguracao1 = _remessa.Sequencia.ToString().Substring(0, 1);
            //}
            //else
            //{
            //    boletoController.Remessa.TipoDocumentoCobranca = "DM";
            //    boletoController.Remessa.OutroDadoConfiguracao1 = _remessa.VariacaoCarteira;
            //}
            boletoController.ConfigurarComponente();
            return boletoController;
        }



        private void Baixar(bool UtilizaDataCredito)
        {
            if (ValidaTela())
            {
                var InfoArquivo = new FileInfo(txtArqRetorno.Text);

                Boleto b = SetDadosRemessa(null, (Remessa)lkRemessa.Selecionado, FilialController.Instancia.GetFilialPrincipal());
                b.LerRetorno(InfoArquivo.DirectoryName, InfoArquivo.Name);

                var ArquivoIni = new IniFile(InfoArquivo.DirectoryName + "\\Retorno.ini");
                var DadosRetorno = new Retorno().PreencheDadosRetorno(ArquivoIni);

                if (DadosRetorno.Count == 0)
                    MessageBox.Show("Arquivo de retorno com dados inválidos ou inexistentes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Cursor = Cursors.WaitCursor;
                BaixaDocumentoRetorno baixa = new BaixaDocumentoRetorno(DadosRetorno, (Remessa)lkRemessa.Selecionado);
                baixa.BaixarDocumentos(UtilizaDataCredito);
                Cursor = Cursors.Default;
                var form = new GridLogRetornoDetalhe(baixa.logRetorno);
                form.Show();
            }
            else
            {
                MessageBox.Show(Err.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Err.Remove(0, Err.Length);
        }

        private bool ValidaTela()
        {
            bool Flag = true;

            if (string.IsNullOrEmpty(txtArqRetorno.Text.Trim()))
            {
                dxErroProvider.SetError(txtArqRetorno, "Campo Obrigatório");
                Err.Append("Por favor escolha um arquivo de retorno.\n");
                Flag = false;
            }
            else if (!ValidaExistencia(txtArqRetorno.Text))
            {
                dxErroProvider.SetError(txtArqRetorno, "Caminho Inválido");
                Err.Append("Por favor indique um caminho válido para Arquivo retorno.\n");
                Flag = false;
            }
            else
            {
                dxErroProvider.SetError(txtArqRetorno, "");
            }

            if (string.IsNullOrEmpty(lkRemessa.Text.Trim()))
            {
                dxErroProvider.SetError(lkRemessa, "Campo Obrigatório");
                Err.Append("Por favor escolha uma remessa.\n");
                Flag = false;
            }
            //else if (!ValidaExistencia(((Remessa) lkRemessa.Selecionado).ArquivoDeLicensa))
            //{
            //    dxErroProvider.SetError(lkRemessa, "Caminho Inválido");
            //    Err.Append("Por favor indique um caminho válido para Arquivo de licensa na remessa.\n");
            //    Flag = false;
            //}
            else
            {
                dxErroProvider.SetError(lkRemessa, "");
            }

            return Flag;
        }

        private void FormRetorno_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    sbGravar_Click(sender, e);
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void sbCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ValidaExistencia(string pArq)
        {
            FileInfo ArqInfo = new FileInfo(pArq);
            return ArqInfo.Exists;
        }

        private void btnRemessa_Click(object sender, EventArgs e)
        {
            GridGenerica<Remessa> grid = new GridGenerica<Remessa>(RemessaController.GetAll(), new FormRemessa(), (Remessa)lkRemessa.Selecionado, false) { Selecionando = true };
            grid.ShowDialog();

            if (grid.Selecionado != null)
                lkRemessa.Localizar(Convert.ToInt32(grid.Selecionado.ID));

            lkRemessa.Focus();
        }
    }
}