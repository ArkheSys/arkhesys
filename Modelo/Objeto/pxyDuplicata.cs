using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyDuplicata : pxyBase
    {
        public String Local { get; set; }
        public DateTime? DataEmissao { get; set; }
        public Decimal? ValorFatura { get; set; }
        public Decimal? ValorDuplicata { get; set; }
        public String NumFatura { get; set; }
        public String NumOrdemDuplicata { get; set; }
        public String Vencimento { get; set; }
        public String NomeSacado { get; set; }
        public String EnderecoSacado { get; set; }
        public String CidadeSacado { get; set; }
        public String UFSacado { get; set; }
        public String PracaSacado { get; set; }
        public String InscrSacado { get; set; }
        public String CNPJSacado { get; set; }
        public String UFPracaPgtoSacado { get; set; }
        public String InscrEstSacado { get; set; }
        public String NomeEmpresa { get; set; }
        public String EnderecoEmpresa { get; set; }
        public String NumEmpresa { get; set; }
        public String BairroEmpresa { get; set; }
        public String CidadeEmpresa { get; set; }
        public String UFEmpresa { get; set; }
        public String FoneEmpresa { get; set; }
        public String FaxEmpresa { get; set; }
        public String CNPJEmpresa { get; set; }
        public String CEPEmpresa { get; set; }
        public String InscricaoEstadual { get; set; }
        public DateTime? OrderVencimento { get; set; }
    }
}
