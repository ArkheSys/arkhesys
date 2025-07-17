using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cidade")]
    public class Cidade : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1,"Código", 90)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;

        [InformacaoExibicao(2, "Nome", 170)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private int _IDUF;
        public virtual int IDUF { get { return _IDUF; } set { _IDUF = value; } }

        private UF _UF;
        [InformacaoExibicao(4, "Estado", 70)]
        public virtual UF UF 
        { 
            get { return _UF; } 
            set 
            { 
                _UF = value;
                if (_UF != null)
                    _IDUF = _UF.ID; 
            } 
        }

        private string _IBGE;
        [InformacaoExibicao(3, "IBGE", 70, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string IBGE { get { return _IBGE; } set { _IBGE = value; } }
        
        [InformacaoExibicao(5, "País", 110)]
        public virtual Pais Pais { get { return UF.Pais; } }

        public virtual string CodigoTributacaoMunicipio { get; set; }

        //public virtual string CodigoCidadePrestacao { get; set; }

    }
}
