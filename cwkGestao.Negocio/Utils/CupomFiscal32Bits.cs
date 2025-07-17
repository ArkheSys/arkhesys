using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiscalPrinterBematech;
using System.Runtime.InteropServices;

namespace cwkGestao.Negocio.Utils
{
    public class CupomFiscal32Bits : ICupomFiscal
    {
        #region Funções de tratamento de erro
        public void Analisa_RetornoImpressora()
        {
            BemaFI32.Analisa_RetornoImpressora();
        }

        public void Analisa_iRetorno(int IRetorno)
        {
            BemaFI32.Analisa_iRetorno(IRetorno);
        }
        #endregion

        #region Funções de Inicialização
        public int Bematech_FI_AlteraSimboloMoeda(string SimboloMoeda)
        {
            return BemaFI32.Bematech_FI_AlteraSimboloMoeda(SimboloMoeda);
        }

        public int Bematech_FI_ProgramaAliquota(string Aliquota, int ICMS_ISS)
        {
            return BemaFI32.Bematech_FI_ProgramaAliquota(Aliquota, ICMS_ISS);
        }

        public int Bematech_FI_NomeiaDepartamento(int Indice, string Departamento)
        {
            return BemaFI32.Bematech_FI_NomeiaDepartamento(Indice, Departamento);
        }

        public int Bematech_FI_NomeiaTotalizadorNaoSujeitoIcms(int Indice, string Totalizador)
        {
            return BemaFI32.Bematech_FI_NomeiaTotalizadorNaoSujeitoIcms(Indice, Totalizador);
        }

        public int Bematech_FI_LinhasEntreCupons(int Linhas)
        {
            return BemaFI32.Bematech_FI_LinhasEntreCupons(Linhas);
        }

        public int Bematech_FI_EspacoEntreLinhas(int Dots)
        {
            return BemaFI32.Bematech_FI_EspacoEntreLinhas(Dots);
        }

        public int Bematech_FI_ForcaImpactoAgulhas(int ForcaImpacto)
        {
            return BemaFI32.Bematech_FI_ForcaImpactoAgulhas(ForcaImpacto);
        }

        public int Bematech_FI_ProgramaHorarioVerao()
        {
            return BemaFI32.Bematech_FI_ProgramaHorarioVerao();
        }

        public int Bematech_FI_ProgramaArredondamento()
        {
            return BemaFI32.Bematech_FI_ProgramaArredondamento();
        }

        public int Bematech_FI_ProgramaTruncamento()
        {
            return BemaFI32.Bematech_FI_ProgramaTruncamento();
        }
        #endregion

        #region Funções do Cupom Fiscal
        public int Bematech_FI_AbreCupom(string CGC_CPF)
        {
            return BemaFI32.Bematech_FI_AbreCupom(CGC_CPF);
        }

        public int Bematech_FI_VendeItem(string Codigo, string Descricao, string Aliquota, string TipoQuantidade, string Quantidade, int CasasDecimais, string ValorUnitario, string TipoDesconto, string Desconto)
        {
            return BemaFI32.Bematech_FI_VendeItem(Codigo, Descricao, Aliquota, TipoQuantidade, Quantidade, CasasDecimais, ValorUnitario, TipoDesconto, Desconto);
        }

        public int Bematech_FI_VendeItemDepartamento(string Codigo, string Descricao, string Aliquota, string ValorUnitario, string Quantidade, string Acrescimo, string Desconto, string IndiceDepartamento, string UnidadeMedida)
        {
            return BemaFI32.Bematech_FI_VendeItemDepartamento(Codigo, Descricao, Aliquota, ValorUnitario, Quantidade, Acrescimo, Desconto, IndiceDepartamento, UnidadeMedida);
        }

        public int Bematech_FI_CancelaItemAnterior()
        {
            return BemaFI32.Bematech_FI_CancelaItemAnterior();
        }

        public int Bematech_FI_CancelaItemGenerico(string NumeroItem)
        {
            return BemaFI32.Bematech_FI_CancelaItemGenerico(NumeroItem);
        }

        public int Bematech_FI_CancelaCupom()
        {
            return BemaFI32.Bematech_FI_CancelaCupom();
        }

        public int Bematech_FI_FechaCupomResumido(string FormaPagamento, string Mensagem)
        {
            return BemaFI32.Bematech_FI_FechaCupomResumido(FormaPagamento, Mensagem);
        }

        public int Bematech_FI_FechaCupom(string FormaPagamento, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem)
        {
            return BemaFI32.Bematech_FI_FechaCupom(FormaPagamento, AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto, ValorPago, Mensagem);
        }

        public int Bematech_FI_IniciaFechamentoCupom(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto)
        {
            return BemaFI32.Bematech_FI_IniciaFechamentoCupom(AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto);
        }

        public int Bematech_FI_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento)
        {
            return BemaFI32.Bematech_FI_EfetuaFormaPagamento(FormaPagamento, ValorFormaPagamento);
        }

        public int Bematech_FI_EfetuaFormaPagamentoDescricaoForma(string FormaPagamento, string ValorFormaPagamento, string Descricao)
        {
            return BemaFI32.Bematech_FI_EfetuaFormaPagamentoDescricaoForma(FormaPagamento, ValorFormaPagamento, Descricao);
        }

        public int Bematech_FI_TerminaFechamentoCupom(string Mensagem)
        {
            return BemaFI32.Bematech_FI_TerminaFechamentoCupom(Mensagem);
        }

