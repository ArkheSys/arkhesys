using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data.Linq;

namespace Modelo
{
    public partial class Configuracao : IMetodosCwork
    {
        public int GetFaturamentoInativoSistema(DataClassesDataContext db)
        {
            int ativo;
            ativo = Convert.ToInt32((from c in db.Configuracaos
                                     select c.PermitirFatClienteNaoAtivo).Single());

            return ativo;
        }


        public static Modelo.Configuracao GetConfiguracao(Modelo.DataClassesDataContext db)
        {
            Modelo.Configuracao objConfiguracao = (from c in db.Configuracaos
                                                   select c).First();
            return objConfiguracao;
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

        #endregion
    }

}
