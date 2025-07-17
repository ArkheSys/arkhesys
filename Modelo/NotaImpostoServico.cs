using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class NotaImpostoServico
    {
        public String NomeImposto { get { return ServicoImposto != null ? ServicoImposto.Nome : ""; } }

    }
}
