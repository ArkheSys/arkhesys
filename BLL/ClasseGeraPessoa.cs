using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace BLL
{
    public class ClasseGeraPessoa
    {
        private Modelo.DataClassesDataContext db;
        BLL.Pessoa BLLPessoa;


        public ClasseGeraPessoa()
        {
            db = new Modelo.DataClassesDataContext();
            BLLPessoa = new BLL.Pessoa(db);
        }

        public bool incluirPessoas(int qtd)
        {
            Modelo.Cliente objPessoaCliente;
            bool ret = true;
            int cod = 9;
            try
            {
                for (int i = 0; i < qtd; i++)
                {
                    Modelo.Pessoa objPessoa = new Modelo.Pessoa();

                    db.Pessoas.InsertOnSubmit(objPessoa);

                    objPessoa.Codigo = cod;
                    cod++;
                    objPessoa.Nome = RandomString(2, false) + "-" + cod.ToString();
                    objPessoa.bAtivo = 1;
                    objPessoa.bFornecedor = (byte)RandomNumber(0, 1);
                    objPessoa.DtCadastro = DateTime.Now;

                    objPessoa.Classificacao = db.Classificacaos.First<Modelo.Classificacao>();
                    objPessoa.Filial = db.Filials.First<Modelo.Filial>();
                    objPessoa.Http = RandomString(20, false);
                    objPessoa.Email1 = RandomString(20, false);
                    objPessoa.Email2 = RandomString(20, false);
                    objPessoa.Observacao = RandomString(50, false);

                    objPessoa.bClienteRevenda = 0;
                    objPessoa.bFuncionario = 0;
                    objPessoa.bFuncionarioCliente = 0;
                    objPessoa.bRevenda = 0;
                    objPessoa.bVendedor = 0;

                    if (i < (qtd / 2))
                    {
                        objPessoa.TipoPessoa = "Física";
                        objPessoa.DtNascimento = DateTime.Now;
                        objPessoa.Apelido = RandomString(50, false);
                        objPessoa.RG = RandomNumber(8, 10).ToString();
                        objPessoa.EstadoCivil = "Casado(a)";
                        objPessoa.CNPJ_CPF = "931.517.684-74";
                        objPessoa.bOrgaoPublico = 0;
                        objPessoa.Sexo = "Feminino";
                    }
                    else
                    {
                        objPessoa.TipoPessoa = "Jurídica";
                        objPessoa.bOrgaoPublico = (byte)RandomNumber(0, 1);
                        objPessoa.Fantasia = RandomString(50, false);
                        objPessoa.Inscricao = RandomNumber(0, 20).ToString();
                        objPessoa.CNPJ_CPF = "45.341.155/0001-20";                        
                        objPessoa.DtFundacao = DateTime.Now;
                    }

                    Modelo.Telefone objTelefone;
                    Modelo.Endereco objEndereco;

                    for (int j = 0; j < 3; j++)
                    {
                        objTelefone = new Modelo.Telefone();
                        objEndereco = new Modelo.Endereco();

                        objTelefone.Numero = RandomNumber(11000000, 99999999).ToString();
                        objTelefone.Sequencia = objTelefone.RetornaMAXSequencia(objPessoa);
                        objTelefone.Contato = objPessoa.Nome;
                        objTelefone.Tipo = "Residencial";
                        if (j == 0)
                        {
                            objTelefone.bPrincipal = 1;
                            objEndereco.bPrincipal = 1;
                        }
                        else
                        {
                            objTelefone.bPrincipal = 0;
                            objEndereco.bPrincipal = 0;
                        }

                        objEndereco.Rua = RandomString(100, false);
                        objEndereco.Bairro = RandomString(50, false);
                        objEndereco.bAtivo = 1;
                        objEndereco.bCobranca = 0;
                        objEndereco.bEntrega = 0;
                        objEndereco.CEP = RandomNumber(8, 8).ToString();
                        objEndereco.Cidade = db.Cidades.First<Modelo.Cidade>();
                        objEndereco.Contato = objPessoa.Nome;
                        objEndereco.Telefone = objTelefone.Numero;
                        objEndereco.Numero = RandomNumber(3, 6).ToString();
                        objEndereco.Sequencia = objEndereco.RetornaMAXSequencia(objPessoa);

                        objPessoa.Telefones.Add(objTelefone);
                        objPessoa.Enderecos.Add(objEndereco);
                    }

                    if (i % 2 == 0)
                    {
                        objPessoaCliente = new Modelo.Cliente();
                        objPessoa.Clientes.Add(objPessoaCliente);
                        objPessoa.bCliente = 1;

                        objPessoaCliente.Condicao = db.Condicaos.First<Modelo.Condicao>();
                        objPessoaCliente.TabelaPreco = db.TabelaPrecos.First<Modelo.TabelaPreco>();
                        objPessoaCliente.Vendedor = db.Pessoas.First();
                        objPessoaCliente.NomeConjuge = RandomString(80, false);
                        objPessoaCliente.NomeMae = RandomString(80, false);
                        objPessoaCliente.NomePai = RandomString(80, false);

                        objPessoaCliente.LimiteCredito = RandomNumber(0, 2000);

                        objPessoaCliente.bBloqueiaCondicao = 0;

                        objPessoaCliente.bBloqueiaTabelaPreco = 0;

                        objPessoaCliente.bContribuinte = 0;
                    }
                    else
                    {
                        objPessoa.bCliente = 0;
                    }
                    db.SubmitChanges();
                }
               
            }
            catch (Exception ex)
            {
                ret = false;
                throw new Exception(ex.Message);
            }

            return ret;
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, false));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
