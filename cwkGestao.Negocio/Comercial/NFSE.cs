
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Comercial
{
    public class NFSE
    {
        public HeaderControleRPS Controle { get; set; }
        public HeaderRPS Cabecalho { get; set; }
        public IList<DadosRPS> Dados { get; set; }

        public NFSE()
        {
            HeaderControleRPS controle = new HeaderControleRPS();
            HeaderRPS header = new HeaderRPS();
            IList<DadosRPS> dados = new List<DadosRPS>();
            Cabecalho = header;
            Dados = dados;
        }

        public NFSE(HeaderControleRPS controle, HeaderRPS header, IList<DadosRPS> ListaRPS)
        {
            this.Controle = controle;
            this.Cabecalho = header;
            this.Dados = ListaRPS;
        }


    }
}
