using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Impostos e Tributos")]
    public class ImpostosTributos : ModeloBase
    {
        public ImpostosTributos()
        {
            ImpostosTributosExcessoesItens = new List<ImpostosTributosExcessoes>();
        }
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 100)]
        public virtual string Descricao { get; set; }
        public virtual int CSTCSOSNIcms { get; set; }
        public virtual int CstIpi { get; set; }
        public virtual int ModBaseCalculoIcms { get; set; }
        public virtual int ModBaseCalculoIcmsSt { get; set; }

        public virtual string InfAdicionaisDentroEstado { get; set; }
        public virtual string InfAdicionaisForaEstado { get; set; }

        public virtual decimal PIcmsInterno { get; set; }
        public virtual decimal PReducaoIcms { get; set; }
        public virtual decimal PReducaoIcmsST { get; set; }
        public virtual decimal PDeferimento { get; set; }
        public virtual decimal PDesoneracao { get; set; }
        public virtual decimal PMVA { get; set; }


        public virtual bool DestacarIcms { get; set; }
        public virtual bool DestacarIPI { get; set; }
        public virtual bool DestacarIcmsST { get; set; }
        public virtual bool IPIFazBaseIcms { get; set; }
        public virtual bool IcmsFazBasePisCofins { get; set; }

        public virtual ClassificacaoFiscal ClassificacaoFiscal { get; set; }
        public virtual PerfilTributarioCliente PerfilTributarioCliente { get; set; }
        
        public virtual TabelaCFOP CfopDentroDoEstado { get; set; }
        public virtual TabelaCFOP CfopForaDoEstado { get; set; }


        public virtual IList<ImpostosTributosExcessoes> ImpostosTributosExcessoesItens { get; set; }

        public virtual ImpostosTributosExcessoes UsarEssaExcessao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
