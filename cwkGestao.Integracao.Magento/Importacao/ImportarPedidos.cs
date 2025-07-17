using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using Ez.Newsletter.MagentoApi;
using cwkGestao.Integracao.Magento.Utils;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using cwkGestao.Integracao.Magento.Exportacao;
using cwkGestao.Integracao.Magento.WebService;

namespace cwkGestao.Integracao.Magento.Importacao
{
    public class ImportadorPedidos
    {
        private readonly MagentoConfiguracao configuracao;
        private readonly Buscador importador;
        private Filial filial;

        private PedidosMagento importarPedidoMagento;
        private salesOrderEntity pedidoMagento;
        private Comunicador Comunicador;

        public ImportadorPedidos(MagentoConfiguracao configuracao, Buscador importador, Filial filial)
        {
            this.configuracao = configuracao;
            this.importador = importador;
            this.filial = filial;
            this.Comunicador = new Comunicador(configuracao.Usuario, configuracao.Senha);

            importarPedidoMagento = new PedidosMagento(configuracao, importador, filial);
        }

        public void Importar()
        {
            foreach (salesOrderEntity pedidoRequisitado in importador.ImportarPedidos())
            {
                if (importarPedidoMagento.DesconsiderarPedido(pedidoRequisitado))
                    continue;

                    importarPedidoMagento.IncluirPedido(pedidoRequisitado);
            }
        }

        public void ImportarPedidosPendentes(string id)
        {
           var _PedidoPendente = Comunicador.RequisitarPedido(id);
           importarPedidoMagento.IncluirPedido(_PedidoPendente);
        }

        public void Importar(string id)
        {
            pedidoMagento = importador.ImportarPedido(id);

            if (importarPedidoMagento.DesconsiderarPedido(pedidoMagento))
                return;
            else
                importarPedidoMagento.IncluirPedido(pedidoMagento);
        }
    }
}