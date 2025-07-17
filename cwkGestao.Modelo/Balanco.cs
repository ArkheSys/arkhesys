using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Balanço")]
    public class Balanco : ModeloBase
    {
        public Balanco()
        {
            BalancoItems = new List<BalancoItem>();
        }

        public override int ID { get; set; }
        public virtual DateTime Data { get; set; }        
        public virtual LocalEstoque LocalEstoque { get; set; }
        public virtual IList<BalancoItem> BalancoItems { get; set; }

        [InformacaoExibicao(1, "Iniciado em", 80)]
        public virtual string IniciadoEm { get { return this.IncData.HasValue ? this.IncData.Value.ToShortDateString() : ""; } }
        [InformacaoExibicao(3, "Fechado em", 80)]
        public virtual string FechadoEm { get { return this.Data.Year > 1950 ? this.Data.ToShortDateString() : ""; } }

        [InformacaoExibicao(2, "Local Estoque", 150)]
        public virtual string LocalEstoqueStr { get { return LocalEstoque != null ? LocalEstoque.Descricao : String.Empty; } }
    }
}
