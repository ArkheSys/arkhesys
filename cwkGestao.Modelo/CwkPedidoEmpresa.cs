using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CwkPedidoEmpresa : ModeloBase
    {
        public CwkPedidoEmpresa()
        {
            ArquivoLicencaSerializado = "";
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _IDPedido;
        public virtual int IDPedido
        {
            get { return _IDPedido; }
            set
            {
                if (value != 0)
                    _CwkPedido = new CwkPedido() { ID = value };
                else
                    _CwkPedido = null;

                _IDPedido = value;
            }
        }

        private int _Seq;
        public virtual int Seq { get { return _Seq; } set { _Seq = value; } }

        private int _IDClienteRevenda;
        public virtual int IDClienteRevenda
        {
            get
            {
                return _IDClienteRevenda;
            }
            set
            {
                if (value != 0)
                    _ClienteRevenda = new ClienteRevenda() { ID = value };
                else
                    _ClienteRevenda = null;

                _IDClienteRevenda = value;
            }
        }

        private string _Observacao;
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private DateTime? _IncData;
#pragma warning disable CS0114 // "CwkPedidoEmpresa.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime? IncData { get { return _IncData; } set { _IncData = value; } }
#pragma warning restore CS0114 // "CwkPedidoEmpresa.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private DateTime? _IncHora;
        public virtual DateTime? IncHora { get { return _IncHora; } set { _IncHora = value; } }

        private string _IncUsuario;
#pragma warning disable CS0114 // "CwkPedidoEmpresa.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual string IncUsuario { get { return _IncUsuario; } set { _IncUsuario = value; } }
#pragma warning restore CS0114 // "CwkPedidoEmpresa.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private DateTime? _AltData;
#pragma warning disable CS0114 // "CwkPedidoEmpresa.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime? AltData { get { return _AltData; } set { _AltData = value; } }
#pragma warning restore CS0114 // "CwkPedidoEmpresa.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private DateTime? _AltHora;
        public virtual DateTime? AltHora { get { return _AltHora; } set { _AltHora = value; } }

        private string _AltUsuario;
#pragma warning disable CS0114 // "CwkPedidoEmpresa.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual string AltUsuario { get { return _AltUsuario; } set { _AltUsuario = value; } }
#pragma warning restore CS0114 // "CwkPedidoEmpresa.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private int? _IDServico;
        public virtual int? IDServico
        {
            get { return _IDServico; }
            set
            {
                if (value.HasValue)
                    _Servico = new Servico() { ID = value.Value };
                else
                    _Servico = null;

                _IDServico = value;
            }
        }

        private int? _IDProduto;
        public virtual int? IDProduto
        {
            get { return _IDProduto; }
            set
            {
                if (value.HasValue)
                    _Produto = new Produto() { ID = value.Value };
                else
                    _Produto = null;

                _IDProduto = value;
            }
        }

        private string _Senha;
        public virtual string Senha { get { return _Senha; } set { _Senha = value; } }

        private string _ContraSenha;
        public virtual string ContraSenha { get { return _ContraSenha; } set { _ContraSenha = value; } }

        private int? _TipoLicenca;
        public virtual int? TipoLicenca { get { return _TipoLicenca; } set { _TipoLicenca = value; } }
        public virtual int TipoServico { get; set; }

        private int? _Quantidade;
        public virtual int? Quantidade { get { return _Quantidade; } set { _Quantidade = value; } }

        private string _InfMaquina;
        public virtual string InfMaquina { get { return _InfMaquina; } set { _InfMaquina = value; } }

        private decimal? _ValorProduto;
        public virtual decimal? ValorProduto { get { return _ValorProduto; } set { _ValorProduto = value; } }

        private Servico _Servico;
        public virtual Servico Servico
        {
            get { return _Servico; }
            set
            {
                _Servico = value;
                if (_Servico != null)
                    _IDServico = _Servico.ID;
                else
                    _IDServico = 0;
            }
        }


        private Produto _Produto;
        public virtual Produto Produto
        {
            get { return _Produto; }
            set
            {
                _Produto = value;
                if (_Produto != null)
                    _IDProduto = _Produto.ID;
                else
                    _IDProduto = 0;
            }
        }

        private ClienteRevenda _ClienteRevenda;
        public virtual ClienteRevenda ClienteRevenda
        {
            get { return _ClienteRevenda; }
            set
            {
                _ClienteRevenda = value;
                if (_ClienteRevenda != null)
                    _IDClienteRevenda = _ClienteRevenda.ID;
                else
                    _IDClienteRevenda = 0;
            }
        }

        private CwkPedido _CwkPedido;
        public virtual CwkPedido Pedido
        {
            get { return _CwkPedido; }
            set
            {
                _CwkPedido = value;
                if (_CwkPedido != null)
                    _IDPedido = _CwkPedido.ID;
                else
                    _IDPedido = 0;
            }
        }

        public virtual string ArquivoLicencaSerializado { get; set; }

        private int? _NumeroSerie;
        public virtual int? NumeroSerie { get { return _NumeroSerie; } set { _NumeroSerie = value; } }
    }
}
