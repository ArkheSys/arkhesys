using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Integracao;
using Microsoft.VisualBasic.FileIO;
using DevExpress;
using System.Globalization;
using ExcelDataReader;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text;
using System.Collections.Generic;

namespace Aplicacao.Utilitarios
{
    public partial class FormImportaNcmCestCFOP : Form
    {
        #region Propriedades

        #endregion

        #region Construtores

        public FormImportaNcmCestCFOP()
        {
            InitializeComponent();
            cboTipoRegistro.Text = cboTipoRegistro.Items[0].ToString();
        }

        #endregion

        #region Eventos
        private void btnProcurarArquivo_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Excel e CSV | *.xlsx;*.xls;*.csv"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            txtCaminhoArquivo.Text = dialog.FileName;
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            if (SeArquivoInvalido()) return;

            string extensao = Path.GetExtension(txtCaminhoArquivo.Text).ToLower();

            switch (extensao)
            {
                case ".xls":
                case ".xlsx":
                    CarregarDadosDoArquivo();
                    break;
                case ".csv":
                    CarregarDadosDoArquivoCsv();
                    break;
                default:
                    MessageBox.Show("Formato de arquivo não suportado.");
                    return;
            }
        }

        //private void btnExibir_Click(object sender, EventArgs e)
        //{
        //    if (SeArquivoInvalido()) return;
        //    CarregarDadosDoArquivo();
        //}

