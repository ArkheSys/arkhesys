using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Classificação")]
	public class Classificacao : ModeloBase
	{
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 640)]
        public virtual string Descricao { get; set; }

        public virtual bool bShlMailing { get; set; }

        public virtual int CodShl { get; set; }

        public override string ToString()
        {
            try { return Descricao; }
            catch { return base.ToString(); }
        }
	}
}
		