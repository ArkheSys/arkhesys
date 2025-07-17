using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;
using cwkGestao.Modelo;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0450 : Registro
    {
        /// <summary>
        /// TABELA DE INFORMAÇÃO COMPLEMENTAR DO DOCUMENTO FISCAL
        /// </summary>
        
        public string Header = "0450";

        [FieldConverter(typeof(StringNumberCharConverter), "06", "", "", "false")]
        public string codigoDaInformacaoComplementarDoDocumento;
        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "false")]
        public string textoLivreDaInformacaoComplementarExistenteNoDocumentoFiscal;
#pragma warning disable CS0618 // "FieldIgnoredAttribute" é obsoleto: "You must use [FieldNotInFile] instead"
        [FieldIgnored]
#pragma warning restore CS0618 // "FieldIgnoredAttribute" é obsoleto: "You must use [FieldNotInFile] instead"
        public IList<Nota> NotasDoTextoLei;

 
    }
}
