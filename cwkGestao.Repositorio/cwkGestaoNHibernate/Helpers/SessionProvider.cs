using System;
using System.Reflection;
using Modelo;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Tool.hbm2ddl;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Helpers
{
    internal class SessionProvider
    {
        static SessionProvider()
        {
            Config = new Configuration();
            Config.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            Config.SetProperty("dialect", "NHibernate.Dialect.MsSql2008Dialect");
            Config.SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            Config.SetProperty("connection.connection_string", cwkGlobal.CONN_STRING);
            Config.SetProperty("proxyfactory.factory_class", "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");
            
            Config.SetProperty("show_sql", "false");
            Config.SetProperty("adonet.batch_size", "100");
            Config.SetProperty("max_fetch_depth", "0");
            Config.SetProperty("cache.use_query_cache", "true");
            Config.SetProperty("cache.use_second_level_cache", "true");
            Config.SetProperty("default_batch_fetch_size", "200");

            Config.Cache(x =>
            {
                x.UseMinimalPuts = true;
            });

            Config.AddAssembly(Assembly.GetCallingAssembly());
            try
            {
                SessionFactory = Config.BuildSessionFactory();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public static ISessionFactory SessionFactory { get; }

        private static Configuration Config { get; }

        public static void RebuildSchema()
        {
            var schema = new SchemaUpdate(Config);
            schema.Execute(true, true);
            //schema.Create(true, true);
        }
    }
}