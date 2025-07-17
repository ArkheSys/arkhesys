using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OrdemServico : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Resumo;
        public virtual string Resumo { get { return _Resumo; } set { _Resumo = value; } }

        #region Cliente
        private int? _IDCliente;
        public virtual int? IDCliente
        {
            get { return _IDCliente; }
            set
            {
                if (value.HasValue)
                    _Cliente = new Pessoa() { ID = value.Value };
                else
                    _Cliente = null;
                _IDCliente = value;
            }
        }

        private Pessoa _Cliente;
        public virtual Pessoa Cliente
        {
            get { return _Cliente; }
            set
            {
                if (value != null)
                    _IDCliente = value.ID;
                else
                    _IDCliente = null;

                _Cliente = value;
            }
        }
        #endregion

        private string _Contato;
        public virtual string Contato { get { return _Contato; } set { _Contato = value; } }

        #region TipoAtendimento
        private int? _IDTipoAtendimento;
        public virtual int? IDTipoAtendimento
        {
            get { return _IDTipoAtendimento; }
            set
            {
                if (value.HasValue)
                    _tipoAtendimento = new TipoAtendimento() { ID = value.Value };
                else
                    _tipoAtendimento = null;

                _IDTipoAtendimento = value;
            }
        }

        private TipoAtendimento _tipoAtendimento;
        public virtual TipoAtendimento TipoAtendimento
        {
            get { return _tipoAtendimento; }
            set
            {
                if (value != null)
                    _IDTipoAtendimento = value.ID;
                else
                    _IDTipoAtendimento = null;

                _tipoAtendimento = value;
            }
        }
        #endregion

        #region Sistema
        private int? _IDSistema;
        public virtual int? IDSistema
        {
            get { return _IDSistema; }
            set
            {
                if (value.HasValue)
                    _sistema = new Sistema() { ID = value.Value };
                else
                    _sistema = null;

                _IDSistema = value;
            }
        }

        private Sistema _sistema;
        public virtual Sistema Sistema
        {
            get { return _sistema; }
            set
            {
                if (value != null)
                    _IDSistema = value.ID;
                else
                    _IDSistema = null;

                _sistema = value;
            }
        }
        #endregion

        #region Versao
        private int? _IDVersao;
        public virtual int? IDVersao
        {
            get { return _IDVersao; }
            set
            {
                if (value.HasValue)
                    _versao = new Versao() { ID = value.Value };
                else
                    _versao = null;

                _IDVersao = value;
            }
        }
        private Versao _versao;
        public virtual Versao Versao
        {
            get { return _versao; }
            set
            {
                if (value != null)
                    _IDVersao = value.ID;
                else
                    _IDVersao = null;

                _versao = value;
            }
        }
        #endregion

        #region Status
        private int? _IDStatus;
        public virtual int? IDStatus
        {
            get { return _IDStatus; }
            set
            {
                if (value.HasValue)
                    _Status = new StatusOrdemServico() { ID = value.Value };
                else
                    _Status = null;

                _IDStatus = value;
            }
        }

        private StatusOrdemServico _Status;
        public virtual StatusOrdemServico Status
        {
            get { return _Status; }
            set
            {
                if (value != null)
                    _IDStatus = value.ID;
                else
                    _IDVersaoFechamento = null;

                _Status = value;
            }
        }
        #endregion

        #region VersaoFechamento
        private int? _IDVersaoFechamento;
        public virtual int? IDVersaoFechamento
        {
            get { return _IDVersaoFechamento; }
            set
            {
                if (value.HasValue)
                    _VersaoFechamento = new Versao() { ID = value.Value };
                else
                    _VersaoFechamento = null;

                _IDVersaoFechamento = value;
            }
        }

        private Versao _VersaoFechamento;
        public virtual Versao VersaoFechamento
        {
            get { return _VersaoFechamento; }
            set
            {
                if (value != null)
                    _IDVersaoFechamento = value.ID;
                else
                    _IDVersaoFechamento = null;
                _VersaoFechamento = value;

            }
        }
        #endregion

        private string _Descricao;
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        private DateTime? _DataPrevisao;
        public virtual DateTime? DataPrevisao { get { return _DataPrevisao; } set { _DataPrevisao = value; } }

        private DateTime? _DataPrevisaoFinal;
        public virtual DateTime? DataPrevisaoFinal { get { return _DataPrevisaoFinal; } set { _DataPrevisaoFinal = value; } }

        private bool? _Lancado;
        public virtual bool? Lancado { get { return _Lancado; } set { _Lancado = value; } }

        private DateTime? _Incdata;
        public virtual DateTime? Incdata { get { return _Incdata; } set { _Incdata = value; } }

        private string _Incusuario;
        public virtual string Incusuario { get { return _Incusuario; } set { _Incusuario = value; } }

        private DateTime? _Altdata;
        public virtual DateTime? Altdata { get { return _Altdata; } set { _Altdata = value; } }

        private string _Altusuario;
        public virtual string Altusuario { get { return _Altusuario; } set { _Altusuario = value; } }

        private string _Observacao;
        public virtual string Observacao { get { return Observacao; } set { _Observacao = value; } }


        //public virtual string 

        #region Responsavel
        private int? _IDResponsavel;
        public virtual int? IDResponsavel
        {
            get
            { return _IDResponsavel; }
            set
            {
                if (value.HasValue)
                {
                    _responsavel = new Cw_usuario() { ID = value.Value };
                }
                else
                    _responsavel = null;
                _IDResponsavel = value;

            }
        }

        private Cw_usuario _responsavel;
        public virtual Cw_usuario Responsavel
        {
            get { return _responsavel; }
            set
            {
                if (value != null)
                    _IDResponsavel = value.ID;
                else
                    _IDResponsavel = null;

                _responsavel = value;

            }
        }
        #endregion

        #region Cliente Revenda
        private int? _IDClienteRevenda;
        public virtual int? IDClienteRevenda
        {
            get { return _IDClienteRevenda; }
            set
            {
                if (value.HasValue)
                    _ClienteRevenda = new ClienteRevenda() { ID = value.Value };
                else
                    _ClienteRevenda = null;

                _IDClienteRevenda = value;
            }
        }

        private ClienteRevenda _ClienteRevenda;
        public virtual ClienteRevenda ClienteRevenda
        {
            get { return _ClienteRevenda; }
            set
            {
                if (value != null)
                    _IDClienteRevenda = value.ID;
                else
                    _IDClienteRevenda = null;

                _ClienteRevenda = value;
            }
        }
        #endregion

