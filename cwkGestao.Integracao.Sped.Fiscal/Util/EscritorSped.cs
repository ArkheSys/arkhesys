using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using System.Collections;
using System.IO;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Util
{
    public static class EscritorSped
    {
        public static void EscritorBloco(Bloco bloco, string filename)
        {
            PropertyInfo[] props = bloco.GetType().GetProperties();
            foreach (PropertyInfo item in props)
            {
                if (item.GetValue(bloco, null) == null)
                {
                    continue;
                }
                if (item.PropertyType.BaseType == typeof(Registro))
                {
                    EscritorRegistro((Registro)item.GetValue(bloco, null), filename);
                }
                else if (item.PropertyType.BaseType == typeof(Bloco))
                {
                    EscritorBloco((Bloco)item.GetValue(bloco, null), filename);
                }
                else if (IsGenericList(item.PropertyType))
                {
                    var ienum = item.GetValue(bloco, null) as IEnumerable;
                    if (ienum != null)
                    {
                        foreach (var itemLista in ienum)
                        {
                            if (itemLista.GetType().BaseType == typeof(Registro))
                            {
                                EscritorRegistro((Registro)itemLista, filename);
                            }
                            if (itemLista.GetType().BaseType == typeof(Bloco))
                            {
                                EscritorBloco((Bloco)itemLista, filename);
                            }
                        }
                    }
                }
            }
        }

        private static void EscritorRegistro(Registro r, string arquivo)
        {
            FileHelperEngine engine = new FileHelperEngine(r.GetType());
            engine.AfterWriteRecord += new FileHelpers.Events.AfterWriteHandler<object>(engine_AfterWriteRecord);
            List<Registro> l = new List<Registro>() { r };
            if (!File.Exists(arquivo))
            {
                var arq = File.Create(arquivo);
                arq.Close();
            }
            engine.AppendToFile(arquivo, l.ToArray());
        }

        private static void engine_AfterWriteRecord(EngineBase engine, FileHelpers.Events.AfterWriteEventArgs<object> e)
        {
            e.RecordLine = "|" + e.RecordLine + "|";
        }

        public static bool IsGenericList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Dictionary<string, int> ConcatDictionary(Dictionary<string, int> d1, Dictionary<string, int> d2)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var key in d1.Keys)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, d1[key]);
                }
                else
                {
                    result[key] += d1[key];
                }
            }

            foreach (var key in d2.Keys)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, d2[key]);
                }
                else
                {
                    result[key] += d2[key];
                }
            }

            return result;
        }
    }
}
