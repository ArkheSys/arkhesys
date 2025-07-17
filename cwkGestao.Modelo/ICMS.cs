using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
	public class ICMS : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private string _Descricao;
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        private TabelaICMS _TabelaICMS;
        public virtual TabelaICMS TabelaICMS { get { return _TabelaICMS; } set { _TabelaICMS = value; } }

        private Filial _Filial;
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        private UF _UF;
        public virtual UF UF { get { return _UF; } set { _UF = value; } }

        private string _TextoLeiContrib;
        public virtual string TextoLeiContrib { get { return _TextoLeiContrib; } set { _TextoLeiContrib = value; } }

        private string _TextoLeiNContrib;
        public virtual string TextoLeiNContrib { get { return _TextoLeiNContrib; } set { _TextoLeiNContrib = value; } }

        private decimal _AliqContrib;
        public virtual decimal AliqContrib { get { return _AliqContrib; } set { _AliqContrib = value; } }

        private decimal _AliqContribNormal;
        public virtual decimal AliqContribNormal { get { return _AliqContribNormal; } set { _AliqContribNormal = value; } }

        private decimal _AliqNContrib;
        public virtual decimal AliqNContrib { get { return _AliqNContrib; } set { _AliqNContrib = value; } }

        private decimal _AliqNContribNormal;
        public virtual decimal AliqNContribNormal { get { return _AliqNContribNormal; } set { _AliqNContribNormal = value; } }

        private decimal _ReducaoContrib;
        public virtual decimal ReducaoContrib { get { return _ReducaoContrib; } set { _ReducaoContrib = value; } }

        private decimal _ReducaoNContrib;
        public virtual decimal ReducaoNContrib { get { return _ReducaoNContrib; } set { _ReducaoNContrib = value; } }

        private decimal _SubstLucro;
        public virtual decimal SubstLucro { get { return _SubstLucro; } set { _SubstLucro = value; } }

        private string _SitTrib;
        public virtual string SitTrib { get { return _SitTrib; } set { _SitTrib = value; } }

        private bool _BIcmsIsento;
        public virtual bool BIcmsIsento { get { return _BIcmsIsento; } set { _BIcmsIsento = value; } }

        private decimal _AliqSubstTributaria;
        public virtual decimal AliqSubstTributaria { get { return _AliqSubstTributaria; } set { _AliqSubstTributaria = value; } }

        private bool _BOrgaoPublico;
        public virtual bool BOrgaoPublico { get { return _BOrgaoPublico; } set { _BOrgaoPublico = value; } }

        private bool _Subst_Trib;
        public virtual bool Subst_Trib { get { return _Subst_Trib; } set { _Subst_Trib = value; } }

        private decimal _AliqSimplesSubst;
        public virtual decimal AliqSimplesSubst { get { return _AliqSimplesSubst; } set { _AliqSimplesSubst = value; } }

        private int _TAG_CST;
        public virtual int TAG_CST { get { return _TAG_CST; } set { _TAG_CST = value; } }

        private int _modBC_N13;
        public virtual int modBC_N13 { get { return _modBC_N13; } set { _modBC_N13 = value; } }

        private int _modBCST_N18;
        public virtual int modBCST_N18 { get { return _modBCST_N18; } set { _modBCST_N18 = value; } }

        private int _CST_Pis;
        public virtual int CST_Pis { get { return _CST_Pis; } set { _CST_Pis = value; } }

        private decimal _pPIS_Q08;
        public virtual decimal pPIS_Q08 { get { return _pPIS_Q08; } set { _pPIS_Q08 = value; } }

        private int _CST_Cofins;
        public virtual int CST_Cofins { get { return _CST_Cofins; } set { _CST_Cofins = value; } }

        private decimal _pCOFINS_S08;
        public virtual decimal pCOFINS_S08 { get { return _pCOFINS_S08; } set { _pCOFINS_S08 = value; } }
	}
}
		