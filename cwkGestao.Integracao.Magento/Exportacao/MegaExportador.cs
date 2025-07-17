using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Magento.WebService;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Exceptions;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class MegaExportador : IDisposable
    {
        private readonly MagentoConfiguracao configuracao;
        private readonly Comunicador comunicador;
        private ExportadorCategoria exportadorCategoria;
        private ExportadorProdutoReferencia exportadorProduto;
        private ExportadorProdutoEstoque exportadorProdutoEstoque;
        private ExportadorPessoa exportadorPessoa;
        private ExportadorPedidoStatusEntrega exportadorPedidoStatusEntrega;
        private ExportadorPedidoStatusFatura exportadorPedidoStatusFatura;
        private ExportarCodigoRastreamento exportadorCodigoRastreamento;

        public MegaExportador()
        {
            configuracao = MagentoConfiguracaoController.Instancia.GetConfiguracao();

            if (configuracao == null)
                throw new ConfiguracaoIncorretaException("Configure os parâmetros de integração com o sistema magento.");

            comunicador = new Comunicador(configuracao.Usuario, configuracao.Senha);
            exportadorCategoria = new ExportadorCategoria(configuracao, comunicador);
            exportadorProduto = new ExportadorProdutoReferencia(comunicador);
            exportadorProdutoEstoque = new ExportadorProdutoEstoque(comunicador);
            exportadorPessoa = new ExportadorPessoa(comunicador);
            exportadorPedidoStatusEntrega = new ExportadorPedidoStatusEntrega(comunicador);
            exportadorPedidoStatusFatura = new ExportadorPedidoStatusFatura(comunicador);
            exportadorCodigoRastreamento = new ExportarCodigoRastreamento(comunicador);
            
        }

        public static void ExportarUnicoObjeto(ModeloBase entidade)
        {
            try
            {
                var exportador = new MegaExportador();
                    exportador.Exportar(entidade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        public void Exportar(object entidade)
        {
            var config = ConfiguracaoController.Instancia.GetConfiguracao();
            var tipo = entidade.GetType();

            switch (tipo.Name)
            {
                case "Produto":
                    exportadorProduto.Exportar((Produto)entidade);
                    if (!exportadorProduto.StatusExportacao())
                        throw new Exception("Produto inexistente na lista de produtos do Magento. Verifique.");
                    break;
                case "Balanco":
                    exportadorProdutoEstoque.ExportarBalanco((Balanco)entidade);
                    break;
                case "Nota":
                    exportadorProdutoEstoque.Exportar((Nota)entidade);
                    break;
                case "GrupoEstoque":
                    exportadorCategoria.Exportar((GrupoEstoque)entidade);
                    break;
                case "Pessoa":
                    exportadorPessoa.Exportar((Pessoa)entidade);
                    break;
                case "Pedido":
                    exportadorPedidoStatusEntrega.Exportar((Pedido)entidade);
                    break;
                case "Documento":
                    exportadorPedidoStatusFatura.Exportar((Documento)entidade);
                    break;
                case "ExpedicaoRastreamento":
                    exportadorCodigoRastreamento.Exportar((ExpedicaoRastreamento)entidade);
                    break;

                default:

                    break;
            }

        }

        public void Dispose()
        {
            exportadorProduto = null;
            exportadorCategoria = null;
            exportadorPessoa = null;
            exportadorProdutoEstoque = null;
            exportadorPedidoStatusEntrega = null;
            exportadorPedidoStatusFatura = null;

            comunicador.Dispose();
        }
    }
}
