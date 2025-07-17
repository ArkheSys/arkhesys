using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public interface IExportador<T>
    {
        void Exportar(T objeto);
    }
}
