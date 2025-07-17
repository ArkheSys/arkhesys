using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class PessoaCliente : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int? _IDPessoa;
        public virtual int? IDPessoa { get { return _IDPessoa; } set { _IDPessoa = value; } }

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        private bool _BContribuinte;
        

        private bool _BBloqueiaCondicao;
        public virtual bool BBloqueiaCondicao { get { return _BBloqueiaCondicao; } set { _BBloqueiaCondicao = value; } }

        private int? _IDCondicao;
        public virtual int? IDCondicao { get { return _IDCondicao; } set { _IDCondicao = value; } }

        private Condicao _Condicao;
        public virtual Condicao Condicao { get { return _Condicao; } set { _Condicao = value; } }

        private bool _BBloqueiaTabelaPreco;
        public virtual bool BBloqueiaTabelaPreco { get { return _BBloqueiaTabelaPreco; } set { _BBloqueiaTabelaPreco = value; } }

        private int? _IDTabelaPreco;
        public virtual int? IDTabelaPreco { get { return _IDTabelaPreco; } set { _IDTabelaPreco = value; } }

        private TabelaPreco _TabelaPreco;
        public virtual TabelaPreco TabelaPreco { get { return _TabelaPreco; } set { _TabelaPreco = value; } }

        private int? _IDVendedor;
        public virtual int? IDVendedor { get { return _IDVendedor; } set { _IDVendedor = value; } }

        private Pessoa _Vendedor;
        public virtual Pessoa Vendedor { get { return _Vendedor; } set { _Vendedor = value; } }

        private decimal? _LimiteCredito;
        public virtual decimal? LimiteCredito { get { return _LimiteCredito; } set { _LimiteCredito = value; } }

        private string _NomeConjuge;
        public virtual string NomeConjuge { get { return _NomeConjuge; } set { _NomeConjuge = value; } }

        private string _NomePai;
        public virtual string NomePai { get { return _NomePai; } set { _NomePai = value; } }

        private string _NomeMae;
        public virtual string NomeMae { get { return _NomeMae; } set { _NomeMae = value; } }

        private bool _BBloqueiaVendedor;
        public virtual bool BBloqueiaVendedor { get { return _BBloqueiaVendedor; } set { _BBloqueiaVendedor = value; } }

        public virtual bool BContribuinte 
        {
            get 
                { 
                    return _BContribuinte; 
                } 
            set {
                    _BContribuinte = value;
                    Pessoa.BContribuinte = _BContribuinte;
                }   
        }

    }
}
