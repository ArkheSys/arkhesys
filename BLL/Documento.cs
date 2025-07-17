using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixReporter;
using Modelo;
using System.IO;
using Modelo.ModeloProxy;

namespace BLL
{
    public class Documento : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;

        public Documento()
        {
            db = new Modelo.DataClassesDataContext();
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
        }

        public Documento(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public void Dispose()
        {
            db.Dispose();
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

            lista = from u in db.Documentos
                    select new
                    {
                        Código = u.Codigo,
                        Filial = u.Filial.Codigo,
                        Vencimento = u.DtVencimento,
                        Previsão = u.DtPrevisao,
                        Doc = u.NumDocumento,
                        Requisição = u.NumRequisicao,
                        Cliente = u.Pessoa.Nome,
                        Valor = String.Format("{0:C2}", u.Valor),
                        Saldo = String.Format("{0:C2}", db.f_SaldoDocumento(u.ID)),
                        Situação = db.f_SituacaoDocumento(u.ID),
                        ID = u.ID
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

            lista = from u in db.Documentos
                    where u.Codigo.ToString() == pFiltro ||
                    u.Pessoa.Nome.Contains(pFiltro)
                    select new
                    {
                        Código = u.Codigo,
                        Filial = u.Filial.Codigo,
                        Vencimento = u.DtVencimento,
                        Previsão = u.DtPrevisao,
                        Doc = u.NumDocumento,
                        Requisição = u.NumRequisicao,
                        Cliente = u.Pessoa.Nome,
                        Valor = String.Format("{0:C2}", u.Valor),
                        Saldo = String.Format("{0:C2}", db.f_SaldoDocumento(u.ID)),
                        Situação = db.f_SituacaoDocumento(u.ID),
                        ID = u.ID
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
        public IQueryable getListaTipo(Modelo.DataClassesDataContext db, Modelo.TipoDocumentoType tipo)
        {
            IQueryable lista;

            lista = from u in db.Documentos
                    where u.Tipo == tipo
                    && u.DtVencimento >= System.DateTime.Today.AddYears(-5) &&
                    u.Saldo > 0
                    select new
                    {
                        Código = u.Codigo,
                        Filial = u.Filial.Codigo,
                        Vencimento = u.DtVencimento,
                        Previsão = u.DtPrevisao,
                        Doc = u.NumDocumento,
                        Requisição = u.NumRequisicao,
                        Cliente = u.Pessoa.Nome,
                        Valor = u.Valor,//String.Format("{0:C2}", u.Valor),
                        Saldo = u.Saldo,//String.Format("{0:C2}", u.Saldo),//String.Format("{0:C2}", db.f_SaldoDocumento(u.ID)),
                        Situação = u.Situacao,//db.f_SituacaoDocumento(u.ID),
                        DtUltimaBaixa = u.DtUltimaBaixa,
                        NomeFantasia = u.Pessoa.Fantasia,
                        ID = u.ID,
                        TipoDocumento = u.TipoDocumento.DescReduzida,
                        Emissao = u.Dt,
                        Portador = u.Portador.Nome
                    };
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

            lista = from u in db.Documentos
                    select new
                    {
                        Nome = u.Codigo + " | " + u.Pessoa.Nome,
                        ID = u.ID
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

            lista = from u in db.Documentos
                    where u.Codigo.ToString() == pFiltro ||
                    u.Pessoa.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = u.Codigo + " | " + u.Pessoa.Nome,
                        ID = u.ID
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
            return (from u in db.Documentos where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(string pValor)
        {
            return (from p in db.Documentos where p.Codigo.ToString() == pValor select p).Single().ID;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo()
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from n in db.Documentos
                             where n.Codigo == db.Documentos.Max(b => b.Codigo)
                             select n).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public bool ValidaExclusao(Modelo.DataClassesDataContext pdb, string pNumDoc)
        {

            IQueryable<Modelo.Documento> listaDoc = (from d in pdb.Documentos
                                                     where d.NumDocumento == pNumDoc
                                                     select d);

            foreach (Modelo.Documento d in listaDoc)
            {
                decimal saldo = pdb.f_SaldoDocumento(d.ID).Value;
                if (d.Valor != saldo)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Método responsável para calcular os valores corrigidos de um determinado cliente em um determinado período
        /// </summary>
        /// <param name="pIdCliente">Id do Cliente</param>
        /// <param name="pIdEmpresa">Id da Empresa</param>
        /// <param name="pIdAcrescimo">Id da Tabela de Acrescimo - OBS: Informando esse parâmetro o método irá desconsiderar a tabela de acrescimo do documento</param>
        /// <param name="pDataBase">Data base para cálculo do Juro e Multa</param>
        /// <param name="pDataI">Vencimento Inicial</param>
        /// <param name="pDataF">Vencimento Final</param>
        /// <returns>Retorna uma lista de documentos com os valores corrigidos</returns>
        public List<Modelo.ExtratoCliente> getExtratoCliente(int pIdCliente, int pIdEmpresa, int pIdAcrescimo, DateTime pDataBase, DateTime? pDataI, DateTime? pDataF)
        {
            List<Modelo.ExtratoCliente> listaExtratoCliente = new List<Modelo.ExtratoCliente>();
            Decimal valor = 0;

            //Cria o objeto de acrescimo
            Modelo.Acrescimo objAcrescimo = new Modelo.Acrescimo();
            if (pIdAcrescimo == 0)
            {
                objAcrescimo = null;
            }
            else
            {
                objAcrescimo = (from a in db.Acrescimos
                                where a.ID == pIdAcrescimo
                                select a).Single();
            }

            if (pDataI == null && pDataF == null)
            {
                listaExtratoCliente = GetExtratoClienteSemVencimento(pIdCliente, pIdEmpresa, listaExtratoCliente);
            }
            else
            {
                listaExtratoCliente = GetExtratoClienteComVencimento(pIdCliente, pIdEmpresa, pDataI, pDataF, listaExtratoCliente);
            }

            //Processa a lista de extrato calculando os juros
            foreach (Modelo.ExtratoCliente objExtratoCliente in listaExtratoCliente)
            {
                valor = objExtratoCliente.Saldo;

                //Calcula os dias de atrazo
                objExtratoCliente.DiasAtraso = (pDataBase - objExtratoCliente.Vencimento).Days;

                //Caso não tenha acrescimo geral, considerar o que está no documento
                if (pIdAcrescimo == 0)
                {
                    objAcrescimo = objExtratoCliente.objAcrecimo;
                }

                //Calcula Multa
                if (objExtratoCliente.DiasAtraso > objAcrescimo.DiasMulta && objAcrescimo.PercMulta > 0)
                {
                    objExtratoCliente.Multa = (Decimal)(objExtratoCliente.Saldo * objAcrescimo.PercMulta / 100);
                }

                //Calcula o Juros
                if (objExtratoCliente.DiasAtraso > objAcrescimo.DiasJuro && objAcrescimo.PercJuro > 0)
                {
                    //Verifica se é para calcular os juros sobre a multa
                    if (objAcrescimo.JurosMulta == 1)
                    {
                        valor = valor + objExtratoCliente.Multa;
                    }

                    objExtratoCliente.Juro = (Decimal)(valor * (objAcrescimo.PercJuro / 30) / 100) * objExtratoCliente.DiasAtraso;
                }

                //Calcula o valor corrigido
                objExtratoCliente.ValorCorrigido = objExtratoCliente.Saldo + objExtratoCliente.Multa + objExtratoCliente.Juro;
            }

            return listaExtratoCliente;
        }

        private List<Modelo.ExtratoCliente> GetExtratoClienteComVencimento(int pIdCliente, int pIdEmpresa, DateTime? pDataI, DateTime? pDataF, List<Modelo.ExtratoCliente> listaExtratoCliente)
        {
            //Cria a lista de Extrato do cliente com os valores de juros zerados
            listaExtratoCliente = (from d in db.Documentos
                                   where d.IDPessoa == pIdCliente
                                   && d.IDFilial == pIdEmpresa
                                   && d.DtVencimento >= pDataI
                                   && d.DtVencimento <= pDataF
                                   && d.Saldo > 0
                                   select new Modelo.ExtratoCliente
                                   {
                                       IdDocumento = d.ID,
                                       Codigo = d.Codigo,
                                       Filial = (int)d.IDFilial,
                                       Data = (DateTime)d.Dt,
                                       Vencimento = (DateTime)d.DtVencimento,
                                       DiasAtraso = 0,//System.DateTime.Compare(pDataBase, (DateTime)d.DtVencimento),
                                       Documento = d.NumDocumento,
                                       ValorDoc = (Decimal)d.Valor,
                                       Saldo = (Decimal)d.Saldo,
                                       Juro = 0,
                                       Multa = 0,
                                       ValorCorrigido = 0,
                                       objAcrecimo = d.Acrescimo,
                                       Tipo = d.Tipo,
                                       Parcela = d.Parcela.ToString() + " / " + d.QtParcela.ToString()
                                   }).ToList();
            return listaExtratoCliente;
        }

        private List<Modelo.ExtratoCliente> GetExtratoClienteSemVencimento(int pIdCliente, int pIdEmpresa, List<Modelo.ExtratoCliente> listaExtratoCliente)
        {
            //Cria a lista de Extrato do cliente com os valores de juros zerados
            listaExtratoCliente = (from d in db.Documentos
                                   where d.IDPessoa == pIdCliente
                                   && d.IDFilial == pIdEmpresa
                                   && d.Saldo > 0
                                   select new Modelo.ExtratoCliente
                                   {
                                       IdDocumento = d.ID,
                                       Codigo = d.Codigo,
                                       Filial = (int)d.IDFilial,
                                       Data = (DateTime)d.Dt,
                                       Vencimento = (DateTime)d.DtVencimento,
                                       DiasAtraso = 0,//System.DateTime.Compare(pDataBase, (DateTime)d.DtVencimento),
                                       Documento = d.NumDocumento,
                                       ValorDoc = (Decimal)d.Valor,
                                       Saldo = (Decimal)d.Saldo,
                                       Juro = 0,
                                       Multa = 0,
                                       ValorCorrigido = 0,
                                       objAcrecimo = d.Acrescimo,
                                       Tipo = d.Tipo,
                                       Parcela = d.Parcela.ToString() + " / " + d.QtParcela.ToString()
                                   }).ToList();
            return listaExtratoCliente;
        }

        public IQueryable getHistoricoCliente(Modelo.DataClassesDataContext db, Modelo.TipoDocumentoType tipo, DateTime pDataI, DateTime pDataF, int pIdCliente)
        {
            IQueryable lista;

            lista = from u in db.Documentos
                    where u.Tipo == tipo &&
                    u.DtVencimento >= pDataI &&
                    u.DtVencimento <= pDataF &&
                    u.IDPessoa == pIdCliente &&
                    u.Situacao != "Canc"
                    select new
                    {
                        Codigo = u.Codigo,
                        Filial = u.Filial.Codigo,
                        Doc = u.NumDocumento,
                        TipoDocumento = u.TipoDocumento.DescReduzida,
                        Data = u.Dt,
                        Vencimento = u.DtVencimento,
                        DtPagamento = u.DtUltimaBaixa,
                        DiasAtraso = u.DtUltimaBaixa == null ? (System.DateTime.Today - u.DtVencimento.Value).Days : (u.DtUltimaBaixa.Value - u.DtVencimento.Value).Days,
                        Valor = u.Valor,//String.Format("{0:C2}", u.Valor),
                        Saldo = u.Saldo,//String.Format("{0:C2}", u.Saldo),//String.Format("{0:C2}", db.f_SaldoDocumento(u.ID)),
                        Situação = u.Situacao,//db.f_SituacaoDocumento(u.ID),
                        NomeFantasia = u.Pessoa.Fantasia,
                        ID = u.ID,
                    };
            return lista;
        }

        public void ImprimeDocumentos(List<Modelo.Documento> documentos, int pNota, Modelo.Impressora objImpressora)
        {
            //ImprimeDocumentosGabarito();
            //return;

            string instrucaocaixa1;
            string instrucaocaixa2;
            string instrucaocaixa5;
            decimal valorjuro = 0;

            StringBuilder endereco = new StringBuilder();
            Modelo.Endereco objEnd = new Modelo.Endereco();

            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;
            int count;

            lPrinter = new Reporter();
            if (objImpressora.Caminho.IndexOf('\\') == -1)
                lPrinter.Output = objImpressora.Caminho;
            else
                lPrinter.Output = @objImpressora.Caminho;

            //lPrinter.Output = @"\\vendas2\epson-esc";

            lPrinter.NomeArquivo = "d" + pNota.ToString() + ".txt";

            lPrinter.StartJob();

            count = 0;

            foreach (Modelo.Documento doc in documentos)
            {
                lPrinter.PrintText(count, 0, lCodes.Reset + lCodes.LinesInch8 + lCodes.CondensedOn);
                lPrinter.PrintText(count, 95, String.Format("{0:dd/MM/yyyy}", doc.DtVencimento));

                lPrinter.PrintText(++count, 1, "");
                lPrinter.PrintText(++count, 1, "");
                lPrinter.PrintText(++count, 1, "");
                lPrinter.PrintText(++count, 1, "");

                //5
                lPrinter.PrintText(++count, 3, String.Format("{0:dd/MM/yyyy}", doc.Dt));
                lPrinter.PrintText(count, 30, String.Format("{0:000000}", Convert.ToInt32(doc.NumDocumento)));

                lPrinter.PrintText(++count, 1, "");

                //7
                lPrinter.PrintText(++count, 95, String.Format("{0:c2}", doc.Saldo));

                if (doc.Acrescimo.InstrucaoCaixa1.IndexOf("*vlrjuro") == -1)
                    instrucaocaixa1 = doc.Acrescimo.InstrucaoCaixa1;
                else
                {
                    valorjuro = (doc.Saldo.Value * doc.Acrescimo.PercJuro.Value / 100) / 30;
                    instrucaocaixa1 = doc.Acrescimo.InstrucaoCaixa1.Replace("*vlrjuro", String.Format("{0:c2}", valorjuro));
                }
                if (doc.Acrescimo.InstrucaoCaixa2.IndexOf("*protesto") == -1)
                    instrucaocaixa2 = doc.Acrescimo.InstrucaoCaixa2;
                else
                    instrucaocaixa2 = doc.Acrescimo.InstrucaoCaixa2.Replace("*protesto", doc.Acrescimo.DiasProtesto.ToString());

                if (doc.Acrescimo.InstrucaoCaixa5.IndexOf("*parcela") == -1)
                    instrucaocaixa5 = doc.Acrescimo.InstrucaoCaixa5;
                else
                    instrucaocaixa5 = doc.Acrescimo.InstrucaoCaixa5.Replace("*parcela", doc.Parcela.ToString() + "/" + doc.QtParcela.ToString());

                lPrinter.PrintText(++count, 1, "");

                //9
                lPrinter.PrintText(++count, 3, instrucaocaixa1);
                lPrinter.PrintText(++count, 3, instrucaocaixa2);
                lPrinter.PrintText(++count, 3, doc.Acrescimo.InstrucaoCaixa3);
                lPrinter.PrintText(++count, 3, doc.Acrescimo.InstrucaoCaixa4);
                lPrinter.PrintText(++count, 3, instrucaocaixa5);
                lPrinter.PrintText(++count, 3, doc.Acrescimo.InstrucaoCaixa6);
                lPrinter.PrintText(++count, 3, doc.Acrescimo.InstrucaoCaixa7);

                lPrinter.PrintText(++count, 1, "");
                lPrinter.PrintText(++count, 1, "");
                lPrinter.PrintText(++count, 1, "");

                //19
                lPrinter.PrintText(++count, 3, lCodes.CondensedOn);
                lPrinter.PrintText(count, 3, doc.Pessoa.Nome + " (" + doc.Pessoa.Codigo.ToString() + ") " + (doc.Pessoa.CNPJ_CPF == null ? "" : doc.Pessoa.CNPJ_CPF.TrimEnd()) + " " + (doc.Pessoa.Inscricao == null ? "" : doc.Pessoa.Inscricao.TrimEnd()));

                lPrinter.PrintText(++count, 1, "");

                //22
                objEnd = doc.Pessoa.Enderecos.Where(w => w.bPrincipal == 1).Single();

                lPrinter.PrintText(++count, 3, (objEnd.Rua == null ? "" : objEnd.Rua) + " " + (objEnd.Numero == null ? "" : objEnd.Numero.ToString()) + " " + (objEnd.Cidade == null ? "" : objEnd.Cidade.Nome + "-" + objEnd.Cidade.UF.Sigla) + ", F:" + (objEnd.Telefone == null ? "" : objEnd.Telefone.ToString()) + lCodes.CondensedOff);

                count = count + 11;
            }

            lPrinter.PrintText(++count, 0, lCodes.Reset);

            lPrinter.EndJob();

            //lPrinter.PreviewJob();
            lPrinter.PrintJob();
        }

        public void ImprimeDuplicatas(List<Modelo.Documento> documentos)
        {
            foreach (Modelo.Documento doc in documentos)
            {
                ImprimeDuplicata(doc);
            }
        }

        public void ImprimeDuplicata(Modelo.Documento objDocumento)
        {
            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;

            lPrinter = new Reporter();
            //if (objImpressora.Caminho.IndexOf('\\') == -1)
            //    lPrinter.Output = objImpressora.Caminho;
            //else
            //    lPrinter.Output = @objImpressora.Caminho;
            lPrinter.Output = @"\\dev02\EPSONLX300";

            lPrinter.NomeArquivo = "dup" + objDocumento.Codigo + ".txt";

            lPrinter.StartJob();

            int count = 0;

            lPrinter.PrintText(count, 0, lCodes.Reset + lCodes.LinesInch6 + lCodes.SetPageSize(44) + lCodes.CondensedOn);
            //Local
            count = 12;
            lPrinter.PrintText(count, 97, objDocumento.Filial.Cidade.Nome);
            //Data Emissão
            count = 13;
            lPrinter.PrintText(count, 97, DateTime.Now.ToShortDateString());
            //Valor e Código - Fatura
            count = 16;
            lPrinter.PrintText(count, 0, objDocumento.Valor.Value.ToString());
            lPrinter.PrintText(count, 20, objDocumento.Codigo.ToString());
            //Valor e Código - Duplicata
            lPrinter.PrintText(count, 38, objDocumento.Valor.Value.ToString());
            lPrinter.PrintText(count, 63, objDocumento.Codigo.ToString());
            //Vencimento
            lPrinter.PrintText(count, 87, objDocumento.DtVencimento.Value.ToShortDateString());
            //Nome do Sacado
            count = 20;
            lPrinter.PrintText(count, 32, objDocumento.Pessoa.Nome);
            //Endereço
            count = 21;
            Modelo.Endereco end = objDocumento.Pessoa.Enderecos.Where(p => p.bPrincipal == 1).First();
            lPrinter.PrintText(count, 32, end.Rua);
            //Municipio - Estado
            count = 22;
            lPrinter.PrintText(count, 32, end.Cidade.Nome);
            lPrinter.PrintText(count, 113, end.Cidade.UF.Sigla);
            //Praça de Pagamento ?
            count = 23;
            lPrinter.PrintText(count, 32, end.Cidade.Nome);
            lPrinter.PrintText(count, 113, end.Cidade.UF.Sigla);
            //CNPJ - Inscrição
            count = 24;
            lPrinter.PrintText(count, 32, objDocumento.Pessoa.CNPJ_CPF);
            if (objDocumento.Pessoa.Inscricao != null)
                lPrinter.PrintText(count, 113, objDocumento.Pessoa.Inscricao);

            //Valor Por Extenso
            count = 27;
            string extenso = ClsExtenso.Extenso_Valor(objDocumento.Valor.Value);
            lPrinter.PrintText(count, 35, lCodes.BoldOn + extenso + lCodes.BoldOff);

            //Passa para a próxima folha
            while (count <= 44)
            {
                lPrinter.PrintText(count, 0, " ");
                count++;
            }

            lPrinter.EndJob();
            lPrinter.PreviewJob();
            //lPrinter.PrintJob();
        }

        public void ImprimeDocumentosGabarito()
        {
            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;

            int count;
            lPrinter = new Reporter();
            lPrinter.Output = @"\\internet\boleto";
            lPrinter.NomeArquivo = "doc.txt";
            lPrinter.StartJob();
            count = 0;
            lPrinter.PrintText(count, 0, lCodes.Reset + lCodes.LinesInch6 + lCodes.SetPageSize(5));

            for (int i = 0; i < 29; i++)
            {
                if (i % 2 == 0)
                    lPrinter.PrintText(count++, 0, (i + 1).ToString() + "23456789012345678901234567890123456789012345678901234567890");
                else
                    lPrinter.PrintText(count++, 0, (i + 1).ToString() + "        1         2         3         5         6         7");
            }
            lPrinter.PrintText(count++, 0, "a");

            lPrinter.EndJob();

            //lPrinter.PreviewJob();
            lPrinter.PrintJob();
        }

        public object getListaTipoCliente(Modelo.DataClassesDataContext db, int? idCliente, int? idTipoDocumento, DateTime dataInicioFiltro, DateTime dataFimFiltro, DateTime dataBaseCalculo, TipoDocumentoType receberPagar)
        {
            List<Modelo.Objeto.pxyDocumentoBaixa> lista;

            if (idCliente == null && idTipoDocumento == null)
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = 0,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = 0,//(decimal)db.f_VlrJuro(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro),
                             Multa = 0,//(decimal)db.f_VlrMulta(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro, u.Acrescimo.PercMulta, u.Acrescimo.DiasMulta),
                             objAcrecimo = u.Acrescimo,
                             Saldo = (decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento,
                             ValorBaixa = (decimal)u.Saldo
                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }
            else if (idCliente > 0 && idTipoDocumento == null)
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         && u.IDPessoa == idCliente
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = 0,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = 0,//(decimal)db.f_VlrJuro(u.Acrescimo.ID, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro),
                             Multa = 0,//(decimal)db.f_VlrMulta(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro, u.Acrescimo.PercMulta, u.Acrescimo.DiasMulta),
                             objAcrecimo = u.Acrescimo,
                             Saldo = (decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento,
                             ValorBaixa = (decimal)u.Saldo
                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }
            else if (idCliente == null && idTipoDocumento > 0)
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         && u.IDTipoDocumento == idTipoDocumento
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = 0,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = 0,//(decimal)db.f_VlrJuro(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro),
                             Multa = 0,//(decimal)db.f_VlrMulta(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro, u.Acrescimo.PercMulta, u.Acrescimo.DiasMulta),
                             objAcrecimo = u.Acrescimo,
                             Saldo = (decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento,
                             ValorBaixa = (decimal)u.Saldo
                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }
            else
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         && u.IDPessoa == idCliente
                         && u.IDTipoDocumento == idTipoDocumento
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = 0,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = 0,//(decimal)db.f_VlrJuro(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro),
                             Multa = 0,//(decimal)db.f_VlrMulta(u.IDAcrescimo, u.Saldo, dataBaseCalculo, u.DtVencimento, u.Acrescimo.PercJuro, u.Acrescimo.PercMulta, u.Acrescimo.DiasMulta),
                             objAcrecimo = u.Acrescimo,
                             Saldo = (decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento,
                             ValorBaixa = (decimal)u.Saldo
                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }

            foreach (Modelo.Objeto.pxyDocumentoBaixa item in lista)
            {
                decimal? juro = db.f_VlrJuro(item.objAcrecimo.ID, item.Saldo, dataBaseCalculo, item.Vencimento, 0);
                decimal? multa = db.f_VlrMulta(item.objAcrecimo.ID, item.Saldo, dataBaseCalculo, item.Vencimento, item.objAcrecimo.PercJuro, item.objAcrecimo.PercMulta, item.objAcrecimo.DiasMulta);
                if (juro == null)
                    item.Juro = 0;
                else
                    item.Juro = juro.Value;

                if (multa == null)
                    item.Multa = 0;
                else
                    item.Multa = multa.Value;

                item.ValorCorrigido = item.ValorBaixa + item.Multa + item.Juro;
            }
            return lista;
        }

        public object getListaTipoCliente(Modelo.DataClassesDataContext db, int? idCliente, int? idTipoDocumento, DateTime dataInicioFiltro, DateTime dataFimFiltro, DateTime dataBaseCalculo, TipoDocumentoType receberPagar, decimal juro, decimal multa, decimal desconto)
        {
            List<Modelo.Objeto.pxyDocumentoBaixa> lista;

            if (idCliente == null && idTipoDocumento == null)
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = (Decimal)u.Saldo * desconto / 100,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = (Decimal)db.f_VlrJuroEspecifico(juro, u.Saldo, dataBaseCalculo, u.DtVencimento, 0),
                             Multa = (Decimal)u.Saldo * multa / 100,
                             objAcrecimo = u.Acrescimo,
                             Saldo = (Decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (Decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento

                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }
            else if (idCliente > 0 && idTipoDocumento == null)
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         && u.IDPessoa == idCliente
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = (Decimal)u.Saldo * desconto / 100,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = (Decimal)db.f_VlrJuroEspecifico(juro, u.Saldo, dataBaseCalculo, u.DtVencimento, 0),
                             Multa = (Decimal)u.Saldo * multa / 100,
                             objAcrecimo = u.Acrescimo,
                             Saldo = (Decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (Decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento

                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }
            else if (idCliente == null && idTipoDocumento > 0)
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         && u.IDTipoDocumento == idTipoDocumento
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = (Decimal)u.Saldo * desconto / 100,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = (Decimal)db.f_VlrJuroEspecifico(juro, u.Saldo, dataBaseCalculo, u.DtVencimento, 0),
                             Multa = (Decimal)u.Saldo * multa / 100,
                             objAcrecimo = u.Acrescimo,
                             Saldo = (Decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (Decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento

                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }
            else
            {
                lista = (from u in db.Documentos
                         where (u.DtVencimento >= dataInicioFiltro && u.DtVencimento <= dataFimFiltro)
                         && u.Tipo == receberPagar
                         && u.Saldo > 0
                         && u.IDTipoDocumento == idTipoDocumento
                         && u.IDPessoa == idCliente
                         select new Modelo.Objeto.pxyDocumentoBaixa
                         {
                             Baixar = false,
                             idBanco = u.IDBanco == null ? 0 : (int)u.IDBanco,
                             Cliente = u.Pessoa.Nome,
                             Codigo = u.Codigo,
                             Data = (DateTime)u.Dt,
                             Descontos = (Decimal)u.Saldo * desconto / 100,
                             DiasAtraso = 0,
                             Documento = u.NumDocumento,
                             DocumentoBase = u,
                             Filial = (int)u.IDFilial,
                             IdDocumento = u.ID,
                             Juro = (Decimal)db.f_VlrJuroEspecifico(juro, u.Saldo, dataBaseCalculo, u.DtVencimento, 0),
                             Multa = (Decimal)u.Saldo * multa / 100,
                             objAcrecimo = u.Acrescimo,
                             Saldo = (Decimal)u.Saldo,
                             Tipo = u.Tipo,
                             ValorCorrigido = 0,
                             ValorDoc = (Decimal)u.Valor,
                             Vencimento = (DateTime)u.DtVencimento

                         }).ToList<Modelo.Objeto.pxyDocumentoBaixa>();
            }

            foreach (Modelo.Objeto.pxyDocumentoBaixa item in lista)
            {
                item.ValorCorrigido = item.Saldo + item.Multa + item.Juro - item.Descontos;
            }

            return lista;
        }

        public List<pxyDocumentoLista> getListaTipoReceberPagar(Modelo.DataClassesDataContext db, TipoDocumentoType tipo, bool mostrarBaixados)
        {
            List<pxyDocumentoLista> lista;

            Modelo.Configuracao objConfiguracao = (from c in db.Configuracaos
                                                   where c.Codigo == 1
                                                   select c).Single();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  DOC.Codigo as Codigo");
            sql.AppendLine("        , FIL.Codigo as Filial");
            sql.AppendLine("        , DOC.DtVencimento as Vencimento");
            sql.AppendLine("        , DOC.DtPrevisao as Previsao");
            sql.AppendLine("        , DOC.NumDocumento as Doc");
            sql.AppendLine("        , ISNULL(DOC.NumRequisicao,'') as Requisicao");
            sql.AppendLine("        , PES.Nome as Cliente");
            sql.AppendLine("        , DOC.Valor as Valor");
            sql.AppendLine("        , DOC.Saldo as Saldo");
            sql.AppendLine("        , DOC.Situacao as Situacao");
            sql.AppendLine("        , DOC.DtUltimaBaixa as DtUltimaBaixa");
            sql.AppendLine("        , PES.Fantasia as NomeFantasia");
            sql.AppendLine("        , DOC.ID as ID");
            sql.AppendLine("        , TP.DescReduzida as TipoDocumento");
            sql.AppendLine("        , DOC.Dt as Emissao");
            sql.AppendLine("        , POR.Nome as Portador");
            sql.AppendLine("        , CAST(DOC.Parcela as VARCHAR)+' / '+CAST(DOC.QtParcela as VARCHAR) as Parcela");
            sql.AppendLine(" FROM Documento DOC");
            sql.AppendLine(" INNER JOIN Filial FIL ON FIL.ID = DOC.IDFilial");
            sql.AppendLine(" INNER JOIN Pessoa PES ON PES.ID = DOC.IDPessoa");
            sql.AppendLine(" INNER JOIN TipoDocumento TP ON TP.ID = DOC.IDTipoDocumento");
            sql.AppendLine(" INNER JOIN Portador POR ON POR.ID = DOC.IDPortador");

            if (tipo == TipoDocumentoType.Receber)
                sql.AppendLine(" WHERE DOC.Tipo = 1");
            else
                sql.AppendLine(" WHERE DOC.Tipo = 2");

            if (mostrarBaixados == false)
            {
                if (objConfiguracao.ListagemAbertos > 0)
                    sql.AppendLine(" AND DOC.DtVencimento >= DATEADD(YYYY, -" + objConfiguracao.ListagemAbertos.ToString() + ", GETDATE())");

                if (objConfiguracao.bListarBaixados)
                    sql.AppendLine(" AND DOC.Saldo >= 0");
                else
                    sql.AppendLine(" AND DOC.Saldo > 0");
            }
            else
            {
                if (objConfiguracao.ListagemBaixados > 0)
                    sql.AppendLine(" AND DOC.DtVencimento >= DATEADD(YYYY, -" + objConfiguracao.ListagemBaixados.ToString() + ", GETDATE())");

                sql.AppendLine(" AND DOC.Saldo = 0");
            }

            object[] param = new object[0];
            lista = db.ExecuteQuery<pxyDocumentoLista>(sql.ToString(), param).ToList();
            return lista;
        }

        public void SetImpresso(Modelo.DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                item.bImpresso = true;
            }
            db.SubmitChanges();
        }

        public void SetPDFGerado(Modelo.DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                item.bGeradoPDF = true;
            }

            db.SubmitChanges();
        }

        public void SetRemessaGerada(Modelo.DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                item.bGeradoRemessa = true;
            }

            db.SubmitChanges();
        }

        public void SetEmailEnviado(Modelo.DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                item.bEnviadoEmail = true;
            }

            db.SubmitChanges();
        }

        public String VerificaMarcadoImpressao(DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            StringBuilder Str = new StringBuilder();
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID) && (d.bImpresso != null || d.bImpresso != false)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                Str.AppendLine("Código " + item.Codigo + " Cliente " + item.Pessoa.Nome);
            }
            return Str.ToString();
        }

        public String VerificaMarcadoPDF(DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            StringBuilder Str = new StringBuilder();
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID) && (d.bGeradoPDF != null || d.bGeradoPDF != false)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                Str.AppendLine("Código " + item.Codigo + " Cliente " + item.Pessoa.Nome);
            }
            return Str.ToString();
        }

        public String VerificaMarcadoEmail(DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            StringBuilder Str = new StringBuilder();
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID) && (d.bEnviadoEmail != null || d.bEnviadoEmail != false)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                Str.AppendLine("Código " + item.Codigo + " Cliente " + item.Pessoa.Nome);
            }
            return Str.ToString();
        }

        public String VerificaMarcadoRemessa(DataClassesDataContext db, List<int> pIDs)
        {
            List<Modelo.Documento> Lista;
            StringBuilder Str = new StringBuilder();
            Lista = (from d in db.Documentos
                     where pIDs.Contains(d.ID) && (d.bGeradoRemessa != null || d.bGeradoRemessa != false)
                     select d).ToList();
            foreach (Modelo.Documento item in Lista)
            {
                Str.AppendLine("Código " + item.Codigo + " Cliente " + item.Pessoa.Nome);
            }
            return Str.ToString();
        }

        public List<Modelo.Objeto.pxyDuplicata> GetDocumentosFinanceiro(DataClassesDataContext db, int IDNota)
        {
            List<Modelo.Objeto.pxyDuplicata> ListaDocs = new List<Modelo.Objeto.pxyDuplicata>();
            bool Cobranca = EnderecoCobranca(IDNota);

            ListaDocs = (from d in db.Documentos
                         join n in db.Notas on d.IDNota equals n.ID // Endereco PessoaNota
                         join p in db.Pessoas on d.IDPessoa equals p.ID //Pessoa
                         join pe in db.Enderecos on d.IDPessoa equals pe.IDPessoa//Endereco Pessoa   
                         join cp in db.Cidades on pe.IDCidade equals cp.ID // Cidade Pessoa
                         join puf in db.UFs on cp.IDUF equals puf.ID // UF pessoa
                         join f in db.Filials on d.IDFilial equals f.ID // Empresa
                         join ce in db.Cidades on f.IDCidade equals ce.ID // Cidade Filial
                         join euf in db.UFs on ce.IDUF equals euf.ID //UF Filial
                         where d.IDNota == IDNota && (Cobranca ? pe.bCobranca == 1 : true)
                         select new Modelo.Objeto.pxyDuplicata
                         {
                             Local = "",
                             DataEmissao = d.Dt == null ? DateTime.Now : d.Dt,
                             ValorFatura = d.ValorTotal == null ? 0 : d.ValorTotal,
                             ValorDuplicata = d.Valor == null ? 0 : d.Valor,
                             NumFatura = d.NumDocumento == null ? "" : d.NumDocumento,
                             NumOrdemDuplicata = (d.NumDocumento == null ? "" : d.NumDocumento.ToString()) + "/" + d.DtVencimento.Value.Year.ToString().Substring(2, 4) + (d.Parcela == null ? "" : "-" + d.Parcela.ToString()),
                             Vencimento = d.DtVencimento == d.Dt ? "CONTRA APRESENTAÇÃO" : d.DtVencimento.ToString(),
                             NomeSacado = p.Nome == null ? "" : p.Nome,
                             EnderecoSacado = (n.PessoaEndereco == null ? "" : n.PessoaEndereco) + (n.PessoaBairro == null ? "" : " - " + n.PessoaBairro) + (n.PessoaCEP == null ? "" : " - CEP: " + n.PessoaCEP),
                             CidadeSacado = n.PessoaCidade == null ? "" : n.PessoaCidade,
                             UFSacado = n.PessoaUF == null ? "" : n.PessoaUF,
                             PracaSacado = (pe.Rua == null ? "" : pe.Rua) + (pe.Numero == null ? "" : ", " + pe.Numero) + (pe.Complemento == null ? "" : ", " + pe.Complemento) + (pe.Bairro == null ? "" : " - " + pe.Bairro) + (pe.CEP == null ? "" : " - CEP " + pe.CEP) + (pe.Cidade.Nome == null ? "" : " - " + pe.Cidade.Nome),
                             InscrSacado = p.Inscricao == null ? "" : p.Inscricao,
                             CNPJSacado = p.CNPJ_CPF == null ? "" : p.CNPJ_CPF,
                             UFPracaPgtoSacado = pe.Cidade.UF == null ? "" : pe.Cidade.UF.Nome,
                             InscrEstSacado = "",
                             NomeEmpresa = f.Nome == null ? "" : f.Nome,
                             EnderecoEmpresa = (f.Endereco == null ? "" : f.Endereco + ", "),
                             NumEmpresa = f.Numero == null ? "" : f.Numero,
                             BairroEmpresa = (f.Numero == null ? "" : "N° " + f.Numero) + (f.Bairro == null ? "" : " - " + f.Bairro),
                             CidadeEmpresa = ce.Nome == null ? "" : ce.Nome,
                             UFEmpresa = euf.Nome == null ? "" : euf.Nome,
                             FoneEmpresa = (f.Telefone == null ? "" : "FONE: " + f.Telefone) + (f.Fax == null ? "" : " - FAX: " + f.Fax),
                             FaxEmpresa = f.Fax == null ? "" : f.Fax,
                             CNPJEmpresa = (f.CNPJ == null ? "" : "CNPJ: " + f.CNPJ),
                             CEPEmpresa = (f.CEP == null ? "" : "CEP " + f.CEP) + (ce.Nome == null ? "" : " - " + ce.Nome) + (ce.UF == null ? "" : " - " + ce.UF.Nome),
                             InscricaoEstadual = (f.Inscricao == null ? "" : "I.EST. " + f.Inscricao),
                             OrderVencimento = d.DtVencimento
                         }).OrderBy(p => p.OrderVencimento).ToList();
            return ListaDocs;
        }

        private bool EnderecoCobranca(int? IDNota)
        {
            Int32 cobranca;
            cobranca = (from n in db.Notas
                        join p in db.Pessoas on n.IDPessoa equals p.ID
                        join pe in db.Enderecos on p.ID equals pe.IDPessoa
                        where n.ID == IDNota && pe.bCobranca == 1
                        select pe.ID).Count();

            if (cobranca > 0)
                return true;
            else
                return false;
        }
    }
}