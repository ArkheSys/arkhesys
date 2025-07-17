using System;
using DBUtils.Classes;

using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Helpers;

namespace cwkGestao.Integracao.ACBr.Boleto
{
    public class Retorno
    {
        public List<DadosRetorno> PreencheDadosRetorno(IniFile arq)
        {
            List<DadosRetorno> Retorno = new List<DadosRetorno>();
            foreach (var section in arq.GetSections("Titulo"))
            {
                DadosRetorno dadosRetono = new DadosRetorno();
                dadosRetono.Agencia = arq.GetValue("CONTA", "Agencia", "");
                dadosRetono.Banco = arq.GetValue("BANCO", "Numero", "");
                dadosRetono.Carteira = arq.GetValue(section, "Carteira", "");
                dadosRetono.CodigoCedente = arq.GetValue("CEDENTE", "CodigoCedente", "");
                dadosRetono.CodigoOcorrencia = GetCodigoRetorno(arq.GetValue(section, "CodTipoOcorrencia", ""));
                dadosRetono.ContaCorrente = arq.GetValue("CONTA", "Conta", ""); ;
                dadosRetono.DataCredito = arq.GetValue(section, "DataCredito", "");
                dadosRetono.DataOcorrencia = arq.GetValue(section, "DataOcorrencia", "");

                dadosRetono.NossoNumero = arq.GetValue(section, "NossoNumero", "");

                //dadosRetono.NumeroControle = item.NumeroControle;
                dadosRetono.NumeroDocumento = arq.GetValue(section, "NumeroDocumento", "");
                dadosRetono.Pagamento = !string.IsNullOrEmpty(arq.GetValue(section, "DataBaixa", ""));
                dadosRetono.ValorCredito = Convert.ToDecimal(arq.GetValue(section, "ValorOutrosCreditos", "0"));
                dadosRetono.ValorDesconto = Convert.ToDecimal(arq.GetValue(section, "ValorDesconto", "0"));
                dadosRetono.ValorJurosPago = Convert.ToDecimal(arq.GetValue(section, "ValorMoraJuros", "0"));
                //dadosRetono.ValorMultaPaga = Convert.ToDecimal(arq.GetValue(section, "ValorDespesaCobranca", "0"));
                dadosRetono.ValorOutrosAcrescimos = Convert.ToDecimal(arq.GetValue(section, "ValorOutrasDespesas", "0"));
                dadosRetono.ValorPago = Convert.ToDecimal(arq.GetValue(section, "ValorRecebido", "0"));
                dadosRetono.ValorTaxaCobranca = Convert.ToDecimal(arq.GetValue(section, "ValorDespesaCobranca", "0"));
                dadosRetono.ValorIOF = Convert.ToDecimal(arq.GetValue(section, "ValorIOF", "0"));

                dadosRetono.MotivoOcorrencia = new Dictionary<string, string>();
                dadosRetono.MotivoOcorrencia.Add("CodTipoOcorrencia", GetCodigoRetorno(arq.GetValue(section, "CodTipoOcorrencia", "")));
                dadosRetono.MotivoOcorrencia.Add("DescricaoTipoOcorrencia", arq.GetValue(section, "DescricaoTipoOcorrencia", ""));
                dadosRetono.MotivoOcorrencia.Add("MotivoRejeicao7", arq.GetValue(section, "MotivoRejeicao7", ""));

                Retorno.Add(dadosRetono);
            }
            return Retorno;
        }

        private string GetCodigoRetorno(string TipoOcorrencia)
        {
            switch (TipoOcorrencia)
            {
                case "toRetornoRegistroConfirmado": return "02";
                case "toRetornoComandoRecusado": return "03";
                case "toRetornoLiquidadoSemRegistro": return "05";
                case "toRetornoLiquidado": return "06";
                case "toRetornoLiquidadoPorConta": return "07";
                case "toRetornoBaixado": return "09";
                case "toRetornoBaixaSolicitada": return "10";
                case "toRetornoTituloEmSer": return "11";
                case "toRetornoAbatimentoConcedido": return "12";
                case "toRetornoAbatimentoCancelado": return "13";
                case "toRetornoVencimentoAlterado": return "14";
                case "toRetornoLiquidadoEmCartorio": return "15";
                case "toRetornoRecebimentoInstrucaoProtestar": return "19";
                case "toRetornoDebitoEmConta": return "20";
                case "toRetornoRecebimentoInstrucaoAlterarNomeSacado": return "21";
                case "toRetornoRecebimentoInstrucaoAlterarEnderecoSacado": return "22";
                case "toRetornoEncaminhadoACartorio": return "23";
                case "toRetornoProtestoSustado": return "24";
                case "toRetornoJurosDispensados": return "25";
                case "toRetornoManutencaoTituloVencido": return "28";
                case "toRetornoDescontoConcedido": return "31";
                case "toRetornoDescontoCancelado": return "32";
                case "toRetornoAcertoControleParticipante": return "41";
                case "toRetornoTituloPagoEmCheque": return "46";
                case "toRetornoTipoCobrancaAlterado": return "72";
                case "toRetornoDespesasProtesto": return "96";
                case "toRetornoDespesasSustacaoProtesto": return "97";
                case "toRetornoDebitoCustasAntecipadas": return "98";
                default: return TipoOcorrencia;
            }
        }
    }
}