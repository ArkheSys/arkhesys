using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.Importacao;
using System.Threading;
using System.Web;

namespace cwkGestao
{
    public class PedidoMagentoThread
    {
        private MagentoConfiguracao configuracao;
        private Buscador importador;
        private Filial filial;

        public PedidoMagentoThread()
        {
            Executar = true;
        }

        public void ExecutarThread()
        {
            configuracao = MagentoConfiguracaoController.Instancia.GetConfiguracao();
            importador = new Buscador(configuracao.Usuario, configuracao.Senha, null);
            filial = FilialController.Instancia.GetFilialPrincipal();

            ImportadorPedidos importadorPedidos = new ImportadorPedidos(configuracao, importador, filial);

            while (Executar)
            {
                try
                {
                    importadorPedidos.Importar();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(300000);
                }
            }
        }

        public bool Executar { get; set; }
    }

}
