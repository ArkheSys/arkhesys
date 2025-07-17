using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CwkPedido : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private OrcamentoStatus _Status;
        public virtual OrcamentoStatus Status { get { return _Status; } set { _Status = value; } }

        private String _MotivoCancelamento = "";
        public virtual String MotivoCancelamento { get { return _MotivoCancelamento; } set { _MotivoCancelamento = value; } }

        private Boolean _BDemonstracao = false;
        [InformacaoExibicao(0, "Demonstração", 40)]
        public virtual Boolean BDemonstracao { get { return _BDemonstracao; } set { _BDemonstracao = value; } }
        
        private int _Codigo;
        [InformacaoExibicao(1, "Código", 425)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private DateTime? _Data;
        public virtual DateTime? Data { get { return _Data; } set { _Data = value; } }

        private DateTime? _Hora;
        public virtual DateTime? Hora { get { return _Hora; } set { _Hora = value; } }

        private int _IDPessoa;
        public virtual int IDPessoa
        {
            get { return _IDPessoa; }
            set
            {
                if (value != 0)
                    _Pessoa = new Pessoa() { ID = value };
                else
                    _Pessoa = null; 
                
                _IDPessoa = value;
            }
        }

        private string _Solicitante;
        public virtual string Solicitante { get { return _Solicitante; } set { _Solicitante = value; } }

        private int _IDSistema;
        public virtual int IDSistema
        {
            get { return _IDSistema; }
            set
            {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                if (value != null)
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'int' nunca é igual a "null" do tipo 'int?'
                    _Sistema = new Sistema() { ID = value };
                else
                    _Sistema = null;

                _IDSistema = value;
            }
        }

        private int? _IDVersao;
        public virtual int? IDVersao
        {
            get { return _IDVersao; }
            set
            {
                if (value.HasValue)
                    _Versao = new Versao() { ID = value.Value };
                else
                    _Versao = null;

                _IDVersao = value;
            }
        }

        private String _Observacao;
        public virtual String Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private DateTime? _DataSite;
        public virtual DateTime? DataSite { get { return _DataSite; } set { _DataSite = value; } }

        private DateTime? _HoraSite;
        public virtual DateTime? HoraSite { get { return _HoraSite; } set { _HoraSite = value; } }

        private String _Liberado;
        public virtual String Liberado { get { return _Liberado; } set { _Liberado = value; } }

        private int? _NumeroSerie;
        public virtual int? NumeroSerie { get { return _NumeroSerie; } set { _NumeroSerie = value; } }

        private DateTime? _IncData;
#pragma warning disable CS0114 // "CwkPedido.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime? IncData { get { return _IncData; } set { _IncData = value; } }
#pragma warning restore CS0114 // "CwkPedido.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private DateTime? _IncHora;
        public virtual DateTime? IncHora { get { return _IncHora; } set { _IncHora = value; } }

        private String _IncUsuario;
#pragma warning disable CS0114 // "CwkPedido.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual String IncUsuario { get { return _IncUsuario; } set { _IncUsuario = value; } }
#pragma warning restore CS0114 // "CwkPedido.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private DateTime? _AltData;
#pragma warning disable CS0114 // "CwkPedido.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime? AltData { get { return _AltData; } set { _AltData = value; } }
#pragma warning restore CS0114 // "CwkPedido.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private DateTime? _AltHora;
        public virtual DateTime? AltHora { get { return _AltHora; } set { _AltHora = value; } }

        private String _AltUsuario;
#pragma warning disable CS0114 // "CwkPedido.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual String AltUsuario { get { return _AltUsuario; } set { _AltUsuario = value; } }
#pragma warning restore CS0114 // "CwkPedido.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa
        {
            get { return _Pessoa; }
            set
            {
                _Pessoa = value;
                if (_Pessoa != null)
                    _IDPessoa = _Pessoa.ID;
                else
                    _IDPessoa = 0;
            }
        }
        
        private Sistema _Sistema;
        [InformacaoExibicao(2, "Sistema", 425)]
        public virtual Sistema Sistema
        {
            get { return _Sistema; }
            set
            {
                _Sistema = value;
                if (_Sistema != null)
                    _IDSistema = _Sistema.ID;
                else
                    _IDSistema = 0;
            }
        }

        private Versao _Versao;
        [InformacaoExibicao(3, "Versão", 425)]
        public virtual Versao Versao
        {
            get { return _Versao; }
            set
            {
                _Versao = value;
                if (_Versao != null)
                    _IDVersao = _Versao.ID;
                else
                    _IDVersao = null;
            }
        }

        public virtual bool BloqueioUsuarios { get; set; }
        public virtual bool Relatorioabsenteismo { get; set; }
        public virtual bool ExportacaoHorasAbonadas { get; set; }
        public virtual bool ModuloRefeitorio { get; set; }

        public virtual bool LicencaReservada { get; set; }
        public virtual int Idrevenda { get; set; }

        private IList<CwkPedidoEmpresa> _CwkPedidoEmpresa;
        public virtual IList<CwkPedidoEmpresa> CwkPedidoEmpresas
        {
            get { return _CwkPedidoEmpresa; }
            set
            {
                _CwkPedidoEmpresa = value;
                foreach (CwkPedidoEmpresa item in _CwkPedidoEmpresa)
                {
                    item.IDPedido = this.ID;
                    item.Pedido = this;
                }
            }
        }

        public virtual string NumeroSerieEmpresas
        {
            get {
                return string.Join(",", CwkPedidoEmpresas.Select(x => x.NumeroSerie == null ? "" : x.NumeroSerie.ToString()).ToArray());
            }
        }

        public virtual System.Collections.ArrayList CwkPedidoEmpresasArrayList { get { return new System.Collections.ArrayList(CwkPedidoEmpresas.ToArray<CwkPedidoEmpresa>()); } }
    }
}
