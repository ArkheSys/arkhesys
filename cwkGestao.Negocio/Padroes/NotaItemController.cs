using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Negocio
{
    public class NotaItemController : BaseController<NotaItem>
    {
        #region Singleton
        private static NotaItemController instance;
        private static IRepositorioNotaItem repositorioNotaItem;

        private NotaItemController()
        { }

        static NotaItemController()
        {
            instance = new NotaItemController();
            repositorioT = RepositorioFactory<NotaItem>.GetRepositorio();
            repositorioNotaItem = (IRepositorioNotaItem)repositorioT;
        }

        public static NotaItemController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(NotaItem objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public IcmsBase RecuperaIcmsOriginal(NotaItem notaItem)
        {
            Nota nota = notaItem.Nota;
            nota.BPessoaContribuinte = nota.Pessoa.BContribuinte;
            if (nota.PessoaUF.IsNullOrEmpty())
                nota.PessoaUF = nota.Pessoa.Estado;
            return RecuperaIcmsOriginal(nota.Ent_Sai, nota.Filial.Cidade.UF.Sigla, nota.PessoaUF, nota.BPessoaContribuinte.Value, notaItem.Produto, notaItem);
        }

        public IcmsBase RecuperaIcmsOriginal(InOutType ent_sai, string filialUF, string pessoaUF, bool pessoaContribuinte, Produto produto, NotaItem _notaitem)
        {
            string siglaUfOrigem = "";
            string siglaUfDestino = "";

            if (ent_sai == InOutType.Entrada)
            {
                siglaUfOrigem = pessoaUF;
                siglaUfDestino = filialUF;
            }
            else
            {
                siglaUfOrigem = filialUF;
                siglaUfDestino = pessoaUF;
            }

            IcmsBase icms = repositorioNotaItem.RecuperaIcmsOriginal(siglaUfOrigem, siglaUfDestino, produto, _notaitem);
            if (icms == null)
            {
                throw new InvalidOperationException("Icms não configurado para o estado " + pessoaUF);
            }
            return icms;
        }

        public decimal BuscaValorDoCustoMedio(LocalEstoque local, Produto produto)
        {
            var estoque = EstoqueController.Instancia.GetEstoque(local, produto);
            return estoque.CustoMedioUnit;
        }

        public string ConverterIndiceParaCSOSN(int? indiceCSOSN)
        {
            return cwkGestao.Repositorio.Util.ConversorCsts.ConverterIndiceParaCSOSN(indiceCSOSN);
        }

        /// <summary>
        /// Retorna o saldo do estoque de acordo com os filtros informados
        /// </summary>
        /// <param name="identificador">FILIAL - Consulta por filial; LOCALESTOQUE - Consulta por local estoque</param>
        /// <param name="id">id da filial ou do local estoque</param>
        /// <param name="idProduto">id do produto</param>
        /// <returns></returns>
        public decimal SaldoEstoque(string identificador, int id, int idProduto)
        {
            return repositorioNotaItem.SaldoEstoque(identificador, id, idProduto);
        }

        /// <summary>
        /// Retorna o saldo do estoque em uma determinada filial
        /// </summary>
        /// <param name="idFilial">id da filial</param>
        /// <param name="idProduto">id do produto</param>
        /// <returns></returns>
        public decimal SaldoEstoqueFilial(int idFilial, int idProduto)
        {
            return repositorioNotaItem.SaldoEstoqueFilial(idFilial, idProduto);
        }

        /// <summary>
        /// Retorna o saldo do estoque em uma determinada filial na data informada 
        /// </summary>
        /// <param name="idFilial">id da filial</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="data">data da consulta</param>
        /// <returns></returns>
        public decimal SaldoEstoqueFilialData(int idFilial, int idProduto, DateTime data, int idNota)
        {
            return repositorioNotaItem.SaldoEstoqueFilialData(idFilial, idProduto, data, idNota);
        }

        /// <summary>
        /// Retorna o saldo do estoque em uma determinada filial e um determinado identificador
        /// </summary>
        /// <param name="idFilial">id da filial</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="idIdentificador">id do identificador</param>
        /// <returns></returns>
        public decimal SaldoEstoqueFilialIdentificador(int idFilial, int idProduto, string idIdentificador)
        {
            return repositorioNotaItem.SaldoEstoqueFilialIdentificador(idFilial, idProduto, idIdentificador);
        }

        /// <summary>
        /// Retorna o saldo do estoque em uma determinada filial e um determinado identificador na data informada
        /// </summary>
        /// <param name="idFilial">id da filial</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="idIdentificador">id do identificador</param>
        /// <param name="data">data da consulta</param>
        /// <returns></returns>
        public decimal SaldoEstoqueFilialIdentificadorData(int idFilial, int idProduto, string idIdentificador, DateTime data)
        {
            return repositorioNotaItem.SaldoEstoqueFilialIdentificadorData(idFilial, idProduto, idIdentificador, data);
        }

        /// <summary>
        /// Retorna o saldo do estoque em um determinado local
        /// </summary>
        /// <param name="idLocalEstoque">id do local estoque</param>
        /// <param name="idProduto">id do produto</param>
        /// <returns></returns>
        public decimal SaldoEstoqueLocalEstoque(int idLocalEstoque, int idProduto)
        {
            return repositorioNotaItem.SaldoEstoqueLocalEstoque(idLocalEstoque, idProduto);
        }

        /// <summary>
        /// Retorna o saldo do estoque em um determinado local na data informada
        /// </summary>
        /// <param name="idLocalEstoque">id do local estoqu</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="data">data da consulta</param>
        /// <returns></returns>
        public decimal SaldoEstoqueLocalEstoqueData(int idLocalEstoque, int idProduto, DateTime data)
        {
            return repositorioNotaItem.SaldoEstoqueLocalEstoqueData(idLocalEstoque, idProduto, data);
        }

        /// <summary>
        /// Retorna o saldo do estoque em um determinado local e em determinado identificador
        /// </summary>
        /// <param name="idLocalEstoque">id do local estoqu</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="idIdentificador">id do identificador</param>
        /// <returns></returns>
        public decimal SaldoEstoqueLocalEstoqueIdentificador(int idLocalEstoque, int idProduto, int idIdentificador)
        {
            return repositorioNotaItem.SaldoEstoqueLocalEstoqueIdentificador(idLocalEstoque, idProduto, idIdentificador);
        }

        /// <summary>
        /// Retorna o saldo do estoque em um determinado local e em determinado identificador na data informada
        /// </summary>
        /// <param name="idLocalEstoque">id do local estoqu</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="idIdentificador">id do identificador</param>
        /// <param name="data">data da consulta</param>
        /// <returns></returns>
        public decimal SaldoEstoqueLocalEstoqueIdentificadorData(int idLocalEstoque, int idProduto, int idIdentificador, DateTime data)
        {
            return repositorioNotaItem.SaldoEstoqueLocalEstoqueIdentificadorData(idLocalEstoque, idProduto, idIdentificador, data);
        }

        public IList<object> SaldoEstoqueFilialListaIdentificador(int idFilial, int idProduto)
        {
            return repositorioNotaItem.SaldoEstoqueFilialListaIdentificador(idFilial, idProduto);
        }

        public IList<object> SaldoEstoqueFilialListaIdentificadorData(int idFilial, int idProduto, DateTime data)
        {
            return repositorioNotaItem.SaldoEstoqueFilialListaIdentificadorData(idFilial, idProduto, data);
        }

        public IList<pxyEstoqueIdentificador> ListaTodosIdentificadores(int idLocalEstoque, int idProduto, int idNota)
        {
            var identificadoresEstoque = repositorioNotaItem.SaldoEstoqueLocalEstoqueListaIdentificador(idLocalEstoque, idProduto, idNota);
            if (ConfiguracaoController.Instancia.GetConfiguracao().ModoBaixaEstoque == ModoBaixaEstoqueType.PEPS)
                return identificadoresEstoque.OrderBy(i => i.Identificador).ToList();
            else
                return identificadoresEstoque.OrderByDescending(i => i.Identificador).ToList();
        }

        public IList<pxyEstoqueIdentificador> SaldoEstoqueLocalEstoqueListaIdentificador(int idLocalEstoque, int idProduto, int idNota, bool expedicao)
        {
            var identificadoresEstoque = repositorioNotaItem.SaldoEstoqueLocalEstoqueListaIdentificador(idLocalEstoque, idProduto, idNota);
            List<pxyEstoqueIdentificador> pxyIdentificador = new List<pxyEstoqueIdentificador>();

            //pega somente os identificadores que não foram verificados.
            foreach (var item in identificadoresEstoque)
            {
                var _identificador = IdentificadorController.Instancia.LoadObjectById(item.IdIdentificador);
                
                if (!_identificador.bVerificado)
                {
                    pxyIdentificador.Add(item);
                }

            }

            if (ConfiguracaoController.Instancia.GetConfiguracao().ModoBaixaEstoque == ModoBaixaEstoqueType.PEPS)
            {
                if(expedicao)
                  return pxyIdentificador.OrderBy(i => i.Identificador).ToList();
                else
                  return identificadoresEstoque.OrderBy(i => i.Identificador).ToList();
            }
            else
            {
                if(expedicao)
                  return pxyIdentificador.OrderByDescending(i => i.Identificador).ToList();
                else
                  return identificadoresEstoque.OrderByDescending(i => i.Identificador).ToList();
            }
        }

        public IList<object> SaldoEstoqueLocalEstoqueListaIdentificadorData(int idLocalEstoque, int idProduto, DateTime data)
        {
            return repositorioNotaItem.SaldoEstoqueLocalEstoqueListaIdentificadorData(idLocalEstoque, idProduto, data);
        }

        public IList<pxyMovimentoEstoqueProduto> GetMovimentosEstoque(int idProduto, int idLocalEstoque)
        {
            return repositorioNotaItem.GetMovimentosEstoque(idProduto, idLocalEstoque);
        }
    }
}
