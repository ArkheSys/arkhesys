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
    public class Reg_0100 : Registro
    {
        /// <summary>
        ///  DADOS DO CONTABILISTA 
        /// </summary>
        public string Header = "0100";

        [FieldConverter(typeof(StringNumberCharConverter), "100", "", "", "false")]
        public string nomeDoContabilista;

        [FieldConverter(typeof(StringNumberCharConverter), "11", "", "", "true")]
        public string numeroInscricaoContabilistaNoCPF;

        [FieldConverter(typeof(StringNumberCharConverter), "15", "", "", "true")]
        public string numeroInscricaoContabilistaNoConselhoContabilista;

        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string numeroDeInscricaoDoEscritorioDeContabilidadeNoCNPJ;

        [FieldConverter(typeof(StringNumberCharConverter), "8", "", "", "true")]
        public string codigoEnderecamentoPostal;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string logradouroEEnderecoDoImovel;

        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "true")]
        public string numDoImovel;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string dadosComplementaresDoEndereco;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string bairroEmQueOImovelEstaSituado;

        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "true")]
        public string numeroDoTelefone;

        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "true")]
        public string numeroDoFax;

        public string enderecoDoCorreioEletronico;

        [FieldConverter(typeof(StringNumberCharConverter), "7", "", "", "true")]
        public string codMunicipioIBGE;
    }
}
