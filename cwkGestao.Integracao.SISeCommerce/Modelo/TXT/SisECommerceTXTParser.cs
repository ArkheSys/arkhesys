using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Reflection;

namespace cwkGestao.Integracao.SISeCommerce.Util
{
    public static class SISeCommerceTXTParser
    {
        public static IList<PedidoSISeCText> ParseText(string caminho)
        {
            List<string> ArquivoOriginal = File.ReadAllLines(caminho).ToList();
            Dictionary<string, Dictionary<string, object>> PreProcessado = LeitorArquivoTexto(ArquivoOriginal);
            IList<PedidoSISeCText> Pedidos = ArquivoTextoTransformer(PreProcessado);
            return Pedidos;
        }

        #region metodos auxiliares

        private static Dictionary<string, Dictionary<string, object>> LeitorArquivoTexto (List<string> ArquivoOriginal)
        {
            Dictionary<string, Dictionary<string, object>> preprocessado = new Dictionary<string, Dictionary<string, object>>();

            string pedidoCorrente = "";
            int contadorPedidoItem = 0;

            #region Leitura e preparação do objeto Pedido

            for (int i = 0; i < ArquivoOriginal.Count; i++)
            {
                if (ArquivoOriginal[i].StartsWith("01|"))
                {
                    string linhatratada = ArquivoOriginal[i].Remove(0, 3);
                    string[] linhasplitted = linhatratada.Split(':');
                    preprocessado.Add(linhasplitted.Last(), new Dictionary<string, object>());
                    pedidoCorrente = linhasplitted.Last();
                    preprocessado[pedidoCorrente].Add(linhasplitted.First(), linhasplitted.Last());
                    contadorPedidoItem = 0;
                }
                else
                {
                    if (ArquivoOriginal[i].StartsWith("9X|"))
                    {
                        continue;
                    }
                    string linhatratada = ArquivoOriginal[i];
                    if (ArquivoOriginal[i].StartsWith("99|"))
                    {
                        //adiciona tag de pedido item, para permitir tratamento posterior dos itens do pedido
                        contadorPedidoItem++;
                        linhatratada = linhatratada.Replace("99|", "99|PEDIDO_ITEM" + contadorPedidoItem.ToString() + ":");
                    }
                    linhatratada = linhatratada.Remove(0, 3);
                    string[] linhasplitted = linhatratada.Split(':');

                    if (!preprocessado[pedidoCorrente].ContainsKey(linhasplitted.First()))
                    {
                        preprocessado[pedidoCorrente].Add(linhasplitted.First(), linhasplitted.Last());
                    }
                    else
                    {
                        preprocessado[pedidoCorrente][linhasplitted.First()] =
                            preprocessado[pedidoCorrente][linhasplitted.First()] +
                            linhasplitted.Last();
                    }
                }
            }

            #endregion

            #region Leitura e preparação dos objetos PedidoItem

            foreach (string key in preprocessado.Keys)
            {
                Dictionary<string, Dictionary<string, string>> PedidoItens = new Dictionary<string, Dictionary<string, string>>();
                IList<string> listaItems = preprocessado[key].Keys.Where(k => k.Contains("PEDIDO_ITEM")).ToList();
                foreach (string key2 in listaItems)
                {
                    string item = (string)preprocessado[key][key2];
                    string[] valores = item.Split('|');
                    string[] tags = new string[8] { 
                        "CODIGO_PRODUTO", "PRODUTO", "PRECO_UNITARIO", 
                        "QUANTIDADE", "PRECO_TOTAL", "PESO_EM_KG", 
                        "OPCOES_PRODUTO", "COD_REFERENCIA" };
                    Dictionary<string, string> pedidoItem = new Dictionary<string, string>();
                    for (int i = 0; i < tags.Length; i++)
                    {
                        pedidoItem.Add(tags[i], valores[i]);
                    }
                    string codProd = pedidoItem["CODIGO_PRODUTO"];
                    PedidoItens.Add(codProd, pedidoItem);

                }
                foreach (string item in listaItems)
                {
                    preprocessado[key].Remove(item);
                }
                preprocessado[key].Add("PEDIDO_ITENS", PedidoItens);

            }

            #endregion

            return preprocessado;
        }

        private static IList<PedidoSISeCText> ArquivoTextoTransformer(Dictionary<string, Dictionary<string, object>> Preprocessado)
        {
            IList<PedidoSISeCText> result = new List<PedidoSISeCText>();
            foreach (string key in Preprocessado.Keys)
            {
                PedidoSISeCText pedido = new PedidoSISeCText();
                PropertyInfo[] propsPedido = pedido.GetType().GetProperties();
                foreach (PropertyInfo prop in propsPedido)
                {
                    if (!(prop.Name == "PEDIDO_ITENS"))
                    {
                        try
                        {
                            PropertySetter(pedido, prop, Preprocessado[key][prop.Name]);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        
                    }
                    else
                    {
                        pedido.PEDIDO_ITENS = new List<PedidoItemSISeCText>();
                        Dictionary<string, Dictionary<string, string>> DicPedidoItens = 
                            (Dictionary<string, Dictionary<string, string>>)Preprocessado[key][prop.Name];
                        foreach (string chave in DicPedidoItens.Keys)
                        {
                            PedidoItemSISeCText item = new PedidoItemSISeCText();
                            PropertyInfo[] propsItem = item.GetType().GetProperties();
                            foreach (PropertyInfo propItem in propsItem)
                            {
                                try
                                {
                                    PropertySetter(item, propItem, DicPedidoItens[chave][propItem.Name]);
                                }
                                catch (Exception e)
                                {
                                    
                                    throw e;
                                }
                            }
                            pedido.PEDIDO_ITENS.Add(item);
                        }
                    }
                }
                result.Add(pedido);
            }
            return result;
        }

        private static void PropertySetter(object Variavel, PropertyInfo Propriedade, object Valor)
        {
            if (!(Propriedade.PropertyType == typeof(DateTime)))
            {
                string str = (string)Valor;

                if ((Propriedade.PropertyType == typeof(decimal)) || ((Propriedade.PropertyType == typeof(Int32))))
                {
                    if (String.IsNullOrEmpty(str))
                    {
                        str = "0";
                    }
                    str = str.Replace(".", ",");
                }

                var valor = Convert.ChangeType(str, Propriedade.PropertyType, null);
                Propriedade.SetValue(Variavel, valor, null);
            }
            else
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("pt-BR", true);
                string date = (string)Valor;
                if (!String.IsNullOrEmpty(date))
                {
                    var valor = DateTime.Parse(date, culture,
                        System.Globalization.DateTimeStyles.AssumeLocal);
                    Propriedade.SetValue(Variavel, valor, null);
                }
                else
                {
                    Propriedade.SetValue(Variavel, null, null);
                }
            }
        }

        #endregion
    }
}
