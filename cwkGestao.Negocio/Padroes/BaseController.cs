using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces;

namespace cwkGestao.Negocio
{
    public abstract class BaseController<T> where T : ModeloBase, new()
    {
        protected static IRepositorio<T> repositorioT;

        public virtual Dictionary<string, string> Salvar(T objeto, Acao acao)
        {
            TrataDatas(objeto);
            Dictionary<string, string> ret = new Dictionary<string, string>();
            try
            {
                if (acao == Acao.Excluir)
                {
                    Delete(objeto);
                    return new Dictionary<string, string>();
                }

                ret = ValidaObjeto(objeto);
                if (ret.Count == 0)
                {
                    SetaCamposDateTimeEstourados(objeto, DateTime.Now.AddYears(-200));
                    SetaCamposIncAltUsuario(objeto, acao);
                    switch (acao)
                    {
                        case Acao.Incluir:
                            Create(objeto);
                            break;
                        case Acao.Alterar:
                            try
                            {
                                Update(objeto);
                            }
                            catch (Exception e)
                            {
                                if (e.GetType().Name == "HibernateException")
                                    Merge(objeto);
                            }
                            break;
                    }

                    return ret;
                }

                return ret;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Exception y = new Exception(ex.InnerException.Message);
                    throw y;
                }

                throw ex;
            }
        }

        protected void SetaCamposIncAltUsuario(T objeto, Acao acao)
        {
            //DateTime agoraNoServidor = repositorioT.GetServerDateTime();
            DateTime agoraNoServidor = DateTime.Now;
            switch (acao)
            {
                case Acao.Incluir:
                    objeto.IncData = agoraNoServidor;
                    objeto.IncUsuario = repositorioT.GetUsuarioLogado();
                    objeto.AltData = null;
                    objeto.AltUsuario = null;
                    break;
                case Acao.Alterar:
                    objeto.AltData = agoraNoServidor;
                    objeto.AltUsuario = repositorioT.GetUsuarioLogado();
                    break;
            }
        }

        public static void TrataDatas(object objeto)
        {
            if (objeto != null)
                foreach (PropertyInfo prop in objeto.GetType().GetProperties().Where(p => p.PropertyType.Equals(typeof(DateTime))))
                    if (prop.GetValue(objeto, null) != null && Convert.ToDateTime(prop.GetValue(objeto, null)).Year < 1800)

                        prop.SetValue(objeto, new DateTime(1800, 1, 1), null);
        }

        public static IList<string> ListaDateTimesZerados(object objeto1)
        {
            List<string> retorno = new List<string>();
            if (objeto1 != null)
            {
                if (objeto1.GetType().GetGenericArguments().Count() == 0)
                    foreach (PropertyInfo propertyInfo in objeto1.GetType().GetProperties())
                        try
                        {
                            object value = propertyInfo.GetValue(objeto1, null);
                            if (propertyInfo.PropertyType == typeof(DateTime))
                            {
                                if ((DateTime)value == DateTime.MinValue)
                                    retorno.Add(objeto1.GetType().Name + " " + propertyInfo.Name + " " + value);
                            }
                            else
                            {
                                retorno.AddRange(ListaDateTimesZerados(value));
                            }
                        }
                        catch { }
                else
                    foreach (object objeto in (IEnumerable)objeto1)
                        retorno.AddRange(ListaDateTimesZerados(objeto));
            }

            return retorno;
        }

