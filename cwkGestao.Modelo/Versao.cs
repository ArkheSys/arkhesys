using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Versão")]
    public class Versao : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private int? _IDSistema;
        public virtual int? IDSistema { get { return _IDSistema; } set { _IDSistema = value; } }

        private Sistema _Sistema;
        public virtual Sistema Sistema
        {
            get { return _Sistema; }
            set
            {
                _Sistema = value;
                if (_Sistema != null)
                    _IDSistema = _Sistema.ID;
                else
                    _IDSistema = null;
            }
        }

        private string _Numero;
        [InformacaoExibicao(1, "Versão", 100)]
        public virtual string Numero { get { return _Numero; } set { _Numero = value; } }

        private DateTime? _DataLiberacao;
        [InformacaoExibicao(2, "Data Liberação", 100)]
        public virtual DateTime? DataLiberacao { get { return _DataLiberacao; } set { _DataLiberacao = value; } }

        private string _Observacao;
        [InformacaoExibicao(3, "Observação", 100)]
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private DateTime? _Incdata;
        public virtual DateTime? Incdata { get { return _Incdata; } set { _Incdata = value; } }

        private string _Incusuario;
        public virtual string Incusuario { get { return _Incusuario; } set { _Incusuario = value; } }

        private DateTime? _Altdata;
        public virtual DateTime? Altdata { get { return _Altdata; } set { _Altdata = value; } }

        private string _Altusuario;
        public virtual string Altusuario { get { return _Altusuario; } set { _Altusuario = value; } }

        private string _Caminhobancolimpo;
        public virtual string Caminhobancolimpo { get { return _Caminhobancolimpo; } set { _Caminhobancolimpo = value; } }

        private short _Tipobanco;
        public virtual short Tipobanco { get { return _Tipobanco; } set { _Tipobanco = value; } }

        private string _Servidor;
        public virtual string Servidor { get { return _Servidor; } set { _Servidor = value; } }

        private string _Banco;
        public virtual string Banco { get { return _Banco; } set { _Banco = value; } }

        private string _Usuario;
        public virtual string Usuario { get { return _Usuario; } set { _Usuario = value; } }

        private string _Senha;
        public virtual string Senha { get { return _Senha; } set { _Senha = value; } }

        private string _Parametros;
        public virtual string Parametros { get { return _Parametros; } set { _Parametros = value; } }

        private string _CaminhoBackup;
        public virtual string CaminhoBackup { get { return _CaminhoBackup; } set { _CaminhoBackup = value; } }

        private string _CaminhoWebAPI;
        public virtual string CaminhoWebAPI { get { return _CaminhoWebAPI; } set { _CaminhoWebAPI = value; } }

        private string _CaminhoCentralCliente;
        public virtual string CaminhoCentralCliente { get { return _CaminhoCentralCliente; } set { _CaminhoCentralCliente = value; } }


        public override string ToString()
        {
            return Numero;

        }
    }
}