        public int Bematech_FI_EstornoFormasPagamento(string FormaOrigem, string FormaDestino, string Valor)
        {
            return BemaFI32.Bematech_FI_EstornoFormasPagamento(FormaOrigem, FormaDestino, Valor);
        }

        public int Bematech_FI_AumentaDescricaoItem(string Descricao)
        {
            return BemaFI32.Bematech_FI_AumentaDescricaoItem(Descricao);
        }

        public int Bematech_FI_UsaUnidadeMedida(string UnidadeMedida)
        {
            return BemaFI32.Bematech_FI_UsaUnidadeMedida(UnidadeMedida);
        }
        #endregion

        #region Funções dos Relatórios Fiscais
        public int Bematech_FI_LeituraX()
        {
            return BemaFI32.Bematech_FI_LeituraX();
        }

        public int Bematech_FI_LeituraXSerial()
        {
            return BemaFI32.Bematech_FI_LeituraXSerial();
        }
        
        public int Bematech_FI_ReducaoZ(string Data, string Hora)
        {
            return BemaFI32.Bematech_FI_ReducaoZ(Data, Hora);
        }
        
        public int Bematech_FI_RelatorioGerencial(string Texto)
        {
            return BemaFI32.Bematech_FI_RelatorioGerencial(Texto);
        }
        
        public int Bematech_FI_FechaRelatorioGerencial()
        {
            return BemaFI32.Bematech_FI_FechaRelatorioGerencial();
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalData(string DataInicial, string DataFinal)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalData(DataInicial, DataFinal);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalReducao(string ReducaoInicial, string ReducaoFinal)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalReducao(ReducaoInicial, ReducaoFinal);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalSerialData(string DataInicial, string DataFinal)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalSerialData(DataInicial, DataFinal);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalSerialReducao(string ReducaoInicial, string ReducaoFinal)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalSerialReducao(ReducaoInicial, ReducaoFinal);
        }
        #endregion

        #region Funções das Operações Não Fiscais
        public int Bematech_FI_RecebimentoNaoFiscal(string IndiceTotalizador, string Valor, string FormaPagamento)
        {
            return BemaFI32.Bematech_FI_RecebimentoNaoFiscal(IndiceTotalizador, Valor, FormaPagamento);
        }

        public int Bematech_FI_AbreComprovanteNaoFiscalVinculado(string FormaPagamento, string Valor, string NumeroCupom)
        {
            return BemaFI32.Bematech_FI_AbreComprovanteNaoFiscalVinculado(FormaPagamento, Valor, NumeroCupom);
        }
        
        public int Bematech_FI_UsaComprovanteNaoFiscalVinculado(string Texto)
        {
            return BemaFI32.Bematech_FI_UsaComprovanteNaoFiscalVinculado(Texto);
        }
        
        public int Bematech_FI_FechaComprovanteNaoFiscalVinculado()
        {
            return BemaFI32.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
        }
        
        public int Bematech_FI_Sangria(string Valor)
        {
            return BemaFI32.Bematech_FI_Sangria(Valor);
        }
        
        public int Bematech_FI_Suprimento(string Valor, string FormaPagamento)
        {
            return BemaFI32.Bematech_FI_Suprimento(Valor, FormaPagamento);
        }
        #endregion

