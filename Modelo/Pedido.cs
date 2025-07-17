using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace Modelo
{
    public partial class Pedido : Modelo.IMetodosCwork
    {
        partial void OnCreated()
        {
            itensExcluir = new List<PedidoItem>();
        }

        public List<Modelo.PedidoItem> itensExcluir;

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.Pedidos
                    select new
                    {
                        Codigo = p.Codigo,
                        Data = p.Dt,
                        Digitacao = p.DtDigitacao,
                        Status = p.Status,
                        Nome = p.Pessoa.Nome,
                        Fantasia = p.Pessoa.Fantasia,
                        Total = p.TotalPedido,
                        Vendedor = p.Vendedor.Pessoa.Apelido,
                        ID = p.ID
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
        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.Pedidos
                    where p.Codigo.ToString() == pFiltro ||
                    p.Dt.ToString().Contains(pFiltro) ||
                    p.DtDigitacao.ToString().Contains(pFiltro) ||
                    p.Pessoa.Nome.Contains(pFiltro)
                    select new
                    {
                        Codigo = p.Codigo,
                        Data = p.Dt,
                        Digitacao = p.DtDigitacao,
                        Status = p.Status,
                        Nome = p.Pessoa.Nome,
                        Fantasia = p.Pessoa.Fantasia,
                        Total = p.TotalPedido,
                        Vendedor = p.Vendedor.Pessoa.Apelido,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <param name="tipo"></param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>        
        public IQueryable getListaTipo(DataClassesDataContext db, String pFiltro, InOutType tipo)
        {
            IQueryable lista;
            lista = from p in db.Pedidos
                    where p.Ent_Sai == tipo &&
                    (p.Codigo.ToString() == pFiltro ||
                    p.Dt.ToString().Contains(pFiltro) ||
                    p.DtDigitacao.ToString().Contains(pFiltro) ||
                    p.Pessoa.Nome.Contains(pFiltro))
                    select new
                    {
                        Codigo = p.Codigo,
                        Data = p.Dt,
                        Digitacao = p.DtDigitacao,
                        Status = p.Status,
                        Nome = p.Pessoa.Nome,
                        Fantasia = p.Pessoa.Fantasia,
                        Total = p.TotalPedido,
                        Vendedor = p.Vendedor.Pessoa.Apelido,
                        Tipo = p.TipoPedido.Nome,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IList getListaCombo(DataClassesDataContext db)
        {
            return (from p in db.Pedidos
                    select new
                    {
                        Nome = p.Pessoa.Nome + " | " + p.Codigo,
                        ID = p.ID
                    }).ToList();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.Pedidos
                    where p.Codigo.ToString() == pFiltro ||
                    p.Dt.ToString().Contains(pFiltro) ||
                    p.DtDigitacao.ToString().Contains(pFiltro) ||
                    p.Pessoa.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = p.Pessoa.Nome + " | " + p.Codigo,
                        ID = p.ID
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
        public IQueryable getListaCombo(DataClassesDataContext db, InOutType tipo)
        {
            IQueryable lista;
            lista = from p in db.Pedidos
                    where p.Ent_Sai == tipo
                    select new
                    {
                        Nome = p.Pessoa.Nome + " | " + p.Codigo,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            foreach (var itemPedido in db.Pedidos)
            {
                if (itemPedido.ID == pID)
                    return (itemPedido);
            }
            return (from u in db.Pedidos where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.Pedidos where u.Codigo.ToString() == pValor select u).Single().ID;
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
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.Pedidos
                             where p.Codigo == db.Pedidos.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public Int32 EncontraCodigoDisponivel(DataClassesDataContext db)
        {
            int? maxCodigo = 0;
            try
            {
                maxCodigo = db.f_PedidoCodigoDisponivel();
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo.HasValue ? maxCodigo.Value : 1;
        }

        public int BuscaUltimoNumero(Modelo.DataClassesDataContext pdb, int pFilial, Modelo.TipoPedidoType pTipo)
        {
            Int32? maxNumero = 0;
            try
            {
                maxNumero = (from p in pdb.Pedidos
                             join t in pdb.TipoPedidos on p.IDTipoPedido equals t.ID
                             where t.Tipo == pTipo
                             select p.Numero).Max();
                if (maxNumero.HasValue)
                {
                    return maxNumero.Value + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        /// <summary>
        /// Método para retornar a soma dos valor retidos de ICMS dos itens do pedido
        /// </summary>
        /// <returns>soma dos valores retidos de ICMS</returns>
        public decimal getSubTributaria()
        {
            /*
            decimal soma = 0;
            foreach (Modelo.PedidoItem x in this.PedidoItems)
            {
                soma += (decimal)x.ValorRetidoICMS;
            }
            return soma;
            */
            return 0;
        }

        /// <summary>
        /// Rotina que geras as parcelas do pedido
        /// </summary>
        /// <returns>Retorna true ou false (bool)</returns>
        public bool gerarParcelas(DataClassesDataContext db, List<Modelo.PedidoParcela> pParcelas)
        {
            bool ret;

            foreach (Modelo.PedidoParcela pp in this.PedidoParcelas)
            {
                if (pp.ID != 0)
                {
                    db.PedidoParcelas.DeleteOnSubmit(pp);
                }
            }
            this.PedidoParcelas.Clear();

            decimal valorfixo = 0;
            decimal total = 0;
            decimal valorcalc = 0;

            if (this.TotalPedido == 0)
            {
                return true;
            }

            if (!this.Dt.HasValue)
            {
                throw new Exception("Data base não foi informada!");
            }

            if (this.Condicao == null)
            {
                throw new Exception("Deve ser informado uma condição de pagamento");
            }

            foreach (Modelo.CondicaoParcela c in this.Condicao.CondicaoParcelas)
            {
                if (c.TipoDt != "Dia" && c.TipoDt != "Mês")
                {
                    throw new Exception("Tipo de dados inválido para o campo Tipo da Data na condição.");
                }
                if (this.Condicao.Divide == 0)
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

            if (valorfixo > (decimal)this.TotalPedido)
            {
                throw new Exception("O valor fixado nas parcelas é maior que o valor total. Verifique.");
            }

            total = System.Math.Round((decimal)this.TotalPedido - valorfixo, 2);
            int NumParcela = -1;
            
            foreach (Modelo.CondicaoParcela c in this.Condicao.CondicaoParcelas)
            {
                DateTime vencimento = this.Dt.Value;
                Modelo.PedidoParcela objPedidoParcela = new PedidoParcela();
                objPedidoParcela.Parcela = c.Parcela;
                if (pParcelas != null)
                {
                    NumParcela++;

                    objPedidoParcela.NomeBanco = pParcelas[NumParcela].NomeBanco == null ? "" : pParcelas[NumParcela].NomeBanco;
                    objPedidoParcela.NumAgencia = pParcelas[NumParcela].NumAgencia == null ? "" : pParcelas[NumParcela].NumAgencia;
                    objPedidoParcela.NumBanco = pParcelas[NumParcela].NumBanco == null ? "" : pParcelas[NumParcela].NumBanco;
                    objPedidoParcela.NumCheque = pParcelas[NumParcela].NumCheque == null ? "" : pParcelas[NumParcela].NumCheque;
                    objPedidoParcela.CpfCnpj = pParcelas[NumParcela].CpfCnpj == null ? "" : pParcelas[NumParcela].CpfCnpj;
                    objPedidoParcela.NumContaCorrente = pParcelas[NumParcela].NumContaCorrente == null ? "" : pParcelas[NumParcela].NumContaCorrente;
                    objPedidoParcela.Emitente = pParcelas[NumParcela].Emitente == null ? "" : pParcelas[NumParcela].Emitente;
                    objPedidoParcela.Vencimento = pParcelas[NumParcela].Vencimento == null ? null : (DateTime?)pParcelas[NumParcela].Vencimento;

                    List<Modelo.Feriado> feriados = db.Feriados.ToList();

                    while (this.Condicao.VencimentoFeriado != Modelo.VencimentoFeriado.Nada && (objPedidoParcela.Vencimento.Value.DayOfWeek == DayOfWeek.Saturday || objPedidoParcela.Vencimento.Value.DayOfWeek == DayOfWeek.Sunday || feriados.Where(feriado => feriado.Data.Day == objPedidoParcela.Vencimento.Value.Day && feriado.Data.Month == objPedidoParcela.Vencimento.Value.Month).Count() > 0))
                        switch (objPedidoParcela.Pedido.Condicao.VencimentoFeriado)
                        {
                            case Modelo.VencimentoFeriado.Adiantar:
                                objPedidoParcela.Vencimento = objPedidoParcela.Vencimento.Value.AddDays(-1);
                                break;
                            case Modelo.VencimentoFeriado.Adiar:
                                objPedidoParcela.Vencimento = objPedidoParcela.Vencimento.Value.AddDays(1);
                                break;
                        }
                }
                else
                {
                    //if (c.TipoDt == "Dia")
                    //{
                    //    objPedidoParcela.Vencimento = Modelo.MetodosEstaticos.geraVencimento((DateTime)this.Dt, TipoDataType.Dia, (int)c.DiaMes);
                    //}
                    //else
                    //{
                    //    objPedidoParcela.Vencimento = Modelo.MetodosEstaticos.geraVencimento((DateTime)this.Dt, TipoDataType.Mês, (int)c.DiaMes);
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

                    objPedidoParcela.Vencimento = vencimento;
                }

                if (this.Condicao.Divide == 1)
                {
                    if (this.Condicao.QtParcela != 0)
                    {
                        objPedidoParcela.Valor = System.Math.Round((total / this.Condicao.QtParcela), 2);
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
                        objPedidoParcela.Valor = System.Math.Round((((decimal)total * (decimal)c.VlrPerc) / 100), 2);
                    }
                    else
                    {
                        objPedidoParcela.Valor = c.VlrPerc;
                    }
                }
                valorcalc = System.Math.Round((valorcalc + (decimal)objPedidoParcela.Valor), 2);

                objPedidoParcela.TipoDocumento = c.TipoDocumento;

                this.PedidoParcelas.Add(objPedidoParcela);
            }

            if (valorcalc > (decimal)this.TotalPedido)
            {
                Modelo.PedidoParcela objPedidoParcela = new PedidoParcela();
                objPedidoParcela = (from t in this.PedidoParcelas
                                    where t.Parcela == 1
                                    select t).Single();

                objPedidoParcela.Valor = objPedidoParcela.Valor - (valorcalc - (decimal)this.TotalPedido);
            }
            else
            {
                if (valorcalc < (decimal)this.TotalPedido)
                {
                    Modelo.PedidoParcela objPedidoParcela = new PedidoParcela();
                    objPedidoParcela = (from t in this.PedidoParcelas
                                        where t.Parcela == 1
                                        select t).Single();

                    objPedidoParcela.Valor = objPedidoParcela.Valor + ((decimal)this.TotalPedido - valorcalc);
                }
            }

            ret = true;

            return ret;
        }

        /// <summary>
        /// Rotina que atualiza as parcelas - Caso não tenha valor alterado se baseia na condição
        /// agora caso tenha algum valor alterado a rotina sempre irá dividir o valor
        /// </summary>
        /// <returns>Retorna true ou false (bool)</returns>
        public bool atualizaParcelas(DataClassesDataContext db, List<Modelo.PedidoParcela> pParcelas)
        {
            bool ret;
            decimal valoralterado = 0;
            decimal valoraretear = 0;
            decimal valorcalc = 0;
            decimal valorfixo = 0;
            decimal total = 0;

            int qtdratear = 0;
            int id = 0;

            if (this.PedidoParcelas.Count == 0)
            {
                return true;
            }

            if (this.TotalPedido <= 0)
            {
                foreach (Modelo.PedidoParcela pp in this.PedidoParcelas)
                {
                    if (pp.ID != 0)
                    {
                        db.PedidoParcelas.DeleteOnSubmit(pp);
                    }
                }
                this.PedidoParcelas.Clear();

                return true;
            }

            foreach (Modelo.PedidoParcela p in this.PedidoParcelas)
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

            if (qtdratear == 0)
            {
                return true;
            }

            if (valoralterado == 0)
            {
                valoraretear = (decimal)this.TotalPedido - valoralterado;

                foreach (Modelo.CondicaoParcela c in this.Condicao.CondicaoParcelas)
                {
                    if (c.VlrPerc != null && c.TipoVlr == "Valor")
                    {
                        valorfixo = valorfixo + (decimal)c.VlrPerc;
                    }
                }

                if (valorfixo > (decimal)this.TotalPedido)
                {
                    throw new Exception("O valor fixado nas parcelas é maior que o valor total. Verifique.");
                }

                total = (decimal)this.TotalPedido - valorfixo;

                Modelo.CondicaoParcela objCP;
                int NumParcela = -1;
                foreach (Modelo.PedidoParcela p in this.PedidoParcelas)
                {
                    objCP = (from c in this.Condicao.CondicaoParcelas
                             where c.Parcela == (int)p.Parcela
                             select c).Single();

                    if (pParcelas != null)
                    {
                        NumParcela++;

                        p.NomeBanco = pParcelas[NumParcela].NomeBanco == null ? "" : pParcelas[NumParcela].NomeBanco;
                        p.NumAgencia = pParcelas[NumParcela].NumAgencia == null ? "" : pParcelas[NumParcela].NumAgencia;
                        p.NumBanco = pParcelas[NumParcela].NumBanco == null ? "" : pParcelas[NumParcela].NumBanco;
                        p.NumCheque = pParcelas[NumParcela].NumCheque == null ? "" : pParcelas[NumParcela].NumCheque;
                        p.CpfCnpj = pParcelas[NumParcela].CpfCnpj == null ? "" : pParcelas[NumParcela].CpfCnpj;
                        p.NumContaCorrente = pParcelas[NumParcela].NumContaCorrente == null ? "" : pParcelas[NumParcela].NumContaCorrente;
                        p.Emitente = pParcelas[NumParcela].Emitente == null ? "" : pParcelas[NumParcela].Emitente;
                        p.Vencimento = pParcelas[NumParcela].Vencimento == null ? null : (DateTime?)pParcelas[NumParcela].Vencimento;
                    }
                    if (this.Condicao.Divide == 1)
                    {
                        if (this.Condicao.QtParcela != 0)
                        {
                            p.Valor = System.Math.Round((total / this.Condicao.QtParcela), 2);
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
                            p.Valor = System.Math.Round((((decimal)total * (decimal)objCP.VlrPerc) / 100), 2);
                        }
                        else
                        {
                            p.Valor = objCP.VlrPerc;
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
                valoraretear = (decimal)this.TotalPedido - valoralterado;
                foreach (Modelo.PedidoParcela p in this.PedidoParcelas)
                {
                    if (p.bAlterado == 1)
                    {
                        continue;
                    }

                    p.Valor = System.Math.Round((valoraretear / qtdratear), 2);
                    valorcalc = valorcalc + (decimal)p.Valor;

                    if (id == 0)
                    {
                        id = p.ID;
                    }
                }
            }

            if (valorcalc > valoraretear)
            {
                Modelo.PedidoParcela objPedidoParcela = new PedidoParcela();
                if (id != 0)
                {
                    objPedidoParcela = (from t in this.PedidoParcelas
                                        where t.ID == id
                                        select t).Single();
                }
                else
                {
                    objPedidoParcela = (from t in this.PedidoParcelas
                                        where t.ID == id
                                        && (t.bAlterado == 0 || t.bAlterado == null)
                                        select t).First();
                }

                objPedidoParcela.Valor = objPedidoParcela.Valor - (valorcalc - valoraretear);
            }
            else
            {
                if (valorcalc < valoraretear)
                {
                    Modelo.PedidoParcela objPedidoParcela = new PedidoParcela();
                    if (id != 0)
                    {
                        objPedidoParcela = (from t in this.PedidoParcelas
                                            where t.ID == id
                                            select t).Single();
                    }
                    else
                    {
                        objPedidoParcela = (from t in this.PedidoParcelas
                                            where t.ID == id
                                            && (t.bAlterado == 0 || t.bAlterado == null)
                                            select t).First();
                    }

                    objPedidoParcela.Valor = objPedidoParcela.Valor + (valoraretear - valorcalc);
                }
            }

            ret = true;

            return ret;
        }

        public List<Modelo.Objeto.pxyAtualizaCotacaoLST> GetPedidoLista(DataClassesDataContext db, int ID)
        {
            List<Modelo.Objeto.pxyAtualizaCotacaoLST> lista;
            lista = (from c in db.PedidoItems
                     where c.IDPedido == ID && c.CotacaoProdutoPedidoItems.Count == 0
                     select new Modelo.Objeto.pxyAtualizaCotacaoLST
                     {
                         ID = c.ID,
                         Sequencia = c.Sequencia,
                         Produto = c.ProdutoNome,
                         IDProduto = c.IDProduto,
                         Quantidade = Convert.ToInt32(c.Quantidade),
                         Selec = false,
                         DescReduzida = c.ProdutoDescReduzida
                     }).ToList<Modelo.Objeto.pxyAtualizaCotacaoLST>();

            return lista;
        }

        public int QtdPedidosDemonstracao(DataClassesDataContext db)
        {
            int qtd = db.Pedidos.Count();

            return qtd;
        }

        //public IQueryable getLista(DataClassesDataContext db) 
        //{
        //    IQueryable lista;
        //    lista = from p in db.Pedidos
        //            select
        //            Nome = p.

        //}
        public Pedido Clone()
        {
            Pedido clone = new Pedido();

            foreach (System.Reflection.PropertyInfo property in this.GetType().GetProperties())
            {

                property.SetValue(clone, property.GetValue(this, null), null);
            }


            return clone;
        }
    }
}