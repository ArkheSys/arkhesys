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
    public class Reg_0200 : Registro
    {
        /// <summary>
        ///  TABELA DE IDENTIFICAÇÃO DO ITEM (PRODUTOS E SERVIÇOS) 
        /// </summary>
        
        public string Header = "0200";

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string codigoDoIten;

        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "true")]
        public string descricaoDoItem;

        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "true")]
        public string representacaoAlfanumericoDoCodigoDeBarraDoProduto;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "true")]
        public string codigoAnteriorDoItemComRelacaoAUltimaInformacaoApresentada;

        [FieldConverter(typeof(StringNumberCharConverter), "06", "", "", "false")]
        public string unidadeDeMedidaUtilizadaNaQuantificacaoDeEstoques;

        [FieldConverter(typeof(StringNumberCharConverter), "02", "", "", "false")]
        public string tipoDoItem;

        [FieldConverter(typeof(StringNumberCharConverter), "08", "", "", "true")]
        public string codigoDaNomenclaturaComumDoMercosul;

        [FieldConverter(typeof(StringNumberCharConverter), "03", "", "", "true")]
        public string codigoExConformeATIPI;

        [FieldConverter(typeof(StringNumberCharConverter), "02", "", "", "false")]
        public string codigoDoGeneroDoItem;

        [FieldConverter(typeof(StringNumberCharConverter), "04", "", "", "false")]
        public string codigoDoServico;

        [FieldConverter(typeof(StringNumberCharConverter), "06", "", "", "false")]
        public decimal aliquotaDeIcmsAplicavel;
    }
}
