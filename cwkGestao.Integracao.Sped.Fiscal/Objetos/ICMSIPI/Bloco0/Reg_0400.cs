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
    public class Reg_0400 : Registro
    {
        /// <summary>
        ///  TABELA DE NATUREZA DA OPERAÇÃO/PRESTAÇÃO 
        /// </summary>
        
        public string Header = "0400";

        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "false")]
        public int codigoDaNaturezaDaOperacaoOuPrestacao;

        public string descricaoDaNaturezaDaOperacaoOuPrestacao;


    }
}
