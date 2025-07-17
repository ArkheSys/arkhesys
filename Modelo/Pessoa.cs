using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Modelo
{
    public partial class Pessoa : Modelo.IMetodosCwork
    {
        public Modelo.Endereco GetEnderecoPrincipal()
        {
            var end = Enderecos.OrderByDescending(e => e.bPrincipal);
            if (end.Count() > 0)
                return end.First();
            return new Endereco();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.Pessoas
                    //join end in db.Enderecos on p.ID equals end.IDPessoa
                    //where end.bPrincipal.Value == 1
                    join endereco in db.Enderecos
                    on p equals endereco.Pessoa into pes_join_end
                    from endereco in pes_join_end.DefaultIfEmpty()
                    where Convert.ToBoolean(endereco.bPrincipal.HasValue ? endereco.bPrincipal.Value : 0)
                    join telefone in db.Telefones
                    on p equals telefone.Pessoa into pes_join_tel
                    from telefone in pes_join_tel.DefaultIfEmpty()
                    where Convert.ToBoolean(telefone.bPrincipal.HasValue ? telefone.bPrincipal.Value : 0)
                    //join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    //from xe in end.DefaultIfEmpty()
                    //where xe.bPrincipal == 1
                    //join tel in db.Telefones on p.ID equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = endereco.Cidade.Nome,
                        Estado = endereco.Cidade.UF.Sigla,
                        Telefone = telefone.Numero,
                        Classificação = p.Classificacao.Descricao,
                        Cli = (p.bCliente == 1 ? "Sim" : "Não"),
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;

            lista = from p in db.Pessoas
                    join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    from endereco in end.DefaultIfEmpty()
                    where endereco.bPrincipal == 1
                    join telefone in db.Telefones
                    on p.ID equals telefone.IDPessoa into tel
                    from telefone in tel.DefaultIfEmpty()
                    where telefone.bPrincipal == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = endereco == null ? "" : endereco.Cidade.Nome,
                        Estado = endereco == null ? "" : endereco.Cidade.UF.Sigla,
                        Telefone = telefone == null ? "" : telefone.Numero,
                        Classificação = p.Classificacao.Descricao,
                        bCliente = (p.bCliente == 1 ? "Sim" : "Não"),
                        bFuncionario = (p.bFuncionario == 1 ? "Sim" : "Não"),
                        bVendedor = (p.bVendedor == 1 ? "Sim" : "Não"),
                        bFornecedor = (p.bFornecedor == 1 ? "Sim" : "Não"),
                        ID = p.ID
                    };
            return lista;
        }

        public IList<Objeto.pxyPessoa> GetAll(DataClassesDataContext db)
        {
            string querry = @"select 
                            Pessoa.Codigo,
                            Pessoa.Nome,
                            Pessoa.Fantasia,
                            Pessoa.CNPJ_CPF,
                            Cidade.Nome as Cidade,
                            UF.Sigla as Estado,
                            PessoaTelefone.Numero as Telefone,
                            Classificacao.Descricao as Classificacao,
                            case when Pessoa.bCliente = 1 then 'Sim' else 'Não' end as bCliente,
                            case when Pessoa.bFuncionario = 1 then 'Sim' else 'Não' end as bFuncionario,
                            case when Pessoa.bVendedor = 1 then 'Sim' else 'Não' end as bVendedor,
                            case when Pessoa.bFornecedor = 1 then 'Sim' else 'Não' end as bFornecedor,
                            Pessoa.ID
                            from Pessoa
                            left join Classificacao on Classificacao.ID = Pessoa.IDClassificacao
                            left join PessoaEndereco on PessoaEndereco.IDPessoa = Pessoa.ID and PessoaEndereco.bPrincipal = 1
                            left join Cidade on Cidade.ID = PessoaEndereco.IDCidade
                            left join UF on UF.ID = Cidade.IDUF
                            left join PessoaTelefone on PessoaTelefone.IDPessoa = Pessoa.ID and PessoaTelefone.bPrincipal = 1";
            return db.ExecuteQuery<Objeto.pxyPessoa>(querry, new object[] { }).ToList();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IList getListaCombo(DataClassesDataContext db)
        {
            return (from p in db.Pessoas
                    select new
                    {
                        Nome = p.Nome + " | " + p.Codigo,
                        ID = p.ID
                    }).ToList();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.Pessoas
                    select new
                    {
                        Nome = p.Nome + " | " + p.Codigo,
                        ID = p.ID
                    };
            return lista;
        }

        /* public IQueryable getListaEnderecos(DataClassesDataContext db, int pID)
         {
             IQueryable lista;
             lista = from e in db.Enderecos
                     where e.IDPessoa == pID
                     select new
                     {
                         Rua = e.Rua,
                         Bairro = e.Bairro,
                         CEP = e.CEP,
                         Cidade = e.Cidade.Nome + " | " + e.Cidade.UF.Nome,
                         IDPessoa = e.IDPessoa,
                         Sequencia = e.Sequencia,
                         ID = e.ID
                     };
             return lista;
         }*/

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.Pessoas where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.Pessoas where u.Codigo.ToString() == pValor select u).Single().ID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.Pessoas
                             where p.Codigo == db.Pessoas.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public bool Valida(ErrorProvider erro, Control.ControlCollection controles, bool pStatus, bool pesq, bool pcheck)
        {
            bool status = pStatus;
            Control a = new Control();
            bool b = pcheck;
            if (pesq)
            {
                a = (from c in controles.Cast<Control>()
                     where c.Name == "cbTipo"
                     select c).Single();

                b = ((DevExpress.XtraEditors.CheckEdit)(from c in controles.Cast<Control>()
                                                        where ((Control)c).Name == "chbCliente"
                                                        select c).Single()).Checked;
            }


            foreach (Control ctr in controles)
            {
                if ((ctr is GroupBox) || (ctr is TabControl) || (ctr is DevExpress.XtraTab.XtraTabControl) || (ctr is DevExpress.XtraTab.XtraTabPage))
                {
                    status = Valida(erro, ctr.Controls, status, false, b);
                }
                else
                {
                    switch (ctr.Name)
                    {
                        case "txtCodigo":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "0")
                            {
                                erro.SetError(ctr, "Valor tem que ser diferente de 0 (Zero).");
                                status = false;
                            }
                            break;
                        case "cbTipo":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            else
                            {
                                if (!((DevExpress.XtraEditors.ComboBoxEdit)ctr).Properties.Items.Contains(ctr.Text))
                                {
                                    erro.SetError(ctr, "Valor incorreto.");
                                    status = false;
                                }
                            }
                            break;
                        case "txtNome":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            if (ctr.Text.Length > 60)
                            {
                                erro.SetError(ctr, "Este campo aceita no máximo 60 caracteres devido às limitações da NFe");
                                status = false;
                            }
                            break;
                        case "txtDtCad":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Preencha a data de cadastro.");
                                status = false;
                            }
                            break;
                        case "cbFilial":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "cbCidade":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "txtCPF":
                            erro.SetError(ctr, "");
                            if (ctr.Enabled)
                            {
                                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                                string tempCpf;
                                string digito;
                                string cpf;
                                int soma;
                                int resto;

                                cpf = ctr.Text;

                                cpf = cpf.Replace(".", "").Replace("-", "").Replace("_", "");
                                cpf = cpf.Trim();

                                if (cpf.Length != 11)
                                {
                                    if (cpf.Length == 0)
                                    {
                                        erro.SetError(ctr, "CPF não pode ficar sem valor.");
                                        status = false;
                                    }
                                    else
                                    {
                                        if (cpf.Length != 0)
                                        {
                                            erro.SetError(ctr, "CPF Inválido.");
                                            status = false;
                                        }
                                    }
                                }
                                else
                                {
                                    tempCpf = cpf.Substring(0, 9);
                                    soma = 0;

                                    for (int i = 0; i < 9; i++)
                                    {
                                        soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                                    }

                                    resto = soma % 11;
                                    if (resto < 2)
                                    {
                                        resto = 0;
                                    }
                                    else
                                    {
                                        resto = 11 - resto;
                                    }

                                    digito = resto.ToString();
                                    tempCpf = tempCpf + digito;
                                    soma = 0;

                                    for (int i = 0; i < 10; i++)
                                    {
                                        soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                                    }
                                    resto = soma % 11;

                                    if (resto < 2)
                                    {
                                        resto = 0;
                                    }
                                    else
                                    {
                                        resto = 11 - resto;
                                    }
                                    digito = digito + resto.ToString();

                                    if (cpf.EndsWith(digito))
                                    {
                                        erro.SetError(ctr, "");
                                    }
                                    else
                                    {
                                        erro.SetError(ctr, "CPF Inválido.");
                                        status = false;
                                    }
                                }
                            }
                            break;
                        case "txtCNPJ":
                            erro.SetError(ctr, "");
                            if (ctr.Enabled)
                            {
                                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                                int soma;
                                int resto;
                                string digito;
                                string tempCnpj;
                                string cnpj;

                                cnpj = ctr.Text;

                                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
                                cnpj = cnpj.Trim();

                                if (cnpj.Length != 14)
                                {
                                    if (cnpj.Length == 0)
                                    {
                                        erro.SetError(ctr, "CNPJ não pode ficar sem valor.");
                                        status = false;
                                    }
                                    else
                                    {
                                        if (cnpj.Length != 0)
                                        {
                                            erro.SetError(ctr, "CNPJ Inválido.");
                                            status = false;
                                        }
                                    }
                                }
                                else
                                {
                                    tempCnpj = cnpj.Substring(0, 12);
                                    soma = 0;
                                    for (int i = 0; i < 12; i++)
                                    {
                                        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                                    }

                                    resto = (soma % 11);
                                    if (resto < 2)
                                    {
                                        resto = 0;
                                    }
                                    else
                                    {
                                        resto = 11 - resto;
                                    }

                                    digito = resto.ToString();
                                    tempCnpj = tempCnpj + digito;
                                    soma = 0;
                                    for (int i = 0; i < 13; i++)
                                    {
                                        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                                    }

                                    resto = (soma % 11);
                                    if (resto < 2)
                                    {
                                        resto = 0;
                                    }
                                    else
                                    {
                                        resto = 11 - resto;
                                    }

                                    digito = digito + resto.ToString();

                                    if (cnpj.EndsWith(digito))
                                    {
                                        erro.SetError(ctr, "");
                                    }
                                    else
                                    {
                                        erro.SetError(ctr, "CNPJ Inválido.");
                                        status = false;
                                    }
                                }
                            }
                            break;
                        case "txtFantasia":
                        case "txtEnd":
                        case "txtComplemento":
                            if (ctr.Text.Length > 60)
                            {
                                erro.SetError(ctr, "Este campo aceita no máximo 60 caracteres devido às limitações da NFe");
                                status = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return status;

        }

        public StringBuilder GetAvisos(DataClassesDataContext db)
        {
            StringBuilder avisos = new StringBuilder();

            if (this.PessoaAvisos.Where(pa => pa.DtValidade >= System.DateTime.Today).Count() > 0)
            {
                foreach (Modelo.PessoaAviso pa in this.PessoaAvisos.Where(pa => pa.DtValidade >= System.DateTime.Today))
                {
                    avisos.AppendLine(pa.Aviso);
                    avisos.AppendLine("----------");
                }
            }

            return avisos;
        }

        /// <summary>
        /// Método responsável para buscar as pessoas por Codigo, Nome, Fantasia, CNPJ, Telefone
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getBuscaPessoa(DataClassesDataContext db, string pValor)
        {
            IQueryable lista;

            lista = from p in db.Pessoas
                    join end in db.Enderecos on p.ID equals end.IDPessoa
                    where end.bPrincipal.Value == 1
                    join tel in db.Telefones on p.ID equals tel.IDPessoa
                    where tel.bPrincipal.Value == 1
                    where p.Codigo.ToString() == pValor
                    || p.Nome.Contains(pValor)
                    || p.Fantasia.Contains(pValor)
                    || p.CNPJ_CPF == pValor
                    || tel.Numero == pValor
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        Fantasia = p.Fantasia,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = end.Cidade.Nome,
                        Estado = end.Cidade.UF.Sigla,
                        Telefone = tel.Numero,
                        Classificação = p.Classificacao.Descricao,
                        bCliente = (p.bCliente == 1 ? "Sim" : "Não"),
                        bFuncionario = (p.bFuncionario == 1 ? "Sim" : "Não"),
                        bVendedor = (p.bVendedor == 1 ? "Sim" : "Não"),
                        bFornecedor = (p.bFornecedor == 1 ? "Sim" : "Não"),
                        ID = p.ID
                    };

            return lista;
        }

        /// <summary>
        /// Método responsável para buscar as pessoas por Codigo, Nome, Fantasia, CNPJ, Telefone
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public int getQtdPessoa(DataClassesDataContext db, string pValor, out int pID)
        {
            List<Modelo.Pessoa> lista = new List<Modelo.Pessoa>();

            lista = (from p in db.Pessoas
                     join tel in db.Telefones on p.ID equals tel.IDPessoa
                     where tel.bPrincipal.Value == 1
                     where p.Codigo.ToString() == pValor
                     || p.Nome.Contains(pValor)
                     || p.Fantasia.Contains(pValor)
                     || p.CNPJ_CPF == pValor
                     || tel.Numero == pValor
                     select p).ToList();

            if (lista.Count() == 1)
            {
                pID = lista[0].ID;
            }
            else
            {
                pID = 0;
            }

            return lista.Count();
        }

        public int QtdPessoaDemonstracao(DataClassesDataContext db)
        {
            int qtd = db.Pessoas.Count();

            return qtd;
        }

        public int RetornaAtivo(DataClassesDataContext db, int ID)
        {
            byte? ativo;
            ativo = (from p in db.Pessoas
                     where p.ID == ID
                     select p.bAtivo).Single();
            return Convert.ToInt32(ativo);
        }

        public IQueryable getListaClientesFornecedores(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.Pessoas
                    where p.bFornecedor == 1 || p.bCliente == 1
                    join e in db.Enderecos on p.ID equals e.IDPessoa into end
                    from xe in end.DefaultIfEmpty()
                    where xe.bPrincipal == 1
                    //join tel in db.Telefones on p.ID equals tel.IDPessoa
                    //where tel.bPrincipal.Value == 1
                    select new
                    {
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        CNPJ_CPF = p.CNPJ_CPF,
                        Cidade = xe.Cidade.Nome + " | " + xe.Cidade.UF.Sigla,
                        Telefone = xe.Numero,
                        ID = p.ID
                    };
            return lista;
        }

        public string PrimeiroEmailVendas()
        {
            try
            {
                return this.PessoaEmails.Where(i => i.bVenda == 1).First().Email;
            }
            catch
            {
                return "";
            }
        }

        public string PrimeiroEmailAdministrativo()
        {
            try
            {
                return this.PessoaEmails.Where(i => i.bAdministrativo == 1).First().Email;
            }
            catch
            {
                return "";
            }
        }

        public string PrimeiroEmailNFe()
        {
            try
            {
                return this.PessoaEmails.Where(i => i.BNFe).First().Email;
            }
            catch
            {
                return "";
            }
        }
    }
}
