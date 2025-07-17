using System;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Exceptions;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Magento.Importacao
{
    public class GerenciadorImportacao
    {
        private readonly bool importarProdutos;
        private readonly bool importarClientes;
        private readonly bool importarPedidos;
        private readonly bool importarPedidoUnico;

        private MagentoConfiguracao configuracao;
        private Buscador importador;
        private Filial filial;

        public GerenciadorImportacao(bool produtos, bool clientes, bool pedidos, bool pedidounico, Action<string> atualizadorContador)
        {
            configuracao = MagentoConfiguracaoController.Instancia.GetConfiguracao();

            if (configuracao == null)
                throw new ConfiguracaoIncorretaException("Configure os parâmetros de integração com o sistema magento.");

            importarProdutos = produtos;
            importarClientes = clientes;
            importarPedidos = pedidos;
            importarPedidoUnico = pedidounico;

            filial = FilialController.Instancia.GetFilialPrincipal();
            importador = new Buscador(configuracao.Usuario, configuracao.Senha, atualizadorContador);
        }

        public void Importar()
        {
            if (importarProdutos)
                ImportarProdutos();

            if (importarClientes)
                ImportarClientes();

            if (importarPedidos)
                ImportarPedidos();
        }

        private void ImportarProdutos()
        {
            ImportadorProdutos imp = new ImportadorProdutos(configuracao, importador, filial);
            imp.Importar();
        }

        private void ImportarClientes()
        {
            ImportadorClientes imp = new ImportadorClientes(configuracao, importador, filial);
            imp.Importar();
        }

        private void ImportarPedidos()
        {
            ImportadorPedidos imp = new ImportadorPedidos(configuracao, importador, filial);
            imp.Importar();
        }
    }
}
