using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class CotacaoProduto : Modelo.IMetodosCwork
    {

        public int RetornaMAXCodigo(Modelo.Cotacao cotacao)
        {
            int maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from p in cotacao.CotacaoProdutos
                             where p.Codigo == cotacao.CotacaoProdutos.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
                        
        #region IMetodosCwork Members

        public IQueryable getLista(DataClassesDataContext db)
        {
            throw new NotImplementedException();
        }

        public IQueryable getLista(DataClassesDataContext db, string pFiltro)
        {
            throw new NotImplementedException();
        }

        public IList getListaCombo(DataClassesDataContext db)
        {
            throw new NotImplementedException();
        }

        public IQueryable getListaCombo(DataClassesDataContext db, string pFiltro)
        {
            throw new NotImplementedException();
        }

        public object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.CotacaoProdutos where u.ID == pID select u).Single();
        }

        public int getID(DataClassesDataContext db, string pValor)
        {
            throw new NotImplementedException();
        }

        public int RetornaMAXCodigo(DataClassesDataContext db)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
