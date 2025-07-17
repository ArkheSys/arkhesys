using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao(0, "Tipos de Serviço de Transporte", 100)]
    public class PessoaTipoServicoTransporte : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pessoa Pessoa { get; set; }
        [InformacaoExibicao(1, "Serviço", 100)]
		public virtual TipoServicoTransporte TipoServicoTransporte { get; set; }
		public virtual int CodShl { get; set; }
        [InformacaoExibicao(2, "Contrato", 100)]
        public virtual string CodContrato { get; set; }

        public override string ToString()
        {
            return Pessoa.Codigo + " | " + TipoServicoTransporte.Descricao;
        }
    }
}
