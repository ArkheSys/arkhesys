using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Operação Fiscal")]
    public class Operacao : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 600, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }                     

        private TabelaCFOP _CFOP_DEstado;
        public virtual TabelaCFOP CFOP_DEstado { get { return _CFOP_DEstado; } set { _CFOP_DEstado = value; } }

        [InformacaoExibicao(3, "CFOP Dentro Estado", 120)]
        public virtual string CodigoCFOP_DEstado { get { return _CFOP_DEstado?.CFOP; } }

        private TabelaCFOP _CFOP_FEstado_C;
        public virtual TabelaCFOP CFOP_FEstado_C { get { return _CFOP_FEstado_C; } set { _CFOP_FEstado_C = value; } }
        [InformacaoExibicao(4, "CFOP Fora Est.Contrib.", 130)]
        public virtual string CodigoCFOP_FEstado { get { return _CFOP_FEstado_C?.CFOP; } }

        private TabelaCFOP _CFOP_FEstado_NC;
        public virtual TabelaCFOP CFOP_FEstado_NC { get { return _CFOP_FEstado_NC; } set { _CFOP_FEstado_NC = value; } }
        [InformacaoExibicao(5, "CFOP Fora Est.Não Contrib.", 150)]
        public virtual string CodigoCFOP_FEstado_NC { get { return _CFOP_FEstado_NC?.CFOP; } }

        private IList<TipoNota> _TipoNotas;
        public virtual IList<TipoNota> TipoNotas { get { return _TipoNotas; } set { _TipoNotas = value; } }

        private TabelaCFOP _CFOP_DEstado_ST;
        public virtual TabelaCFOP CFOP_DEstado_ST { get { return _CFOP_DEstado_ST; } set { _CFOP_DEstado_ST = value; } }
        [InformacaoExibicao(6, "CFOP Dentro Estado C/ ST", 150)]
        public virtual string CodigoCFOP_DEstado_ST { get { return _CFOP_DEstado_ST?.CFOP; } }

        private TabelaCFOP _CFOP_FEstado_C_ST;
        public virtual TabelaCFOP CFOP_FEstado_C_ST { get { return _CFOP_FEstado_C_ST; } set { _CFOP_FEstado_C_ST = value; } }
        [InformacaoExibicao(7, "CFOP Fora Est.Contrib. C/ ST", 150)]
        public virtual string CodigoCFOP_FEstado_C_ST { get { return _CFOP_FEstado_C_ST?.CFOP; } }

        private TabelaCFOP _CFOP_FEstado_NC_ST;
        public virtual TabelaCFOP CFOP_FEstado_NC_ST { get { return _CFOP_FEstado_NC_ST; } set { _CFOP_FEstado_NC_ST = value; } }
        [InformacaoExibicao(8, "CFOP Fora Est.Não Contrib. C/ ST", 180)]
        public virtual string CodigoCFOP_FEstado_NC_ST { get { return _CFOP_FEstado_NC_ST?.CFOP; } }

        public virtual CodigoImposto NatBcCred { get; set; }
    }
}