#pragma warning disable CS0169 // O campo "OrdemServico._ServicoOS" nunca é usado
        private IList<ServicoOS> _ServicoOS;
#pragma warning restore CS0169 // O campo "OrdemServico._ServicoOS" nunca é usado
        public virtual IList<ServicoOS> ServicoOS
        {
            get;
            //{ return _ServicoOS; }
            set;
            //{
            //    _ServicoOS = value;
            //    foreach (ServicoOS item in _ServicoOS)
            //    {
            //        item.IDOrdemServico = this.ID;
            //        item.OrdemServico = this;
            //    }
            //}
        }

#pragma warning disable CS0169 // O campo "OrdemServico._AcompanhamentoOS" nunca é usado
        private IList<AcompanhamentoOS> _AcompanhamentoOS;
#pragma warning restore CS0169 // O campo "OrdemServico._AcompanhamentoOS" nunca é usado
        public virtual IList<AcompanhamentoOS> AcompanhamentoOS
        {
            get;
            //{ return _AcompanhamentoOS; }
            set;
            //{
            //    _AcompanhamentoOS = value;
            //    foreach (AcompanhamentoOS item in _AcompanhamentoOS)
            //    {
            //        item.IDOrdemServico = this.ID;
            //        item.OrdemServico = this;
            //    }
            //}
        }

        //Não é persistido - Somente para passar a informação da tela para o Controller
        public virtual DateTime DataStatus { get; set; }
        //public object Pessoa { get; set; }
    }
}
