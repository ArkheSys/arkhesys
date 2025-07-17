using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MO = Modelo;
using System.IO;

namespace cwkGestao.Importacao.Cwork
{
    public class ImportacaoOS
    {

        private static cwkGestao.Negocio.UFController ufController = cwkGestao.Negocio.UFController.Instancia;
        private static cwkGestao.Negocio.CidadeController cidadeController = cwkGestao.Negocio.CidadeController.Instancia;
        private static cwkGestao.Negocio.PessoaController pessoaController = cwkGestao.Negocio.PessoaController.Instancia;
        private static cwkGestao.Negocio.ProdutoController produtoController = cwkGestao.Negocio.ProdutoController.Instancia;
        List<string> log = new List<string>();

        internal void Importar(string connStringAdm)
        {
            #region Declaração das variáveis
            StringBuilder builder = new StringBuilder();

            DataTable dtSistema = new DataTable();
            DataTable dtVersao = new DataTable();
            DataTable dtTipoAtendimento = new DataTable();
            DataTable dtAcompanhamentoOS = new DataTable();
            DataTable dtClienteRevenda = new DataTable();
            DataTable dtServicos = new DataTable();
            DataTable dtServicoOS = new DataTable();
            DataTable dtStatusOS = new DataTable();
            DataTable dtOrdemServico = new DataTable();
            DataTable dtPedido = new DataTable();
            DataTable dtPedidoEmpresa = new DataTable();

            List<Modelo.ClienteRevenda> clientesRevenda = new List<Modelo.ClienteRevenda>();

            #endregion

            #region Recupera os dados do Administração
            using (SqlConnection conn = new SqlConnection(connStringAdm))
            {
                conn.Open();
                SqlCommand cmd;

                builder.AppendLine("SELECT * FROM sistema");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtSistema.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM versao");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtVersao.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM tipoAtendimento");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtTipoAtendimento.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT acompanhamentoOS.*, uant.login AS responsavelant, uatu.login AS responsavel FROM acompanhamentoOS");
                builder.AppendLine("LEFT JOIN Cw_Usuario uant ON uant.id = acompanhamentoOS.idResponsavelAnt");
                builder.AppendLine("LEFT JOIN Cw_Usuario uatu ON uatu.id = acompanhamentoOS.idResponsavelAtu");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtAcompanhamentoOS.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT clienterevenda.*, pessoa.codigo AS pessoaCodigo, pessoa.nome AS pessoaNome");
                builder.AppendLine(", cidade.nome AS cidadeNome, uf.sigla AS ufSigla FROM clienterevenda");
                builder.AppendLine("LEFT JOIN pessoa ON pessoa.id = clienterevenda.idpessoa");
                builder.AppendLine("INNER JOIN cidade ON cidade.id = clienterevenda.idcidade");
                builder.AppendLine("INNER JOIN uf ON uf.id = cidade.iduf");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtClienteRevenda.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM servico");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtServicos.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT servicoOS.*, servico.nome AS servicoNome, Cw_Usuario.login AS responsavel FROM servicoOS");
                builder.AppendLine("LEFT JOIN servico ON servico.id = servicoOS.idServico");
                builder.AppendLine("INNER JOIN Cw_Usuario ON Cw_Usuario.id = servicoOS.idresponsavel");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtServicoOS.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT * FROM statusOrdemServico");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtStatusOS.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT ordemservico.*, pessoa.codigo AS pessoaCodigo, pessoa.nome AS pessoaNome, Cw_Usuario.login AS responsavel FROM ordemservico");
                builder.AppendLine("INNER JOIN pessoa ON pessoa.id = ordemservico.idcliente");
                builder.AppendLine("INNER JOIN Cw_Usuario ON Cw_Usuario.id = ordemservico.idresponsavel");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtOrdemServico.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT pedido.*, pessoa.codigo AS pessoaCodigo, pessoa.nome AS pessoaNome FROM pedido");
                builder.AppendLine("INNER JOIN pessoa ON pessoa.id = pedido.idpessoa");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtPedido.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                builder.AppendLine("SELECT pedidoempresa.*, servico.tipo, servico.nome AS servicoNome, servico.Codigo AS servicoCodigo FROM pedidoempresa");
                builder.AppendLine("INNER JOIN servico ON servico.id = pedidoempresa.idservico");
                cmd = new SqlCommand(builder.ToString(), conn);
                dtPedidoEmpresa.Load(cmd.ExecuteReader());
                builder.Remove(0, builder.Length);

