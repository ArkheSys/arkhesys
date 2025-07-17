using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Rastreamento")]
    public class RastreamentoCorreio : ModeloBase
    {
        public RastreamentoCorreio()
        {
        }

        public RastreamentoCorreio(int id, TipoServicoTransporte tipoServico, string letra, string numero, Filial filial)
        {
            ID = id;
            TipoServico = tipoServico;
            Letra = letra;
            Numero = numero;
            Filial = filial;
        }

        public override int ID { get; set; }

        public virtual TipoServicoTransporte TipoServico { get; set; }
        
        [InformacaoExibicao(2, "Letra", 100)]
        public virtual string Letra { get; set; }
        
        [InformacaoExibicao(1, "Numero", 200)]
        public virtual string Numero { get; set; }

        [InformacaoExibicao(0, "Tipo Serviço", 250)]
        public virtual string TipoServicoTela { get { return TipoServico == null ? "" : TipoServico.Descricao; } }

        public virtual string CodigoEntregaMagento { get; set; }

        public virtual bool SolicitadoWScorreios { get; set; }

        [InformacaoExibicao(3, "Empresa", 250)]
        public virtual Filial Filial { get; set; }

        public virtual bool RastreamentoCompleto { get; set; }

        public virtual string Codigo
        {
            get { return Letra + Numero + "BR"; }
        }
    }
}
