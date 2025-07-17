using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio.Enums;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Tributacao
{
    public interface ITributavel
    {
        decimal BaseCalculoGeral { get; set; }

        string PisCST { get; }
        decimal PisBaseCalculo { get; set; }
        decimal PisAliquota { get; set; }
        decimal PisValor { get; set; }

        string IpiCST { get; }
        decimal IpiBaseCalculo { get; set; }
        decimal IpiAliquota { get; set; }
        decimal IpiValor { get; set; }

        string CofinsCST { get; set; }
        decimal CofinsBaseCalculo { get; set; }
        decimal CofinsAliquota { get; set; }
        decimal CofinsValor { get; set; }

        string IcmsCST { get; set; }
        string IcmsTextoLei { get; set; }
        decimal IcmsBaseCalculo { get; set; }
        decimal IcmsReducaoBaseCalculo { get; set; }
        decimal IcmsAliquota { get; set; }
        decimal IcmsAliquotaNormal { get; set; }
        decimal ReducaoImposto { get; set; }
        decimal IcmsValorDiferencaComReducao { get; set; }
        decimal IcmsValor { get; set; }
        decimal IcmsValorIsento { get; set; }
        decimal IcmsValorRetido { get; set; }
        decimal IcmsValorOutros { get; set; }
        decimal IcmsSTBaseCalculo { get; set; }
        decimal IcmsSTReducaoBaseCalculo { get; set; }
        decimal IcmsSTAliquota { get; set; }
        decimal IcmsSTMargemValorAgregado { get; set; }
        // FCP
        decimal FCPAliquota { get; set; }
        decimal FCPBaseClaculo { get; set; }
        decimal FCPValor { get; set; }

        decimal FCPAliquotaST { get; set; }
        decimal FCPBaseClaculoST { get; set; }
        decimal FCPValorST { get; set; }
        //----------

        // Diferimento
        decimal AliquotaDiferimento { get; set; }
        decimal ValorIcmsDiferimento { get; set; }
        decimal ValorIcmsOp { get; set; }

        decimal ValorICMSDesoneracao { get; set; }

        decimal CreditoReducaoBasePercentual { get; set; }
        decimal CreditoPercentualReduzido { get; set; }
        decimal CreditoValor { get; set; }
        decimal CreditoPercentual { get; set; }

        decimal EmpresaAliquota { get; set; }
        EmpresaST EmpresaST { get; }
        bool EhClienteOrgaoPublico { get; }
        bool EhCfopTributavelEmIcms { get; }
        bool EhSubstitutoTributario { get; set; }

        bool EhICMSProprio { get; }
        decimal PrecoProduto { get; }

        Filial Filial { get; }
        Pessoa Pessoa { get; }
    }
}
