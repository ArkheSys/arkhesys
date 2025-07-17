using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio.Enums;
using cwkGestao.Negocio.Tributacao.Impl;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Tributacao
{
    public class Tributacao
    {
        ITributavel _tributavel;

        public Tributacao(ITributavel objetoTributavel)
        {
            _tributavel = objetoTributavel;
        }

        #region booleans
        private bool EhParaCalcularPis(string cstPis)
        {
            try
            {
                return CstCalculoProvider.PisCsts.Contains(cstPis);
            }
            catch
            {
                this.CstCalculoProvider = new CstCalculoProviderFromXml();
            }
            return CstCalculoProvider.PisCsts.Contains(cstPis);

        }

        private bool EhParaCalcularIpi(string cstIpi)
        {
            try
            {
                return CstCalculoProvider.IpiCsts.Contains(cstIpi);
            }
            catch
            {
                this.CstCalculoProvider = new CstCalculoProviderFromXml();
            }
            return CstCalculoProvider.IpiCsts.Contains(cstIpi);
        }

        private bool EhParaCalcularCofins(string cstCofins)
        {
            try
            {
                return CstCalculoProvider.CofinsCsts.Contains(cstCofins);
            }
            catch
            {
                this.CstCalculoProvider = new CstCalculoProviderFromXml();
            }
            return CstCalculoProvider.CofinsCsts.Contains(cstCofins);
        }

        private bool EhParaCalcularSubstTributaria(string cstIcms)
        {
            try
            {
                return CstCalculoProvider.SubstituicaoTributariaCsts.Contains(cstIcms);
            }
            catch
            {
                this.CstCalculoProvider = new CstCalculoProviderFromXml();
            }
            return CstCalculoProvider.SubstituicaoTributariaCsts.Contains(cstIcms);
        }

        private bool EhTributacaoIsentaDeIcms(bool cfopTributavel, string cstIcms)
        {
            return !cfopTributavel || !EhParaCalcularIcms(cfopTributavel, cstIcms);
        }

        private bool EhParaCalcularIcms(bool cfopTributavel, string cstIcms)
        {
            bool retorno = true;
            try
            {
                if (cfopTributavel)
                {
                    retorno = CstCalculoProvider.IcmsCsts.Contains(cstIcms);
                }
                else
                {
                    retorno = false;
                }

            }
            catch
            {
                this.CstCalculoProvider = new CstCalculoProviderFromXml();
                retorno = CstCalculoProvider.IcmsCsts.Contains(cstIcms);
            }
            return retorno;
        }
        #endregion

        #region metodos auxiliares

        public ICstCalculoProvider CstCalculoProvider { get; set; }

        public decimal ReduzirBaseCalculo(decimal baseCalculo, decimal reducaoBaseCalculo)
        {
            decimal novaBaseCalculo = baseCalculo * (1 - (reducaoBaseCalculo / 100));

            return Math.Round(novaBaseCalculo, 2);
        }

        public decimal CalcularValorAliquota(decimal baseCalculo, decimal aliquota)
        {
            decimal valorAliquota = baseCalculo * (aliquota / 100);

            return Math.Round(valorAliquota, 2);
        }

        #endregion

        #region IPI/PIS/COFINS/CREDITOS ICMS

        public void CalculaPis()
        {
            if (EhParaCalcularPis(_tributavel.PisCST))
            {
                _tributavel.PisBaseCalculo = _tributavel.BaseCalculoGeral;
                _tributavel.PisValor = CalcularValorAliquota(_tributavel.PisBaseCalculo, _tributavel.PisAliquota);
            }
            else
            {
                _tributavel.PisAliquota = 0;
                _tributavel.PisBaseCalculo = 0;
                _tributavel.PisValor = 0;
            }
        }

        public void CalculaIpi()
        {
            if (EhParaCalcularIpi(_tributavel.IpiCST))
            {
                _tributavel.IpiBaseCalculo = _tributavel.BaseCalculoGeral;
                _tributavel.IpiValor = CalcularValorAliquota(_tributavel.IpiBaseCalculo, _tributavel.IpiAliquota);

            }
            else
            {
                _tributavel.IpiAliquota = 0;
                _tributavel.IpiBaseCalculo = 0;
                _tributavel.IpiValor = 0;
            }
        }

        public void CalculaCofins()
        {
            if (EhParaCalcularCofins(_tributavel.CofinsCST))
            {
                _tributavel.CofinsBaseCalculo = _tributavel.BaseCalculoGeral;
                _tributavel.CofinsValor = CalcularValorAliquota(_tributavel.CofinsBaseCalculo, _tributavel.CofinsAliquota);
            }
            else
            {
                _tributavel.CofinsAliquota = 0;
                _tributavel.CofinsBaseCalculo = 0;
                _tributavel.CofinsValor = 0;
            }
        }

        public void CalculaCredito()
        {
            if (_tributavel.EmpresaST == EmpresaST.Simples && EhParaCalcularCredito(_tributavel.IcmsCST))
            {
                decimal percentualReduzido = this.ReduzirBaseCalculo(_tributavel.CreditoPercentual, _tributavel.CreditoReducaoBasePercentual);
                _tributavel.CreditoPercentualReduzido = percentualReduzido;
                _tributavel.CreditoValor = this.CalcularValorAliquota(_tributavel.BaseCalculoGeral, percentualReduzido);
            }
            else
            {
                _tributavel.CreditoPercentual = 0;
                _tributavel.CreditoReducaoBasePercentual = 0;
                _tributavel.CreditoPercentualReduzido = 0;
                _tributavel.CreditoValor = 0;
            }
        }

        private bool EhParaCalcularCredito(string cstIcms)
        {
            try
            {
                return CstCalculoProvider.CreditoCsts.Contains(cstIcms);
            }
            catch
            {
                this.CstCalculoProvider = new CstCalculoProviderFromXml();
            }
            return CstCalculoProvider.CreditoCsts.Contains(cstIcms);
        }

        #endregion

        public void CalculaIcms()
        {
            CalculaICMSeST(_tributavel);
        }
        private void CalculaICMSeST(ITributavel trib)
        {
            //FLAGS
            bool Simples = trib.EmpresaST == EmpresaST.Simples ? true : false;
            bool Isento = EhTributacaoIsentaDeIcms(_tributavel.EhCfopTributavelEmIcms, _tributavel.IcmsCST);
            bool OrgaoPublico = trib.EhClienteOrgaoPublico;
            bool Substituto = trib.EhSubstitutoTributario;
            bool CalcularST = EhParaCalcularSubstTributaria(trib.IcmsCST);
            bool ICMS_Proprio = trib.EhICMSProprio;
            bool CalculaICMS = EhParaCalcularIcms(_tributavel.EhCfopTributavelEmIcms, trib.IcmsCST);
            
            //SET-VALORES
            decimal BC = trib.BaseCalculoGeral;
            decimal ALIQ_INTERNA = trib.IcmsAliquota / 100;
            decimal ALIQ_DIFERIMENTO = trib.AliquotaDiferimento / 100;
            decimal PERC_REDUCAO_ALIQ = trib.ReducaoImposto / 100;
            decimal PERC_REDUCAO_BC = trib.IcmsReducaoBaseCalculo / 100;
            decimal ALIQ_INTERNA_REDUZIDA = ALIQ_INTERNA * (1 - PERC_REDUCAO_ALIQ);
            decimal ALIQ_EXTERNA = trib.IcmsSTAliquota / 100;
            decimal MVA = trib.IcmsSTMargemValorAgregado / 100;
            decimal PERC_REDUCAO_ST = trib.IcmsSTReducaoBaseCalculo / 100;
            decimal IPI = trib.IpiValor;
            
            //CALCULOS
            decimal BC_REDUZIDA = Math.Round(BC * (1 - PERC_REDUCAO_BC), 2);
            decimal ICMS_OP = Math.Round(BC_REDUZIDA * ALIQ_INTERNA, 2);
            decimal ICMS_OP_REDUZIDA = Math.Round(BC_REDUZIDA * ALIQ_INTERNA_REDUZIDA, 2);
            decimal BCST = Math.Round(BC_REDUZIDA + IPI + (BC + IPI) * MVA, 2);
            if (PERC_REDUCAO_ST > 0)
                BCST = BCST - (BCST * PERC_REDUCAO_ST);

            decimal ICMS_ST = Math.Round(((BCST * ALIQ_EXTERNA) - ICMS_OP)/* * (1 - PERC_REDUCAO_ST)*/, 2);
            
            //SET-TRIBUTAVEL
            trib.IcmsAliquota = ALIQ_INTERNA * 100;
            trib.IcmsValorRetido = ICMS_ST;
            trib.IcmsSTBaseCalculo = BCST;
            trib.IcmsBaseCalculo = BC_REDUZIDA;
            trib.IcmsValor = ICMS_OP_REDUZIDA;
            trib.IcmsValorDiferencaComReducao = ICMS_OP_REDUZIDA;

            trib.ValorIcmsOp = ICMS_OP;
            trib.ValorIcmsDiferimento = (ICMS_OP * ALIQ_DIFERIMENTO);

            if (trib.IcmsCST == "20" || trib.IcmsCST == "51" || trib.IcmsCST == "40")
            {
                //trib.IcmsValor = ICMS_OP - trib.ValorIcmsDiferimento;
                //trib.ValorICMSDesoneracao = Math.Round((BC_REDUZIDA * (1 - (ALIQ_INTERNA * (1 - PERC_REDUCAO_BC))) / (1 - ALIQ_INTERNA) - BC_REDUZIDA), 2);
                //Valor do ICMSdesonerado = Preço na Nota Fiscal *(1 - (Alíquota * (1 - Percentual de redução da BC))) / (1 - Alíquota) - Preço na NotaFiscal

                // Atualizado regra de ICMS desonerado 05/07/2023
                trib.ValorICMSDesoneracao = BC * (1 - (ALIQ_INTERNA * (1 - PERC_REDUCAO_BC))) / (1 - ALIQ_INTERNA) - BC;
                trib.ValorICMSDesoneracao = Math.Round(trib.ValorICMSDesoneracao, 2);
            }

            //NORMAL	
            if (!Simples)
            {
                trib.IcmsValorDiferencaComReducao = BC;
            }
            if (Simples || OrgaoPublico)
            {
                if (trib.IcmsCST != "900")
                {
                    trib.IcmsAliquota = 0;
                    trib.IcmsBaseCalculo = 0;
                    trib.IcmsReducaoBaseCalculo = 0;
                    trib.IcmsValor = 0;
                    trib.IcmsValorIsento = 0;
                    trib.IcmsValorOutros = BC;
                    if (Simples && ICMS_Proprio)
                    {
                        Isento = false;
                    }
                    if (Isento)
                    {
                        trib.IcmsValorIsento = 0;
                        trib.IcmsValorOutros = BC;
                        trib.IcmsValorRetido = 0;
                    }
                }
            }

            //TEXTO-LEI
            if (!String.IsNullOrEmpty(trib.IcmsTextoLei))
            {
                if (trib.IcmsTextoLei.IndexOf("*valor") > -1)
                    trib.IcmsTextoLei = trib.IcmsTextoLei.Replace("*valor", String.Format("{0:C2}", trib.IcmsValorDiferencaComReducao));
                else
                    trib.IcmsTextoLei = trib.IcmsTextoLei;
            }
            if (!CalculaICMS || ICMS_OP_REDUZIDA <= 0)
            {
                trib.IcmsAliquota = 0;
                trib.IcmsBaseCalculo = 0;
                trib.IcmsValor = 0;
                trib.IcmsValorDiferencaComReducao = 0;
            }
            if (!CalcularST || ICMS_ST <= 0)
            {
                trib.IcmsValorRetido = 0;
                trib.IcmsSTBaseCalculo = 0;
                trib.IcmsSTAliquota = 0;
                trib.IcmsSTMargemValorAgregado = 0;
            }
            if (_tributavel.Filial.bSTSomenteRevenda)
            {
                if (!_tributavel.Pessoa.bSTRevenda)
                {
                    trib.IcmsValorRetido = 0;
                    trib.IcmsSTBaseCalculo = 0;
                    trib.IcmsSTAliquota = 0;
                    trib.IcmsSTMargemValorAgregado = 0;
                }
            }
        }
        public void CalculaFCP(ITributavel trib)
        {
            if (trib.FCPAliquota <= 0) return;
            if (trib.IcmsCST == "101" || trib.IcmsCST == "102" || trib.IcmsCST == "300" || trib.IcmsCST == "400") return;
            if ((trib.IcmsCST == "500" || trib.IcmsCST == "900") && trib.IcmsSTAliquota == 0) return;

            if (trib.IcmsCST == "60")
            {
                var baseCalculo = trib.BaseCalculoGeral == 0 ? trib.PrecoProduto : trib.BaseCalculoGeral;
                trib.FCPBaseClaculoST = baseCalculo;
                trib.FCPAliquotaST = trib.FCPAliquota;
                trib.FCPValorST = trib.FCPBaseClaculoST * (trib.FCPAliquotaST / 100);
            }
            else
            {
                trib.FCPBaseClaculo = trib.BaseCalculoGeral;
                trib.FCPAliquota = trib.FCPAliquota;
                trib.FCPValor = trib.FCPBaseClaculo * (trib.FCPAliquota / 100);
            }
        }
    }

}
