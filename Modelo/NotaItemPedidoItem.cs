using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class NotaItemPedidoItem : Modelo.IMetodosCwork
    {

        //public NotaItemPedidoItem()
        //{
        //}
        
        public NotaItemPedidoItem(NotaItem notaitem, PedidoItem pedidoitem)
        {
        }
        /*
        #region set-gets
        public override int ID { get; set; }
        
        public virtual NotaItem NotaItem { get; set; }
        
        public virtual PedidoItem PedidoItem { get; set; }
        #endregion         
        */

        partial void OnLoaded()
        {
            throw new NotImplementedException();
        }

        partial void OnCreated()
        {
            throw new NotImplementedException();
        }

        public IQueryable getLista(DataClassesDataContext db) {
            IQueryable lista;
            lista = null;
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, string pFiltro)
        {
            IQueryable lista;
            lista = null;
            return lista;
        }
        
        public IList getListaCombo(DataClassesDataContext db)
        {
            throw new NotImplementedException();
        }
        
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = null;
            return lista;
        }

        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            throw new NotImplementedException();
        }

        public int getID(DataClassesDataContext db, string pValor)
        {
            throw new NotImplementedException();
        }

        public int RetornaMAXCodigo(DataClassesDataContext db)
        {
            throw new NotImplementedException();        
        }
    }
}
