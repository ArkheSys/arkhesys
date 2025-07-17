using cwkGestao.Modelo;
using cwkGestao.Repositorio.cwkGestaoNHibernate;
using cwkGestao.Repositorio.cwkGestaoNHibernate.Helpers;
using cwkGestao.Repositorio.Interfaces;

namespace cwkGestao.Repositorio
{
    public class RepositorioFactory<T> where T : ModeloBase, new()
    {
        public static IRepositorio<T> GetRepositorio()
        {
            return (IRepositorio<T>) RepositoriosNHibernate.Repositorios[typeof(T)];
        }

        public static void Start() { }

        public static object getSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }
    }
}