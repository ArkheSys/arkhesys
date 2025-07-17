using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0150 : Registro
    {
        /// <summary>
        /// TABELA DE CADASTRO DO PARTICIPANTE 
        /// </summary>
        
        public string Header = "0150";

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public int codigoDeIdentificacaoDoParticipante;

        [FieldConverter(typeof(StringNumberCharConverter), "100", "", "", "false")]
        public string nomePessoalOuEmpresarialDoParticipante;

        [FieldConverter(typeof(StringNumberCharConverter), "05", "", "", "false")]
        public string codigoDoPaisDoParticipante;

        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string cnpjDoParticipante;

        [FieldConverter(typeof(StringNumberCharConverter), "11", "", "", "true")]
        public string cpfDoParticipante;

        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string inscricaoEstadualDoParticipante;

        [FieldConverter(typeof(StringNumberCharConverter), "7", "", "", "true")]
        public string codigoDoMunicipio;

        [FieldConverter(typeof(StringNumberCharConverter), "9", "", "", "false")]
        public string numeroDeInscricaoDoPartipanteNoSuframa;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string logradouroEEnderecoMovel;

        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "false")]
        public string numeroDoImovel;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string dadosComplementaresDoEndereco;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string bairroEmQueOImovelEstaSituado;
    }
}
