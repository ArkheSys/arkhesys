using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.Interfaces
{
    public interface IRepositorio<T>
    {
        T LoadObjectById(int id);
        void Create(T saveObj);
        void Merge(T updateObj);
        void Update(T updateObj);
        void Delete(T delObj);
        IList<T> GetAll();

        IList<T> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros);

        //void Initialize(object completeObject, params object[] property);
        int MaxCodigo();

        //IList<T> GetByExample(T example);
        IList<T> GetByExample(object restricoes);

        void RegisterDeleteIntoTransaction(T entity);
        void RegisterNewIntoTransaction(T entity);
        void CommitUnitOfWorkTransaction();
        void RollBackUnitOfWorkTransaction();
        DateTime GetServerDateTime();
        string GetUsuarioLogado();
        void Refresh(T refreshObj);
    }
}