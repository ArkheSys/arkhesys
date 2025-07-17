using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("ICMS")]
	public class NotaICMS : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private Nota _Nota;
        public virtual Nota Nota { get { return _Nota; } set { _Nota = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private TabelaCFOP _CFOP;
        [InformacaoExibicao(0, "CFOP", 70)]
        public virtual TabelaCFOP CFOP { get { return _CFOP; } set { _CFOP = value; } }

        private ICMS _ICMS;
        public virtual ICMS ICMS { get { return _ICMS; } set { _ICMS = value; } }

        private decimal _BaseICMS;
        [InformacaoExibicao(1, "Base", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal BaseICMS { get { return _BaseICMS; } set { _BaseICMS = value; } }

        private decimal _AliqICMS;
        [InformacaoExibicao(2, "Aliq.", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Porcentagem4Casas)]
        public virtual decimal AliqICMS { get { return _AliqICMS; } set { _AliqICMS = value; } }

        private decimal _AliqICMSNormal;
        public virtual decimal AliqICMSNormal { get { return _AliqICMSNormal; } set { _AliqICMSNormal = value; } }

        private decimal _ValorICMS;
        [InformacaoExibicao(3, "Valor", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorICMS { get { return _ValorICMS; } set { _ValorICMS = value; } }

        private decimal _ValorDiferidoICMS;
        public virtual decimal ValorDiferidoICMS { get { return _ValorDiferidoICMS; } set { _ValorDiferidoICMS = value; } }

        private decimal _ValorIsentoICMS;
        [InformacaoExibicao(5, "Isento", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorIsentoICMS { get { return _ValorIsentoICMS; } set { _ValorIsentoICMS = value; } }

        private decimal _ValorOutroICMS;
        [InformacaoExibicao(4, "Outro", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorOutroICMS { get { return _ValorOutroICMS; } set { _ValorOutroICMS = value; } }

        private decimal _ValorTotalProdutos;
        [InformacaoExibicao(8, "Produtos", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorTotalProdutos { get { return _ValorTotalProdutos; } set { _ValorTotalProdutos = value; } }

        private string _TextoIcms;
        public virtual string TextoIcms { get { return _TextoIcms; } set { _TextoIcms = value; } }

        private decimal _ValorSubst;
        [InformacaoExibicao(6, "Icms Subst.", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorSubst { get { return _ValorSubst; } set { _ValorSubst = value; } }

        private decimal _BaseSubst;
        [InformacaoExibicao(7, "Base Subst.", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal BaseSubst { get { return _BaseSubst; } set { _BaseSubst = value; } }

        private decimal _ValorIPI;
        public virtual decimal ValorIPI { get { return _ValorIPI; } set { _ValorIPI = value; } }

        private decimal? _TotalImpostos;
        public virtual decimal? TotalImpostos { get { return _TotalImpostos; } set { _TotalImpostos = value; } }

        private decimal _ValorPIS;
        public virtual decimal ValorPIS { get { return _ValorPIS; } set { _ValorPIS = value; } }

        private decimal _ValorCOFINS;
        public virtual decimal ValorCOFINS { get { return _ValorCOFINS; } set { _ValorCOFINS = value; } }

        private decimal _vICMSDeson;
        [InformacaoExibicao(8, "Valor ICMS Deson.", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal vICMSDeson { get { return _vICMSDeson; } set { _vICMSDeson = value; } }

        public static NotaICMS CloneNewObject(NotaICMS objNotaICMS)
        {
            AutoMapper.Mapper.CreateMap<Modelo.NotaICMS, Modelo.NotaICMS>().ForMember(dest => dest.ID, o => o.Ignore());
            return AutoMapper.Mapper.Map<NotaICMS, NotaICMS>(objNotaICMS);
        }

        public static void GetClones(ref Nota objNota)
        {
            IList<NotaICMS> lstNotaICMS = new List<NotaICMS>(objNota.NotaICMSs);
            objNota.NotaICMSs = new List<NotaICMS>();
            foreach (var NotaICMS in lstNotaICMS)
            {
                NotaICMS objNotaICMS = NotaICMS.CloneNewObject(NotaICMS);
                objNotaICMS.Nota = objNota;

                objNota.NotaICMSs.Add(objNotaICMS);
            }
        }
	}
}