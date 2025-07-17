using System;
using System.Linq;
using System.Collections.Generic;
using MO = Modelo.Objeto;

namespace BLL
{
    public class NotaItem : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;

        //public NotaItem()
        //{
        //    db = new Modelo.DataClassesDataContext();
        //    //db.ObjectTrackingEnabled = false;
        //    //db.DeferredLoadingEnabled = false;
        //}

        public NotaItem(Modelo.DataClassesDataContext pdb)
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
            lista = null;
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
            lista = null;
            return lista;
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
            lista = null;
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
            lista = null;
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
            return (from u in db.NotaItems where u.ID == pID select u).Single();
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
                return (from u in db.NotaItems where u.Sequencia.ToString() == pValor select u).Single().ID;
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
            return 0;
        }

        /// <summary>
        /// Método para retornar a maior sequencia de uma determinada lista
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXSequencia(Modelo.Nota pNota)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from c in pNota.NotaItems
                                  where c.Sequencia == pNota.NotaItems.Max(b => b.Sequencia)
                                  select c).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// Método para calcular o ICMS do Produto 
        /// </summary>
        /// <param name="pNotaItem">Nota Item</param>
        /// <param name="pNota">Objeto Nota</param>
        //public void calculaICMS(MO.NotaItem pNotaItem, Modelo.Nota pNota)
        //{
        //    if (pNotaItem.IDProduto == 0)
        //    {
        //        return;
        //    }
        //    decimal _AliqSimplesSubst = 0;
        //    decimal _Aliq = 0;
        //    decimal _AliqNormal = 0;
        //    string _TextoLei = "";
        //    decimal _Reducao = 0;
        //    int _TAG_CST = 0;
        //    int _modBC_N13 = 0;
        //    int _modBCST_N18 = 0;
        //    int _CST_Pis = 0;
        //    decimal _pPIS_Q08 = 0;
        //    int _CST_Cofins = 0;
        //    decimal _pCOFINS_S08 = 0;
        //    bool _bIcmsIsento = false;
        //    decimal _SubstLucro = 0;
        //    decimal _AliqSubstTributaria = 0;
        //    string ufOrigem = "";
        //    string ufDestino = "";

        //    Modelo.TabelaCFOP objCFOP = new Modelo.TabelaCFOP();

        //    byte simplesnascional = (byte)pNota.Filial.SimplesNacional;
        //    _AliqSimplesSubst = (decimal)pNota.Filial.AliqSimplesSubst;

        //    if (pNota.PessoaUF != pNota.Filial.Cidade.UF.Sigla)
        //    {
        //        if (pNota.bPessoaContribuinte == 1)
        //        {
        //            objCFOP = pNota.TipoNota.Operacao.TabelaCfopFEstC;
        //            pNotaItem.AliqCupom = pNotaItem.Produto.AliqCupomContrib;
        //        }
        //        else
        //        {
        //            objCFOP = pNota.TipoNota.Operacao.TabelaCfopFEstNC;
        //            pNotaItem.AliqCupom = pNotaItem.Produto.AliqCupomNContrib;
        //        }

        //        ufOrigem = (pNota.Ent_Sai == Modelo.InOutType.Entrada ? pNota.PessoaUF : pNota.Filial.Cidade.UF.Sigla);
        //        ufDestino = (pNota.Ent_Sai == Modelo.InOutType.Entrada ? pNota.Filial.Cidade.UF.Sigla : pNota.PessoaUF);

        //        int temregra = (int)(from r in db.Produto_Icms
        //                             where r.UFOrigem.Sigla == ufOrigem &&
        //                             r.UFDestino.Sigla == ufDestino
        //                             select r).Count();
        //        if (temregra == 0)
        //        {
        //            Modelo.ICMS_Interestadual obj;

        //            var icmsInterestadual = (from r in db.ICMS_Interestaduals
        //                                     where r.UFOrigem.Sigla == ufOrigem &&
        //                                     r.UFDestino.Sigla == ufDestino
        //                                     select r);

        //            if (icmsInterestadual.Count() > 0)
        //                obj = icmsInterestadual.First();
        //            else
        //                throw new Exception(String.Format("ICMS não está configurado para {0} -> {1}.", ufOrigem, ufDestino));

        //            pNotaItem.SitTrib = pNotaItem.Produto.OrigemProd.ToString() + NFe.Conv_TAG_CST((int)obj.Tributacao);

        //            if (pNota.bPessoaContribuinte == 1)
        //            {
        //                _Aliq = obj.AliqContrib.Value;
        //                _AliqNormal = obj.AliqContribNormal.Value;
        //                _TextoLei = obj.TextoLeiContrib;
        //                _Reducao = obj.ReducaoContrib.Value;
        //            }
        //            else
        //            {
        //                _Aliq = obj.AliqNContrib.Value;
        //                _AliqNormal = obj.AliqNContribNormal.Value;
        //                _TextoLei = obj.TextoLeiNContrib;
        //                _Reducao = obj.ReducaoNContrib.Value;
        //            }

        //            _TAG_CST = obj.Tributacao.Value;
        //            _modBC_N13 = obj.modBCICMS.Value;
        //            _modBCST_N18 = obj.modBCST.Value;

        //            _SubstLucro = obj.LucroST.Value;
        //            _AliqSubstTributaria = obj.AliqSubstTributaria.Value;
        //        }
        //        else
        //        {
        //            Modelo.Produto_Icm obj = (from r in db.Produto_Icms
        //                                      where r.UFOrigem.Sigla == (pNota.Ent_Sai == Modelo.InOutType.Entrada ? pNota.PessoaUF : pNota.Filial.Cidade.UF.Sigla) &&
        //                                      r.UFDestino.Sigla == (pNota.Ent_Sai == Modelo.InOutType.Entrada ? pNota.Filial.Cidade.UF.Sigla : pNota.PessoaUF)
        //                                      select r).Single();

        //            pNotaItem.SitTrib = pNotaItem.Produto.OrigemProd.ToString() + NFe.Conv_TAG_CST((int)obj.Tributacao);

        //            if (pNota.bPessoaContribuinte == 1)
        //            {
        //                _Aliq = obj.AliqContrib.Value;
        //                _AliqNormal = obj.AliqContribNormal.Value;
        //                _TextoLei = obj.TextoLeiContrib;
        //                _Reducao = obj.ReducaoContrib.Value;
        //            }
        //            else
        //            {
        //                _Aliq = obj.AliqNContrib.Value;
        //                _AliqNormal = obj.AliqNContribNormal.Value;
        //                _TextoLei = obj.TextoLeiNContrib;
        //                _Reducao = obj.ReducaoNContrib.Value;
        //            }

        //            _TAG_CST = obj.Tributacao.Value;
        //            _modBC_N13 = obj.modBCICMS.Value;
        //            _modBCST_N18 = obj.modBCST.Value;

        //            _SubstLucro = obj.LucroST.Value;
        //            _AliqSubstTributaria = obj.AliqSubstTributaria.Value;
        //        }
        //    }
        //    else
        //    {
        //        pNotaItem.SitTrib = pNotaItem.Produto.OrigemProd.ToString() + NFe.Conv_TAG_CST((int)pNotaItem.Produto.Tributacao);
        //        objCFOP = pNota.TipoNota.Operacao.TabelaCfopDEst;

        //        if (pNota.bPessoaContribuinte == 1)
        //        {
        //            _Aliq = pNotaItem.Produto.AliqContrib.Value;
        //            _AliqNormal = pNotaItem.Produto.AliqContribNormal.Value;
        //            _TextoLei = pNotaItem.Produto.TextoLeiContrib;
        //            _Reducao = pNotaItem.Produto.ReducaoContrib.Value;
        //            pNotaItem.AliqCupom = pNotaItem.Produto.AliqCupomContrib;
        //        }
        //        else
        //        {
        //            _Aliq = pNotaItem.Produto.AliqNContrib.Value;
        //            _AliqNormal = pNotaItem.Produto.AliqNContribNormal.Value;
        //            _TextoLei = pNotaItem.Produto.TextoLeiNContrib;
        //            _Reducao = pNotaItem.Produto.ReducaoNContrib.Value;
        //            pNotaItem.AliqCupom = pNotaItem.Produto.AliqCupomNContrib;
        //        }

        //        _TAG_CST = pNotaItem.Produto.Tributacao.Value;
        //        _modBC_N13 = pNotaItem.Produto.modBCICMS.Value;
        //        _modBCST_N18 = pNotaItem.Produto.modBCST.Value;

        //        _SubstLucro = pNotaItem.Produto.LucroST.Value;
        //        _AliqSubstTributaria = pNotaItem.Produto.AliqSubstTributaria.Value;
        //    }

        //    _CST_Pis = (int)pNotaItem.Produto.CST_Pis;
        //    _pPIS_Q08 = (decimal)pNotaItem.Produto.pPIS_Q08;
        //    _CST_Cofins = (int)pNotaItem.Produto.CST_Cofins;
        //    _pCOFINS_S08 = (decimal)pNotaItem.Produto.pCOFINS_S08;

        //    if (pNotaItem.Produto.IDCFOP != null && pNotaItem.Produto.IDCFOP != 0)
        //        pNotaItem.IDCFOP = pNotaItem.Produto.IDCFOP;
        //    else
        //        pNotaItem.IDCFOP = objCFOP.ID;

        //    //Verifica se o ICMS é isento ou não
        //    if (objCFOP.bCalcICMS == false)
        //    {
        //        _bIcmsIsento = true;
        //    }
        //    else
        //    {
        //        if ((_TAG_CST >= 3 && _TAG_CST <= 6) || (_TAG_CST == 8) || (_TAG_CST == 10))
        //        {
        //            _bIcmsIsento = true;
        //        }
        //        else
        //        {
        //            _bIcmsIsento = false;
        //        }
        //    }

        //    if (pNotaItem.RAT_Acrescimo == null)
        //    {
        //        pNotaItem.RAT_Acrescimo = 0;
        //    }
        //    if (pNotaItem.RAT_Desconto == null)
        //    {
        //        pNotaItem.RAT_Desconto = 0;
        //    }

        //    pNotaItem.TAG_CST = NFe.Conv_TAG_CST(_TAG_CST);
        //    pNotaItem.CST_Ipi = BLL.NFe.Conv_CST_IPI((int)pNotaItem.Produto.CST_IPI);
        //    pNotaItem.CST_Cofins = NFe.Conv_CST_Pis(_CST_Cofins);
        //    pNotaItem.CST_Pis = NFe.Conv_CST_Pis(_CST_Pis);
        //    pNotaItem.TextoLei = _TextoLei;

        //    if (!objCFOP.bDevolucao.Value)
        //    {
        //        pNotaItem.AliqICMS = _Aliq;
        //        pNotaItem.AliqICMSNormal = _AliqNormal;
        //        pNotaItem.TextoLei = _TextoLei;

        //        //Começa os calculos
        //        pNotaItem.BaseICMS = 0;
        //        pNotaItem.ValorICMS = 0;
        //        pNotaItem.ValorIsentoICMS = 0;
        //        pNotaItem.ValorOutroICMS = 0;
        //        pNotaItem.ValorRetidoICMS = 0;
        //        pNotaItem.BaseICMSSubst = 0;

        //        pNotaItem.modBC_N13 = _modBC_N13;
        //        pNotaItem.modBCST_N18 = _modBCST_N18;

        //        pNotaItem.ValorICMSReducao = _Reducao;
        //        pNotaItem.pRedBC_N14 = pNotaItem.ValorICMSReducao;

        //        pNotaItem.BaseICMS = pNotaItem.Total + pNotaItem.RAT_Acrescimo - pNotaItem.RAT_Desconto;

        //        if (simplesnascional == 1)
        //        {
        //            pNotaItem.ValorICMS = pNotaItem.BaseICMS * _AliqSimplesSubst / 100;

        //            calculaSubst(pNotaItem, pNota, _TAG_CST, _SubstLucro, _AliqSubstTributaria);
        //            pNotaItem.ValorOutroICMS = pNotaItem.BaseICMS;
        //            pNotaItem.BaseICMS = 0;
        //            pNotaItem.ValorICMS = 0;
        //            pNotaItem.AliqICMS = 0;
        //            pNotaItem.AliqICMSNormal = 0;
        //        }
        //        else
        //        {
        //            if (_bIcmsIsento == true)
        //            {
        //                if (_TAG_CST >= 3 || _TAG_CST == 8)
        //                {
        //                    pNotaItem.ValorICMS = pNotaItem.BaseICMS * pNotaItem.AliqICMS / 100;

        //                    calculaSubst(pNotaItem, pNota, _TAG_CST, _SubstLucro, _AliqSubstTributaria);
        //                }

        //                pNotaItem.ValorIsentoICMS = pNotaItem.BaseICMS;
        //                pNotaItem.ValorOutroICMS = 0;
        //                pNotaItem.BaseICMS = 0;
        //                pNotaItem.ValorICMS = 0;
        //                pNotaItem.AliqICMS = 0;
        //                pNotaItem.AliqICMSNormal = 0;
        //            }
        //            else
        //            {
        //                if (pNotaItem.ValorICMSReducao > 0)
        //                {
        //                    if (pNotaItem.ValorICMSReducao > 100)
        //                    {
        //                        pNotaItem.ValorICMSReducao = 100;
        //                    }

        //                    pNotaItem.ValorOutroICMS = pNotaItem.BaseICMS;
        //                    pNotaItem.BaseICMS = pNotaItem.BaseICMS * (1 - (pNotaItem.ValorICMSReducao / 100));
        //                    pNotaItem.ValorOutroICMS = pNotaItem.ValorOutroICMS - pNotaItem.BaseICMS;
        //                }
        //                else
        //                {
        //                    pNotaItem.ValorOutroICMS = 0;
        //                }

        //                pNotaItem.ValorICMS = pNotaItem.BaseICMS * pNotaItem.AliqICMS / 100;
        //                pNotaItem.ValorICMSReducaoDif = (pNotaItem.BaseICMS * pNotaItem.AliqICMSNormal / 100) - pNotaItem.ValorICMS;

        //                if (pNotaItem.TextoLei.IndexOf("*valor") != -1)
        //                {
        //                    pNotaItem.TextoLei = pNotaItem.TextoLei.Replace("*valor", String.Format("{0:C2}", pNotaItem.ValorICMSReducaoDif));
        //                }

        //                //Substituição Tributária
        //                calculaSubst(pNotaItem, pNota, _TAG_CST, _SubstLucro, _AliqSubstTributaria);
        //            }
        //        }

        //        //Calcula PIS
        //        if (_CST_Pis >= 6 && _CST_Pis <= 8)
        //        {
        //            pNotaItem.vBC_Q07 = 0;
        //            pNotaItem.pPIS_Q08 = 0;
        //            pNotaItem.vPIS_Q09 = 0;
        //        }
        //        else
        //        {
        //            pNotaItem.vBC_Q07 = pNotaItem.Total + pNotaItem.RAT_Acrescimo - pNotaItem.RAT_Desconto;
        //            pNotaItem.pPIS_Q08 = _pPIS_Q08;
        //            pNotaItem.vPIS_Q09 = pNotaItem.vBC_Q07 * pNotaItem.pPIS_Q08 / 100;
        //        }

        //        //Calcula COFINS
        //        if (_CST_Cofins >= 6 && _CST_Cofins <= 8)
        //        {
        //            pNotaItem.vBC_S07 = 0;
        //            pNotaItem.pCOFINS_S08 = 0;
        //            pNotaItem.vCOFINS_S11 = 0;
        //        }
        //        else
        //        {
        //            pNotaItem.vBC_S07 = pNotaItem.Total + pNotaItem.RAT_Acrescimo - pNotaItem.RAT_Desconto;
        //            pNotaItem.pCOFINS_S08 = _pCOFINS_S08;
        //            pNotaItem.vCOFINS_S11 = pNotaItem.vBC_S07 * pNotaItem.pCOFINS_S08 / 100;
        //        }

        //        //Calcula IPI
        //        if ((pNotaItem.CST_Ipi == "00") || (pNotaItem.CST_Ipi == "49") || (pNotaItem.CST_Ipi == "50") || (pNotaItem.CST_Ipi == "99"))
        //        {
        //            pNotaItem.vBC_O10 = pNotaItem.Total + pNotaItem.RAT_Acrescimo - pNotaItem.RAT_Desconto;
        //            pNotaItem.pIPI_O13 = pNotaItem.Produto.AliquotaIPI;
        //            pNotaItem.vIPI_O14 = pNotaItem.vBC_O10 * pNotaItem.pIPI_O13 / 100;
        //        }
        //        else
        //        {
        //            pNotaItem.vBC_O10 = 0;
        //            pNotaItem.pIPI_O13 = 0;
        //            pNotaItem.vIPI_O14 = 0;
        //        }
        //    }

        //    return;
        //}

        //public decimal CustoMedio(int IDProduto)
        //{
        //    Decimal CustoMedio;
        //    CustoMedio = (from n in db.NotaItems
        //                  where n.IDProduto == IDProduto
        //                  select n).OrderByDescending(y => y.Dt).First().ValorCustoMedio;
        //    return CustoMedio;
        //}

        //private static void calculaSubst(MO.NotaItem pNotaItem, Modelo.Nota pNota, int _TAG_CST, decimal _SubstLucro, decimal _AliqSubstTributaria)
        //{
        //    if ((_TAG_CST == 3 || _TAG_CST == 8) && pNota.bPessoaContribuinte == 1)
        //    {
        //        pNotaItem.BaseICMSSubst = pNotaItem.Total + pNotaItem.RAT_Acrescimo - pNotaItem.RAT_Desconto;
        //        pNotaItem.BaseICMSSubst = pNotaItem.BaseICMSSubst + (pNotaItem.BaseICMSSubst * _SubstLucro / 100);

        //        pNotaItem.ValorRetidoICMS = pNotaItem.BaseICMSSubst * _AliqSubstTributaria / 100;
        //        pNotaItem.ValorRetidoICMS = pNotaItem.ValorRetidoICMS - pNotaItem.ValorICMS;

        //        pNotaItem.pICMSST_N22 = _AliqSubstTributaria;
        //    }
        //    else
        //    {
        //        pNotaItem.pICMSST_N22 = 0;
        //    }
        //}
    }
}