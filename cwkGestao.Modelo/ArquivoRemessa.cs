using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("ArquivoRemessa")]
    public class ArquivoRemessa : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _NumRemessa;
        public virtual int NumRemessa { get { return _NumRemessa; } set { _NumRemessa = value; } }

        private string _UsuarioCriacao;
        public virtual string UsuarioCriacao { get { return _UsuarioCriacao; } set { _UsuarioCriacao = value; } }

        private string _UsuarioAlteracao;
        public virtual string UsuarioAlteracao { get { return _UsuarioAlteracao; } set { _UsuarioAlteracao = value; } }

        private DateTime _DataCriacao;
        public virtual DateTime DataCriacao { get { return _DataCriacao; } set { _DataCriacao = value; } }

        private DateTime? _DataAlteracao;
        public virtual DateTime? DataAlteracao { get { return _DataAlteracao; } set { _DataAlteracao = value; } }

        private Filial _Filial;
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        private Remessa _Remessa;
        public virtual Remessa Remessa { get { return _Remessa; } set { _Remessa = value; } }

        public virtual bool Selecionado { get; set; }

        private DateTime _dataInicialFiltro;
        public virtual DateTime dataInicialFiltro { get { return _dataInicialFiltro; } set { _dataInicialFiltro = value; } }

        private DateTime _dataFinalFiltro;
        public virtual DateTime dataFinalFiltro { get { return _dataFinalFiltro; } set { _dataFinalFiltro = value; } }

        private int _tipoFiltro;
        public virtual int tipoFiltro { get { return _tipoFiltro; } set { _tipoFiltro = value; } }

        private TipoDocumento _TipoDocumento;
        public virtual TipoDocumento TipoDocumento { get { return _TipoDocumento; } set { _TipoDocumento = value; } }

        private Portador _Portador;
        public virtual Portador Portador { get { return _Portador; } set { _Portador = value; } }

    }
}
