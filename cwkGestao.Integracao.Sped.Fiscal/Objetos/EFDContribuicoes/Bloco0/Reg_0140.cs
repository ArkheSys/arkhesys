using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0140 : Registro
    {
        /// <summary>
        ///  TABELA DE CADASTRO DE ESTABELECIMENTO
        /// </summary>
        
        public string Header = "0140";

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public int codigoDeIdentificacaoDoEstabelecimento;

        [FieldConverter(typeof(StringNumberCharConverter), "100", "", "", "false")]
        public string nomeEmpresarialDoEstabelecimento;

        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string numeroDeInscricaoDoEstabelecimentoNoCNPJ;

        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string siglaDaUnidadeDaFederaçãoDoEstabelecimento;

        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string inscricaoEstadualDoEstabelecimento;

        [FieldConverter(typeof(StringNumberCharConverter), "7", "", "", "true")]
        public string codigoDoMunicipioDoDomicilioFiscalDoEstabelecimento;

        public string inscricaoMunicipalDoEstabelecimento;

        [FieldConverter(typeof(StringNumberCharConverter), "9", "", "", "false")]
        public string inscricaoDoEstabelecimentoNaSuframa;
        
    }
}
