using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Comercial
{
    public class HeaderControleRPS
    {
        public string formato { get; private set; }
        public string Grupo { get; set; }
        public string CNPJ { get; set; }
        public string padrao { get; private set; }
        public string NomeCidade { get; set; }

        public HeaderControleRPS()
        {
            this.formato = "tx2";
            this.Grupo = "";
            this.CNPJ = "";
            this.padrao = "TecnoNFSe";
            this.NomeCidade = "";
        }

        public HeaderControleRPS(string grupo, string cnpj, string cidade)
        {
            this.formato = "tx2";
            this.Grupo = grupo;
            this.CNPJ = cnpj;
            this.padrao = "TecnoNFSe";
            this.NomeCidade = cidade;
        }
    }
}