                conn.Close();
            }
            #endregion

            IList<Modelo.UF> ufs = ufController.GetAll();
            IList<Modelo.Cidade> cidades = cidadeController.GetAll();
            IList<Modelo.Pessoa> pessoas = pessoaController.GetAll();
            IList<MO.Cw_Usuario> usuarios = cwkControleUsuario.Facade.GetAllList();
            IList<Modelo.Produto> produtos = produtoController.GetAll();

            #region Grava os dados no Gestão
            using (SqlConnection conn = new SqlConnection(MO.cwkGlobal.CONN_STRING))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                ImportaSistema(dtSistema, conn);
                ImportaVersao(dtVersao, conn);
                ImportaTipoAtendimento(dtTipoAtendimento, conn);
                ImportaStatusOS(dtStatusOS, conn);
                ImportaServico(dtServicos, conn);
                ImportaClienteRevenda(dtClienteRevenda, conn, ufs, cidades, pessoas);
                ImportaOrdemServico(dtOrdemServico, conn, pessoas, usuarios);
                ImportaServicoOS(dtServicoOS, conn, usuarios);
                ImportaAcompanhamentoOS(dtAcompanhamentoOS, conn, usuarios);
                ImportaPedido(dtPedido, conn, pessoas);
                ImportaPedidoEmpresa(dtPedidoEmpresa, conn, produtos);

                conn.Close();

                StreamWriter str = new StreamWriter("log.txt", false);
                foreach (string item in log)
                {
                    str.WriteLine(item);
                }
                str.Close();
            }
            #endregion
        }

        private void ImportaPedidoEmpresa(DataTable dtPedidoEmpresa, SqlConnection conn, IList<Modelo.Produto> produtos)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("IDPedido", typeof(int)),
                    new DataColumn("Seq", typeof(int)),
                    new DataColumn("IDClienteRevenda", typeof(int)),
                    new DataColumn("Observacao", typeof(string)),
                    new DataColumn("IncData", typeof(DateTime)),
                    new DataColumn("IncHora", typeof(DateTime)),
                    new DataColumn("IncUsuario", typeof(string)),
                    new DataColumn("AltData", typeof(DateTime)),
                    new DataColumn("AltHora", typeof(DateTime)),
                    new DataColumn("AltUsuario", typeof(string)),
                    new DataColumn("IDServico", typeof(int)),
                    new DataColumn("Senha", typeof(string)),
                    new DataColumn("ContraSenha", typeof(string)),
                    new DataColumn("TipoLicenca", typeof(int)),
                    new DataColumn("Quantidade", typeof(int)),
                    new DataColumn("InfMaquina", typeof(string)),
                    new DataColumn("ValorProduto", typeof(decimal)),
                    new DataColumn("IDProduto", typeof(int)),
                    new DataColumn("TipoServico", typeof(int)),
                });

                int? idProduto, idServico;
                foreach (DataRow row in dtPedidoEmpresa.Rows)
                {
                    if (row["tipo"].ToString() == "1")
                    {
                        var aux = produtos.Where(p => p.Codigo == row["servicoCodigo"].ToString());
                        if (aux.Count() > 0)
                            idProduto = aux.First().ID;
                        else
                        {
                            idProduto = null;
                            log.Add("O Produto (" + row["servicoNome"] + ") do PedidoEmpresa (" + row["ID"] + ") não foi encontrado no cadastro.");
                        }
                        idServico = null;
                    }
                    else
                    {
                        idProduto = null;
                        idServico = Convert.ToInt32(row["idservico"]);
                    }

                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["idpedido"],
                            row["seq"],
                            row["idclienterevenda"],
                            row["observacao"],
                            row["incdata"],
                            row["inchora"],
                            row["incusuario"],
                            row["altdata"],
                            row["althora"],
                            row["altusuario"],
                            idServico,
                            row["senha"],
                            row["contrasenha"],
                            row["tipolicenca"],
                            row["quantidade"],
                            row["infmaquina"],
                            row["valorproduto"],
                            idProduto,
                            row["tipo"],
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "CwkPedidoEmpresa";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar ServiçoOS: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaPedido(DataTable dtPedido, SqlConnection conn, IList<Modelo.Pessoa> pessoas)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("Data", typeof(DateTime)),
                    new DataColumn("Hora", typeof(DateTime)),
                    new DataColumn("IDPessoa", typeof(int)),
                    new DataColumn("Solicitante", typeof(string)),
                    new DataColumn("IDSistema", typeof(int)),
                    new DataColumn("IDVersao", typeof(int)),
                    new DataColumn("Observacao", typeof(string)),
                    new DataColumn("DataSite", typeof(DateTime)),
                    new DataColumn("HoraSite", typeof(DateTime)),
                    new DataColumn("Liberado", typeof(string)),
                    new DataColumn("NumeroSerie", typeof(int)),
                    new DataColumn("IncData", typeof(DateTime)),
                    new DataColumn("IncHora", typeof(DateTime)),
                    new DataColumn("IncUsuario", typeof(string)),
                    new DataColumn("AltData", typeof(DateTime)),
                    new DataColumn("AltHora", typeof(DateTime)),
                    new DataColumn("AltUsuario", typeof(string)),
                    new DataColumn("Status", typeof(int)),
                    new DataColumn("MotivoCancelamento", typeof(string)),
                    new DataColumn("BDemonstracao", typeof(bool)),
                });
                int? idCliente;
                foreach (DataRow row in dtPedido.Rows)
                {
                    var pes = pessoas.Where(p => p.Codigo == Convert.ToInt32(row["pessoaCodigo"]));
                    if (pes.Count() > 0)
                    {
                        idCliente = pes.First().ID;
                    }
                    else
                    {
                        idCliente = null;
                        log.Add("A pessoa (" + row["pessoaNome"] + ") do pedido " + row["codigo"] + " não foi encontrada no cadastro.");
                    }

                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["data"],
                            row["hora"],
                            idCliente,
                            row["solicitante"],
                            row["idsistema"],
                            row["idversao"],
                            row["observacao"],
                            row["datasite"],
                            row["horasite"],
                            row["liberado"],
                            row["numeroserie"],
                            row["incdata"],
                            row["inchora"],
                            row["incusuario"], 
                            row["altdata"],
                            row["althora"],
                            row["altusuario"],
                            0,
                            String.Empty,
                            false,
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "CwkPedido";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar Pedido: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaAcompanhamentoOS(DataTable dtAcompanhamentoOS, SqlConnection conn, IList<MO.Cw_Usuario> usuarios)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("IDOrdemServico", typeof(int)),
                    new DataColumn("IDResponsavelAnt", typeof(int)),
                    new DataColumn("IDResponsavelAtu", typeof(int)),
                    new DataColumn("IDStatusAnt", typeof(int)),
                    new DataColumn("IDStatusAtu", typeof(int)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string))
                });

                int? idResponsavelAnt, idResponsavel;
                foreach (DataRow row in dtAcompanhamentoOS.Rows)
                {
                    var resp = usuarios.Where(u => u.Login == row["responsavel"].ToString());
                    if (resp.Count() > 0)
                        idResponsavel = resp.First().Id;
                    else
                    {
                        idResponsavel = null;
                        log.Add("O usuário (" + row["responsavel"] + ") do acompanhametoOS (" + row["codigo"] + ") não foi encontrado no cadastro.");
                    }

                    if (row["responsavelant"] is DBNull)
                        idResponsavelAnt = null;
                    else
                    {
                        resp = usuarios.Where(u => u.Login == row["responsavelant"].ToString());
                        if (resp.Count() > 0)
                            idResponsavelAnt = resp.First().Id;
                        else
                        {
                            idResponsavelAnt = null;
                            log.Add("O usuário (" + row["responsavelant"] + ") do acompanhamentoOS (" + row["codigo"] + ") não foi encontrado no cadastro.");
                        }
                    }

                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["idOrdemServico"],
                            idResponsavelAnt,
                            idResponsavel,
                            row["idStatusAnt"],
                            row["idStatusAtu"],
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"]
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "AcompanhamentoOS";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar AcompanhamentoOS: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaServicoOS(DataTable dtServicoOS, SqlConnection conn, IList<MO.Cw_Usuario> usuarios)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("IDOrdemServico", typeof(int)),
                    new DataColumn("IDServico", typeof(int)),
                    new DataColumn("Resumo", typeof(string)),
                    new DataColumn("Descricao", typeof(string)),
                    new DataColumn("IDResponsavel", typeof(int)),
                    new DataColumn("DataInicio", typeof(DateTime)),
                    new DataColumn("DataTermino", typeof(DateTime)),
                    new DataColumn("TempoHoras", typeof(DateTime)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string))
                });

                int? idResponsavel;
                foreach (DataRow row in dtServicoOS.Rows)
                {
                    var resp = usuarios.Where(u => u.Login == row["responsavel"].ToString());
                    if (resp.Count() > 0)
                        idResponsavel = resp.First().Id;
                    else
                    {
                        idResponsavel = null;
                        log.Add("O usuário (" + row["responsavel"] + ") do serviçoOS (" + row["codigo"] + ") não foi encontrado no cadastro.");
                    }

                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["idOrdemServico"],
                            row["idServico"],
                            row["resumo"],
                            row["descricao"],
                            idResponsavel,
                            row["dataInicio"],
                            row["dataTermino"],
                            row["tempoHoras"],
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"]
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "ServicoOS";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar ServiçoOS: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaOrdemServico(DataTable dtOrdemServico, SqlConnection conn, IList<Modelo.Pessoa> pessoas, IList<MO.Cw_Usuario> usuarios)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("Resumo", typeof(string)),
                    new DataColumn("IDCliente", typeof(int)),
                    new DataColumn("Contato", typeof(string)),
                    new DataColumn("IDTipoAtendimento", typeof(int)),
                    new DataColumn("IDSistema", typeof(int)),
                    new DataColumn("IDVersao", typeof(int)),
                    new DataColumn("IDStatus", typeof(int)),
                    new DataColumn("IDVersaoFechamento", typeof(int)),
                    new DataColumn("Descricao", typeof(string)),
                    new DataColumn("DataPrevisao", typeof(DateTime)),
                    new DataColumn("DataPrevisaoFinal", typeof(DateTime)),
                    new DataColumn("Lancado", typeof(bool)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string)),
                    new DataColumn("IDResponsavel", typeof(int)),
                    new DataColumn("IDClienteRevenda", typeof(int)),
                });

                int? idCliente, idResponsavel;
                foreach (DataRow row in dtOrdemServico.Rows)
                {
                    var resp = usuarios.Where(u => u.Login == row["responsavel"].ToString());
                    if (resp.Count() > 0)
                        idResponsavel = resp.First().Id;
                    else
                    {
                        idResponsavel = null;
                        log.Add("O usuário (" + row["responsavel"] + ") da ordem de serviço (" + row["codigo"] + ") não foi encontrado no cadastro.");
                    }


                    var pes = pessoas.Where(p => p.Codigo == Convert.ToInt32(row["pessoaCodigo"]));
                    if (pes.Count() > 0)
                    {
                        idCliente = pes.First().ID;
                    }
                    else
                    {
                        idCliente = null;
                        log.Add("A pessoa (" + row["pessoaNome"] + ") da ordem de serviço " + row["codigo"] + " não foi encontrada no cadastro.");
                    }


                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["resumo"],
                            idCliente,
                            row["contato"],
                            row["idTipoAtendimento"],
                            row["idSistema"],
                            row["idVersao"],
                            row["idStatus"],
                            row["idVersaoFechamento"],
                            row["descricao"],
                            row["dataPrevisao"],
                            row["dataPrevisaoFinal"],
                            row["lancado"],
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"],
                            idResponsavel,
                            row["idClienteRevenda"],
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "OrdemServico";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar Ordem Serviço: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaClienteRevenda(DataTable dtClienteRevenda, SqlConnection conn, IList<Modelo.UF> ufs, IList<Modelo.Cidade> cidades, IList<Modelo.Pessoa> pessoas)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("IdFirebird", typeof(int)),
                    new DataColumn("IDPessoa", typeof(int)),
                    new DataColumn("IDClienteRevenda", typeof(int)),
                    new DataColumn("Nome", typeof(string)),
                    new DataColumn("Principal", typeof(bool)),
                    new DataColumn("Endereco", typeof(string)),
                    new DataColumn("IDCidade", typeof(int)),
                    new DataColumn("Cep", typeof(string)),
                    new DataColumn("Cnpj", typeof(string)),
                    new DataColumn("Cpf", typeof(string)),
                    new DataColumn("Telefone", typeof(string)),
                    new DataColumn("Fax", typeof(string)),
                    new DataColumn("Celular", typeof(string)),
                    new DataColumn("Contato", typeof(string)),
                    new DataColumn("Http", typeof(string)),
                    new DataColumn("Email", typeof(string)),
                    new DataColumn("Observacao", typeof(string)),
                    new DataColumn("TipoLicenca", typeof(short)),
                    new DataColumn("Quantidade", typeof(int)),
                    new DataColumn("PontoVirtual", typeof(bool)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string)),
                    new DataColumn("Chave", typeof(string)),
                    new DataColumn("Cei", typeof(string)),
                    new DataColumn("BdAlterado", typeof(bool)),
                    new DataColumn("Inscricao", typeof(string)),
                    new DataColumn("Bairro", typeof(string)),
                    new DataColumn("Tipo", typeof(bool)),
                    new DataColumn("Numero", typeof(string)),
                    new DataColumn("Responsavel", typeof(string)),
                    new DataColumn("IdentificacaoHD", typeof(string)),
                    new DataColumn("Complemento", typeof(string))
                });

                int? idCidade, idPessoa;
                foreach (DataRow row in dtClienteRevenda.Rows)
                {
                    var cid = cidades.Where(c => c.Nome.ToUpper() == row["cidadeNome"].ToString().TrimEnd().ToUpper());
                    if (cid.Count() > 0)
                    {
                        idCidade = cid.First().ID;
                    }
                    else
                    {
                        var uf = ufs.Where(u => u.Sigla == row["ufSigla"].ToString());
                        if (uf.Count() > 0)
                        {
                            SqlParameter[] parms = new SqlParameter[]
			                {
				                new SqlParameter ("@id", SqlDbType.Int),
                                new SqlParameter ("@codigo", SqlDbType.Int),
                                new SqlParameter ("@nome", SqlDbType.VarChar),
                                new SqlParameter ("@iduf", SqlDbType.Int),
                                new SqlParameter ("@ibge", SqlDbType.VarChar),
                            };
                            parms[0].Direction = ParameterDirection.Output;
                            parms[1].Value = cidadeController.MaxCodigo();
                            parms[2].Value = row["cidadeNome"].ToString().TrimEnd();
                            parms[3].Value = uf.First().ID;
                            parms[4].Value = String.Empty;
                            string sql = "INSERT INTO Cidade VALUES (@codigo, @nome, @iduf, @ibge) SET @id = SCOPE_IDENTITY()";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddRange(parms);
                            cmd.ExecuteNonQuery();
                            idCidade = Convert.ToInt32(parms[0].Value);
                            cidades.Add(new cwkGestao.Modelo.Cidade
                            {
                                ID = idCidade.Value
                                ,
                                Codigo = (int)parms[1].Value
                                ,
                                Nome = row["cidadeNome"].ToString()
                                ,
                                IBGE = String.Empty
                                ,
                                UF = uf.First()
                                ,
                                IDUF = (int)parms[3].Value
                            });
                        }
                        else
                        {
                            idCidade = null;
                            log.Add("A cidade (" + row["cidadeNome"] + ") do cliente da revenda (" + row["nome"] + ") não foi encontrada no cadastro.");
                            continue;
                        }
                    }

                    if (row["pessoaCodigo"] is DBNull)
                        idPessoa = null;
                    else
                    {
                        var pes = pessoas.Where(p => p.Codigo == Convert.ToInt32(row["pessoaCodigo"]));
                        if (pes.Count() > 0)
                        {
                            idPessoa = pes.First().ID;
                        }
                        else
                        {
                            idPessoa = null;
                            log.Add("A pessoa (" + row["pessoaNome"] + ") do cliente da revenda " + row["nome"] + " não foi encontrada no cadastro.");
                        }
                    }

                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["codigoTPS"],
                            idPessoa,
                            row["idClienteRevenda"],
                            row["nome"],
                            row["bPrincipal"] is DBNull ? false : Convert.ToBoolean(row["bPrincipal"]),
                            row["endereco"],
                            idCidade,
                            row["cep"],
                            row["cnpj"],
                            row["cpf"],
                            row["telefone"],
                            row["fax"],
                            row["celular"],
                            row["contato"],
                            row["http"],
                            row["email"],
                            row["observacao"],
                            row["tipoLicenca"] is DBNull ? null : (short?)Convert.ToInt16(row["tipoLicenca"]),
                            row["quantidade"],
                            Convert.ToBoolean(row["bPontoVirtual"]),
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"],
                            row["chave"],
                            row["cei"],
                            row["bdalterado"],
                            row["inscricao"],
                            row["bairro"],
                            row["tipo"],
                            row["numero"],
                            row["responsavel"],
                            row["identificacaoHD"],
                            "",
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "ClienteRevenda";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar clientes da revenda: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaServico(DataTable dtServico, SqlConnection conn)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("id", typeof(int)),
                    new DataColumn("codigo", typeof(int)),
                    new DataColumn("nome", typeof(string)),
                    new DataColumn("valor", typeof(float)),
                    new DataColumn("Descricao", typeof(string)),
                    new DataColumn("tipo", typeof(byte)),
                    new DataColumn("geraBD", typeof(bool)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string))
                });

                foreach (DataRow row in dtServico.Rows)
                {
                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["nome"],
                            row["valor"],
                            row["descricao"],
                            row["tipo"],
                            false,
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"]
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "Servico";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar Serviço: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaStatusOS(DataTable dtStatusOS, SqlConnection conn)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("Descricao", typeof(string)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string))
                });

                foreach (DataRow row in dtStatusOS.Rows)
                {
                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["descricao"],
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"]
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "StatusOrdemServico";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar Status Ordem Serviço: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaTipoAtendimento(DataTable dtTipoAtendimento, SqlConnection conn)
        {
            try
            {
                DataTable dtIncluir = new DataTable();
                dtIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("Descricao", typeof(string)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string))
                });

                foreach (DataRow row in dtTipoAtendimento.Rows)
                {
                    dtIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["descricao"],
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"]
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "TipoAtendimento";
                    bulk.WriteToServer(dtIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar tipo atendimento: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaVersao(DataTable dtVersao, SqlConnection conn)
        {
            try
            {
                DataTable versoesIncluir = new DataTable();
                versoesIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("IDSistema", typeof(int)),
                    new DataColumn("Numero", typeof(string)),
                    new DataColumn("DataLiberacao", typeof(DateTime)),
                    new DataColumn("Observacao", typeof(string)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string)),
                    new DataColumn("Caminhobancolimpo", typeof(string)),
                    new DataColumn("Tipobanco", typeof(short)),
                    new DataColumn("Servidor", typeof(string)),
                    new DataColumn("Banco", typeof(string)),
                    new DataColumn("Usuario", typeof(string)),
                    new DataColumn("Senha", typeof(string)),
                    new DataColumn("Parametros", typeof(string)),                    
                });

                foreach (DataRow row in dtVersao.Rows)
                {
                    versoesIncluir.Rows.Add
                    (new object[]
                        {
                            row["id"],
                            row["codigo"],
                            row["idSistema"],
                            row["numero"],
                            row["dataLiberacao"],
                            row["observacao"],
                            row["incdata"],
                            row["incusuario"],
                            row["altdata"],
                            row["altusuario"],
                            row["caminhobancolimpo"],
                            row["tipobanco"],
                            row["servidor"],
                            row["banco"],
                            row["usuario"],
                            row["senha"],
                            row["parametros"]
                        });
                }
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "Versao";
                    bulk.WriteToServer(versoesIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar versão: " + Environment.NewLine + ex.Message);
            }
        }

        private void ImportaSistema(DataTable dtSistema, SqlConnection conn)
        {
            try
            {
                DataTable sistemasIncluir = new DataTable();
                sistemasIncluir.Columns.AddRange
                (new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Codigo", typeof(int)),
                    new DataColumn("Nome", typeof(string)),
                    new DataColumn("Incdata", typeof(DateTime)),
                    new DataColumn("Incusuario", typeof(string)),
                    new DataColumn("Altdata", typeof(DateTime)),
                    new DataColumn("Altusuario", typeof(string))
                });

                foreach (DataRow row in dtSistema.Rows)
                {
                    sistemasIncluir.Rows.Add
                    (new object[]
                    {
                        row["id"],
                        row["codigo"],
                        row["nome"],
                        row["incdata"],
                        row["incusuario"],
                        row["altdata"],
                        row["altusuario"]
                    });
                }

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans);
                    bulk.DestinationTableName = "Sistema";
                    bulk.WriteToServer(sistemasIncluir);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Add("Erro ao importar sistema: " + Environment.NewLine + ex.Message);
            }
        }
    }
}
