using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Financeiro
{
    public class Parcelamento
    {
        public Modelo.Condicao Condicao { get; set; }
        public Modelo.TipoDocumento TipoDocumento { get; set; }
        public List<DateTime> ListaVencimento { get; set; }

        public List<Modelo.FinanceiroParcela> GerarParcelas(DateTime dataContabil, decimal pValor, decimal pPercAcrescimo)
        {
            var listaParcelas = new List<Modelo.FinanceiroParcela>();
            Modelo.FinanceiroParcela objFinanceiroParcela;
            decimal valorfixo = 0;
            decimal total = 0;
            decimal valorcalc = 0;

            int diasatraso = 0;
            int qtdparcela = Condicao.QtParcela;
            Decimal valordividido = 0;

            foreach (Modelo.CondicaoParcela c in Condicao.CondicaoParcelas)
            {
                if (c.TipoDt != "Dia" && c.TipoDt != "Mês")
                {
                    throw new Exception("Tipo de dados inválido para o campo Tipo da Data na condição.");
                }
                if (Condicao.Divide == 0)
                {
                    if (c.TipoVlr != "Valor" && c.TipoVlr != "Percentual")
                    {
                        throw new Exception("Tipo de dados inválido para o campo Tipo do Valor na condição.");
                    }
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                    if (c.TipoVlr == "Percentual" && (c.VlrPerc == 0 || c.VlrPerc == null))
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                    {
                        throw new Exception("Valor do percentual não pode ser 0(Zero)");
                    }
                }

#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                if (c.VlrPerc != null && c.TipoVlr == "Valor")
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
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

            if (ListaVencimento == null)
                GerarListaVencimentos(dataContabil);

            int count = 1;
            foreach (Modelo.CondicaoParcela c in Condicao.CondicaoParcelas)
            {
                objFinanceiroParcela = new Modelo.FinanceiroParcela();
                objFinanceiroParcela.Parcela = c.Parcela;
                objFinanceiroParcela.DtContabil = dataContabil;
                objFinanceiroParcela.Vencimento = ListaVencimento[c.Parcela - 1];

                if (Condicao.Divide == 1)
                {
                    if (Condicao.QtParcela != 0)
                    {
                        if (count == 1)
                        {
                            objFinanceiroParcela.Valor = Math.Round((decimal)(total / qtdparcela), 2);
                        }
                        else
                        {
                            diasatraso = (objFinanceiroParcela.Vencimento - dataContabil).Days;
                            var atrasoCalculado = ((valordividido * (pPercAcrescimo / 30)) / 100) * diasatraso;
                            objFinanceiroParcela.Valor = Math.Round(atrasoCalculado + valordividido, 2);
                        }
                    }
                    else
                    {
                        throw new Exception("Não foi configurada a quantidade de parcelas na condição.");
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
                valorcalc = valorcalc + objFinanceiroParcela.Valor;

                if (TipoDocumento != null)
                    objFinanceiroParcela.TipoDocumento = TipoDocumento;
                else
                    objFinanceiroParcela.TipoDocumento = c.TipoDocumento;

                if (c.BAVista)
                    objFinanceiroParcela.BEntrada = true;
                else
                    objFinanceiroParcela.BEntrada = false;

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
                    objFinanceiroParcela = new Modelo.FinanceiroParcela();
                    objFinanceiroParcela = (from t in listaParcelas
                                            where t.Parcela == 1
                                            select t).Single();

                    objFinanceiroParcela.Valor = objFinanceiroParcela.Valor - (valorcalc - pValor);
                }
                else
                {
                    if (valorcalc < pValor)
                    {
                        objFinanceiroParcela = new Modelo.FinanceiroParcela();
                        objFinanceiroParcela = (from t in listaParcelas
                                                where t.Parcela == 1
                                                select t).Single();

                        objFinanceiroParcela.Valor = objFinanceiroParcela.Valor + (pValor - valorcalc);
                    }
                }
            }

            return listaParcelas;
        }

        public void GerarListaVencimentos(DateTime pDataContabil)
        {
            ListaVencimento = new List<DateTime>();
            foreach (Modelo.CondicaoParcela c in Condicao.CondicaoParcelas)
            {
                DateTime vencimento;

                if (c.TipoDt == "Dia")
                {
                    vencimento = GerarVencimento(pDataContabil, Modelo.TipoDataType.Dia, (int)c.DiaMes);
                }
                else
                {
                    vencimento = GerarVencimento(pDataContabil, Modelo.TipoDataType.Mês, (int)c.DiaMes);
                }

                ListaVencimento.Add(vencimento);
            }
        }

        public static DateTime GerarVencimento(DateTime data, Modelo.TipoDataType tipo, int valor)
        {
            DateTime vencimento = new DateTime();

            if (tipo == Modelo.TipoDataType.Dia)
            {
                vencimento = data.AddDays(Double.Parse(valor.ToString()));
            }
            else if (tipo == Modelo.TipoDataType.Mês)
            {
                vencimento = data.AddMonths(valor);
            }

            return vencimento;
        }

        public void AtualizarParcelas(List<Modelo.FinanceiroParcela> listaParcelas, decimal valorTotal)
        {
            decimal valoralterado = 0;
            decimal valoraretear = 0;
            decimal valorcalc = 0;
            decimal valorfixo = 0;
            decimal total = 0;
            int qtdratear = 0;
            int Parcela = 0;

            if (listaParcelas.Count == 0)
                return;
            if (valorTotal <= 0)
            {
                listaParcelas.Clear();
                return;
            }

            foreach (Modelo.FinanceiroParcela p in listaParcelas)
            {
                if (p.BAlterado)
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
                return;
            }

            if (valoralterado == 0)
            {
                valoraretear = (decimal)valorTotal - valoralterado;

                foreach (Modelo.CondicaoParcela c in Condicao.CondicaoParcelas)
                {
#pragma warning disable CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                    if (c.VlrPerc != null && c.TipoVlr == "Valor")
#pragma warning restore CS0472 // O resultado da expressão é sempre 'true', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
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
                foreach (Modelo.FinanceiroParcela p in listaParcelas)
                {
                    objCP = (from c in Condicao.CondicaoParcelas
                             where c.Parcela == (int)p.Parcela
                             select c).Single();

                    if (Condicao.Divide == 1)
                    {
                        if (Condicao.QtParcela != 0)
                        {
                            p.Valor = total / Condicao.QtParcela;
                        }
                        else
                        {
                            throw new Exception("Não foi configurada a quantidade de parcelas na condição.");
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
                foreach (Modelo.FinanceiroParcela p in listaParcelas)
                {
                    if (p.BAlterado)
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
                var objFinanceiroParc = new Modelo.FinanceiroParcela();
                objFinanceiroParc = (from t in listaParcelas
                                     where t.Parcela == Parcela
                                     select t).Single();

                objFinanceiroParc.Valor = objFinanceiroParc.Valor - (valorcalc - valoraretear);
            }
            else
            {
                if (valorcalc < valoraretear)
                {
                    var objFinanceiroParc = new Modelo.FinanceiroParcela();
                    objFinanceiroParc = (from t in listaParcelas
                                         where t.Parcela == Parcela
                                         select t).Single();

                    objFinanceiroParc.Valor = objFinanceiroParc.Valor + (valoraretear - valorcalc);
                }
            }
        }
    }
}
