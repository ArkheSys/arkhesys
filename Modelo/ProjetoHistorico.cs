using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class ProjetoHistorico : Modelo.IMetodosCwork
    {
        #region IMetodosCwork Members

        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.ProjetoHistoricos
                    select new
                    {
                       ID = p.ID,
                       IDProjeto = p.IDProjeto,
                       Data = p.Data,
                       Descricao = p.Descricao,
                    };
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, string pFiltro)
        {
            IQueryable lista;
            lista = from p in db.ProjetoHistoricos
                    where p.Descricao.ToString() == pFiltro ||
                    p.Data.ToString().Contains(pFiltro)
                    select new
                    {
                       ID = p.ID,
                       IDProjeto = p.IDProjeto,
                       Data = p.Data,
                       Descricao = p.Descricao,
                    };
            return lista;
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
            return (from u in db.ProjetoHistoricos where u.ID == pID select u).Single();
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

        public IQueryable GetProjetos(DataClassesDataContext db, int pID) 
        {
            IQueryable lista;
            lista = (from p in db.ProjetoHistoricos
                     where p.IDProjeto == pID
                     orderby p.Data, p.ID
                     select new
                     {
                         ID = p.ID,
                         Data = p.Data,
                         Descricao = p.Descricao
                     });
            return lista;
        }
    }
}
