using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0000 : Registro
    {
        /// <summary>
        ///  ABERTURA DO ARQUIVO DIGITAL E IDENTIFICAÇÃO DA PESSOA JURÍDICA 
        /// </summary>

        public string Header = "0000";

        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public string codigoVersaoLeiaute;
        
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int tipoDeEscrituracao;

        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int indicadorDeSituacaoEspecial;

        [FieldConverter(typeof(StringNumberCharConverter), "41", "", "", "false")]
        public string numeroDoReciboDeEscrituracaoAnteriorASerRetificada;

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime dtInicioInformacoesArquivo;

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime dtFimInformacoesArquivo;

        [FieldConverter(typeof(StringNumberCharConverter), "100", "", "", "false")]
        public string nomeEmpresarialPessoaJuridica;
        
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string numeroDeInscriçãoDaMatrizPessoaJurídicaCNPJ;

        [FieldConverter(typeof(StringNumberCharConverter), "02", "", "", "false")]
        public string siglaDaUnidadeDaFederaçãoDaPessoaJuridica;

        [FieldConverter(typeof(StringNumberCharConverter), "07", "", "", "false")]
        public string codigoDoMunicipioDoDomicilioFiscalDaPessoaJuridica;

        [FieldConverter(typeof(StringNumberCharConverter), "09", "", "", "false")]
        public string inscricaoDaPessoaJuridicaNaSuframa;

        [FieldConverter(typeof(StringNumberCharConverter), "02", "", "", "false")]
        public string indicadorDaNaturezaDaPessoaJuridica;

        [FieldConverter(typeof(StringNumberCharConverter), "01", "", "", "false")]
        public string indicadorDeTipoDeAtividadePreponderante;
     
    }
}
