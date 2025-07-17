using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using MO = Modelo;

namespace cwkGestao.Importacao.Cwork
{
    internal class ImportacaoBasicos
    {
        internal void Importar(string connStringAdm, int idEmpresa, int idTabelaPreco, int idFornecedor, int idUnidade, int idPais, int idPortador, int idTipoNota)
        {
            #region Declaração das variáveis
            StringBuilder builder = new StringBuilder();

            DataTable dtProdutos = new DataTable();
            DataTable dtUFs = new DataTable();
            DataTable dtCidades = new DataTable();
            DataTable dtPessoas = new DataTable();
            DataTable dtAcrescimos = new DataTable();
            DataTable dtContasBancarias = new DataTable();
            DataTable dtTiposDocumento = new DataTable();
            DataTable dtHistoricos = new DataTable();
            DataTable dtPlanosConta = new DataTable();
            DataTable dtCondicoes = new DataTable();
            DataTable dtCondicaoParcelas = new DataTable();
            DataTable dtDocumentos = new DataTable();
            DataTable dtMovimentos = new DataTable();
            DataTable dtMovCaixas = new DataTable();
            DataTable dtNotas = new DataTable();
            DataTable dtNotaItens = new DataTable();

            List<MO.Produto> produtos = new List<MO.Produto>();
            List<MO.UF> ufs = new List<MO.UF>();
            List<MO.Cidade> cidades = new List<MO.Cidade>();
            List<MO.Pessoa> pessoas = new List<MO.Pessoa>();
            List<MO.Acrescimo> acrescimos = new List<MO.Acrescimo>();
            List<MO.Banco> bancos = new List<MO.Banco>();
            List<MO.TipoDocumento> tiposDocumento = new List<MO.TipoDocumento>();
            List<MO.Historico> historicos = new List<MO.Historico>();
            List<MO.PlanoConta> planoContas = new List<MO.PlanoConta>();
            List<MO.Condicao> condicoes = new List<MO.Condicao>();
            List<MO.CondicaoParcela> condicaoParcelas = new List<MO.CondicaoParcela>();
            List<MO.Documento> documentos = new List<MO.Documento>();
            List<MO.Movimento> movimentos = new List<MO.Movimento>();
            List<MO.MovCaixa> movCaixas = new List<MO.MovCaixa>();
            List<MO.Nota> notas = new List<MO.Nota>();
            List<MO.NotaItem> notaItems = new List<MO.NotaItem>();

            #endregion

            #region Recupera os dados do Administração
            using (SqlConnection conn = new SqlConnection(connStringAdm))
            {
                conn.Open();
                SqlCommand cmd;

                builder.AppendLine("SELECT * FROM servico");
                builder.AppendLine("WHERE servico.tipo = 1");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtProdutos.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT pessoa.*, cidade.nome AS nomeCidade FROM pessoa");
                builder.AppendLine("INNER JOIN cidade ON cidade.id = pessoa.idcidade");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtPessoas.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM uf");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtUFs.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT cidade.*, uf.sigla FROM cidade");
                builder.AppendLine("INNER JOIN uf on uf.id = cidade.iduf");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtCidades.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM Acrescimo");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtAcrescimos.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM banco");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtContasBancarias.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM tipodocumento");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtTiposDocumento.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM historico");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtHistoricos.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM planoconta");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtPlanosConta.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM condicao");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtCondicoes.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM condicaoparcela");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtCondicaoParcelas.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM movcaixa");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtMovCaixas.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT documento.*, dbo.f_SaldoDocumento(documento.id) AS saldo, dbo.f_SituacaoDocumento(documento.id) AS situacao");
                builder.AppendLine("FROM documento");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtDocumentos.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM movimento");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtMovimentos.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM nota");
                builder.AppendLine("WHERE nota.tiponota = 1");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtNotas.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM notaservico");
                builder.AppendLine("INNER JOIN nota on nota.id = notaservico.idnota");
                builder.AppendLine("WHERE nota.tiponota = 1");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtNotaItens.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                conn.Close();
            }
            #endregion

            #region Grava os dados no Gestão
            using (MO.DataClassesDataContext db = new MO.DataClassesDataContext())
            {
                db.Connection.ConnectionString = MO.cwkGlobal.CONN_STRING;

                List<MO.Cidade> cidadesExistentes = db.Cidades.ToList();
                List<MO.UF> ufsExistentes = db.UFs.ToList();
                MO.TipoNota objTipoNota = db.TipoNotas.Where(t => t.ID == idTipoNota).First();

                if (db.Connection.State == ConnectionState.Closed)
                    db.Connection.Open();

                #region UFs e Cidades
                //MO.UF objUF;
                //foreach (DataRow row in dtUFs.Rows)
                //{
                //    objUF = new MO.UF();
                //    objUF.ID = Convert.ToInt32(row["id"]);
                //    objUF.IDPais = idPais;
                //    objUF.Sigla = row["sigla"].ToString();
                //    objUF.Nome = row["nome"].ToString();
                //    objUF.IBGE = "";
                //    ufs.Add(objUF);
                //}
                //dtUFs.Dispose();

                //MO.Cidade objCidade;
                //foreach (DataRow row in dtCidades.Rows)
                //{
                //    objCidade = new MO.Cidade();
                //    objCidade.ID = Convert.ToInt32(row["id"]);
                //    objCidade.Codigo = Convert.ToInt32(row["codigo"]);
                //    var uf = ufsExistentes.Where(c => c.Sigla.ToLower() == row["sigla"].ToString().ToLower());
                //    if (uf.Count() > 0)
                //    {
                //        objCidade.IDUF = uf.First().ID;
                //    }
                //    else
                //    {
                //        objCidade.UF = ufs.Where(u => u.ID == Convert.ToInt32(row["iduf"])).First();
                //    }
                //    objCidade.Nome = row["nome"].ToString();
                //    cidades.Add(objCidade);
                //}
                //dtCidades.Dispose();
                #endregion

                MO.GrupoEstoque objGrupoEstoque = new MO.GrupoEstoque();
                objGrupoEstoque.Classificacao = "1";
                objGrupoEstoque.bTitulo = MO.TituloType.Não;
                objGrupoEstoque.ClassificacaoProduto = MO.ClassificacaoGrupoEstoqueType.ProdutoAcabado;
                objGrupoEstoque.Codigo = 1;
                objGrupoEstoque.Nome = "IMPORTAÇÃO";
                objGrupoEstoque.NomeReduzido = "IMPORTAÇÃO";
                db.GrupoEstoques.InsertOnSubmit(objGrupoEstoque);

                #region Produtos
                MO.Produto objProduto;
                MO.TabelaPrecoProduto objTabelaPrecoProduto;
                int codigoTabPrecoProd = 1;
                foreach (DataRow row in dtProdutos.Rows)
                {
                    objProduto = new MO.Produto();
                    objProduto.ID = Convert.ToInt32(row["id"]);
                    objProduto.Nome = row["nome"].ToString();
                    objProduto.Codigo = row["codigo"].ToString();
                    objProduto.DescLonga1 = row["descricao"].ToString();
                    objProduto.IDFornecedor = idFornecedor;
                    objProduto.IDUnidade = idUnidade;
                    objProduto.GrupoEstoque = objGrupoEstoque;
                    objProduto.PrecoFornecedor = Convert.ToDecimal(row["valor"]);
                    objProduto.DescReduzida = objProduto.Nome.Length > 30 ? objProduto.Nome.Substring(0, 30) : objProduto.Nome;
                    objProduto.InfAdicionais = String.Empty;
                    objProduto.Barra = objProduto.Codigo;
                    objProduto.PrecoBase = objProduto.PrecoFornecedor;
                    objProduto.AliquotaIPI = 0;
                    objProduto.AliqContrib = 18;
                    objProduto.AliqContribNormal = 18;
                    objProduto.AliqNContrib = 18;
                    objProduto.AliqNContribNormal = 18;
                    objProduto.AliqCupomContrib = "";
                    objProduto.AliqCupomNContrib = "";
                    objProduto.AliqISS = 0;
                    objProduto.AliqSimplesSubst = 0;
                    objProduto.AliqSubstTributaria = 0;
                    objProduto.CustoFrete = 0;
                    objProduto.CustoIPI = 0;
                    objProduto.CustoExtra = 0;
                    objProduto.CST_Cofins = 0;
                    objProduto.CST_IPI = 0;
                    objProduto.CST_Pis = 0;
                    objProduto.DescontoFornecedor = 0;
                    objProduto.ENQ_IPI = 0;
                    objProduto.LucroST = 0;
                    objProduto.modBCICMS = 0;
                    objProduto.modBCST = 0;
                    objProduto.OrigemProd = 0;
                    objProduto.PesoBruto = 0;
                    objProduto.PesoLiquido = 0;
                    objProduto.pCOFINS_S08 = 0;
                    objProduto.pPIS_Q08 = 0;
                    objProduto.pRedBCST = 0;
                    objProduto.ReducaoContrib = 0;
                    objProduto.ReducaoNContrib = 0;
                    objProduto.Tributacao = 0;

                    objTabelaPrecoProduto = new MO.TabelaPrecoProduto();
                    objTabelaPrecoProduto.Codigo = codigoTabPrecoProd++;
                    objTabelaPrecoProduto.IDTabelaPreco = idTabelaPreco;
                    objTabelaPrecoProduto.MargemLucro = 0;
                    objTabelaPrecoProduto.PAcrescimo = 0;
                    objTabelaPrecoProduto.PDesconto = 0;
                    objTabelaPrecoProduto.Dt = DateTime.Now;
                    objProduto.TabelaPrecoProdutos.Add(objTabelaPrecoProduto);

                    produtos.Add(objProduto);
                }
                dtProdutos.Dispose();
                db.Produtos.InsertAllOnSubmit(produtos);
                #endregion

                #region Pessoas
                MO.Pessoa objPessoa;
                int aux = 0;
                foreach (DataRow row in dtPessoas.Rows)
                {
                    objPessoa = new MO.Pessoa();
                    objPessoa.ID = Convert.ToInt32(row["id"]);
                    objPessoa.IDFilial = idEmpresa;
                    objPessoa.Codigo = Convert.ToInt32(row["codigo"]);
                    objPessoa.bCliente = Convert.ToByte(row["bCliente"]);
                    objPessoa.bFornecedor = Convert.ToByte(row["bFornecedor"]);
                    objPessoa.bFuncionario = Convert.ToByte(row["bFuncionario"]);
                    objPessoa.bClienteRevenda = 0;
                    objPessoa.bFuncionarioCliente = 0;
                    objPessoa.bOrgaoPublico = 0;
                    objPessoa.bRevenda = 0;
                    objPessoa.bAtivo = Convert.ToByte(row["bAtivo"]);
                    objPessoa.Nome = row["nome"].ToString();

                    aux = Convert.ToInt32(row["tipoPessoa"]);
                    objPessoa.TipoPessoa = aux == 0 ? "Física" : "Jurídica";

                    aux = row["sexo"] is DBNull ? -1 : Convert.ToInt32(row["sexo"]);
                    switch (aux)
                    {
                        case 0:
                            objPessoa.Sexo = "Masculino";
                            break;
                        case 1:
                            objPessoa.Sexo = "Feminino";
                            break;
                        default:
                            objPessoa.Sexo = String.Empty;
                            break;
                    }

                    aux = row["estadocivil"] is DBNull ? -1 : Convert.ToInt32(row["estadocivil"]);
                    switch (aux)
                    {
                        case 0:
                            objPessoa.EstadoCivil = "Solteiro(a)";
                            break;
                        case 1:
                            objPessoa.EstadoCivil = "Casado(a)";
                            break;
                        case 2:
                            objPessoa.EstadoCivil = "Separado(a) Judicialmente";
                            break;
                        case 3:
                            objPessoa.EstadoCivil = "Divorciado(a)";
                            break;
                        case 4:
                            objPessoa.EstadoCivil = "Viúvo(a)";
                            break;
                        default:
                            objPessoa.EstadoCivil = String.Empty;
                            break;
                    }

                    objPessoa.Apelido = row["apelido"] is DBNull ? String.Empty : row["apelido"].ToString();
                    objPessoa.Fantasia = row["fantasia"] is DBNull ? String.Empty : row["fantasia"].ToString();
                    objPessoa.CNPJ_CPF = row["cnpj_cpf"].ToString();
                    objPessoa.Inscricao = row["inscricao"] is DBNull ? String.Empty : row["inscricao"].ToString();
                    objPessoa.RG = row["rg"] is DBNull ? String.Empty : row["rg"].ToString();
                    objPessoa.Http = row["http"] is DBNull ? String.Empty : row["http"].ToString();
                    objPessoa.Email1 = row["email1"] is DBNull ? String.Empty : row["email1"].ToString();
                    objPessoa.Email2 = row["email2"] is DBNull ? String.Empty : row["email2"].ToString();
                    objPessoa.DtNascimento = row["dtNascimento"] is DBNull ? null : (DateTime?)row["dtNascimento"];
                    objPessoa.Observacao = row["observacao"] is DBNull ? String.Empty : row["observacao"].ToString();
                    objPessoa.DtCadastro = DateTime.Now;
                    objPessoa.NumeroBanco = row["numBanco"] is DBNull ? (short)0 : Convert.ToInt16(row["numBanco"]);
                    objPessoa.Agencia = row["numAgencia"] is DBNull ? String.Empty : row["numAgencia"].ToString();
                    objPessoa.ContaCorrente = row["numContaCorrente"] is DBNull ? String.Empty : row["numContaCorrente"].ToString();

                    if (!(row["rua"] is DBNull && row["numero"] is DBNull && row["bairro"] is DBNull && row["idCidade"] is DBNull
                        && row["cep"] is DBNull && row["contato"] is DBNull))
                    {
                        MO.Endereco objEndereco = new MO.Endereco();
                        objEndereco.bPrincipal = 1;
                        objEndereco.bAtivo = 1;
                        objEndereco.bCobranca = 1;
                        objEndereco.Rua = row["rua"] is DBNull ? String.Empty : row["rua"].ToString();
                        objEndereco.Numero = row["numero"] is DBNull ? String.Empty : row["numero"].ToString();
                        objEndereco.Bairro = row["bairro"] is DBNull ? String.Empty : row["bairro"].ToString();
                        var cid = cidadesExistentes.Where(c => c.Nome.ToLower() == row["nomeCidade"].ToString().ToLower());
                        if (cid.Count() > 0)
                        {
                            objEndereco.Cidade = cid.First();
                        }
                        //else
                        //{
                        //    aux = row["idCidade"] is DBNull ? 0 : Convert.ToInt32(row["idCidade"]);
                        //    if (aux > 0)
                        //        objEndereco.Cidade = cidades.Where(c => c.ID == aux).First();
                        //}
                        objEndereco.CEP = row["cep"] is DBNull ? String.Empty : row["cep"].ToString();
                        objEndereco.Descricao = row["complemento"] is DBNull ? String.Empty : row["complemento"].ToString();
                        if (row["contato"] is DBNull)
                        {
                            objEndereco.bEntrega = 0;
                            objEndereco.Contato = String.Empty;
                        }
                        else
                        {
                            objEndereco.bEntrega = 1;
                            objEndereco.Contato = row["contato"].ToString();
                        }
                        objPessoa.Enderecos.Add(objEndereco);
                    }

                    int seq = 1;
                    if (!(row["telefoneRes"] is DBNull))
                    {
                        MO.Telefone objTelefone = new MO.Telefone();
                        objTelefone.bPrincipal = 1;
                        objTelefone.Numero = row["telefoneRes"].ToString();
                        objTelefone.Tipo = "Residencial";
                        objTelefone.Sequencia = seq++;
                        objPessoa.Telefones.Add(objTelefone);
                    }

                    if (!(row["telefoneCel"] is DBNull))
                    {
                        MO.Telefone objTelefone = new MO.Telefone();
                        objTelefone.bPrincipal = (byte)(seq == 1 ? 1 : 0);
                        objTelefone.Numero = row["telefoneCel"].ToString();
                        objTelefone.Tipo = "Celular";
                        objTelefone.Sequencia = seq++;
                        objPessoa.Telefones.Add(objTelefone);
                    }

                    if (!(row["telefoneCom"] is DBNull))
                    {
                        MO.Telefone objTelefone = new MO.Telefone();
                        objTelefone.bPrincipal = (byte)(seq == 1 ? 1 : 0);
                        objTelefone.Numero = row["telefoneCom"].ToString();
                        objTelefone.Tipo = "Comercial";
                        objTelefone.Sequencia = seq++;
                        objPessoa.Telefones.Add(objTelefone);
                    }

                    if (!(row["telefoneFax"] is DBNull))
                    {
                        MO.Telefone objTelefone = new MO.Telefone();
                        objTelefone.bPrincipal = (byte)(seq == 1 ? 1 : 0);
                        objTelefone.Numero = row["telefoneFax"].ToString();
                        objTelefone.Tipo = "Fax";
                        objTelefone.Sequencia = seq++;
                        objPessoa.Telefones.Add(objTelefone);
                    }

                    if (objPessoa.bCliente == 1)
                    {
                        MO.Cliente objCliente = new MO.Cliente();
                        objCliente.bContribuinte = 1;
                        objCliente.bBloqueiaCondicao = 0;
                        objCliente.bBloqueiaTabelaPreco = 0;
                        objCliente.NomeConjuge = row["nomeConjuge"] is DBNull ? String.Empty : row["nomeConjuge"].ToString();
                        objCliente.NomePai = row["nomePai"] is DBNull ? String.Empty : row["nomePai"].ToString();
                        objCliente.NomeMae = row["nomeMae"] is DBNull ? String.Empty : row["nomeMae"].ToString();
                        objPessoa.Clientes.Add(objCliente);
                    }

                    if (objPessoa.bFuncionario == 1)
                    {
                        MO.Funcionario objFuncionario = new MO.Funcionario();
                        objFuncionario.NumCarteiraTrab = row["numCarteiraTrab"] is DBNull ? String.Empty : row["numCarteiraTrab"].ToString();
                        objPessoa.Funcionarios.Add(objFuncionario);
                    }

                    pessoas.Add(objPessoa);
                }
                dtPessoas.Dispose();
                db.Pessoas.InsertAllOnSubmit(pessoas);
                #endregion

                #region Cadastros do Financeiro
                MO.Acrescimo objAcrescimo;
                foreach (DataRow row in dtAcrescimos.Rows)
                {
                    objAcrescimo = new MO.Acrescimo();
                    objAcrescimo.ID = Convert.ToInt32(row["id"]);
                    objAcrescimo.Codigo = Convert.ToInt32(row["Codigo"]);
                    objAcrescimo.Nome = row["Nome"].ToString();
                    objAcrescimo.PercMulta = Convert.ToDecimal(row["PercMulta"]);
                    objAcrescimo.DiasMulta = Convert.ToInt32(row["DiasMulta"]);
                    objAcrescimo.PercJuro = Convert.ToDecimal(row["PercJuro"]);
                    objAcrescimo.DiasJuro = Convert.ToInt32(row["DiasJuro"]);
                    objAcrescimo.JurosMulta = Convert.ToByte(row["JurosMulta"]);
                    objAcrescimo.DiasProtesto = Convert.ToInt32(row["DiasProtesto"]);
                    objAcrescimo.InstrucaoCaixa1 = row["InstrucaoCaixa1"].ToString();
                    objAcrescimo.InstrucaoCaixa2 = row["InstrucaoCaixa2"].ToString();
                    objAcrescimo.InstrucaoCaixa3 = row["InstrucaoCaixa3"].ToString();
                    objAcrescimo.InstrucaoCaixa4 = row["InstrucaoCaixa4"].ToString();
                    objAcrescimo.InstrucaoCaixa5 = row["InstrucaoCaixa5"].ToString();
                    objAcrescimo.InstrucaoCaixa6 = row["InstrucaoCaixa6"].ToString();
                    objAcrescimo.InstrucaoCaixa7 = row["InstrucaoCaixa7"].ToString();
                    acrescimos.Add(objAcrescimo);
                }
                dtAcrescimos.Dispose();
                db.Acrescimos.InsertAllOnSubmit(acrescimos);

                MO.Banco objBanco;
                foreach (DataRow row in dtContasBancarias.Rows)
                {
                    objBanco = new MO.Banco();
                    objBanco.ID = Convert.ToInt32(row["id"]);
                    objBanco.Codigo = Convert.ToInt32(row["codigo"]);
                    objBanco.Nome = row["nome"].ToString();
                    objBanco.Ordem = row["ordem"].ToString();
                    objBanco.ArquivoDeLicenca = row["arquivodelicenca"].ToString();
                    objBanco.CodigoAgencia = row["codigoagencia"].ToString();
                    objBanco.ContaCorrente = row["contacorrente"].ToString();
                    objBanco.CodigoCedente = row["codigocedente"].ToString();
                    objBanco.CaminhoImagens = row["caminhoimagens"].ToString();
                    objBanco.LayoutBoleto = row["layoutboleto"].ToString();
                    objBanco.CaminhoLayoutBoleto = row["caminholayoutboleto"].ToString();
                    objBanco.InstrucoesCaixa = row["instrucoescaixa"].ToString();
                    objBanco.Logotipo = row["logotipo"].ToString();
                    bancos.Add(objBanco);
                }
                dtContasBancarias.Dispose();
                db.Bancos.InsertAllOnSubmit(bancos);

                MO.TipoDocumento objTipoDocumento;
                foreach (DataRow row in dtTiposDocumento.Rows)
                {
                    objTipoDocumento = new MO.TipoDocumento();
                    objTipoDocumento.ID = Convert.ToInt32(row["id"]);
                    objTipoDocumento.Codigo = Convert.ToInt32(row["codigo"]);
                    objTipoDocumento.Nome = row["nome"].ToString();
                    objTipoDocumento.DescReduzida = row["descreduzida"].ToString();
                    tiposDocumento.Add(objTipoDocumento);
                }
                dtTiposDocumento.Dispose();
                db.TipoDocumentos.InsertAllOnSubmit(tiposDocumento);

                MO.Historico objHistorico;
                foreach (DataRow row in dtHistoricos.Rows)
                {
                    objHistorico = new MO.Historico();
                    objHistorico.ID = Convert.ToInt32(row["id"]);
                    objHistorico.Codigo = Convert.ToInt32(row["codigo"]);
                    objHistorico.Nome = row["nome"].ToString();
                    historicos.Add(objHistorico);
                }
                dtHistoricos.Dispose();
                db.Historicos.InsertAllOnSubmit(historicos);

                MO.PlanoConta objPlanoConta;
                foreach (DataRow row in dtPlanosConta.Rows)
                {
                    objPlanoConta = new MO.PlanoConta();
                    objPlanoConta.ID = Convert.ToInt32(row["id"]);
                    objPlanoConta.Codigo = Convert.ToInt32(row["codigo"]);
                    objPlanoConta.Nome = row["nome"].ToString();
                    objPlanoConta.Classificacao = row["classificacao"].ToString();
                    objPlanoConta.bTitulo = (MO.TituloType)row["bTitulo"];
                    objPlanoConta.DebCre = (MO.DebCreType)row["debcre"];
                    planoContas.Add(objPlanoConta);
                }
                dtPlanosConta.Dispose();
                db.PlanoContas.InsertAllOnSubmit(planoContas);

                MO.Condicao objCondicao;
                foreach (DataRow row in dtCondicoes.Rows)
                {
                    objCondicao = new MO.Condicao();
                    objCondicao.ID = Convert.ToInt32(row["id"]);
                    objCondicao.Codigo = Convert.ToInt32(row["codigo"]);
                    objCondicao.Nome = row["nome"].ToString();
                    objCondicao.Divide = Convert.ToInt32(row["bDivide"]);
                    objCondicao.QtParcela = Convert.ToInt32(row["qtparcela"]);
                    condicoes.Add(objCondicao);
                }
                dtCondicoes.Dispose();
                db.Condicaos.InsertAllOnSubmit(condicoes);

                MO.CondicaoParcela objCondicaoParcela;
                foreach (DataRow row in dtCondicaoParcelas.Rows)
                {
                    objCondicaoParcela = new MO.CondicaoParcela();
                    objCondicaoParcela.ID = Convert.ToInt32(row["id"]);
                    objCondicaoParcela.Parcela = Convert.ToInt32(row["parcela"]);
                    objCondicaoParcela.TipoDt = row["tipodt"].ToString();
                    objCondicaoParcela.Dt = Convert.ToDateTime(row["dt"]);
                    objCondicaoParcela.DiaMes = Convert.ToInt32(row["diames"]);
                    objCondicaoParcela.TipoVlr = row["tipovlr"].ToString();
                    objCondicaoParcela.VlrPerc = Convert.ToDecimal(row["vlrperc"]);
                    objCondicaoParcela.Entrada = Convert.ToByte(row["bentrada"]);
                    objCondicaoParcela.TipoDocumento = tiposDocumento.Where(t => t.ID == Convert.ToInt32(row["idtipodocumento"])).First();
                    objCondicaoParcela.Condicao = condicoes.Where(c => c.ID == Convert.ToInt32(row["idcondicao"])).First();
                    condicaoParcelas.Add(objCondicaoParcela);
                }
                dtCondicaoParcelas.Dispose();
                db.CondicaoParcelas.InsertAllOnSubmit(condicaoParcelas);
                #endregion

                #region MovCaixa

                MO.MovCaixa objMovCaixa;
                foreach (DataRow row in dtMovCaixas.Rows)
                {
                    objMovCaixa = new MO.MovCaixa();
                    objMovCaixa.ID = Convert.ToInt32(row["id"]);
                    objMovCaixa.Codigo = Convert.ToInt32(row["codigo"]);
                    objMovCaixa.Banco = bancos.Where(b => b.ID == Convert.ToInt32(row["idbanco"])).First();
                    objMovCaixa.IDFilial = idEmpresa;
                    objMovCaixa.Dt = Convert.ToDateTime(row["dt"]);
                    objMovCaixa.DtDigitacao = Convert.ToDateTime(row["dtdigitacao"]);
                    objMovCaixa.NumDocumento = row["numdocumento"].ToString();
                    objMovCaixa.Ent_Sai = Convert.ToInt32(row["entsai"]);
                    objMovCaixa.Valor = Convert.ToDecimal(row["valor"]);
                    objMovCaixa.Historico = historicos.Where(h => h.ID == Convert.ToInt32(row["idhistorico"])).First();
                    objMovCaixa.ComplementoHist = row["complementohist"].ToString();
                    objMovCaixa.NumBanco = row["numbanco"].ToString();
                    objMovCaixa.NumAgencia = row["numagencia"].ToString();
                    objMovCaixa.NumContaCorrente = row["numcontacorrente"].ToString();
                    objMovCaixa.NumCheque = row["numcheque"].ToString();
                    objMovCaixa.Emitente = row["emitente"].ToString();
                    movCaixas.Add(objMovCaixa);
                }
                dtMovCaixas.Dispose();
                db.MovCaixas.InsertAllOnSubmit(movCaixas);

                #endregion

                #region Documentos e Movimentos
                MO.Documento objDocumento;
                foreach (DataRow row in dtDocumentos.Rows)
                {
                    objDocumento = new MO.Documento();
                    objDocumento.ID = Convert.ToInt32(row["id"]);
                    objDocumento.Codigo = Convert.ToInt32(row["codigo"]);
                    var tipo = Convert.ToInt32(row["tipo"]);
                    objDocumento.Tipo = (MO.TipoDocumentoType)(tipo == 0 ? 1 : 2);
                    objDocumento.IDFilial = idEmpresa;
                    objDocumento.IDPortador = idPortador;
                    objDocumento.Pessoa = pessoas.Where(p => p.ID == Convert.ToInt32(row["idpessoa"])).First();
                    objDocumento.TipoDocumento = tiposDocumento.Where(t => t.ID == Convert.ToInt32(row["idtipodocumento"])).First();
                    objDocumento.Banco = bancos.Where(b => b.ID == Convert.ToInt32(row["idbanco"])).First();
                    objDocumento.Dt = Convert.ToDateTime(row["dt"]);
                    objDocumento.DtDigitacao = Convert.ToDateTime(row["dtdigitacao"]);
                    objDocumento.DtVencimento = Convert.ToDateTime(row["dtvencimento"]);
                    objDocumento.DtPrevisao = Convert.ToDateTime(row["dtprevisao"]);
                    objDocumento.NumDocumento = row["numdocumento"].ToString();
                    objDocumento.Condicao = condicoes.Where(c => c.ID == Convert.ToInt32(row["idcondicao"])).First();
                    objDocumento.Parcela = Convert.ToInt32(row["parcela"]);
                    objDocumento.QtParcela = Convert.ToInt32(row["qtparcela"]);
                    objDocumento.Valor = Convert.ToDecimal(row["valor"]);
                    objDocumento.ValorTotal = Convert.ToDecimal(row["valortotal"]);
                    objDocumento.Historico = historicos.Where(h => h.ID == Convert.ToInt32(row["idhistorico"])).First();
                    objDocumento.ComplementoHist = row["complementohist"].ToString();
                    objDocumento.CodBanco = Convert.ToInt32(row["codbanco"]);
                    objDocumento.Banco_Str = row["bancostr"].ToString();
                    objDocumento.Agencia = row["agencia"].ToString();
                    objDocumento.Conta = row["conta"].ToString();
                    objDocumento.Cheque = row["cheque"].ToString();
                    objDocumento.Emitente = row["emitente"].ToString();
                    objDocumento.bEntrada = Convert.ToByte(row["bentrada"]);
                    objDocumento.Saldo = Convert.ToDecimal(row["saldo"]);
                    objDocumento.Situacao = row["situacao"].ToString();
                    objDocumento.Acrescimo = acrescimos.Where(a => a.ID == Convert.ToInt32(row["idAcrescimo"])).First();
                    documentos.Add(objDocumento);
                }
                dtDocumentos.Dispose();
                db.Documentos.InsertAllOnSubmit(documentos);

                MO.Movimento objMovimento;
                foreach (DataRow row in dtMovimentos.Rows)
                {
                    objMovimento = new MO.Movimento();
                    objMovimento.Codigo = Convert.ToInt32(row["codigo"]);
                    objMovimento.Sequencia = Convert.ToInt32(row["sequencia"]);
                    objMovimento.Origem = (MO.OrigemMovimentoType)row["origem"];
                    objMovimento.Documento = documentos.Where(d => d.ID == Convert.ToInt32(row["iddocumento"])).First();
                    if (!(row["idmovcaixa"] is DBNull))
                    {
                        objMovimento.MovCaixa = movCaixas.Where(m => m.ID == Convert.ToInt32(row["idmovcaixa"])).First();
                    }

                    objMovimento.Dt = Convert.ToDateTime(row["dt"]);
                    objMovimento.Operacao = (MO.OperacaoMovimentoType)row["operacao"];
                    objMovimento.Tipo = (MO.TipoMovimentoType)row["tipo"];
                    objMovimento.PlanoConta = planoContas.Where(p => p.ID == Convert.ToInt32(row["idplanoconta"])).First();
                    objMovimento.Historico = historicos.Where(h => h.ID == Convert.ToInt32(row["idhistorico"])).First();
                    objMovimento.ComplementoHist = row["complementohist"].ToString();
                    objMovimento.Valor = Convert.ToDecimal(row["valor"]);
                    movimentos.Add(objMovimento);
                }
                dtMovimentos.Dispose();
                db.Movimentos.InsertAllOnSubmit(movimentos);
                #endregion

                #region Notas
                //    MO.Nota objNota;
                //    foreach (DataRow row in dtNotas.Rows)
                //    {
                //        objNota = new MO.Nota();
                //        objNota.ID = Convert.ToInt32(row["id"]);
                //        objNota.Codigo = objNota.ID;
                //        objNota.Numero = Convert.ToInt32(row["numero"]);
                //        objNota.DtDigitacao = Convert.ToDateTime(row["data"]);
                //        objNota.Dt = Convert.ToDateTime(row["emissao"]);
                //        objNota.Pessoa = pessoas.Where(p => p.ID == Convert.ToInt32(row["idcliente"])).First();
                //        objNota.PessoaEndereco = row["clienteendereco"].ToString();
                //        objNota.PessoaBairro = row["clientebairro"].ToString();
                //        objNota.PessoaCidade = row["clientecidade"].ToString();
                //        objNota.PessoaUF = row["clienteuf"].ToString();
                //        objNota.PessoaCEP = row["clientecep"].ToString();
                //        objNota.PessoaTelefone = row["clientefone"].ToString();
                //        objNota.PessoaCNPJCPF = row["clientecnpjcpf"].ToString();
                //        objNota.PessoaInscRG = row["clienteinscricao"].ToString();
                //        objNota.Condicao = condicoes.Where(c => c.ID == Convert.ToInt32(row["idcondicao"])).First();
                //        objNota.TotalNota = Convert.ToDecimal(row["totalnota"]);
                //        objNota.ObservacaoSistema = row["observacao"].ToString();                     
                //        objNota.IDFilial = idEmpresa;
                //        objNota.PessoaNome = row["clientenome"].ToString();
                //        if (objNota.PessoaNome.Length > 60)
                //            objNota.PessoaNome = objNota.PessoaNome.Substring(0, 60);
                //        objNota.TotalProduto = row["totalproduto"] is DBNull ? 0 : Convert.ToDecimal(row["totalproduto"]);
                //        objNota.ValorDesconto = row["desconto"] is DBNull ? 0 : Convert.ToDecimal(row["desconto"]);
                //        objNota.TipoNota = objTipoNota;
                //        objNota.Serie = objTipoNota.Serie;
                //        notas.Add(objNota);
                //    }
                //    dtNotas.Dispose();
                //    db.Notas.InsertAllOnSubmit(notas);

                //    MO.NotaItem objNotaItem;
                //    foreach (DataRow row in dtNotaItens.Rows)
                //    {
                //        objNotaItem = new MO.NotaItem();
                //        objNotaItem.Sequencia = Convert.ToInt32(row["sequencia"]);
                //        var prod = produtos.Where(p => p.ID == Convert.ToInt32(row["idservico"]));
                //        if (prod.Count() > 0)
                //            objNotaItem.Produto = prod.First();
                //        else
                //            continue;
                //        objNotaItem.ProdutoDescReduzida = objNotaItem.Produto.DescReduzida;
                //        objNotaItem.ProdutoNome = objNotaItem.Produto.Nome;
                //        objNotaItem.Quantidade = Convert.ToDecimal(row["quantidade"]);
                //        objNotaItem.Valor = Convert.ToDecimal(row["valorunitario"]);
                //        //total]
                //        objNotaItem.Nota = notas.Where(n => n.ID == Convert.ToInt32(row["idnota"])).First();
                //        notaItems.Add(objNotaItem);
                //    }
                //    dtNotaItens.Dispose();
                //    db.NotaItems.InsertAllOnSubmit(notaItems);
                #endregion

                db.SubmitChanges();
            }
            #endregion
        }
    }
}
