using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace cwkGestao.Negocio.Utils
{
    public interface ICupomFiscal
    {
        #region Funções de tratamento de erro
        void Analisa_RetornoImpressora();
        void Analisa_iRetorno(int IRetorno);
        #endregion

        #region Funções de Inicialização
        int Bematech_FI_AlteraSimboloMoeda(string SimboloMoeda);
        int Bematech_FI_ProgramaAliquota(string Aliquota, int ICMS_ISS);
        int Bematech_FI_NomeiaDepartamento(int Indice, string Departamento);
        int Bematech_FI_NomeiaTotalizadorNaoSujeitoIcms(int Indice, string Totalizador);
        int Bematech_FI_LinhasEntreCupons(int Linhas);
        int Bematech_FI_EspacoEntreLinhas(int Dots);
        int Bematech_FI_ForcaImpactoAgulhas(int ForcaImpacto);
        int Bematech_FI_ProgramaHorarioVerao();
        int Bematech_FI_ProgramaArredondamento();
        int Bematech_FI_ProgramaTruncamento();
        #endregion

        #region Funções do Cupom Fiscal
        int Bematech_FI_AbreCupom(string CGC_CPF);
        int Bematech_FI_VendeItem(string Codigo, string Descricao, string Aliquota, string TipoQuantidade, string Quantidade, int CasasDecimais, string ValorUnitario, string TipoDesconto, string Desconto);
        int Bematech_FI_VendeItemDepartamento(string Codigo, string Descricao, string Aliquota, string ValorUnitario, string Quantidade, string Acrescimo, string Desconto, string IndiceDepartamento, string UnidadeMedida);
        int Bematech_FI_CancelaItemAnterior();
        int Bematech_FI_CancelaItemGenerico(string NumeroItem);
        int Bematech_FI_CancelaCupom();
        int Bematech_FI_FechaCupomResumido(string FormaPagamento, string Mensagem);
        int Bematech_FI_FechaCupom(string FormaPagamento, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem);
        int Bematech_FI_IniciaFechamentoCupom(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        int Bematech_FI_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento);
        int Bematech_FI_EfetuaFormaPagamentoDescricaoForma(string FormaPagamento, string ValorFormaPagamento, string Descricao);
        int Bematech_FI_TerminaFechamentoCupom(string Mensagem);
        int Bematech_FI_EstornoFormasPagamento(string FormaOrigem, string FormaDestino, string Valor);
        int Bematech_FI_AumentaDescricaoItem(string Descricao);
        int Bematech_FI_UsaUnidadeMedida(string UnidadeMedida);
        #endregion

        #region Funções dos Relatórios Fiscais
        int Bematech_FI_LeituraX();
        int Bematech_FI_LeituraXSerial();
        int Bematech_FI_ReducaoZ(string Data, string Hora);
        int Bematech_FI_RelatorioGerencial(string Texto);
        int Bematech_FI_FechaRelatorioGerencial();
        int Bematech_FI_LeituraMemoriaFiscalData(string DataInicial, string DataFinal);
        int Bematech_FI_LeituraMemoriaFiscalReducao(string ReducaoInicial, string ReducaoFinal);
        int Bematech_FI_LeituraMemoriaFiscalSerialData(string DataInicial, string DataFinal);
        int Bematech_FI_LeituraMemoriaFiscalSerialReducao(string ReducaoInicial, string ReducaoFinal);
        #endregion

        #region Funções das Operações Não Fiscais
        int Bematech_FI_RecebimentoNaoFiscal(string IndiceTotalizador, string Valor, string FormaPagamento);
        int Bematech_FI_AbreComprovanteNaoFiscalVinculado(string FormaPagamento, string Valor, string NumeroCupom);
        int Bematech_FI_UsaComprovanteNaoFiscalVinculado(string Texto);
        int Bematech_FI_FechaComprovanteNaoFiscalVinculado();
        int Bematech_FI_Sangria(string Valor);
        int Bematech_FI_Suprimento(string Valor, string FormaPagamento);
        #endregion

        #region Funções de Informações da Impressora
        int Bematech_FI_Acrescimos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorAcrescimos);
        int Bematech_FI_Cancelamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCancelamentos);
        int Bematech_FI_CGC_IE([MarshalAs(UnmanagedType.VBByRefStr)] ref string CGC, [MarshalAs(UnmanagedType.VBByRefStr)] ref string IE);
        int Bematech_FI_ClicheProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string Cliche);
        int Bematech_FI_ContadorBilhetePassagem(string ContadorPassagem);
        int Bematech_FI_ContadoresTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Contadores);
        int Bematech_FI_DadosUltimaReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string DadosReducao);
        int Bematech_FI_DataHoraImpressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
        int Bematech_FI_DataHoraReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
        int Bematech_FI_DataMovimento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data);
        int Bematech_FI_Descontos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorDescontos);
        int Bematech_FI_FlagsFiscais(ref int Flag);
        int Bematech_FI_GrandeTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string GrandeTotal);
        int Bematech_FI_MinutosLigada([MarshalAs(UnmanagedType.VBByRefStr)] ref string Minutos);
        int Bematech_FI_MinutosImprimindo([MarshalAs(UnmanagedType.VBByRefStr)] ref string Minutos);
        int Bematech_FI_MonitoramentoPapel(ref int Linhas);
        int Bematech_FI_NumeroCaixa([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCaixa);
        int Bematech_FI_NumeroCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCupom);
        int Bematech_FI_NumeroCuponsCancelados([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCancelamentos);
        int Bematech_FI_NumeroIntervencoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroIntervencoes);
        int Bematech_FI_NumeroOperacoesNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroOperacoes);
        int Bematech_FI_NumeroReducoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroReducoes);
        int Bematech_FI_NumeroSerie([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerie);
        int Bematech_FI_NumeroSubstituicoesProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSubstituicoes);
        int Bematech_FI_RetornoAliquotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string Aliquotas);
        int Bematech_FI_SimboloMoeda([MarshalAs(UnmanagedType.VBByRefStr)] ref string SimboloMoeda);
        int Bematech_FI_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string SubTotal);
        int Bematech_FI_UltimoItemVendido([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroItem);
        int Bematech_FI_ValorFormaPagamento(string Forma, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorForma);
        int Bematech_FI_ValorPagoUltimoCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCupom);
        int Bematech_FI_ValorTotalizadorNaoFiscal(string Totalizador, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorTotalizador);
        int Bematech_FI_VerificaAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        int Bematech_FI_VerificaEpromConectada([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        int Bematech_FI_VerificaDepartamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string Departamentos);
        int Bematech_FI_VerificaEstadoImpressora(ref int ACK, ref int ST1, ref int ST2);
        int Bematech_FI_VerificaFormasPagamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Formas);
        int Bematech_FI_VerificaIndiceAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        int Bematech_FI_VerificaModoOperacao([MarshalAs(UnmanagedType.VBByRefStr)] ref string Modo);
        int Bematech_FI_VerificaRecebimentoNaoFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string Recebimentos);
        int Bematech_FI_VerificaTipoImpressora(ref int TipoImpressora);
        int Bematech_FI_VerificaTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores);
        int Bematech_FI_VerificaTotalizadoresParciais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores);
        int Bematech_FI_VerificaTruncamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        int Bematech_FI_VersaoFirmware([MarshalAs(UnmanagedType.VBByRefStr)] ref string VersaoFirmware);
        int Bematech_FI_ImprimeConfiguracoes();
        int Bematech_FI_VerificaReducaoZAutomatica(ref int Flag);
        #endregion

        #region Funções de Autenticação e Gaveta de Dinheiro
        int Bematech_FI_Autenticacao();
        int Bematech_FI_ProgramaCaracterAutenticacao(string Parametros);
        int Bematech_FI_AcionaGaveta();
        int Bematech_FI_VerificaEstadoGaveta(out int EstadoGaveta);
        #endregion

        #region Funções de Impressão de Cheques
        int Bematech_FI_CancelaImpressaoCheque();
        int Bematech_FI_ImprimeCheque(string Banco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem);
        int Bematech_FI_ImprimeCopiaCheque();
        int Bematech_FI_IncluiCidadeFavorecido(string Cidade, string Favorecido);
        int Bematech_FI_ProgramaMoedaPlural(string MoedaPlural);
        int Bematech_FI_ProgramaMoedaSingular(string MoedaSingular);
        int Bematech_FI_VerificaStatusCheque(ref int StatusCheque);
        #endregion

        #region Outras Funções
        int Bematech_FI_AberturaDoDia(string Valor, string FormaPagto);
        int Bematech_FI_AbrePortaSerial();
        int Bematech_FI_FechamentoDoDia();
        int Bematech_FI_FechaPortaSerial();
        int Bematech_FI_ImprimeDepartamentos();
        int Bematech_FI_MapaResumo();
        int Bematech_FI_RelatorioTipo60Analitico();
        int Bematech_FI_RelatorioTipo60Mestre();
        int Bematech_FI_RetornoImpressora(ref int ACK, ref int ST1, ref int ST2);
        int Bematech_FI_VerificaImpressoraLigada();
        int Bematech_FI_ResetaImpressora();
        int Bematech_FI_AbreBilhetePassagem(string ImprimeValorFinal, string ImprimeEnfatizado, string Embarque, string Destino, string Linha, string Prefixo, string Agente, string Agencia, string Data, string Hora, string Poltrona, string Plataforma);
        int Bematech_FI_ImpressaoCarne(string Titulo, string Parcelas, string Datas, int Quantidade, string Texto, string Cliente, string RG_CPF, string Cupom, int Vias, int Assina);
        int Bematech_FI_DadosSintegra(String DataInicial, String DataFinal);
        int Bematech_FI_DadosSintegraMFD(String DataInicial, String DataFinal);
        int Bematech_FI_RegistrosTipo60();
        int Bematech_FI_RelatorioSintegraMFD(int iRelatorios, string cArquivo, string cMes,
             string cAno, string cRazaoSocial, string cEndereco, string cNumero, string cComplemento,
             string cBairro, string cCidade, string cCEP, string cTelefone, string cFax, string cContato);
        #endregion

        #region Funções para a Impressora Restaurante
        int Bematech_FIR_AbreConferenciaMesa(string Mesa);
        int Bematech_FIR_AbreCupomRestaurante(string Mesa, string CGC_CPF);
        int Bematech_FIR_CancelaVenda(string Mesa, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto);
        int Bematech_FIR_ConferenciaMesa(string Mesa, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        int Bematech_FIR_ContaDividida(string NumeroCupons, string ValorPago, string CGC_CPF);
        int Bematech_FIR_CardapioPelaSerial();
        int Bematech_FIR_FechaConferenciaMesa(string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        int Bematech_FIR_FechaCupomContaDividida(string NumeroCupons, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string FormasPagamento, string ValorFormasPagamento, string ValorPagoCliente, string CGC_CPF);
        int Bematech_FIR_FechaCupomRestaurante(string FormaPagamento, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorFormaPagto, string Mensagem);
        int Bematech_FIR_ImprimeCardapio();
        int Bematech_FIR_RegistraVenda(string Mesa, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto);
        int Bematech_FIR_RegistroVendaSerial(string Mesa);
        int Bematech_FIR_RelatorioMesasAbertas(int TipoRelatorio);
        int Bematech_FIR_RelatorioMesasAbertasSerial();
        int Bematech_FIR_TransferenciaItem(string MesaOrigem, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto, string MesaDestino);
        int Bematech_FIR_TransferenciaMesa(string MesaOrigem, string MesaDestino);
        int Bematech_FIR_VerificaMemoriaLivre(string Bytes);
        int Bematech_FIR_FechaCupomResumidoRestaurante(string FormaPagamento, string Mensagem);
        #endregion

        #region Funções da Impressora Fiscal MFD
        int Bematech_FI_AbreBilhetePassagemMFD(string Embarque, string Destino, string Linha, string Agencia, string Data, string Hora, string Poltrona, string Plataforma, string TipoPassagem);
        int Bematech_FI_AbreComprovanteNaoFiscalVinculadoMFD(string FormaPagamento, string Valor, string NumeroCupom, string CGC, string nome, string Endereco);
        int Bematech_FI_AbreCupomMFD(string CGC, string Nome, string Endereco);
        int Bematech_FI_AbreRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);
        int Bematech_FI_AbreRelatorioGerencialMFD(string Indice);
        int Bematech_FI_AcrescimoDescontoItemMFD(string Item, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        int Bematech_FI_AcrescimoDescontoSubtotalRecebimentoMFD(string cFlag, string cTipo, string cValor);
        int Bematech_FI_AcrescimoDescontoSubtotalMFD(string cFlag, string cTipo, string cValor);
        int Bematech_FI_AutenticacaoMFD(string Linhas, string Texto);
        int Bematech_FI_CancelaAcrescimoDescontoItemMFD(string cFlag, string cItem);
        int Bematech_FI_CancelaAcrescimoDescontoSubtotalMFD(string cFlag);
        int Bematech_FI_CancelaAcrescimoDescontoSubtotalRecebimentoMFD(string cFlag);
        int Bematech_FI_CancelaCupomMFD(string CGC, string Nome, string Endereco);
        int Bematech_FI_CancelaRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);
        int Bematech_FI_ComprovantesNaoFiscaisNaoEmitidosMFD(string Comprovantes);
        int Bematech_FI_CNPJMFD(string CNPJ);
        int Bematech_FI_ContadorComprovantesCreditoMFD(string Comprovantes);
        int Bematech_FI_ContadorCupomFiscalMFD(string CuponsEmitidos);
        int Bematech_FI_ContadorFitaDetalheMFD(string ContadorFita);
        int Bematech_FI_ContadorOperacoesNaoFiscaisCanceladasMFD(string OperacoesCanceladas);
        int Bematech_FI_ContadorRelatoriosGerenciaisMFD(string Relatorios);
        int Bematech_FI_ContadoresTotalizadoresNaoFiscaisMFD(string Contadores);
        int Bematech_FI_CupomAdicionalMFD();
        int Bematech_FI_DadosUltimaReducaoMFD(string DadosReducao);
        int Bematech_FI_DataHoraUltimoDocumentoMFD(string cDataHora);
        int Bematech_FI_EfetuaFormaPagamentoMFD(string FormaPagamento, string ValorFormaPagamento, string Parcelas, string DescricaoFormaPagto);
        int Bematech_FI_EfetuaRecebimentoNaoFiscalMFD(string IndiceTotalizador, string ValorRecebimento);
        int Bematech_FI_EstornoNaoFiscalVinculadoMFD(string CGC, string Nome, string Endereco);
        int Bematech_FI_FechaRecebimentoNaoFiscalMFD(string Mensagem);
        int Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD(string FlagRetorno);
        int Bematech_FI_ImprimeChequeMFD(string NumeroBanco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem, string ImpressaoVerso, string Linhas);
        int Bematech_FI_IniciaFechamentoCupomMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto);
        int Bematech_FI_IniciaFechamentoRecebimentoNaoFiscalMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto);
        int Bematech_FI_InscricaoEstadualMFD(string InscricaoEstadual);
        int Bematech_FI_InscricaoMunicipalMFD(string InscricaoMunicipal);
        int Bematech_FI_LeituraChequeMFD(string CodigoCMC7);
        int Bematech_FI_LeituraMemoriaFiscalDataMFD(string DataInicial, string DataFinal, string FlagLeitura);
        int Bematech_FI_LeituraMemoriaFiscalReducaoMFD(string ReducaoInicial, string ReducaoFinal, string FlagLeitura);
        int Bematech_FI_LeituraMemoriaFiscalSerialDataMFD(string DataInicial, string DataFinal, string FlagLeitura);
        int Bematech_FI_LeituraMemoriaFiscalSerialReducaoMFD(string ReducaoInicial, string ReducaoFinal, string FlagLeitura);
        int Bematech_FI_MapaResumoMFD();
        int Bematech_FI_MarcaModeloTipoImpressoraMFD(string Marca, string Modelo, string Tipo);
        int Bematech_FI_MinutosEmitindoDocumentosFiscaisMFD(string Minutos);
        int Bematech_FI_NomeiaRelatorioGerencialMFD(string Indice, string Descricao);
        int Bematech_FI_NumeroSerieMFD(string NumeroSerie);
        int Bematech_FI_NumeroSerieMemoriaMFD(string NumeroSerieMFD);
        int Bematech_FI_PercentualLivreMFD(string cMemoriaLivre);
        int Bematech_FI_ProgramaFormaPagamentoMFD(string FormaPagto, string OperacaoTef);
        int Bematech_FI_ReducoesRestantesMFD(string Reducoes);
        int Bematech_FI_ReimpressaoNaoFiscalVinculadoMFD();
        int Bematech_FI_RelatorioTipo60AnaliticoMFD();
        int Bematech_FI_RetornoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3);
        int Bematech_FI_SegundaViaNaoFiscalVinculadoMFD();
        int Bematech_FI_SubTotalizaCupomMFD();
        int Bematech_FI_SubTotalizaRecebimentoMFD();
        int Bematech_FI_TotalLivreMFD(string cMemoriaLivre);
        int Bematech_FI_TamanhoTotalMFD(string cTamanhoMFD);
        int Bematech_FI_TempoOperacionalMFD(string TempoOperacional);
        int Bematech_FI_TotalizaCupomMFD();
        int Bematech_FI_TotalizaRecebimentoMFD();
        int Bematech_FI_UsaRelatorioGerencialMFD(string Texto);
        int Bematech_FI_ValorFormaPagamentoMFD(string Forma, string ValorForma);
        int Bematech_FI_ValorTotalizadorNaoFiscalMFD(string Totalizador, string ValorTotalizador);
        int Bematech_FI_VerificaEstadoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3);
        int Bematech_FI_VerificaFormasPagamentoMFD(string FormasPagamento);
        int Bematech_FI_VerificaRecebimentoNaoFiscalMFD(string Recebimentos);
        int Bematech_FI_VerificaRelatorioGerencialMFD(string Relatorios);
        int Bematech_FI_VerificaTotalizadoresNaoFiscaisMFD(string Totalizadores);
        int Bematech_FI_VerificaTotalizadoresParciaisMFD(string Totalizadores);
        int Bematech_FI_VersaoFirmwareMFD(string VersaoFirmware);
        #endregion
    }
}
