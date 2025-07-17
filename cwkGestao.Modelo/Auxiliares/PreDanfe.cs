using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Auxiliares
{
    public class PreDanfe
    {
        public PreDanfe()
        {
            DanfeItems = new List<Item>();
            DanfeParcelas = new List<Parcela>();
            CalculoImpostoBaseCalculoICMS = "";
        }

        private static readonly NumberFormatInfo formatacao = new CultureInfo("pt-BR", false).NumberFormat;
        public string Numero { get; set; }
        public string Serie { get; set; }
        private string _naturezaOperacao;
        public string NaturezaOperacao
        {
            get
            {
                switch (_naturezaOperacao) //NAO IMPLEMENTADO
                {
                    case "":
                        return "";
                    default:
                        return _naturezaOperacao;
                }
            }
            set { _naturezaOperacao = value; }
        }

        public string EmitenteNome { get; set; }
        public string EmitenteEndereco { get; set; }
        public string EmitenteComplemento { get; set; }
        public string EmitenteBairro { get; set; }
        public string EmitenteCidade { get; set; }
        public string EmitenteEstado { get; set; }
        public string EmitenteCEP { get; set; }
        public string EmitenteTelefone { get; set; }
        public string EntradaSaida { get; set; }
        public string EmitenteCNPJ { get; set; }
        public string EmitenteInscricaoEstadual { get; set; }
        public string EmitenteInscricaoEstadualSubTributaria { get; set; }
        public string DestinatarioNome { get; set; }
        public string DestinatarioEndereco { get; set; }
        public string DestinatarioMunicipio { get; set; }
        public string DestinatarioBairro { get; set; }
        public string DestinatarioCNPJ { get; set; }
        private string _destinatarioDataEmissao;
#pragma warning disable CS0169 // O campo "PreDanfe._destinatarioFone" nunca é usado
        private string _destinatarioFone;
#pragma warning restore CS0169 // O campo "PreDanfe._destinatarioFone" nunca é usado
        public string DestinatarioDataEmissao
        {
            get { return _destinatarioDataEmissao == null ? null : DateTime.Parse(_destinatarioDataEmissao).ToString("dd/MM/yyyy"); }
            set { _destinatarioDataEmissao = value; }
        }

        private string _destinatarioDataSaida;
        public string DestinatarioDataSaida
        {
            get { return _destinatarioDataSaida == null ? null : DateTime.Parse(_destinatarioDataSaida).ToString("dd/MM/yyyy"); }
            set { _destinatarioDataSaida = value; }
        }

        public string DestinatarioHoraSaida { get; set; }
        public string DestinatarioCEP { get; set; }
        private string _DestinatarioFone;
        public string DestinatarioFone
        {
            get { return _DestinatarioFone; }
            set { _DestinatarioFone = String.Format("{0:(00)0000-0000}",Convert.ToInt64(value)); }
        }
        public string DestinatarioUF { get; set; }
        public string DestinatarioInscricaoEstadual { get; set; }

        private string _calculoImpostoBaseCalculoICMS;
        public string CalculoImpostoBaseCalculoICMS
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoBaseCalculoICMS, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoBaseCalculoICMS = value; }
        }

        private string _calculoImpostoValorICMS;
        public string CalculoImpostoValorICMS
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorICMS, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorICMS = value; }
        }

        private string _calculoImpostoValorFrete;
        public string CalculoImpostoValorFrete
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorFrete, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorFrete = value; }
        }

        private string _calculoImpostoValorSeguro;
        public string CalculoImpostoValorSeguro
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorSeguro, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorSeguro = value; }
        }

        private string _calculoImpostoDesconto;
        public string CalculoImpostoDesconto
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoDesconto, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoDesconto = value; }
        }

        private string _calculoImpostoOutrasDespesas;
        public string CalculoImpostoOutrasDespesas
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoOutrasDespesas, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoOutrasDespesas = value; }
        }

        private string _calculoImpostoBaseCalculoIcmsst;
        public string CalculoImpostoBaseCalculoIcmsst
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoBaseCalculoIcmsst, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoBaseCalculoIcmsst = value; }
        }

        private string _calculoImpostoValorICMSSubstituicao;
        public string CalculoImpostoValorICMSSubstituicao
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorICMSSubstituicao, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorICMSSubstituicao = value; }
        }

        private string _calculoImpostoValorTotalProdutos;
        public string CalculoImpostoValorTotalProdutos
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorTotalProdutos, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorTotalProdutos = value; }
        }

        private string _calculoImpostoValorTotalNota;
        public string CalculoImpostoValorTotalNota
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorTotalNota, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorTotalNota = value; }
        }

        private string _calculoImpostoValorIPI;
        public string CalculoImpostoValorIPI
        {
            get { return Convert.ToDecimal("0" + _calculoImpostoValorIPI, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
            set { _calculoImpostoValorIPI = value; }
        }

        public string TransportadorRazaoSocial { get; set; }
        public string TransportadorEndereco { get; set; }
        public string TransportadorQuantidade { get; set; }
        public string TransportadorEspecie { get; set; }
        public string TransportadorMarca { get; set; }
        public string TransportadorMunicipio { get; set; }
        public string TransportadorUF { get; set; }
        public string TransportadorNumeracao { get; set; }
        private string _transportadorFretePorConta;
        public string TransportadorFretePorConta
        {
            get
            {
                switch (_transportadorFretePorConta)
                {
                    case "0":
                        return "0-Emitente";
                    case "1":
                        return "1-Destinatario";
                    case "2":
                        return "2-Terceiros";
                    case "9":
                        return "9-Sem Frete";
                    default:
                        return _transportadorFretePorConta;
                }
            }
            set { _transportadorFretePorConta = value; }
        }

        public string TransportadorCodigoAntt { get; set; }
        public string TransportadorPlacaVeiculo { get; set; }
        public string TransportadorPlacaUF { get; set; }
        public string TransportadorCNPJ { get; set; }
        public string TransportadorInscricaoEstadual { get; set; }

        private string _transportadorPesoLiquido;
        public string TransportadorPesoLiquido 
        {
            get { return Convert.ToDecimal("0" + _transportadorPesoLiquido, NumberFormatInfo.InvariantInfo).ToString("N3", formatacao); }
            set { _transportadorPesoLiquido = value; }
        }

        private string _transportadorPesoBruto;
        public string TransportadorPesoBruto 
        {
            get { return Convert.ToDecimal("0" + _transportadorPesoBruto, NumberFormatInfo.InvariantInfo).ToString("N3", formatacao); }
            set { _transportadorPesoBruto = value; }
        }

        public List<Item> DanfeItems { get; set; }
        public List<Parcela> DanfeParcelas { get; set; }

        public string InformacoesComplementares { get; set; }

        public class Item
        {
            public string CodigoProduto { get; set; }
            public string DescricaoProduto { get; set; }
            public string NCMSH { get; set; }
            public string CST 
            { 
                get; 
                set; 
            }
            public string CFOP { get; set; }
            public string Unidade { get; set; }

            private string _quantidade;
            public string Quantidade 
            {
                get { return Convert.ToDecimal("0" + _quantidade, NumberFormatInfo.InvariantInfo).ToString("N4", formatacao); }
                set { _quantidade = value; }
            }

            private string _valorUnitario;
            public string ValorUnitario
            {
                get { return Convert.ToDecimal("0" + _valorUnitario, NumberFormatInfo.InvariantInfo).ToString("N4", formatacao); }
                set { _valorUnitario = value; }
            }

            private string _valorTotal;
            public string ValorTotal
            {
                get {
                    return Convert.ToDecimal("0" + _valorTotal, NumberFormatInfo.InvariantInfo).ToString("N", formatacao);
                }
                set { _valorTotal = value; }
            }

            private string _baseCalculoICMS;
            public string BaseCalculoICMS
            {
                get { return Convert.ToDecimal("0" + _baseCalculoICMS, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _baseCalculoICMS = value; }
            }

            private string _valorICMS;
            public string ValorICMS
            {
                get { return Convert.ToDecimal("0" + _valorICMS, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _valorICMS = value; }
            }

            private string _valorIPI;
            public string ValorIPI
            {
                get { return Convert.ToDecimal("0" + _valorIPI, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _valorIPI = value; }
            }

            private string _aliquotaICMS;
            public string AliquotaICMS 
            {
                get { return Convert.ToDecimal("0" + _aliquotaICMS, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _aliquotaICMS = value; } 
            }

            private string _aliquotaIPI;
            public string AliquotaIPI
            {
                get { return Convert.ToDecimal("0" + _aliquotaIPI, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _aliquotaIPI = value; }
            }

            public string Origem { get; set; }
            public string InfAdicional { get; set; }
        }
        public class Parcela
        {
            public string Numero1 { get; set; }
            private string _dataVcto1;
            public string DataVcto1
            {
                get { return _dataVcto1 == null ? null : DateTime.Parse(_dataVcto1).ToString("dd/MM/yyyy"); }
                set { _dataVcto1 = value; }
            }

            private string _valor1;
            public string Valor1
            {
                get { return _valor1 == null ? null : Convert.ToDecimal(_valor1, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _valor1 = (String.IsNullOrEmpty(value) ? null : value); }
            }

            public string Numero2 { get; set; }
            private string _dataVcto2;
            public string DataVcto2
            {
                get { return _dataVcto2 == null ? null : DateTime.Parse(_dataVcto2).ToString("dd/MM/yyyy"); }
                set { _dataVcto2 = value; }
            }

            private string _valor2;
            public string Valor2
            {
                get { return _valor2 == null ? null : Convert.ToDecimal(_valor2, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _valor2 = (String.IsNullOrEmpty(value) ? null : value); }
            }

            public string Numero3 { get; set; }
            private string _dataVcto3;
            public string DataVcto3
            {
                get { return _dataVcto3 == null ? null : DateTime.Parse(_dataVcto3).ToString("dd/MM/yyyy"); }
                set { _dataVcto3 = value; }
            }

            private string _valor3;
            public string Valor3
            {
                get { return _valor3 == null ? null : Convert.ToDecimal(_valor3, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _valor3 = (String.IsNullOrEmpty(value) ? null : value); }
            }

            public string Numero4 { get; set; }
            private string _dataVcto4;
            public string DataVcto4
            {
                get { return _dataVcto4 == null ? null : DateTime.Parse(_dataVcto4).ToString("dd/MM/yyyy"); }
                set { _dataVcto4 = value; }
            }

            private string _valor4;
            public string Valor4
            {
                get { return _valor4 == null ? null : Convert.ToDecimal(_valor4, NumberFormatInfo.InvariantInfo).ToString("N", formatacao); }
                set { _valor4 = (String.IsNullOrEmpty(value) ? null : value); }
            }
        }
    }
}
