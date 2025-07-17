using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Sistema")]
    public class Sistema : ModeloBase
    {
        public Sistema()
        {
            Versaos = new List<Versao>();
            SistemaLicenca = 999;
        }


        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        [InformacaoExibicao(1, "Código", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Nome", 200)]
        public virtual string Nome { get; set; }

        public virtual short SistemaLicenca { get; set; }

#pragma warning disable CS0169 // O campo "Sistema._Versaos" nunca é usado
        private IList<Versao> _Versaos;
#pragma warning restore CS0169 // O campo "Sistema._Versaos" nunca é usado
        public virtual IList<Versao> Versaos
        {
            get;
            set;
        }

        public override string ToString()
        {

            return this.Nome;

        }
    }
}
