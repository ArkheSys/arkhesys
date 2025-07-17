using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ClienteRevenda : ModeloBase
    {
        public ClienteRevenda()
        {
            Filiais = new List<ClienteRevenda>();

        }
        public ClienteRevenda(int ID, int Codigo, String Nome, string Cnpj, string Telefone, string Email)
        {
            this.ID = ID;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.Cnpj = Cnpj;
            this.Telefone = Telefone;
            this.Email = Email;
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private int? _IdFirebird;
        public virtual int? IdFirebird { get { return _IdFirebird; } set { _IdFirebird = value; } }

        #region Pessoa
        private int? _IDPessoa;
        public virtual int? IDPessoa
        {
            get { return _IDPessoa; }
            set 
            {
                if (value.HasValue)
                    _pessoa = new Pessoa() { ID = value.Value };
                else
                    _pessoa = null;
                
                _IDPessoa = value; }
        }

        private Pessoa _pessoa;
        public virtual Pessoa Pessoa
        { get { return _pessoa; }

            set
            {
                if (value != null)
                    _IDPessoa = value.ID;
                else
                    _IDPessoa = null;

                _pessoa = value;
            }
        }
        #endregion

        #region ClienteRevenda
        private int? _IDClienteRevenda;
        public virtual int? IDClienteRevenda
        {
            get { return _IDClienteRevenda; }
            set
            {
                if (value.HasValue)
                    _Matriz = new ClienteRevenda() { ID = value.Value };
                else
                    _Matriz = null;

                _IDClienteRevenda = value;
            }
        }
        private ClienteRevenda _Matriz;

        public virtual ClienteRevenda Matriz
        {
            get { return _Matriz; }

            set
            {
                if (value != null)
                    _IDClienteRevenda = value.ID;
                else
                    _IDClienteRevenda = null;

                _Matriz = value;
            }
        }
        #endregion


        private string _Nome;
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private bool? _Principal;
        public virtual bool? Principal
        {
            get { return _Principal; }
            set { _Principal = value; }
        }

        private string _Endereco;
        public virtual string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
        }


        #region Cidade
        private int _IDCidade;
        public virtual int IDCidade
        {
            get { return _IDCidade; }
            set
            {
                if (value != 0)
                    _cidade = new Cidade() { ID = value };
                else
                    _cidade = null;

                _IDCidade = value;
            }
        }
        private Cidade _cidade;
        public virtual Cidade Cidade
        {
            get { return _cidade; }
            set
            {
                if (value != null)
                    _IDCidade = value.ID;
                else
                    _IDCidade = 0;

                _cidade = value;
            }
        }

        #endregion

        private string _Cep;
        public virtual string Cep
        {
            get { return _Cep; }
            set { _Cep = value; }
        }


        private string _Cnpj;
        public virtual string Cnpj
        {
            get { return _Cnpj; }
            set { _Cnpj = value; }
        }

        private string _Cpf;
        public virtual string Cpf
        {
            get { return _Cpf; }
            set { _Cpf = value; }
        }

        private string _Telefone;
        public virtual string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }

        private string _Fax;
        public virtual string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        private string _Celular;
        public virtual string Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

        private string _Contato;
        public virtual string Contato
        {
            get { return _Contato; }
            set { _Contato = value; }
        }

        private string _Http;
        public virtual string Http
        {
            get { return _Http; }
            set { _Http = value; }
        }

        private string _Email;
        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Observacao;
        public virtual string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }

        private short? _TipoLicenca;
        public virtual short? TipoLicenca
        {
            get { return _TipoLicenca; }
            set
            {
                _TipoLicenca = value;
                if (Filiais != null)
                {
                    foreach (ClienteRevenda item in Filiais)
                    {
                        item.TipoLicenca = value;
                    }
                }
            }
        }

        private int? _Quantidade;
        public virtual int? Quantidade
        {
            get { return _Quantidade; }
            set { _Quantidade = value; }
        }

        private bool? _PontoVirtual;
        public virtual bool? PontoVirtual
        {
            get { return _PontoVirtual; }
            set { _PontoVirtual = value; }
        }

        private string _Chave;
        public virtual string Chave
        {
            get { return _Chave; }
            set { _Chave = value; }
        }

        private string _Cei;
        public virtual string Cei
        {
            get { return _Cei; }
            set { _Cei = value; }
        }

        private bool? _BdAlterado;
        public virtual bool? BdAlterado
        {
            get { return _BdAlterado; }
            set { _BdAlterado = value; }
        }

        private string _Inscricao;
        public virtual string Inscricao
        {
            get { return _Inscricao; }
            set { _Inscricao = value; }
        }

        private string _Bairro;
        public virtual string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }

        private bool? _Tipo;
        public virtual bool? Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        private string _Numero;
        public virtual string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        private string _Responsavel;
        public virtual string Responsavel
        {
            get { return _Responsavel; }
            set { _Responsavel = value; }
        }

        private string _IdentificacaoHD;
        public virtual string IdentificacaoHD
        {
            get { return _IdentificacaoHD; }
            set { _IdentificacaoHD = value; }
        }

        private string _Complemento;
        public virtual string Complemento { get { return _Complemento; } set { _Complemento = value; } }

        private DateTime? _Incdata;
        public virtual DateTime? Incdata { get { return _Incdata; } set { _Incdata = value; } }

        private string _Incusuario;
        public virtual string Incusuario
        {
            get { return _Incusuario; }
            set { _Incusuario = value; }
        }

        private DateTime? _Altdata;
        public virtual DateTime? Altdata
        {
            get { return _Altdata; }
            set { _Altdata = value; }
        }

        private string _Altusuario;
        public virtual string Altusuario
        {
            get { return _Altusuario; }
            set { _Altusuario = value; }
        }


        private IList<ClienteRevenda> _ClientesRevenda;
        public virtual IList<ClienteRevenda> Filiais
        {
            get { return _ClientesRevenda; }
            set
            {
                _ClientesRevenda = value;
                foreach (ClienteRevenda item in _ClientesRevenda)
                {
                    item.IDClienteRevenda = this.ID;
                    item.Matriz = this;
                }
            }
        }

        //private Cidade _Cidade;
        //public Cidade Cidade
        //{
        //    get { return _Cidade; }
        //    set
        //    {
        //        _Cidade = value;
        //        if (value != null)
        //            this.IDCidade = value.ID;
        //    }
        //}

    }
}
