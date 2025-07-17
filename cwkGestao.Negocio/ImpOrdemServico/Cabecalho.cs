using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.ImpOrdemServico
{
    public class Cabecalho
    {
        public bool EhCondensado { get; set; }
        public bool EhNegrito { get; set; }
        public bool EhExpandido { get; set; }
        public bool RepeteNovaFolha { get; set; }

        public Cabecalho()
        {
            EhCondensado = false;
            EhNegrito = false;
            EhExpandido = false;
            RepeteNovaFolha = false;
        }
    }
}
