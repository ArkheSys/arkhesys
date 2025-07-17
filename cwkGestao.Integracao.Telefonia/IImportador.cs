using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.Telefonia
{
    public interface IImportador
    {
        IEnumerable<Ligacao> Importar();
        IEnumerable<Ligacao> ImportarFixoECelular();
        int GetQuantidadeRegistros();
    }
}
