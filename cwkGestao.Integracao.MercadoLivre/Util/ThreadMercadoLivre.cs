using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.MercadoLivre.Negocio;
using cwkGestao.Negocio;
using System.Threading;
using cwkGestao.Integracao.MercadoLivre.WebService;

namespace cwkGestao.Integracao.MercadoLivre.Util
{
    public class ThreadMercadoLivre
    {
        ImportadorPedidos import = new ImportadorPedidos();
        MercadoLivreConfiguracao mConf = MercadoLivreConfiguracaoController.Instancia.GetAll().First();
        public bool rodar = true;

        public void ExecutarThread(MercadoLivreConfiguracao mConf){
            while (rodar)
            {
                try
                {
                    import.ImportarPedidosMercadoLivre();

                }
                catch (Exception)
                {

                }
                finally
                {
                    Thread.Sleep(mConf.TempoSinc);
                }  
            }
        }
    }
}
