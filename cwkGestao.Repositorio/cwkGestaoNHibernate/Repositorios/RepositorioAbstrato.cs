using System;
using System.Collections.Generic;
using System.Linq;

using cwkControleUsuario;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.cwkGestaoNHibernate.Helpers;
using cwkGestao.Repositorio.Interfaces;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Exceptions;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    internal abstract partial class RepositorioAbstrato<T> : IRepositorio<T> where T : ModeloBase, new()
    {
        public Dictionary<string, object> ConfigsAdicionais { get; set; }

        protected static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public DateTime GetServerDateTime()
        {
            using (var session = GetSession())
            {
                string sql = "SELECT GETDATE()";
                DateTime retorno = (DateTime)session.CreateSQLQuery(sql).UniqueResult();
                return retorno;
            }
        }

        public string GetUsuarioLogado()
        {
            return Facade.getUsuarioLogado.Login;
        }

        public virtual T LoadObjectById(int id)
        {
            using (var session = GetSession())
                try
                {
                    string sql = HqlLoadById();
                    //T retorno = session.CreateQuery(sql).SetParameter("id", id).List<T>().FirstOrDefault();
                    T retorno = session.CreateQuery(sql).SetParameter("id", id).UniqueResult<T>();
                    return retorno;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    session.Close();
                }
        }

        protected abstract string HqlLoadById();

        public virtual void Create(T saveObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                    try
                    {
                        session.Save(saveObj);
                        trans.Commit();
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        trans.Dispose();
                    }

                session.Close();
            }
        }

        public virtual void Delete(T delObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                    try
                    {
                        session.Delete(delObj);
                        trans.Commit();
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        trans.Dispose();
                    }

                session.Close();
            }
        }

        public virtual int MaxCodigo()
        {
            using (var session = GetSession())
                try
                {
                    return session.CreateQuery("select max(Codigo) from " + typeof(T).Name).UniqueResult<int>() + 1;
                }
                catch (GenericADOException)
                {
                    return 1;
                }
        }

        public void Merge(T updateObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                    try
                    {
                        session.Merge(updateObj);
                        trans.Commit();
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        trans.Dispose();
                    }

                session.Close();
            }
        }

        public void Update(T updateObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                    try
                    {
                        session.Update(updateObj);
                        trans.Commit();
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        trans.Dispose();
                    }

                session.Close();
            }
        }

        public void Refresh(T refreshObj)
        {
            using (var session = GetSession()) session.Refresh(refreshObj);
        }

        public virtual IList<T> GetAll()
        {
            using (var session = GetSession())
                try
                {
                    string hql = "";
                    try
                    {
                        hql = hqlGetAll();
                    }
                    catch (Exception)
                    {
                        hql = "from " + typeof(T).Name + " x ";
                    }

                    // codigo original
                    HashSet<T> set = new HashSet<T>(session.CreateQuery(hql).List<T>());
                    ///HashSet<T> set = new HashSet<T>(session.CreateQuery(hql).SetFirstResult(0).SetMaxResults(10).List<T>());
                    return set.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    session.Close();
                }
        }

        protected abstract string hqlGetAll();

        protected abstract string hqlGetAllByPeriod(DateTime start, DateTime end);

        public virtual IList<T> GetByExample(T example)
        {
            using (var session = GetSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                return criteria.Add(Example.Create(example)
                        .ExcludeNone()
                        .EnableLike(MatchMode.Anywhere)
                        .IgnoreCase()
                        .ExcludeZeroes())
                    .List<T>();
            }
        }

        public void Initialize(object completeObject, params object[] property)
        {
            using (var session = GetSession())
            {
                session.Lock(completeObject, LockMode.None);
                foreach (var item in property) NHibernateUtil.Initialize(item);
            }
        }

        public IList<T> GetByExample(object restricoes)
        {
            try
            {
                using (var session = GetSession())
                {
                    ICriteria criteria = session.CreateCriteria(typeof(T));

                    if (restricoes != null)
                    {
                        ICriterion criterion = (ICriterion)restricoes;

                        return criteria.Add(criterion).List<T>();
                    }

                    return criteria.List<T>();
                }
            }
            catch (InvalidCastException)
            {
                throw new Exception("Restrição passada para consulta não é do tipo ICriterion.");
            }
        }

        #region IRepositorio<T> Members

        public virtual IList<T> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros)
        {
            IList<T> result = new List<T>();
            using (var session = GetSession())
                try
                {
                    string hql = "";
                    try
                    {
                        hql = hqlGetAllByPeriod(start, end);
                    }
                    catch (Exception)
                    {
                        hql = "from " + typeof(T).Name + " x where x.IncData between :dataInicio and :dataFim";
                    }

                    result = session.CreateQuery(hql).SetDateTime("dataInicio", start).SetDateTime("dataFim", end).List<T>();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    session.Close();
                }

            return result;
        }

        #endregion
    }
}