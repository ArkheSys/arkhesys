using System;
using System.Collections.Generic;
using System.Data;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    internal abstract partial class RepositorioAbstrato<T>
    {
        public void RegisterDeleteIntoTransaction(T entity)
        {
            RegisterIntoTransaction(entity, Acao.Excluir);
        }

        public void RegisterNewIntoTransaction(T entity)
        {
            Acao acao = Acao.Alterar;
            if (entity.ID == 0)
                acao = Acao.Incluir;

            RegisterIntoTransaction(entity, acao);
        }

        private void RegisterIntoTransaction(T entity, Acao acao)
        {
            var novo = new UnitOfWork.Tuple {entity = entity, acao = acao};
            UnitOfWork.entities.Add(novo);
        }

        public void CommitUnitOfWorkTransaction()
        {
            using (var session = GetSession())
            using (var trans = session.BeginTransaction(IsolationLevel.ReadCommitted))
                try
                {
                    foreach (var item in UnitOfWork.entities)
                        if (item.acao == Acao.Excluir)
                            session.Delete(item.entity);
                        else
                            session.SaveOrUpdate(item.entity);
                    trans.Commit();
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                    {
                        Exception y = new Exception(exc.InnerException.Message.Replace("The transaction ended in the trigger. The batch has been aborted.", ""));
                        throw y;
                    }
                    else
                    {
                        throw exc;
                    }
                }
                finally
                {
                    UnitOfWork.entities.Clear();
                    session.Close();
                }
        }

        public void RollBackUnitOfWorkTransaction()
        {
            UnitOfWork.entities.Clear();
        }
    }

    internal class UnitOfWork
    {
#pragma warning disable CS0659 // "UnitOfWork.Tuple" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
        internal struct Tuple
#pragma warning restore CS0659 // "UnitOfWork.Tuple" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
        {
            internal ModeloBase entity;
            internal Acao acao;

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(Tuple))
                {
                    var comparado = (Tuple) obj;
                    return acao == comparado.acao && entity.Equals(comparado.entity);
                }

                return false;
            }
        }

        internal static List<Tuple> entities = new List<Tuple>();
    }
}