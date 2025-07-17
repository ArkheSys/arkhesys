using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using MO = Modelo.Objeto;
using MatrixReporter;
using System.Data.Common;

using System.Transactions;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Text;
using Cwork.Utilitarios.Funcoes.Sintegra;
using System.Xml.Linq;

namespace BLL
{
    public class Nota : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;

        public Modelo.SelecionaTipoImpressora tipoImpressora;

        public Nota(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista()
        {
            IQueryable lista;
            lista = from n in db.Notas
                    select new
                    {
                        Codigo = n.Codigo,
                        Data = n.Dt,
                        Filial = n.Filial.Codigo,
                        Serie = n.Serie,
                        Numero = n.Numero,
                        Tipo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                        Nome = n.PessoaNome,
                        Total = n.TotalNota,
                        ID = n.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(String pFiltro)
        {
            IQueryable lista;
            lista = from n in db.Notas
                    where n.Codigo.ToString() == pFiltro ||
                    n.Dt.ToString().Contains(pFiltro) ||
                    n.Filial.Codigo.ToString() == pFiltro ||
                    n.Serie.Contains(pFiltro) ||
                    n.Numero.ToString() == pFiltro ||
                    n.PessoaNome.Contains(pFiltro) ||
                    n.TotalNota.ToString() == pFiltro
                    select new
                    {
                        Codigo = n.Codigo,
                        Data = n.Dt,
                        Tipo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                        Filial = n.Filial.Codigo,
                        Serie = n.Serie,
                        Numero = n.Numero,
                        Nome = n.PessoaNome,
                        Total = n.TotalNota,
                        ID = n.ID
                    };
            return lista;
        }

        public IQueryable getListaEntSai(Modelo.InOutType aEntSai, String pFiltro)
        {
            IQueryable lista;
            lista = from n in db.Notas
                    where n.CancDt == null &&
                    n.NotaComplementada == null &&
                    n.ModeloDocto > 0 &&
                    n.Ent_Sai == aEntSai &&
                     n.NotaItems.Count > 0 &&
                    (
                    n.Codigo.ToString() == pFiltro ||
                    n.Dt.ToString().Contains(pFiltro) ||
                    n.Filial.Codigo.ToString() == pFiltro ||
                    n.Serie.Contains(pFiltro) ||
                    n.Numero.ToString() == pFiltro ||
                    n.PessoaNome.Contains(pFiltro) ||
                    n.TotalNota.ToString() == pFiltro)
                    select new
                    {
                        Codigo = n.Codigo,
                        Data = n.Dt,
                        Modelo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                        Filial = n.Filial.Codigo,
                        Serie = n.Serie,
                        Numero = n.Numero,
                        Nome = n.PessoaNome,
                        Total = n.TotalNota,
                        TipoMovto = n.TipoNota.Nome,
                        FilialNome = n.Filial.Nome,
                        ID = n.ID
                    };
            return lista;
        }

        public IQueryable getListaEntSaiServico(Modelo.InOutType aEntSai, String pFiltro)
        {
            IQueryable lista;
            lista = from n in db.Notas
                    where n.CancDt == null &&
                    n.ModeloDocto > 0 &&
                    n.Ent_Sai == aEntSai &&
                    //n.NotaItems.Count == 0
                    n.NotaItemServicos.Count > 0
                    && (
                    n.Codigo.ToString() == pFiltro ||
                    n.Dt.ToString().Contains(pFiltro) ||
                    n.Filial.Codigo.ToString() == pFiltro ||
                    n.Serie.Contains(pFiltro) ||
                    n.Numero.ToString() == pFiltro ||
                    n.PessoaNome.Contains(pFiltro) ||
                    n.TotalNota.ToString() == pFiltro)
                    select new
                    {
                        Codigo = n.Codigo,
                        Data = n.Dt,
                        Modelo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                        Filial = n.Filial.Codigo,
                        Serie = n.Serie,
                        Numero = n.Numero,
                        Nome = n.PessoaNome,
                        Total = n.TotalNota,
                        TipoMovto = n.TipoNota.Nome,
                        FilialNome = n.Filial.Nome,
                        ID = n.ID
                    };
            return lista;
        }

        public IQueryable getListaEntSaiModeloDocumentoZero(Modelo.InOutType aEntSai, String pFiltro)
        {
            IQueryable lista;
            lista = from n in db.Notas
                    where n.CancDt == null &&
                    n.Ent_Sai == aEntSai &&
                    n.ModeloDocto == 0 && (
                    n.Codigo.ToString() == pFiltro ||
                    n.Dt.ToString().Contains(pFiltro) ||
                    n.Filial.Codigo.ToString() == pFiltro ||
                    n.Serie.Contains(pFiltro) ||
                    n.Numero.ToString() == pFiltro ||
                    n.PessoaNome.Contains(pFiltro) ||
                    n.TotalNota.ToString() == pFiltro)
                    select new
                    {
                        Codigo = n.Codigo,
                        Data = n.Dt,
                        Modelo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                        Filial = n.Filial.Codigo,
                        Serie = n.Serie,
                        Numero = n.Numero,
                        Nome = n.PessoaNome,
                        Total = n.TotalNota,
                        TipoMovto = n.TipoNota.Nome,
                        FilialNome = n.Filial.Nome,
                        ID = n.ID
                    };
            return lista;
        }

        public IQueryable getListaEntSaiCancelada(Modelo.InOutType aEntSai, String pFiltro)
        {
            IQueryable lista;
            lista = from n in db.Notas
                    where n.CancDt != null &&
                    n.NotaComplementada == null &&
                    n.Ent_Sai == aEntSai && (
                    n.Codigo.ToString() == pFiltro ||
                    n.Dt.ToString().Contains(pFiltro) ||
                    n.Filial.Codigo.ToString() == pFiltro ||
                    n.Serie.Contains(pFiltro) ||
                    n.Numero.ToString() == pFiltro ||
                    n.PessoaNome.Contains(pFiltro) ||
                    n.TotalNota.ToString() == pFiltro)
                    select new
                    {
                        Codigo = n.Codigo,
                        Data = n.Dt,
                        Modelo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                        Filial = n.Filial.Codigo,
                        Serie = n.Serie,
                        Numero = n.Numero,
                        Nome = n.PessoaNome,
                        Total = n.TotalNota,
                        TipoMovto = n.TipoNota.Nome,
                        FilialNome = n.Filial.Nome,
                        ID = n.ID,
                        DataCanc = n.CancDt,
                        Motivo = n.CancMotivo,
                        CancUsuario = n.CancUsuario
                    };
            return lista;
        }

        public IQueryable getListaNFe(Modelo.InOutType ent_sai)
        {
            IQueryable retorno;

            retorno = from n in db.Notas
                      where n.ModeloDocto.Value == 55 && n.Ent_Sai == ent_sai
                      select new
                        {
                            Codigo = n.Codigo,
                            Data = n.Dt,
                            Status = n.Status == "-1" ? "Não enviada" : (n.Status == "0" ? "Negada" : (n.Status == "1" ? "Aguardando" : (n.Status == "2" ? "Autorizado" : (n.Status == "3" ? "Cancelada" : "")))),
                            Modelo = n.ModeloDocto.Value == 1 ? "NF" : (n.ModeloDocto.Value == 33 ? "CF" : (n.ModeloDocto.Value == 55 ? "NFe" : "")),
                            Filial = n.Filial.Codigo,
                            Serie = n.Serie,
                            Numero = n.Numero,
                            Nome = n.PessoaNome,
                            Total = n.TotalNota,
                            TipoMovto = n.TipoNota.Nome,
                            FilialNome = n.Filial.Nome,
                            ID = n.ID,
                            DataCanc = n.CancDt,
                            Motivo = n.CancMotivo,
                            CancUsuario = n.CancUsuario
                        };
            return retorno;

        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo()
        {
            IQueryable lista;
            lista = from n in db.Notas
                    select new
                    {
                        Nome = n.Codigo,
                        ID = n.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(String pFiltro)
        {
            IQueryable lista;
            lista = from n in db.Notas
                    where n.Codigo.ToString() == pFiltro ||
                    n.Dt.ToString().Contains(pFiltro) ||
                    n.Filial.Codigo.ToString() == pFiltro ||
                    n.Serie.Contains(pFiltro) ||
                    n.Numero.ToString() == pFiltro ||
                    n.PessoaNome.Contains(pFiltro) ||
                    n.TotalNota.ToString() == pFiltro
                    select new
                    {
                        Nome = n.Codigo,
                        ID = n.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(int pID)
        {
            Object nota = (from u in db.Notas where u.ID == pID select u).Single();

            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, nota);

            return nota;
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.Notas where u.Codigo.ToString() == pValor select u).Single().ID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo()
        {
            Int32 maxCodigo = BuscaUltimoCodigoNF(db);
            return maxCodigo;
        }

        public Int32 RetornaMAXCodigo(Modelo.DataClassesDataContext pdb)
        {
            Int32 maxCodigo = BuscaUltimoCodigoNF(pdb);
            return maxCodigo;
        }

        private static Int32 BuscaUltimoCodigoNF(Modelo.DataClassesDataContext pdb)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from n in pdb.Notas
                             where n.Codigo == pdb.Notas.Max(b => b.Codigo)
                             select n).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// Rotina que geras as parcelas do pedido
        /// </summary>
        /// <returns>Retorna true ou false (bool)</returns>
        public bool gerarParcelas(Modelo.Nota objNota)
        {
            bool ret;

            decimal valorfixo = 0;
            decimal total = 0;
            decimal valorcalc = 0;

            if (objNota.TotalNota == 0)
            {
                return true;
            }

            try
            {
                if (objNota.Dt.Day == 0)
                {
                    throw new Exception("Data base não foi informada!");
                }

                if (objNota.Condicao == null)
                {
                    throw new Exception("Deve ser informado uma condição de pagamento");
                }

                foreach (Modelo.CondicaoParcela c in objNota.Condicao.CondicaoParcelas)
                {
                    if (c.TipoDt != "Dia" && c.TipoDt != "Mês")
                    {
                        throw new Exception("Tipo de dados inválido para o campo Tipo da Data na condição.");
                    }
                    if (objNota.Condicao.Divide == 0)
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

                if (valorfixo > (decimal)objNota.TotalNota)
                {
                    throw new Exception("O valor fixado nas parcelas é maior que o valor total. Verifique.");
                }

                //Remove as parcelas sem dar exceção
                foreach (Modelo.NotaParcela parc in objNota.NotaParcelas.ToList())
                {
                    parc.TipoDocumento = null;
                    parc.Nota = null;
                }
                
                decimal totalST = 0m;
                if (objNota.TipoNota.ValorSTPrimeiraParcela && objNota.NotaICMs.Count > 0)
                {
                    totalST = objNota.NotaICMs.Sum(n => n.ValorSubst).Value;
                }
                total = (decimal)objNota.TotalNota - totalST - valorfixo;

                foreach (Modelo.CondicaoParcela c in objNota.Condicao.CondicaoParcelas)
                {
                    DateTime vencimento = objNota.Dt;
                    Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();
                    objNotaParcela.Parcela = c.Parcela;
                    //if (c.TipoDt == "Dia")
                    //{
                    //    objNotaParcela.Vencimento = Modelo.MetodosEstaticos.geraVencimento((DateTime)objNota.Dt, Modelo.TipoDataType.Dia, (int)c.DiaMes);
                    //}
                    //else
                    //{
                    //    objNotaParcela.Vencimento = Modelo.MetodosEstaticos.geraVencimento((DateTime)objNota.Dt, Modelo.TipoDataType.Mês, (int)c.DiaMes);
                    //}

                    if (c.TipoDt == "Dia")
                        vencimento = vencimento.AddDays(c.DiaMes.Value);
                    else
                        vencimento = vencimento.AddMonths(c.DiaMes.Value);

                    List<Modelo.Feriado> feriados = db.Feriados.ToList();

                    while (c.Condicao.VencimentoFeriado != Modelo.VencimentoFeriado.Nada && (vencimento.DayOfWeek == DayOfWeek.Saturday || vencimento.DayOfWeek == DayOfWeek.Sunday || feriados.Where(feriado => feriado.Data.Day == vencimento.Day && feriado.Data.Month == vencimento.Month).Count() > 0))
                        switch (c.Condicao.VencimentoFeriado)
                        {
                            case Modelo.VencimentoFeriado.Adiantar:
                                vencimento = vencimento.AddDays(-1);
                                break;
                            case Modelo.VencimentoFeriado.Adiar:
                                vencimento = vencimento.AddDays(1);
                                break;
                        }

                    objNotaParcela.Vencimento = vencimento;
                    if (objNota.Condicao.Divide == 1)
                    {
                        if (objNota.Condicao.QtParcela != 0)
                        {
                            objNotaParcela.Valor = System.Math.Round((total / objNota.Condicao.QtParcela), 2);
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
                            objNotaParcela.Valor = (total * (decimal)c.VlrPerc) / 100;
                        }
                        else
                        {
                            objNotaParcela.Valor = (decimal)c.VlrPerc;
                        }
                    }
                    valorcalc = valorcalc + (decimal)objNotaParcela.Valor;

                    objNotaParcela.TipoDocumento = c.TipoDocumento;
                    objNota.NotaParcelas.Add(objNotaParcela);
                }

                if (totalST > 0)
                {
                    Modelo.NotaParcela objNotaParcela = (from t in objNota.NotaParcelas
                                                         where t.Parcela == 1
                                                         select t).Single();

                    objNotaParcela.Valor = objNotaParcela.Valor + totalST;
                    valorcalc = valorcalc + totalST;
                }

                if (valorcalc > objNota.TotalNota)
                {
                    Modelo.NotaParcela objNotaParcela = (from t in objNota.NotaParcelas
                                                         where t.Parcela == 1
                                                         select t).Single();

                    objNotaParcela.Valor = objNotaParcela.Valor - (valorcalc - (decimal)objNota.TotalNota);
                }
                else
                {
                    if (valorcalc < objNota.TotalNota)
                    {
                        Modelo.NotaParcela objNotaParcela = (from t in objNota.NotaParcelas
                                                             where t.Parcela == 1
                                                             select t).Single();

                        objNotaParcela.Valor = objNotaParcela.Valor + ((decimal)objNota.TotalNota - valorcalc);
                    }
                }

                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Rotina que atualiza as parcelas - Caso não tenha valor alterado se baseia na condição
        /// agora caso tenha algum valor alterado a rotina sempre irá dividir o valor
        /// </summary>
        /// <returns>Retorna true ou false (bool)</returns>
        public bool atualizaParcelas(Modelo.Nota objNota)
        {
            bool ret;
            decimal valoralterado = 0;
            decimal valoraretear = 0;
            decimal valorcalc = 0;
            decimal valorfixo = 0;
            decimal total = 0;
            decimal valorsubst = 0;

            int qtdratear = 0;
            int id = 0;

            valorsubst = objNota.NotaICMs.Sum(t => t.ValorSubst).Value;

            try
            {
                if (objNota.NotaParcelas.Count == 0)
                {
                    return true;
                }

                foreach (Modelo.NotaParcela p in objNota.NotaParcelas)
                {
                    if (p.bAlterado == 1)
                    {
                        if (p.Parcela == 1)
                        {
                            valoralterado = (valoralterado + (decimal)p.Valor) - valorsubst;
                        }
                        else
                        {
                            valoralterado = valoralterado + (decimal)p.Valor;
                        }
                    }
                    else
                    {
                        qtdratear++;
                    }
                }

                if (qtdratear == 0)
                {
                    return true;
                }

                if (valoralterado == 0)
                {
                    valoraretear = (decimal)objNota.TotalNota - valorsubst - valoralterado;

                    foreach (Modelo.CondicaoParcela c in objNota.Condicao.CondicaoParcelas)
                    {
                        if (c.VlrPerc != null && c.TipoVlr == "Valor")
                        {
                            valorfixo = valorfixo + (decimal)c.VlrPerc;
                        }
                    }

                    if (valorfixo > (decimal)objNota.TotalNota)
                    {
                        throw new Exception("O valor fixado nas parcelas é maior que o valor total. Verifique.");
                    }

                    total = (decimal)objNota.TotalNota - valorsubst - valorfixo;

                    Modelo.CondicaoParcela objCP;
                    foreach (Modelo.NotaParcela p in objNota.NotaParcelas)
                    {
                        //#594b
                        if ((from c in objNota.Condicao.CondicaoParcelas
                             where c.Parcela == (int)p.Parcela
                             select c).Count() == 0) continue;

                        //#594e
                        objCP = (from c in objNota.Condicao.CondicaoParcelas
                                 where c.Parcela == (int)p.Parcela
                                 select c).Single();
                        if (objNota.Condicao.Divide == 1)
                        {
                            if (objNota.Condicao.QtParcela != 0)
                            {
                                p.Valor = System.Math.Round((total / objNota.Condicao.QtParcela), 2);
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
                                p.Valor = (total * (decimal)objCP.VlrPerc) / 100;
                            }
                            else
                            {
                                p.Valor = (decimal)objCP.VlrPerc;
                            }
                        }
                        valorcalc = valorcalc + (decimal)p.Valor;

                        if (id == 0)
                        {
                            id = p.ID;
                        }
                    }
                }
                else
                {
                    valoraretear = (decimal)objNota.TotalNota - valorsubst - valoralterado;
                    foreach (Modelo.NotaParcela p in objNota.NotaParcelas)
                    {
                        if (p.bAlterado == 1)
                        {
                            if (p.Parcela == 1)
                            {
                                p.Valor -= valorsubst;
                            }

                            continue;
                        }

                        p.Valor = valoraretear / qtdratear;
                        valorcalc = valorcalc + (decimal)p.Valor;

                        if (id == 0)
                        {
                            id = p.ID;
                        }
                    }
                }

                if (objNota.NotaICMs.Sum(t => t.ValorSubst).Value > 0)
                {
                    Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();
                    objNotaParcela = (from t in objNota.NotaParcelas
                                      where t.Parcela == 1
                                      select t).Single();

                    objNotaParcela.Valor = objNotaParcela.Valor + objNota.NotaICMs.Sum(t => t.ValorSubst).Value;
                    valorcalc = valorcalc + objNota.NotaICMs.Sum(t => t.ValorSubst).Value;
                }

                if (valorcalc > (valoraretear + objNota.NotaICMs.Sum(t => t.ValorSubst).Value))
                {
                    Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();
                    objNotaParcela = (from t in objNota.NotaParcelas
                                      where t.Parcela == 1
                                      select t).Single();

                    objNotaParcela.Valor = objNotaParcela.Valor - (valorcalc - (valoraretear + objNota.NotaICMs.Sum(t => t.ValorSubst).Value));
                }
                else
                {
                    if (valorcalc < (valoraretear + objNota.NotaICMs.Sum(t => t.ValorSubst).Value))
                    {
                        Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();
                        objNotaParcela = (from t in objNota.NotaParcelas
                                          where t.Parcela == 1
                                          select t).Single();

                        objNotaParcela.Valor = objNotaParcela.Valor + ((valoraretear + objNota.NotaICMs.Sum(t => t.ValorSubst).Value) - valorcalc);
                    }
                }

                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Método para retornar a última numeração de Nota Fiscal
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 getRetornaUltimoNF(int pFilial, string pSerie)
        {
            Int32? maxNumero = BuscaUltimoNumeroNF(db, pFilial, pSerie);
            return (int)maxNumero;
        }

        public Int32 getRetornaUltimoNF(Modelo.DataClassesDataContext pdb, int pFilial, string pSerie)
        {
            Int32? maxNumero = BuscaUltimoNumeroNF(pdb, pFilial, pSerie);
            return (int)maxNumero;
        }

        private static Int32? BuscaUltimoNumeroNF(Modelo.DataClassesDataContext pdb, int pFilial, string pSerie)
        {
            Int32? maxNumero = 0;
            try
            {
                maxNumero = (pdb.Notas.Where(n => n.IDFilial == pFilial && n.Serie == pSerie && n.TipoNota.NFOrigem == Modelo.OrigemNFType.Própria).Max(b => b.Numero));
                if (maxNumero.HasValue)
                {
                    maxNumero = maxNumero + 1;
                }
                else
                {
                    maxNumero = 1;
                }
            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
            {
                maxNumero = 1;
            }
            return maxNumero;
        }

        public void TotalizaImposto(Modelo.Nota objNota, bool pCalcImposto, int pAcao, int filial)
        {
            if(objNota.TipoNota == null ) return;
            objNota.NotaICMs.Clear();
            TotalizarValoresICMS(db, objNota, pCalcImposto, pAcao, filial);
        }

        public void TotalizaImposto(Modelo.DataClassesDataContext pdb, Modelo.Nota objNota, bool pCalcImposto, int pAcao)
        {
            TotalizarValoresICMS(pdb, objNota, pCalcImposto, pAcao, 0);
        }

        private static void TotalizarValoresICMS(Modelo.DataClassesDataContext pdb, Modelo.Nota objNota, bool pCalcImposto, int pAcao, int filial)
        {
            int acao = pAcao;
            BLL.NotaItem bllNotaItem = new NotaItem(pdb);
            BLL.Produto bllProduto = new BLL.Produto(pdb);
            bool atulizaPB = false;
            bool atualizaPL = false;

            foreach (Modelo.NotaICM obj in objNota.NotaICMs)
            {
                if (obj.ID > 0)
                {
                    //pdb.NotaICMs.Attach(obj);
                    //var arg = pdb.GetChangeSet();
                    obj.AliqICMS = obj.AliqICMSNormal = obj.BaseICMS = obj.BaseSubst = obj.ValorDiferidoICMS =
                        obj.ValorICMS = obj.ValorIPI = obj.ValorIsentoICMS = obj.ValorOutroICMS = obj.ValorSubst = obj.ValorTotalProdutos = 0;
                    var n = (from ni in pdb.NotaICMs where ni.ID == obj.ID select ni).Single();
                    try
                    {

                        pdb.NotaICMs.DeleteOnSubmit(n);
                    }
#pragma warning disable CS0168 // A variável "exccc" está declarada, mas nunca é usada
                    catch (Exception exccc) { }
#pragma warning restore CS0168 // A variável "exccc" está declarada, mas nunca é usada
                    //NotaICMSController
                    //cwkControleUsuario.Facade.
                }
            }
            //objNota.NotaICMs.Clear();

            if (objNota.VolumePesoBruto == 0)
            {
                atulizaPB = true;
            }
            if (objNota.VolumePesoLiquido == 0)
            {
                atualizaPL = true;
            }

            string ufDestino = (objNota.Ent_Sai == Modelo.InOutType.Entrada ? objNota.Filial.Cidade.UF.Sigla : objNota.PessoaUF);
            Modelo.TabelaCFOP objTabelaCFOP;
            if (objNota.Filial.Cidade.UF.Sigla != objNota.PessoaUF)
            {
                if (objNota.bPessoaContribuinte == 1)
                    objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopFEstC;
                else
                    objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopFEstNC;
            }
            else
                objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopDEst;

            foreach (Modelo.NotaItem objNotaItem in objNota.NotaItems)
            {
                bool temicm = false;
                if (pCalcImposto == true)
                {
                    Modelo.TabelaCFOP objCFOP;
                    if (objNotaItem.Produto.IDCFOP.HasValue && objNotaItem.Produto.IDCFOP > 0)
                        objCFOP = objNotaItem.Produto.TabelaCFOP;
                    else
                        objCFOP = objTabelaCFOP;
                    Modelo.ImpostoNF imposto = BLL.Tributacao.CalculaIcms(objNota.Filial, ufDestino, Convert.ToBoolean(objNota.bPessoaContribuinte), objNotaItem.Produto, objCFOP, (decimal)objNotaItem.Total, (decimal)objNotaItem.RAT_Acrescimo, (decimal)objNotaItem.RAT_Desconto, objNota.Ent_Sai, objNota.Pessoa.bOrgaoPublico, objNota.ModeloDocto == 55 ? Tributacao.ModeloNota.NFe : Tributacao.ModeloNota.Modelo1);
                    objNotaItem.AtualizaImposto(imposto);
                    objNotaItem.IDCFOP = objCFOP.ID;
                }

                objNotaItem.CalcCustoMedio = objNota.TipoNota.bCalcCustoMedio;
                if (objNotaItem.CalcCustoMedio == true && (acao != 3 || filial != 0))
                    objNotaItem.ValorCustoMedio = (decimal)pdb.f_CustoMedio((int?)objNotaItem.IDProduto, (decimal?)objNotaItem.SubTotal, objNotaItem.Quantidade, Convert.ToInt32(objNota.TipoNota.bCalcCustoMedio), filial);

                Modelo.NotaICM objNotaICM;
                if ((from i in objNota.NotaICMs
                     where i.IDCFOP == objNotaItem.IDCFOP
                     select i).Count() != 0)
                {
                    objNotaICM = (from i in objNota.NotaICMs
                                  where i.IDCFOP == objNotaItem.IDCFOP
                                  select i).Single();
                    temicm = true;
                }
                else
                {
                    objNotaICM = new Modelo.NotaICM();

                    objNotaICM.Codigo = objNotaICM.RetornaMAXCodigo(objNota);
                    objNotaICM.BaseICMS = 0;
                    objNotaICM.ValorICMS = 0;
                    objNotaICM.ValorIsentoICMS = 0;
                    objNotaICM.ValorOutroICMS = 0;
                    objNotaICM.BaseSubst = 0;
                    objNotaICM.ValorSubst = 0;
                    objNotaICM.ValorTotalProdutos = 0;
                    objNotaICM.ValorIPI = 0;
                }
                objNotaItem.ValorRetidoICMS = objNotaItem.ValorRetidoICMS == null ? 0 : objNotaItem.ValorRetidoICMS;

                if (objNotaItem.IDCFOP == null)
                    objNotaICM.IDCFOP = null;
                else
                    objNotaICM.IDCFOP = (int)objNotaItem.IDCFOP;

                objNotaICM.IDICMS = null;
                objNotaICM.AliqICMS = objNotaItem.AliqICMS;
                objNotaICM.AliqICMSNormal = objNotaItem.AliqICMSNormal;
                objNotaICM.TextoIcms = objNotaItem.TextoLei;
                objNotaICM.BaseICMS = objNotaICM.BaseICMS + objNotaItem.BaseICMS;
                objNotaICM.ValorICMS = objNotaICM.ValorICMS + objNotaItem.ValorICMS;
                objNotaICM.ValorIsentoICMS = objNotaICM.ValorIsentoICMS + objNotaItem.ValorIsentoICMS;
                objNotaICM.ValorOutroICMS = objNotaICM.ValorOutroICMS + objNotaItem.ValorOutroICMS;
                objNotaICM.BaseSubst = objNotaICM.BaseSubst + objNotaItem.BaseICMSSubst;
                objNotaICM.ValorSubst = objNotaICM.ValorSubst + objNotaItem.ValorRetidoICMS;
                objNotaICM.ValorIPI = objNotaICM.ValorIPI + (objNotaItem.vIPI_O14 == null ? 0 : objNotaItem.vIPI_O14);
               
                objNotaICM.ValorTotalProdutos = objNotaICM.ValorTotalProdutos + (objNotaItem.Total + objNotaItem.vIPI_O14 + objNotaItem.ValorRetidoICMS + objNotaItem.RAT_Acrescimo - objNotaItem.RAT_Desconto);

                if (temicm == false)
                {
                    objNota.NotaICMs.Add(objNotaICM);
                }

                if (atulizaPB == true)
                {
                    objNota.VolumePesoBruto += objNotaItem.PesoBruto;
                }
                if (atualizaPL == true)
                {
                    objNota.VolumePesoLiquido += objNotaItem.PesoLiquido;
                }
            }
            bllNotaItem.Dispose();
        }

        #region Baixar Requisição

        public bool FecharRequisicao(List<int> pPedido, int pTipoMovto, DateTime? pDataFat, string pObs1, string pObs2, string pObs3, bool pFatLote)
        {
            bool ret = true;

            int cp = 0;

            Modelo.Nota objNota = new Modelo.Nota();
            objNota.AltData = DateTime.Now;
            objNota.IncData = DateTime.Now;
            objNota.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            objNota.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            using (Modelo.DataClassesDataContext _db = new Modelo.DataClassesDataContext())
            {
                Modelo.Condicao objCondicao = new Modelo.Condicao();
                BLL.Estoque bllEstoque = new Estoque();
                BLL.Produto bllProduto = new Produto(_db);

                //Cria o objeto Nota
                using (TransactionScope transacao = new TransactionScope())
                {
                    try
                    {
                        #region RegraFaturamento
                        //Diz para o linq que será incluido uma Nota
                        _db.Notas.InsertOnSubmit(objNota);

                        objNota.ValorDesconto = 0;
                        objNota.ValorAcrescimo = 0;

                        //Cria a bll Nota Item
                        BLL.NotaItem BLLNotaItem = new NotaItem(_db);

                        //Carrega o Tipo de Nota
                        Modelo.TipoNota tno = (from t in _db.TipoNotas
                                               where t.ID == pTipoMovto
                                               select t).Single();

                        List<Modelo.Objeto.pxyItemOrcamento> itensPedido;
                        Modelo.Objeto.pxyItemOrcamento objItemPedido;
                        //Foreach em todos os pedidos a ser faturados
                        for (int i = 0; i < pPedido.Count; i++)
                        {
                            cp = pPedido[i];

                            //Carrega o Pedido
                            Modelo.Pedido p;
                            p = (from p1 in _db.Pedidos
                                 where p1.ID == cp
                                 select p1).Single();

                            if (i == 0)
                            {
                                PreencheNotaRequisicao(pDataFat, pObs1, pObs2, pObs3, objNota, _db, tno, p);
                            }

                            itensPedido = new List<Modelo.Objeto.pxyItemOrcamento>();
                            foreach (Modelo.PedidoItem item in p.PedidoItems)
                            {
                                objItemPedido = new Modelo.Objeto.pxyItemOrcamento();
                                objItemPedido.ID = item.ID;
                                objItemPedido.Descricao = item.ProdutoNome;
                                objItemPedido.Marcado = true;
                                objItemPedido.Produto = item.ProdutoNome;
                                objItemPedido.Unidade = item.Unidade;
                                objItemPedido.Quantidade = item.Quantidade;
                                objItemPedido.QuantidadeAnt = objItemPedido.Quantidade;
                                objItemPedido.Desc = item.PercDesconto.HasValue ? item.PercDesconto.Value : 0;
                                objItemPedido.Total = item.Total;
                                objItemPedido.Valor = item.Valor;
                                objItemPedido.ValorCalc = item.ValorCalculado;
                                objItemPedido.ValorRetidoICMS = item.ValorRetidoICMS.HasValue ? item.ValorRetidoICMS.Value : 0;
                                objItemPedido.Item = item;
                                itensPedido.Add(objItemPedido);
                            }
                            AuxFechaRequisicao(objNota, _db, tno, bllEstoque, BLLNotaItem, p, itensPedido);
                        }
                        bllEstoque.AtualizaEstoqueNota(objNota, false, _db);

                        //Finaliza o processo que Fatura os Pedidos da Lista
                        _db.SubmitChanges();

                        transacao.Complete();
                        #endregion
                    }
                    catch (ChangeConflictException cce)
                    {
                        ret = false;
                        MessageBox.Show("Erro ao baixar requisições.\nErro " + cce.Message, "Aviso [Conflito]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    catch (SqlException ex)
                    {
                        ret = false;
                        string mensagem = "Erro ao baixar requisições.\nErro " + ex.Message;

                        if (ex.Number == 1205)
                            mensagem = "Não foi possivel baixar as requisições.\nTente novamente.";

                        MessageBox.Show(mensagem, "Aviso [SQL]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    catch (Exception e)
                    {
                        ret = false;
                        MessageBox.Show("Erro ao baixar requisições.\nErro " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }

                bllProduto.Dispose();
            }

            return ret;
        }

        public bool FecharRequisicao(Modelo.DataClassesDataContext _db, Modelo.Pedido objPedido, List<Modelo.Objeto.pxyItemOrcamento> itens, DateTime? pDataFat)
        {
            bool ret = true;
            Modelo.Nota objNota = new Modelo.Nota();
            Modelo.Condicao objCondicao = new Modelo.Condicao();
            BLL.Estoque bllEstoque = new Estoque();
            BLL.Produto bllProduto = new Produto(_db);
            objNota.AltData = DateTime.Now;
            objNota.IncData = DateTime.Now;
            objNota.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            objNota.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            //Cria o objeto Nota
            using (TransactionScope transacao = new TransactionScope())
            {
                try
                {
                    #region RegraFaturamento
                    //Diz para o linq que será incluido uma Nota
                    _db.Notas.InsertOnSubmit(objNota);

                    objNota.ValorDesconto = 0;
                    objNota.ValorAcrescimo = 0;

                    //Cria a bll Nota Item
                    BLL.NotaItem BLLNotaItem = new NotaItem(_db);

                    //Carrega o Tipo de Nota
                    Modelo.TipoNota tno = (from t in _db.TipoNotas
                                           where t.ID == objPedido.TipoPedido.IDTipoNota.Value
                                           select t).Single();

                    PreencheNotaRequisicao(pDataFat, null, null, null, objNota, _db, tno, objPedido);
                    AuxFechaRequisicao(objNota, _db, tno, bllEstoque, BLLNotaItem, objPedido, itens);
                    bllEstoque.AtualizaEstoqueNota(objNota, false, _db);

                    //Finaliza o processo que Fatura os Pedidos da Lista
                    _db.SubmitChanges();

                    transacao.Complete();
                    #endregion
                }
                catch (ChangeConflictException cce)
                {
                    ret = false;
                    MessageBox.Show("Erro ao baixar requisições.\nErro " + cce.Message, "Aviso [Conflito]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (SqlException ex)
                {
                    ret = false;
                    string mensagem = "Erro ao baixar requisições.\nErro " + ex.Message;

                    if (ex.Number == 1205)
                        mensagem = "Não foi possivel baixar as requisições.\nTente novamente.";

                    MessageBox.Show(mensagem, "Aviso [SQL]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (Exception e)
                {
                    ret = false;
                    MessageBox.Show("Erro ao baixar requisições.\nErro " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            bllProduto.Dispose();
            return ret;
        }

        private void PreencheNotaRequisicao(DateTime? pDataFat, string pObs1, string pObs2, string pObs3, Modelo.Nota objNota, Modelo.DataClassesDataContext _db, Modelo.TipoNota tno, Modelo.Pedido p)
        {
            //Carrega a Filial
            Modelo.Filial fil = (from f in _db.Filials
                                 where f.ID == (int)p.IDFilial
                                 select f).Single();

            //Carrega a Filial para numeração de Nota
            Modelo.Filial filn = (from f in _db.Filials
                                  where f.Codigo == (int)fil.FilialNF
                                  select f).Single();

            //Atribui as informações da Nota
            objNota.Codigo = this.RetornaMAXCodigo(_db);
            objNota.Filial = fil;
            objNota.TipoNota = tno;
            objNota.Ent_Sai = p.Ent_Sai;
            objNota.Dt = pDataFat == null ? (p.Dt.Value < System.DateTime.Today ? System.DateTime.Today : p.Dt.Value) : (DateTime)pDataFat;
            objNota.DtDigitacao = p.DtDigitacao;
            objNota.Pessoa = p.Pessoa;
            objNota.PessoaNome = p.PessoaNome.TrimEnd();
            objNota.PessoaEndereco = p.PessoaEndereco == null ? "" : p.PessoaEndereco.TrimEnd();
            objNota.PessoaBairro = p.PessoaBairro == null ? "" : p.PessoaBairro.TrimEnd();
            objNota.PessoaCidade = p.PessoaCidade == null ? "" : p.PessoaCidade.TrimEnd();
            objNota.PessoaUF = p.PessoaUF == null ? "" : p.PessoaUF.TrimEnd();
            objNota.PessoaCNPJCPF = p.PessoaCNPJCPF == null ? "" : p.PessoaCNPJCPF.TrimEnd();
            objNota.PessoaInscRG = p.PessoaInscRG == null ? "" : p.PessoaInscRG.TrimEnd();
            objNota.PessoaTelefone = p.PessoaTelefone == null ? "" : p.PessoaTelefone.TrimEnd();
            objNota.PessoaCEP = p.PessoaCEP == null ? "" : p.PessoaCEP.TrimEnd();

            objNota.IDVendedor = null;
            objNota.IDTabelaPreco = null;

            objNota.IDCondicao = null;

            objNota.ModeloDocto = 0;
            objNota.ObservacaoUsuario = objNota.ObservacaoSistema = String.Empty;
            if (pObs1 != null)
            {
                objNota.Observacao1 = pObs1.Length == 0 ? p.Observacao1 : pObs1;
            }
            if (pObs2 != null)
            {
                objNota.Observacao2 = pObs2.Length == 0 ? p.Observacao2 : pObs2;
            }
            if (pObs3 != null)
            {
                objNota.Observacao3 = pObs3.Length == 0 ? p.Observacao3 : pObs3;
            }

            objNota.Serie = tno.Serie;
            if (objNota.ModeloDocto == 33)
            {
                objNota.Serie = "ecf";
                objNota.Numero = 0;
            }
            else
                objNota.Numero = this.getRetornaUltimoNF(_db, (int)filn.ID, objNota.Serie);

            objNota.bGeraFinanceiro = (byte)tno.bGeraFinanceiro;
            objNota.bPessoaContribuinte = 0;
        }

        private static bool AuxFechaRequisicao(Modelo.Nota objNota, Modelo.DataClassesDataContext _db, Modelo.TipoNota tno, BLL.Estoque bllEstoque, BLL.NotaItem BLLNotaItem, Modelo.Pedido p, List<Modelo.Objeto.pxyItemOrcamento> itensBaixa)
        {
            bool parcial = false;

            //Foreach em todos os itens do pedido
            int count = 0;
            foreach (Modelo.Objeto.pxyItemOrcamento pi in itensBaixa.Where(i => i.Marcado))
            {
                if (pi.Quantidade < pi.QuantidadeAnt)
                    parcial = true;

                Modelo.NotaItem objNotaItem = new Modelo.NotaItem();

                //Atribui as informações ref. aos itens da nota
                objNotaItem.Sequencia = BLLNotaItem.RetornaMAXSequencia(objNota);
                objNotaItem.IDProduto = pi.Item.IDProduto;
                objNotaItem.ProdutoNome = pi.Item.ProdutoNome.TrimEnd();
                objNotaItem.ProdutoDescReduzida = pi.Item.ProdutoDescReduzida == null ? string.Empty : pi.Item.ProdutoDescReduzida.TrimEnd();
                objNotaItem.Quantidade = pi.Quantidade;
                objNotaItem.Unidade = pi.Unidade.TrimEnd();
                objNotaItem.ValorCalculado = pi.ValorCalc;
                objNotaItem.Valor = pi.Valor;
                objNotaItem.SubTotal = pi.Total;
                objNotaItem.PercDesconto = pi.Desc;
                objNotaItem.ValorDesconto = 0;
                objNotaItem.RAT_Desconto = 0;
                objNotaItem.RAT_Acrescimo = 0;
                objNotaItem.Total = pi.Total;
                objNotaItem.PesoBruto = pi.Item.PesoBruto;
                objNotaItem.PesoLiquido = pi.Item.PesoLiquido;
                objNotaItem.Dt = pi.Item.Dt;
                objNotaItem.bAtualizaEstoque = tno.bAtualizaEstoque;
                objNotaItem.Ent_Sai = pi.Item.Ent_Sai;

                //Verificar se é para calcular o custo médio quando for compra,
                //quando for venda buscar o último custo médio calculado
                objNotaItem.CalcCustoMedio = tno.bCalcCustoMedio;
                objNotaItem.ValorCustoMedio = 0;

                if (tno.bAtualizaPedido == Modelo.TituloType.Sim)
                {
                    //Vincula o registro Nota Item com o itens do pedido
                    objNotaItem.PedidoItem = pi.Item;
                    //Atualiza a Quantidade retirada no Itens do Pedido
                    pi.Item.QtdRetirada += pi.Quantidade;
                }

                //Grava o objeto nota item na Nota
                objNota.NotaItems.Add(objNotaItem);
                count++;
            }

            if (itensBaixa.Count > count)
                parcial = true;

            //Calcula o Total da Nota
            objNota.TotalProduto = 0;
            foreach (Modelo.NotaItem ni in objNota.NotaItems)
            {
                objNota.TotalProduto += ni.Total;
            }
            objNota.ValorDesconto += p.ValorDesconto == null ? 0 : p.ValorDesconto;
            objNota.ValorAcrescimo += p.ValorAcrescimo == null ? 0 : p.ValorAcrescimo;
            objNota.PercDesconto = ((objNota.TotalProduto > 0) ? (decimal)(objNota.ValorDesconto / objNota.TotalProduto * 100) : 0);
            objNota.PercDesconto = System.Math.Round((decimal)objNota.PercDesconto, 4);
            objNota.TotalNota = objNota.TotalProduto - objNota.ValorDesconto + objNota.ValorAcrescimo;

            if (tno.bAtualizaPedido == Modelo.TituloType.Sim)
            {
                //Atualiza o Status do Pedido
                if (parcial)
                    p.Status = Modelo.StatusOrcamento.FaturadoParcial;
                else
                    p.Status = Modelo.StatusOrcamento.Faturado;
            }
            else if (tno.bAtualizaPedido == Modelo.TituloType.Não)
            {
                bllEstoque.AtualizaEstoquePedido(p, true, _db);
                if ((from c in _db.Configuracaos
                     select c).Single().BExcluirOrcamentoFaturado)
                    _db.Pedidos.DeleteOnSubmit(p);
                //#803 ^
            }

            return parcial;
        }

        #endregion

        //public bool FaturarPedido(List<int> pPedido, List<Modelo.Objeto.FinanceiroParcela> pParcelas, int pTipoMovto, int pCondicao, DateTime? pDataFat, Modelo.MessageButtonFaturar pMessageButtonFaturar, string pObs1, string pObs2, string pObs3, bool pFatLote, out string mensagem)
        //{
        //    mensagem = String.Empty;
        //    bool ret = true;
        //    int cp = 0;

        //    Modelo.Nota objNota = new Modelo.Nota();
        //    objNota.IncData = DateTime.Now;
        //    objNota.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
        //    objNota.AltData = DateTime.Now;
        //    objNota.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
        //    using (Modelo.DataClassesDataContext _db = new Modelo.DataClassesDataContext())
        //    {
        //        Modelo.TipoNota tno = new Modelo.TipoNota();
        //        Modelo.Filial fil = new Modelo.Filial();
        //        Modelo.Filial filn = new Modelo.Filial();
        //        //Modelo.Pedido ped = new Modelo.Pedido();
        //        Modelo.Condicao objCondicao = new Modelo.Condicao();
        //        Modelo.TabelaCFOP objTabelaCFOP = new Modelo.TabelaCFOP();
        //        BLL.Estoque bllEstoque = new Estoque();
        //        BLL.Produto bllProduto = new Produto(_db);
        //        Modelo.ProdutoVendedorComissao pvc;
        //        IQueryable<Modelo.ProdutoVendedorComissao> tempPvc;

        //        try
        //        {
        //            #region RegraFaturamento
        //            //Diz para o linq que será incluido uma Nota
        //            _db.Notas.InsertOnSubmit(objNota);

        //            objNota.ValorDesconto = 0;
        //            objNota.ValorAcrescimo = 0;

        //            //Cria a bll Nota Item
        //            BLL.NotaItem BLLNotaItem = new NotaItem(_db);

        //            //Foreach em todos os pedidos a ser faturados
        //            for (int i = 0; i < pPedido.Count; i++)
        //            {
        //                cp = pPedido[i];

        //                //Carrega o Pedido
        //                Modelo.Pedido p;
        //                p = (from p1 in _db.Pedidos
        //                     where p1.ID == cp
        //                     select p1).Single();

        //                if (i == 0)
        //                {
        //                    //Carrega o Tipo de Nota
        //                    tno = (from t in _db.TipoNotas
        //                           where t.ID == (pTipoMovto == 0 ? (int)p.TipoPedido.IDTipoNota : pTipoMovto)
        //                           select t).Single();

        //                    //Carrega a Filial
        //                    fil = (from f in _db.Filials
        //                           where f.ID == (int)p.IDFilial
        //                           select f).Single();

        //                    //Carrega a Filial para numeração de Nota
        //                    filn = (from f in _db.Filials
        //                            where f.Codigo == (int)fil.FilialNF
        //                            select f).Single();

        //                    //Atribui as informações da Nota
        //                    objNota.Codigo = this.RetornaMAXCodigo(_db);
        //                    objNota.Filial = fil;
        //                    objNota.TipoNota = tno;
        //                    objNota.Ent_Sai = p.Ent_Sai;
        //                    objNota.Dt = pDataFat == null ? (p.Dt.Value < System.DateTime.Today ? System.DateTime.Today : p.Dt.Value) : (DateTime)pDataFat;
        //                    objNota.DtDigitacao = p.DtDigitacao;
        //                    objNota.Pessoa = p.Pessoa;
        //                    objNota.PessoaNome = p.PessoaNome.TrimEnd();
        //                    objNota.PessoaEndereco = p.PessoaEndereco == null ? "" : p.PessoaEndereco.TrimEnd();
        //                    objNota.PessoaBairro = p.PessoaBairro == null ? "" : p.PessoaBairro.TrimEnd();
        //                    objNota.PessoaCidade = p.PessoaCidade == null ? "" : p.PessoaCidade.TrimEnd();
        //                    objNota.PessoaUF = p.PessoaUF == null ? "" : p.PessoaUF.TrimEnd();
        //                    objNota.PessoaCNPJCPF = p.PessoaCNPJCPF == null ? "" : p.PessoaCNPJCPF.TrimEnd();
        //                    objNota.PessoaInscRG = p.PessoaInscRG == null ? "" : p.PessoaInscRG.TrimEnd();
        //                    objNota.PessoaTelefone = p.PessoaTelefone == null ? "" : p.PessoaTelefone.TrimEnd();
        //                    objNota.PessoaCEP = p.PessoaCEP == null ? "" : p.PessoaCEP.TrimEnd();
        //                    EntitySet<Modelo.Endereco> a = p.Pessoa.Enderecos;
        //                    objNota.PessoaCidadeIBGE = a.Where(end => end.bPrincipal == 1).Single().Cidade.IBGE;
        //                    objNota.TipoFrete = 1;
        //                    //objNota.ValorFrete = 0;
        //                    objNota.VolumeQuant = 0;
        //                    objNota.VolumeEspecie = "";
        //                    objNota.VolumeMarca = "";
        //                    objNota.VolumePesoLiquido = 0;
        //                    objNota.VolumePesoBruto = 0;
        //                    //#538 Begin

        //                    objNota.ValorFrete = p.ValorFrete;
        //                    objNota.IDTransportadora = p.IDTransportadora;
        //                    if (p.Transportadora != null)
        //                    {
        //                        objNota.Transportadora = p.Transportadora;
        //                        objNota.TransCidade = p.Transportadora.Cidade.Nome;
        //                        objNota.TransCNPJCPF = p.Transportadora.CNPJ_CPF;
        //                        objNota.TransEndereco = p.Transportadora.Endereco;
        //                        try
        //                        {
        //                            objNota.TransUF = p.Transportadora.Cidade.UF.Sigla;
        //                        }
        //                        catch { }
        //                        objNota.TransNome = p.Transportadora.Nome;
        //                        objNota.TransPlaca = p.Transportadora.Placa;
        //                        objNota.TransPlacaUF = p.Transportadora.PlacaUF;
        //                        objNota.TipoFrete = p.TipoFrete;

        //                    }
        //                    //objNota.TipoFrete = ped.TipoFrete;
        //                    //#538 End

        //                    if (p.IDVendedor != null)
        //                    {
        //                        objNota.IDVendedor = (int?)p.IDVendedor;
        //                    }
        //                    objNota.IDTabelaPreco = (int)p.IDTabelaPreco;

        //                    objNota.IDCondicao = pCondicao == 0 ? p.IDCondicao.Value : pCondicao;
        //                    objNota.Condicao = (Modelo.Condicao)objCondicao.getObjeto(_db, (int)objNota.IDCondicao);

        //                    if (pMessageButtonFaturar == Modelo.MessageButtonFaturar.CupomFiscal)
        //                    {
        //                        objNota.ModeloDocto = 33; //tno.ModeloDocto;
        //                    }
        //                    else if (pMessageButtonFaturar == Modelo.MessageButtonFaturar.NotaFiscal)
        //                    {
        //                        objNota.ModeloDocto = 1; //tno.ModeloDocto;
        //                    }
        //                    else if (pMessageButtonFaturar == Modelo.MessageButtonFaturar.NFe)
        //                    {
        //                        objNota.ModeloDocto = 55;
        //                    }
        //                    else
        //                    {
        //                        objNota.ModeloDocto = 0; //tno.ModeloDocto;
        //                    }

        //                    if (p.Pessoa.Codigo == 1 && objNota.ModeloDocto != 33)
        //                    {
        //                        MessageBox.Show("Não pode ser faturado nota fiscal para consumidor.\nVerifique.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //                        return false;
        //                    }

        //                    if (pObs1 != null)
        //                    {
        //                        //objNota.Observacao1 = pObs1.Length == 0 ? p.Observacao1 : pObs1;
        //                        objNota.Observacao1 = pObs1;
        //                    }
        //                    if (pObs2 != null)
        //                    {
        //                        objNota.Observacao2 = pObs2;
        //                    }
        //                    if (pObs3 != null)
        //                    {
        //                        objNota.Observacao3 = pObs3;
        //                    }

        //                    objNota.ObservacaoUsuario += String.IsNullOrEmpty(objNota.Observacao1) ? "" : objNota.Observacao1 + " ";
        //                    objNota.ObservacaoUsuario += String.IsNullOrEmpty(objNota.Observacao2) ? "" : objNota.Observacao2 + " ";
        //                    objNota.ObservacaoUsuario += String.IsNullOrEmpty(objNota.Observacao3) ? "" : objNota.Observacao3 + " ";
        //                    objNota.ObservacaoUsuario = objNota.ObservacaoUsuario.Trim();

        //                    objNota.Serie = tno.Serie;
        //                    if (objNota.ModeloDocto == 33)
        //                    {
        //                        objNota.Serie = "ecf";
        //                        objNota.Numero = 0;
        //                    }
        //                    else
        //                        objNota.Numero = this.getRetornaUltimoNF(_db, (int)filn.ID, objNota.Serie);

        //                    objNota.bGeraFinanceiro = (byte)tno.bGeraFinanceiro;

        //                    if (objNota.Ent_Sai == Modelo.InOutType.Saída)
        //                    {
        //                        Modelo.Cliente objCliente;
        //                        objCliente = (from c in p.Pessoa.Clientes
        //                                      where c.IDPessoa == p.Pessoa.ID
        //                                      select c).Single();
        //                        objNota.bPessoaContribuinte = objCliente.bContribuinte;

        //                        if (objNota.Filial.Cidade.UF.Sigla != objNota.PessoaUF)
        //                        {
        //                            if (objNota.bPessoaContribuinte == 1)
        //                                objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopFEstC;
        //                            else
        //                                objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopFEstNC;
        //                        }
        //                        else
        //                            objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopDEst;
        //                    }
        //                    else
        //                    {
        //                        objNota.bPessoaContribuinte = 0;
        //                    }
        //                }

        //                //Foreach em todos os itens do pedido
        //                foreach (Modelo.PedidoItem pi in p.PedidoItems)
        //                {
        //                    //Cria o objeto Nota Item
        //                    Modelo.NotaItem objNotaItem = new Modelo.NotaItem();

        //                    //Atribui as informações ref. aos itens da nota
        //                    objNotaItem.Sequencia = BLLNotaItem.RetornaMAXSequencia(objNota);
        //                    objNotaItem.IDProduto = pi.IDProduto;
        //                    objNotaItem.ProdutoNome = pi.ProdutoNome.TrimEnd();
        //                    objNotaItem.ProdutoDescReduzida = pi.ProdutoDescReduzida != null ? pi.ProdutoDescReduzida.TrimEnd() : "";
        //                    objNotaItem.Quantidade = pi.Quantidade;
        //                    objNotaItem.Unidade = pi.Unidade.TrimEnd();
        //                    objNotaItem.ValorCalculado = pi.ValorCalculado;
        //                    objNotaItem.Valor = pi.Valor;
        //                    objNotaItem.SubTotal = pi.SubTotal;
        //                    //objNotaItem.PercDesconto = pi.PercDesconto;
        //                    objNotaItem.ValorDesconto = pi.ValorDesconto;
        //                    objNotaItem.RAT_Desconto = pi.RAT_Desconto == null ? 0 : pi.RAT_Desconto;
        //                    objNotaItem.RAT_Acrescimo = pi.RAT_Acrescimo == null ? 0 : pi.RAT_Acrescimo;
        //                    objNotaItem.Total = pi.Total;
        //                    objNotaItem.PesoBruto = pi.PesoBruto;
        //                    objNotaItem.PesoLiquido = pi.PesoLiquido;
        //                    objNotaItem.Dt = pi.Dt;
        //                    objNotaItem.bAtualizaEstoque = tno.bAtualizaEstoque;
        //                    objNotaItem.Ent_Sai = pi.Ent_Sai;
        //                    objNotaItem.InfAdicionais = pi.InformacoesAdicionais;

        //                    //Verificar se é para calcular o custo médio quando for compra,
        //                    //quando for venda buscar o último custo médio calculado
        //                    objNotaItem.CalcCustoMedio = tno.bCalcCustoMedio;
        //                    objNotaItem.ValorCustoMedio = 0;


        //                    //#516 Begin

        //                    tempPvc = (from cheat in db.ProdutoVendedorComissaos
        //                               where cheat.IDVendedor == objNota.IDVendedor && cheat.IDProduto == objNotaItem.IDProduto
        //                               select cheat);
        //                    if (tempPvc.Count() > 0)
        //                    {
        //                        pvc = tempPvc.Single();
        //                        objNotaItem.ComissaoPorcentagem = pvc.ComissaoPct;
        //                        objNotaItem.TipoComissao = pvc.TipoComissao;
        //                    }
        //                    //#516 End

        //                    Modelo.ImpostoNF imposto;
        //                    BLL.Tributacao.db = _db;

        //                    Modelo.TabelaCFOP objCFOP;
        //                    if (pi.Produto.IDCFOP.HasValue && pi.Produto.IDCFOP > 0)
        //                        objCFOP = pi.Produto.TabelaCFOP;
        //                    else
        //                        objCFOP = objTabelaCFOP;
        //                    imposto = BLL.Tributacao.CalculaIcms(objNota.Filial, objNota.PessoaUF, (objNota.bPessoaContribuinte == 1 ? true : false), (Modelo.Produto)bllProduto.getObjeto(pi.IDProduto), objCFOP, (decimal)objNotaItem.Total, (decimal)objNotaItem.RAT_Acrescimo, (decimal)objNotaItem.RAT_Desconto, objNota.Ent_Sai, objNota.Pessoa.bOrgaoPublico, objNota.ModeloDocto == 55 ? Tributacao.ModeloNota.NFe : Tributacao.ModeloNota.Modelo1);
        //                    objNotaItem.AtualizaImposto(imposto);
        //                    objNotaItem.IDCFOP = objCFOP.ID;

        //                    if (tno.bAtualizaPedido == Modelo.TituloType.Sim)
        //                    {
        //                        //Vincula o registro Nota Item com o itens do pedido
        //                        objNotaItem.IDPedidoItem = pi.ID;
        //                        //Atualiza a Quantidade retirada no Itens do Pedido
        //                        pi.QtdRetirada = pi.Quantidade;
        //                    }

        //                    //Grava o objeto nota item na Nota
        //                    objNota.NotaItems.Add(objNotaItem);

        //                    //Chama o método que totaliza o Imposto na Nota
        //                    this.TotalizaImposto(objNota, false, 0, 0);
        //                }

        //                //Calcula o Total da Nota
        //                objNota.TotalProduto = 0;
        //                foreach (Modelo.NotaItem ni in objNota.NotaItems)
        //                {
        //                    objNota.TotalProduto += ni.Total;
        //                }
        //                objNota.ValorDesconto += p.ValorDesconto == null ? 0 : p.ValorDesconto;
        //                objNota.ValorAcrescimo += p.ValorAcrescimo == null ? 0 : p.ValorAcrescimo;
        //                objNota.PercDesconto = ((objNota.TotalProduto > 0) ? (decimal)(objNota.ValorDesconto / objNota.TotalProduto * 100) : 0);
        //                objNota.PercDesconto = System.Math.Round((decimal)objNota.PercDesconto, 4);

        //                if (tno.bAtualizaPedido == Modelo.TituloType.Sim)
        //                {
        //                    //Atualiza o Status do Pedido
        //                    p.Status = Modelo.StatusOrcamento.Faturado;
        //                }

        //                decimal icmsretido = 0;
        //                foreach (Modelo.NotaICM ni in objNota.NotaICMs)
        //                {
        //                    icmsretido = icmsretido + (decimal)ni.ValorSubst;
        //                }
        //                objNota.TotalNota = objNota.TotalProduto + icmsretido - objNota.ValorDesconto + objNota.ValorAcrescimo + objNota.ValorFrete;

        //                if (i == (pPedido.Count - 1))
        //                {
        //                    if (!pFatLote)
        //                    {
        //                        //Foreach nas parcelas do pedido
        //                        foreach (Modelo.PedidoParcela pp in p.PedidoParcelas)
        //                        {
        //                            //Cria o objeto da parcela da nota
        //                            Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();

        //                            //Atribui os valores da parcela
        //                            objNotaParcela.Parcela = (int)pp.Parcela;
        //                            objNotaParcela.bEntrada = pp.bEntrada;
        //                            objNotaParcela.IDTipoDocumento = (int)pp.IDTipoDocumento;
        //                            objNotaParcela.IDPortador = pp.IDPortador;
        //                            objNotaParcela.IDBanco = pp.IDBanco;
        //                            objNotaParcela.Vencimento = (DateTime)pp.Vencimento;
        //                            objNotaParcela.Valor = (Decimal)pp.Valor;
        //                            objNotaParcela.bAlterado = pp.bAlterado;
        //                            objNotaParcela.NumBanco = pp.NumBanco;
        //                            objNotaParcela.NumAgencia = pp.NumAgencia;
        //                            objNotaParcela.NumContaCorrente = pp.NumContaCorrente;
        //                            objNotaParcela.NumCheque = pp.NumCheque;
        //                            objNotaParcela.Emitente = pp.Emitente;

        //                            //Grava o objeto parcela na Nota
        //                            objNota.NotaParcelas.Add(objNotaParcela);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (pParcelas == null)
        //                        {
        //                            this.gerarParcelas(objNota);
        //                        }
        //                        else
        //                        {
        //                            foreach (Modelo.Objeto.FinanceiroParcela parc in pParcelas)
        //                            {
        //                                //Cria o objeto da parcela da nota
        //                                Modelo.NotaParcela objNotaParcela = new Modelo.NotaParcela();

        //                                //Atribui os valores da parcela
        //                                objNotaParcela.Parcela = (int)parc.Parcela;
        //                                objNotaParcela.bEntrada = parc.bEntrada;
        //                                objNotaParcela.IDTipoDocumento = (int)parc.IDTipoDocumento;
        //                                objNotaParcela.IDPortador = tno.IDPortador;
        //                                objNotaParcela.IDBanco = tno.IDBanco;
        //                                objNotaParcela.Vencimento = (DateTime)parc.Vencimento;
        //                                objNotaParcela.Valor = (Decimal)parc.Valor;
        //                                objNotaParcela.bAlterado = (byte)parc.bAlterado;
        //                                objNotaParcela.NumBanco = parc.NumBanco;
        //                                objNotaParcela.NumAgencia = parc.NumAgencia;
        //                                objNotaParcela.NumContaCorrente = parc.NumContaCorrente;
        //                                objNotaParcela.NumCheque = parc.NumCheque;
        //                                objNotaParcela.Emitente = parc.Emitente;
        //                                objNotaParcela.CpfCnpj = parc.CpfCnpj;
        //                                objNotaParcela.NomeBanco = parc.NomeBanco;

        //                                //Grava o objeto parcela na Nota
        //                                objNota.NotaParcelas.Add(objNotaParcela);
        //                            }
        //                        }
        //                    }
        //                }

        //                if (tno.bAtualizaPedido == Modelo.TituloType.Não)
        //                {
        //                    bllEstoque.AtualizaEstoquePedido(p, true, _db);
        //                    if ((from c in _db.Configuracaos
        //                         select c).Single().BExcluirOrcamentoFaturado)
        //                        _db.Pedidos.DeleteOnSubmit(p);
        //                    //#803^
        //                }
        //            }
        //            using (TransactionScope transacao = new TransactionScope())
        //            {
        //                bllEstoque.AtualizaEstoqueNota(objNota, false, _db);

        //                //Finaliza o processo que Fatura os Pedidos da Lista
        //                _db.SubmitChanges();

        //                //Chama a rotina que gera o Financeiro
        //                if (objNota.bGeraFinanceiro == 1)
        //                {
        //                    this.GeraFinanceiro(_db, objNota, false, out mensagem);
        //                }

        //                transacao.Complete();
        //            }
        //            #endregion
        //        }
        //        catch (ChangeConflictException cce)
        //        {
        //            ret = false;
        //            MessageBox.Show("Erro ao faturar pedido(s).\nErro " + cce.Message, "Aviso [Conflito]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        }
        //        catch (SqlException ex)
        //        {
        //            ret = false;
        //            string erro = "Erro ao faturar pedido(s).\nErro " + ex.Message;

        //            if (ex.Number == 1205)
        //                erro = "Não foi possivel faturar o pedido.\nTente novamente.";

        //            MessageBox.Show(erro, "Aviso [SQL]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        }
        //        catch (Exception e)
        //        {
        //            ret = false;
        //            MessageBox.Show("Erro ao faturar pedido(s)." + Environment.NewLine + e.Message + Environment.NewLine + (e.InnerException != null ? e.InnerException.Message : ""), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        }

        //        bllProduto.Dispose();
        //    }

        //    if (ret == true)
        //    {
        //        objNota = (from n in db.Notas
        //                   where n.ID == objNota.ID
        //                   select n).Single();

        //        if (pMessageButtonFaturar == Modelo.MessageButtonFaturar.NotaFiscal)
        //        {
        //            if (MessageBox.Show("Deseja imprimir a nota fiscal?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                this.ImprimeNota(objNota, 35, tipoImpressora(), objNota.TipoNota.Impressora);
        //        }
        //        else if (pMessageButtonFaturar == Modelo.MessageButtonFaturar.CupomFiscal)
        //        {
        //            cwkGestao. CupomFiscal.Imprime(objNota);
        //        }
        //    }

        //    return ret;
        //}

        public List<string> IncluirProduto(Modelo.DataClassesDataContext pdb, List<int> pLista, Modelo.Nota objNota, List<int> pProdsCFOPs)
        {
            List<string> naoIncluidos = new List<string>();
            Modelo.Produto objProduto = new Modelo.Produto();

            BLL.Produto bllProduto = new BLL.Produto(db);
            BLL.Unidade bllUnidade = new BLL.Unidade(db);
            BLL.NotaItem bllNotaItem = new BLL.NotaItem(db);

            string ufDestino = (objNota.Ent_Sai == Modelo.InOutType.Entrada ? objNota.Filial.Cidade.UF.Sigla : objNota.PessoaUF);
            Modelo.TabelaCFOP objTabelaCFOP;
            if (objNota.Filial.Cidade.UF.Sigla != objNota.PessoaUF)
            {
                if (objNota.bPessoaContribuinte == 1)
                    objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopFEstC;
                else
                    objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopFEstNC;
            }
            else
                objTabelaCFOP = objNota.TipoNota.Operacao.TabelaCfopDEst;

            for (int y = 0; y < pLista.Count; y++)
            {
                try
                {
                    Modelo.NotaItem objNotaItem = new Modelo.NotaItem();
                    objProduto = (Modelo.Produto)bllProduto.getObjeto(pLista[y]);
                    objNotaItem.Produto = objProduto;
                    objNotaItem.IDProduto = objProduto.ID;
                    objNotaItem.InfAdicionais = objProduto.InfAdicionais;
                    objNotaItem.IDCFOP = objProduto.IDCFOP;

                    pProdsCFOPs.Add(objProduto.IDCFOP == null ? 0 : (int)objProduto.IDCFOP);

                    //#594 ajuste
                    objNotaItem.Valor = bllProduto.getPreco(objProduto, objNota.TabelaPreco.Codigo, Modelo.TipoPrecoType.Normal, objNota.Condicao);
                    objNotaItem.Valor = System.Math.Round(objNotaItem.Valor, 4);
                    objNotaItem.ValorCalculado = objNotaItem.Valor;

                    decimal vlrminimo = bllProduto.getPreco(objProduto, objNota.TabelaPreco.Codigo, Modelo.TipoPrecoType.Mínimo);
                    vlrminimo = System.Math.Round(vlrminimo, 4);

                    objNotaItem.ProdutoNome = objProduto.Nome;
                    objNotaItem.ProdutoDescReduzida = objProduto.DescReduzida;
                    objNotaItem.orig_N11 = objProduto.OrigemProd;
                    objNotaItem.cEnq_O06 = objProduto.ENQ_IPI.ToString();

                    objNotaItem.Quantidade = 1;
                    objNotaItem.ValorDesconto = 0;
                    objNotaItem.RAT_Acrescimo = 0;
                    objNotaItem.RAT_Desconto = 0;

                    objNotaItem.SubTotal = objNotaItem.Quantidade * objNotaItem.Valor;

                    objNotaItem.Total = ((decimal)(objNotaItem.Valor * objNotaItem.Quantidade) - (decimal)objNotaItem.ValorDesconto);

                    if (objNotaItem.Total > 0 && (objNota.Ent_Sai == Modelo.InOutType.Saída || (objNota.Ent_Sai == Modelo.InOutType.Entrada && objTabelaCFOP.bDevolucao == true)))
                    {
                        Modelo.TabelaCFOP objCFOP;
                        if (objProduto.IDCFOP.HasValue && objProduto.IDCFOP > 0)
                            objCFOP = objProduto.TabelaCFOP;
                        else
                            objCFOP = objTabelaCFOP;
                        Modelo.ImpostoNF imposto = BLL.Tributacao.CalculaIcms(objNota.Filial, ufDestino, Convert.ToBoolean(objNota.bPessoaContribuinte), objNotaItem.Produto, objCFOP, (decimal)objNotaItem.Total, (decimal)objNotaItem.RAT_Acrescimo, (decimal)objNotaItem.RAT_Desconto, objNotaItem.Ent_Sai, objNota.Pessoa.bOrgaoPublico, objNota.ModeloDocto == 55 ? Tributacao.ModeloNota.NFe : Tributacao.ModeloNota.Modelo1);
                        objNotaItem.AtualizaImposto(imposto);
                        objNotaItem.IDCFOP = objCFOP.ID;
                    }
                    else
                    {
                        objNotaItem.orig_N11 = objProduto.OrigemProd;
                        objNotaItem.pPIS_Q08 = objProduto.pPIS_Q08;
                        objNotaItem.pCOFINS_S08 = objProduto.CST_Cofins;
                        objNotaItem.pIPI_O13 = objProduto.AliquotaIPI;
                        objNotaItem.cEnq_O06 = objProduto.ENQ_IPI.Value.ToString();

                        objNotaItem.CST_Pis = BLL.NFe.Conv_CST_Pis(objProduto.CST_Pis.Value);
                        objNotaItem.CST_Cofins = BLL.NFe.Conv_CST_Cofins(objProduto.CST_Cofins.Value);
                        objNotaItem.CST_Ipi = BLL.NFe.Conv_CST_IPI(objProduto.CST_IPI.Value);
                        objNotaItem.TAG_CST = BLL.NFe.Conv_TAG_CST(objProduto.Tributacao.Value);

                        objNotaItem.SitTrib = objProduto.OrigemProd + objNotaItem.TAG_CST;
                    }

                    objNotaItem.SubTotal = objNotaItem.Total;
                    objNotaItem.ValorDesconto = 0;
                    objNotaItem.PercDesconto = 0;
                    objNotaItem.Unidade = bllUnidade.getSigla((int)objProduto.IDUnidade);
                    objNotaItem.Ent_Sai = objNota.TipoNota.Ent_Sai;
                    objNotaItem.Dt = objNota.Dt;
                    objNotaItem.bAtualizaEstoque = objNota.TipoNota.bAtualizaEstoque;
                    objNotaItem.PesoBruto = objProduto.PesoBruto;
                    objNotaItem.PesoLiquido = objProduto.PesoLiquido;

                    objNotaItem.Sequencia = bllNotaItem.RetornaMAXSequencia(objNota);

                    objNota.NotaItems.Add(objNotaItem);
                }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                {
                    naoIncluidos.Add("Produto: " + objProduto.Codigo.ToString() + " - " + objProduto.Nome);
                }
            }

            bllProduto.Dispose();
            bllUnidade.Dispose();
            bllNotaItem.Dispose();

            return naoIncluidos;
        }

        public Modelo.Objeto.Documento GeraFinanceiro(Modelo.DataClassesDataContext pdb, Modelo.Nota objNota, bool pManual, out string mensagem)
        {
            mensagem = String.Empty;
            //Monta a Lista com as Parcelas
            List<Modelo.Objeto.FinanceiroParcela> listaParcelas = GeraParcelasFinanceiro(objNota);

            //Monta a Lista com os Planos de Contas 
            List<Modelo.Objeto.FinanceiroPlanoContas> listaPContas = new List<Modelo.Objeto.FinanceiroPlanoContas>();
            mensagem = GeraPlanosContasFinanceiro(pdb, objNota, listaPContas);

            //Cria o Objeto com as informações do Documento
            Modelo.Objeto.Documento moDocumento = new Modelo.Objeto.Documento();
            moDocumento.IDBanco = objNota.TipoNota.IDBanco.Value;
            moDocumento.IDAcrescimo = objNota.TipoNota.IDAcrescimo.Value;
            moDocumento.IDCondicao = pManual == true ? objNota.Condicao.ID : objNota.IDCondicao.Value;
            moDocumento.IDFilial = objNota.Filial.ID;
            moDocumento.IDHistorico = objNota.TipoNota.IDHistorico.Value;
            moDocumento.IDPessoa = objNota.Pessoa == null ? objNota.IDPessoa.Value : objNota.Pessoa.ID;
            moDocumento.IDPortador = objNota.TipoNota.IDPortador.Value;
            moDocumento.NumDocumento = objNota.Numero.Value.ToString();
            moDocumento.NumRequisicao = "";
            if (objNota.Ent_Sai == Modelo.InOutType.Saída)
            {
                moDocumento.Tipo = Modelo.TipoDocumentoType.Receber;
            }
            else
            {
                moDocumento.Tipo = Modelo.TipoDocumentoType.Pagar;
            }
            moDocumento.Dt = objNota.Dt;
            moDocumento.DtDigitacao = objNota.DtDigitacao;
            moDocumento.ValorTotal = objNota.TotalNota;
            moDocumento.ComplementoHist = objNota.TipoNota.ComplementoHist;
            moDocumento.Nota = objNota;

            //Chama o processo que gera o financeiro
            BLL.Financeiro bllFinanceiro = new Financeiro(pdb);
            bllFinanceiro.GeraFinanceiro(listaParcelas, listaPContas, moDocumento);
            
            bllFinanceiro.Dispose();
            return moDocumento;
        }

        private List<Modelo.Objeto.FinanceiroParcela> GeraParcelasFinanceiro(Modelo.Nota objNota)
        {
            List<Modelo.Objeto.FinanceiroParcela> listaParcelas = new List<Modelo.Objeto.FinanceiroParcela>();

            foreach (Modelo.NotaParcela parc in objNota.NotaParcelas)
            {
                Modelo.Objeto.FinanceiroParcela parcela = new Modelo.Objeto.FinanceiroParcela();

                parcela.Parcela = parc.Parcela;
                parcela.Vencimento = parc.Vencimento;
                parcela.Valor = parc.Valor;
                parcela.DtContabil = objNota.Dt;
                parcela.bAlterado = 0;
                parcela.IDTipoDocumento = parc.IDTipoDocumento;
                parcela.bEntrada = parc.bEntrada;
                parcela.NomeBanco = parc.NomeBanco;
                parcela.NumAgencia = parc.NumAgencia;
                parcela.NumBanco = parc.NumBanco;
                parcela.NumCheque = parc.NumCheque;
                parcela.NumContaCorrente = parc.NumContaCorrente;
                parcela.Emitente = parc.Emitente;
                parcela.CpfCnpj = parc.CpfCnpj;
                listaParcelas.Add(parcela);
            }
            return listaParcelas;
        }

        private string GeraPlanosContasFinanceiro(Modelo.DataClassesDataContext pdb, Modelo.Nota objNota, List<Modelo.Objeto.FinanceiroPlanoContas> listaPContas)
        {
            string mensagem = String.Empty;
            GerarPlanosContaProdutos(pdb, objNota, listaPContas, ref mensagem);
            GerarPlanosContaServicos(pdb, objNota, listaPContas);
            GerarPlanoContaFrete(objNota, listaPContas);
            GerarPlanoContaIPI(objNota, listaPContas);
            return mensagem;
        }        

        private static void GerarPlanosContaProdutos(Modelo.DataClassesDataContext pdb, Modelo.Nota objNota, List<Modelo.Objeto.FinanceiroPlanoContas> listaPContas, ref string mensagem)
        {
            List<Modelo.NotaItem> itensSemPlanoConta = new List<Modelo.NotaItem>();
            if (objNota.Ent_Sai == Modelo.InOutType.Entrada)
            {
                foreach (Modelo.NotaItem item in objNota.NotaItems)
                {
                    if (item.Produto.PlanoConta != null)
                    {
                        var existente = listaPContas.Where(p => p.ID == item.Produto.PlanoConta.ID);
                        if (existente.Count() == 1)
                            existente.Single().Valor += item.Total - (item.RAT_Desconto.HasValue ? item.RAT_Desconto.Value : 0);
                        else
                        {
                            Modelo.Objeto.FinanceiroPlanoContas fPlanoConta = new Modelo.Objeto.FinanceiroPlanoContas();
                            fPlanoConta.ID = item.Produto.PlanoConta.ID;
                            fPlanoConta.Conta = item.Produto.PlanoConta.Codigo;
                            fPlanoConta.Descricao = item.Produto.PlanoConta.Nome;
                            fPlanoConta.Valor = item.Total - (item.RAT_Desconto.HasValue ? item.RAT_Desconto.Value : 0);
                            listaPContas.Add(fPlanoConta);
                        }
                    }
                    else
                        itensSemPlanoConta.Add(item);
                }
            }
            else
                itensSemPlanoConta.AddRange(objNota.NotaItems.ToList());

            Modelo.Objeto.FinanceiroPlanoContas planoconta;
            if (itensSemPlanoConta.Count > 0)
            {
                if (objNota.TipoNota.PlanoConta == null)
                    objNota.TipoNota.PlanoConta = (from p in pdb.PlanoContas
                                                   where p.ID == objNota.TipoNota.IDPlanoConta
                                                   select p).Single();

                var existente = listaPContas.Where(p => p.ID == objNota.TipoNota.PlanoConta.ID);
                if (existente.Count() == 1)
                    planoconta = existente.Single();
                else
                {
                    planoconta = new Modelo.Objeto.FinanceiroPlanoContas();
                    planoconta.ID = objNota.TipoNota.PlanoConta.ID;
                    planoconta.Conta = objNota.TipoNota.PlanoConta.Codigo;
                    planoconta.Descricao = objNota.TipoNota.PlanoConta.Nome;
                    planoconta.Valor = 0;
                    listaPContas.Add(planoconta);
                }

                if (objNota.Ent_Sai == Modelo.InOutType.Entrada)
                {
                    planoconta.Valor += itensSemPlanoConta.Sum(p => p.Total);
                    StringBuilder str = new StringBuilder();
                    str.AppendLine("Foi utilizado o plano de contas configurado no tipo de movimentação para os seguintes produtos:");
                    foreach (Modelo.NotaItem item in itensSemPlanoConta)
                    {
                        str.AppendLine(" -" + item.Produto.Nome);
                    }
                    mensagem = str.ToString();
                }
                else
                    planoconta.Valor = objNota.TotalProduto.Value - itensSemPlanoConta.Sum(p => p.RAT_Desconto.Value);
            }
        }

        private static void GerarPlanosContaServicos(Modelo.DataClassesDataContext pdb, Modelo.Nota objNota, List<Modelo.Objeto.FinanceiroPlanoContas> listaPContas)
        {
            if (objNota.NotaItemServicos.Count > 0 && objNota.TipoNota.IDPlanoContaServico.HasValue && objNota.TipoNota.IDPlanoContaServico > 0)
            {
                Modelo.Objeto.FinanceiroPlanoContas planoconta;
                if (objNota.TipoNota.PlanoContaServico == null)
                    objNota.TipoNota.PlanoContaServico = pdb.PlanoContas.Where(p => p.ID == objNota.TipoNota.IDPlanoContaServico).Single();
                var existente = listaPContas.Where(p => p.ID == objNota.TipoNota.PlanoContaServico.ID);
                if (existente.Count() == 1)
                    planoconta = existente.Single();
                else
                {
                    planoconta = new Modelo.Objeto.FinanceiroPlanoContas();
                    planoconta.ID = objNota.TipoNota.PlanoContaServico.ID;
                    planoconta.Conta = objNota.TipoNota.PlanoContaServico.Codigo;
                    planoconta.Descricao = objNota.TipoNota.PlanoContaServico.Nome;
                    planoconta.Valor = 0;
                    listaPContas.Add(planoconta);
                }
                planoconta.Valor += objNota.NotaItemServicos.Sum(p => p.SubTotal);
            }
        }

        private static void GerarPlanoContaFrete(Modelo.Nota objNota, List<Modelo.Objeto.FinanceiroPlanoContas> listaPContas)
        {
            if (objNota.ValorFrete.HasValue && objNota.ValorFrete.Value > 0)
            {
                var planoconta = new Modelo.Objeto.FinanceiroPlanoContas();
                Modelo.PlanoConta objPlanoConta = objNota.TipoNota.PlanoContaFrete;
                planoconta.ID = objPlanoConta.ID;
                planoconta.Conta = objPlanoConta.Codigo;
                planoconta.Descricao = objPlanoConta.Nome;
                planoconta.Valor = objNota.ValorFrete.Value;
                listaPContas.Add(planoconta);
            }
        }

        private static void GerarPlanoContaIPI(Modelo.Nota objNota, List<Modelo.Objeto.FinanceiroPlanoContas> listaPContas)
        {
            decimal? valorIPI = objNota.NotaICMs.Sum(n => n.ValorIPI);
            if (valorIPI.HasValue && valorIPI.Value > 0)
            {
                var planoconta = new Modelo.Objeto.FinanceiroPlanoContas();
                Modelo.PlanoConta objPlanoConta = objNota.TipoNota.PlanoContaIPI;
                planoconta.ID = objPlanoConta.ID;
                planoconta.Conta = objPlanoConta.Codigo;
                planoconta.Descricao = objPlanoConta.Nome;
                planoconta.Valor = valorIPI.Value;
                listaPContas.Add(planoconta);
            }
        }

        #region ImprimeNotaFiscal
        public void ImprimeNota(Modelo.Nota objNota, int itensPag, Modelo.DestinoImpressao pDestinoImpressao, Modelo.Impressora objImpressora)
        {
            if (objNota.NotaItems.Count() > itensPag)
            {
                MessageBox.Show("Quantidade de itens maior que permitido para impressão.\nVerifique.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (Modelo.dcNotaDataContext _db = new Modelo.dcNotaDataContext())
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    try
                    {
                        var nota = (from n in _db.NotaImpressao
                                    where n.ID == objNota.ID
                                    select n).Single();

                        nota.bImpressa = 1;

                        _db.SubmitChanges();
                        transacao.Complete();
                    }
                    catch (ChangeConflictException cce)
                    {
                        MessageBox.Show("Nota Fiscal não foi impressa.\nErro " + cce.Message, "Aviso [Conflito]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Nota Fiscal não foi impressa.\nErro " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }

            EpsonCodes lCodes = new EpsonCodes();

            Reporter lPrinter = new Reporter();

            //if (objImpressora.Caminho.IndexOf('\\') == -1)
            //    lPrinter.Output = objImpressora.Caminho;
            //else
            //    lPrinter.Output = @objImpressora.Caminho;

            lPrinter.Output = "LPT1";

            lPrinter.NomeArquivo = "n" + objNota.Codigo.ToString() + ".txt";

            //Verifica se é para imprimir o crédito de icms
            decimal perc = 0;
            decimal vlr = 0;
            string texto = "";
            bool impFaixaSimples = false;
            bool verificaFaixa = false;

            if (MessageBox.Show("Deseja imprimir texto para crédito de ICMS?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                verificaFaixa = true;

            if (verificaFaixa)
            {
                texto = "PERMITE O APROVEITAMENTO DO CREDITO DE ICMS SOB A ALIQUOTA DE {0}%, NO VALOR DE {1}, CONFORME ARTIGO 23 DA LC 123/2006";
                Modelo.Configuracao objCfg = (from c in db.Configuracaos
                                              where c.Codigo == 1
                                              select c).Single();
                if (objNota.Filial.IdTabelaFaixaFatSimples != null && objNota.Filial.IdTabelaFaixaFatSimples > 0)
                {
                    Modelo.TabelaFaixaFatSimples objTabela = (from t in db.TabelaFaixaFatSimples
                                                              where t.ID == objNota.Filial.IdTabelaFaixaFatSimples
                                                              select t).Single();


                    if (objTabela.PercReducaoBC != null && objTabela.PercReducaoBC > 0)
                        perc = (decimal)objTabela.ICMS - (((decimal)objTabela.ICMS * (decimal)objTabela.PercReducaoBC) / 100);
                    else
                        perc = (decimal)objTabela.ICMS;

                    perc = System.Math.Round(perc, 2);

                    vlr = (System.Math.Round((decimal)objNota.TotalNota, 2) * (perc / 100));

                    impFaixaSimples = true;

                    itensPag--;
                }
            }

            lPrinter.StartJob();
            int count = 0;
            int pag = 1;

            lPrinter.PrintText(count, 0, lCodes.Reset + lCodes.LinesInch8 + lCodes.CondensedOn);

            ImprimeCabeca(lCodes, lPrinter, objNota, ref count, StrNumeroPagina(pag, objNota.NotaItems.Count, itensPag));

            int cItens = 0;
            decimal? Desconto = 0;

            int qtCFOP = (from c in db.NotaICMs
                          where c.IDNota == objNota.ID
                          select c).Count();

            foreach (Modelo.NotaItem p in objNota.NotaItems)
            {
                Desconto = Math.Round((p.Valor * (decimal)p.PercDesconto) / 100, 4);

                if (qtCFOP > 1)
                {
                    lPrinter.PrintText(count, 0, String.Format("{0,-4} ", p.TabelaCFOP.CFOP));
                    lPrinter.PrintText(count, 6, String.Format("{0,-7} ", p.Produto.Codigo));
                }
                else
                {
                    lPrinter.PrintText(count, 0, String.Format("{0,-12} ", p.Produto.Codigo));
                }

                if (p.ProdutoDescReduzida != null && p.ProdutoDescReduzida.Length > 0)
                {
                    lPrinter.PrintText(count, 14, String.Format("{0,-60} ", p.ProdutoNome + "-" + p.ProdutoDescReduzida));
                }
                else
                {
                    lPrinter.PrintText(count, 14, String.Format("{0,-60} ", p.ProdutoNome));
                }

                lPrinter.PrintText(count, 76, String.Format("{0,2} ", p.SitTrib));
                lPrinter.PrintText(count, 82, String.Format("{0,4} ", p.Unidade));
                lPrinter.PrintText(count, 89, String.Format("{0,9} ", p.Quantidade));
                lPrinter.PrintText(count, 100, String.Format("{0,11} ", String.Format("{0:n}", p.Valor - Desconto)));
                lPrinter.PrintText(count, 115, String.Format("{0,15} ", p.Total));
                lPrinter.PrintText(count, 133, String.Format("{0,2} ", Math.Truncate(p.AliqICMS.Value)));

                count++;
                cItens++;

                if (cItens == itensPag && (cItens * pag) < objNota.NotaItems.Count)
                {
                    if (impFaixaSimples)
                    {
                        lPrinter.PrintText(count, 0, String.Format(texto, String.Format("{0:n2}", perc), String.Format("{0:c2}", vlr)));
                        impFaixaSimples = false;
                    }

                    pag++;
                    ImprimeRoda(lCodes, lPrinter, objNota, false, ref count, pag);
                    count += 6;
                    //lPrinter.PrintText(count++, 0, " ");
                    ImprimeCabeca(lCodes, lPrinter, objNota, ref count, StrNumeroPagina(pag, objNota.NotaItems.Count, itensPag));
                    cItens = 0;
                }
            }

            if (qtCFOP > 1)
            {
                if ((pag - 1) > 1)
                {
                    count = 84 + (59 * (pag - 2));
                }
                else
                {
                    count = 55;
                }

                var totalPag = (from n in db.NotaICMs
                                join c in db.TabelaCFOPs on n.IDCFOP equals c.ID
                                where n.IDNota == objNota.ID
                                group n by new { c.CFOP } into g
                                select new
                                {
                                    CFOP = g.Key.CFOP,
                                    ValorTotalProd = g.Sum(p => p.ValorTotalProdutos)
                                });

                int x, x2, t, t2, auxCount;
                x = 1;
                t = 7;
                x2 = t + 1;
                t2 = 19;
                auxCount = 1;
                foreach (var tot in totalPag)
                {
                    lPrinter.PrintText(count, x, String.Format("{0," + t.ToString() + "}", tot.CFOP));
                    lPrinter.PrintText(count, x2, String.Format("{0," + t2.ToString() + "}", tot.ValorTotalProd));
                    x += 20;

                    if (auxCount != totalPag.Count())
                    {
                        lPrinter.PrintText(count, x, "/");
                    }

                    x += 2;
                    t = x + 6;
                    x2 = t + 1;
                    t2 = x2 + 11;
                    auxCount++;
                }
            }

            if (!String.IsNullOrEmpty(objNota.Observacao1))
            {
                count = 52;
                lPrinter.PrintText(count, 0, "OBS: " + objNota.Observacao1);
            }

            count = 53;
            if (impFaixaSimples)
            {
                lPrinter.PrintText(count, 0, String.Format(texto, String.Format("{0:n2}", perc), String.Format("{0:c2}", vlr)));
                impFaixaSimples = false;
            }

            ++count;
            ++count;
            ++count;

            pag++;

            ImprimeRoda(lCodes, lPrinter, objNota, true, ref count, pag);

            count = 86;
            lPrinter.PrintText(count, 0, " " + lCodes.Reset);

            lPrinter.EndJob();

            switch (pDestinoImpressao)
            {
                case Modelo.DestinoImpressao.Vídeo:
                    lPrinter.PreviewJob();
                    break;
                case Modelo.DestinoImpressao.Impressora:
                    lPrinter.PrintJob();
                    break;
                default:
                    break;
            }
        }

        private void ImprimeCabeca(EpsonCodes lCodes, Reporter lPrinter, Modelo.Nota objNota, ref int count, string pag)
        {
            int colTipo = 0;
            if (objNota.Ent_Sai == Modelo.InOutType.Saída)
            {
                colTipo = 86;
            }
            else
            {
                colTipo = 96;
            }
            //Marca o tipo de nota: Entrada ou Saida
            lPrinter.PrintText(++count, colTipo, "XX");

            //Imprime o numero da nota
            lPrinter.PrintText(count, 112, lCodes.ExpandedOn + lCodes.BoldOn + String.Format("{0:000000}", objNota.Numero.Value) + lCodes.BoldOff + lCodes.ExpandedOff);

            //Numero da pagina
            ++count;
            lPrinter.PrintText(++count, 112, pag);

            ++count;
            ++count;
            ++count;
            ++count;
            ++count;

            var objNICMS = (from n in db.NotaICMs
                            join c in db.TabelaCFOPs on n.IDCFOP equals c.ID
                            where n.IDNota == objNota.ID
                            select new
                            {
                                CFOP = c.CFOP,
                                Nome = c.Nome
                            }).First();

            lPrinter.PrintText(count, 0, String.Format("{0,-33}", objNICMS.Nome));
            lPrinter.PrintText(count, 35, String.Format("{0,-4}", objNICMS.CFOP));

            ++count;
            ++count;
            ++count;

            lPrinter.PrintText(count, 0, String.Format("{0,-83}", lPrinter.CleanText(objNota.Pessoa.Nome) + " (" + objNota.Pessoa.Codigo.ToString() + ")"));
            lPrinter.PrintText(count, 87, String.Format("{0,-27}", objNota.Pessoa.CNPJ_CPF));
            lPrinter.PrintText(count, 119, objNota.Dt.ToShortDateString());

            ++count;
            ++count;

            lPrinter.PrintText(count, 0, String.Format("{0,-58}", lPrinter.CleanText(objNota.PessoaEndereco + ", " + objNota.Complemento)));
            lPrinter.PrintText(count, 61, String.Format("{0,-35}", lPrinter.CleanText(objNota.PessoaBairro)));
            lPrinter.PrintText(count, 101, objNota.PessoaCEP == null ? "" : objNota.PessoaCEP.TrimEnd()); //cep

            ++count;
            ++count;

            lPrinter.PrintText(count, 0, String.Format("{0,-53}", lPrinter.CleanText(objNota.PessoaCidade)));
            lPrinter.PrintText(count, 56, String.Format("{0,-20}", objNota.PessoaTelefone));
            lPrinter.PrintText(count, 83, String.Format("{0,-2}", objNota.PessoaUF));
            lPrinter.PrintText(count, 89, String.Format("{0,-26}", objNota.PessoaInscRG));

            ++count;
            ++count;
            ++count;
            ++count;
        }

        private void ImprimeRoda(EpsonCodes lCodes, Reporter lPrinter, Modelo.Nota objNota, bool imp, ref int count, int pag)
        {
            if ((pag - 1) > 1)
            {
                count = 84 + (60 * (pag - 2));
            }
            else
            {
                count = 56;
            }
            if (imp)
            {
                decimal? BaseICMS = db.NotaICMs.Where(n => n.IDNota == objNota.ID).Sum(n => n.BaseICMS.Value);
                decimal? ValorICMS = db.NotaICMs.Where(n => n.IDNota == objNota.ID).Sum(n => n.ValorICMS.Value);
                decimal? BaseSubst = db.NotaICMs.Where(n => n.IDNota == objNota.ID).Sum(n => n.BaseSubst.Value);
                decimal? ValorSubst = db.NotaICMs.Where(n => n.IDNota == objNota.ID).Sum(n => n.ValorSubst.Value);

                lPrinter.PrintText(count, 12, String.Format("{0,-12} ", String.Format("{0:n}", BaseICMS)));
                lPrinter.PrintText(count, 38, String.Format("{0,-12} ", String.Format("{0:n}", ValorICMS)));
                lPrinter.PrintText(count, 66, String.Format("{0,-12} ", String.Format("{0:n}", BaseSubst)));
                lPrinter.PrintText(count, 93, String.Format("{0,-12} ", String.Format("{0:n}", ValorSubst)));
                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", objNota.TotalProduto)));

                ++count;
                ++count;

                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", objNota.TotalNota)));

                ++count;
                ++count;
                ++count;

                lPrinter.PrintText(count, 0, String.Format("{0,-56}", lPrinter.CleanText(objNota.TransNome)));
                lPrinter.PrintText(count, 77, "1");
                lPrinter.PrintText(count, 83, String.Format("{0,-14}", objNota.TransPlaca));
                lPrinter.PrintText(count, 101, String.Format("{0,-2}", objNota.TransPlacaUF));
                lPrinter.PrintText(count, 107, String.Format("{0,-26}", objNota.TransCNPJCPF));

                ++count;
                ++count;

                lPrinter.PrintText(count, 0, String.Format("{0,-56}", lPrinter.CleanText(objNota.TransEndereco)));
                lPrinter.PrintText(count, 58, String.Format("{0,-39}", lPrinter.CleanText(objNota.TransCidade)));
                lPrinter.PrintText(count, 101, String.Format("{0,-2}", objNota.TransPlacaUF));
                lPrinter.PrintText(count, 107, String.Format("{0,-26}", ""));

                ++count;
                ++count;

                string aux = (objNota.VolumeQuant == null || objNota.VolumeQuant == 0 ? "" : objNota.VolumeQuant.ToString());

                lPrinter.PrintText(count, 0, String.Format("{0,-15}", aux));

                lPrinter.PrintText(count, 19, String.Format("{0,-12}", objNota.VolumeEspecie));
                lPrinter.PrintText(count, 37, String.Format("{0,-18}", objNota.VolumeMarca));
                lPrinter.PrintText(count, 59, String.Format("{0,-22}", objNota.VolumeNumero));

                aux = (objNota.VolumePesoBruto == null || objNota.VolumePesoBruto == 0 ? "" : objNota.VolumePesoBruto.ToString());

                lPrinter.PrintText(count, 89, String.Format("{0,-12}", aux));

                aux = (objNota.VolumePesoLiquido == null || objNota.VolumePesoLiquido == 0 ? "" : objNota.VolumePesoLiquido.ToString());

                lPrinter.PrintText(count, 114, String.Format("{0,-12}", aux));

                ++count;
                ++count;

                for (int i = 0; i < objNota.NotaParcelas.Count; i++)
                {
                    if (i == 0)
                    {
                        lPrinter.PrintText(count, 0, String.Format("{0,-12}", "Vencimento: "));
                    }
                    else
                    {
                        lPrinter.PrintText(count, 0, String.Format("{0,-12}", "            "));
                    }
                    lPrinter.PrintText(count, 12, String.Format("{0,-13}", objNota.Numero + " - " + objNota.NotaParcelas[i].Parcela + "/" + objNota.NotaParcelas.Count));
                    lPrinter.PrintText(count, 25, String.Format("{0,-10}", objNota.NotaParcelas[i].Vencimento.ToShortDateString()));
                    lPrinter.PrintText(count, 37, String.Format("{0,-12}", objNota.NotaParcelas[i].Valor));
                    count++;
                }
            }
            else
            {
                lPrinter.PrintText(count, 12, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 38, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 66, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 93, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", "****")));

                count += 2;

                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
            }

            if ((pag - 1) > 1)
            {
                count = 84 + (85 * (pag - 2));
            }
            else
            {
                count = 81;
            }
            lPrinter.PrintText(count, 122, lCodes.BoldOn + String.Format("{0:000000}", objNota.Numero.Value) + lCodes.BoldOff);
        }

        /// <summary>
        /// Método que retorna uma string com o número da página
        /// </summary>
        /// <param name="pag">Número da página atual</param>
        /// <param name="qtItens">Quantidade de itens do pedido</param>
        /// <param name="itensPag">Quantidade de itens por página</param>
        /// <returns>string no formato "nº/qt"</returns>
        private string StrNumeroPagina(int pag, int qtItens, int itensPag)
        {
            return pag.ToString() + "/" + CalculaQtPag(pag, qtItens, itensPag).ToString();
        }

        /// <summary>
        /// Método que calcula a quantidade de páginas do pedido
        /// </summary>
        /// <param name="pag">Número da página atual</param>
        /// <param name="qtItens">Quantidade de itens do pedido</param>
        /// <param name="itensPag">Quantidade de itens por página</param>
        /// <returns>Quantidade de páginas</returns>
        private int CalculaQtPag(int pag, int qtItens, int itensPag)
        {
            if (qtItens < itensPag)
            {
                return 1;
            }

            if (qtItens % itensPag > 0)
            {
                return Convert.ToInt32(Math.Truncate((decimal)qtItens / itensPag) + 1);
            }
            else
            {
                return Convert.ToInt32(qtItens / itensPag);
            }
        }
        public void ImprimeNotaGabarito()
        {
            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;
            int count;
            lPrinter = new Reporter();
            lPrinter.Output = @"lpt1";
            lPrinter.NomeArquivo = "doc.txt";
            lPrinter.StartJob();
            count = 0;

            lCodes.SetPageSize(83);

            lPrinter.PrintText(count, 0, lCodes.Normal + lCodes.LinesInch8 + lCodes.CondensedOn);

            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count, 0, lCodes.CondensedOff + lCodes.LinesInch6 + lCodes.Normal + lCodes.Eject);

            lPrinter.EndJob();

            //lPrinter.PreviewJob();
            lPrinter.PrintJob();
        }
        #endregion

        public List<Modelo.Documento> getListaDocumento(int pIDNota)
        {
            List<Modelo.Documento> lista = new List<Modelo.Documento>();

            lista = (from d in db.Documentos
                     where d.IDNota == pIDNota
                     select d).ToList();

            return lista;
        }

        public bool CancelarNF(int pIDNota, DateTime pDataCanc, string pMovito)
        {
            bool ret = true;

            using (TransactionScope transacao = new TransactionScope())
            {
                try
                {
                    using (Modelo.dcNotaDataContext _db = new Modelo.dcNotaDataContext())
                    {
                        var nota = (from n in _db.NotaImpressao
                                    where n.ID == pIDNota
                                    select n).Single();

                        nota.CancDt = pDataCanc;
                        nota.CancMotivo = pMovito;
                        nota.CancUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;

                        _db.SubmitChanges();
                    }

                    List<Modelo.Documento> lista;

                    lista = (from d in db.Documentos
                             where d.IDNota == pIDNota
                             select d).ToList();

                    foreach (Modelo.Documento doc in lista)
                    {
                        if (doc.Saldo != doc.ValorTotal)
                            throw new Exception("Nota não pode ser cancelada porque possui documentos no financeiro já baixados.");
                    }

                    Financeiro bllFinanceiro = new Financeiro(db);
                    bllFinanceiro.cancelarDocumentoLote(lista, pDataCanc);
                    bllFinanceiro.Dispose();

                    transacao.Complete();
                }
                catch (ChangeConflictException cce)
                {
                    ret = false;
                    MessageBox.Show("Erro ao cancelar nota.\nErro " + cce.Message, "Aviso [Conflito]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (Exception e)
                {
                    ret = false;
                    MessageBox.Show("Erro ao cancelar nota.\nErro " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            return ret;
        }

        public bool DescancelarNF(int pIDNota, string pMovito)
        {
            bool ret = true;

            using (TransactionScope transacao = new TransactionScope())
            {
                try
                {
                    using (Modelo.dcNotaDataContext _db = new Modelo.dcNotaDataContext())
                    {
                        var nota = (from n in _db.NotaImpressao
                                    where n.ID == pIDNota
                                    select n).Single();

                        nota.CancDt = null;
                        nota.CancMotivo = pMovito;
                        nota.CancUsuario = null;

                        _db.SubmitChanges();
                    }

                    List<Modelo.Documento> lista;

                    lista = (from d in db.Documentos
                             where d.IDNota == pIDNota
                             select d).ToList();

                    Financeiro bllFinanceiro = new Financeiro(db);
                    bllFinanceiro.DescancelarDocumentoLote(lista);
                    bllFinanceiro.Dispose();

                    transacao.Complete();
                }
                catch (ChangeConflictException cce)
                {
                    ret = false;
                    MessageBox.Show("Erro ao cancelar nota.\nErro " + cce.Message, "Aviso [Conflito]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (Exception e)
                {
                    ret = false;
                    MessageBox.Show("Erro ao cancelar nota.\nErro " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            return ret;
        }

        /// <summary>
        /// Método para montar a observação da nota fiscal.
        /// Esse método retorna a quantidade de caracteres que será liberado para obs do usuário
        /// </summary>
        /// <param name="pObjNota">Objeto da Nota</param>
        /// <returns>Retorna a quantidade de cartacter</returns>
        public int MontarObservacao(int pTipoNota, List<Modelo.NotaItem> pListaNotaItem, out string pObservacaoSistema)
        {
            string obs = "";

            //Verificar se a configuração geral possui observação
            string obsCfg = (from cfg in db.Configuracaos
                             select cfg.Observacao).First();

            if (!String.IsNullOrEmpty(obsCfg))
                obs += String.IsNullOrEmpty(obs) ? obsCfg : " " + obsCfg;

            //Verificar se o tipo de movimentação possui observação
            string obsTno = (from tno in db.TipoNotas
                             where tno.ID == pTipoNota
                             select tno.Observacao).First();
            if (!String.IsNullOrEmpty(obsTno))
                obs += String.IsNullOrEmpty(obs) ? obsTno : " " + obsTno;

            //Percorrer todos os produtos da nota encontrando texto lei
            foreach (Modelo.NotaItem ntai in pListaNotaItem)
            {
                if (String.IsNullOrEmpty(ntai.TextoLei))
                    continue;

                obs += String.IsNullOrEmpty(obs) ? ntai.TextoLei : " " + ntai.TextoLei; ;
            }

            obs += String.IsNullOrEmpty(obs) ? MontaObservacaoCredito(pListaNotaItem) : " " + MontaObservacaoCredito(pListaNotaItem);

            //Calcula a quantidade de caracter liberado para a observação do usuário
            pObservacaoSistema = obs.TrimEnd();
            return (500 - obs.Length);
        }

        private string MontaObservacaoCredito(List<Modelo.NotaItem> pListaNotaItem)
        {
            if (pListaNotaItem.Count > 0)
            {
                bool ehSimples = pListaNotaItem[0].Nota.Filial.SimplesNacional == 0;

                if (ehSimples)
                {
                    decimal valorCredito = 0;
                    decimal percCredito = 0;

                    foreach (Modelo.NotaItem item in pListaNotaItem)
                    {
                        if (item.TAG_CST == "101" || item.TAG_CST == "201" || item.TAG_CST == "900")
                        {
                            valorCredito += item.vCredICMSSN_N30.HasValue == true ? item.vCredICMSSN_N30.Value : 0;
                            percCredito = item.pCredSN_N29.HasValue == true ? item.pCredSN_N29.Value : 0;
                        }
                    }

                    string textoLeiCredito = RecuperarTextoLeiCredito();
                    int indiceVCred = textoLeiCredito.IndexOf("*vCred");
                    int indicePCred = textoLeiCredito.IndexOf("*pCred");

                    if ((indicePCred > 0 && percCredito > 0) && (indiceVCred > 0 && valorCredito > 0))
                    {
                        textoLeiCredito = textoLeiCredito.Replace("*vCred", String.Format("{0:C}", valorCredito));
                        textoLeiCredito = textoLeiCredito.Replace("*pCred", String.Format("{0:#0.00%}", percCredito / 100));

                        return textoLeiCredito;
                    }
                }
            }
            return "";


        }

        private static string _textoLeiCredito = null;

        private string RecuperarTextoLeiCredito()
        {
            if (_textoLeiCredito == null)
            {
                XDocument xmlConfiguracao = XDocument.Load("ConfiguracaoCwork.xml");

                if (xmlConfiguracao.Root.Elements().Where(i => i.Name.LocalName == "TextoLeiCredito").Count() == 1)
                {
                    _textoLeiCredito = (from nohTextoLei in xmlConfiguracao.Root.Elements() where nohTextoLei.Name.LocalName == "TextoLeiCredito" select nohTextoLei.Value).Single();

                }
                else
                {
                    throw new Exception("Texto Lei de Crédito não está presente na configuração do sistema.");

                }
                return _textoLeiCredito;
            }
            else
                return _textoLeiCredito;

        }

        /// <summary>
        /// Método responsável em juntar observações
        /// </summary>
        /// <param name="pObservacao">Observação</param>
        /// <param name="pTexto">Texto para ser colocado na observação</param>
        private string JuntarObservacao(string pObservacao, string pTexto)
        {
            if (!pObservacao.Contains(pTexto))
                pObservacao = pObservacao.TrimEnd() + " " + pTexto.TrimEnd();

            if (pObservacao.Length > 500)
                pObservacao = pObservacao.Substring(0, 500);

            return pObservacao;
        }

        public int QtdNotaDemonstracao()
        {
            int qtd = db.Notas.Count();

            return qtd;
        }
    }
}