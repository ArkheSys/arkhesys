using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo;

namespace cwkGestao.Modelo
{
    public class Vinculo : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        [InformacaoExibicao(0, "Código", 90)]
        public virtual string CodProduto { get; set; }

        [InformacaoExibicao(1, "CFOP", 90)] public virtual string CFOP { get; set; }

        [InformacaoExibicao(2, "Produto Cwork", 90)]
        public virtual Produto Produto { get; set; }

        public virtual TabelaCFOP IDCFOP { get; set; }

        [InformacaoExibicao(4, "Descrição", 90)]
        public virtual string descricao { get; set; }

        [InformacaoExibicao(3, "CFOP", 90)] public virtual string CFOP_Cwork { get; set; }

        public virtual string Ncm { get; set; }


        /* Campos novos para importação do XML */
        public virtual string UnidadeEntradaSigla { get; set; }
        public virtual Unidade UnidadeEntrada { get; set; }
        public virtual decimal QuantidadeEntrada { get; set; }

        public virtual string UnidadeSaidaSigla { get; set; }
        public virtual Unidade UnidadeSaida { get; set; }
        public virtual decimal QuantidadeSaida { get; set; }

        public virtual string CodigoReferencia { get { return CodProduto; } }

        public virtual string CodEan { get; set; }
        public virtual string BarraImportacao { get { return Produto?.Barra; } }
    }

    public class DemaisInformações
    {
        public virtual string chave_nota { get; set; }
    }
}