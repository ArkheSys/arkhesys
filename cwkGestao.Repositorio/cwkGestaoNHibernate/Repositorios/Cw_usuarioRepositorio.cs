using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Cw_usuarioRepositorio : RepositorioAbstrato<Cw_usuario>, IRepositorioCw_usuario
    {
        protected override string HqlLoadById()
        {
            return "from Cw_usuario n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Cw_usuario n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        //public override Cw_usuario LoadObjectById(int id)
        //{
        //    using (var session = GetSession())
        //    {
        //        Cw_usuario cw = session.Get<Cw_usuario>(id);
        //        if (cw != null)
        //        {
        //            InitializeCamposUsuario(cw);
        //        }
        //        return cw;
        //    }
        //}

        //private void InitializeCamposUsuario(Cw_usuario cw)
        //{
        //    try
        //    {
        //        NHibernateUtil.Initialize(cw.Vendedor);
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
            
        //}

        public Cw_usuario FindByLogin(string login)
        {
            Cw_usuario user = new Cw_usuario();

            using (var session = GetSession())
            {
                
                string hql = @"from Cw_usuario c 
                               left join fetch c.Vendedor v
                               where c.Login = :login";
                try
                {
                    user = session.CreateQuery(hql).SetString("login", login).List<Cw_usuario>().FirstOrDefault();
                    //InitializeCamposUsuario(user);
                    return user;
                }
                catch (Exception z)
                {
                    
                    throw z;
                }
            }
#pragma warning disable CS0162 // Código inacessível detectado
            return null;
#pragma warning restore CS0162 // Código inacessível detectado
        }
    }
}
