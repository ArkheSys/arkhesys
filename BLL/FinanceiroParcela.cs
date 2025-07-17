using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class FinanceiroParcela
    {
        public List<Modelo.Objeto.FinanceiroParcela> GeraParcelas(Modelo.DataClassesDataContext pDb, int pIdCondicao, DateTime dataContabil, decimal valor)
        {
            List<Modelo.Objeto.FinanceiroParcela> listaParcelas = new List<Modelo.Objeto.FinanceiroParcela>();
            Modelo.Condicao objCondicao = pDb.Condicaos.Where(c => c.ID == pIdCondicao).Single();
            Modelo.Objeto.FinanceiroParcela objFinanceiroParcela;

            if (objCondicao.QtParcela == 0)
            {
                objFinanceiroParcela = new Modelo.Objeto.FinanceiroParcela();
                objFinanceiroParcela.DtContabil = dataContabil;
                objFinanceiroParcela.Parcela = 1;
                objFinanceiroParcela.Valor = valor;

                if (objCondicao.TipoDt == Modelo.TipoDataType.Dia.ToString())
                {
                    objFinanceiroParcela.Vencimento = dataContabil.Date.AddDays(Convert.ToDouble(objCondicao.CondicaoParcelas[0].DiaMes));
                }
                else
                {
                    objFinanceiroParcela.Vencimento = dataContabil.Date.AddMonths((int)objCondicao.CondicaoParcelas[0].DiaMes);
                }

                listaParcelas.Add(objFinanceiroParcela);
            }
            else
            {
                decimal valorParc = Decimal.Round(valor / objCondicao.QtParcela, 2);
                for (int i = 0; i < objCondicao.QtParcela; i++)
                {
                    objFinanceiroParcela = new Modelo.Objeto.FinanceiroParcela();
                    objFinanceiroParcela.DtContabil = dataContabil;
                    objFinanceiroParcela.Parcela = i + 1;
                    objFinanceiroParcela.Vencimento = dataContabil;

                    if (objCondicao.TipoDt == Modelo.TipoDataType.Dia.ToString())
                    {
                        objFinanceiroParcela.Vencimento = dataContabil.Date.AddDays(Convert.ToDouble(objCondicao.CondicaoParcelas[i].DiaMes));
                    }
                    else
                    {
                        objFinanceiroParcela.Vencimento = dataContabil.Date.AddMonths((int)objCondicao.CondicaoParcelas[i].DiaMes);
                    }

                    objFinanceiroParcela.Valor = valorParc;

                    if (i + 1 == objCondicao.QtParcela)
                    {
                        decimal valorComp = valorParc;
                        foreach (Modelo.Objeto.FinanceiroParcela item in listaParcelas)
                        {
                            valorComp += (decimal)item.Valor;
                        }
                        if (valor > valorComp)
                        {
                            objFinanceiroParcela.Valor += (valor - valorComp);
                        }
                        else
                        {
                            if (valor < valorComp)
                            {
                                objFinanceiroParcela.Valor -= (valorComp - valor);
                            }
                        }
                    }

                    listaParcelas.Add(objFinanceiroParcela);
                }
            }

            return listaParcelas;
        }

        public List<Modelo.Objeto.FinanceiroParcela> geraParcelasNew(Modelo.DataClassesDataContext pDb, int pIdCondicao, DateTime dataContabil, decimal pValor, int pIdTipoDocumento, decimal pPercAcrescimo, List<DateTime> listaVencimento)
        {
            List<Modelo.Objeto.FinanceiroParcela> listaParcelas = new List<Modelo.Objeto.FinanceiroParcela>();
            
            Modelo.Condicao objCondicao = pDb.Condicaos.Where(c => c.ID == pIdCondicao).Single();
            Modelo.Objeto.FinanceiroParcela objFinanceiroParcela;
            decimal valorfixo = 0;
            decimal total = 0;
            decimal valorcalc = 0;
            
            int diasatraso = 0;
            int qtdparcela = objCondicao.QtParcela;
            Decimal valordividido = 0;

            foreach (Modelo.CondicaoParcela c in objCondicao.CondicaoParcelas.OrderBy(sx => sx.DiaMes))
            {
                if (c.TipoDt != "Dia" && c.TipoDt != "Mês")
                {
                    throw new Exception("Tipo de dados inválido para o campo Tipo da Data na condição.");
                }
                if (objCondicao.Divide == 0)
                {
                    if (c.TipoVlr != "Valor" && c.TipoVlr != "Percentual")
                    {
                        throw new Exception("Tipo de dados inválido para o campo Tipo do Valor na condição.");
                    }
                    if (c.TipoVlr == "Percentual" && (c.VlrPerc == 0 || c.VlrPerc == null))
                    {
                        throw new Exception("Valor do percentual não pode ser 0(Zero)");
                    }
                }

                if (c.VlrPerc != null && c.TipoVlr == "Valor")
                {
                    valorfixo = valorfixo + (decimal)c.VlrPerc;
                }
            }

            if (valorfixo > (decimal)pValor)
            {
                throw new Exception("O valor fixado nas parcelas é maior que o valor total. Verifique.");
            }

            total = (decimal)pValor - valorfixo;
            valordividido = Math.Round((decimal)(total / qtdparcela), 2);

            if (listaVencimento == null)
            {
                listaVencimento = GeraListaVencimentos((DateTime)dataContabil, objCondicao);
            }
           
            listaVencimento = listaVencimento.OrderBy(s => s.Date).ToList();
            int count = 1;
            var x = objCondicao.CondicaoParcelas.OrderBy(con => con.DiaMes);
            foreach (Modelo.CondicaoParcela c in x)
            {
                objFinanceiroParcela = new Modelo.Objeto.FinanceiroParcela();
                objFinanceiroParcela.Parcela = c.Parcela;                
                objFinanceiroParcela.DtContabil = dataContabil;
                objFinanceiroParcela.Vencimento = listaVencimento[c.Parcela - 1];

                //if (c.TipoDt == "Dia")
                //{
                //    objFinanceiroParcela.Vencimento = Modelo.MetodosEstaticos.geraVencimento((DateTime)dataContabil, Modelo.TipoDataType.Dia, (int)c.DiaMes);
                //}
                //else
                //{
                //    objFinanceiroParcela.Vencimento = Modelo.MetodosEstaticos.geraVencimento((DateTime)dataContabil, Modelo.TipoDataType.Mês, (int)c.DiaMes);
                //}

                if (objCondicao.Divide == 1)
                {
                    if (objCondicao.QtParcela != 0)
                    {
                        if (count == 1)
                        {
                            objFinanceiroParcela.Valor = Math.Round((decimal)(total / qtdparcela), 2);                         
                        }
                        else
                        {
                            diasatraso = ((DateTime)objFinanceiroParcela.Vencimento - dataContabil).Days;
                            //objFinanceiroParcela.Valor = Math.Round((decimal)(total / objCondicao.QtParcela), 2) + (((Math.Round((decimal)(total / objCondicao.QtParcela), 2) * (pPercAcrescimo / 30)) / 100) * diasatraso);
                            objFinanceiroParcela.Valor = Math.Round((((valordividido * (pPercAcrescimo / 30)) / 100) * diasatraso) + valordividido, 2);
                        }
                    }
                    else
                    {
                        throw new Exception("Não foi configurado a quantidade de parcelas na condição.");
                    }
                }
                else
                {
                    if (c.TipoVlr == "Percentual")
                    {
                        objFinanceiroParcela.Valor = Math.Round((decimal)((total * c.VlrPerc) / 100), 2);
                    }
                    else
                    {
                        objFinanceiroParcela.Valor = c.VlrPerc;
                    }
                }
                valorcalc = valorcalc + (decimal)objFinanceiroParcela.Valor;

                if (pIdTipoDocumento != 0)
                {
                    objFinanceiroParcela.IDTipoDocumento = pIdTipoDocumento;
                }
                else
                {
                    objFinanceiroParcela.IDTipoDocumento = c.IDTipoDocumento.Value;
                }
                if (c.bAVista == 1)
                    objFinanceiroParcela.bEntrada = 1;
                else
                    objFinanceiroParcela.bEntrada = 0;
                listaParcelas.Add(objFinanceiroParcela);

                if (pPercAcrescimo != 0)
                {
                    count++;
                    qtdparcela--;
                }
            }

            if (pPercAcrescimo == 0)
            {
                if (valorcalc > pValor)
                {
                    objFinanceiroParcela = new Modelo.Objeto.FinanceiroParcela();
                    objFinanceiroParcela = (from t in listaParcelas
                                            where t.Parcela == 1
                                            select t).Single();

                    objFinanceiroParcela.Valor = objFinanceiroParcela.Valor - (valorcalc - pValor);
                }
                else
                {
                    if (valorcalc < pValor)
                    {
                        objFinanceiroParcela = new Modelo.Objeto.FinanceiroParcela();
                        objFinanceiroParcela = (from t in listaParcelas
                                                where t.Parcela == 1
                                                select t).Single();

                        objFinanceiroParcela.Valor = objFinanceiroParcela.Valor + (pValor - valorcalc);
                    }
                }
            }

            return listaParcelas;
        }

        public List<DateTime> GeraListaVencimentos(DateTime pDataContabil, Modelo.Condicao pobjCondicao)
        {
            List<DateTime> listaVencimento = new List<DateTime>();

            foreach (Modelo.CondicaoParcela c in pobjCondicao.CondicaoParcelas)
            {
                DateTime vencimento;

                if (c.TipoDt == "Dia")
                {
                    vencimento = Modelo.MetodosEstaticos.geraVencimento(pDataContabil, Modelo.TipoDataType.Dia, (int)c.DiaMes);
                }
                else
                {
                    vencimento = Modelo.MetodosEstaticos.geraVencimento(pDataContabil, Modelo.TipoDataType.Mês, (int)c.DiaMes);
                }

                listaVencimento.Add(vencimento);
            }

            return listaVencimento;
        }

        /// <summary>
        /// Rotina que atualiza as parcelas - Caso não tenha valor alterado se baseia na condição
        /// agora caso tenha algum valor alterado a rotina sempre irá dividir o valor
        /// </summary>
        /// <param name="listaParcelas"></param>
        /// <param name="valorTotal"></param>
        /// <param name="condicao"></param>
        /// <returns></returns>
        public bool AtualizaParcelas(Modelo.DataClassesDataContext pDb, List<Modelo.Objeto.FinanceiroParcela> listaParcelas, decimal valorTotal, int idCondicao)
        {
            bool ret;
            decimal valoralterado = 0;
            decimal valoraretear = 0;
            decimal valorcalc = 0;
            decimal valorfixo = 0;
            decimal total = 0;

            int qtdratear = 0;
            int Parcela = 0;


            if (listaParcelas.Count == 0)
            {
                return true;
            }

            if (valorTotal <= 0)
            {
                listaParcelas.Clear();
                return true;
            }

            Modelo.Condicao condicao = pDb.Condicaos.Where(c => c.ID == idCondicao).Single();

            foreach (Modelo.Objeto.FinanceiroParcela p in listaParcelas)
            {
                if (p.bAlterado == 1)
                {
                    valoralterado = valoralterado + (decimal)p.Valor;
                }
                else
                {
                    qtdratear++;
                }
            }

            if ((qtdratear == 0) || (listaParcelas.Count == qtdratear))
            {
                return true;
            }

            if (valoralterado == 0)
            {
                valoraretear = (decimal)valorTotal - valoralterado;

                foreach (Modelo.CondicaoParcela c in condicao.CondicaoParcelas)
                {
                    if (c.VlrPerc != null && c.TipoVlr == "Valor")
                    {
                        valorfixo = valorfixo + (decimal)c.VlrPerc;
                    }
                }

                if (valorfixo > (decimal)valorTotal)
                {
                    throw new Exception("O valor fixado nas parcelas é maior que o valor total. Verifique.");
                }

                total = (decimal)valorTotal - valorfixo;

                Modelo.CondicaoParcela objCP;
                foreach (Modelo.Objeto.FinanceiroParcela p in listaParcelas)
                {
                    objCP = (from c in condicao.CondicaoParcelas
                             where c.Parcela == (int)p.Parcela
                             select c).Single();

                    if (condicao.Divide == 1)
                    {
                        if (condicao.QtParcela != 0)
                        {
                            p.Valor = total / condicao.QtParcela;
                        }
                        else
                        {
                            throw new Exception("Não foi configurado a quantidade de parcelas na condição.");
                        }
                    }
                    else
                    {
                        if (objCP.TipoVlr == "Percentual")
                        {
                            p.Valor = (total * objCP.VlrPerc) / 100;
                        }
                        else
                        {
                            p.Valor = objCP.VlrPerc;
                        }
                    }
                    valorcalc = valorcalc + (decimal)p.Valor;

                    if (Parcela == 0)
                    {
                        Parcela = p.Parcela;
                    }
                }
            }
            else
            {
                valoraretear = (decimal)valorTotal - valoralterado;
                foreach (Modelo.Objeto.FinanceiroParcela p in listaParcelas)
                {
                    if (p.bAlterado == 1)
                    {
                        continue;
                    }

                    p.Valor = valoraretear / qtdratear;
                    valorcalc = valorcalc + (decimal)p.Valor;

                    if (Parcela == 0)
                    {
                        Parcela = p.Parcela;
                    }
                }

            }

            if (valorcalc > valoraretear)
            {
                Modelo.Objeto.FinanceiroParcela objFinanceiroParc = new Modelo.Objeto.FinanceiroParcela();
                objFinanceiroParc = (from t in listaParcelas
                                     where t.Parcela == Parcela
                                     select t).Single();

                objFinanceiroParc.Valor = objFinanceiroParc.Valor - (valorcalc - valoraretear);
            }
            else
            {
                if (valorcalc < valoraretear)
                {
                    Modelo.Objeto.FinanceiroParcela objFinanceiroParc = new Modelo.Objeto.FinanceiroParcela();
                    objFinanceiroParc = (from t in listaParcelas
                                         where t.Parcela == Parcela
                                         select t).Single();

                    objFinanceiroParc.Valor = objFinanceiroParc.Valor + (valoraretear - valorcalc);
                }
            }

            ret = true;

            return ret;
        }

    }
}