        protected void SetaCamposDateTimeEstourados(T objeto, DateTime dateTime)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
                if (prop.PropertyType.Equals(typeof(DateTime)) && Convert.ToDateTime(prop.GetValue(objeto, null)).Year < 1800)
                    prop.SetValue(objeto, dateTime, null);
        }

        public abstract Dictionary<string, string> ValidaObjeto(T objeto);

        protected virtual void Create(T objeto)
        {
            repositorioT.Create(objeto);
        }

        protected virtual void Update(T objeto)
        {
            repositorioT.Update(objeto);
        }

        protected virtual void Merge(T objeto)
        {
            repositorioT.Merge(objeto);
        }

        protected virtual void Delete(T objeto)
        {
            repositorioT.Delete(objeto);
        }

        public virtual IList<T> GetAll()
        {
            return repositorioT.GetAll();
        }

        public virtual T LoadObjectById(int id)
        {
            return repositorioT.LoadObjectById(id);
        }

        public virtual int MaxCodigo()
        {
            return repositorioT.MaxCodigo();
        }

        public object getSession()
        {
            return RepositorioFactory<Pessoa>.getSession();
        }

        public void SetRepositorio(object objeto)
        {
            repositorioT = (IRepositorio<T>)objeto;
            GetType().GetField("repositorio" + typeof(T).Name, BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, objeto);
        }

        public IList<T> GetByExample(object restricoes)
        {
            return repositorioT.GetByExample(restricoes);
        }

        #region Unit Of Work

        public virtual void RegisterDeleteIntoTransaction(T entity)
        {
            TrataDatas(entity);
            repositorioT.RegisterDeleteIntoTransaction(entity);
        }

        public virtual void RegisterDeleteIntoTransaction(IList<T> entities)
        {
            foreach (var entity in entities) RegisterDeleteIntoTransaction(entity);
        }

        public virtual void RegisterNewIntoTransaction(T entity)
        {
            Acao acao = Acao.Alterar;
            if (entity.ID == 0)
                acao = Acao.Incluir;

            TrataDatas(entity);
            SetaCamposDateTimeEstourados(entity, DateTime.Now.AddYears(-200));
            SetaCamposIncAltUsuario(entity, acao);

            repositorioT.RegisterNewIntoTransaction(entity);
        }

        public virtual void RegisterNewIntoTransaction(IEnumerable<T> entities)
        {
            foreach (var entity in entities) RegisterNewIntoTransaction(entity);
        }

        public virtual void CommitUnitOfWorkTransaction()
        {
            repositorioT.CommitUnitOfWorkTransaction();
        }

        public virtual void RollBackUnitOfWorkTransaction()
        {
            repositorioT.RollBackUnitOfWorkTransaction();
        }

        #endregion

        public void Clonar(T fonte, T destino)
        {
            foreach (PropertyInfo propertyInfo in fonte.GetType().GetProperties())
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    object valorFonte = propertyInfo.GetValue(fonte, null);

                    propertyInfo.SetValue(destino, valorFonte, null);
                }
        }

        public virtual IList<T> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros)
        {
            return repositorioT.GetAllByPeriod(start, end, parametros);
        }

        public virtual IList<string> GetAllCampos()
        {
            IList<string> listaCampos = new List<string>();
            PropertyInfo[] prop = typeof(T).GetProperties();
            foreach (PropertyInfo item in prop)
                if (item.GetCustomAttributes(typeof(InformacaoExibicao), false).Count() > 0)
                {
                    InformacaoExibicao campo = (InformacaoExibicao)item.GetCustomAttributes(typeof(InformacaoExibicao), false).First();
                    InformacaoExibicao modelo = (InformacaoExibicao)typeof(T).GetCustomAttributes(typeof(InformacaoExibicao), false).First();
                    listaCampos.Add(modelo.Header + "." + campo.Header);
                }

            return listaCampos;
        }

        public virtual string GetValorAtributo(T obj, string att)
        {
            try
            {
                string result;
                PropertyInfo[] props = typeof(T).GetProperties();
                PropertyInfo atributo = props.Where(
                    w => (w.GetCustomAttributes(typeof(InformacaoExibicao), false).Count() > 0 ? (InformacaoExibicao)w.GetCustomAttributes(typeof(InformacaoExibicao), false).First() : new InformacaoExibicao("")).Header == att
                ).FirstOrDefault();
                object resultado;
                //if (atributo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                //{
                //    NullableConverter converter = new NullableConverter(atributo.PropertyType);
                //    resultado = converter.ConvertTo(atributo.GetValue(obj, null), converter.UnderlyingType);
                //}
                //else
                //{
                resultado = atributo.GetValue(obj, null);
                //}
                result = resultado == null ? "" : resultado.ToString();
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}