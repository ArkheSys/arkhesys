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
            //if (EhParaCalcularPis(_tributavel.PisCST))
            //{
            //    _tributavel.PisBaseCalculo = _tributavel.BaseCalculoGeral;
            //    _tributavel.PisValor = CalcularValorAliquota(_tributavel.PisBaseCalculo, _tributavel.PisAliquota);
            //}
            //else
            //{
                _tributavel.PisAliquota = 0;
                _tributavel.PisBaseCalculo = 0;
                _tributavel.PisValor = 0;
            //}
        }

        public void CalculaIpi()
        {
            //if (EhParaCalcularIpi(_tributavel.IpiCST))
            //{
            //    _tributavel.IpiBaseCalculo = _tributavel.BaseCalculoGeral;
            //    _tributavel.IpiValor = CalcularValorAliquota(_tributavel.IpiBaseCalculo, _tributavel.IpiAliquota);

            //}
            //else
            //{
                _tributavel.IpiAliquota = 0;
                _tributavel.IpiBaseCalculo = 0;
                _tributavel.IpiValor = 0;
            //}
        }

        public void CalculaCofins()
        {
            //if (EhParaCalcularCofins(_tributavel.CofinsCST))
            //{
            //    _tributavel.CofinsBaseCalculo = _tributavel.BaseCalculoGeral;
            //    _tributavel.CofinsValor = CalcularValorAliquota(_tributavel.CofinsBaseCalculo, _tributavel.CofinsAliquota);
            //}
            //else
            //{
                _tributavel.CofinsAliquota = 0;
                _tributavel.CofinsBaseCalculo = 0;
                _tributavel.CofinsValor = 0;
            //}
        }

        public void CalculaCredito()
        {
            // 1. Condição principal: Só faz algo se for Simples Nacional
            /*if (_tributavel.EmpresaST == EmpresaST.Simples)
            {
                // 2. Verifica se o CSOSN é um que permite crédito (101 ou 201)
                if (_tributavel.IcmsCST == "101" || _tributavel.IcmsCST == "201")
                {
                    decimal baseCalculo = CalcularBaseCalculoIcmsProprio(_tributavel);

                    // 3. Lógica final usando os NOMES CORRETOS
                    if (_tributavel.PCredSN_N29 > 0)
                    {
                        // Alíquota foi informada manualmente, apenas calcula o valor.
                        _tributavel.VCredICMSSN_N30 = CalcularValorIcmsProprio(baseCalculo, _tributavel.PCredSN_N29);
                    }
                    else
                    {
                        // Ninguém informou, faz o cálculo automático buscando da Filial.
                        _tributavel.PCredSN_N29 = _tributavel.Filial.AliqSimplesSubst;
                        _tributavel.VCredICMSSN_N30 = CalcularValorIcmsProprio(baseCalculo, _tributavel.PCredSN_N29);
                    }
                }
            }*/
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

        #region Métodos de Cálculo de ICMS (Estrutura Nova)

        public void CalculaIcms()
        {
            CalculaICMSeST(_tributavel);
        }

        // 1. MÉTODO PRINCIPAL (SUBSTITUA O SEU MÉTODO ATUAL POR ESTE)
        // Este método agora atua como um "diretor", zerando os campos e chamando o cálculo correto para o regime tributário.
        private void CalculaICMSeST(ITributavel trib)
            {
                // Passo 1: Garante que todos os campos comecem zerados a cada novo cálculo.
                ZerarCamposIcms(trib);

                // Passo 2: Verifica o regime tributário da empresa e chama o método de cálculo apropriado.
                bool ehSimplesNacional = trib.EmpresaST == EmpresaST.Simples;

                if (ehSimplesNacional)
                {
                    CalcularIcms_SimplesNacional(trib);
                }
                else
                {
                    CalcularIcms_RegimeNormal(trib);
                }

                // Passo 3 (Opcional): Lógicas comuns que rodam no final, como o Texto da Lei.
                // ... seu código de Texto-Lei pode vir aqui se necessário ...
            }

        // 2. MÉTODO PARA REGIME NORMAL (CST)
        // Contém todas as regras para empresas de Lucro Presumido/Real.
        private void CalcularIcms_RegimeNormal(ITributavel trib)
        {
            // Valores base para os cálculos
            decimal baseCalculo = CalcularBaseCalculoIcmsProprio(trib);
            decimal valorIpi = trib.IpiValor;

            switch (trib.IcmsCST)
            {
                case "00": // Tributada integralmente
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    break;

                case "10": // Tributada com cobrança do ICMS por ST
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    CalcularValoresST(trib, baseCalculo, valorIpi, trib.IcmsValor);
                    break;

                case "20": // Com redução de base de cálculo
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    // Lógica de desoneração, se aplicável, pode ser chamada aqui.
                    break;

                case "30": // Isenta ou não tributada e com cobrança do ICMS por ST
                           // Não calcula ICMS próprio, apenas ST.
                    CalcularValoresST(trib, baseCalculo, valorIpi, 0);
                    break;

                case "51": // Diferimento
                    trib.IcmsBaseCalculo = baseCalculo;
                    decimal icmsOperacao = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    trib.ValorIcmsOp = icmsOperacao;
                    trib.AliquotaDiferimento = trib.AliquotaDiferimento; // Já deve ter sido carregada
                    trib.ValorIcmsDiferimento = Math.Round(icmsOperacao * (trib.AliquotaDiferimento / 100), 2);
                    trib.IcmsValor = icmsOperacao - trib.ValorIcmsDiferimento;
                    break;

                case "70": // Com redução de BC e cobrança do ICMS por ST
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    CalcularValoresST(trib, baseCalculo, valorIpi, trib.IcmsValor);
                    break;

                case "90": // Outras
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    CalcularValoresST(trib, baseCalculo, valorIpi, trib.IcmsValor);
                    break;

                // Para estes CSTs, o ICMS é sempre zero. Como zeramos no início, não fazemos nada.
                case "40": // Isenta
                case "41": // Não tributada
                case "50": // Suspensão
                case "60": // ICMS cobrado anteriormente por ST
                    break;
            }
        }

        // 3. MÉTODO PARA SIMPLES NACIONAL (CSOSN)
        // Contém todas as regras para empresas optantes pelo Simples Nacional
        private void CalcularIcms_SimplesNacional(ITributavel trib)
        {
            decimal baseCalculo = CalcularBaseCalculoIcmsProprio(trib);
            decimal valorIpi = trib.IpiValor;

            // Bloco de lógica para o crédito do Simples Nacional
            // Será usado nos casos 101 e 201
            Action calcularCreditoSN = () => {
                // Respeita a alíquota de crédito se ela foi digitada manualmente na tela
                if (trib.CreditoPercentual > 0)
                {
                    trib.CreditoValor = CalcularValorIcmsProprio(baseCalculo, trib.CreditoPercentual);
                }
                else // Senão, faz o cálculo automático buscando da Filial
                {
                    trib.CreditoPercentual = trib.Filial.AliqSimplesSubst;
                    trib.CreditoValor = CalcularValorIcmsProprio(baseCalculo, trib.CreditoPercentual);
                }
            };

            switch (trib.IcmsCST) // No Simples, IcmsCST armazena o CSOSN
            {
                case "101":
                    // 1. Calcula o ICMS próprio para uso interno
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);

                    // 2. Executa a lógica de cálculo de crédito
                    calcularCreditoSN();
                    break;

                case "201":
                    // 1. Calcula o ICMS próprio para uso interno
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);

                    // 2. Executa a lógica de cálculo de crédito
                    calcularCreditoSN();

                    // 3. Calcula o ICMS-ST
                    CalcularValoresST(trib, baseCalculo, valorIpi, trib.IcmsValor);
                    break;

                case "202":
                case "203":
                    CalcularValoresST(trib, baseCalculo, valorIpi, 0);
                    break;

                case "900":
                    trib.IcmsBaseCalculo = baseCalculo;
                    trib.IcmsValor = CalcularValorIcmsProprio(baseCalculo, trib.IcmsAliquota);
                    CalcularValoresST(trib, baseCalculo, valorIpi, trib.IcmsValor);
                    break;

                case "102":
                case "103":
                case "300":
                case "400":
                case "500":
                    break;
            }
        }

        #endregion

        #region Métodos Auxiliares (Helpers)

        // HELPER 1: Zera todos os campos de ICMS para garantir um cálculo limpo.
        private void ZerarCamposIcms(ITributavel trib)
        {
           // trib.IcmsAliquota = 0;
            trib.IcmsBaseCalculo = 0;
            trib.IcmsValor = 0;
            trib.IcmsReducaoBaseCalculo = 0;

            trib.IcmsSTBaseCalculo = 0;
            trib.IcmsValorRetido = 0;

            trib.ValorIcmsOp = 0;
            trib.ValorIcmsDiferimento = 0;
            trib.ValorICMSDesoneracao = 0;

            // Usando os nomes da sua interface
            //trib.PCredSN_N29 = 0;
            //trib.VCredICMSSN_N30 = 0;
        }

        // HELPER 2: Calcula a Base de Cálculo do ICMS Próprio, já aplicando a redução.
        private decimal CalcularBaseCalculoIcmsProprio(ITributavel trib)
        {
            decimal baseCheia = trib.BaseCalculoGeral;
            decimal percReducao = trib.IcmsReducaoBaseCalculo / 100;
            return Math.Round(baseCheia * (1 - percReducao), 2);
        }

        // HELPER 3: Calcula o Valor do ICMS Próprio.
        private decimal CalcularValorIcmsProprio(decimal baseCalculo, decimal aliquota)
        {
            decimal aliquotaDecimal = aliquota / 100;
            return Math.Round(baseCalculo * aliquotaDecimal, 2);
        }

        // HELPER 4: Calcula todos os valores relacionados à Substituição Tributária (ST).
        private void CalcularValoresST(ITributavel trib, decimal baseCalculoIcmsProprio, decimal valorIpi, decimal valorIcmsProprio)
        {
            decimal mva = trib.IcmsSTMargemValorAgregado / 100;
            decimal aliquotaExterna = trib.IcmsSTAliquota / 100;
            decimal percReducaoST = trib.IcmsSTReducaoBaseCalculo / 100;

            // Fórmula da base de cálculo do ST
            decimal baseCalculoST = Math.Round((baseCalculoIcmsProprio + valorIpi) * (1 + mva), 2);

            // Aplica a redução da base do ST, se houver
            if (percReducaoST > 0)
            {
                baseCalculoST = Math.Round(baseCalculoST * (1 - percReducaoST), 2);
            }

            // Fórmula do valor do ICMS ST
            decimal valorIcmsST = Math.Round((baseCalculoST * aliquotaExterna) - valorIcmsProprio, 2);

            if (valorIcmsST > 0)
            {
                trib.IcmsSTBaseCalculo = baseCalculoST;
                trib.IcmsValorRetido = valorIcmsST; // Usando o campo 'IcmsValorRetido' para o valor do ST
            }
        }

        #endregion
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
