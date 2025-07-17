using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class IcmsBase : ModeloBase, IComparable<IcmsBase>
    {
        public override int ID { get; set; }

        public virtual int Codigo { get; set; }

        public virtual UF UFOrigem { get; set; }
        [InformacaoExibicao(1, "Origem", 300)]
        public virtual string UFOrigemStr { get { return UFOrigem != null ? UFOrigem.Sigla : String.Empty; } }

        public virtual UF UFDestino { get; set; }
        [InformacaoExibicao(2, "Destino", 300)]
        public virtual string UFDestinoStr { get { return UFDestino != null ? UFDestino.Sigla : String.Empty; } }

        public virtual int Tributacao { get; set; }

        public virtual int OrigemProd { get; set; }

        public virtual int ModBCICMS { get; set; }

        [InformacaoExibicao(3, "Alíquota", 180)]
        public virtual decimal AliqContrib { get; set; }

        public virtual decimal AliqContribNormal { get; set; }
       
        public virtual decimal AliqFCPNormal { get; set; }
        
        public virtual decimal AliqInterna { get; set; }
        public virtual decimal AliqDiferimento { get; set; }

        public virtual decimal ReducaoContrib { get; set; }

        public virtual string TextoLeiContrib { get; set; }

        public virtual decimal AliqNContrib { get; set; }
       
        public virtual decimal AliqFCPNContrib { get; set; }

        public virtual decimal AliqNContribNormal { get; set; }

        public virtual decimal ReducaoNContrib { get; set; }

        public virtual string TextoLeiNContrib { get; set; }

        public virtual int ModBCST { get; set; }

        public virtual decimal LucroST { get; set; }

        public virtual decimal PRedBCST { get; set; }

        public virtual decimal AliqSubstTributaria { get; set; }

        public virtual decimal AliqSimplesSubst { get; set; }

        public virtual int CSOSN { get; set; }

        public virtual TextoLei TextoContribuinte { get; set; }
        public virtual TextoLei TextoNaoContribuinte { get; set; }

        public virtual decimal ReducaoImpostoCont { get; set; }
        public virtual decimal ReducaoImpostoNCont { get; set; }
        public virtual decimal vICMSUFDest_NA15 { get; set; }
        public virtual decimal vICMSUFRemet_NA17 { get; set; }
     
        public virtual int CompareTo(IcmsBase other)
        {
            return Codigo.CompareTo(other.Codigo);
        }
    }
}