        #region Funções de Informações da Impressora
        public int Bematech_FI_Acrescimos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorAcrescimos)
        {
            return BemaFI32.Bematech_FI_Acrescimos(ref ValorAcrescimos);
        }
        
        public int Bematech_FI_Cancelamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCancelamentos)
        {
            return BemaFI32.Bematech_FI_Cancelamentos(ref ValorCancelamentos);
        }
        
        public int Bematech_FI_CGC_IE([MarshalAs(UnmanagedType.VBByRefStr)] ref string CGC, [MarshalAs(UnmanagedType.VBByRefStr)] ref string IE)
        {
            return BemaFI32.Bematech_FI_CGC_IE(ref CGC, ref IE);
        }
        
        public int Bematech_FI_ClicheProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string Cliche)
        {
            return BemaFI32.Bematech_FI_ClicheProprietario(ref Cliche);
        }
        
        public int Bematech_FI_ContadorBilhetePassagem(string ContadorPassagem)
        {
            return BemaFI32.Bematech_FI_ContadorBilhetePassagem(ContadorPassagem);
        }
        
        public int Bematech_FI_ContadoresTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Contadores)
        {
            return BemaFI32.Bematech_FI_ContadoresTotalizadoresNaoFiscais(ref Contadores);
        }
        
        public int Bematech_FI_DadosUltimaReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string DadosReducao)
        {
            return BemaFI32.Bematech_FI_DadosUltimaReducao(ref DadosReducao);
        }
        
        public int Bematech_FI_DataHoraImpressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora)
        {
            return BemaFI32.Bematech_FI_DataHoraImpressora(ref Data, ref Hora);
        }
        
        public int Bematech_FI_DataHoraReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora)
        {
            return BemaFI32.Bematech_FI_DataHoraReducao(ref Data, ref Hora);
        }
        
        public int Bematech_FI_DataMovimento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data)
        {
            return BemaFI32.Bematech_FI_DataMovimento(ref Data);
        }
        
        public int Bematech_FI_Descontos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorDescontos)
        {
            return BemaFI32.Bematech_FI_Descontos(ref ValorDescontos);
        }
        
        public int Bematech_FI_FlagsFiscais(ref int Flag)
        {
            return BemaFI32.Bematech_FI_FlagsFiscais(ref Flag);
        }
        
        public int Bematech_FI_GrandeTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string GrandeTotal)
        {
            return BemaFI32.Bematech_FI_GrandeTotal(ref GrandeTotal);
        }
        
        public int Bematech_FI_MinutosLigada([MarshalAs(UnmanagedType.VBByRefStr)] ref string Minutos)
        {
            return BemaFI32.Bematech_FI_MinutosLigada(ref Minutos);
        }
        
        public int Bematech_FI_MinutosImprimindo([MarshalAs(UnmanagedType.VBByRefStr)] ref string Minutos)
        {
            return BemaFI32.Bematech_FI_MinutosImprimindo(ref Minutos);
        }
        
        public int Bematech_FI_MonitoramentoPapel(ref int Linhas)
        {
            return BemaFI32.Bematech_FI_MonitoramentoPapel(ref Linhas);
        }
        
        public int Bematech_FI_NumeroCaixa([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCaixa)
        {
            return BemaFI32.Bematech_FI_NumeroCaixa(ref NumeroCaixa);
        }
        
        public int Bematech_FI_NumeroCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCupom)
        {
            return BemaFI32.Bematech_FI_NumeroCupom(ref NumeroCupom);
        }
        
        public int Bematech_FI_NumeroCuponsCancelados([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCancelamentos)
        {
            return BemaFI32.Bematech_FI_NumeroCuponsCancelados(ref NumeroCancelamentos);
        }
        
        public int Bematech_FI_NumeroIntervencoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroIntervencoes)
        {
            return BemaFI32.Bematech_FI_NumeroIntervencoes(ref NumeroIntervencoes);
        }
        
        public int Bematech_FI_NumeroOperacoesNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroOperacoes)
        {
            return BemaFI32.Bematech_FI_NumeroOperacoesNaoFiscais(ref NumeroOperacoes);
        }
        
        public int Bematech_FI_NumeroReducoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroReducoes)
        {
            return BemaFI32.Bematech_FI_NumeroReducoes(ref NumeroReducoes);
        }
        
        public int Bematech_FI_NumeroSerie([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerie)
        {
            return BemaFI32.Bematech_FI_NumeroSerie(ref NumeroSerie);
        }
        
        public int Bematech_FI_NumeroSubstituicoesProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSubstituicoes)
        {
            return BemaFI32.Bematech_FI_NumeroSubstituicoesProprietario(ref NumeroSubstituicoes);
        }
        
        public int Bematech_FI_RetornoAliquotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string Aliquotas)
        {
            return BemaFI32.Bematech_FI_RetornoAliquotas(ref Aliquotas);
        }
        
        public int Bematech_FI_SimboloMoeda([MarshalAs(UnmanagedType.VBByRefStr)] ref string SimboloMoeda)
        {
            return BemaFI32.Bematech_FI_SimboloMoeda(ref SimboloMoeda);
        }
        
        public int Bematech_FI_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string SubTotal)
        {
            return BemaFI32.Bematech_FI_SubTotal(ref SubTotal);
        }
        
        public int Bematech_FI_UltimoItemVendido([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroItem)
        {
            return BemaFI32.Bematech_FI_UltimoItemVendido(ref NumeroItem);
        }
        
        public int Bematech_FI_ValorFormaPagamento(string Forma, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorForma)
        {
            return BemaFI32.Bematech_FI_ValorFormaPagamento(Forma, ref ValorForma);
        }
        
        public int Bematech_FI_ValorPagoUltimoCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCupom)
        {
            return BemaFI32.Bematech_FI_ValorPagoUltimoCupom(ref ValorCupom);
        }
        
        public int Bematech_FI_ValorTotalizadorNaoFiscal(string Totalizador, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorTotalizador)
        {
            return BemaFI32.Bematech_FI_ValorTotalizadorNaoFiscal(Totalizador, ref ValorTotalizador);
        }
        
        public int Bematech_FI_VerificaAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag)
        {
            return BemaFI32.Bematech_FI_VerificaAliquotasIss(ref Flag);
        }
        
        public int Bematech_FI_VerificaEpromConectada([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag)
        {
            return BemaFI32.Bematech_FI_VerificaEpromConectada(ref Flag);
        }
        
        public int Bematech_FI_VerificaDepartamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string Departamentos)
        {
            return BemaFI32.Bematech_FI_VerificaDepartamentos(ref Departamentos);
        }
        
        public int Bematech_FI_VerificaEstadoImpressora(ref int ACK, ref int ST1, ref int ST2)
        {
            return BemaFI32.Bematech_FI_VerificaEstadoImpressora(ref ACK, ref ST1, ref ST2);
        }
        
        public int Bematech_FI_VerificaFormasPagamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Formas)
        {
            return BemaFI32.Bematech_FI_VerificaFormasPagamento(ref Formas);
        }
        
        public int Bematech_FI_VerificaIndiceAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag)
        {
            return BemaFI32.Bematech_FI_VerificaIndiceAliquotasIss(ref Flag);
        }
        
        public int Bematech_FI_VerificaModoOperacao([MarshalAs(UnmanagedType.VBByRefStr)] ref string Modo)
        {
            return BemaFI32.Bematech_FI_VerificaModoOperacao(ref Modo);
        }
        
        public int Bematech_FI_VerificaRecebimentoNaoFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string Recebimentos)
        {
            return BemaFI32.Bematech_FI_VerificaRecebimentoNaoFiscal(ref Recebimentos);
        }
        
        public int Bematech_FI_VerificaTipoImpressora(ref int TipoImpressora)
        {
            return BemaFI32.Bematech_FI_VerificaTipoImpressora(ref TipoImpressora);
        }
        
        public int Bematech_FI_VerificaTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores)
        {
            return BemaFI32.Bematech_FI_VerificaTotalizadoresNaoFiscais(ref Totalizadores);
        }
        
        public int Bematech_FI_VerificaTotalizadoresParciais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores)
        {
            return BemaFI32.Bematech_FI_VerificaTotalizadoresParciais(ref Totalizadores);
        }
        
        public int Bematech_FI_VerificaTruncamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag)
        {
            return BemaFI32.Bematech_FI_VerificaTruncamento(ref Flag);
        }
        
        public int Bematech_FI_VersaoFirmware([MarshalAs(UnmanagedType.VBByRefStr)] ref string VersaoFirmware)
        {
            return BemaFI32.Bematech_FI_VersaoFirmware(ref VersaoFirmware);
        }
        
        public int Bematech_FI_ImprimeConfiguracoes()
        {
            return BemaFI32.Bematech_FI_ImprimeConfiguracoes();
        }
        
        public int Bematech_FI_VerificaReducaoZAutomatica(ref int Flag)
        {
            return BemaFI32.Bematech_FI_VerificaReducaoZAutomatica(ref Flag);
        }
        #endregion

        #region Funções de Autenticação e Gaveta de Dinheiro
        public int Bematech_FI_Autenticacao()
        {
            return BemaFI32.Bematech_FI_Autenticacao();
        }
        
        public int Bematech_FI_ProgramaCaracterAutenticacao(string Parametros)
        {
            return BemaFI32.Bematech_FI_ProgramaCaracterAutenticacao(Parametros);
        }
        
        public int Bematech_FI_AcionaGaveta()
        {
            return BemaFI32.Bematech_FI_AcionaGaveta();
        }
        
        public int Bematech_FI_VerificaEstadoGaveta(out int EstadoGaveta)
        {
            return BemaFI32.Bematech_FI_VerificaEstadoGaveta(out EstadoGaveta);
        }
        #endregion

        #region Funções de Impressão de Cheques
        public int Bematech_FI_CancelaImpressaoCheque()
        {
            return BemaFI32.Bematech_FI_CancelaImpressaoCheque();
        }
        
        public int Bematech_FI_ImprimeCheque(string Banco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem)
        {
            return BemaFI32.Bematech_FI_ImprimeCheque(Banco, Valor, Favorecido, Cidade, Data, Mensagem);
        }
        
        public int Bematech_FI_ImprimeCopiaCheque()
        {
            return BemaFI32.Bematech_FI_ImprimeCopiaCheque();
        }
        
        public int Bematech_FI_IncluiCidadeFavorecido(string Cidade, string Favorecido)
        {
            return BemaFI32.Bematech_FI_IncluiCidadeFavorecido(Cidade, Favorecido);
        }
        
        public int Bematech_FI_ProgramaMoedaPlural(string MoedaPlural)
        {
            return BemaFI32.Bematech_FI_ProgramaMoedaPlural(MoedaPlural);
        }
        
        public int Bematech_FI_ProgramaMoedaSingular(string MoedaSingular)
        {
            return BemaFI32.Bematech_FI_ProgramaMoedaSingular(MoedaSingular);
        }
        
        public int Bematech_FI_VerificaStatusCheque(ref int StatusCheque)
        {
            return BemaFI32.Bematech_FI_VerificaStatusCheque(ref StatusCheque);
        }
        #endregion

        #region Outras Funções
        public int Bematech_FI_AberturaDoDia(string Valor, string FormaPagto)
        {
            return BemaFI32.Bematech_FI_AberturaDoDia(Valor, FormaPagto);
        }
        
        public int Bematech_FI_AbrePortaSerial()
        {
            return BemaFI32.Bematech_FI_AbrePortaSerial();
        }
        
        public int Bematech_FI_FechamentoDoDia()
        {
            return BemaFI32.Bematech_FI_FechamentoDoDia();
        }
        
        public int Bematech_FI_FechaPortaSerial()
        {
            return BemaFI32.Bematech_FI_FechaPortaSerial();
        }
        
        public int Bematech_FI_ImprimeDepartamentos()
        {
            return BemaFI32.Bematech_FI_ImprimeDepartamentos();
        }
        
        public int Bematech_FI_MapaResumo()
        {
            return BemaFI32.Bematech_FI_MapaResumo();
        }
        
        public int Bematech_FI_RelatorioTipo60Analitico()
        {
            return BemaFI32.Bematech_FI_RelatorioTipo60Analitico();
        }
        
        public int Bematech_FI_RelatorioTipo60Mestre()
        {
            return BemaFI32.Bematech_FI_RelatorioTipo60Mestre();
        }
        
        public int Bematech_FI_RetornoImpressora(ref int ACK, ref int ST1, ref int ST2)
        {
            return BemaFI32.Bematech_FI_RetornoImpressora(ref ACK, ref ST1, ref ST2);
        }
        
        public int Bematech_FI_VerificaImpressoraLigada()
        {
            return BemaFI32.Bematech_FI_VerificaImpressoraLigada();
        }
        
        public int Bematech_FI_ResetaImpressora()
        {
            return BemaFI32.Bematech_FI_ResetaImpressora();
        }
        
        public int Bematech_FI_AbreBilhetePassagem(string ImprimeValorFinal, string ImprimeEnfatizado, string Embarque, string Destino, string Linha, string Prefixo, string Agente, string Agencia, string Data, string Hora, string Poltrona, string Plataforma)
        {
            return BemaFI32.Bematech_FI_AbreBilhetePassagem(ImprimeValorFinal, ImprimeEnfatizado, Embarque, Destino, Linha, Prefixo, Agente, Agencia, Data, Hora, Poltrona, Plataforma);
        }
        
        public int Bematech_FI_ImpressaoCarne(string Titulo, string Parcelas, string Datas, int Quantidade, string Texto, string Cliente, string RG_CPF, string Cupom, int Vias, int Assina)
        {
            return BemaFI32.Bematech_FI_ImpressaoCarne(Titulo, Parcelas, Datas, Quantidade, Texto, Cliente, RG_CPF, Cupom, Vias, Assina);
        }
        
        public int Bematech_FI_DadosSintegra(String DataInicial, String DataFinal)
        {
            return BemaFI32.Bematech_FI_DadosSintegra(DataInicial, DataFinal);
        }
        
        public int Bematech_FI_DadosSintegraMFD(String DataInicial, String DataFinal)
        {
            return BemaFI32.Bematech_FI_DadosSintegraMFD(DataInicial, DataFinal);
        }
        
        public int Bematech_FI_RegistrosTipo60()
        {
            return BemaFI32.Bematech_FI_RegistrosTipo60();
        }
        #endregion

        #region Funções para a Impressora Restaurante
        public int Bematech_FIR_AbreConferenciaMesa(string Mesa)
        {
            return BemaFI32.Bematech_FIR_AbreConferenciaMesa(Mesa);
        }
        
        public int Bematech_FIR_AbreCupomRestaurante(string Mesa, string CGC_CPF)
        {
            return BemaFI32.Bematech_FIR_AbreCupomRestaurante(Mesa, CGC_CPF);
        }
        
        public int Bematech_FIR_CancelaVenda(string Mesa, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto)
        {
            return BemaFI32.Bematech_FIR_CancelaVenda(Mesa, Codigo, Descricao, Aliquota, Quantidade, ValorUnitario, FlagAcrescimoDesconto, ValorAcrescimoDesconto);
        }
        
        public int Bematech_FIR_ConferenciaMesa(string Mesa, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto)
        {
            return BemaFI32.Bematech_FIR_ConferenciaMesa(Mesa, FlagAcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto);
        }
        
        public int Bematech_FIR_ContaDividida(string NumeroCupons, string ValorPago, string CGC_CPF)
        {
            return BemaFI32.Bematech_FIR_ContaDividida(NumeroCupons, ValorPago, CGC_CPF);
        }
        
        public int Bematech_FIR_CardapioPelaSerial()
        {
            return BemaFI32.Bematech_FIR_CardapioPelaSerial();
        }
        
        public int Bematech_FIR_FechaConferenciaMesa(string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto)
        {
            return BemaFI32.Bematech_FIR_FechaConferenciaMesa(FlagAcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto);
        }
        
        public int Bematech_FIR_FechaCupomContaDividida(string NumeroCupons, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string FormasPagamento, string ValorFormasPagamento, string ValorPagoCliente, string CGC_CPF)
        {
            return BemaFI32.Bematech_FIR_FechaCupomContaDividida(NumeroCupons, FlagAcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto, FormasPagamento, ValorFormasPagamento, ValorPagoCliente, CGC_CPF);
        }
        
        public int Bematech_FIR_FechaCupomRestaurante(string FormaPagamento, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorFormaPagto, string Mensagem)
        {
            return BemaFI32.Bematech_FIR_FechaCupomRestaurante(FormaPagamento, FlagAcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto, ValorFormaPagto, Mensagem);
        }
        
        public int Bematech_FIR_ImprimeCardapio()
        {
            return BemaFI32.Bematech_FIR_ImprimeCardapio();
        }
        
        public int Bematech_FIR_RegistraVenda(string Mesa, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto)
        {
            return BemaFI32.Bematech_FIR_RegistraVenda(Mesa, Codigo, Descricao, Aliquota, Quantidade, ValorUnitario, FlagAcrescimoDesconto, ValorAcrescimoDesconto);
        }
        
        public int Bematech_FIR_RegistroVendaSerial(string Mesa)
        {
            return BemaFI32.Bematech_FIR_RegistroVendaSerial(Mesa);
        }
        
        public int Bematech_FIR_RelatorioMesasAbertas(int TipoRelatorio)
        {
            return BemaFI32.Bematech_FIR_RelatorioMesasAbertas(TipoRelatorio);
        }
        
        public int Bematech_FIR_RelatorioMesasAbertasSerial()
        {
            return BemaFI32.Bematech_FIR_RelatorioMesasAbertasSerial();
        }
        
        public int Bematech_FIR_TransferenciaItem(string MesaOrigem, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto, string MesaDestino)
        {
            return BemaFI32.Bematech_FIR_TransferenciaItem(MesaOrigem, Codigo, Descricao, Aliquota, Quantidade, ValorUnitario, FlagAcrescimoDesconto, ValorAcrescimoDesconto, MesaDestino);
        }
        
        public int Bematech_FIR_TransferenciaMesa(string MesaOrigem, string MesaDestino)
        {
            return BemaFI32.Bematech_FIR_TransferenciaMesa(MesaOrigem, MesaDestino);
        }
        
        public int Bematech_FIR_VerificaMemoriaLivre(string Bytes)
        {
            return BemaFI32.Bematech_FIR_VerificaMemoriaLivre(Bytes);
        }
        
        public int Bematech_FIR_FechaCupomResumidoRestaurante(string FormaPagamento, string Mensagem)
        {
            return BemaFI32.Bematech_FIR_FechaCupomResumidoRestaurante(FormaPagamento, Mensagem);
        }
        #endregion

        #region Funções da Impressora Fiscal MFD
        public int Bematech_FI_AbreBilhetePassagemMFD(string Embarque, string Destino, string Linha, string Agencia, string Data, string Hora, string Poltrona, string Plataforma, string TipoPassagem)
        {
            return BemaFI32.Bematech_FI_AbreBilhetePassagemMFD(Embarque, Destino, Linha, Agencia, Data, Hora, Poltrona, Plataforma, TipoPassagem);
        }
        
        public int Bematech_FI_AbreComprovanteNaoFiscalVinculadoMFD(string FormaPagamento, string Valor, string NumeroCupom, string CGC, string nome, string Endereco)
        {
            return BemaFI32.Bematech_FI_AbreComprovanteNaoFiscalVinculadoMFD(FormaPagamento, Valor, NumeroCupom, CGC, nome, Endereco);
        }
        
        public int Bematech_FI_AbreCupomMFD(string CGC, string Nome, string Endereco)
        {
            return BemaFI32.Bematech_FI_AbreCupomMFD(CGC, Nome, Endereco);
        }
        
        public int Bematech_FI_AbreRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco)
        {
            return BemaFI32.Bematech_FI_AbreRecebimentoNaoFiscalMFD(CGC, Nome, Endereco);
        }
        
        public int Bematech_FI_AbreRelatorioGerencialMFD(string Indice)
        {
            return BemaFI32.Bematech_FI_AbreRelatorioGerencialMFD(Indice);
        }
        
        public int Bematech_FI_AcrescimoDescontoItemMFD(string Item, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto)
        {
            return BemaFI32.Bematech_FI_AcrescimoDescontoItemMFD(Item, AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto);
        }
        
        public int Bematech_FI_AcrescimoDescontoSubtotalRecebimentoMFD(string cFlag, string cTipo, string cValor)
        {
            return BemaFI32.Bematech_FI_AcrescimoDescontoSubtotalRecebimentoMFD(cFlag, cTipo, cValor);
        }
        
        public int Bematech_FI_AcrescimoDescontoSubtotalMFD(string cFlag, string cTipo, string cValor)
        {
            return BemaFI32.Bematech_FI_AcrescimoDescontoSubtotalMFD(cFlag, cTipo, cValor);
        }
        
        public int Bematech_FI_AutenticacaoMFD(string Linhas, string Texto)
        {
            return BemaFI32.Bematech_FI_AutenticacaoMFD(Linhas, Texto);
        }
        
        public int Bematech_FI_CancelaAcrescimoDescontoItemMFD(string cFlag, string cItem)
        {
            return BemaFI32.Bematech_FI_CancelaAcrescimoDescontoItemMFD(cFlag, cItem);
        }
        
        public int Bematech_FI_CancelaAcrescimoDescontoSubtotalMFD(string cFlag)
        {
            return BemaFI32.Bematech_FI_CancelaAcrescimoDescontoSubtotalMFD(cFlag);
        }
        
        public int Bematech_FI_CancelaAcrescimoDescontoSubtotalRecebimentoMFD(string cFlag)
        {
            return BemaFI32.Bematech_FI_CancelaAcrescimoDescontoSubtotalRecebimentoMFD(cFlag);
        }
        
        public int Bematech_FI_CancelaCupomMFD(string CGC, string Nome, string Endereco)
        {
            return BemaFI32.Bematech_FI_CancelaCupomMFD(CGC, Nome, Endereco);
        }
        
        public int Bematech_FI_CancelaRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco)
        {
            return BemaFI32.Bematech_FI_CancelaRecebimentoNaoFiscalMFD(CGC, Nome, Endereco);
        }
        
        public int Bematech_FI_ComprovantesNaoFiscaisNaoEmitidosMFD(string Comprovantes)
        {
            return BemaFI32.Bematech_FI_ComprovantesNaoFiscaisNaoEmitidosMFD(Comprovantes);
        }
        
        public int Bematech_FI_CNPJMFD(string CNPJ)
        {
            return BemaFI32.Bematech_FI_CNPJMFD(CNPJ);
        }
        
        public int Bematech_FI_ContadorComprovantesCreditoMFD(string Comprovantes)
        {
            return BemaFI32.Bematech_FI_ContadorComprovantesCreditoMFD(Comprovantes);
        }
        
        public int Bematech_FI_ContadorCupomFiscalMFD(string CuponsEmitidos)
        {
            return BemaFI32.Bematech_FI_ContadorCupomFiscalMFD(CuponsEmitidos);
        }
        
        public int Bematech_FI_ContadorFitaDetalheMFD(string ContadorFita)
        {
            return BemaFI32.Bematech_FI_ContadorFitaDetalheMFD(ContadorFita);
        }
        
        public int Bematech_FI_ContadorOperacoesNaoFiscaisCanceladasMFD(string OperacoesCanceladas)
        {
            return BemaFI32.Bematech_FI_ContadorOperacoesNaoFiscaisCanceladasMFD(OperacoesCanceladas);
        }
        
        public int Bematech_FI_ContadorRelatoriosGerenciaisMFD(string Relatorios)
        {
            return BemaFI32.Bematech_FI_ContadorRelatoriosGerenciaisMFD(Relatorios);
        }
        
        public int Bematech_FI_ContadoresTotalizadoresNaoFiscaisMFD(string Contadores)
        {
            return BemaFI32.Bematech_FI_ContadoresTotalizadoresNaoFiscaisMFD(Contadores);
        }
        
        public int Bematech_FI_CupomAdicionalMFD()
        {
            return BemaFI32.Bematech_FI_CupomAdicionalMFD();
        }
        
        public int Bematech_FI_DadosUltimaReducaoMFD(string DadosReducao)
        {
            return BemaFI32.Bematech_FI_DadosUltimaReducaoMFD(DadosReducao);
        }
        
        public int Bematech_FI_DataHoraUltimoDocumentoMFD(string cDataHora)
        {
            return BemaFI32.Bematech_FI_DataHoraUltimoDocumentoMFD(cDataHora);
        }
        
        public int Bematech_FI_EfetuaFormaPagamentoMFD(string FormaPagamento, string ValorFormaPagamento, string Parcelas, string DescricaoFormaPagto)
        {
            return BemaFI32.Bematech_FI_EfetuaFormaPagamentoMFD(FormaPagamento, ValorFormaPagamento, Parcelas, DescricaoFormaPagto);
        }
        
        public int Bematech_FI_EfetuaRecebimentoNaoFiscalMFD(string IndiceTotalizador, string ValorRecebimento)
        {
            return BemaFI32.Bematech_FI_EfetuaRecebimentoNaoFiscalMFD(IndiceTotalizador, ValorRecebimento);
        }
        
        public int Bematech_FI_EstornoNaoFiscalVinculadoMFD(string CGC, string Nome, string Endereco)
        {
            return BemaFI32.Bematech_FI_EstornoNaoFiscalVinculadoMFD(CGC, Nome, Endereco);
        }
        
        public int Bematech_FI_FechaRecebimentoNaoFiscalMFD(string Mensagem)
        {
            return BemaFI32.Bematech_FI_FechaRecebimentoNaoFiscalMFD(Mensagem);
        }
        
        public int Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD(string FlagRetorno)
        {
            return BemaFI32.Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD(FlagRetorno);
        }
        
        public int Bematech_FI_ImprimeChequeMFD(string NumeroBanco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem, string ImpressaoVerso, string Linhas)
        {
            return BemaFI32.Bematech_FI_ImprimeChequeMFD(NumeroBanco, Valor, Favorecido, Cidade, Data, Mensagem, ImpressaoVerso, Linhas);
        }
        
        public int Bematech_FI_IniciaFechamentoCupomMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto)
        {
            return BemaFI32.Bematech_FI_IniciaFechamentoCupomMFD(AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimo, ValorDesconto);
        }
        
        public int Bematech_FI_IniciaFechamentoRecebimentoNaoFiscalMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto)
        {
            return BemaFI32.Bematech_FI_IniciaFechamentoRecebimentoNaoFiscalMFD(AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimo, ValorDesconto);
        }
        
        public int Bematech_FI_InscricaoEstadualMFD(string InscricaoEstadual)
        {
            return BemaFI32.Bematech_FI_InscricaoEstadualMFD(InscricaoEstadual);
        }
        
        public int Bematech_FI_InscricaoMunicipalMFD(string InscricaoMunicipal)
        {
            return BemaFI32.Bematech_FI_InscricaoMunicipalMFD(InscricaoMunicipal);
        }
        
        public int Bematech_FI_LeituraChequeMFD(string CodigoCMC7)
        {
            return BemaFI32.Bematech_FI_LeituraChequeMFD(CodigoCMC7);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalDataMFD(string DataInicial, string DataFinal, string FlagLeitura)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalDataMFD(DataInicial, DataFinal, FlagLeitura);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalReducaoMFD(string ReducaoInicial, string ReducaoFinal, string FlagLeitura)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalReducaoMFD(ReducaoInicial, ReducaoFinal, FlagLeitura);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalSerialDataMFD(string DataInicial, string DataFinal, string FlagLeitura)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalSerialDataMFD(DataInicial, DataFinal, FlagLeitura);
        }
        
        public int Bematech_FI_LeituraMemoriaFiscalSerialReducaoMFD(string ReducaoInicial, string ReducaoFinal, string FlagLeitura)
        {
            return BemaFI32.Bematech_FI_LeituraMemoriaFiscalSerialReducaoMFD(ReducaoInicial, ReducaoFinal, FlagLeitura);
        }
        
        public int Bematech_FI_MapaResumoMFD()
        {
            return BemaFI32.Bematech_FI_MapaResumoMFD();
        }
        
        public int Bematech_FI_MarcaModeloTipoImpressoraMFD(string Marca, string Modelo, string Tipo)
        {
            return BemaFI32.Bematech_FI_MarcaModeloTipoImpressoraMFD(Marca, Modelo, Tipo);
        }
        
        public int Bematech_FI_MinutosEmitindoDocumentosFiscaisMFD(string Minutos)
        {
            return BemaFI32.Bematech_FI_MinutosEmitindoDocumentosFiscaisMFD(Minutos);
        }
        
        public int Bematech_FI_NomeiaRelatorioGerencialMFD(string Indice, string Descricao)
        {
            return BemaFI32.Bematech_FI_NomeiaRelatorioGerencialMFD(Indice, Descricao);
        }
        
        public int Bematech_FI_NumeroSerieMFD(string NumeroSerie)
        {
            return BemaFI32.Bematech_FI_NumeroSerieMFD(NumeroSerie);
        }
        
        public int Bematech_FI_NumeroSerieMemoriaMFD(string NumeroSerieMFD)
        {
            return BemaFI32.Bematech_FI_NumeroSerieMemoriaMFD(NumeroSerieMFD);
        }
        
        public int Bematech_FI_PercentualLivreMFD(string cMemoriaLivre)
        {
            return BemaFI32.Bematech_FI_PercentualLivreMFD(cMemoriaLivre);
        }
        
        public int Bematech_FI_ProgramaFormaPagamentoMFD(string FormaPagto, string OperacaoTef)
        {
            return BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD(FormaPagto, OperacaoTef);
        }
        
        public int Bematech_FI_ReducoesRestantesMFD(string Reducoes)
        {
            return BemaFI32.Bematech_FI_ReducoesRestantesMFD(Reducoes);
        }
        
        public int Bematech_FI_ReimpressaoNaoFiscalVinculadoMFD()
        {
            return BemaFI32.Bematech_FI_ReimpressaoNaoFiscalVinculadoMFD();
        }
        
        public int Bematech_FI_RelatorioTipo60AnaliticoMFD()
        {
            return BemaFI32.Bematech_FI_RelatorioTipo60AnaliticoMFD();
        }
        
        public int Bematech_FI_RetornoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3)
        {
            return BemaFI32.Bematech_FI_RetornoImpressoraMFD(ref ACK, ref ST1, ref ST2, ref ST3);
        }
        
        public int Bematech_FI_SegundaViaNaoFiscalVinculadoMFD()
        {
            return BemaFI32.Bematech_FI_SegundaViaNaoFiscalVinculadoMFD();
        }
        
        public int Bematech_FI_SubTotalizaCupomMFD()
        {
            return BemaFI32.Bematech_FI_SubTotalizaCupomMFD();
        }
        
        public int Bematech_FI_SubTotalizaRecebimentoMFD()
        {
            return BemaFI32.Bematech_FI_SubTotalizaRecebimentoMFD();
        }
        
        public int Bematech_FI_TotalLivreMFD(string cMemoriaLivre)
        {
            return BemaFI32.Bematech_FI_TotalLivreMFD(cMemoriaLivre);
        }
        
        public int Bematech_FI_TamanhoTotalMFD(string cTamanhoMFD)
        {
            return BemaFI32.Bematech_FI_TamanhoTotalMFD(cTamanhoMFD);
        }
        
        public int Bematech_FI_TempoOperacionalMFD(string TempoOperacional)
        {
            return BemaFI32.Bematech_FI_TempoOperacionalMFD(TempoOperacional);
        }
        
        public int Bematech_FI_TotalizaCupomMFD()
        {
            return BemaFI32.Bematech_FI_TotalizaCupomMFD();
        }
        
        public int Bematech_FI_TotalizaRecebimentoMFD()
        {
            return BemaFI32.Bematech_FI_TotalizaRecebimentoMFD();
        }
        
        public int Bematech_FI_UsaRelatorioGerencialMFD(string Texto)
        {
            return BemaFI32.Bematech_FI_UsaRelatorioGerencialMFD(Texto);
        }
        
        public int Bematech_FI_ValorFormaPagamentoMFD(string Forma, string ValorForma)
        {
            return BemaFI32.Bematech_FI_ValorFormaPagamentoMFD(Forma, ValorForma);
        }
        
        public int Bematech_FI_ValorTotalizadorNaoFiscalMFD(string Totalizador, string ValorTotalizador)
        {
            return BemaFI32.Bematech_FI_ValorTotalizadorNaoFiscalMFD(Totalizador, ValorTotalizador);
        }
        
        public int Bematech_FI_VerificaEstadoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3)
        {
            return BemaFI32.Bematech_FI_VerificaEstadoImpressoraMFD(ref ACK, ref ST1, ref ST2, ref ST3);
        }
        
        public int Bematech_FI_VerificaFormasPagamentoMFD(string FormasPagamento)
        {
            return BemaFI32.Bematech_FI_VerificaFormasPagamentoMFD(FormasPagamento);
        }
        
        public int Bematech_FI_VerificaRecebimentoNaoFiscalMFD(string Recebimentos)
        {
            return BemaFI32.Bematech_FI_VerificaRecebimentoNaoFiscalMFD(Recebimentos);
        }
        
        public int Bematech_FI_VerificaRelatorioGerencialMFD(string Relatorios)
        {
            return BemaFI32.Bematech_FI_VerificaRelatorioGerencialMFD(Relatorios);
        }
        
        public int Bematech_FI_VerificaTotalizadoresNaoFiscaisMFD(string Totalizadores)
        {
            return BemaFI32.Bematech_FI_VerificaTotalizadoresNaoFiscaisMFD(Totalizadores);
        }
        
        public int Bematech_FI_VerificaTotalizadoresParciaisMFD(string Totalizadores)
        {
            return BemaFI32.Bematech_FI_VerificaTotalizadoresParciaisMFD(Totalizadores);
        }
        
        public int Bematech_FI_VersaoFirmwareMFD(string VersaoFirmware)
        {
            return BemaFI32.Bematech_FI_VersaoFirmwareMFD(VersaoFirmware);
        }

        public int Bematech_FI_RelatorioSintegraMFD(int iRelatorios, string Arquivo, string Mes,
             string Ano, string RazaoSocial, string Endereco, string Numero, string Complemento,
             string Bairro, string Cidade, string CEP, string Telefone, string Fax, string Contato)
        {
            return BemaFI32.Bematech_FI_RelatorioSintegraMFD(iRelatorios, Arquivo, Mes,
             Ano, RazaoSocial, Endereco, Numero, Complemento,
             Bairro, Cidade, CEP, Telefone, Fax, Contato);
            
        }
        #endregion
    }
}
