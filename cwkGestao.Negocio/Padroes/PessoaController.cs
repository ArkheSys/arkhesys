using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class PessoaController : BaseController<Pessoa>
    {
        #region Singleton
        private static PessoaController instance;
        private static IRepositorioPessoa repositorioPessoa;

        private PessoaController()
        { }

        static PessoaController()
        {
            instance = new PessoaController();
            repositorioT = RepositorioFactory<Pessoa>.GetRepositorio();
            repositorioPessoa = (IRepositorioPessoa)repositorioT;
        }

        public static PessoaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Pessoa objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<cwkGestao.Modelo.ImpressaoEtiquetaPessoaView> GetImpressaoEtiquetaPessoa()
        {
            return repositorioPessoa.GetImpressaoEtiquetaPessoa();
        }

        public Pessoa GetClienteMercadoLivre(int id)
        {
            return repositorioPessoa.GetClienteMercadoLivre(id);
        }

        public IList<Pessoa> GetAllOperadoresCartao()
        {
            return repositorioPessoa.GetAllOperadoresCartao();
        }

        public IList<Pessoa> GetAllTransportadoras()
        {
            return repositorioPessoa.GetAllTransportadoras();
        }

        public System.Collections.Hashtable GetPessoasImportadas()
        {
            return repositorioPessoa.GetPessoasImportadas();
        }

        public IList<Pessoa> GetParaCadastroEngenheiro()
        {
            return repositorioPessoa.GetParaCadastroEngenheiro();
        }

        public IList<Pessoa> GetAllContabilistas()
        {
            return repositorioPessoa.GetAllContabilistas();
        }

        public static bool ValidarCNPJ(string pCNPJ, out string pErro)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;
            string digito;
            string tempCnpj;
            string cnpj;

            cnpj = pCNPJ;

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
            cnpj = cnpj.Trim();

            if (cnpj.Length != 14)
            {
                    if (cnpj.Length != 0)
                    {
                        pErro = "CNPJ Inválido.";
                        return false;
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
                    pErro = "";
                }
                else
                {
                    pErro = "CNPJ Inválido.";
                    return false;
                }
            }

            pErro = "";
            return true;
        }

        public static bool ValidarCPF(string pCPF, out string pErro)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            string cpf;
            int soma;
            int resto;

            cpf = pCPF;

            cpf = cpf.Replace(".", "").Replace("-", "").Replace("_", "");
            cpf = cpf.Trim();

            if (cpf.Length != 11)
            {
                    if (cpf.Length != 0)
                    {
                        pErro = "CPF Inválido.";
                        return false;
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
                    pErro = "";
                }
                else
                {
                    pErro = "CPF Inválido.";
                    return false;
                }
            }

            pErro = "";
            return true;
        }

        public cwkGestao.Modelo.Pessoa GetPessoa(int ID)
        {
            return repositorioPessoa.LoadObjectById(ID);
        }

        public IList<cwkGestao.Modelo.Pessoa> GetAllFornecedores()
        {
            return repositorioPessoa.GetAllFornecedores().Distinct().ToList();
        }

        public IList<cwkGestao.Modelo.Pessoa> GetAllVendedores()
        {
            return repositorioPessoa.GetAllVendedores().Distinct().ToList();
        }

        public IList<cwkGestao.Modelo.Pessoa> GetAllEntregadores()
        {
            return repositorioPessoa.GetAllEntregadores().Distinct().ToList();
        }

        public IList<cwkGestao.Modelo.Pessoa> GetAllFuncionarios()
        {
            return repositorioPessoa.GetAllFuncionarios().Distinct().ToList();
        }

        public IList<cwkGestao.Modelo.Pessoa> GetAllClientes()
        {
            return repositorioPessoa.GetAllClientes();
        }

        public Pessoa GetByCodigo(int codigo, string propriedadeTrue)
        {
            return repositorioPessoa.GetByCodigo(codigo, propriedadeTrue);
        }

        public Pessoa GetByNome(string value, string propriedadeTrue)
        {
            return repositorioPessoa.GetByNome(value, propriedadeTrue);
        }

        public Pessoa GetByFantasia(string fantasia)
        {
            return repositorioPessoa.GetByFantasia(fantasia);
        }

        public bool GetByCPFeCNPJ(string value, int? IDSelecionado)
        {
            return repositorioPessoa.GetByCPFeCNPJ(value, IDSelecionado);
        }

        public IList<Pessoa> GetAtivosByCPFeCNPJ(string value, bool bAtivo)
        {
            return repositorioPessoa.GetAtivosByCPFeCNPJ(value, bAtivo);
        }

        public IList<Pessoa> GetClienteTel_Clientes()
        {
            return repositorioPessoa.GetClienteTel_Clientes();
        }

        public int MaxCodigoIntegracao()
        {
            return repositorioPessoa.MaxCodigoIntegracao();
        }

        public Pessoa GetByIdIntegracao(string idIntegracao)
        {
            return repositorioPessoa.GetByIdIntegracao(idIntegracao);
        }

        public Pessoa FindByTextField(string text)
        {
            return repositorioPessoa.FindByTextField(text);
        }
        public Pessoa FindByIDSISeCommerce(int id)
        {
            return repositorioPessoa.FindByIDSISeCommerce(id);
        }

        public IList<Pessoa> GetAllPorDiaPadraoLeitura(int dtInicio, int dtFim)
        {
            return repositorioPessoa.GetAllPorDiaPadraoLeitura(dtInicio, dtFim);
        }

        #region validar cpf e cnpj
        public static string ValidaCpf(object conteudo)
        {
            if (String.IsNullOrEmpty("" + conteudo))
                return "Campo obrigatório";
            string retorno = "";
            ValidarCPF(conteudo.ToString(), out retorno);
            return retorno;
        }

        public static string ValidaCnpj(object conteudo)
        {
            if (String.IsNullOrEmpty("" + conteudo))
                return "Campo obrigatório";
            string retorno = "";
            ValidarCNPJ(conteudo.ToString(), out retorno);
            return retorno;
        }

        #endregion

        public IList<Pessoa> getEntregadores()
        {
            return repositorioPessoa.GetEntregadores();
        }
    }
}
