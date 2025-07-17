using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using cwkGestao.Integracao.Sped.Fiscal.Util;

namespace cwkGestao.Integracao.Sped.Fiscal.Interfaces
{
    public abstract class Bloco
    {
        public Dictionary<string, object> ParamsBloco { get; set; }

        public abstract void GerarBloco();

        public int GetContagemRegistros()
        {
            int contagem = 0;
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(this, null) == null)
                {
                    continue;
                }
                if (prop.PropertyType.BaseType == typeof(Registro))
                {
                    contagem++;
                }
                else if (prop.PropertyType.BaseType == typeof(Bloco))
                {
                    contagem += ((Bloco)prop.GetValue(this, null)).GetContagemRegistros();
                }
                else if (Util.EscritorSped.IsGenericList(prop.PropertyType))
                {
                    var ienum = prop.GetValue(this, null) as IEnumerable;
                    if (ienum != null)
                    {
                        foreach (var item in ienum)
                        {
                            if (item.GetType().BaseType == typeof(Registro))
                            {
                                contagem++;
                            }
                            else if (item.GetType().BaseType == typeof(Bloco))
                            {
                                contagem += ((Bloco)item).GetContagemRegistros();
                            }
                        }
                    }
                }
            }
            return contagem;
        }

        public string GetStringSomenteAlfanumerico(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return String.Empty;
            }
            string r = new string(s.ToCharArray().Where((c => char.IsLetterOrDigit(c))).ToArray());
            return r;
        }

        public Dictionary<string, int> GetContagemRegistrosIndividuais()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

#pragma warning disable CS0219 // A variável "contagem" é atribuída, mas seu valor nunca é usado
            int contagem = 0;
#pragma warning restore CS0219 // A variável "contagem" é atribuída, mas seu valor nunca é usado
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(this, null) == null)
                {
                    continue;
                }
                if (prop.PropertyType.BaseType == typeof(Registro))
                {
                    string nome = prop.PropertyType.Name.Replace("Reg_", "");
                    if (!result.ContainsKey(nome))
                    {
                        result.Add(nome, 1);
                    }
                    else
                    {
                        result[nome] += 1;
                    }
                }
                else if (prop.PropertyType.BaseType == typeof(Bloco))
                {
                    result = EscritorSped.ConcatDictionary(result,((Bloco)prop.GetValue(this, null)).GetContagemRegistrosIndividuais());
                }
                else if (Util.EscritorSped.IsGenericList(prop.PropertyType))
                {
                    var ienum = prop.GetValue(this, null) as IEnumerable;
                    if (ienum != null)
                    {
                        foreach (var item in ienum)
                        {
                            if (item.GetType().BaseType == typeof(Registro))
                            {
                                string nome = item.GetType().Name.Replace("Reg_", "");
                                if (!result.ContainsKey(nome))
                                {
                                    result.Add(nome, 1);
                                }
                                else
                                {
                                    result[nome] += 1;
                                }
                            }
                            else if (item.GetType().BaseType == typeof(Bloco))
                            {
                                result = EscritorSped.ConcatDictionary(result, ((Bloco)item).GetContagemRegistrosIndividuais());
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
