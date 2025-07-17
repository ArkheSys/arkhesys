using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class ClienteRevendaController : BaseController<ClienteRevenda>
    {
        #region Singleton
        private static ClienteRevendaController instance;
        private static IRepositorioClienteRevenda repositorioClienteRevenda;

        private ClienteRevendaController()
        { }

        static ClienteRevendaController()
        {
            instance = new ClienteRevendaController();
            repositorioT = RepositorioFactory<ClienteRevenda>.GetRepositorio();
            repositorioClienteRevenda = (IRepositorioClienteRevenda)repositorioT;
        }

        public static ClienteRevendaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ClienteRevenda objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(objeto.Nome.Trim()))
                ret.Add("txtNome", "Preencha o nome corretamente.");
            if (objeto.IDCidade <= 0)
                ret.Add("cbCidade", "Selecione a cidade.");

            if (!String.IsNullOrEmpty(objeto.Cnpj.Trim()))
            {
                string erro = "";
                if (!PessoaController.ValidarCNPJ(objeto.Cnpj.Trim(), out erro))
                {
                    ret.Add("txtCnpj", erro);
                }
            }

            if (!String.IsNullOrEmpty(objeto.Cpf.Trim()))
            {
                string erro = "";
                if (!PessoaController.ValidarCPF(objeto.Cpf.Trim(), out erro))
                {
                    ret.Add("txtCpf", erro);
                }
            }
            return ret;
        }

        public Dictionary<string, string> Salvar(Modelo.ClienteRevenda clienteRevendaPai, Modelo.ClienteRevenda objClienteRevenda, Modelo.Acao acao)
        {
            Dictionary<string, string> ret;
            if (acao == Acao.Excluir)
            {
                objClienteRevenda.IDClienteRevenda = null;
                objClienteRevenda.Matriz = null;
                clienteRevendaPai.Filiais.Remove(objClienteRevenda);
                ret = new Dictionary<string, string>();
            }
            else
            {
                ret = ValidaObjeto(objClienteRevenda);
                if (ret.Count == 0 && acao == Acao.Incluir)
                {
                    objClienteRevenda.Matriz = clienteRevendaPai;
                    clienteRevendaPai.Filiais.Add(objClienteRevenda);
                }
            }
            return ret;
        }

        public IList<ClienteRevenda> GetAllByPessoa(int pIdPessoa)
        {
            return repositorioClienteRevenda.GetAllByPessoa(pIdPessoa);
        }

        public int MaxCodigo(IList<ClienteRevenda> lista)
        {
            return lista.Count + 1;
        }

        public IList<ClienteRevenda> GetAllByMatriz(int pIdMatriz)
        {
            return repositorioClienteRevenda.GetAllByMatriz(pIdMatriz);
        }
    }
}
