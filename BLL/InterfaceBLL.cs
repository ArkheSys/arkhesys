using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public interface InterfaceBLL
    {
        IQueryable getLista();

        IQueryable getLista(String pFiltro);

        IQueryable getListaCombo();

        IQueryable getListaCombo(String pFiltro);
        
        Object getObjeto(int pID);

        int getID(string pValor);

        Int32 RetornaMAXCodigo();
    }
}
