using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Grupo Cliente Preço")]
    public class Tel_GrupoClientePreco : Tel_PrecoBase
	{
        public override int ID { get; set; }
        public virtual Tel_GrupoCliente GrupoCliente { get; set; }
        [InformacaoExibicao(2, "Valor", 85)]
        public override decimal Valor { get; set; }
        public override Tel_TipoLigacao TipoLigacao { get; set; }
        [InformacaoExibicao(3, "Área Inicial", 85)]
        public override short Area_Inicial { get; set; }
        [InformacaoExibicao(4, "Área Final", 85)]
        public override short Area_Final { get; set; }
        [InformacaoExibicao(5, "Prefixo Inicial", 85)]
        public override int Prefixo_Inicial { get; set; }
        [InformacaoExibicao(6, "Prefixo Final", 85)]
        public override int Prefixo_Final { get; set; }
        [InformacaoExibicao(7, "Range Inicial", 85)]
        public override int Range_Inicial { get; set; }
        [InformacaoExibicao(8, "Range Final", 85)]
        public override int Range_Final { get; set; }
        [InformacaoExibicao(1, "Tipo Ligação", 85)]
        public virtual String DescTipoLigacao { get { return TipoLigacao.Descricao; } }

    }
}
		