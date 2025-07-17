using System;

using static cwkGestao.Modelo.ParcelaBase;

namespace cwkGestao.Modelo
{
    public abstract class ParcelaBase : ModeloBase
    {
        [InformacaoExibicao(0, "Parcela", 57)]
        public virtual int Parcela { get; set; }
        public virtual bool BEntrada { get; set; }
        [InformacaoExibicao(2, "Tipo Dcto", 96)]
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual Portador Portador { get; set; }
        public virtual Banco Banco { get; set; }
        [InformacaoExibicao(1, "Vencimento", 77)]
        public virtual DateTime Vencimento { get; set; }
        [InformacaoExibicao(3, "Valor", 109, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        public virtual bool BAlterado { get; set; }
        public virtual string NumBanco { get; set; }
        public virtual string NumAgencia { get; set; }
        public virtual string NumContaCorrente { get; set; }
        public virtual string NumCheque { get; set; }
        public virtual string Emitente { get; set; }
        public virtual string CpfCnpj { get; set; }
        public virtual string NomeBanco { get; set; }

        public virtual DateTime Previsao { get; set; }

        private string _TipoFormaPagamento;
        public virtual string TipoFormaPagamento
        {
            get
            {
                return _TipoFormaPagamento;
            }
            set
            {
                _TipoFormaPagamento = value;

                switch (_TipoFormaPagamento)
                {
                    case "01":
                        _FormaPagamento = EnumFormaPagamento.Dinheiro;
                        break;
                    case "02":
                        _FormaPagamento = EnumFormaPagamento.Cheque;
                        break;
                    case "03":
                        _FormaPagamento = EnumFormaPagamento.CartaoCredito;
                        break;
                    case "04":
                        _FormaPagamento = EnumFormaPagamento.CartaoDebito;
                        break;
                    case "05":
                        _FormaPagamento = EnumFormaPagamento.CreditoLoja;
                        break;
                    case "10":
                        _FormaPagamento = EnumFormaPagamento.ValeAlimentacao;
                        break;
                    case "11":
                        _FormaPagamento = EnumFormaPagamento.ValeRefeicao;
                        break;
                    case "12":
                        _FormaPagamento = EnumFormaPagamento.ValePresente;
                        break;
                    case "13":
                        _FormaPagamento = EnumFormaPagamento.ValeCombustivel;
                        break;
                    case "14":
                        _FormaPagamento = EnumFormaPagamento.DuplicataMercantil;
                        break;
                    case "15":
                        _FormaPagamento = EnumFormaPagamento.BoletoBancario;
                        break;
                    case "16":
                        _FormaPagamento = EnumFormaPagamento.DepositoBancario;
                        break;
                    case "17":
                        _FormaPagamento = EnumFormaPagamento.PagamentoInstantaneoPIX;
                        break;
                    case "18":
                        _FormaPagamento = EnumFormaPagamento.Transferenciabancaria;
                        break;
                    case "19":
                        _FormaPagamento = EnumFormaPagamento.Programadefidelidade;
                        break;
                    case "20":
                        _FormaPagamento = EnumFormaPagamento.PagamentoInstantaneoPixEstatico;
                        break;
                    case "21":
                        _FormaPagamento = EnumFormaPagamento.CreditoEmLoja;
                        break;
                    case "22":
                        _FormaPagamento = EnumFormaPagamento.PagamentoEletronicoNaoInformado;
                        break;
                    case "90":
                        _FormaPagamento = EnumFormaPagamento.SemPagamento;
                        break;
                    default:
                        _FormaPagamento = EnumFormaPagamento.Outro;
                        break;
                }
            }
        }

        private EnumFormaPagamento _FormaPagamento;
        public virtual EnumFormaPagamento FormaPagamento
        {
            get
            {
                return _FormaPagamento;
            }
            set
            {
                _FormaPagamento = value;
                switch (_FormaPagamento)
                {
                    case EnumFormaPagamento.Dinheiro:
                        _TipoFormaPagamento = "01";
                        break;
                    case EnumFormaPagamento.Cheque:
                        _TipoFormaPagamento = "02";
                        break;
                    case EnumFormaPagamento.CartaoCredito:
                        _TipoFormaPagamento = "03";
                        break;
                    case EnumFormaPagamento.CartaoDebito:
                        _TipoFormaPagamento = "04";
                        break;
                    case EnumFormaPagamento.CreditoLoja:
                        _TipoFormaPagamento = "05";
                        break;
                    case EnumFormaPagamento.ValeAlimentacao:
                        _TipoFormaPagamento = "10";
                        break;
                    case EnumFormaPagamento.ValeRefeicao:
                        _TipoFormaPagamento = "11";
                        break;
                    case EnumFormaPagamento.ValePresente:
                        _TipoFormaPagamento = "12";
                        break;
                    case EnumFormaPagamento.ValeCombustivel:
                        _TipoFormaPagamento = "13";
                        break;
                    case EnumFormaPagamento.DuplicataMercantil:
                        _TipoFormaPagamento = "14";
                        break;
                    case EnumFormaPagamento.BoletoBancario:
                        _TipoFormaPagamento = "15";
                        break;
                    case EnumFormaPagamento.DepositoBancario:
                        _TipoFormaPagamento = "16";
                        break;
                    case EnumFormaPagamento.PagamentoInstantaneoPIX:
                        _TipoFormaPagamento = "17";
                        break;
                    case EnumFormaPagamento.Transferenciabancaria:
                        _TipoFormaPagamento = "18";
                        break;
                    case EnumFormaPagamento.Programadefidelidade:
                        _TipoFormaPagamento = "19";
                        break;
                    case EnumFormaPagamento.PagamentoInstantaneoPixEstatico:
                        _TipoFormaPagamento = "20";
                        break;
                    case EnumFormaPagamento.CreditoEmLoja:
                        _TipoFormaPagamento = "21";
                        break;
                    case EnumFormaPagamento.PagamentoEletronicoNaoInformado:
                        _TipoFormaPagamento = "22";
                        break;
                    case EnumFormaPagamento.SemPagamento:
                        _TipoFormaPagamento = "90";
                        break;
                    case EnumFormaPagamento.Outro:
                        _TipoFormaPagamento = "99";
                        break;
                    default:
                        _TipoFormaPagamento = "99";
                        break;
                }
            }
        }

        public enum EnumFormaPagamento
        {
            //fpDinheiro = 1,
            //fpCheque = 2,
            //fpCartaoCredito = 3,
            //fpCartaoDebito = 4,
            //fpCreditoLoja = 5,
            //fpValeAlimentacao = 10,
            //fpValeRefeicao = 11,
            //fpValePresente = 12,
            //fpValeCombustivel = 13,
            //fpDuplicataMercantil = 14,
            //fpBoletoBancario = 15,
            //fpSemPagamento = 90,
            //fpOutro = 99

            Dinheiro = 1,
            Cheque = 2,
            CartaoCredito = 3,
            CartaoDebito = 4,
            CreditoLoja = 5,
            ValeAlimentacao = 10,
            ValeRefeicao = 11,
            ValePresente = 12,
            ValeCombustivel = 13,
            DuplicataMercantil = 14,
            BoletoBancario = 15,
            DepositoBancario = 16,
            PagamentoInstantaneoPIX = 17,
            Transferenciabancaria = 18,
            Programadefidelidade = 19,
            PagamentoInstantaneoPixEstatico = 20,
            CreditoEmLoja = 21, 
            PagamentoEletronicoNaoInformado = 22,
            SemPagamento = 90,
            Outro = 99
        }

        public virtual string DescFormaPagamentoCompleta(EnumFormaPagamento _formaPgto)
        {
            switch (_formaPgto)
            {
                case EnumFormaPagamento.Dinheiro:
                    return "01 - Dinheiro";
                case EnumFormaPagamento.Cheque:
                    return "02 - Cheque";
                case EnumFormaPagamento.CartaoCredito:
                    return "03 - Cartão de Crédito";
                case EnumFormaPagamento.CartaoDebito:
                    return "04 - Cartão de Débito";
                case EnumFormaPagamento.CreditoLoja:
                    return "05 - Cartão da Loja (Private Label)";
                case EnumFormaPagamento.ValeAlimentacao:
                    return "10 - Vale Alimentação";
                case EnumFormaPagamento.ValeRefeicao:
                    return "11 - Vale Refeição";
                case EnumFormaPagamento.ValePresente:
                    return "12 - Vale Presente";
                case EnumFormaPagamento.ValeCombustivel:
                    return "13 - Vale Combustível";
                case EnumFormaPagamento.DuplicataMercantil:
                    return "14 - Duplicata Mercantil";
                case EnumFormaPagamento.BoletoBancario:
                    return "15 - Boleto Bancário";
                case EnumFormaPagamento.DepositoBancario:
                    return "16 - Depósito Bancário";
                case EnumFormaPagamento.PagamentoInstantaneoPIX:
                    return "17 - Pagamento Instantâneo (PIX) - Dinâmico";
                case EnumFormaPagamento.Transferenciabancaria :
                    return "18 - Transferência bancária, Carteira Digital";
                case EnumFormaPagamento.Programadefidelidade:
                    return "19 - Programa de fidelidade, Cashback, Crédito Virtual";
                case EnumFormaPagamento.PagamentoInstantaneoPixEstatico:
                    return "20 - Pagamento Instantâneo (PIX) - Estático";
                case EnumFormaPagamento.CreditoEmLoja:
                    return "21 - Crédito em Loja";
                case EnumFormaPagamento.PagamentoEletronicoNaoInformado:
                    return "22 - Pagamento Eletrônico não Informado - falha de hardware do sistema emissor";
                case EnumFormaPagamento.SemPagamento:
                    return "90 - Sem pagamento";
                case EnumFormaPagamento.Outro:
                    return "99 - Outros";
                default:
                    return "Não Cadastrado";
            }
        }

        public override int ID { get; set; }
    }

    public class ConversorFormaPagamento
    {
        public static EnumFormaPagamento GetFormaPagamento(string Codigo)
        {
            switch (Convert.ToInt32(Codigo))
            {
                case 1:
                    return EnumFormaPagamento.Dinheiro;
                case 2:
                    return EnumFormaPagamento.Cheque;
                case 3:
                    return EnumFormaPagamento.CartaoCredito;
                case 4:
                    return EnumFormaPagamento.CartaoDebito;
                case 5:
                    return EnumFormaPagamento.CreditoLoja;
                case 10:
                    return EnumFormaPagamento.ValeAlimentacao;
                case 11:
                    return EnumFormaPagamento.ValeRefeicao;
                case 12:
                    return EnumFormaPagamento.ValePresente;
                case 13:
                    return EnumFormaPagamento.ValeCombustivel;
                case 14:
                    return EnumFormaPagamento.DuplicataMercantil;
                case 15:
                    return EnumFormaPagamento.BoletoBancario;
                case 16:
                    return EnumFormaPagamento.DepositoBancario;
                case 17:
                    return EnumFormaPagamento.PagamentoInstantaneoPIX;
                case 18:
                    return EnumFormaPagamento.Transferenciabancaria;
                case 19:
                    return EnumFormaPagamento.Programadefidelidade;
                case 20:
                    return EnumFormaPagamento.PagamentoInstantaneoPixEstatico;
                case 21:
                    return EnumFormaPagamento.CreditoEmLoja;
                case 22:
                    return EnumFormaPagamento.PagamentoEletronicoNaoInformado;
                case 90:
                    return EnumFormaPagamento.SemPagamento;
                default:
                    return EnumFormaPagamento.Outro;
            }
        }

        public static string GetFormaPagamento(EnumFormaPagamento Enumeracao)
        {
            switch (Enumeracao)
            {
                case EnumFormaPagamento.Dinheiro: return "01";
                case EnumFormaPagamento.Cheque: return "02";
                case EnumFormaPagamento.CartaoCredito: return "03";
                case EnumFormaPagamento.CartaoDebito: return "04";
                case EnumFormaPagamento.CreditoLoja: return "05";
                case EnumFormaPagamento.ValeAlimentacao: return "10";
                case EnumFormaPagamento.ValeRefeicao: return "11";
                case EnumFormaPagamento.ValePresente: return "12";
                case EnumFormaPagamento.ValeCombustivel: return "13";
                case EnumFormaPagamento.DuplicataMercantil: return "14";
                case EnumFormaPagamento.BoletoBancario: return "15";
                case EnumFormaPagamento.DepositoBancario: return "16";
                case EnumFormaPagamento.PagamentoInstantaneoPIX: return "17";
                case EnumFormaPagamento.Transferenciabancaria: return "18";
                case EnumFormaPagamento.Programadefidelidade: return "19";
                case EnumFormaPagamento.PagamentoInstantaneoPixEstatico: return "20";
                case EnumFormaPagamento.CreditoEmLoja: return "21";
                case EnumFormaPagamento.PagamentoEletronicoNaoInformado: return "22";
                case EnumFormaPagamento.SemPagamento: return "90";
                case EnumFormaPagamento.Outro: return "99";
                default: return "99";
            }
        }
    }
}