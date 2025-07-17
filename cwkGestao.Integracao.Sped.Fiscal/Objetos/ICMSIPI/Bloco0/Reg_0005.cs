using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0005 : Registro
    {
        /// <summary>
        ///  DADOS COMPLEMENTARES DA ENTIDADE
        /// </summary>

        public string Header = "0005";

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string fantasia;
        
        [FieldConverter(typeof(StringNumberCharConverter), "8", "", "", "true")]
        public string cep;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string endereco;

        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "false")]
        public string numImovel;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string complemento;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string bairro;

        [FieldConverter(typeof(StringNumberCharConverter), "11", "", "", "true")]
        public string fone;

        [FieldConverter(typeof(StringNumberCharConverter), "11", "", "", "true")]
        public string fax;

        public string email;


     
    }
}
