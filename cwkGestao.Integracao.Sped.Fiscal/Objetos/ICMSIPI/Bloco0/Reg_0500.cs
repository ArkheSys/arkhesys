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
    public class Reg_0500 : Registro
    {
        /// <summary>
        /// Plano de Contas Contábeis
        /// </summary>
        
        public string Header = "0500";

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime datainclusaoOuAlteracao;

        [FieldConverter(typeof(StringNumberCharConverter), "02", "", "", "false")]
        public string codigoDaNaturezaDaContaOuGrupoDeContas;

        [FieldConverter(typeof(StringNumberCharConverter), "01", "", "", "false")]
        public string indicadorTipoConta;

        [FieldConverter(typeof(StringNumberCharConverter), "05", "", "", "false")]
        public int nivelContaAnaliticaOuGrupoDeContas;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "true")]
        public int codigoContaAnaliticaOuGrupoDeContas;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string nomeDaContaAnaliticaOuGrupoDeContas;
 
    }
}
