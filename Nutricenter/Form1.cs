using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Collections;

namespace cwkGestao.Importacao.Nutricenter
{
    public partial class Form1 : Form
    {
        private string CONNSTRING;
        private Hashtable fornecedores = new Hashtable();
        private Hashtable gruposEstoque = new Hashtable();
        private Modelo.Filial filial;

        public Form1()
        {
            InitializeComponent();

            global::Modelo.cwkGlobal.objUsuarioLogado = new global::Modelo.Cw_Usuario
            {
                Id = 1,
                Codigo = 1,
                Login = "IMPORTACAO"
            };

            cwkGestao.Negocio.EmpresaController.Instancia.InicializaBase();
        }

        public DataSet GetDataSetFromAdapter(string queryString)
        {
            DataSet dataSet = new DataSet();
            using (OdbcConnection connection = new OdbcConnection(CONNSTRING))
            {
                OdbcDataAdapter adapter = new OdbcDataAdapter(queryString, connection);

                try
                {
                    connection.Open();
                    adapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return dataSet;
        }

        private void btImportarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                CONNSTRING = @"Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 5.X;DefaultDir="+ txtDiretorio.Text + ";Dbq=" + txtDiretorio.Text + ";CollatingSequence=ASCII;";

                fornecedores.Clear();
                gruposEstoque.Clear();

                filial = Negocio.FilialController.Instancia.GetFilialPrincipal();

                ImportarFornecedores();                
                ImportarGrupos();                
                ImportarProdutos();
                MessageBox.Show("Importação efetuada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportarFornecedores()
        {
            DataTable fornecedorDT = GetDataSetFromAdapter("SELECT * FROM ACADFORN").Tables[0];
            IList<Modelo.Pessoa> pessoasCadastradas = Negocio.PessoaController.Instancia.GetAll();
            IList<Modelo.Cidade> cidadesCadastradas = Negocio.CidadeController.Instancia.GetAll();
            Modelo.Cidade MARINGA = cidadesCadastradas.Where(c => c.Nome.ToLower() == "maringá").First();
            int codigoDisponivel = Negocio.PessoaController.Instancia.MaxCodigo();

            List<Modelo.Pessoa> fornecedoresCadastar = new List<cwkGestao.Modelo.Pessoa>();

            foreach (DataRow item in fornecedorDT.Rows)
            {
                Modelo.Pessoa pessoa;
                var pessoaExistente = pessoasCadastradas.Where(p => p.Nome == item["RAZAO_SOCIAL"].ToString() && p.CNPJ_CPF == item["INSCRICAO"].ToString());
                if (pessoaExistente.Count() > 0)
                {
                    pessoa = pessoaExistente.First();
                    pessoa.BFornecedor = true;
                }
                else
                {
                    pessoa = new Modelo.Pessoa
                    {
                        Codigo = codigoDisponivel++,
                        Nome = item["RAZAO_SOCIAL"].ToString(),
                        Fantasia = item["NOME"].ToString(),
                        CNPJ_CPF = item["CGC"].ToString(),
                        Inscricao = item["INSCRICAO"].ToString(),
                        DtCadastro = Convert.ToDateTime(item["DATA"].ToString()),
                        BFornecedor = true,
                        BAtivo = true,
                        Observacao = item["CODIGO"].ToString(),
                        TipoPessoa = "Jurídica",
                        Filial = filial
                    };

                    string contato = item["CONTATO"].ToString();
                    string representante = item["REPRESENTANTE"].ToString();

                    var fone = new Modelo.PessoaTelefone
                    {
                        Pessoa = pessoa,
                        Contato = representante,
                        BPrincipal = true,
                        Numero = item["FONE"].ToString(),
                        Sequencia = pessoa.PessoaTelefones.Count + 1,
                        Tipo = "Comercial"
                    };
                    pessoa.PessoaTelefones.Add(fone);

                    if (item["FAX"] != DBNull.Value && item["FAX"].ToString() != String.Empty)
                    {
                        var fax = new Modelo.PessoaTelefone
                        {
                            Pessoa = pessoa,
                            Contato = contato,
                            Numero = item["FAX"].ToString(),
                            Sequencia = pessoa.PessoaTelefones.Count + 1,
                            Tipo = "Fax"
                        };
                        pessoa.PessoaTelefones.Add(fax);
                    }

                    if (item["EMAIL"] != DBNull.Value && item["EMAIL"].ToString() != String.Empty)
                    {
                        var email = new Modelo.PessoaEmail
                        {
                            Email = item["EMAIL"].ToString(),
                            Pessoa = pessoa,
                            BContato = true
                        };
                        pessoa.PessoaEmails.Add(email);
                    }

                    var endereco = new Modelo.PessoaEndereco
                    {
                        Pessoa = pessoa,
                        BPrincipal = true,
                        Sequencia = pessoa.PessoaEnderecos.Count + 1,
                        Endereco = item["ENDERECO"].ToString(),
                        Bairro = item["BAIRRO"].ToString(),
                        CEP = item["CEP"].ToString(),
                        Contato = contato
                    };
                    string nomeCidade = item["CIDADE"].ToString().ToLower().RetiraAcentos();
                    string nomeUF = item["UF"].ToString().ToLower();

                    if (!String.IsNullOrEmpty(nomeCidade))
                    {
                        var cidadesEncontradas = cidadesCadastradas.Where(c => c.Nome.ToLower().RetiraAcentos() == nomeCidade);
                        if (cidadesEncontradas.Count() > 1 && !String.IsNullOrEmpty(nomeUF))
                            cidadesEncontradas = cidadesEncontradas.Where(c => c.UF.Nome.ToLower() == nomeUF);

                        if (cidadesEncontradas.Count() == 0)
                        {
                            endereco.Complemento = nomeCidade + " - " + nomeUF;
                        }
                        else
                            endereco.Cidade = cidadesEncontradas.First();
                    }
                    else
                        endereco.Cidade = MARINGA;

                    pessoa.PessoaEnderecos.Add(endereco);
                }

                if (fornecedoresCadastar.Where(f => f.Nome == pessoa.Nome && f.CNPJ_CPF == pessoa.CNPJ_CPF).Count() > 0)
                    pessoa.Nome = pessoa.Nome + " | " + pessoa.Codigo;

                fornecedoresCadastar.Add(pessoa);
                fornecedores.Add(item["CODIGO"].ToString(), pessoa);
            }

            Negocio.PessoaController.Instancia.RegisterNewIntoTransaction(fornecedoresCadastar);
            Negocio.GrupoEstoqueController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void ImportarGrupos()
        {
            DataTable grupoDT = GetDataSetFromAdapter("SELECT * FROM ACADGPRD").Tables[0];

            foreach (DataRow item in grupoDT.Rows)
            {
                var grupo = new Modelo.GrupoEstoque
                {
                    Codigo = Convert.ToInt32(item["CODIGO"]),
                    Nome = item["DESCRICAO"].ToString(),
                    NomeReduzido = item["DESCRICAO"].ToString(),
                    ClassificacaoProduto = cwkGestao.Modelo.ClassificacaoProduto.ProdutoAcabado,
                    Classificacao = item["CODIGO"].ToString()
                };
                Negocio.GrupoEstoqueController.Instancia.RegisterNewIntoTransaction(grupo);
                gruposEstoque.Add(item["CODIGO"].ToString(), grupo);
            }
            Negocio.GrupoEstoqueController.Instancia.CommitUnitOfWorkTransaction();
        }

        private void ImportarProdutos()
        {
            DataTable produtosDT = GetDataSetFromAdapter("SELECT * FROM ACADPROD").Tables[0];
            Modelo.TabelaPreco tabelaPreco = Negocio.TabelaPrecoController.Instancia.GetAll()[0];
            int codigoDisponivel = 1;
            foreach (DataRow item in produtosDT.Rows)
            {
                var produto = new Modelo.Produto
                {
                    Codigo = (codigoDisponivel++).ToString(),
                    Nome = item["NOME"].ToString(),
                    Barra = item["CODIGO"].ToString(),
                    PrecoBase = Convert.ToDecimal(item["PRECO_CUST"]),
                };

                produto.GrupoEstoque = (Modelo.GrupoEstoque)gruposEstoque[item["GRUPO"].ToString()];
                produto.Fornecedor = (Modelo.Pessoa)fornecedores[item["FORNECEDOR"].ToString()];
                produto.Unidade = Negocio.UnidadeController.Instancia.LoadObjectById(Convert.ToInt32(txtUnidade.Text));

                var tabelaPrecoProd = new Modelo.TabelaPrecoProduto
                {
                    Codigo = 1,
                    Produto = produto,
                    TabelaPreco = tabelaPreco,
                    MargemLucro = Convert.ToDecimal(item["PERCLLIQUI"])
                };
                produto.TabelaPrecoProdutos.Add(tabelaPrecoProd);
                var estoque = new Modelo.Estoque
                {
                    Produto = produto,
                    Filial = filial,
                    Qtd_Estoque_Fisico = item["SALDO_ATU"] == DBNull.Value ? 0m : Convert.ToDecimal(item["SALDO_ATU"])
                };
                produto.Estoques.Add(estoque);

                Negocio.ProdutoController.Instancia.RegisterNewIntoTransaction(produto);
            }
            Negocio.ProdutoController.Instancia.CommitUnitOfWorkTransaction();
        }
    }
}
