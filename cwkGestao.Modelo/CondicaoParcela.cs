using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Parcela da Condição")]
	public class CondicaoParcela : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private Condicao _Condicao;        
        public virtual Condicao Condicao { get { return _Condicao; } set { _Condicao = value; } }

        private int _Parcela;
        [InformacaoExibicao(1, "Parcela")]
        public virtual int Parcela { get { return _Parcela; } set { _Parcela = value; } }

        private string _TipoDt;
        public virtual string TipoDt { get { return _TipoDt; } set { _TipoDt = value; } }

        private DateTime? _Dt;
        
        public virtual DateTime? Dt { get { return _Dt; } set { _Dt = value; } }

        private int _DiaMes;
        [InformacaoExibicao(2, "Dia/Mes")]
        public virtual int DiaMes { get { return _DiaMes; } set { _DiaMes = value; } }

        private string _TipoVlr;
        [InformacaoExibicao(4, "Tipo de Valor")]
        public virtual string TipoVlr { get { return _TipoVlr; } set { _TipoVlr = value; } }

        private decimal _VlrPerc;
        [InformacaoExibicao(3, "Valor")]
        public virtual decimal VlrPerc { get { return _VlrPerc; } set { _VlrPerc = value; } }

        private bool _Entrada;
        public virtual bool Entrada { get { return _Entrada; } set { _Entrada = value; } }

        private TipoDocumento _TipoDocumento;
        public virtual TipoDocumento TipoDocumento { get { return _TipoDocumento; } set { _TipoDocumento = value; } }

        private bool _BAVista;
        public virtual bool BAVista { get { return _BAVista; } set { _BAVista = value; } }

        [InformacaoExibicao(4, "Tipo Documento")]
        public virtual string TipoDocumentoStr { get { return TipoDocumento?.Nome; } }
    }
}
		