        private void btnImportarRegistros_Click(object sender, EventArgs e)
        {
            #region Validação

            if (ValidateWithMessage(dgvDados.DataSource == null, "Sem dados para importação!")) return;

            #endregion

            if (MessageBox.Show("Deseja Importar os Dados?", "Importar os Dados", MessageBoxButtons.YesNo) == DialogResult.No) return;

            switch (cboTipoRegistro.Text)
            {
                case "CEST":
                    ImportarCest();
                    break;
                case "CFOP":
                    ImportarCFOP();
                    break;
                case "NCM":
                    ImportarNcm();
                    break;
                default:
                    MessageBox.Show("Importação não configurada para o tipo selecionado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
            Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Métodos

        private void ImportarNcm()
        {
            var configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            var dataTable = (DataTable)dgvDados.DataSource;
            var ncmImportados = 0;
            var ncmAtualizados = 0;

            foreach (DataRow item in dataTable.Rows)
            {
                if (string.IsNullOrEmpty(item["ncm"].ToString()))
                    continue;

                Acao _Acao = Acao.Alterar;

                var _Ncm = NCMController.Instancia.GetByNcm(item["ncm"].ToString());
                DateTime? dtRevogacao = null;

                try
                {
                    if (!string.IsNullOrEmpty(item["vigenciafim"].ToString()))
                    {
                        if (DateTime.TryParseExact(item["vigenciafim"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataFormatada))
                        {
                            dtRevogacao = dataFormatada;
                        }
                    }
                }
                catch 
                { 
                    dtRevogacao = null; 
                }

                if (_Ncm == null)
                {
                    _Acao = Acao.Incluir;

                    _Ncm = new NCM
                    {
                        Codigo = NCMController.Instancia.MaxCodigo(),
                        Ncm = item["ncm"].ToString(),
                        Descricao = item["descricao"].ToString().TrimCk().ToUpper().Substring(0, Math.Min(100, item["descricao"].ToString().TrimCk().Length)),
                    };

                    ncmImportados++;
                }
                else
                {
                    _Ncm.Descricao = item["descricao"].ToString().TrimCk();
                    _Ncm.DtRevogacao = null;

                    ncmAtualizados++;
                }

                NCMController.Instancia.Salvar(_Ncm, _Acao);
            }

            MessageBox.Show($"Dados importados com sucesso!{Environment.NewLine}Importados: {ncmImportados},{Environment.NewLine}Atualizados: {ncmAtualizados}");
        }

        private void ConverterValores(DataRow item, out decimal? nacionalFederal,out decimal? importadosFederal,out decimal? estadual,out decimal? municipal)
        {
            nacionalFederal = 0;
            importadosFederal = 0;
            estadual = 0;
            municipal = 0;
            try
            {
                nacionalFederal = ConvertToDecimal(item["nacionalfederal"].ToString());
            }
            catch { }

            try
            {
                importadosFederal = ConvertToDecimal(item["importadosfederal"].ToString());
            }
            catch { }

            try
            {
                estadual = ConvertToDecimal(item["estadual"].ToString());
            }
            catch { }

            try
            {
                municipal = ConvertToDecimal(item["municipal"].ToString());
            }
            catch { }

        }

        private decimal? ConvertToDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var cleanInput = input.Replace(",", ".").Replace(" ", "");
            if (decimal.TryParse(cleanInput, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            return null;
        }


        private void ImportarCest()
        {
            var dataTable = (DataTable)dgvDados.DataSource;
            if (dataTable == null)
            {
                MessageBox.Show("Não há dados para importar.", "Atenção");
                return;
            }

            var cestImportados = 0;
            var cestAtualizados = 0;

            foreach (DataRow item in dataTable.Rows)
            {
                var codigoDoArquivo = item["CODIGO"].ToString().Trim();
                if (string.IsNullOrEmpty(codigoDoArquivo))
                    continue;

                Acao acao;
                var cestExistente = CESTController.Instancia.GetByCodigo(codigoDoArquivo);

                CEST cestParaSalvar;

                if (cestExistente == null) 
                {
                    acao = Acao.Incluir;

                    cestParaSalvar = new CEST
                    {
                        Codigo = codigoDoArquivo,
                        Descricao = item["DESCRICAO"].ToString().Trim()
                    };
                    cestImportados++;
                }
                else 
                {
                    acao = Acao.Alterar;
                    cestParaSalvar = cestExistente; 

                    cestParaSalvar.Descricao = item["DESCRICAO"].ToString().Trim();
                    cestAtualizados++;
                }

                CESTController.Instancia.Salvar(cestParaSalvar, acao);
            }

            MessageBox.Show($"Dados importados com sucesso!{Environment.NewLine}Importados: {cestImportados}{Environment.NewLine}Atualizados: {cestAtualizados}", "Importação Concluída");
        }

        private void ImportarCFOP()
        {
            // CODIGO
            // DESCRICAO
            var dataTable = (DataTable)dgvDados.DataSource;
            var cfopImportados = 0;
            var cfopAtualizados = 0;

            foreach (DataRow item in dataTable.Rows)
            {
                if (item["CODIGO"].ToString().IsNullOrEmpty())
                    continue;

                Acao _Acao = Acao.Alterar;
                var _Cfop = TabelaCFOPController.Instancia.GetCFOPPorNumero(item["CODIGO"].ToString());
                if (_Cfop == null)
                {
                    _Acao = Acao.Incluir;
                    _Cfop = new TabelaCFOP
                    {
                        Codigo = TabelaCFOPController.Instancia.MaxCodigo(),
                        CFOP = item["CODIGO"].ToString(),
                        Nome = item["DESCRICAO"].ToString().TrimCk(),
                        Aplicacao = item["APLICACAO"].ToString().TrimCk()
                    };
                    cfopImportados++;
                }
                else
                {
                    _Cfop.Nome = item["DESCRICAO"].ToString().TrimCk();
                    _Cfop.Aplicacao = item["APLICACAO"].ToString().TrimCk();
                    cfopAtualizados++;
                }

                TabelaCFOPController.Instancia.Salvar(_Cfop, _Acao);
            }
            MessageBox.Show($"Dados importados com sucesso!{Environment.NewLine}Importados: {cfopImportados},{Environment.NewLine}Atualizados: {cfopAtualizados}");
        }

        //private void CarregarDadosDoArquivo()
        //{
        //    var dataTable = ObterTabela();

        //    // Verificar se o dataTable é nulo
        //    if (dataTable == null)
        //    {
        //        MessageBox.Show("Falha ao carregar dados: DataTable é nulo.");
        //        txtRegistros.Text = "0";
        //        return;
        //    }
        //    dgvDados.DataSource = dataTable;
        //    // Verificação adicional para depuração
        //    if (dataTable.Rows.Count == 0)
        //    {
        //        MessageBox.Show("DataTable carregado, mas não contém linhas.");
        //    }
        //    txtRegistros.Text = dataTable.Rows.Count.ToString();
        //}

        private void CarregarDadosDoArquivo()
        {
            var dataTable = ObterTabela();

            // Verificar se o dataTable é nulo
            if (dataTable == null)
            {
                MessageBox.Show("Falha ao carregar dados: DataTable é nulo.");
                txtRegistros.Text = "0";
                return;
            }
            dgvDados.DataSource = dataTable;
            // Verificação adicional para depuração
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("DataTable carregado, mas não contém linhas.");
            }
            txtRegistros.Text = dataTable.Rows.Count.ToString();
        }

        //Código Antigo onde importava apenas os campos do NCM com Excel - 18/01/2024

        //private void CarregarDadosDoArquivo()
        //{
        //    var dataTable = ObterTabela();
        //    dgvDados.DataSource = dataTable;
        //    txtRegistros.Text = dataTable == null ? "0" : dataTable.Rows.Count.ToString();
        //}
        //Código Antigo onde importava apenas os campos do NCM com Excel - 18/01/2024

        //private DataTable ObterTabela()
        //{
        //    var dataTable = new DataTable();

        //    try
        //    {
        //        var extensao = Path.GetExtension(txtCaminhoArquivo.Text);
        //        var connectionString = extensao.Equals(".xls")
        //            ? "Provider=Microsoft.Jet.OLEDB.4.0; Data Source =" + txtCaminhoArquivo.Text.TrimCk() + "; Extended Properties = 'Excel 8.0;HDR=YES'"
        //            : "Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + txtCaminhoArquivo.Text.TrimCk() + "; Extended Properties = 'Excel 8.0;HDR=YES'";

        //        using (var conn = new OleDbConnection(connectionString))
        //        {
        //            conn.Open();

        //            var schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            var nomePlanilha = schema.Rows[0]["TABLE_NAME"].ToString();

        //            if (!nomePlanilha.Contains(cboTipoRegistro.Text))
        //            {
        //                dataTable = null;
        //                MessageBox.Show("Arquivo inválido de acordo com o tipo selecionado. Por favor, tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //            else
        //            {
        //                var cmd = new OleDbCommand($"SELECT * From [{nomePlanilha}]", conn);
        //                var dataAdapter = new OleDbDataAdapter(cmd);
        //                dataAdapter.Fill(dataTable);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    return dataTable;
        //}

        private void CarregarDadosDoArquivoCsv()
        {
            var dataTable = new DataTable();
            var colunasDesejadas = new HashSet<string> { "codigo", "descricao", "nacionalfederal", "importadosfederal", "estadual", "municipal", "vigenciainicio", "vigenciafim" };

            using (TextFieldParser csvReader = new TextFieldParser(txtCaminhoArquivo.Text))
            {
                csvReader.SetDelimiters(new string[] { ";" });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Lendo as colunas do cabeçalho
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    if (colunasDesejadas.Contains(column.ToLower()))
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        dataTable.Columns.Add(datecolumn);
                    }
                }

                // Lendo os dados
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    if (fieldData.Length == colFields.Length)
                    {
                        var rowToAdd = fieldData.Where((field, index) => colunasDesejadas.Contains(colFields[index].ToLower())).ToArray();
                        dataTable.Rows.Add(rowToAdd);
                    }
                }
            }

            dgvDados.DataSource = dataTable;
        }

        private DataTable ObterTabela()
        {
            var dataTable = new DataTable();
            var caminhoArquivo = txtCaminhoArquivo.Text.Trim();

            try
            {
                using (var stream = File.Open(caminhoArquivo, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        dataTable = result.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                // Este código vai extrair TODOS os detalhes do erro, incluindo as causas internas.
                StringBuilder sb = new StringBuilder();
                Exception inner = ex;
                int level = 0;

                sb.AppendLine("Ocorreu um erro ao gerar o modelo:");
                sb.AppendLine("=============================================");

                while (inner != null)
                {
                    sb.AppendLine($"\n--- Nível de Erro {level} ---");
                    sb.AppendLine($"Tipo: {inner.GetType().Name}");
                    sb.AppendLine($"Mensagem: {inner.Message}");
                    sb.AppendLine($"Origem: {inner.Source}");
                    sb.AppendLine($"Stack Trace: {inner.StackTrace}");

                    inner = inner.InnerException;
                    level++;
                }

                MessageBox.Show(sb.ToString(), "Erro Detalhado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        private bool SeArquivoInvalido()
        {
            if (ValidateWithMessage(txtCaminhoArquivo.Text.IsNullOrEmpty(), "Arquivo inválido. Por favor, tente novamente"))
                return true;

            if (ValidateWithMessage(!File.Exists(txtCaminhoArquivo.Text), "Arquivo não encontrado. Por favor, tente novamente"))
                return true;

            return false;
        }

        private bool ValidateWithMessage(bool predicate, string message)
        {
            if (predicate) MessageBox.Show(message, "Importação de Dados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return predicate;
        }

        #endregion
        private void btnVerModelo_Click(object sender, EventArgs e)
        {
            // Pega a lista de colunas necessárias usando o método que já temos.
            List<string> headers = GetHeadersParaModelo();

            if (headers == null)
            {
                MessageBox.Show("Selecione um tipo de registro válido (NCM, CEST ou CFOP).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Usa um StringBuilder para construir uma mensagem de ajuda bem formatada.
            StringBuilder mensagem = new StringBuilder();

            mensagem.AppendLine($"Para importar '{cboTipoRegistro.Text}', seu arquivo Excel ou CSV deve conter as seguintes colunas, exatamente com estes nomes e na ordem sugerida:");
            mensagem.AppendLine("----------------------------------------------------------");

            // Adiciona cada nome de coluna à mensagem
            foreach (string header in headers)
            {
                mensagem.AppendLine($"- {header}");
            }

            mensagem.AppendLine("----------------------------------------------------------");
            mensagem.AppendLine("\nObservações:");
            mensagem.AppendLine("• A primeira linha do seu arquivo deve ser o cabeçalho com os nomes das colunas.");
            mensagem.AppendLine("• Para arquivos CSV, o separador utilizado deve ser o ponto e vírgula (;).");

            // Exibe a mensagem final em uma MessageBox informativa.
            MessageBox.Show(mensagem.ToString(), $"Modelo para Importação de {cboTipoRegistro.Text}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBaixarModelo_Click(object sender, EventArgs e)
        {

        }

        // Este método decide quais colunas o modelo deve ter
        private List<string> GetHeadersParaModelo()
        {
            switch (cboTipoRegistro.Text)
            {
                case "NCM":
                    // Colunas exatas usadas no seu método ImportarNcm
                    return new List<string> { "ncm", "descricao",  };
                case "CEST":
                    // Colunas exatas usadas no seu método ImportarCest
                    return new List<string> { "CODIGO", "DESCRICAO" };
                case "CFOP":
                    // Colunas exatas usadas no seu método ImportarCFOP
                    return new List<string> { "CODIGO", "DESCRICAO", "APLICACAO" };
                default:
                    return null;
            }
        }

        // Este método usa o ClosedXML para criar o arquivo Excel
        // APAGUE o método GerarPlanilhaModelo antigo e SUBSTITUA por este:
        private void GerarPlanilhaModelo(string caminhoArquivo, List<string> headers)
        {
            // Cria um novo arquivo no formato .xlsx
            IWorkbook workbook = new XSSFWorkbook();

            // Cria uma nova planilha chamada "Dados"
            ISheet sheet = workbook.CreateSheet("Dados");

            // Cria um estilo para o cabeçalho (negrito, fundo cinza)
            ICellStyle headerStyle = workbook.CreateCellStyle();
            IFont headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);
            headerStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            // Cria a primeira linha para o cabeçalho
            IRow headerRow = sheet.CreateRow(0);

            // Escreve os cabeçalhos na primeira linha
            for (int i = 0; i < headers.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
                cell.CellStyle = headerStyle;
            }

            // Ajusta a largura das colunas ao conteúdo
            for (int i = 0; i < headers.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            // Salva o arquivo no disco
            using (var fileData = new FileStream(caminhoArquivo, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileData);
            }
        }

    }
}
