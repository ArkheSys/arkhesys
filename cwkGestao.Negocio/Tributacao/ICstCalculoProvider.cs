using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Tributacao
{
    public interface ICstCalculoProvider
    {
        IList<string> PisCsts { get; }
        IList<string> IpiCsts { get; }
        IList<string> CofinsCsts { get; }

        IList<string> IcmsCsts { get; }
        IList<string> SubstituicaoTributariaCsts { get; }

        IList<string> CreditoCsts { get; }
    }
}
