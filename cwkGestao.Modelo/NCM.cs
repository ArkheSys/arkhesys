using System;
using System.Collections.Generic;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("NCM")]
    public class NCM : ModeloBase
    {
        public NCM()
        {
            NCMItens = new List<NCMItem>();
            CestsVinculados = new List<NCMCEST>();
        }

        public override int ID { get; set; }

        [InformacaoExibicao(0, "Código", 60)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(1, "NCM", 60)]
        public virtual string Ncm { get; set; }


        [InformacaoExibicao(2, "Descrição", 150)]
        public virtual string Descricao { get; set; }

        /*[InformacaoExibicao(10, "nacionalfederal", 50)]
        public virtual decimal? Nacionalfederal { get; set; }

        [InformacaoExibicao(11, "importadosfederal", 50)]
        public virtual decimal? Importadosfederal { get; set; }*/

        [InformacaoExibicao(12, "estadual", 50)]
        public virtual decimal? Estadual { get; set; }

        [InformacaoExibicao(13, "municipal", 50)]
        public virtual decimal? Municipal { get; set; }

        public virtual string AliqCupom { get; set; }

        /*[InformacaoExibicao(8, "Ipi", 50)]
        public virtual decimal Ipi { get; set; }

        [InformacaoExibicao(4, "Pis", 50)]
        public virtual decimal Pis { get; set; }

        [InformacaoExibicao(6, "Cofins", 50)]
        public virtual decimal Cofins { get; set; }

        [InformacaoExibicao(3, "CST_Pis", 50)]
        public virtual int CST_Pis { get; set; }

        [InformacaoExibicao(5, "CST_Cofins", 50)]
        public virtual int CST_Cofins { get; set; }

        [InformacaoExibicao(7, "CST_Ipi", 50)]
        public virtual int CST_Ipi { get; set; }

        [InformacaoExibicao(9, "Cód.Enq.", 50)]
        public virtual int EnqGeral { get; set; }*/
        public virtual decimal? NacionalFederal { get; set; }
        public virtual decimal? ImportadosFederal { get; set; }

        public virtual IList<NCMItem> NCMItens { get; set; }

        public virtual IList<NCMCEST> CestsVinculados { get; set; } = new List<NCMCEST>();

        public virtual DateTime? DtRevogacao { get; set; }
        public override string ToString() { return this.Ncm; }
    }
}