using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Negocio;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Integracao
{
    public class BConfiguraçãoSisECommerce
    {
        public void Importar(out string retornoErro)
        {
            IList<Pedido> listaPedidosNovos = new List<Pedido>();
            IList<object> listaPedidosSis = new List<object>();
#pragma warning disable CS0219 // A variável "houveErro" é atribuída, mas seu valor nunca é usado
            bool houveErro = false;
#pragma warning restore CS0219 // A variável "houveErro" é atribuída, mas seu valor nunca é usado
            retornoErro = "";

            foreach (var item in listaPedidosSis)
            {
                try
                {
                    Pedido p = ConvertePedidoGestao(item);
                }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
                catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
                {
                    houveErro = true;
                    AdicionaPedidoSincronismoPendente(item);
                    retornoErro += item + "\n";
                }
            }
        }

        private void AdicionaPedidoSincronismoPendente(object item)
        {
            SincronismoPendenteSisECommerce sincPendente = new SincronismoPendenteSisECommerce();
            SincronismoPendenteSisECommerceController.Instancia.Salvar(sincPendente, Acao.Incluir);
        }

        private Pedido ConvertePedidoGestao(object item)
        {
            try
            {
                return new Pedido();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
