using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using System.Reflection;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using Modelo;

namespace cwkGestao.Importacao.ArquivosNFe
{
    internal class SessionProvider
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _config;

        static SessionProvider()
        {
            _config = new Configuration();

            _config.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            _config.SetProperty("dialect", "NHibernate.Dialect.MsSql2008Dialect");
            _config.SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            _config.SetProperty("connection.connection_string", cwkGlobal.CONN_STRING);
            _config.SetProperty("proxyfactory.factory_class", "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");

            _config.AddAssembly(Assembly.GetCallingAssembly());
            _config.AddAssembly("cwkGestao.Repositorio");
            
            _sessionFactory = Config.BuildSessionFactory();
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        private static Configuration Config
        {
            get
            {
                return _config;
            }
        }
    }
}
