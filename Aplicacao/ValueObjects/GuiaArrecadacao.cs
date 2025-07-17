using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;

using System;
using System.Linq;

namespace Aplicacao.ValueObjects
{
    public class GuiaArrecadacao
    {
        #region Guias

        // Identificação do Produto = 8
        // Identificação do Seguimento = 4 (Telecomunicação)
        // Identificação Valor Referente = 6 (valor cobrado em reais modulo 10)
        // Valor Referente = 10,25 (Valor efetivo)
        // Identificação Empresa Orgao = 0508 (Codigo ou cnpj)
        // Numero Documento = 012345678
        // Data Vencimento = 22/12/2022 

        #endregion

        #region Propriedades / Fields

        private string _codigoLinhaDigitavel;
        private string _campoLivre;

        #endregion

        #region Construtor

        public GuiaArrecadacao(int identificacaoProduto, int identificacaoSeguimento, int identificacaoValorReferente, decimal valorReferente, string identificacaoEmpresa, string numeroDocumento, DateTime dataVencimento)
        {
            var valor = valorReferente.ToString("n2").OnlyNumbers().PadLeft(11, '0');
            var campoLivre = dataVencimento.ToString("yyyyMMdd");
            campoLivre += numeroDocumento.OnlyNumbers().PadLeft(25 - campoLivre.Length, '0');

            _campoLivre = campoLivre;
            _codigoLinhaDigitavel = $"{identificacaoProduto}" +
                                    $"{identificacaoSeguimento}" +
                                    $"{identificacaoValorReferente}" +
                                    $"{valor}" +
                                    $"{identificacaoEmpresa}" +
                                    $"{campoLivre}";
        }

        #endregion

        #region Métodos Publicos

        public string ObterLinhaDigitavel() => ObterCalculoDac(_codigoLinhaDigitavel, false);
        public string ObterLinhaDigitavelComDv() => ObterCalculoDac(_codigoLinhaDigitavel, true);
        public string GetCampoLivre() => _campoLivre;

        #endregion

        #region Métodos Privados

        private string ObterCalculoDac(string codigoLinhaDigitavel, bool comDv)
        {
            var tempCodigo = "";
            for (int i = 0; i < codigoLinhaDigitavel.Length; i++)
            {
                if (i == 3) tempCodigo += ObterDigitoVerificadorGeral(codigoLinhaDigitavel);
                tempCodigo += codigoLinhaDigitavel[i].ToString();
            }

            if (comDv)
            {
                var linhaDigitavel = "";
                for (int i = 0; i < tempCodigo.Length; i++)
                {
                    if (i == 11 || i == 22 || i == 33) linhaDigitavel += $"-{ObterDigitoVerificadorGeral(tempCodigo.Substring(i - 11, 11))} ";
                    linhaDigitavel += tempCodigo[i];
                }

                return linhaDigitavel += $"-{ObterDigitoVerificadorGeral(tempCodigo.Substring(33, 11))}";
            }
            else
            {
                var linhaDigitavel = "";
                for (int i = 0; i < tempCodigo.Length; i++)
                    linhaDigitavel += tempCodigo[i];

                return linhaDigitavel;
            }
        }

        private string ObterDigitoVerificadorGeral(string codigo)
        {
            int peso = 2;
            string codigoCalculo = "";

            for (int i = 0; i < codigo.Length; i++)
            {
                codigoCalculo += codigo[i].ToString().ToInt() * peso;
                peso = peso == 2 ? 1 : 2;
            }

            var soma = codigoCalculo.ToArray().Sum(x => x.ToString().ToInt());
            return soma % 10 == 0 ? "0" : (10 - (soma % 10)).ToString();
        }

        #endregion

        #region Método Estático

        public static string Obter(Tel_FaturaD8 fatura, bool ComDv)
        {
            const int identificacaoProduto = 8;
            const int identificacaoSeguimento = 4;
            const int identificacaoValorReferente = 6;
            const string identificacaoEmpresa = "0508";

            var guia = new GuiaArrecadacao(identificacaoProduto, identificacaoSeguimento, identificacaoValorReferente, fatura.Valor, identificacaoEmpresa, fatura.NumeroDocumento, fatura.DataVencimento);
            if (ComDv)
                return guia.ObterLinhaDigitavelComDv(); 
            else
                return guia.ObterLinhaDigitavel();
        }

        public static string ObterCampoLivre(Tel_FaturaD8 fatura)
        {
            const int identificacaoProduto = 8;
            const int identificacaoSeguimento = 4;
            const int identificacaoValorReferente = 6;
            const string identificacaoEmpresa = "0508";

            return new GuiaArrecadacao(identificacaoProduto, identificacaoSeguimento, identificacaoValorReferente, fatura.Valor, identificacaoEmpresa, fatura.NumeroDocumento, fatura.DataVencimento)
                .GetCampoLivre();
        }

        #endregion
    }
}
